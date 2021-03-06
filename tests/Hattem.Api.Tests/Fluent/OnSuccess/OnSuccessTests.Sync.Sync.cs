﻿using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent.OnSuccess
{
    [CategoryTrait(nameof(ApiResponseExtensions.OnSuccess) + " tests")]
    public sealed partial class OnSuccessTests
    {
        [Fact(DisplayName = "(Sync, Sync) Should execute onSuccess if response is ok")]
        public void Sync_Sync_IsOk_ExecuteOnSuccess()
        {
            const int expectedData = 2;

            var mock = new Mock<ISyncExecutionProvider<int>>();

            var response = ApiResponse
                .Ok(expectedData)
                .OnSuccess(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.Equal(expectedData, response.Data);

            mock.Verify(v => v.Execute(expectedData), Times.Once());
        }

        [Fact(DisplayName = "(Sync, Sync) Shouldn't execute onSuccess if response has errors")]
        public void Sync_Sync_HasErrors_DoesNotExecuteOnSuccess()
        {
            const int expectedData = 2;

            var mock = new Mock<ISyncExecutionProvider<int>>();

            var response = ApiResponse
                .Error<int>(TestError.Default)
                .OnSuccess(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error);

            mock.Verify(v => v.Execute(expectedData), Times.Never());
        }
    }
}
