#if NETCOREAPP3_1
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace Hattem.Api.NewtonsoftJson.DependencyInjection
{
    public static class MvcNewtonsoftJsonOptionsExtensions
    {
        public static void AddHattemJsonConverters(this MvcNewtonsoftJsonOptions options)
        {
            options.SerializerSettings.AddHattemConverters();
        }
    }
}
#endif