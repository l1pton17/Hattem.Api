using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hattem.Api.NewtonsoftJson.Converters
{
    public sealed class ApiResponseJsonConverter : JsonConverter
    {
        [ThreadStatic]
        private static bool _isWriting;

        public override bool CanWrite
        {
            get
            {
                if (_isWriting)
                {
                    _isWriting = false;

                    return false;
                }

                return true;
            }
        }

        public override bool CanRead => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                _isWriting = true;

                var json = JObject.FromObject(value, serializer);
                var isUnitOrHasErrors = value is ApiResponse<Unit> || json[ApiResponseConstants.Error] != null;

                if (isUnitOrHasErrors)
                {
                    json.Remove(ApiResponseConstants.Data);
                }

                json.WriteTo(writer);
            }
            finally
            {
                _isWriting = false;
            }
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer
        )
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(ApiResponse<>))
            {
                return true;
            }

            return false;
        }
    }
}