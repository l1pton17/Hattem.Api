using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
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
