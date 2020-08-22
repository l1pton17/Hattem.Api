using System;
using System.Collections.Generic;
using Hattem.Api.Tests.Framework;
using Xunit;

namespace Hattem.Api.Tests
{
    [CategoryTrait(nameof(Error))]
    public sealed class ErrorTests
    {
        [Fact(DisplayName = "Should create error with code and description")]
        public void Ctor_CodeAndDescription()
        {
            var code = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();
            var actual = new Error(code, description);

            Assert.Equal(code, actual.Code);
            Assert.Equal(description, actual.Description);
        }

        [Fact(DisplayName = "Should create error with code, description and data")]
        public void Ctor_CodeAndDescriptionAndData()
        {
            var code = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();
            var data = new object();
            var actual = new Error(code, description, data);

            Assert.Equal(code, actual.Code);
            Assert.Equal(description, actual.Description);
            Assert.Equal(data, actual.Data);
        }

        [Fact(DisplayName = "Should create typed error with code, description and data")]
        public void CtorTyped_CodeAndDescriptionAndData()
        {
            var code = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();
            var data = new List<int>();
            var actual = new Error<List<int>>(code, description, data);

            Assert.Equal(code, actual.Code);
            Assert.Equal(description, actual.Description);
            Assert.Equal(data, actual.Data);
        }
    }
}
