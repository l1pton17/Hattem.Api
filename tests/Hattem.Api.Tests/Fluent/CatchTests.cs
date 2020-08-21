using System;
using System.Threading.Tasks;
using Hattem.Api.Errors;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Xunit;

namespace Hattem.Api.Tests.Fluent
{
    [CategoryTrait(nameof(ApiResponseExtensions.Cast) + " tests")]
    public sealed class CatchTests
    {
        #region Task

        [Fact(DisplayName = "(Task) Should return error if exception was thrown")]
        public async Task Task_ThrowException_ReturnsError()
        {
            var response = await Task
                .Run(() => throw new InvalidOperationException())
                .Catch();

            Assert.True(response.HasErrors);
            Assert.IsType<ExceptionError>(response.Error);
        }

        [Fact(DisplayName = "(Task) Should return ok if exception wasn't thrown")]
        public async Task Task_NoException_ReturnsOk()
        {
            var response = await Task
                .Run(() => { })
                .Catch();

            Assert.True(response.IsOk);
        }

        [Fact(DisplayName = "(Task) Should return task result if exception wasn't thrown")]
        public async Task Task_NoException_TaskHasResult_ReturnsOk()
        {
            const int expectedData = 3;

            var response = await Task
                .Run(() => expectedData)
                .Catch();

            Assert.True(response.IsOk);
            Assert.Equal(expectedData, response.Data);
        }

        #endregion

        #region ValueTask

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

        [Fact(DisplayName = "(ValueTask) Should return task result if exception wasn't thrown")]
        public async Task ValueTask_NoException_TaskHasResult_ReturnsOk()
        {
            const int expectedData = 3;

            var response = await new ValueTask<int>(expectedData)
                .Catch();

            Assert.True(response.IsOk);
            Assert.Equal(expectedData, response.Data);
        }

        #endregion
    }
}
