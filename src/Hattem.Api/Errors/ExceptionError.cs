using System;

namespace Hattem.Api.Errors
{
    /// <summary>
    /// Exception thrown
    /// </summary>
    [ApiErrorCode(ApiErrorCodes.Exception)]
    public sealed class ExceptionError : Error
    {
        public ExceptionError(Exception e)
            : base(
                  ApiErrorCodes.Exception,
                  $"Exception was thrown: {e}")
        {
        }
    }
}
