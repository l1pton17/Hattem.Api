using System;
using System.Threading.Tasks;
using Hattem.Api.Extensions;

namespace Hattem.Api.Fluent
{
    public static partial class ApiResponseExtensions
    {
        public static async Task<ApiResponse<TOutput>> Then<TInput, TOutput>(
            this Task<ApiResponse<TInput>> source,
            Func<TInput, ApiResponse<TOutput>> next)
        {
            var response = await source.ConfigureAwait(false);

            return response.Then(next);
        }

        public static async Task<ApiResponse<TOutput>> Then<TInput, TOutput>(
            this Task<ApiResponse<TInput>> source,
            Func<TInput, Task<ApiResponse<TOutput>>> next)
        {
            var response = await source.ConfigureAwait(false);

            return await response.Then(next).ConfigureAwait(false);
        }

        public static ApiResponse<TOutput> Then<TInput, TOutput>(
            this ApiResponse<TInput> source,
            Func<TInput, ApiResponse<TOutput>> next)
        {
            if (source.HasErrors)
            {
                return source.Error.ToResponse(source.StatusCode, To<TOutput>.Type);
            }

            return next(source.Data);
        }

        public static async Task<ApiResponse<TOutput>> Then<TInput, TOutput>(
            this ApiResponse<TInput> source,
            Func<TInput, Task<ApiResponse<TOutput>>> evaluator)
        {
            if (source.HasErrors)
            {
                return source.Error.ToResponse(source.StatusCode, To<TOutput>.Type);
            }

            return await evaluator(source.Data).ConfigureAwait(false);
        }
    }
}
