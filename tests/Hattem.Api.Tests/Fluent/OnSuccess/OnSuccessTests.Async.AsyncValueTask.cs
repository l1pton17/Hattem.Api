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
        [Fact(DisplayName = "(Async, Async ValueTask) Should execute onSuccess if response is ok")]
        public async Task Async_AsyncValueTask_IsOk_ExecuteOnSuccess()
        {
            const int expectedData = 2;

            var mock = new Mock<IAsyncValueTaskExecutionProvider<int>>();

            var response = await ApiResponse
                .Ok(expectedData)
                .AsTask()
                .OnSuccess(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.Equal(expectedData, response.Data);

            mock.Verify(v => v.Execute(expectedData), Times.Once());
        }

        [Fact(DisplayName = "(Async, Async ValueTask) Shouldn't execute onSuccess if response has errors")]
        public async Task Async_AsyncValueTask_HasErrors_DoesNotExecuteOnSuccess()
        {
            const int expectedData = 2;

            var mock = new Mock<IAsyncValueTaskExecutionProvider<int>>();

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsTask()
                .OnSuccess(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error);

            mock.Verify(v => v.Execute(expectedData), Times.Never());
        }
    }
}
