﻿using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Return
{
    partial class ReturnTests
    {
        [Fact(DisplayName = "(Async ValueTask, Simple value) Should return value if response successful")]
        public async Task AsyncValueTask_Successful_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Ok(1)
                .AsValueTask()
                .Return(expected);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask, Simple value) Should return error if response has error")]
        public async Task AsyncValueTask_HasErrors_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsValueTask()
                .Return(expected);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask, Value factory) Should return value if response successful")]
        public async Task AsyncValueTask_ValueFactory_Successful_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Ok(1)
                .AsValueTask()
                .Return(_ => expected);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask, Value factory) Should return error if response has error")]
        public async Task AsyncValueTask_ValueFactory_HasErrors_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsValueTask()
                .Return(_ => expected);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask, Sync value factory, T2) Should return value if response successful")]
        public async Task AsyncValueTask_SyncValueFactory_T2_Successful_ReturnsValue()
        {
            const int data1 = 12;
            const int data2 = 23;
            const string expected = "test";

            var mock = new Mock<ISyncReturnProvider<int, int, string>>();

            mock.Setup(v => v.Execute(data1, data2))
                .Returns(expected);

            var response = await ApiResponse
                .Ok(data1)
                .AsValueTask()
                .Union(_ => ApiResponse.Ok(data2))
                .Return(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);

            mock.Verify(v => v.Execute(data1, data2), Times.Once());
        }

        [Fact(DisplayName = "(Async ValueTask, Sync value factory, T2) Should return error if response has error")]
        public async Task AsyncValueTask_SyncValueFactory_T2_HasErrors_ReturnsValue()
        {
            const int data2 = 23;
            const string expected = "test";

            var mock = new Mock<ISyncReturnProvider<int, int, string>>();

            mock.Setup(v => v.Execute(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(expected);

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsValueTask()
                .Union(_ => ApiResponse.Ok(data2))
                .Return(mock.Object.Execute);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            mock.Verify(v => v.Execute(It.IsAny<int>(), It.IsAny<int>()), Times.Never());
        }

        [Fact(DisplayName = "(Async ValueTask, Sync value factory, T3) Should return value if response successful")]
        public async Task AsyncValueTask_SyncValueFactory_T3_Successful_ReturnsValue()
        {
            const int data1 = 12;
            const int data2 = 23;
            const int data3 = 34;
            const string expected = "test";

            var mock = new Mock<ISyncReturnProvider<int, int, int, string>>();

            mock.Setup(v => v.Execute(data1, data2, data3))
                .Returns(expected);

            var response = await ApiResponse
                .Ok(data1)
                .AsValueTask()
                .Union(_ => ApiResponse.Ok(data2))
                .Union(() => ApiResponse.Ok(data3))
                .Return(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);

            mock.Verify(
                v => v.Execute(
                    data1,
                    data2,
                    data3),
                Times.Once());
        }

        [Fact(DisplayName = "(Async ValueTask, Sync value factory, T3) Should return error if response has error")]
        public async Task AsyncValueTask_SyncValueFactory_T3_HasErrors_ReturnsValue()
        {
            const int data2 = 23;
            const int data3 = 34;
            const string expected = "test";

            var mock = new Mock<ISyncReturnProvider<int, int, int, string>>();

            mock.Setup(v => v.Execute(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(expected);

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsValueTask()
                .Union(_ => ApiResponse.Ok(data2))
                .Union(() => ApiResponse.Ok(data3))
                .Return(mock.Object.Execute);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            mock.Verify(
                v => v.Execute(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Never());
        }

        [Fact(DisplayName = "(Async ValueTask, Sync value factory, T4) Should return value if response successful")]
        public async Task AsyncValueTask_SyncValueFactory_T4_Successful_ReturnsValue()
        {
            const int data1 = 12;
            const int data2 = 23;
            const int data3 = 34;
            const int data4 = 45;
            const string expected = "test";

            var mock = new Mock<ISyncReturnProvider<int, int, int, int, string>>();

            mock.Setup(
                    v => v.Execute(
                        data1,
                        data2,
                        data3,
                        data4))
                .Returns(expected);

            var response = await ApiResponse
                .Ok(data1)
                .AsValueTask()
                .Union(_ => ApiResponse.Ok(data2))
                .Union(() => ApiResponse.Ok(data3))
                .Union(() => ApiResponse.Ok(data4))
                .Return(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);

            mock.Verify(
                v => v.Execute(
                    data1,
                    data2,
                    data3,
                    data4),
                Times.Once());
        }

        [Fact(DisplayName = "(Async ValueTask, Sync value factory, T4) Should return error if response has error")]
        public async Task AsyncValueTask_SyncValueFactory_T4_HasErrors_ReturnsValue()
        {
            const int data2 = 23;
            const int data3 = 34;
            const int data4 = 45;
            const string expected = "test";

            var mock = new Mock<ISyncReturnProvider<int, int, int, int, string>>();

            mock.Setup(
                    v => v.Execute(
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>()))
                .Returns(expected);

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsValueTask()
                .Union(_ => ApiResponse.Ok(data2))
                .Union(() => ApiResponse.Ok(data3))
                .Union(() => ApiResponse.Ok(data4))
                .Return(mock.Object.Execute);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            mock.Verify(
                v => v.Execute(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Never());
        }

        [Fact(DisplayName = "(Async ValueTask, Sync value factory, T5) Should return value if response successful")]
        public async Task AsyncValueTask_SyncValueFactory_T5_Successful_ReturnsValue()
        {
            const int data1 = 12;
            const int data2 = 23;
            const int data3 = 34;
            const int data4 = 45;
            const int data5 = 56;
            const string expected = "test";

            var mock = new Mock<ISyncReturnProvider<int, int, int, int, int, string>>();

            mock.Setup(
                    v => v.Execute(
                        data1,
                        data2,
                        data3,
                        data4,
                        data5))
                .Returns(expected);

            var response = await ApiResponse
                .Ok(data1)
                .AsValueTask()
                .Union(_ => ApiResponse.Ok(data2))
                .Union(() => ApiResponse.Ok(data3))
                .Union(() => ApiResponse.Ok(data4))
                .Union(() => ApiResponse.Ok(data5))
                .Return(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);

            mock.Verify(
                v => v.Execute(
                    data1,
                    data2,
                    data3,
                    data4,
                    data5),
                Times.Once());
        }

        [Fact(DisplayName = "(Async ValueTask, Sync value factory, T5) Should return error if response has error")]
        public async Task AsyncValueTask_SyncValueFactory_T5_HasErrors_ReturnsValue()
        {
            const int data2 = 23;
            const int data3 = 34;
            const int data4 = 45;
            const int data5 = 56;
            const string expected = "test";

            var mock = new Mock<ISyncReturnProvider<int, int, int, int, int, string>>();

            mock.Setup(
                    v => v.Execute(
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>()))
                .Returns(expected);

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsValueTask()
                .Union(_ => ApiResponse.Ok(data2))
                .Union(() => ApiResponse.Ok(data3))
                .Union(() => ApiResponse.Ok(data4))
                .Union(() => ApiResponse.Ok(data5))
                .Return(mock.Object.Execute);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            mock.Verify(
                v => v.Execute(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Never());
        }
    }
}
