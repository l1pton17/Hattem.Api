using System.Net;
using System.Threading.Tasks;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests
{
    [CategoryTrait(nameof(ApiResponse))]
    public sealed class ApiResponseTests
    {
        [Fact(DisplayName = "IsOk should be false and HasErrors should be true if error isn't null")]
        public void ErrorNotNull_IsOkFalse_HasErrorsTrue()
        {
            var response = new ApiResponse<Unit>(TestError.Default);

            Assert.True(response.HasErrors);
            Assert.False(response.IsOk);
            Assert.Null(response.Data);
            Assert.NotNull(response.Error);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
            Assert.Null(response.StatusCode);
        }

        [Fact(DisplayName = "IsOk should be true and HasErrors should be false if data isn't null")]
        public void DataNotNull_IsOkFalse_HasErrorsTrue()
        {
            var response = new ApiResponse<Unit>(Unit.Default);

            Assert.False(response.HasErrors);
            Assert.True(response.IsOk);
            Assert.NotNull(response.Data);
            Assert.Null(response.Error);
            Assert.Equal(Unit.Default, response.Data);
            Assert.Null(response.StatusCode);
        }

        [Fact(DisplayName = "IsOk should be false and HasErrors should be true if error and data are both not null")]
        public void ErrorNotNullAndDataNotNull_IsOkFalse_HasErrorsTrue()
        {
            const int expectedStatusCode = 203;

            var response = new ApiResponse<Unit>(
                statusCode: expectedStatusCode,
                Unit.Default,
                TestError.Default);

            Assert.True(response.HasErrors);
            Assert.False(response.IsOk);
            Assert.NotNull(response.Data);
            Assert.NotNull(response.Error);
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(Unit.Default, response.Data);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "Should change status code by int WithStatusCode method")]
        public void WithStatusCode_Int32_ChangeStatusCode()
        {
            const int expectedStatusCode = 201;

            var response = ApiResponse.Ok(1);

            Assert.Null(response.StatusCode);

            response = response.WithStatusCode(expectedStatusCode);

            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "Should change status code by HTTP WithStatusCode method")]
        public void WithStatusCode_HTTP_ChangeStatusCode()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.Accepted;

            var response = ApiResponse.Ok(1);

            Assert.Null(response.StatusCode);

            response = response.WithStatusCode(expectedStatusCode);

            Assert.Equal((int) expectedStatusCode, response.StatusCode);
        }
        
        [Fact(DisplayName = "Should construct response with data and status code")]
        public void ConstructDataWithStatusCode()
        {
            const int expectedStatusCode = 203;

            var response = new ApiResponse<Unit>(expectedStatusCode, Unit.Default);

            Assert.False(response.HasErrors);
            Assert.True(response.IsOk);
            Assert.NotNull(response.Data);
            Assert.Null(response.Error);
            Assert.Equal(Unit.Default, response.Data);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "Should construct response with error and status code")]
        public void ConstructErrorWithStatusCode()
        {
            const int expectedStatusCode = 203;

            var response = new ApiResponse<Unit>(expectedStatusCode, TestError.Default);

            Assert.True(response.HasErrors);
            Assert.False(response.IsOk);
            Assert.Null(response.Data);
            Assert.NotNull(response.Error);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory(DisplayName = "Should return async boolean")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task BooleanAsync(bool expected)
        {
            var responseTask = expected
                ? ApiResponse.Boolean.TrueAsync
                : ApiResponse.Boolean.FalseAsync;

            var response = await responseTask;

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }
    }
}
