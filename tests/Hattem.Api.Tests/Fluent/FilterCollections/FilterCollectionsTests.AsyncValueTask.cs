using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent.FilterCollections
{
    partial class FilterCollectionsTests
    {
        #region Sync onError

        #region Async ValueTask

        [Fact(DisplayName = "(Async ValueTask) Should throw exception when source is null")]
        public void AsyncValueTask_SyncOnError_ThrowsException()
        {
            IEnumerable<int> source = null;

            Assert.ThrowsAsync<ArgumentNullException>(() => source.Filter(_ => ApiResponse.OkAsyncValueTask().AsTask()));
        }

        [Fact(DisplayName = "(Async ValueTask, Sync onError) Should returns all items if predicate returns ok")]
        public async Task AsyncValueTask_SyncOnError_AllAreOk_Returns()
        {
            var predicateMock = new Mock<IAsyncValueTaskResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.OkAsyncValueTask());

            var expected = Enumerable
                .Range(0, 10)
                .ToArray();

            var response = await expected.Filter(v => predicateMock.Object.Execute(v));

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask, Sync onError) Should returns empty items if predicate returns error")]
        public async Task AsyncValueTask_SyncOnError_AllHasErrors_ReturnsEmpty()
        {
            var handledErrorItems = new Dictionary<int, Error>();

            var predicateMock = new Mock<IAsyncValueTaskResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.Error(TestError.Default).AsValueTask());

            var expected = Enumerable
                .Range(0, 10)
                .ToArray();

            var response = await expected
                .Filter(
                    v => predicateMock.Object.Execute(v),
                    (item, error) => handledErrorItems[item] = error);

            Assert.True(response.IsOk);
            Assert.Empty(response.Data);
            Assert.Equal(expected.Length, handledErrorItems.Count);

            Assert.Equal(
                expected.OrderBy(v => v),
                handledErrorItems.Keys.OrderBy(v => v));

            Assert.All(
                handledErrorItems.Values,
                v => Assert.Equal(TestError.Default, v, ErrorComparer.Default));
        }

        [Fact(DisplayName = "(Async ValueTask, Sync onError) Should returns items without error when predicate returns error")]
        public async Task AsyncValueTask_SyncOnError_SomeHasErrors_ReturnsItemWithoutError()
        {
            var handledErrorItems = new Dictionary<int, Error>();

            var predicateMock = new Mock<IAsyncValueTaskResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns<int>(
                    v => v % 2 == 0
                        ? ApiResponse.OkAsyncValueTask()
                        : ApiResponse.Error(TestError.Default).AsValueTask());

            var values = Enumerable
                .Range(0, 10)
                .ToArray();

            var expectedMissing = values
                .Where(v => v % 2 != 0)
                .ToArray();

            var response = await values
                .Filter(
                    v => predicateMock.Object.Execute(v),
                    (item, error) => handledErrorItems[item] = error);

            Assert.True(response.IsOk);
            Assert.Equal(values.Where(v => v % 2 == 0), response.Data);
            Assert.Equal(expectedMissing.Length, handledErrorItems.Count);

            Assert.Equal(
                expectedMissing.OrderBy(v => v),
                handledErrorItems.Keys.OrderBy(v => v));

            Assert.All(
                handledErrorItems.Values,
                v => Assert.Equal(TestError.Default, v, ErrorComparer.Default));
        }

        #endregion

        #endregion

        #region Async onError

        #region Async ValueTask

        [Fact(DisplayName = "(Async ValueTask, Async ValueTask onError) Should returns empty items if predicate returns error")]
        public async Task AsyncValueTask_AsyncValueTaskOnError_AllHasErrors_ReturnsEmpty()
        {
            var handledErrorItems = new Dictionary<int, Error>();

            var predicateMock = new Mock<IAsyncValueTaskResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.Error(TestError.Default).AsValueTask());

            var expected = Enumerable
                .Range(0, 10)
                .ToArray();

            var response = await expected
                .Filter(
                    v => predicateMock.Object.Execute(v),
                    (item, error) =>
                    {
                        handledErrorItems[item] = error;

                        return new ValueTask();
                    });

            Assert.True(response.IsOk);
            Assert.Empty(response.Data);
            Assert.Equal(expected.Length, handledErrorItems.Count);

            Assert.Equal(
                expected.OrderBy(v => v),
                handledErrorItems.Keys.OrderBy(v => v));

            Assert.All(
                handledErrorItems.Values,
                v => Assert.Equal(TestError.Default, v, ErrorComparer.Default));
        }

        [Fact(DisplayName = "(Async ValueTask, Async ValueTask onError) Should returns items without error when predicate returns error")]
        public async Task AsyncValueTask_AsyncValueTaskOnError_SomeHasErrors_ReturnsItemWithoutError()
        {
            var handledErrorItems = new Dictionary<int, Error>();

            var predicateMock = new Mock<IAsyncValueTaskResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns<int>(
                    v => v % 2 == 0
                        ? ApiResponse.OkAsyncValueTask()
                        : ApiResponse.Error(TestError.Default).AsValueTask());

            var values = Enumerable
                .Range(0, 10)
                .ToArray();

            var expectedMissing = values
                .Where(v => v % 2 != 0)
                .ToArray();

            var response = await values
                .Filter(
                    v => predicateMock.Object.Execute(v),
                    (item, error) =>
                    {
                        handledErrorItems[item] = error;

                        return new ValueTask();
                    });

            Assert.True(response.IsOk);
            Assert.Equal(values.Where(v => v % 2 == 0), response.Data);
            Assert.Equal(expectedMissing.Length, handledErrorItems.Count);

            Assert.Equal(
                expectedMissing.OrderBy(v => v),
                handledErrorItems.Keys.OrderBy(v => v));

            Assert.All(
                handledErrorItems.Values,
                v => Assert.Equal(TestError.Default, v, ErrorComparer.Default));
        }

        #endregion

        #endregion

        #region Boolean predicate

        #region Async ValueTask

        [Fact(DisplayName = "(Async ValueTask, Boolean predicate) Should throw exception when source is null")]
        public void AsyncValueTask_BooleanPredicate_ThrowsException()
        {
            IEnumerable<int> source = null;

            Assert.Throws<ArgumentNullException>(() => source.Filter(_ => ApiResponse.Ok(true)));
        }

        [Fact(DisplayName = "(Async ValueTask, Boolean predicate) Should return only values where predicate returns true")]
        public async Task AsyncValueTask_BooleanPredicate_PredicateIsOk_ReturnsOnlyTrueValues()
        {
            var values = Enumerable
                .Range(0, 10)
                .ToArray();

            var expected = values
                .Where(v => v % 2 == 0)
                .ToArray();

            var response = await values.Filter(v => ApiResponse.Ok(v % 2 == 0).AsValueTask());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask, Boolean predicate) Should return predicate error")]
        public async Task AsyncValueTask_BooleanPredicate_PredicateHasErrors_ReturnsPredicateError()
        {
            var predicateMock = new Mock<IAsyncValueTaskResponseProvider<int, bool>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.Error<bool>(TestError.Default).AsValueTask());

            var values = Enumerable
                .Range(0, 10)
                .ToArray();

            var response = await values.Filter(predicateMock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            predicateMock
                .Verify(v => v.Execute(It.IsAny<int>()), Times.Once());
        }

        #endregion

        #endregion
    }
}
