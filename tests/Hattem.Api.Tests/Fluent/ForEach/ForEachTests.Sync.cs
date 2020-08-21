using System.Collections.Immutable;
using System.Linq;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent.ForEach
{
    [CategoryTrait(nameof(ApiResponseExtensions.ForEach) + " tests")]
    public sealed partial class ForEachTests
    {
        [Fact(DisplayName = "(Sync) Should return ok if all responses are ok")]
        public void Sync_ResponseAreOk_ReturnOk()
        {
            var mock = new Mock<ISyncResponseProvider<int, Unit>>();

            mock.Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.Ok());

            var values = Enumerable
                .Range(0, 100)
                .ToImmutableArray();

            var response = values.ForEach(mock.Object.Execute);

            Assert.True(response.IsOk);

            foreach (var value in values)
            {
                mock.Verify(v => v.Execute(value), Times.Once());
            }
        }

        [Fact(DisplayName = "(Sync, Index) Should return ok if all responses are ok")]
        public void Sync_Index_ResponseAreOk_ReturnOk()
        {
            var mock = new Mock<ISyncResponseProvider<int, int, Unit>>();

            mock.Setup(v => v.Execute(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(ApiResponse.Ok());

            var values = Enumerable
                .Range(0, 100)
                .ToImmutableArray();

            var response = values.ForEach(mock.Object.Execute);

            Assert.True(response.IsOk);

            foreach (var i in values)
            {
                mock.Verify(v => v.Execute(values[i], i), Times.Once());
            }
        }

        [Fact(DisplayName = "(Sync) Should return error if one of responses has error")]
        public void Sync_ResponseHasError_ReturnError()
        {
            const int threshold = 30;

            var mock = new Mock<ISyncResponseProvider<int, Unit>>();

            mock.Setup(v => v.Execute(It.IsAny<int>()))
                .Returns<int>(
                    v => v > threshold
                        ? ApiResponse.Error(TestError.Default)
                        : ApiResponse.Ok());

            var values = Enumerable
                .Range(0, 100)
                .ToImmutableArray();

            var response = values.ForEach(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            for (var i = 0; i <= threshold; i++)
            {
                mock.Verify(v => v.Execute(values[i]), Times.Once());
            }
        }

        [Fact(DisplayName = "(Sync, Index) Should return error if one of responses has error")]
        public void Sync_Index_ResponseHasError_ReturnError()
        {
            const int threshold = 30;

            var mock = new Mock<ISyncResponseProvider<int, int, Unit>>();

            mock.Setup(v => v.Execute(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>(
                    (v, _) => v > threshold
                        ? ApiResponse.Error(TestError.Default)
                        : ApiResponse.Ok());

            var values = Enumerable
                .Range(0, 100)
                .ToImmutableArray();

            var response = values.ForEach(mock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            for (var i = 0; i <= threshold; i++)
            {
                mock.Verify(v => v.Execute(values[i], i), Times.Once());
            }
        }
    }
}
