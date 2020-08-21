using System.Threading.Tasks;

namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static ApiResponse<Unit> AsUnit<TInput>(
            this ApiResponse<TInput> source
        )
        {
            if (source.HasErrors)
            {
                return new ApiResponse<Unit>(source.StatusCode, source.Error);
            }

            return new ApiResponse<Unit>(
                source.StatusCode,
                Unit.Default);
        }

        public static ApiResponse<Unit> AsUnit(
            this ApiResponse<Unit> source
        )
        {
            return source;
        }

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

            if (response.HasErrors)
            {
                return new ApiResponse<Unit>(response.StatusCode, response.Error);
            }

            return new ApiResponse<Unit>(
                response.StatusCode,
                Unit.Default);
        }


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

            if (response.HasErrors)
            {
                return new ApiResponse<Unit>(response.StatusCode, response.Error);
            }

            return new ApiResponse<Unit>(
                response.StatusCode,
                Unit.Default);
        }
    }
}
