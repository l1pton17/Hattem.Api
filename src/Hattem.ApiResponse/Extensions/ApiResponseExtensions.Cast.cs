using System.Threading.Tasks;
using Hattem.ApiResponse.Errors;

namespace Hattem.ApiResponse.Extensions
{
    partial class ApiResponseExtensions
    {
        public static ApiResponse<U> Cast<T, U>(this ApiResponse<T> input, To<U> _)
            where T : class
            where U : class
        {
            if (input.IsOk)
            {
                if (input.Data is U hadCast)
                {
                    return ApiResponse.Ok(hadCast);
                }

                var error = InvalidCastError<T, U>.Default;

                return ApiResponse.Error<U>(error);
            }

            return input.Error.ToResponse(input.StatusCode, To<U>.Type);
        }

        public static async Task<ApiResponse<U>> Cast<T, U>(this Task<ApiResponse<T>> input, To<U> type)
            where T : class
            where U : class
        {
            var response = await input;

            return response.Cast(type);
        }
    }
}
