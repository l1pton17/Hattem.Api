using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static async Task<ApiResponse<TOutput>> Return<TInput, TOutput>(
            this Task<ApiResponse<TInput>> source,
            Func<TInput, Task<TOutput>> valueFactory
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Return(valueFactory)
                .ConfigureAwait(false);
        }

        public static async Task<ApiResponse<TOutput>> Return<T1, T2, TOutput>(
            this Task<ApiResponse<(T1, T2)>> source,
            Func<T1, T2, Task<TOutput>> valueFactory
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Return(valueFactory)
                .ConfigureAwait(false);
        }

        public static async Task<ApiResponse<TOutput>> Return<T1, T2, T3, TOutput>(
            this Task<ApiResponse<(T1, T2, T3)>> source,
            Func<T1, T2, T3, Task<TOutput>> valueFactory
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Return(valueFactory)
                .ConfigureAwait(false);
        }

        public static async Task<ApiResponse<TOutput>> Return<T1, T2, T3, T4, TOutput>(
            this Task<ApiResponse<(T1, T2, T3, T4)>> source,
            Func<T1, T2, T3, T4, Task<TOutput>> valueFactory
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Return(valueFactory)
                .ConfigureAwait(false);
        }

        public static async Task<ApiResponse<TOutput>> Return<T1, T2, T3, T4, T5, TOutput>(
            this Task<ApiResponse<(T1, T2, T3, T4, T5)>> source,
            Func<T1, T2, T3, T4, T5, Task<TOutput>> valueFactory
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Return(valueFactory)
                .ConfigureAwait(false);
        }
    }
}
