using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Comparers;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.Filter
{
    [CategoryTrait("Filter")]
    public sealed partial class FilterTests
    {
        [Fact(DisplayName = "(Sync, Sync predicate) Should return response data if response and predicate are ok")]
        public void Sync_SyncPredicate_ResponseIsOk_PredicateIsOk_ReturnsData()
        {
            const int expected = 2;

            var response = ApiResponse
                .Ok(expected)
                .Filter(_ => ApiResponse.Ok());

            Assert.True(response.IsOk);
            Assert.Equal(expected, response.Data);
        }

        [Fact(DisplayName = "(Sync, Sync predicate) Should return predicate error if response is ok and predicate has errors")]
        public void Sync_SyncPredicate_ResponseIsOk_PredicateHasErrors_ReturnError()
        {
            var response = ApiResponse
                .Ok(2)
                .Filter(_ => ApiResponse.Error(TestError.Default));

            Assert.True(response.HasErrors);
            Assert.Equal(TestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Sync, Sync predicate) Should return response error if response has error and predicate are ok")]
        public void Sync_SyncPredicate_ResponseHasErrors_PredicateIsOk_ReturnsError()
        {
            var response = ApiResponse
                .Error<int>(AnotherTestError.Default)
                .Filter(_ => ApiResponse.Ok());

            Assert.True(response.HasErrors);
            Assert.Equal(AnotherTestError.Default, response.Error, ErrorComparer.Default);
        }

        [Fact(DisplayName = "(Sync, Sync predicate) Should return response error if response and predicate both have errors")]
        public void Sync_SyncPredicate_ResponseHasErrors_PredicateHasErrors_ReturnError()
        {
            var response = ApiResponse
                .Error<int>(AnotherTestError.Default)
                .Filter(_ => ApiResponse.Error(TestError.Default));

            Assert.True(response.HasErrors);
            Assert.Equal(AnotherTestError.Default, response.Error, ErrorComparer.Default);
        }
    }
}
