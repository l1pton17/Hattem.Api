using System.Collections.Generic;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent.IfError
{
    [CategoryTrait("IfError")]
    public sealed partial class IfErrorTests
    {
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

        [Fact(DisplayName = "(Sync, Sync) CodeErrorPredicate")]
        public void Sync_Sync_CodeErrorPredicate()
        {
            const int ifErrorData = 3;

            var response = ApiResponse
                .Error<int>(AnotherTestError.Default)
                .IfError(
                    ErrorPredicate.ByCode(AnotherTestError.Default.Code),
                    e => ApiResponse.Ok(ifErrorData));

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);
        }

        [Fact(DisplayName = "(Sync, Sync) ExactTypeErrorPredicate")]
        public void Sync_Sync_ExactTypeErrorPredicate()
        {
            const int ifErrorData = 3;

            var response = ApiResponse
                .Error<int>(AnotherTestError.Default)
                .IfError(
                    ErrorPredicate.ByType<AnotherTestError>(),
                    e => ApiResponse.Ok(ifErrorData));

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);
        }

        [Fact(DisplayName = "(Sync, Sync) TypeErrorPredicate")]
        public void Sync_Sync_TypeErrorPredicate()
        {
            const int ifErrorData = 3;

            var response = ApiResponse
                .Error<int>(AnotherTestError.Default)
                .IfError(
                    ErrorPredicate.ByType<AnotherTestError, AnotherTestError2>(),
                    e => ApiResponse.Ok(ifErrorData));

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);
        }

        [Theory(DisplayName = "(Sync, Sync) Should call ifError if response has errors and error match")]
        [MemberData(nameof(ErrorPredicates))]
        public void Sync_Sync_HasErrors_ErrorMatch_CallIfError(
            IErrorPredicate errorPredicate,
            Error validError,
            Error _
        )
        {
            const int ifErrorData = 3;

            var mock = new Mock<ISyncResponseProvider<Error, int>>();

            mock.Setup(v => v.Execute(It.IsAny<Error>()))
                .Returns(ApiResponse.Ok(ifErrorData));

            var response = ApiResponse
                .Error<int>(validError)
                .IfError(
                    errorPredicate,
                    e => mock.Object.Execute(e));

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);

            mock.Verify(v => v.Execute(validError), Times.Once());
        }

        [Theory(DisplayName = "(Sync, Sync) Shouldn't call ifError if response has errors but error doesn't match")]
        [MemberData(nameof(ErrorPredicates))]
        public void Sync_Sync_HasErrors_ErrorDoesNotMatch_DoNotCallIfError(
            IErrorPredicate errorPredicate,
            Error _,
            Error invalidError
        )
        {
            const int ifErrorData = 3;

            var mock = new Mock<ISyncResponseProvider<Error, int>>();

            mock.Setup(v => v.Execute(It.IsAny<Error>()))
                .Returns(ApiResponse.Ok(ifErrorData));

            var response = ApiResponse
                .Error<int>(invalidError)
                .IfError(
                    errorPredicate,
                    e => mock.Object.Execute(e));

            Assert.True(response.HasErrors);
            Assert.Equal(invalidError, response.Error, ErrorComparer.Default);

            mock.Verify(v => v.Execute(invalidError), Times.Never());
        }

        [Theory(DisplayName = "(Sync, Sync) Shouldn't call ifError if response is ok")]
        [MemberData(nameof(ErrorPredicates))]
        public void Sync_Sync_IsOk_DoNotCallIfError(
            IErrorPredicate errorPredicate,
            Error _,
            Error __
        )
        {
            const int data = 2;
            const int ifErrorData = 3;

            var mock = new Mock<ISyncResponseProvider<Error, int>>();

            mock.Setup(v => v.Execute(It.IsAny<Error>()))
                .Returns(ApiResponse.Ok(ifErrorData));

            var response = ApiResponse
                .Ok(data)
                .IfError(
                    errorPredicate,
                    e => mock.Object.Execute(e));

            Assert.True(response.IsOk);
            Assert.Equal(data, response.Data);

            mock.Verify(v => v.Execute(It.IsAny<Error>()), Times.Never());
        }
    }
}
