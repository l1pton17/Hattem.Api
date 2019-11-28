using System;
using System.Net;

namespace Hattem.ApiResponse
{
    /// <summary>
    /// Error status code
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ApiErrorStatusCodeAttribute : Attribute
    {
        public int StatusCode { get; }

        public ApiErrorStatusCodeAttribute(int statusCode)
        {
            StatusCode = statusCode;
        }

        public ApiErrorStatusCodeAttribute(HttpStatusCode statusCode)
        {
            StatusCode = (int) statusCode;
        }
    }
}