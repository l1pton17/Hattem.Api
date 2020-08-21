using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Fluent.ErrorPredicates;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.SuppressErrors
{
    partial class SuppressErrorsTests
    {
        [Theory(DisplayName = "(Async) Should return response if it is ok")]
        [MemberData(nameof(ErrorPredicates))]
        public async Task Async_IsOk_ReturnsResponse(
            IErrorPredicate errorPredicate,
            Error validError,
            Error invalidError)
        {
            const int data = 3;
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Ok(data)
                .WithStatusCode(expectedStatusCode)
                .AsTask()
                .SuppressErrors(errorPredicate);

            Assert.True(response.IsOk);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory(DisplayName = "(Async) Should return ok response if error match predicate")]
        [MemberData(nameof(ErrorPredicates))]
        public async Task Async_HasErrors_ValidError_ReturnsOk(
            IErrorPredicate errorPredicate,
            Error validError,
            Error invalidError)
        {
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Error<int>(validError)
                .WithStatusCode(expectedStatusCode)
                .AsTask()
                .SuppressErrors(errorPredicate);

            Assert.True(response.IsOk);
            Assert.Null(response.StatusCode);
        }

        [Fact(DisplayName = "(Async) Should return ok response if error match predicate for " + nameof(CodeErrorPredicate))]
        public async Task Async_CodeErrorPredicate_HasErrors_ValidError_ReturnsOk()
        {
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsTask()
                .SuppressErrors(
                    ErrorPredicate.ByCode(TestError.Default.Code));

            Assert.True(response.IsOk);
            Assert.Null(response.StatusCode);
        }

        [Fact(DisplayName = "(Async) Should return ok response if error match predicate for " + nameof(TypeErrorPredicate))]
        public async Task Async_TypeErrorPredicate_HasErrors_ValidError_ReturnsOk()
        {
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsTask()
                .SuppressErrors(
                    ErrorPredicate.ByType<TestError, AnotherTestError>());

            Assert.True(response.IsOk);
            Assert.Null(response.StatusCode);
        }

        [Fact(DisplayName = "(Async) Should return ok response if error match predicate for " + nameof(ExactTypeErrorPredicate<TestError>))]
        public async Task Async_ExactTypeErrorPredicate_HasErrors_ValidError_ReturnsOk()
        {
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsTask()
                .SuppressErrors(
                    ErrorPredicate.ByType<TestError>());

            Assert.True(response.IsOk);
            Assert.Null(response.StatusCode);
        }

        [Theory(DisplayName = "(Async) Should return error response if error didn't match predicate")]
        [MemberData(nameof(ErrorPredicates))]
        public async Task Async_HasErrors_InvalidError_ReturnsError(
            IErrorPredicate errorPredicate,
            Error validError,
            Error invalidError)
        {
            const int expectedStatusCode = 203;

            var response = await ApiResponse
                .Error<int>(invalidError)
                .WithStatusCode(expectedStatusCode)
                .AsTask()
                .SuppressErrors(errorPredicate);

            Assert.True(response.HasErrors);
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(invalidError, response.Error, ErrorComparer.Default);
        }
    }
}
