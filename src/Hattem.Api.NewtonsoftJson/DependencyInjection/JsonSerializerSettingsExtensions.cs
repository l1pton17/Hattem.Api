using Hattem.Api.NewtonsoftJson.Converters;
using Newtonsoft.Json;

namespace Hattem.Api.NewtonsoftJson.DependencyInjection
{
    public static class JsonSerializerSettingsExtensions
    {
        public static void AddHattemConverters(this JsonSerializerSettings settings)
        {
            settings.Converters.Add(new ApiResponseJsonConverter());
            settings.Converters.Add(new ApiResponseErrorJsonConverter());
        }
    }
}
