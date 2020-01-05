using System;
using System.Collections.Generic;

namespace Hattem.Api.NewtonsoftJson.Tests.Framework.Comparers
{
    public sealed class ErrorComparer : IEqualityComparer<Error>
    {
        public static readonly ErrorComparer Default = new ErrorComparer();

        public bool Equals(Error x, Error y)
        {
            if (x == y)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Code == y.Code
                && x.Description == y.Description;
        }

        public int GetHashCode(Error obj)
        {
            throw new NotImplementedException();
        }
    }
}
