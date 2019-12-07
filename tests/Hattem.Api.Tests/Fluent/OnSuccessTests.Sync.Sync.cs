using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent
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
    }
}
