using System;
using System.Collections.Generic;
using Hattem.Api.Helpers;
using Hattem.Api.Tests.Framework;
using Xunit;

namespace Hattem.Api.Tests
{
    [CategoryTrait(nameof(TypeHelper))]
    public sealed class TypeHelperTests
    {
        [Theory(DisplayName = "Should return friendly name of type")]
        [InlineData(typeof(int), "Int32")]
        [InlineData(typeof(List<int>), "List<Int32>")]
        [InlineData(typeof(Dictionary<byte, string>), "Dictionary<Byte, String>")]
        public void GetFriendlyName(Type type, string expected)
        {
            var actual = TypeHelper.GetFriendlyName(type);

            Assert.Equal(expected, actual);
        }
    }
}
