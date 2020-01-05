using System;
using Hattem.Api.NewtonsoftJson.Converters;
using Hattem.Api.NewtonsoftJson.Tests.Framework;
using Hattem.Api.NewtonsoftJson.Tests.Framework.Comparers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace Hattem.Api.NewtonsoftJson.Tests.Converters
{
    [CategoryTrait(nameof(ApiResponseJsonConverter) + " Tests")]
    public sealed class ApiResponseJsonConverterTests
    {
        public sealed class ErrorData
        {
            public string Data { get; set; } = Guid.NewGuid().ToString();
        }

        public sealed class ErrorWithData : Error<ErrorData>
        {
            public ErrorWithData(string code, string description, ErrorData data)
                : base(code, description, data)
            {
            }
        }

        [Theory(DisplayName = "Should emit only data for ok response")]
        [InlineData(NullValueHandling.Ignore)]
        [InlineData(NullValueHandling.Include)]
        public void HasErrors_Data_EmitAsJToken(NullValueHandling nullValueHandling)
        {
            var expectedErrorData = new ErrorData();
            var expectedError = new ErrorWithData("c", "d", expectedErrorData);
            var response = ApiResponse.Error(expectedError);
            var jObject = Serialize(nullValueHandling, response);
            var jProperty = Assert.Single(jObject.Properties());
            var jsonSerializer = CreateJsonSerializer();

            Assert.NotNull(jProperty);
            Assert.Equal(ApiResponseConstants.Error, jProperty.Name);

            var actualError = jProperty.Value.ToObject<Error>(jsonSerializer);

            Assert.Equal(expectedError, actualError, ErrorComparer.Default);
            Assert.NotNull(actualError.Data);

            var errorDataJToken = Assert.IsAssignableFrom<JToken>(actualError.Data);

            Assert.NotNull(errorDataJToken);

            var actualErrorData = errorDataJToken[nameof(ErrorData.Data).ToLowerInvariant()].Value<string>();

            Assert.Equal(expectedErrorData.Data, actualErrorData);
        }

        [Theory(DisplayName = "Should emit only data for ok response")]
        [InlineData(NullValueHandling.Ignore)]
        [InlineData(NullValueHandling.Include)]
        public void IsOk_EmitOnlyData(NullValueHandling nullValueHandling)
        {
            var response = ApiResponse.Ok(2);
            var jObject = Serialize(nullValueHandling, response);
            var jProperty = Assert.Single(jObject.Properties());

            Assert.NotNull(jProperty);
            Assert.Equal(ApiResponseConstants.Data, jProperty.Name);
        }

        [Theory(DisplayName = "Should emit only error for error response")]
        [InlineData(NullValueHandling.Ignore)]
        [InlineData(NullValueHandling.Include)]
        public void HasErrors_EmitOnlyError(NullValueHandling nullValueHandling)
        {
            var expectedError = new Error("c", "d");
            var response = ApiResponse.Error(expectedError);
            var jObject = Serialize(nullValueHandling, response);
            var jProperty = Assert.Single(jObject.Properties());
            var jsonSerializer = CreateJsonSerializer();

            Assert.NotNull(jProperty);
            Assert.Equal(ApiResponseConstants.Error, jProperty.Name);

            var actualError = jProperty.Value.ToObject<Error>(jsonSerializer);

            Assert.Equal(expectedError, actualError, ErrorComparer.Default);
        }

        private JObject Serialize<T>(
            NullValueHandling nullValueHandling,
            ApiResponse<T> response
        )
        {
            var jsonSerializerSettings = CreateJsonSerializerSettings(nullValueHandling);
            var raw = JsonConvert.SerializeObject(response, jsonSerializerSettings);
            var jObject = JObject.Parse(raw);

            return jObject;
        }

        private JsonSerializer CreateJsonSerializer()
        {
            var settings = CreateJsonSerializerSettings(NullValueHandling.Ignore);

            return JsonSerializer.Create(settings);
        }

        private JsonSerializerSettings CreateJsonSerializerSettings(
            NullValueHandling nullValueHandling)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                NullValueHandling = nullValueHandling,
                Converters =
                {
                    new ApiResponseJsonConverter(),
                    new ApiResponseErrorJsonConverter()
                }
            };

            return jsonSerializerSettings;
        }
    }
}