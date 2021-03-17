using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static ValueTask<ApiResponse<Unit>> AsUnit(
            this ValueTask<ApiResponse<Unit>> source
        )
        {
            return source;
        }

        public static async ValueTask<ApiResponse<Unit>> AsUnit<TInput>(
            this ValueTask<ApiResponse<TInput>> source
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
