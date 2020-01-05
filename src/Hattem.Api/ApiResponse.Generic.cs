using System;
using System.Net;

namespace Hattem.Api
{
    public readonly struct ApiResponse<T> : IApiResponse<T>
    {
        /// <summary>
        /// Data
        /// </summary>
        public T Data { get; }

        /// <summary>
        /// Error
        /// </summary>
        public Error Error { get; }

        /// <summary>
        /// Status code
        /// </summary>
        public int? StatusCode { get; }

        /// <summary>
        /// Contains an error
        /// </summary>
        public bool HasErrors => Error != null;

        /// <summary>
        /// Is response without errors
        /// </summary>
        public bool IsOk => !HasErrors;

        public ApiResponse(T data)
        {
            Error = null;
            StatusCode = null;
            Data = data;
        }

        public ApiResponse(int? statusCode, T data)
        {
            Data = data;
            StatusCode = statusCode;
            Error = null;
        }

        public ApiResponse(int? statusCode, T data, Error error)
        {
            Error = error;
            Data = data;
            StatusCode = statusCode;
        }

        public ApiResponse(Error error)
        {
            Data = default;
            StatusCode = null;
            Error = error ?? throw new ArgumentNullException(nameof(error));
        }

        public ApiResponse(int? statusCode, Error error)
        {
            Data = default;
            StatusCode = statusCode;
            Error = error ?? throw new ArgumentNullException(nameof(error));
        }

        public ApiResponse<T> WithStatusCode(HttpStatusCode statusCode)
        {
            return new ApiResponse<T>((int) statusCode, Data, Error);
        }

        public ApiResponse<T> WithStatusCode(int? statusCode)
        {
            return new ApiResponse<T>(statusCode, Data, Error);
        }
    }
}