using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
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
            Action<T> onSuccess
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.OnSuccess(onSuccess);
        }

        [Obsolete("Use Then instead", error: true)]
        public static Task<ApiResponse<T>> OnSuccess<T, TOnSuccess>(
            this Task<ApiResponse<T>> source,
            Func<T, ApiResponse<TOnSuccess>> onSuccess
        )
        {
            throw new NotImplementedException();
        }
    }
}
