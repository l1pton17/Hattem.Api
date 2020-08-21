using System.Threading.Tasks;
using Hattem.Api.Extensions;

namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Throws exception if <paramref name="source"/> has errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ApiResponse<T> Throw<T>(
            this ApiResponse<T> source
        )
        {
            if (source.HasErrors)
            {
                throw new HattemApiException(source.Error);
            }

            return source;
        }

        /// <summary>
        /// Throws exception if <paramref name="source"/> has errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<T>> Throw<T>(
                this Task<ApiResponse<T>> source
            )
        {
            var response = await source.ConfigureAwait(false);

            return response.Throw();
        }

        /// <summary>
        /// Throws exception if <paramref name="source"/> has errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static async ValueTask<ApiResponse<T>> Throw<T>(
                this ValueTask<ApiResponse<T>> source
            )
        {
            var response = await source.ConfigureAwait(false);

            return response.Throw();
        }
    }
}
