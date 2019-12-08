using System;
using System.Threading.Tasks;

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
        public static async Task<ApiResponse<TOutput>> Then<TInput, TOutput>(
            this Task<ApiResponse<TInput>> source,
            Func<TInput, ApiResponse<TOutput>> next
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Then(next);
        }

        /// <summary>
        /// Execute <paramref name="next"/> if <paramref name="source"/> is ok
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <param name="next"></param>
        public static async Task<ApiResponse<TOutput>> Then<T1, T2, TOutput>(
            this Task<ApiResponse<(T1, T2)>> source,
            Func<T1, T2, ApiResponse<TOutput>> next
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Then(next);
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
        public static async Task<ApiResponse<TOutput>> Then<T1, T2, T3, TOutput>(
            this Task<ApiResponse<(T1, T2, T3)>> source,
            Func<T1, T2, T3, ApiResponse<TOutput>> next
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Then(next);
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
        public static async Task<ApiResponse<TOutput>> Then<T1, T2, T3, T4, TOutput>(
            this Task<ApiResponse<(T1, T2, T3, T4)>> source,
            Func<T1, T2, T3, T4, ApiResponse<TOutput>> next
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Then(next);
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
        public static async Task<ApiResponse<TOutput>> Then<T1, T2, T3, T4, T5, TOutput>(
            this Task<ApiResponse<(T1, T2, T3, T4, T5)>> source,
            Func<T1, T2, T3, T4, T5, ApiResponse<TOutput>> next
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Then(next);
        }
    }
}