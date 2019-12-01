using System;
using System.Threading.Tasks;

namespace Hattem.Api.Extensions
{
    partial class ApiResponseExtensions
    {
        public static ApiResponse<TInput> Filter<TInput>(
            this ApiResponse<TInput> source,
            Func<TInput, ApiResponse<Unit>> predicate
        )
        {
            if (source.HasErrors)
            {
                return source;
            }

            var predicateResponse = predicate(source.Data);

            if (predicateResponse.HasErrors)
            {
                return predicateResponse.Cast(To<TInput>.Type);
            }

            return source;
        }

        public static async Task<ApiResponse<TInput>> Filter<TInput>(
            this Task<ApiResponse<TInput>> source,
            Func<TInput, ApiResponse<Unit>> predicate
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Filter(predicate);
        }

        public static async Task<ApiResponse<TInput>> Filter<TInput>(
            this ApiResponse<TInput> source,
            Func<TInput, Task<ApiResponse<Unit>>> predicate
        )
        {
            if (source.HasErrors)
            {
                return source;
            }

            var predicateResponse = await predicate(source.Data).ConfigureAwait(false);

            if (predicateResponse.HasErrors)
            {
                return predicateResponse.Cast(To<TInput>.Type);
            }

            return source;
        }

        public static async Task<ApiResponse<TInput>> Filter<TInput>(
            this Task<ApiResponse<TInput>> source,
            Func<TInput, Task<ApiResponse<Unit>>> predicate
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Filter(predicate)
                .ConfigureAwait(false);
        }
    }
}