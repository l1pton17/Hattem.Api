using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Unwrap wrapped response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        public static Task<ApiResponse<T>> Unwrap<T>(
            this ApiResponse<Task<ApiResponse<T>>> source)
        {
            if (source.Error is not null)
            {
                return new ApiResponse<T>(
                        source.StatusCode,
                        source.Error)
                    .AsTask();
            }

            return source.Data!;
        }

        /// <summary>
        /// Unwrap wrapped response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        public static async Task<ApiResponse<T>> Unwrap<T>(
            this Task<ApiResponse<Task<ApiResponse<T>>>> source)
        {
            var response = await source.ConfigureAwait(false);

            return await response
                .Unwrap()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Unwrap wrapped response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        public static async Task<ApiResponse<T>> Unwrap<T>(
            this Task<ApiResponse<ApiResponse<T>>> source)
        {
            var response = await source.ConfigureAwait(false);

            return response.Unwrap();
        }
    }
}
