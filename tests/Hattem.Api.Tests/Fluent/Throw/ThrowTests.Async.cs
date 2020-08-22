using System.Threading.Tasks;
using Hattem.Api.Extensions;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Throw
{
    partial class ThrowTests
    {
        [Fact(DisplayName = "(Async) Should throw if response has errors")]
        public async Task Async_HasErrors_Throw()
        {
            var exception = await Assert.ThrowsAsync<HattemApiException>(
                () => ApiResponse
                    .Error(TestError.Default)
                    .AsTask()
                    .Throw());

            Assert.Equal(exception.Error, TestError.Default, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Async) Shouldn't return error if no exception was thrown")]
        public async Task Async_NoErrors_DoesNotThrow()
        {
            var response = await ApiResponse
                .OkAsync()
                .Throw();

            Assert.True(response.IsOk);
        }
    }
}
