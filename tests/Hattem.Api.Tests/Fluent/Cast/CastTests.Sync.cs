using Hattem.Api.Errors;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Cast
{
    [CategoryTrait("Cast")]
    public sealed partial class CastTests
    {
        private class BaseClass
        {
        }

        private sealed class DerivedAClass : BaseClass
        {
        }

        private sealed class DerivedBClass : BaseClass
        {
        }

        [Fact(DisplayName = "(Sync) Should cast successful response data")]
        public void Sync_IsOk_CastPossible_Cast()
        {
            var response = ApiResponse
                .Ok(new DerivedAClass())
                .Cast(To<BaseClass>.Type);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.NotNull(response.Data);
        }

        [Fact(DisplayName = "(Sync) Shouldn't cast error response data")]
        public void Sync_HasErrors_CastPossible_DoNotCast()
        {
            var response = ApiResponse
                .Error<DerivedAClass>(TestError.Default)
                .Cast(To<BaseClass>.Type);

            Assert.False(response.IsOk);
            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Sync) Should return error if cast is impossible")]
        public void Sync_IsOk_CastImpossible_ReturnsError()
        {
            var response = ApiResponse
                .Ok(new DerivedAClass())
                .Cast(To<DerivedBClass>.Type);

            Assert.False(response.IsOk);
            Assert.True(response.HasErrors);
            Assert.IsType<InvalidCastError<DerivedAClass, DerivedBClass>>(response.Error);
        }

        [Fact(DisplayName = "(Sync) Should return original error if cast is impossible and response has errors")]
        public void Sync_HasErrors_CastImpossible_ReturnsResponseError()
        {
            var response = ApiResponse
                .Error<DerivedAClass>(TestError.Default)
                .Cast(To<DerivedBClass>.Type);

            Assert.False(response.IsOk);
            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }
    }
}
