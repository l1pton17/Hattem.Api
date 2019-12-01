using System;
using System.Collections.Generic;
using System.Linq;

namespace Hattem.Api.Extensions
{
    public readonly struct TypeErrorPredicate : IErrorPredicate
    {
        private readonly Func<Error, bool> _condition;

        private readonly Type _errorType1;
        private readonly Type _errorType2;
        private readonly Type _errorType3;
        private readonly IEnumerable<Type> _errorTypes;

        internal TypeErrorPredicate(
            Type errorType1,
            Type errorType2,
            Type errorType3,
            IEnumerable<Type> errorTypes,
            Func<Error, bool> condition
        )
        {
            _errorType1 = errorType1;
            _errorType2 = errorType2;
            _errorType3 = errorType3;
            _errorTypes = errorTypes;
            _condition = condition;
        }

        public bool IsMatch(Error error)
        {
            if (error == null)
            {
                return false;
            }

            var errorType = error.GetType();

            var isValid =  errorType == _errorType1
             || errorType == _errorType2
             || errorType == _errorType3
             || (_errorTypes?.Contains(errorType) ?? false);

            return isValid && (_condition?.Invoke(error) ?? true);
        }

        public TypeErrorPredicate WithCondition(Func<Error, bool> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return new TypeErrorPredicate(
                _errorType1,
                _errorType2,
                _errorType3,
                _errorTypes,
                condition);
        }
    }
}