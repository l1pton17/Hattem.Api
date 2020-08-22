using System.Threading.Tasks;

namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static Task<ApiResponse<Unit>> AsTask(
            this ApiResponse<Unit> source
        )
        {
            return source.IsOk
                ? ApiResponse.OkAsync(source.StatusCode)
                : Task.FromResult(source);
        }

        public static Task<ApiResponse<T>> AsTask<T>(
            this ApiResponse<T> source
        )
        {
            return Task.FromResult(source);
        }
    }
}
