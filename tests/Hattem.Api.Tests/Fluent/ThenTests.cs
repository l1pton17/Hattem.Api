using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent
{
    [CategoryTrait("Then")]
    public sealed class ThenTests
    {
        #region (Async, Sync)

        [Fact(DisplayName = "(Async, Sync) Should call method if result is successful")]
        public async Task Then_Async_Sync_Successful_CallMethod()
        {
            const int data = 2;
            const int expected = 3;

            var mock = new Mock<ISyncResponseProvider<int, int>>();

            mock.Setup(v => v.Execute(data))
                .Returns(ApiResponse.Ok(expected));

            var response = await ApiResponse
                .Ok(data)
                .AsTask()
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(data), Times.Once());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async, Sync) Shouldn't call method if result is error")]
        public async Task Then_Async_Sync_HasError_DoesNotCallMethod()
        {
            var mock = new Mock<ISyncResponseProvider<Unit, Unit>>();

            mock.Setup(v => v.Execute(Unit.Default))
                .Returns(ApiResponse.Ok());

            var response = await ApiResponse
                .Error(TestError.Default)
                .AsTask()
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(Unit.Default), Times.Never());

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        #endregion

        #region (Async, Async)

        [Fact(DisplayName = "(Async, Async) Should call method if result is successful")]
        public async Task Then_Async_Async_Successful_CallMethod()
        {
            const int data = 2;
            const int expected = 3;

            var mock = new Mock<IAsyncResponseProvider<int, int>>();

            mock.Setup(v => v.Execute(data))
                .Returns(ApiResponse.Ok(expected).AsTask());

            var response = await ApiResponse
                .Ok(data)
                .AsTask()
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(data), Times.Once());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async, Async) Shouldn't call method if result is error")]
        public async Task Then_Async_Async_HasError_DoesNotCallMethod()
        {
            var mock = new Mock<IAsyncResponseProvider<Unit, Unit>>();

            mock.Setup(v => v.Execute(Unit.Default))
                .Returns(ApiResponse.OkAsync());

            var response = await ApiResponse
                .Error(TestError.Default)
                .AsTask()
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(Unit.Default), Times.Never());

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        #endregion

        #region (Sync, Async)

        [Fact(DisplayName = "(Sync, Async) Should call method if result is successful")]
        public async Task Then_Sync_Async_Successful_CallMethod()
        {
            const int data = 2;
            const int expected = 3;

            var mock = new Mock<IAsyncResponseProvider<int, int>>();

            mock.Setup(v => v.Execute(data))
                .Returns(ApiResponse.Ok(expected).AsTask());

            var response = await ApiResponse
                .Ok(data)
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(data), Times.Once());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Sync, Async) Shouldn't call method if result is error")]
        public async Task Then_Sync_Async_HasError_DoesNotCallMethod()
        {
            var mock = new Mock<IAsyncResponseProvider<Unit, Unit>>();

            mock.Setup(v => v.Execute(Unit.Default))
                .Returns(ApiResponse.OkAsync());

            var response = await ApiResponse
                .Error(TestError.Default)
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(Unit.Default), Times.Never());

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        #endregion

        #region (Sync, Sync)

        [Fact(DisplayName = "(Sync, Async) Should call method if result is successful")]
        public void Then_Sync_Sync_Successful_CallMethod()
        {
            const int data = 2;
            const int expected = 3;

            var mock = new Mock<ISyncResponseProvider<int, int>>();

            mock.Setup(v => v.Execute(data))
                .Returns(ApiResponse.Ok(expected));

            var response = ApiResponse
                .Ok(data)
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(data), Times.Once());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Sync, Async) Shouldn't call method if result is error")]
        public void Then_Sync_Sync_HasError_DoesNotCallMethod()
        {
            var mock = new Mock<ISyncResponseProvider<Unit, Unit>>();

            mock.Setup(v => v.Execute(Unit.Default))
                .Returns(ApiResponse.Ok());

            var response = ApiResponse
                .Error(TestError.Default)
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(Unit.Default), Times.Never());

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        #endregion
    }
}