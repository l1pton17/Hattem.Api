namespace Hattem.Api.Extensions
{
    partial class ApiResponseExtensions
    {
        public static ApiResponse<TOutput> Return<TInput, TOutput>(
            this ApiResponse<TInput> response,
            TOutput value)
        {
            if (response.HasErrors)
            {
                return response.Error.ToResponse(To<TOutput>.Type);
            }

            return ApiResponse.Ok(value);
        }
    }
}
