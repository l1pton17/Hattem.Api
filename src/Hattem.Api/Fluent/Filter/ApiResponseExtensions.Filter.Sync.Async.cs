using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
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
    }
}
