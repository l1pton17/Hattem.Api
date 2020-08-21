using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hattem.Api.Extensions;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent
{
    [CategoryTrait("Throw tests")]
    public sealed class ThrowTests
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

        [Fact(DisplayName = "(Async ValueTask) Should throw if response has errors")]
        public async Task AsyncValueTask_HasErrors_Throw()
        {
            var exception = await Assert.ThrowsAsync<HattemApiException>(
                () => ApiResponse
                    .Error(TestError.Default)
                    .AsValueTask()
                    .Throw()
                    .AsTask());

            Assert.Equal(exception.Error, TestError.Default, ErrorComparer.Default);
        }
    }
}
