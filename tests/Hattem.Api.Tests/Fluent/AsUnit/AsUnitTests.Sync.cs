using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.AsUnit
{
    [CategoryTrait("AsUnit")]
    public sealed partial class AsUnitTests
    {
        [Fact(DisplayName = "(Sync) Should return successful response")]
        public void Sync_IsOk_ReturnsUnit()
        {
            const int expectedStatusCode = 203;

            var response = ApiResponse
                .Ok(2)
                .WithStatusCode(expectedStatusCode)
                .AsUnit();

            Assert.True(response.IsOk);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Sync) Should return successful response for unit data")]
        public void Sync_IsOk_Unit_ReturnsUnit()
        {
            const int expectedStatusCode = 203;

            var response = ApiResponse
                .Ok()
                .WithStatusCode(expectedStatusCode)
                .AsUnit();

            Assert.True(response.IsOk);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Sync) Should return successful response")]
        public void Sync_HasErrors_ReturnsUnit()
        {
            const int expectedStatusCode = 203;

            var response = ApiResponse
                .Error<int>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsUnit();

            Assert.True(response.HasErrors);
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Sync) Should return successful response for unit data")]
        public void Sync_HasErrors_Unit_ReturnsUnit()
        {
            const int expectedStatusCode = 203;

            var response = ApiResponse
                .Error(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsUnit();

            Assert.True(response.HasErrors);
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }
    }
}
