using System.Threading.Tasks;
using Hattem.Api.Collections;

namespace Hattem.Api
{
    public static class ApiResponse
    {
        private static readonly ObjectCache<int, Task<ApiResponse<Unit>>> _okStatusCodeCache =
            new ObjectCache<int, Task<ApiResponse<Unit>>>(maxCapacity: 100);

        private static readonly Task<ApiResponse<Unit>> _okAsync = Task.FromResult(Ok());
        
        public static class Boolean
        {
            public static readonly Task<ApiResponse<bool>> TrueAsync = Task.FromResult(Ok(true));
            public static readonly Task<ApiResponse<bool>> FalseAsync = Task.FromResult(Ok(false));
        }

        public static ApiResponse<Unit> Error(Error error)
        {
            return new ApiResponse<Unit>(error);
        }

        public static ApiResponse<T> Error<T>(Error error)
        {
            return new ApiResponse<T>(error);
        }

        public static Task<ApiResponse<Unit>> OkAsync()
        {
            return _okAsync;
        }

        public static Task<ApiResponse<Unit>> OkAsync(int? statusCode)
        {
            static Task<ApiResponse<Unit>> ValueFactory(int s)
            {
                return Task.FromResult(new ApiResponse<Unit>(s, Unit.Default));
            }

            return statusCode.HasValue
                ? _okStatusCodeCache.GetOrAdd(statusCode.Value, ValueFactory)
                : _okAsync;
        }

        public static ApiResponse<Unit> Ok()
        {
            return new ApiResponse<Unit>();
        }

        public static ApiResponse<T> Ok<T>(T data)
        {
            return new ApiResponse<T>(data);
        }
    }
}