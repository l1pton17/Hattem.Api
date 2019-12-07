using System.Collections.Generic;
using Hattem.Api.Fluent;
using Hattem.Api.Fluent.ErrorPredicates;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent
{
    [CategoryTrait("OnError")]
    public sealed partial class OnErrorTests
    {
        // ReSharper disable once MemberCanBePrivate.Global
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

        [Theory(DisplayName = "(Sync, Sync) Shouldn't execute on error if response is ok")]
        [MemberData(nameof(ErrorCodePredicates))]
        public void Sync_Sync_IsOk_DoesNotExecuteOnError(
            IErrorPredicate errorPredicate,
            Error validError,
            Error invalidError)
        {
            const int expectedData = 3;

            var onErrorMock = new Mock<ISyncExecutionProvider<Error>>();

            var response = ApiResponse
                .Ok(expectedData)
                .OnError(
                    errorPredicate,
                    e => onErrorMock.Object.Execute(e));

            Assert.True(response.IsOk);
            Assert.Equal(expectedData, response.Data);

            onErrorMock.Verify(v => v.Execute(It.IsAny<Error>()), Times.Never());
        }

        [Theory(DisplayName = "(Sync, Sync) Should execute OnError if error passed predicate")]
        [MemberData(nameof(ErrorCodePredicates))]
        public void Sync_Sync_HasErrors_ValidError_ExecuteOnError(
            IErrorPredicate errorPredicate,
            Error validError,
            Error invalidError)
        {
            var onErrorMock = new Mock<ISyncExecutionProvider<Error>>();

            var response = ApiResponse
                .Error(validError)
                .OnError(
                    errorPredicate,
                    e => onErrorMock.Object.Execute(e));

            Assert.True(response.HasErrors);
            Assert.Equal(validError, response.Error, ErrorComparer.Default);

            onErrorMock.Verify(v => v.Execute(validError), Times.Once());
        }

        [Theory(DisplayName = "(Sync, Sync) Shouldn't execute OnError if error didn't pass predicate")]
        [MemberData(nameof(ErrorCodePredicates))]
        public void Sync_Sync_HasErrors_InvalidError_DoesNotExecuteOnError(
            IErrorPredicate errorPredicate,
            Error validError,
            Error invalidError)
        {
            var onErrorMock = new Mock<ISyncExecutionProvider<Error>>();

            var response = ApiResponse
                .Error(invalidError)
                .OnError(
                    errorPredicate,
                    e => onErrorMock.Object.Execute(e));

            Assert.True(response.HasErrors);
            Assert.Equal(invalidError, response.Error, ErrorComparer.Default);

            onErrorMock.Verify(v => v.Execute(validError), Times.Never());
        }

        [Fact(DisplayName = "(Sync, Sync) Should execute OnError if error passed " + nameof(AnyErrorPredicate))]
        public void Sync_Sync_AnyErrorPredicate_HasErrors_ValidError_ExecuteOnError()
        {
            var onErrorMock = new Mock<ISyncExecutionProvider<Error>>();

            var response = ApiResponse
                .Error(TestError.Default)
                .OnError(
                    ErrorPredicate.Any(),
                    e => onErrorMock.Object.Execute(e));

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            onErrorMock.Verify(v => v.Execute(TestError.Default), Times.Once());
        }

        [Fact(DisplayName = "(Sync, Sync) Should execute OnError if error passed " + nameof(ExactTypeErrorPredicate<TestError>))]
        public void Sync_Sync_ExactTypeErrorPredicate_HasErrors_ValidError_ExecuteOnError()
        {
            var onErrorMock = new Mock<ISyncExecutionProvider<TestError>>();

            var response = ApiResponse
                .Error(TestError.Default)
                .OnError(
                    ErrorPredicate.ByType<TestError>(),
                    onErrorMock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            onErrorMock.Verify(v => v.Execute(TestError.Default), Times.Once());
        }

        [Fact(DisplayName = "(Sync, Sync) Should execute OnError if error passed " + nameof(TypeErrorPredicate))]
        public void Sync_Sync_TypeErrorPredicate_HasErrors_ValidError_ExecuteOnError()
        {
            var onErrorMock = new Mock<ISyncExecutionProvider<Error>>();

            var response = ApiResponse
                .Error(TestError.Default)
                .OnError(
                    ErrorPredicate.ByType<TestError, AnotherTestError>(),
                    onErrorMock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            onErrorMock.Verify(v => v.Execute(TestError.Default), Times.Once());
        }

        [Fact(DisplayName = "(Sync, Sync) Should execute OnError if error passed " + nameof(CodeErrorPredicate))]
        public void Sync_Sync_CodeErrorPredicate_HasErrors_ValidError_ExecuteOnError()
        {
            var onErrorMock = new Mock<ISyncExecutionProvider<Error>>();

            var response = ApiResponse
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
