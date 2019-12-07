namespace Hattem.Api.Fluent.ErrorPredicates
{
    internal sealed class AnyErrorPredicate : IErrorPredicate
    {
        public static readonly AnyErrorPredicate Default = new AnyErrorPredicate();

        private AnyErrorPredicate()
        {
        }

        public bool IsMatch(Error error)
        {
            return true;
        }
    }
}
