using Hattem.Api.Extensions;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests
{
    [CategoryTrait("Return")]
    public sealed class ReturnTests
    {
        [Fact(DisplayName = "Should return value if response successful")]
        public void Sync_Successful_ReturnsValue()
        {
            const string expected = "test";

            var response = ApiResponse
                .Ok(1)
                .Return(expected);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "Should return error if response has error")]
        public void Sync_HasErrors_ReturnsValue()
        {
            const string expected = "test";

            var response = ApiResponse
                .Error<int>(TestError.Default)
                .Return(expected);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
        }
    }
}