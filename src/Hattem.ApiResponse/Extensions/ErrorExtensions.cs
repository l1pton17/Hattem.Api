﻿namespace Hattem.ApiResponse.Extensions
{
    public static class ErrorExtensions
    {
        public static ApiResponse<TData> ToResponse<TError, TData>(this TError error, To<TData> _)
            where TError : Error
        {
            return new ApiResponse<TData>(error);
        }

        public static ApiResponse<TData> ToResponse<TError, TData>(this TError error, int? statusCode, To<TData> _)
            where TError : Error
        {
            return new ApiResponse<TData>(error)
                .WithStatusCode(statusCode);
        }
    }
}