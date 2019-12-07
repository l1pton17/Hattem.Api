using System.Collections.Generic;
using Hattem.Api.Fluent;
using Hattem.Api.Fluent.ErrorPredicates;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent
{
    [CategoryTrait(nameof(ApiResponseExtensions.SuppressErrors) + " tests")]
    public sealed partial class SuppressErrorsTests
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static IEnumerable<object[]> ErrorPredicates
        {
            get
            {
                yield return new object[]
                {
                    ErrorPredicate.ByType<AnotherTestError>(),
                    AnotherTestError.Default,
                    AnotherTestError2.Default
                };

                yield return new object[]
                {
                    ErrorPredicate.ByType<AnotherTestError, AnotherTestError2>(),
                    AnotherTestError2.Default,
                    AnotherTestError3.Default
                };

                yield return new object[]
                {
                    ErrorPredicate.ByCode(AnotherTestError.Default.Code),
                    AnotherTestError.Default,
                    AnotherTestError2.Default
                };
            }
        }

        [Theory(DisplayName = "(Sync) Should return response if it is ok")]
        [MemberData(nameof(ErrorPredicates))]
        public void Sync_IsOk_ReturnsResponse(
            IErrorPredicate errorPredicate,
            Error validError,
            Error invalidError)
        {
            const int data = 3;
            const int expectedStatusCode = 203;

            var response = ApiResponse
                .Ok(data)
                .WithStatusCode(expectedStatusCode)
                .SuppressErrors(errorPredicate);

            Assert.True(response.IsOk);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory(DisplayName = "(Sync) Should return ok response if error match predicate")]
        [MemberData(nameof(ErrorPredicates))]
        public void Sync_HasErrors_ValidError_ReturnsOk(
            IErrorPredicate errorPredicate,
            Error validError,
            Error invalidError)
        {
            const int expectedStatusCode = 203;

            var response = ApiResponse
                .Error<int>(validError)
                .WithStatusCode(expectedStatusCode)
                .SuppressErrors(errorPredicate);

            Assert.True(response.IsOk);
            Assert.Null(response.StatusCode);
        }

        [Fact(DisplayName = "(Sync) Should return ok response if error match predicate for " + nameof(CodeErrorPredicate))]
        public void Sync_CodeErrorPredicate_HasErrors_ValidError_ReturnsOk()
        {
            const int expectedStatusCode = 203;

            var response = ApiResponse
                .Error<int>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .SuppressErrors(
                    ErrorPredicate.ByCode(TestError.Default.Code));

            Assert.True(response.IsOk);
            Assert.Null(response.StatusCode);
        }

        [Fact(DisplayName = "(Sync) Should return ok response if error match predicate for " + nameof(TypeErrorPredicate))]
        public void Sync_TypeErrorPredicate_HasErrors_ValidError_ReturnsOk()
        {
            const int expectedStatusCode = 203;

            var response = ApiResponse
                .Error<int>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .SuppressErrors(
                    ErrorPredicate.ByType<TestError, AnotherTestError>());

            Assert.True(response.IsOk);
            Assert.Null(response.StatusCode);
        }

        [Fact(DisplayName = "(Sync) Should return ok response if error match predicate for " + nameof(ExactTypeErrorPredicate<TestError>))]
        public void Sync_ExactTypeErrorPredicate_HasErrors_ValidError_ReturnsOk()
        {
            const int expectedStatusCode = 203;

            var response = ApiResponse
                .Error<int>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .SuppressErrors(
                    ErrorPredicate.ByType<TestError>());

            Assert.True(response.IsOk);
            Assert.Null(response.StatusCode);
        }

        [Theory(DisplayName = "(Sync) Should return error response if error didn't match predicate")]
        [MemberData(nameof(ErrorPredicates))]
        public void Sync_HasErrors_InvalidError_ReturnsError(
            IErrorPredicate errorPredicate,
            Error validError,
            Error invalidError)
        {
            const int expectedStatusCode = 203;

            var response = ApiResponse
                .Error<int>(invalidError)
                .WithStatusCode(expectedStatusCode)
                .SuppressErrors(errorPredicate);

            Assert.True(response.HasErrors);
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(invalidError, response.Error, ErrorComparer.Default);
        }
    }
}
