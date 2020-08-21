using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Then
{
    partial class ThenTests
    {
        [Fact(DisplayName = "(Async ValueTask, Sync) Should call method if result is successful")]
        public async Task Then_AsyncValueTask_Sync_Successful_CallMethod()
        {
            const int data = 2;
            const int expected = 3;

            var mock = new Mock<ISyncResponseProvider<int, int>>();

            mock.Setup(v => v.Execute(data))
                .Returns(ApiResponse.Ok(expected));

            var response = await ApiResponse
                .Ok(data)
                .AsValueTask()
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(data), Times.Once());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask, Sync) Shouldn't call method if result is error")]
        public async Task Then_AsyncValueTask_Sync_HasError_DoesNotCallMethod()
        {
            var mock = new Mock<ISyncResponseProvider<Unit, Unit>>();

            mock.Setup(v => v.Execute(Unit.Default))
                .Returns(ApiResponse.Ok());

            var response = await ApiResponse
                .Error(TestError.Default)
                .AsValueTask()
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(Unit.Default), Times.Never());

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Async ValueTask, Sync) T2 Should call method if result is successful")]
        public async Task Then_AsyncValueTask_Sync_T2_Successful_CallMethod()
        {
            const int data1 = 2;
            const string data2 = "sta";
            const int expected = 3;

            var mock = new Mock<ISyncResponseProvider<int, string, int>>();

            mock.Setup(v => v.Execute(data1, data2))
                .Returns(ApiResponse.Ok(expected));

            var response = await ApiResponse
                .Ok(data1)
                .Union(_ => ApiResponse.Ok(data2))
                .AsValueTask()
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(data1, data2), Times.Once());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask, Sync) T2 Shouldn't call method if result is error")]
        public async Task Then_AsyncValueTask_Sync_T2_HasError_DoesNotCallMethod()
        {
            const int data1 = 2;

            var mock = new Mock<ISyncResponseProvider<int, Unit, Unit>>();

            mock.Setup(v => v.Execute(data1, Unit.Default))
                .Returns(ApiResponse.Ok());

            var response = await ApiResponse
                .Ok(data1)
                .Union(_ => ApiResponse.Error(TestError.Default))
                .AsValueTask()
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(data1, Unit.Default), Times.Never());

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Async ValueTask, Sync) T3 Should call method if result is successful")]
        public async Task Then_AsyncValueTask_Sync_T3_Successful_CallMethod()
        {
            const int data1 = 2;
            const string data2 = "sta";
            const int data3 = 12;
            const int expected = 3;

            var mock = new Mock<ISyncResponseProvider<int, string, int, int>>();

            mock.Setup(v => v.Execute(data1, data2, data3))
                .Returns(ApiResponse.Ok(expected));

            var response = await ApiResponse
                .Ok(data1)
                .Union(_ => ApiResponse.Ok(data2))
                .Union(() => ApiResponse.Ok(data3))
                .AsValueTask()
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(data1, data2, data3), Times.Once());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask, Sync) T3 Shouldn't call method if result is error")]
        public async Task Then_AsyncValueTask_Sync_T3_HasError_DoesNotCallMethod()
        {
            const int data1 = 2;
            const string data2 = "sta";

            var mock = new Mock<ISyncResponseProvider<int, Unit, string, Unit>>();

            mock.Setup(v => v.Execute(data1, Unit.Default, data2))
                .Returns(ApiResponse.Ok());

            var response = await ApiResponse
                .Ok(data1)
                .Union(_ => ApiResponse.Error(TestError.Default))
                .Union(() => ApiResponse.Ok(data2))
                .AsValueTask()
                .Then(mock.Object.Execute);

            mock
                .Verify(
                    v => v.Execute(data1, Unit.Default, data2),
                    Times.Never());

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Async ValueTask, Sync) T4 Should call method if result is successful")]
        public async Task Then_AsyncValueTask_Sync_T4_Successful_CallMethod()
        {
            const int data1 = 2;
            const string data2 = "sta";
            const int data3 = 12;
            const int data4 = 24;
            const int expected = 3;

            var mock = new Mock<ISyncResponseProvider<int, string, int, int, int>>();

            mock.Setup(v => v.Execute(data1, data2, data3, data4))
                .Returns(ApiResponse.Ok(expected));

            var response = await ApiResponse
                .Ok(data1)
                .Union(_ => ApiResponse.Ok(data2))
                .Union(() => ApiResponse.Ok(data3))
                .Union(() => ApiResponse.Ok(data4))
                .AsValueTask()
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(data1, data2, data3, data4), Times.Once());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask, Sync) T4 Shouldn't call method if result is error")]
        public async Task Then_AsyncValueTask_Sync_T4_HasError_DoesNotCallMethod()
        {
            const int data1 = 2;
            const string data2 = "sta";
            const string data3 = "sta3";

            var mock = new Mock<ISyncResponseProvider<int, Unit, string, string, Unit>>();

            mock.Setup(v => v.Execute(data1, Unit.Default, data2, data3))
                .Returns(ApiResponse.Ok());

            var response = await ApiResponse
                .Ok(data1)
                .Union(_ => ApiResponse.Error(TestError.Default))
                .Union(() => ApiResponse.Ok(data2))
                .Union(() => ApiResponse.Ok(data3))
                .AsValueTask()
                .Then(mock.Object.Execute);

            mock
                .Verify(
                    v => v.Execute(data1, Unit.Default, data2, data3),
                    Times.Never());

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Async ValueTask, Sync) T5 Should call method if result is successful")]
        public async Task Then_AsyncValueTask_Sync_T5_Successful_CallMethod()
        {
            const int data1 = 2;
            const string data2 = "sta";
            const int data3 = 12;
            const int data4 = 24;
            const int data5 = 32;
            const int expected = 3;

            var mock = new Mock<ISyncResponseProvider<int, string, int, int, int, int>>();

            mock.Setup(v => v.Execute(data1, data2, data3, data4, data5))
                .Returns(ApiResponse.Ok(expected));

            var response = await ApiResponse
                .Ok(data1)
                .Union(_ => ApiResponse.Ok(data2))
                .Union(() => ApiResponse.Ok(data3))
                .Union(() => ApiResponse.Ok(data4))
                .Union(() => ApiResponse.Ok(data5))
                .AsValueTask()
                .Then(mock.Object.Execute);

            mock.Verify(v => v.Execute(data1, data2, data3, data4, data5), Times.Once());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask, Sync) T5 Shouldn't call method if result is error")]
        public async Task Then_AsyncValueTask_Sync_T5_HasError_DoesNotCallMethod()
        {
            const int data1 = 2;
            const string data2 = "sta";
            const string data3 = "sta3";
            const string data4 = "sta4";

            var mock = new Mock<ISyncResponseProvider<int, Unit, string, string, string, Unit>>();

            mock.Setup(v => v.Execute(data1, Unit.Default, data2, data3, data4))
                .Returns(ApiResponse.Ok());

            var response = await ApiResponse
                .Ok(data1)
                .Union(_ => ApiResponse.Error(TestError.Default))
                .Union(() => ApiResponse.Ok(data2))
                .Union(() => ApiResponse.Ok(data3))
                .Union(() => ApiResponse.Ok(data4))
                .AsValueTask()
                .Then(mock.Object.Execute);

            mock
                .Verify(
                    v => v.Execute(data1, Unit.Default, data2, data3, data4),
                    Times.Never());

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }
    }
}
