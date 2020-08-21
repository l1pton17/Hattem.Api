using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent.OnSuccess
{
    public sealed partial class OnSuccessTests
    {
        [Fact(DisplayName = "(Async ValueTask, Sync) Should execute onSuccess if response is ok")]
        public async Task AsyncValueTask_Sync_IsOk_ExecuteOnSuccess()
        {
            const int expectedData = 2;

            var mock = new Mock<ISyncExecutionProvider<int>>();

            var response = await ApiResponse
                .Ok(expectedData)
                .AsValueTask()
                .OnSuccess(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.Equal(expectedData, response.Data);

            mock.Verify(v => v.Execute(expectedData), Times.Once());
        }

        [Fact(DisplayName = "(Async ValueTask, Sync) Shouldn't execute onSuccess if response has errors")]
        public async Task AsyncValueTask_Sync_HasErrors_DoesNotExecuteOnSuccess()
        {
            const int expectedData = 2;

            var mock = new Mock<ISyncExecutionProvider<int>>();

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsValueTask()
                .OnSuccess(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error);

            mock.Verify(v => v.Execute(expectedData), Times.Never());
        }
    }
}
