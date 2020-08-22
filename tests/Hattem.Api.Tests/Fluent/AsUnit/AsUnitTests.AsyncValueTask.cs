using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.AsUnit
{
    partial class AsUnitTests
    {
        [Fact(DisplayName = "(Async ValueTask) Should return successful response")]
        public async Task AsyncValueTask_IsOk_ReturnsUnit()
        {
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Ok(2)
                .WithStatusCode(expectedStatusCode)
                .AsValueTask()
                .AsUnit();

            Assert.True(response.IsOk);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Async ValueTask) Should return successful response for unit data")]
        public async Task AsyncValueTask_IsOk_Unit_ReturnsUnit()
        {
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Ok()
                .WithStatusCode(expectedStatusCode)
                .AsValueTask()
                .AsUnit();

            Assert.True(response.IsOk);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Async ValueTask) Should return successful response")]
        public async Task AsyncValueTask_HasErrors_ReturnsUnit()
        {
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsValueTask()
                .AsUnit();

            Assert.True(response.HasErrors);
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Async ValueTask) Should return successful response for unit data")]
        public async Task AsyncValueTask_HasErrors_Unit_ReturnsUnit()
        {
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Error(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsValueTask()
                .AsUnit();

            Assert.True(response.HasErrors);
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }
    }
}
