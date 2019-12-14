using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Execute <paramref name="action"/> for each item in <paramref name="source"/> consistently
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<Unit>> ForEach<T>(
            this IEnumerable<T> source,
            Func<T, Task<ApiResponse<Unit>>> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var item in source)
            {
                var response = await action(item).ConfigureAwait(false);

                if (response.HasErrors)
                {
                    return response;
                }
            }

            return ApiResponse.Ok();
        }
        /// Execute <paramref name="action"/> for each item in <paramref name="source"/> consistently
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<Unit>> ForEach<T>(
            this IEnumerable<T> source,
            Func<T, int, Task<ApiResponse<Unit>>> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var index = 0;

            foreach (var item in source)
            {
                var response = await action(item, index++).ConfigureAwait(false);

                if (response.HasErrors)
                {
                    return response;
                }
            }

            return ApiResponse.Ok();
        }
    }
}
