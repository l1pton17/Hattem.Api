using System.Collections.Generic;
using Hattem.Api.Extensions;
using Hattem.Api.Tests.Framework;
using Xunit;

namespace Hattem.Api.Tests
{
    [CategoryTrait(nameof(CodeErrorPredicate))]
    public sealed class CodeErrorPredicateTests
    {
        public static IEnumerable<object[]> ErrorCodeCombinations
        {
            get
            {
                yield return new object[] {"test1"};
                yield return new object[] {"test1", "test2"};
                yield return new object[] {"test1", "test2", "test3"};
                yield return new object[] {"test1", "test2", "test3", "test4"};
                yield return new object[] {"test1", "test2", "test3", "test4", "test5"};
            }
        }

        [Theory(DisplayName = "Should match")]
        [MemberData(nameof(ErrorCodeCombinations))]
        public void Match(params string[] errorCodes)
        {
            var predicate = ErrorPredicate.ByCode(errorCodes);

            var isMatch = predicate.IsMatch(new Error(errorCodes[0], "test"));

            Assert.True(isMatch);
        }

        [Theory(DisplayName = "Shouldn't match")]
        [MemberData(nameof(ErrorCodeCombinations))]
        public void DoNotMatch(params string[] errorCodes)
        {
            var predicate = ErrorPredicate.ByCode(errorCodes);

            var isMatch = predicate.IsMatch(new Error("invalid", "test"));

            Assert.False(isMatch);
        }

        [Theory(DisplayName = "Should match with condition")]
        [MemberData(nameof(ErrorCodeCombinations))]
        public void Match_WithCondition(params string[] errorCodes)
        {
            var predicate = ErrorPredicate
                .ByCode(errorCodes)
                .WithCondition(_ => true);

            var isMatch = predicate.IsMatch(new Error(errorCodes[0], "test"));

            Assert.True(isMatch);
        }

        [Theory(DisplayName = "Shouldn't match with condition")]
        [MemberData(nameof(ErrorCodeCombinations))]
        public void DoNotMatch_WithCondition(params string[] errorCodes)
        {
            var predicate = ErrorPredicate
                .ByCode(errorCodes)
                .WithCondition(_ => false);

            var isMatch = predicate.IsMatch(new Error(errorCodes[0], "test"));

            Assert.False(isMatch);
        }
    }
}