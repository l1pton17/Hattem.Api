

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static ApiResponse<Unit> AsUnit<TInput>(
            this ApiResponse<TInput> source
        )
        {
            if (source.Error is not null)
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
    }
}
