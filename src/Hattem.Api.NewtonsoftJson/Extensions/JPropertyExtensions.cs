using System;
using Newtonsoft.Json.Linq;

namespace Hattem.Api.NewtonsoftJson.Extensions
{
    internal static class JPropertyExtensions
    {
        public static bool IsNameEquals(this JProperty property, string name)
        {
            return property.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
