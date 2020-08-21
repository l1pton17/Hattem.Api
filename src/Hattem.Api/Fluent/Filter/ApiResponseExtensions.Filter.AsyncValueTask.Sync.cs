using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static async ValueTask<ApiResponse<TInput>> Filter<TInput>(
            this ValueTask<ApiResponse<TInput>> source,
            Func<TInput, ApiResponse<Unit>> predicate
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Filter(predicate);
        }
    }
}
