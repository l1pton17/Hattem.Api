using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
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
        public static async Task<ApiResponse<T>> Throw<T>(
            this Task<ApiResponse<T>> source
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.Throw();
        }
    }
}
