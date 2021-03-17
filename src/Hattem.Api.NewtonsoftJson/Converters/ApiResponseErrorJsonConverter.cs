using System;
using Hattem.Api.NewtonsoftJson.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hattem.Api.NewtonsoftJson.Converters
{
    public sealed class ApiResponseErrorJsonConverter : JsonConverter<Error>
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, Error? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

#pragma warning disable 8764
        public override Error? ReadJson(
#pragma warning restore 8764
            JsonReader reader,
            Type objectType,
            Error? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var json = JObject.Load(reader);

            string? code = null;
            string? description = null;
            JToken? data = null;

            foreach (var property in json.Properties())
            {
                if (property.IsNameEquals(nameof(Error.Code)))
                {
                    code = property.Value.Value<string>();
                }
                else if (property.IsNameEquals(nameof(Error.Description)))
                {
                    description = property.Value.Value<string>();
                }
                else if (property.IsNameEquals(nameof(Error.Data)))
                {
                    data = property.Value;
                }
            }

            return data != null
                ? new Error(code!, description!, data)
                : new Error(code!, description!);
        }
    }
}
