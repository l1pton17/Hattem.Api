using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Execute <paramref name="next"/> if <paramref name="source"/> is ok
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="source"></param>
        /// <param name="next"></param>
        public static async ValueTask<ApiResponse<TOutput>> Then<TInput, TOutput>(
            this ValueTask<ApiResponse<TInput>> source,
            Func<TInput, ValueTask<ApiResponse<TOutput>>> next
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Then(next)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Execute <paramref name="next"/> if <paramref name="source"/> is ok
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <param name="next"></param>
        public static async ValueTask<ApiResponse<TOutput>> Then<T1, T2, TOutput>(
            this ValueTask<ApiResponse<(T1, T2)>> source,
            Func<T1, T2, ValueTask<ApiResponse<TOutput>>> next
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Then(next)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Execute <paramref name="next"/> if <paramref name="source"/> is ok
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="source"></param>
        /// <param name="next"></param>
        public static async ValueTask<ApiResponse<TOutput>> Then<T1, T2, T3, TOutput>(
            this ValueTask<ApiResponse<(T1, T2, T3)>> source,
            Func<T1, T2, T3, ValueTask<ApiResponse<TOutput>>> next
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Then(next)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Execute <paramref name="next"/> if <paramref name="source"/> is ok
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="source"></param>
        /// <param name="next"></param>
        public static async ValueTask<ApiResponse<TOutput>> Then<T1, T2, T3, T4, TOutput>(
            this ValueTask<ApiResponse<(T1, T2, T3, T4)>> source,
            Func<T1, T2, T3, T4, ValueTask<ApiResponse<TOutput>>> next
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Then(next)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Execute <paramref name="next"/> if <paramref name="source"/> is ok
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="source"></param>
        /// <param name="next"></param>
        public static async ValueTask<ApiResponse<TOutput>> Then<T1, T2, T3, T4, T5, TOutput>(
            this ValueTask<ApiResponse<(T1, T2, T3, T4, T5)>> source,
            Func<T1, T2, T3, T4, T5, ValueTask<ApiResponse<TOutput>>> next
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Then(next)
                .ConfigureAwait(false);
        }
    }
}
