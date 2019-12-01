using System;
using System.Collections.Generic;
using Hattem.Api.Extensions;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests
{
    [CategoryTrait(nameof(TypeErrorPredicate))]
    public sealed class TypeErrorPredicateTests
    {
        public static IEnumerable<object[]> ErrorTypeCombinations
        {
            get
            {
                yield return new object[] { typeof(AnotherTestError) };
                yield return new object[] { typeof(AnotherTestError), typeof(AnotherTestError2) };
                yield return new object[] { typeof(AnotherTestError), typeof(AnotherTestError2), typeof(AnotherTestError3) };
                yield return new object[] { typeof(AnotherTestError), typeof(AnotherTestError2), typeof(AnotherTestError3), typeof(AnotherTestError4) };
                yield return new object[] { typeof(AnotherTestError), typeof(AnotherTestError2), typeof(AnotherTestError3), typeof(AnotherTestError4), typeof(AnotherTestError5) };
            }
        }

        [Fact(DisplayName = "Should match by generic type (1 Args)")]
        public void ByGenericType_1Args_Match()
        {
            var predicate = ErrorPredicate.ByType<AnotherTestError>();

            var isMatch = predicate.IsMatch(AnotherTestError.Default);

            Assert.True(isMatch);
        }

        [Fact(DisplayName = "Should match by generic type (2 Args)")]
        public void ByGenericType_2Args_Match()
        {
            var predicate = ErrorPredicate
                .ByType<
                    AnotherTestError,
                    AnotherTestError2
                >();

            var isMatch = predicate.IsMatch(AnotherTestError.Default);

            Assert.True(isMatch);
        }

        [Fact(DisplayName = "Should match by generic type (3 Args)")]
        public void ByGenericType_3Args_Match()
        {
            var predicate = ErrorPredicate
                .ByType<
                    AnotherTestError,
                    AnotherTestError2,
                    AnotherTestError3
                >();

            var isMatch = predicate.IsMatch(AnotherTestError.Default);

            Assert.True(isMatch);
        }

        [Fact(DisplayName = "Should match by types")]
        public void ByType_Match()
        {
            var predicate = ErrorPredicate
                .ByType(
                    typeof(AnotherTestError),
                    typeof(AnotherTestError2)
                );

            var isMatch = predicate.IsMatch(AnotherTestError.Default);

            Assert.True(isMatch);
        }

        [Theory(DisplayName = "Should match")]
        [MemberData(nameof(ErrorTypeCombinations))]
        public void Match(params Type[] errorTypes)
        {
            var predicate = ErrorPredicate.ByType(errorTypes);

            var isMatch = predicate.IsMatch((Error) Activator.CreateInstance(errorTypes[0]));

            Assert.True(isMatch);
        }

        [Theory(DisplayName = "Shouldn't match")]
        [MemberData(nameof(ErrorTypeCombinations))]
        public void DoNotMatch(params Type[] errorTypes)
        {
            var predicate = ErrorPredicate.ByType(errorTypes);

            var isMatch = predicate.IsMatch(TestError.Default);

            Assert.False(isMatch);
        }

        [Theory(DisplayName = "Should match with condition")]
        [MemberData(nameof(ErrorTypeCombinations))]
        public void Match_WithCondition(params Type[] errorTypes)
        {
            var predicate = ErrorPredicate
                .ByType(errorTypes)
                .WithCondition(_ => true);

            var isMatch = predicate.IsMatch((Error) Activator.CreateInstance(errorTypes[0]));

            Assert.True(isMatch);
        }

        [Theory(DisplayName = "Shouldn't match with condition")]
        [MemberData(nameof(ErrorTypeCombinations))]
        public void DoNotMatch_WithCondition(params Type[] errorTypes)
        {
            var predicate = ErrorPredicate
                .ByType(errorTypes)
                .WithCondition(_ => false);

            var isMatch = predicate.IsMatch((Error) Activator.CreateInstance(errorTypes[0]));

            Assert.False(isMatch);
        }
    }
}