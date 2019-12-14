using System;
using System.Collections.Generic;

namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Execute consistently <paramref name="action"/> for each item in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ApiResponse<Unit> ForEach<T>(
            this IEnumerable<T> source,
            Func<T, ApiResponse<Unit>> action
        )
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var item in source)
            {
                var response = action(item);

                if (response.HasErrors)
                {
                    return response;
                }
            }

            return ApiResponse.Ok();
        }
    }
}
