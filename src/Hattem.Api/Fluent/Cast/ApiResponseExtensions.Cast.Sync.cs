using Hattem.Api.Errors;
using Hattem.Api.Extensions;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static ApiResponse<U> Cast<T, U>(this ApiResponse<T> input, To<U> _)
        {
            if (input.Error is null)
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
    }
}
