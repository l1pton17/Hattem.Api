using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static async ValueTask<ApiResponse<U>> Cast<T, U>(this ValueTask<ApiResponse<T>> input, To<U> type)
        {
            var response = await input.ConfigureAwait(false);

            return response.Cast(type);
        }
    }
}
