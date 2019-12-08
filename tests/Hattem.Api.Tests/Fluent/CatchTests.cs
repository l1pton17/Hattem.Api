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
        [Fact(DisplayName = "Should return error if exception was thrown")]
        public async Task ThrowException_ReturnsError()
        {
            var response = await Task
                .Run(() => throw new InvalidOperationException())
                .Catch();

            Assert.True(response.HasErrors);
            Assert.IsType<ExceptionError>(response.Error);
        }

        [Fact(DisplayName = "Should return ok if exception wasn't thrown")]
        public async Task NoException_ReturnsOk()
        {
            var response = await Task
                .Run(() => { })
                .Catch();

            Assert.True(response.IsOk);
        }

        [Fact(DisplayName = "Should return task result if exception wasn't thrown")]
        public async Task NoException_TaskHasResult_ReturnsOk()
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
