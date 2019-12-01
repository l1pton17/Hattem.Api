using System;
using System.Collections.Generic;
using System.Linq;

namespace Hattem.Api.Extensions
{
    public readonly struct CodeErrorPredicate : IErrorPredicate
    {
        private readonly Func<Error, bool> _condition;

        private readonly string _errorCode1;
        private readonly string _errorCode2;
        private readonly string _errorCode3;
        private readonly IEnumerable<string> _errorCodes;

        internal CodeErrorPredicate(
            string errorCode1,
            string errorCode2,
            string errorCode3,
            IEnumerable<string> errorCodes,
            Func<Error, bool> condition
        )
        {
            _errorCode1 = errorCode1;
            _errorCode2 = errorCode2;
            _errorCode3 = errorCode3;
            _errorCodes = errorCodes;
            _condition = condition;
        }

        public bool IsMatch(Error error)
        {
            if (error == null)
            {
                return false;
            }

            var isValid = error.Code == _errorCode1
             || error.Code == _errorCode2
             || error.Code == _errorCode3
             || (_errorCodes?.Contains(error.Code) ?? false);

            return isValid && (_condition?.Invoke(error) ?? true);
        }

        public CodeErrorPredicate WithCondition(Func<Error, bool> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return new CodeErrorPredicate(
                _errorCode1,
                _errorCode2,
                _errorCode3,
                _errorCodes,
                condition);
        }
    }
}