using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests
{
    [CategoryTrait(nameof(ErrorStatusCodeMapper) + " Tests")]
    public sealed class ErrorStatusCodeMapperTests
    {
        [Theory(DisplayName = "Should map status code for successful response by priority")]
        [InlineData(100, false, 100)]
        [InlineData(null, true, 202)]
        [InlineData(123, true, 123)]
        [InlineData(null, false, 200)]
        public void IsOk_ReturnsByPriority(
            int? responseStatusCode,
            bool hasData,
            int expected
        )
        {
            var data = hasData ? new Data202() : null;
            var response = ApiResponse.Ok(data);

            if (responseStatusCode.HasValue)
            {
                response = response.WithStatusCode(responseStatusCode.Value);
            }

            var actual = ErrorStatusCodeMapper.Map(response);

            Assert.Equal(expected, actual);
        }

        [Theory(DisplayName = "Should map status code for error response by priority")]
        [InlineData(100, false, 100)]
        [InlineData(null, true, 402)]
        [InlineData(123, true, 123)]
        [InlineData(null, false, 400)]
        public void HasErrors_ReturnsByPriority(
            int? responseStatusCode,
            bool hasError,
            int expected
        )
        {
            var error = hasError ? (Error) new Error402() : new TestError();
            var response = ApiResponse.Error(error);

            if (responseStatusCode.HasValue)
            {
                response = response.WithStatusCode(responseStatusCode.Value);
            }

            var actual = ErrorStatusCodeMapper.Map(response);

            Assert.Equal(expected, actual);
        }

        [ApiStatusCode(202)]
        private class Data202
        {
        }

        [ApiStatusCode(402)]
        private class Error402 : Error
        {
            public Error402()
                : base("402", "402 error")
            {
            }
        }
    }
}
