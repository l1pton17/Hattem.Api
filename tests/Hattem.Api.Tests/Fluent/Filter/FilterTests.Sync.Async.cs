using System.Threading.Tasks;
using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Filter
{
    partial class FilterTests
    {
        [Fact(DisplayName = "(Sync, Async predicate) Should return response data if response and predicate are ok")]
        public async Task Sync_AsyncPredicate_ResponseIsOk_PredicateIsOk_ReturnsData()
        {
            const int expected = 2;

            var response = await ApiResponse
                .Ok(expected)
                .Filter(_ => ApiResponse.OkAsync());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Sync, Async predicate) Should return predicate error if response is ok and predicate has errors")]
        public async Task Sync_AsyncPredicate_ResponseIsOk_PredicateHasErrors_ReturnError()
        {
            var response = await ApiResponse
                .Ok(2)
                .Filter(_ => ApiResponse.Error(TestError.Default).AsTask());

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Sync, Async predicate) Should return response error if response has error and predicate are ok")]
        public async Task Sync_AsyncPredicate_ResponseHasErrors_PredicateIsOk_ReturnsError()
        {
            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .Filter(_ => ApiResponse.OkAsync());

            Assert.True(response.HasErrors);
            Assert.Equal(AnotherTestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Sync, Async predicate) Should return response error if response and predicate both have errors")]
        public async Task Sync_AsyncPredicate_ResponseHasErrors_PredicateHasErrors_ReturnError()
        {
            var response = await ApiResponse
                .Error<int>(AnotherTestError.Default)
                .Filter(_ => ApiResponse.Error(TestError.Default).AsTask());

            Assert.True(response.HasErrors);
            Assert.Equal(AnotherTestError.Default, response.Error, ErrorComparer.Default);
        }
    }
}
