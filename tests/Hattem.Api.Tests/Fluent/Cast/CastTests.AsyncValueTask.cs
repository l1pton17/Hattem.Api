using System.Threading.Tasks;
using Hattem.Api.Errors;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Cast
{
    partial class CastTests
    {
        [Fact(DisplayName = "(Async ValueTask) Should cast successful response data")]
        public async Task AsyncValueTask_IsOk_CastPossible_Cast()
        {
            var response = await ApiResponse
                .Ok(new DerivedAClass())
                .AsValueTask()
                .Cast(To<BaseClass>.Type);

            Assert.True(response.IsOk);
            Assert.False(response.HasErrors);
            Assert.NotNull(response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask) Shouldn't cast error response data")]
        public async Task AsyncValueTask_HasErrors_CastPossible_DoNotCast()
        {
            var response = await ApiResponse
                .Error<DerivedAClass>(TestError.Default)
                .AsValueTask()
                .Cast(To<BaseClass>.Type);

            Assert.False(response.IsOk);
            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Async ValueTask) Should return error if cast is impossible")]
        public async Task AsyncValueTask_IsOk_CastImpossible_ReturnsError()
        {
            var response = await ApiResponse
                .Ok(new DerivedAClass())
                .AsValueTask()
                .Cast(To<DerivedBClass>.Type);

            Assert.False(response.IsOk);
            Assert.True(response.HasErrors);
            Assert.IsType<InvalidCastError<DerivedAClass, DerivedBClass>>(response.Error);
        }

        [Fact(DisplayName = "(Async ValueTask) Should return original error if cast is impossible and response has errors")]
        public async Task AsyncValueTask_HasErrors_CastImpossible_ReturnsResponseError()
        {
            var response = await ApiResponse
                .Error<DerivedAClass>(TestError.Default)
                .AsValueTask()
                .Cast(To<DerivedBClass>.Type);

            Assert.False(response.IsOk);
            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }
    }
}
