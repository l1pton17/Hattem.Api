using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent
{
    [CategoryTrait(nameof(ApiResponseExtensions.Unwrap) + " tests")]
    public sealed partial class UnwrapTests
    {
        [Fact(DisplayName = "(Sync, Sync) Should return wrapped response")]
        public void Sync_Sync_ResponseOk_ReturnsWrappedResponse()
        {
            const int expectedData = 3;
            const int expectedStatusCode = 203;

            var response = ApiResponse
                .Ok(
                    ApiResponse
                        .Ok(expectedData)
                        .WithStatusCode(expectedStatusCode))
                .Unwrap();

            Assert.True(response.IsOk);
            Assert.Equal(expectedData, response.Data);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Sync, Sync) Should return error if response has errors")]
        public void Sync_Sync_ResponseHasErrors_ReturnsError()
        {
            const int expectedStatusCode = 203;

            var response = ApiResponse
                .Error<ApiResponse<int>>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .Unwrap();

            Assert.True(response.HasErrors);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }
    }
}