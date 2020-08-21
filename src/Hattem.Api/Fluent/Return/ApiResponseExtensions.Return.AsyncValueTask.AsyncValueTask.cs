using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static async ValueTask<ApiResponse<TOutput>> Return<TInput, TOutput>(
            this ValueTask<ApiResponse<TInput>> source,
            Func<TInput, ValueTask<TOutput>> valueFactory
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Return(valueFactory)
                .ConfigureAwait(false);
        }

        public static async ValueTask<ApiResponse<TOutput>> Return<T1, T2, TOutput>(
            this ValueTask<ApiResponse<(T1, T2)>> source,
            Func<T1, T2, ValueTask<TOutput>> valueFactory
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Return(valueFactory)
                .ConfigureAwait(false);
        }

        public static async ValueTask<ApiResponse<TOutput>> Return<T1, T2, T3, TOutput>(
            this ValueTask<ApiResponse<(T1, T2, T3)>> source,
            Func<T1, T2, T3, ValueTask<TOutput>> valueFactory
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Return(valueFactory)
                .ConfigureAwait(false);
        }

        public static async ValueTask<ApiResponse<TOutput>> Return<T1, T2, T3, T4, TOutput>(
            this ValueTask<ApiResponse<(T1, T2, T3, T4)>> source,
            Func<T1, T2, T3, T4, ValueTask<TOutput>> valueFactory
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Return(valueFactory)
                .ConfigureAwait(false);
        }

        public static async ValueTask<ApiResponse<TOutput>> Return<T1, T2, T3, T4, T5, TOutput>(
            this ValueTask<ApiResponse<(T1, T2, T3, T4, T5)>> source,
            Func<T1, T2, T3, T4, T5, ValueTask<TOutput>> valueFactory
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Return(valueFactory)
                .ConfigureAwait(false);
        }
    }
}
