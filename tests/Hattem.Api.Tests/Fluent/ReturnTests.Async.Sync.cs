using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent
{
    partial class ReturnTests
    {
        [Fact(DisplayName = "(Async, Simple value) Should return value if response successful")]
        public async Task Async_Successful_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Ok(1)
                .AsTask()
                .Return(expected);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async, Simple value) Should return error if response has error")]
        public async Task Async_HasErrors_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsTask()
                .Return(expected);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
        }

        [Fact(DisplayName = "(Async, Value factory) Should return value if response successful")]
        public async Task Async_ValueFactory_Successful_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Ok(1)
                .AsTask()
                .Return(_ => expected);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async, Value factory) Should return error if response has error")]
        public async Task Async_ValueFactory_HasErrors_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsTask()
                .Return(_ => expected);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
        }
    }
}