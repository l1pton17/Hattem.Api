using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent.ForEach
{
    partial class ForEachTests
    {
        [Fact(DisplayName = "(Async ValueTask) Should return ok if all responses are ok")]
        public async Task AsyncValueTask_ResponseAreOk_ReturnOk()
        {
            var mock = new Mock<IAsyncValueTaskResponseProvider<int, Unit>>();

            mock.Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.OkAsyncValueTask());

            var values = Enumerable
                .Range(0, 100)
                .ToImmutableArray();

            var response = await values.ForEach(mock.Object.Execute);

            Assert.True(response.IsOk);

            foreach (var value in values)
            {
                mock.Verify(v => v.Execute(value), Times.Once());
            }
        }

        [Fact(DisplayName = "(Async ValueTask, Index) Should return ok if all responses are ok")]
        public async Task AsyncValueTask_Index_ResponseAreOk_ReturnOk()
        {
            var mock = new Mock<IAsyncValueTaskResponseProvider<int, int, Unit>>();

            mock.Setup(v => v.Execute(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(ApiResponse.OkAsyncValueTask());

            var values = Enumerable
                .Range(0, 100)
                .ToImmutableArray();

            var response = await values.ForEach(mock.Object.Execute);

            Assert.True(response.IsOk);

            for (var i = 0; i < values.Length; i++)
            {
                mock.Verify(v => v.Execute(values[i], i), Times.Once());
            }
        }

        [Fact(DisplayName = "(Async ValueTask) Should return error if one of responses has error")]
        public async Task AsyncValueTask_ResponseHasError_ReturnError()
        {
            const int threshold = 30;

            var mock = new Mock<IAsyncValueTaskResponseProvider<int, Unit>>();

            mock.Setup(v => v.Execute(It.IsAny<int>()))
                .Returns<int>(
                    v => v > threshold
                        ? ApiResponse.Error(TestError.Default).AsValueTask()
                        : ApiResponse.OkAsyncValueTask());

            var values = Enumerable
                .Range(0, 100)
                .ToImmutableArray();

            var response = await values.ForEach(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            for (var i = 0; i <= threshold; i++)
            {
                mock.Verify(v => v.Execute(values[i]), Times.Once());
            }
        }

        [Fact(DisplayName = "(Async ValueTask, Index) Should return error if one of responses has error")]
        public async Task AsyncValueTask_Index_ResponseHasError_ReturnError()
        {
            const int threshold = 30;

            var mock = new Mock<IAsyncValueTaskResponseProvider<int, int, Unit>>();

            mock.Setup(v => v.Execute(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>(
                    (v, _) => v > threshold
                        ? ApiResponse.Error(TestError.Default).AsValueTask()
                        : ApiResponse.OkAsyncValueTask());

            var values = Enumerable
                .Range(0, 100)
                .ToImmutableArray();

            var response = await values.ForEach(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            for (var i = 0; i <= threshold; i++)
            {
                mock.Verify(v => v.Execute(values[i], i), Times.Once());
            }
        }
    }
}
