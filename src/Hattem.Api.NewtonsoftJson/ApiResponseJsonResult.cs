using Hattem.Api.NewtonsoftJson.Converters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hattem.Api.NewtonsoftJson
{
    public sealed class ApiResponseJsonResult<T> : JsonResult
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
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

        public ApiResponseJsonResult(ApiResponse<T> value)
            : base(value, _jsonSerializerSettings)
        {
            StatusCode = ErrorStatusCodeMapper.Map(value);
        }
    }
}