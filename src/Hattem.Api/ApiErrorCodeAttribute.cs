using System;

namespace Hattem.Api
{
    /// <summary>
    /// Error code
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ApiErrorCodeAttribute : Attribute
    {
        public string ErrorCode { get; }

        public ApiErrorCodeAttribute(string errorCode)
        {
            ErrorCode = errorCode ?? throw new ArgumentNullException(nameof(errorCode));
        }
    }
}
