using System.Threading.Tasks;

namespace Hattem.Api.Fluent
{
#if !NETSTANDARD2_0
    partial class ApiResponseExtensions
    {
        public static ValueTask<ApiResponse<T>> AsValueTask<T>(
            this ApiResponse<T> source
        )
        {
            return new ValueTask<ApiResponse<T>>(source);
        }
    }
#endif
}
