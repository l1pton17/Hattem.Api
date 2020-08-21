using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Union results of two responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<(T1, T2)>> Union<T1, T2>(
            this ValueTask<ApiResponse<T1>> source,
            Func<T1, Task<ApiResponse<T2>>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Union(selector)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Add response to current assembly of responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<(T1, T2, T3)>> Union<T1, T2, T3>(
            this ValueTask<ApiResponse<(T1, T2)>> source,
            Func<Task<ApiResponse<T3>>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Union(selector)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Add response to current assembly of responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<(T1, T2, T3)>> Union<T1, T2, T3>(
            this ValueTask<ApiResponse<(T1, T2)>> source,
            Func<T1, T2, Task<ApiResponse<T3>>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Union(selector)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Add response to current assembly of responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<(T1, T2, T3, T4)>> Union<T1, T2, T3, T4>(
            this ValueTask<ApiResponse<(T1, T2, T3)>> source,
            Func<Task<ApiResponse<T4>>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Union(selector)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Add response to current assembly of responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<(T1, T2, T3, T4)>> Union<T1, T2, T3, T4>(
            this ValueTask<ApiResponse<(T1, T2, T3)>> source,
            Func<T1, T2, T3, Task<ApiResponse<T4>>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Union(selector)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Add response to current assembly of responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<(T1, T2, T3, T4, T5)>> Union<T1, T2, T3, T4, T5>(
            this ValueTask<ApiResponse<(T1, T2, T3, T4)>> source,
            Func<Task<ApiResponse<T5>>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Union(selector)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Add response to current assembly of responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<(T1, T2, T3, T4, T5)>> Union<T1, T2, T3, T4, T5>(
            this ValueTask<ApiResponse<(T1, T2, T3, T4)>> source,
            Func<T1, T2, T3, T4, Task<ApiResponse<T5>>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Union(selector)
                .ConfigureAwait(false);
        }
    }
}
