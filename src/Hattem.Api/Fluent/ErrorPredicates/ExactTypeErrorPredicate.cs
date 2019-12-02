using System;

namespace Hattem.Api.Fluent.ErrorPredicates
{
    public readonly struct ExactTypeErrorPredicate<TError> : IErrorPredicate
        where TError : Error
    {
        private readonly Func<TError, bool> _condition;

        private ExactTypeErrorPredicate(Func<TError, bool> condition)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        public bool IsMatch(Error error)
        {
            return error is TError typedError
             && (_condition?.Invoke(typedError) ?? true);
        }

        public ExactTypeErrorPredicate<TError> WithCondition(Func<TError, bool> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return new ExactTypeErrorPredicate<TError>(condition);
        }
    }
}
