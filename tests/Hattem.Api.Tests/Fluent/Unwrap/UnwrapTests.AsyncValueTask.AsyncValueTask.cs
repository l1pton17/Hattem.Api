using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Unwrap
{
    partial class UnwrapTests
    {
        [Fact(DisplayName = "(Async ValueTask, Async ValueTask) Should return wrapped response")]
        public async Task AsyncValueTask_AsyncValueTask_ResponseOk_ReturnsWrappedResponse()
        {
            const int expectedData = 3;
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Ok(
                    ApiResponse
                        .Ok(expectedData)
                        .WithStatusCode(expectedStatusCode)
                        .AsValueTask())
                .AsValueTask()
                .Unwrap();

            Assert.True(response.IsOk);
            Assert.Equal(expectedData, response.Data);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Async ValueTask, Async ValueTask) Should return error if response has errors")]
        public async Task AsyncValueTask_AsyncValueTask_ResponseHasErrors_ReturnsError()
        {
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Error<ValueTask<ApiResponse<int>>>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsValueTask()
                .Unwrap();

            Assert.True(response.HasErrors);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }
    }
}
