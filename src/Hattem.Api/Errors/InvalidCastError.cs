using System.Net;
using Hattem.Api.Helpers;

namespace Hattem.Api.Errors
{
    /// <summary>
    /// Invalid type cast
    /// </summary>
    [ApiStatusCode(HttpStatusCode.BadRequest)]
    [ApiErrorCode(ApiErrorCodes.InvalidCast)]
    public sealed class InvalidCastError<T, U> : Error
    {
        public static readonly InvalidCastError<T, U> Default = new InvalidCastError<T, U>();

        public InvalidCastError()
            : base(
                $"Unable to cast from {TypeHelper.GetFriendlyName(typeof(T))} to {TypeHelper.GetFriendlyName(typeof(U))}",
                ApiErrorCodes.InvalidCast)
        {

        }
    }
}
