using Hattem.Api.Fluent;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.ErrorPredicates
{
    [CategoryTrait("ExactTypeErrorPredicate")]
    public sealed class ExactTypeErrorPredicateTests
    {
        [Fact(DisplayName = "Should match")]
        public void Match()
        {
            var predicate = ErrorPredicate.ByType<AnotherTestError>();

            var isMatch = predicate.IsMatch(AnotherTestError.Default);

            Assert.True(isMatch);
        }

        [Fact(DisplayName = "Shouldn't match")]
        public void DoNotMatch()
        {
            var predicate = ErrorPredicate.ByType<AnotherTestError>();

            var isMatch = predicate.IsMatch(TestError.Default);

            Assert.False(isMatch);
        }

        [Fact(DisplayName = "Should match with condition")]
        public void Match_WithCondition()
        {
            var predicate = ErrorPredicate
                .ByType<AnotherTestError>()
                .WithCondition(_ => true);

            var isMatch = predicate.IsMatch(AnotherTestError.Default);

            Assert.True(isMatch);
        }

        [Fact(DisplayName = "Shouldn't match with condition")]
        public void DoNotMatch_WithCondition()
        {
            var predicate = ErrorPredicate
                .ByType<AnotherTestError>()
                .WithCondition(_ => false);

            var isMatch = predicate.IsMatch(AnotherTestError.Default);

            Assert.False(isMatch);
        }
    }
}