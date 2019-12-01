using System.Threading.Tasks;
using Hattem.Api.Extensions;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests
{
    [CategoryTrait("Return")]
    public sealed class ReturnTests
    {
        #region Simple value

        #region Sync

        [Fact(DisplayName = "(Sync, Simple value) Should return value if response successful")]
        public void Sync_Successful_ReturnsValue()
        {
            const string expected = "test";

            var response = ApiResponse
                .Ok(1)
                .Return(expected);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Sync, Simple value) Should return error if response has error")]
        public void Sync_HasErrors_ReturnsValue()
        {
            const string expected = "test";

            var response = ApiResponse
                .Error<int>(TestError.Default)
                .Return(expected);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
        }

        #endregion

        #region Async

        [Fact(DisplayName = "(Async, Simple value) Should return value if response successful")]
        public async Task Async_Successful_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Ok(1)
                .AsTask()
                .Return(expected);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async, Simple value) Should return error if response has error")]
        public async Task Async_HasErrors_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsTask()
                .Return(expected);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
        }

        #endregion

        #endregion

        #region Value factory

        #region Sync

        [Fact(DisplayName = "(Sync, Value factory) Should return value if response successful")]
        public void Sync_ValueFactory_Successful_ReturnsValue()
        {
            const string expected = "test";

            var response = ApiResponse
                .Ok(1)
                .Return(_ => expected);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Sync, Value factory) Should return error if response has error")]
        public void Sync_ValueFactory_HasErrors_ReturnsValue()
        {
            const string expected = "test";

            var response = ApiResponse
                .Error<int>(TestError.Default)
                .Return(_ => expected);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
        }

        #endregion

        #region Async

        [Fact(DisplayName = "(Async, Value factory) Should return value if response successful")]
        public async Task Async_ValueFactory_Successful_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Ok(1)
                .AsTask()
                .Return(_ => expected);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async, Value factory) Should return error if response has error")]
        public async Task Async_ValueFactory_HasErrors_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsTask()
                .Return(_ => expected);

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
        }

        #endregion

        #endregion

        #region Async value factory

        #region Sync

        [Fact(DisplayName = "(Sync, Async value factory) Should return value if response successful")]
        public async Task Sync_AsyncValueFactory_Successful_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Ok(1)
                .Return(_ => Task.FromResult(expected));

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Sync, Async value factory) Should return error if response has error")]
        public async Task Sync_AsyncValueFactory_HasErrors_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .Return(_ => Task.FromResult(expected));

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
        }

        #endregion

        #region Async

        [Fact(DisplayName = "(Async, Async value factory) Should return value if response successful")]
        public async Task Async_AsyncValueFactory_Successful_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Ok(1)
                .AsTask()
                .Return(_ => Task.FromResult(expected));

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async, Async value factory) Should return error if response has error")]
        public async Task Async_AsyncValueFactory_HasErrors_ReturnsValue()
        {
            const string expected = "test";

            var response = await ApiResponse
                .Error<int>(TestError.Default)
                .AsTask()
                .Return(_ => Task.FromResult(expected));

            Assert.False(response.IsOk);
            Assert.Null(response.Data);
        }

        #endregion

        #endregion
    }
}