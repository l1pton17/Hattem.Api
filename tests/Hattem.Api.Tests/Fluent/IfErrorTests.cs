using System.Collections.Generic;
using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent
{
    [CategoryTrait("IfError")]
    public sealed class IfErrorTests
    {
        public static IEnumerable<object[]> ErrorCodePredicates
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

        #region Sync IfError

        #region Sync

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
        [MemberData(nameof(ErrorCodePredicates))]
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
        [MemberData(nameof(ErrorCodePredicates))]
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
        [MemberData(nameof(ErrorCodePredicates))]
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

        #endregion

        #region Async

        [Fact(DisplayName = "(Async, Sync) CodeErrorPredicate")]
        public async Task Async_Sync_CodeErrorPredicate()
        {
            const int ifErrorData = 3;

            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .AsTask()
                .IfError(
                    ErrorPredicate.ByCode(AnotherTestError.Default.Code),
                    e => ApiResponse.Ok(ifErrorData));

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);
        }

        [Fact(DisplayName = "(Async, Sync) ExactTypeErrorPredicate")]
        public async Task Async_Sync_ExactTypeErrorPredicate()
        {
            const int ifErrorData = 3;

            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .AsTask()
                .IfError(
                    ErrorPredicate.ByType<AnotherTestError>(),
                    e => ApiResponse.Ok(ifErrorData));

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);
        }

        [Fact(DisplayName = "(Async, Sync) TypeErrorPredicate")]
        public async Task Async_Sync_TypeErrorPredicate()
        {
            const int ifErrorData = 3;

            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .AsTask()
                .IfError(
                    ErrorPredicate.ByType<AnotherTestError, AnotherTestError2>(),
                    e => ApiResponse.Ok(ifErrorData));

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);
        }

        [Theory(DisplayName = "(Async, Sync) Should call ifError if response has errors and error match")]
        [MemberData(nameof(ErrorCodePredicates))]
        public async Task Async_Sync_HasErrors_ErrorMatch_CallIfError(
            IErrorPredicate errorPredicate,
            Error validError,
            Error _
        )
        {
            const int ifErrorData = 3;

            var mock = new Mock<ISyncResponseProvider<Error, int>>();

            mock.Setup(v => v.Execute(It.IsAny<Error>()))
                .Returns(ApiResponse.Ok(ifErrorData));

            var response = await ApiResponse
                .Error<int>(validError)
                .AsTask()
                .IfError(
                    errorPredicate,
                    e => mock.Object.Execute(e));

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);

            mock.Verify(v => v.Execute(validError), Times.Once());
        }

        [Theory(DisplayName = "(Async, Sync) Shouldn't call ifError if response has errors but error doesn't match")]
        [MemberData(nameof(ErrorCodePredicates))]
        public async Task Async_Sync_HasErrors_ErrorDoesNotMatch_DoNotCallIfError(
            IErrorPredicate errorPredicate,
            Error _,
            Error invalidError
        )
        {
            const int ifErrorData = 3;

            var mock = new Mock<ISyncResponseProvider<Error, int>>();

            mock.Setup(v => v.Execute(It.IsAny<Error>()))
                .Returns(ApiResponse.Ok(ifErrorData));

            var response = await ApiResponse
                .Error<int>(invalidError)
                .AsTask()
                .IfError(
                    errorPredicate,
                    e => mock.Object.Execute(e));

            Assert.True(response.HasErrors);
            Assert.Equal(invalidError, response.Error, ErrorComparer.Default);

            mock.Verify(v => v.Execute(invalidError), Times.Never());
        }

        [Theory(DisplayName = "(Async, Sync) Shouldn't call ifError if response is ok")]
        [MemberData(nameof(ErrorCodePredicates))]
        public async Task Async_Sync_IsOk_DoNotCallIfError(
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

            var response = await ApiResponse
                .Ok(data)
                .AsTask()
                .IfError(
                    errorPredicate,
                    e => mock.Object.Execute(e));

            Assert.True(response.IsOk);
            Assert.Equal(data, response.Data);

            mock.Verify(v => v.Execute(It.IsAny<Error>()), Times.Never());
        }

        #endregion

        #endregion

        #region Async IfError

        #region Sync

        [Fact(DisplayName = "(Sync, Async) CodeErrorPredicate")]
        public async Task Sync_Async_CodeErrorPredicate()
        {
            const int ifErrorData = 3;

            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .IfError(
                    ErrorPredicate.ByCode(AnotherTestError.Default.Code),
                    e => ApiResponse.Ok(ifErrorData).AsTask());

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);
        }

        [Fact(DisplayName = "(Sync, Async) ExactTypeErrorPredicate")]
        public async Task Sync_Async_ExactTypeErrorPredicate()
        {
            const int ifErrorData = 3;

            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .IfError(
                    ErrorPredicate.ByType<AnotherTestError>(),
                    e => ApiResponse.Ok(ifErrorData).AsTask());

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);
        }

        [Fact(DisplayName = "(Sync, Async) TypeErrorPredicate")]
        public async Task Sync_Async_TypeErrorPredicate()
        {
            const int ifErrorData = 3;

            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .IfError(
                    ErrorPredicate.ByType<AnotherTestError, AnotherTestError2>(),
                    e => ApiResponse.Ok(ifErrorData).AsTask());

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);
        }

        [Theory(DisplayName = "(Sync, Async) Should call ifError if response has errors and error match")]
        [MemberData(nameof(ErrorCodePredicates))]
        public async Task Sync_Async_HasErrors_ErrorMatch_CallIfError(
            IErrorPredicate errorPredicate,
            Error validError,
            Error _
        )
        {
            const int ifErrorData = 3;

            var mock = new Mock<IAsyncResponseProvider<Error, int>>();

            mock.Setup(v => v.Execute(It.IsAny<Error>()))
                .Returns(ApiResponse.Ok(ifErrorData).AsTask());

            var response = await ApiResponse
                .Error<int>(validError)
                .IfError(
                    errorPredicate,
                    e => mock.Object.Execute(e));

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);

            mock.Verify(v => v.Execute(validError), Times.Once());
        }

        [Theory(DisplayName = "(Sync, Async) Shouldn't call ifError if response has errors but error doesn't match")]
        [MemberData(nameof(ErrorCodePredicates))]
        public async Task Sync_Async_HasErrors_ErrorDoesNotMatch_DoNotCallIfError(
            IErrorPredicate errorPredicate,
            Error _,
            Error invalidError
        )
        {
            const int ifErrorData = 3;

            var mock = new Mock<IAsyncResponseProvider<Error, int>>();

            mock.Setup(v => v.Execute(It.IsAny<Error>()))
                .Returns(ApiResponse.Ok(ifErrorData).AsTask());

            var response = await ApiResponse
                .Error<int>(invalidError)
                .IfError(
                    errorPredicate,
                    e => mock.Object.Execute(e));

            Assert.True(response.HasErrors);
            Assert.Equal(invalidError, response.Error, ErrorComparer.Default);

            mock.Verify(v => v.Execute(invalidError), Times.Never());
        }

        [Theory(DisplayName = "(Sync, Async) Shouldn't call ifError if response is ok")]
        [MemberData(nameof(ErrorCodePredicates))]
        public async Task Sync_Async_IsOk_DoNotCallIfError(
            IErrorPredicate errorPredicate,
            Error _,
            Error __
        )
        {
            const int data = 2;
            const int ifErrorData = 3;

            var mock = new Mock<IAsyncResponseProvider<Error, int>>();

            mock.Setup(v => v.Execute(It.IsAny<Error>()))
                .Returns(ApiResponse.Ok(ifErrorData).AsTask());

            var response = await ApiResponse
                .Ok(data)
                .IfError(
                    errorPredicate,
                    e => mock.Object.Execute(e));

            Assert.True(response.IsOk);
            Assert.Equal(data, response.Data);

            mock.Verify(v => v.Execute(It.IsAny<Error>()), Times.Never());
        }

        #endregion

        #region Async

        [Fact(DisplayName = "(Async, Async) CodeErrorPredicate")]
        public async Task Async_Async_CodeErrorPredicate()
        {
            const int ifErrorData = 3;

            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .AsTask()
                .IfError(
                    ErrorPredicate.ByCode(AnotherTestError.Default.Code),
                    e => ApiResponse.Ok(ifErrorData).AsTask());

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);
        }

        [Fact(DisplayName = "(Async, Async) ExactTypeErrorPredicate")]
        public async Task Async_Async_ExactTypeErrorPredicate()
        {
            const int ifErrorData = 3;

            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .AsTask()
                .IfError(
                    ErrorPredicate.ByType<AnotherTestError>(),
                    e => ApiResponse.Ok(ifErrorData).AsTask());

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);
        }

        [Fact(DisplayName = "(Async, Async) TypeErrorPredicate")]
        public async Task Async_Async_TypeErrorPredicate()
        {
            const int ifErrorData = 3;

            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .AsTask()
                .IfError(
                    ErrorPredicate.ByType<AnotherTestError, AnotherTestError2>(),
                    e => ApiResponse.Ok(ifErrorData).AsTask());

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);
        }

        [Theory(DisplayName = "(Async, Async) Should call ifError if response has errors and error match")]
        [MemberData(nameof(ErrorCodePredicates))]
        public async Task Async_Async_HasErrors_ErrorMatch_CallIfError(
            IErrorPredicate errorPredicate,
            Error validError,
            Error _
        )
        {
            const int ifErrorData = 3;

            var mock = new Mock<IAsyncResponseProvider<Error, int>>();

            mock.Setup(v => v.Execute(It.IsAny<Error>()))
                .Returns(ApiResponse.Ok(ifErrorData).AsTask());

            var response = await ApiResponse
                .Error<int>(validError)
                .AsTask()
                .IfError(
                    errorPredicate,
                    e => mock.Object.Execute(e));

            Assert.True(response.IsOk);
            Assert.Equal(ifErrorData, response.Data);

            mock.Verify(v => v.Execute(validError), Times.Once());
        }

        [Theory(DisplayName = "(Async, Async) Shouldn't call ifError if response has errors but error doesn't match")]
        [MemberData(nameof(ErrorCodePredicates))]
        public async Task Async_Async_HasErrors_ErrorDoesNotMatch_DoNotCallIfError(
            IErrorPredicate errorPredicate,
            Error _,
            Error invalidError
        )
        {
            const int ifErrorData = 3;

            var mock = new Mock<IAsyncResponseProvider<Error, int>>();

            mock.Setup(v => v.Execute(It.IsAny<Error>()))
                .Returns(ApiResponse.Ok(ifErrorData).AsTask());

            var response = await ApiResponse
                .Error<int>(invalidError)
                .AsTask()
                .IfError(
                    errorPredicate,
                    e => mock.Object.Execute(e));

            Assert.True(response.HasErrors);
            Assert.Equal(invalidError, response.Error, ErrorComparer.Default);

            mock.Verify(v => v.Execute(invalidError), Times.Never());
        }

        [Theory(DisplayName = "(Async, Async) Shouldn't call ifError if response is ok")]
        [MemberData(nameof(ErrorCodePredicates))]
        public async Task Async_Async_IsOk_DoNotCallIfError(
            IErrorPredicate errorPredicate,
            Error _,
            Error __
        )
        {
            const int data = 2;
            const int ifErrorData = 3;

            var mock = new Mock<IAsyncResponseProvider<Error, int>>();

            mock.Setup(v => v.Execute(It.IsAny<Error>()))
                .Returns(ApiResponse.Ok(ifErrorData).AsTask());

            var response = await ApiResponse
                .Ok(data)
                .AsTask()
                .IfError(
                    errorPredicate,
                    e => mock.Object.Execute(e));

            Assert.True(response.IsOk);
            Assert.Equal(data, response.Data);

            mock.Verify(v => v.Execute(It.IsAny<Error>()), Times.Never());
        }

        #endregion

        #endregion
    }
}