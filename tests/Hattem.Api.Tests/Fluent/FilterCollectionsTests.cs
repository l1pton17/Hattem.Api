using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Moq;
using Xunit;

namespace Hattem.Api.Tests.Fluent
{
    [CategoryTrait("Filter collections")]
    public sealed class FilterCollectionsTests
    {
        #region Sync onError

        #region Sync

        [Fact(DisplayName = "(Sync, Sync onError) Should returns all items if predicate returns ok")]
        public void Sync_SyncOnError_AllAreOk_Returns()
        {
            var predicateMock = new Mock<ISyncResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.Ok());

            var expected = Enumerable
                .Range(0, 10)
                .ToArray();

            var response = expected.Filter(v => predicateMock.Object.Execute(v));

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Sync, Sync onError) Should returns empty items if predicate returns error")]
        public void Sync_SyncOnError_AllHasErrors_ReturnsEmpty()
        {
            var handledErrorItems = new Dictionary<int, Error>();

            var predicateMock = new Mock<ISyncResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.Error(TestError.Default));

            var expected = Enumerable
                .Range(0, 10)
                .ToArray();

            var response = expected
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

        [Fact(DisplayName = "(Sync, Sync onError) Should returns items without error when predicate returns error")]
        public void Sync_SyncOnError_SomeHasErrors_ReturnsItemWithoutError()
        {
            var handledErrorItems = new Dictionary<int, Error>();

            var predicateMock = new Mock<ISyncResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns<int>(
                    v => v % 2 == 0
                        ? ApiResponse.Ok()
                        : ApiResponse.Error(TestError.Default));

            var values = Enumerable
                .Range(0, 10)
                .ToArray();

            var expectedMissing = values
                .Where(v => v % 2 != 0)
                .ToArray();

            var response = values
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

        #region Sync

        [Fact(DisplayName = "(Async, Sync onError) Should returns all items if predicate returns ok")]
        public async Task Async_SyncOnError_AllAreOk_Returns()
        {
            var predicateMock = new Mock<IAsyncResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.OkAsync());

            var expected = Enumerable
                .Range(0, 10)
                .ToArray();

            var response = await expected.Filter(v => predicateMock.Object.Execute(v));

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async, Sync onError) Should returns empty items if predicate returns error")]
        public async Task Async_SyncOnError_AllHasErrors_ReturnsEmpty()
        {
            var handledErrorItems = new Dictionary<int, Error>();

            var predicateMock = new Mock<IAsyncResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.Error(TestError.Default).AsTask());

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

        [Fact(DisplayName = "(Async, Sync onError) Should returns items without error when predicate returns error")]
        public async Task Async_SyncOnError_SomeHasErrors_ReturnsItemWithoutError()
        {
            var handledErrorItems = new Dictionary<int, Error>();

            var predicateMock = new Mock<IAsyncResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns<int>(
                    v => v % 2 == 0
                        ? ApiResponse.OkAsync()
                        : ApiResponse.Error(TestError.Default).AsTask());

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

        #region Sync

        [Fact(DisplayName = "(Sync, Async onError) Should returns empty items if predicate returns error")]
        public async Task Sync_AsyncOnError_AllHasErrors_ReturnsEmpty()
        {
            var handledErrorItems = new Dictionary<int, Error>();

            var predicateMock = new Mock<ISyncResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.Error(TestError.Default));

            var expected = Enumerable
                .Range(0, 10)
                .ToArray();

            var response = await expected
                .Filter(
                    v => predicateMock.Object.Execute(v),
                    (item, error) =>
                    {
                        handledErrorItems[item] = error;

                        return Task.CompletedTask;
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

        [Fact(DisplayName = "(Sync, Async onError) Should returns items without error when predicate returns error")]
        public async Task Sync_AsyncOnError_SomeHasErrors_ReturnsItemWithoutError()
        {
            var handledErrorItems = new Dictionary<int, Error>();

            var predicateMock = new Mock<ISyncResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns<int>(
                    v => v % 2 == 0
                        ? ApiResponse.Ok()
                        : ApiResponse.Error(TestError.Default));

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

                        return Task.CompletedTask;
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

        #region Sync

        [Fact(DisplayName = "(Async, Async onError) Should returns empty items if predicate returns error")]
        public async Task Async_AsyncOnError_AllHasErrors_ReturnsEmpty()
        {
            var handledErrorItems = new Dictionary<int, Error>();

            var predicateMock = new Mock<IAsyncResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.Error(TestError.Default).AsTask());

            var expected = Enumerable
                .Range(0, 10)
                .ToArray();

            var response = await expected
                .Filter(
                    v => predicateMock.Object.Execute(v),
                    (item, error) =>
                    {
                        handledErrorItems[item] = error;

                        return Task.CompletedTask;
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

        [Fact(DisplayName = "(Async, Async onError) Should returns items without error when predicate returns error")]
        public async Task Async_AsyncOnError_SomeHasErrors_ReturnsItemWithoutError()
        {
            var handledErrorItems = new Dictionary<int, Error>();

            var predicateMock = new Mock<IAsyncResponseProvider<int, Unit>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns<int>(
                    v => v % 2 == 0
                        ? ApiResponse.OkAsync()
                        : ApiResponse.Error(TestError.Default).AsTask());

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

                        return Task.CompletedTask;
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

        #region Sync

        [Fact(DisplayName = "(Sync, Boolean predicate) Should return only values where predicate returns true")]
        public void Sync_BooleanPredicate_PredicateIsOk_ReturnsOnlyTrueValues()
        {
            var values = Enumerable
                .Range(0, 10)
                .ToArray();

            var expected = values
                .Where(v => v % 2 == 0)
                .ToArray();

            var response = values.Filter(v => ApiResponse.Ok(v % 2 == 0));

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Sync, Boolean predicate) Should return predicate error")]
        public void Sync_BooleanPredicate_PredicateHasErrors_ReturnsPredicateError()
        {
            var predicateMock = new Mock<ISyncResponseProvider<int, bool>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.Error<bool>(TestError.Default));

            var values = Enumerable
                .Range(0, 10)
                .ToArray();

            var response = values.Filter(predicateMock.Object.Execute);

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);

            predicateMock
                .Verify(v => v.Execute(It.IsAny<int>()), Times.Once());
        }

        #endregion

        #region Async

        [Fact(DisplayName = "(Async, Boolean predicate) Should return only values where predicate returns true")]
        public async Task Async_BooleanPredicate_PredicateIsOk_ReturnsOnlyTrueValues()
        {
            var values = Enumerable
                .Range(0, 10)
                .ToArray();

            var expected = values
                .Where(v => v % 2 == 0)
                .ToArray();

            var response = await values.Filter(v => ApiResponse.Ok(v % 2 == 0).AsTask());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async, Boolean predicate) Should return predicate error")]
        public async Task Async_BooleanPredicate_PredicateHasErrors_ReturnsPredicateError()
        {
            var predicateMock = new Mock<IAsyncResponseProvider<int, bool>>();

            predicateMock
                .Setup(v => v.Execute(It.IsAny<int>()))
                .Returns(ApiResponse.Error<bool>(TestError.Default).AsTask());

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