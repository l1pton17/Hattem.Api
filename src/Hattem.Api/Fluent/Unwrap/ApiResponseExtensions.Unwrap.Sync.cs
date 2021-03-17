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
        /// <returns></returns>
        public static ApiResponse<T> Unwrap<T>(
            this ApiResponse<ApiResponse<T>> source)
        {
            if (source.Error is not null)
            {
                return new ApiResponse<T>(
                    source.StatusCode,
                    source.Error);
            }

            return source.Data;
        }
    }
}
