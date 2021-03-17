using System;
using System.Threading.Tasks;
using Hattem.Api.Errors;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Catch
{
    [CategoryTrait(nameof(ApiResponseExtensions.Cast) + " tests")]
    public sealed partial class CatchTests
    {
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

        [Fact(DisplayName = "(Task) Should return error if exception was thrown for method with api response")]
        public async Task TaskApiResponse_ThrowException_ReturnsError()
        {
            var response = await Task
                .Run(() => ApiResponse.Ok(2))
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

        [Fact(DisplayName = "(Task) Should return ok if exception wasn't thrown for method with api response")]
        public async Task TaskApiResponse_NoException_ReturnsOk()
        {
            var response = await Task
                .Run(() => ApiResponse.Ok(2))
                .Catch();

            Assert.True(response.IsOk);
        }

        [Fact(DisplayName = "(Task) Should return error if exception was thrown by task with result")]
        public async Task Task_ThrowException_TaskHasResult_ReturnsError()
        {
            var response = await Task
                .Run(
                    () =>
                    {
                        throw new InvalidOperationException();

                        return 2;
                    })
                .Catch();

            Assert.True(response.HasErrors);
            Assert.IsType<ExceptionError>(response.Error);
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
    }
}
