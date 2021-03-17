using System;
using System.Threading.Tasks;
using Hattem.Api.Errors;
using Hattem.Api.Fluent;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Catch
{
    partial class CatchTests
    {
        [Fact(DisplayName = "(ValueTask) Should return error if exception was thrown")]
        public async Task ValueTask_ThrowException_ReturnsError()
        {
            var response = await new ValueTask(Task.FromException(new InvalidOperationException()))
                .Catch();

            Assert.True(response.HasErrors);
            Assert.IsType<ExceptionError>(response.Error);
        }

        [Fact(DisplayName = "(ValueTask) Should return ok if exception wasn't thrown")]
        public async Task ValueTask_NoException_ReturnsOk()
        {
            var response = await new ValueTask()
                .Catch();

            Assert.True(response.IsOk);
        }

        [Fact(DisplayName = "(ValueTask) Should return error if exception was thrown for method with api response")]
        public async Task ValueTaskApiResponse_ThrowException_ReturnsError()
        {
            var response = await new ValueTask<ApiResponse<int>>(Task.Run(() => ApiResponse.Ok(2)))
                .OnSuccess(
                    _ =>
                    {
                        throw new InvalidOperationException();

                        return Task.CompletedTask;
                    })
                .Catch();

            Assert.True(response.HasErrors);
            Assert.IsType<ExceptionError>(response.Error);
        }

        [Fact(DisplayName = "(ValueTask) Should return ok if exception wasn't thrown for method with api response")]
        public async Task ValueTaskApiResponse_NoException_ReturnsOk()
        {
            var response = await new ValueTask<ApiResponse<int>>(Task.Run(() => ApiResponse.Ok(2)))
                .Catch();

            Assert.True(response.IsOk);
        }

        [Fact(DisplayName = "(ValueTask) Should return error if exception was thrown by task with result")]
        public async Task ValueTask_ThrowException_TaskHasResult_ReturnsError()
        {
            var response = await new ValueTask<int>(Task.FromException<int>(new InvalidOperationException()))
                .Catch();

            Assert.True(response.HasErrors);
            Assert.IsType<ExceptionError>(response.Error);
        }

        [Fact(DisplayName = "(ValueTask) Should return task result if exception wasn't thrown")]
        public async Task ValueTask_NoException_TaskHasResult_ReturnsOk()
        {
            const int expectedData = 3;

            var response = await new ValueTask<int>(expectedData)
                .Catch();

            Assert.True(response.IsOk);
            Assert.Equal(expectedData, response.Data);
        }
    }
}
