using System.Collections.Generic;
using Hattem.Api.Fluent.ErrorPredicates;
using Hattem.Api.Tests.Framework;
using Hattem.Api.Tests.Framework.Errors;
using Xunit;

namespace Hattem.Api.Tests.Fluent.ErrorPredicates
{
    [CategoryTrait(nameof(AnyErrorPredicate) + " tests")]
    public sealed class AnyErrorPredicateTests
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static IEnumerable<object[]> Errors {
            get
            {
                yield return new object[] {TestError.Default};
                yield return new object[] {AnotherTestError.Default};
                yield return new object[] {AnotherTestError2.Default};
                yield return new object[] {AnotherTestError3.Default};
            }
        }

        [Theory(DisplayName = "Should match always")]
        [MemberData(nameof(Errors))]
        public void MatchAlways(Error error)
        {
            var isMatch = AnyErrorPredicate
                .Default
                .IsMatch(error);

            Assert.True(isMatch);
        }
    }
}
