using Hattem.Api.NewtonsoftJson.Converters;
using Hattem.Api.NewtonsoftJson.Tests.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Hattem.Api.NewtonsoftJson.Tests.Converters
{
    [CategoryTrait(nameof(ApiResponseJsonConverter) + " Tests")]
    public sealed class ApiResponseJsonConverterTests
    {


        private JObject Convert<T>(ApiResponse<T> response)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Converters =
                {
                    new ApiResponseJsonConverter()
                }
            };

            var raw = JsonConvert.SerializeObject(response, jsonSerializerSettings);
            var jObject = JObject.Parse(raw);

            return jObject;
        }
    }
}