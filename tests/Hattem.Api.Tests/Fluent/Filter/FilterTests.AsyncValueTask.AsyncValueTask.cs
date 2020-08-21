using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Filter
{
    partial class FilterTests
    {
        [Fact(DisplayName = "(Async ValueTask, Async ValueTask predicate) Should return response data if response and predicate are ok")]
        public async Task AsyncValueTask_AsyncValueTaskPredicate_ResponseIsOk_PredicateIsOk_ReturnsData()
        {
            const int expected = 2;

            var response = await ApiResponse
                .Ok(expected)
                .AsValueTask()
                .Filter(_ => ApiResponse.OkAsyncValueTask());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Async ValueTask, Async ValueTask predicate) Should return predicate error if response is ok and predicate has errors")]
        public async Task AsyncValueTask_AsyncValueTaskPredicate_ResponseIsOk_PredicateHasErrors_ReturnError()
        {
            var response = await ApiResponse
                .Ok(2)
                .AsValueTask()
                .Filter(_ => ApiResponse.Error(TestError.Default).AsValueTask());

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Async ValueTask, Async ValueTask predicate) Should return response error if response has error and predicate are ok")]
        public async Task AsyncValueTask_AsyncValueTaskPredicate_ResponseHasErrors_PredicateIsOk_ReturnsError()
        {
            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .AsValueTask()
                .Filter(_ => ApiResponse.OkAsyncValueTask());

            Assert.True(response.HasErrors);
            Assert.Equal(AnotherTestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Async ValueTask, Async ValueTask predicate) Should return response error if response and predicate both have errors")]
        public async Task AsyncValueTask_AsyncValueTaskPredicate_ResponseHasErrors_PredicateHasErrors_ReturnError()
        {
            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .AsValueTask()
                .Filter(_ => ApiResponse.Error(TestError.Default).AsValueTask());

            Assert.True(response.HasErrors);
            Assert.Equal(AnotherTestError.Default, response.Error, ErrorComparer.Default);
        }
    }
}
