using System;

namespace Hattem.Api.Extensions
{
    public sealed class HattemApiException : Exception
    {
        public Error Error { get; }

        public HattemApiException(Error error)
            : base($"Error: {error} occured")
        {
            Error = error ?? throw new ArgumentNullException(nameof(error));
        }
    }
}