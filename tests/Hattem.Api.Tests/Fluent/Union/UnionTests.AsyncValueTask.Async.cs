using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Union
{
    partial class UnionTests
    {
        [Theory(DisplayName = "(Async ValueTask, Async) T2 Should return value tuple of responses")]
        [InlineData(null, null, null)]
        [InlineData(101, 202, 101)]
        [InlineData(null, 202, 202)]
        [InlineData(101, null, 101)]
        public async Task AsyncValueTask_Async_Union_T2_BothAreOk_ReturnsValueTuple(
            int? statusCode1,
            int? statusCode2,
            int? expectedStatusCode
        )
        {
            const int data1 = 12;
            const string data2 = "tests";

            var mock = new Mock<IAsyncResponseProvider<int, string>>();

            mock.Setup(v => v.Execute(data1))
                .Returns(
                    ApiResponse
                        .Ok(data2)
                        .WithStatusCode(statusCode2)
                        .AsTask());

            var response = await ApiResponse
                .Ok(data1)
                .WithStatusCode(statusCode1)
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.Equal(data1, response.Data.Item1);
            Assert.Equal(data2, response.Data.Item2);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Async ValueTask, Async) T2 Should return error if source has errors")]
        public async Task AsyncValueTask_Async_Union_T2_SourceHasErrors_ReturnsError()
        {
            const int expectedStatusCode = 301;

            var mock = new Mock<IAsyncResponseProvider<int, string>>();

            mock.Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.Error<string>(AnotherTestError.Default).AsTask());

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Async ValueTask, Async) T2 Should return error if selector has errors")]
        public async Task AsyncValueTask_Async_Union_T2_SelectorHasErrors_ReturnsError()
        {
            const int expectedStatusCode = 301;

            var mock = new Mock<IAsyncResponseProvider<int, string>>();

            mock.Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(
                    ApiResponse
                        .Error<string>(AnotherTestError.Default)
                        .WithStatusCode(expectedStatusCode)
                        .AsTask());

            var response = await ApiResponse
                .Ok(2)
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(AnotherTestError.Default, response.Error);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory(DisplayName = "(Async ValueTask, Async) T3 Should return value tuple of responses")]
        [InlineData(null, null, null)]
        [InlineData(101, 202, 101)]
        [InlineData(null, 202, 202)]
        [InlineData(101, null, 101)]
        public async Task AsyncValueTask_Async_Union_T3_BothAreOk_ReturnsValueTuple(
            int? statusCode1,
            int? statusCode2,
            int? expectedStatusCode
        )
        {
            const int data1 = 12;
            const string data2 = "tests";
            const string data3 = "tests";

            var mock = new Mock<IAsyncResponseProvider<int, string, string>>();

            mock.Setup(v => v.Execute(data1, data2))
                .Returns(
                    ApiResponse
                        .Ok(data3)
                        .WithStatusCode(statusCode2)
                        .AsTask());

            var response = await ApiResponse
                .Ok(data1)
                .Union(_ => ApiResponse.Ok(data2))
                .WithStatusCode(statusCode1)
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.Equal(data1, response.Data.Item1);
            Assert.Equal(data2, response.Data.Item2);
            Assert.Equal(data3, response.Data.Item3);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory(DisplayName = "(Async ValueTask, Async) T3 Parameterless Should return value tuple of responses")]
        [InlineData(null, null, null)]
        [InlineData(101, 202, 101)]
        [InlineData(null, 202, 202)]
        [InlineData(101, null, 101)]
        public async Task AsyncValueTask_Async_Union_T3_Parameterless_BothAreOk_ReturnsValueTuple(
            int? statusCode1,
            int? statusCode2,
            int? expectedStatusCode
        )
        {
            const int data1 = 12;
            const string data2 = "tests";
            const string data3 = "tests";

            var mock = new Mock<IAsyncResponseProvider<string>>();

            mock.Setup(v => v.Execute())
                .Returns(
                    ApiResponse
                        .Ok(data3)
                        .WithStatusCode(statusCode2)
                        .AsTask());

            var response = await ApiResponse
                .Ok(data1)
                .Union(_ => ApiResponse.Ok(data2))
                .WithStatusCode(statusCode1)
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.Equal(data1, response.Data.Item1);
            Assert.Equal(data2, response.Data.Item2);
            Assert.Equal(data3, response.Data.Item3);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Async ValueTask, Async) T3 Should return error if source has errors")]
        public async Task AsyncValueTask_Async_Union_T3_SourceHasErrors_ReturnsError()
        {
            const int expectedStatusCode = 301;

            var mock = new Mock<IAsyncResponseProvider<int, int, string>>();

            mock.Setup(v => v.Execute(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(ApiResponse.Error<string>(AnotherTestError.Default).AsTask());

            var response = await ApiResponse
                .Error<(int, int)>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Async ValueTask, Async) T3 Should return error if selector has errors")]
        public async Task AsyncValueTask_Async_Union_T3_SelectorHasErrors_ReturnsError()
        {
            const int expectedStatusCode = 301;

            var mock = new Mock<IAsyncResponseProvider<int, int, string>>();

            mock.Setup(v => v.Execute(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(
                    ApiResponse
                        .Error<string>(AnotherTestError.Default)
                        .WithStatusCode(expectedStatusCode)
                        .AsTask());

            var response = await ApiResponse
                .Ok(2)
                .Union(_ => ApiResponse.Ok(3))
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(AnotherTestError.Default, response.Error);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory(DisplayName = "(Async ValueTask, Async) T4 Should return value tuple of responses")]
        [InlineData(null, null, null)]
        [InlineData(101, 202, 101)]
        [InlineData(null, 202, 202)]
        [InlineData(101, null, 101)]
        public async Task AsyncValueTask_Async_Union_T4_BothAreOk_ReturnsValueTuple(
            int? statusCode1,
            int? statusCode2,
            int? expectedStatusCode
        )
        {
            const int data1 = 12;
            const string data2 = "tests";
            const string data3 = "tests";
            const string data4 = "tests";

            var mock = new Mock<IAsyncResponseProvider<int, string, string, string>>();

            mock.Setup(v => v.Execute(data1, data2, data3))
                .Returns(
                    ApiResponse
                        .Ok(data4)
                        .WithStatusCode(statusCode2)
                        .AsTask());

            var response = await ApiResponse
                .Ok(data1)
                .Union(t1 => ApiResponse.Ok(data2))
                .Union((t1, t2) => ApiResponse.Ok(data3))
                .WithStatusCode(statusCode1)
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.Equal(data1, response.Data.Item1);
            Assert.Equal(data2, response.Data.Item2);
            Assert.Equal(data3, response.Data.Item3);
            Assert.Equal(data4, response.Data.Item4);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory(DisplayName = "(Async ValueTask, Async) T4 Parameterless Should return value tuple of responses")]
        [InlineData(null, null, null)]
        [InlineData(101, 202, 101)]
        [InlineData(null, 202, 202)]
        [InlineData(101, null, 101)]
        public async Task AsyncValueTask_Async_Union_T4_Parameterless_BothAreOk_ReturnsValueTuple(
            int? statusCode1,
            int? statusCode2,
            int? expectedStatusCode
        )
        {
            const int data1 = 12;
            const string data2 = "tests";
            const string data3 = "tests";
            const string data4 = "tests";

            var mock = new Mock<IAsyncResponseProvider<string>>();

            mock.Setup(v => v.Execute())
                .Returns(
                    ApiResponse
                        .Ok(data4)
                        .WithStatusCode(statusCode2)
                        .AsTask());

            var response = await ApiResponse
                .Ok(data1)
                .Union(t1 => ApiResponse.Ok(data2))
                .Union((t1, t2) => ApiResponse.Ok(data3))
                .WithStatusCode(statusCode1)
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.Equal(data1, response.Data.Item1);
            Assert.Equal(data2, response.Data.Item2);
            Assert.Equal(data3, response.Data.Item3);
            Assert.Equal(data4, response.Data.Item4);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Async ValueTask, Async) T4 Should return error if source has errors")]
        public async Task AsyncValueTask_Async_Union_T4_SourceHasErrors_ReturnsError()
        {
            const int expectedStatusCode = 301;

            var mock = new Mock<IAsyncResponseProvider<int, int, int, string>>();

            mock.Setup(
                    v => v.Execute(
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>()))
                .Returns(ApiResponse.Error<string>(AnotherTestError.Default).AsTask());

            var response = await ApiResponse
                .Error<(int, int, int)>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Async ValueTask, Async) T4 Should return error if selector has errors")]
        public async Task AsyncValueTask_Async_Union_T4_SelectorHasErrors_ReturnsError()
        {
            const int expectedStatusCode = 301;

            var mock = new Mock<IAsyncResponseProvider<int, int, int, string>>();

            mock.Setup(
                    v => v.Execute(
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>()))
                .Returns(
                    ApiResponse
                        .Error<string>(AnotherTestError.Default)
                        .WithStatusCode(expectedStatusCode)
                        .AsTask());

            var response = await ApiResponse
                .Ok(2)
                .Union(_ => ApiResponse.Ok(3))
                .Union((t1, t2) => ApiResponse.Ok(4))
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(AnotherTestError.Default, response.Error);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory(DisplayName = "(Async ValueTask, Async) T5 Should return value tuple of responses")]
        [InlineData(null, null, null)]
        [InlineData(101, 202, 101)]
        [InlineData(null, 202, 202)]
        [InlineData(101, null, 101)]
        public async Task AsyncValueTask_Async_Union_T5_BothAreOk_ReturnsValueTuple(
            int? statusCode1,
            int? statusCode2,
            int? expectedStatusCode
        )
        {
            const int data1 = 12;
            const string data2 = "tests";
            const string data3 = "tests";
            const string data4 = "tests";
            const string data5 = "tests5";

            var mock = new Mock<IAsyncResponseProvider<int, string, string, string, string>>();

            mock.Setup(v => v.Execute(data1, data2, data3, data4))
                .Returns(
                    ApiResponse
                        .Ok(data5)
                        .WithStatusCode(statusCode2)
                        .AsTask());

            var response = await ApiResponse
                .Ok(data1)
                .Union(t1 => ApiResponse.Ok(data2))
                .Union((t1, t2) => ApiResponse.Ok(data3))
                .Union((t1, t2, t3) => ApiResponse.Ok(data4))
                .WithStatusCode(statusCode1)
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.Equal(data1, response.Data.Item1);
            Assert.Equal(data2, response.Data.Item2);
            Assert.Equal(data3, response.Data.Item3);
            Assert.Equal(data4, response.Data.Item4);
            Assert.Equal(data5, response.Data.Item5);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory(DisplayName = "(Async ValueTask, Async) T5 Parameterless Should return value tuple of responses")]
        [InlineData(null, null, null)]
        [InlineData(101, 202, 101)]
        [InlineData(null, 202, 202)]
        [InlineData(101, null, 101)]
        public async Task AsyncValueTask_Async_Union_T5_Parameterless_BothAreOk_ReturnsValueTuple(
            int? statusCode1,
            int? statusCode2,
            int? expectedStatusCode
        )
        {
            const int data1 = 12;
            const string data2 = "tests";
            const string data3 = "tests";
            const string data4 = "tests";
            const string data5 = "tests5";

            var mock = new Mock<IAsyncResponseProvider<string>>();

            mock.Setup(v => v.Execute())
                .Returns(
                    ApiResponse
                        .Ok(data5)
                        .WithStatusCode(statusCode2)
                        .AsTask());

            var response = await ApiResponse
                .Ok(data1)
                .Union(t1 => ApiResponse.Ok(data2))
                .Union((t1, t2) => ApiResponse.Ok(data3))
                .Union((t1, t2, t3) => ApiResponse.Ok(data4))
                .WithStatusCode(statusCode1)
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.IsOk);
            Assert.Equal(data1, response.Data.Item1);
            Assert.Equal(data2, response.Data.Item2);
            Assert.Equal(data3, response.Data.Item3);
            Assert.Equal(data4, response.Data.Item4);
            Assert.Equal(data5, response.Data.Item5);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Async ValueTask, Async) T5 Should return error if source has errors")]
        public async Task AsyncValueTask_Async_Union_T5_SourceHasErrors_ReturnsError()
        {
            const int expectedStatusCode = 301;

            var mock = new Mock<IAsyncResponseProvider<int, int, int, int, string>>();

            mock.Setup(
                    v => v.Execute(
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>()))
                .Returns(ApiResponse.Error<string>(AnotherTestError.Default).AsTask());

            var response = await ApiResponse
                .Error<(int, int, int, int)>(TestError.Default)
                .WithStatusCode(expectedStatusCode)
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact(DisplayName = "(Async ValueTask, Async) T5 Should return error if selector has errors")]
        public async Task AsyncValueTask_Async_Union_T5_SelectorHasErrors_ReturnsError()
        {
            const int expectedStatusCode = 301;

            var mock = new Mock<IAsyncResponseProvider<int, int, int, int, string>>();

            mock.Setup(
                    v => v.Execute(
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>()))
                .Returns(
                    ApiResponse
                        .Error<string>(AnotherTestError.Default)
                        .WithStatusCode(expectedStatusCode)
                        .AsTask());

            var response = await ApiResponse
                .Ok(2)
                .Union(_ => ApiResponse.Ok(3))
                .Union((t1, t2) => ApiResponse.Ok(4))
                .Union((t1, t2, t3) => ApiResponse.Ok(5))
                .AsValueTask()
                .Union(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(AnotherTestError.Default, response.Error);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }
    }
}
