using System;
using System.Collections.Concurrent;
using System.Net;
using System.Reflection;

namespace Hattem.Api
{
    public static class ErrorStatusCodeMapper
    {
        private static readonly ConcurrentDictionary<Type, int?> _cache = new ConcurrentDictionary<Type, int?>();

        static ErrorStatusCodeMapper()
        {
        }

        public static int Map<T>(ApiResponse<T> response)
        {
            if (response.IsOk)
            {
                return response.StatusCode
                 ?? (response.Data != null ? GetStatusCode(response.Data.GetType()) : null)
                 ?? (int) HttpStatusCode.OK;
            }

            return response.StatusCode
             ?? GetStatusCode(response.Error.GetType())
             ?? (int) HttpStatusCode.BadRequest;
        }

        private static int? GetStatusCode(Type dataType)
        {
            return _cache.GetOrAdd(
                dataType,
                type => type.GetCustomAttribute<ApiStatusCodeAttribute>()?.StatusCode);
        }
    }
}