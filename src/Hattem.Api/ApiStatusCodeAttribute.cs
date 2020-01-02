using System;
using System.Net;

namespace Hattem.Api
{
    /// <summary>
    /// Error status code
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ApiStatusCodeAttribute : Attribute
    {
        public int StatusCode { get; }

        public ApiStatusCodeAttribute(int statusCode)
        {
            StatusCode = statusCode;
        }

        public ApiStatusCodeAttribute(HttpStatusCode statusCode)
        {
            StatusCode = (int) statusCode;
        }
    }
}