using System;
using System.Threading.Tasks;

namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Execute <paramref name="onSuccess"/> if response is ok
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="onSuccess">Action to be executed</param>
        /// <returns></returns>
        public static async Task<ApiResponse<T>> OnSuccess<T>(
            this Task<ApiResponse<T>> source,
            Func<T, Task> onSuccess
        )
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .OnSuccess(onSuccess)
                .ConfigureAwait(false);
        }

        [Obsolete("Use Then instead", error: true)]
        public static Task<ApiResponse<T>> OnSuccess<T, TOnSuccess>(
            this Task<ApiResponse<T>> source,
            Func<T, Task<ApiResponse<TOnSuccess>>> onSuccess
        )
        {
            throw new NotImplementedException();
        }
    }
}
