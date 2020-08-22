using Hattem.Api.Extensions;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Throw
{
    [CategoryTrait("Throw tests")]
    public sealed partial class ThrowTests
    {
        [Fact(DisplayName = "(Sync) Should throw if response has errors")]
        public void Sync_HasErrors_Throw()
        {
            var exception = Assert.Throws<HattemApiException>(
                () => ApiResponse
                    .Error(TestError.Default)
                    .Throw());

            Assert.Equal(exception.Error, TestError.Default, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Sync) Shouldn't return error if no exception was thrown")]
        public void Sync_NoErrors_DoesNotThrow()
        {
            var response = ApiResponse
                .Ok()
                .Throw();

            Assert.True(response.IsOk);
        }
    }
}
