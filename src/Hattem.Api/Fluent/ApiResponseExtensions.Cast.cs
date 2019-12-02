using System.Threading.Tasks;
using Hattem.Api.Errors;
using Hattem.Api.Extensions;

namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static ApiResponse<U> Cast<T, U>(this ApiResponse<T> input, To<U> _)
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
        {
            var response = await input;

            return response.Cast(type);
        }
    }
}
