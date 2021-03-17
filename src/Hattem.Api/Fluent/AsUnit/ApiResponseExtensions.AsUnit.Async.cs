using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static Task<ApiResponse<Unit>> AsUnit(
            this Task<ApiResponse<Unit>> source
        )
        {
            return source;
        }

        public static async Task<ApiResponse<Unit>> AsUnit<TInput>(
            this Task<ApiResponse<TInput>> source
        )
        {
            var response = await source.ConfigureAwait(false);

            if (response.Error is not null)
            {
                return new ApiResponse<Unit>(response.StatusCode, response.Error);
            }

            return new ApiResponse<Unit>(
                response.StatusCode,
                Unit.Default);
        }
    }
}
