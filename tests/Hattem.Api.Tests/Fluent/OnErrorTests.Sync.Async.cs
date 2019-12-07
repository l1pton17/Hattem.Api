using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Fluent.ErrorPredicates;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent
{
    partial class OnErrorTests
    {
        [Theory(DisplayName = "(Sync, Async) Shouldn't execute on error if response is ok")]
        [MemberData(nameof(ErrorCodePredicates))]
        public async Task Sync_Async_IsOk_DoesNotExecuteOnError(
            IErrorPredicate errorPredicate,
            Error validError,
            Error invalidError)
        {
            const int expectedData = 3;

            var onErrorMock = new Mock<IAsyncExecutionProvider<Error>>();

            var response = await ApiResponse
                .Ok(expectedData)
                .OnError(
                    errorPredicate,
                    e => onErrorMock.Object.Execute(e));

            Assert.True(response.IsOk);
            Assert.Equal(expectedData, response.Data);

            onErrorMock.Verify(v => v.Execute(It.IsAny<Error>()), Times.Never());
        }

        [Theory(DisplayName = "(Sync, Async) Should execute OnError if error passed predicate")]
        [MemberData(nameof(ErrorCodePredicates))]
        public async Task Sync_Async_HasErrors_ValidError_ExecuteOnError(
            IErrorPredicate errorPredicate,
            Error validError,
            Error invalidError)
        {
            var onErrorMock = new Mock<IAsyncExecutionProvider<Error>>();

            var response = await ApiResponse
                .Error(validError)
                .OnError(
                    errorPredicate,
                    e => onErrorMock.Object.Execute(e));

            Assert.True(response.HasErrors);
            Assert.Equal(validError, response.Error, ErrorComparer.Default);

            onErrorMock.Verify(v => v.Execute(validError), Times.Once());
        }

        [Theory(DisplayName = "(Sync, Async) Shouldn't execute OnError if error didn't pass predicate")]
        [MemberData(nameof(ErrorCodePredicates))]
        public async Task Sync_Async_HasErrors_InvalidError_DoesNotExecuteOnError(
            IErrorPredicate errorPredicate,
            Error validError,
            Error invalidError)
        {
            var onErrorMock = new Mock<IAsyncExecutionProvider<Error>>();

            var response = await ApiResponse
                .Error(invalidError)
                .OnError(
                    errorPredicate,
                    e => onErrorMock.Object.Execute(e));

            Assert.True(response.HasErrors);
            Assert.Equal(invalidError, response.Error, ErrorComparer.Default);

            onErrorMock.Verify(v => v.Execute(validError), Times.Never());
        }

        [Fact(DisplayName = "(Sync, Async) Should execute OnError if error passed " + nameof(AnyErrorPredicate))]
        public async Task Sync_Async_AnyErrorPredicate_HasErrors_ValidError_ExecuteOnError()
        {
            var onErrorMock = new Mock<IAsyncExecutionProvider<Error>>();

            var response = await ApiResponse
                .Error(TestError.Default)
                .OnError(
                    ErrorPredicate.Any(),
                    e => onErrorMock.Object.Execute(e));

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            onErrorMock.Verify(v => v.Execute(TestError.Default), Times.Once());
        }

        [Fact(DisplayName = "(Sync, Async) Should execute OnError if error passed " + nameof(ExactTypeErrorPredicate<TestError>))]
        public async Task Sync_Async_ExactTypeErrorPredicate_HasErrors_ValidError_ExecuteOnError()
        {
            var onErrorMock = new Mock<IAsyncExecutionProvider<TestError>>();

            var response = await ApiResponse
                .Error(TestError.Default)
                .OnError(
                    ErrorPredicate.ByType<TestError>(),
                    onErrorMock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            onErrorMock.Verify(v => v.Execute(TestError.Default), Times.Once());
        }

        [Fact(DisplayName = "(Sync, Async) Should execute OnError if error passed " + nameof(TypeErrorPredicate))]
        public async Task Sync_Async_TypeErrorPredicate_HasErrors_ValidError_ExecuteOnError()
        {
            var onErrorMock = new Mock<IAsyncExecutionProvider<Error>>();

            var response = await ApiResponse
                .Error(TestError.Default)
                .OnError(
                    ErrorPredicate.ByType<TestError, AnotherTestError>(),
                    onErrorMock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            onErrorMock.Verify(v => v.Execute(TestError.Default), Times.Once());
        }

        [Fact(DisplayName = "(Sync, Async) Should execute OnError if error passed " + nameof(CodeErrorPredicate))]
        public async Task Sync_Async_CodeErrorPredicate_HasErrors_ValidError_ExecuteOnError()
        {
            var onErrorMock = new Mock<IAsyncExecutionProvider<Error>>();

            var response = await ApiResponse
                .Error(TestError.Default)
                .OnError(
                    ErrorPredicate.ByCode(TestError.Default.Code),
                    onErrorMock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            onErrorMock.Verify(v => v.Execute(TestError.Default), Times.Once());
        }
    }
}
