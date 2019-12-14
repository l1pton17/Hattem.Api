using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent
{
    partial class ReturnTests
    {
        [Fact(DisplayName = "(Async, Async value factory) Should return value if response successful")]
        public async Task Async_AsyncValueFactory_Successful_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Ok(1)
                .AsTask()
                .Return(_ => Task.FromResult(expected));

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async, Async value factory) Should return error if response has error")]
        public async Task Async_AsyncValueFactory_HasErrors_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsTask()
                .Return(_ => Task.FromResult(expected));

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
        }
    }
}