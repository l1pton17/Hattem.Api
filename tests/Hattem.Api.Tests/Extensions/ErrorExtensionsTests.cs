using Hattem.Api.Extensions;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Extensions
{
    [CategoryTrait("ErrorExtensions")]
    public sealed class ErrorExtensionsTests
    {
        [Fact(DisplayName = "Should wrap error to response")]
        public void ToResponse()
        {
            var response = TestError
                .Default
                .ToResponse(To<int>.Type);

            Assert.True(response.HasErrors);
            Assert.False(response.IsOk);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
            Assert.Null(response.StatusCode);
        }

        [Fact(DisplayName = "Should wrap error to response with status code")]
        public void ToResponse_WithStatusCode()
        {
            const int expected = 203;

            var response = TestError
                .Default
                .ToResponse(expected, To<int>.Type);

            Assert.True(response.HasErrors);
            Assert.False(response.IsOk);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
            Assert.Equal(expected, response.StatusCode);
        }
    }
}