using System;
using System.Threading.Tasks;

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
            this Task<ApiResponse<T1>> source,
            Func<T1, ApiResponse<T2>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Union(selector);
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
            this Task<ApiResponse<(T1, T2)>> source,
            Func<ApiResponse<T3>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Union(selector);
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
            this Task<ApiResponse<(T1, T2)>> source,
            Func<T1, T2, ApiResponse<T3>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Union(selector);
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
            this Task<ApiResponse<(T1, T2, T3)>> source,
            Func<ApiResponse<T4>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Union(selector);
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
            this Task<ApiResponse<(T1, T2, T3)>> source,
            Func<T1, T2, T3, ApiResponse<T4>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Union(selector);
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
            this Task<ApiResponse<(T1, T2, T3, T4)>> source,
            Func<ApiResponse<T5>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Union(selector);
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
            this Task<ApiResponse<(T1, T2, T3, T4)>> source,
            Func<T1, T2, T3, T4, ApiResponse<T5>> selector
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Union(selector);
        }
    }
}