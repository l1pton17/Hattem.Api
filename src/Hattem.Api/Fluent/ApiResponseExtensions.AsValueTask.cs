using System.Threading.Tasks;

namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static ValueTask<ApiResponse<T>> AsValueTask<T>(
            this ApiResponse<T> source
        )
        {
            return new ValueTask<ApiResponse<T>>(source);
        }
    }
}
