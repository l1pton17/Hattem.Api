using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent
{
    partial class UnwrapTests
    {
        [Fact(DisplayName = "(Async, Sync) Should return wrapped response")]
        public async Task Async_Sync_ResponseOk_ReturnsWrappedResponse()
        {
            const int expectedData = 3;
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Ok(
                    ApiResponse
                        .Ok(expectedData)
                        .WithStatusCode(expectedStatusCode))
                .AsTask()
                .Unwrap();

            Assert.True(response.IsOk);
            Assert.Equal(expectedData, response.Data);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Async, Sync) Should return error if response has errors")]
        public async Task Async_Sync_ResponseHasErrors_ReturnsError()
        {
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Error<ApiResponse<int>>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsTask()
                .Unwrap();

            Assert.True(response.HasErrors);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }
    }
}