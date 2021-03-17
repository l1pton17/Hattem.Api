using System;
using Hattem.Api.NewtonsoftJson.Extensions;
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

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();

                return;
            }

            try
            {
                _isWriting = true;

                var json = JObject.FromObject(value, serializer);
                var jsonError = json[ApiResponseConstants.Error];
                var hasError = jsonError != null && jsonError.Type != JTokenType.Null;
                var isUnitOrHasErrors = value is ApiResponse<Unit> || hasError;

                JProperty? dataPropertyToDelete = null;
                JProperty? errorPropertyToDelete = null;
                JProperty? statusCodePropertyToDelete = null;
                JProperty? isOkPropertyToDelete = null;
                JProperty? hasErrorsPropertyToDelete = null;

                foreach (var property in json.Properties())
                {
                    if (isUnitOrHasErrors && property.IsNameEquals(nameof(ApiResponse<int>.Data)))
                    {
                        dataPropertyToDelete = property;
                    }
                    else if (!hasError
                     && property.IsNameEquals(nameof(ApiResponse<int>.Error)))
                    {
                        errorPropertyToDelete = property;
                    }
                    else if (property.IsNameEquals(nameof(ApiResponse<int>.StatusCode)))
                    {
                        statusCodePropertyToDelete = property;
                    }
                    else if (property.IsNameEquals(nameof(ApiResponse<int>.IsOk)))
                    {
                        isOkPropertyToDelete = property;
                    }
                    else if (property.IsNameEquals(nameof(ApiResponse<int>.HasErrors)))
                    {
                        hasErrorsPropertyToDelete = property;
                    }
                }

                dataPropertyToDelete?.Remove();
                errorPropertyToDelete?.Remove();
                statusCodePropertyToDelete?.Remove();
                isOkPropertyToDelete?.Remove();
                hasErrorsPropertyToDelete?.Remove();

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
            object? existingValue,
            JsonSerializer serializer
        )
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsGenericType
             && objectType.GetGenericTypeDefinition() == typeof(ApiResponse<>))
            {
                return true;
            }

            return false;
        }
    }
}
