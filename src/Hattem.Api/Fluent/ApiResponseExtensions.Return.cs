using System;
using System.Threading.Tasks;
using Hattem.Api.Extensions;

namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static ApiResponse<TOutput> Return<TInput, TOutput>(
            this ApiResponse<TInput> source,
            TOutput value)
        {
            if (source.HasErrors)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            return ApiResponse.Ok(value);
        }

        public static async Task<ApiResponse<TOutput>> Return<TInput, TOutput>(
            this Task<ApiResponse<TInput>> source,
            TOutput value)
        {
            var response = await source.ConfigureAwait(false);

            return response.Return(value);
        }

        public static ApiResponse<TOutput> Return<TInput, TOutput>(
            this ApiResponse<TInput> source,
            Func<TInput, TOutput> valueFactory)
        {
            if (source.HasErrors)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            return ApiResponse.Ok(valueFactory(source.Data));
        }

        public static async Task<ApiResponse<TOutput>> Return<TInput, TOutput>(
            this Task<ApiResponse<TInput>> source,
            Func<TInput, TOutput> valueFactory)
        {
            var response = await source.ConfigureAwait(false);

            return response.Return(valueFactory);
        }

        public static async Task<ApiResponse<TOutput>> Return<TInput, TOutput>(
            this ApiResponse<TInput> source,
            Func<TInput, Task<TOutput>> valueFactory)
        {
            if (source.HasErrors)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            var value = await valueFactory(source.Data).ConfigureAwait(false);

            return ApiResponse.Ok(value);
        }

        public static async Task<ApiResponse<TOutput>> Return<TInput, TOutput>(
            this Task<ApiResponse<TInput>> source,
            Func<TInput, Task<TOutput>> valueFactory)
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Return(valueFactory)
                .ConfigureAwait(false);
        }
    }
}
