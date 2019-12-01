using System;
using System.Collections.Generic;
using System.Linq;

namespace Hattem.Api.Extensions
{
    public readonly struct CodeErrorPredicate
    {
        private readonly string _errorCode1;
        private readonly string _errorCode2;
        private readonly string _errorCode3;
        private readonly IEnumerable<string> _errorCodes;

        internal CodeErrorPredicate(
            string errorCode1,
            string errorCode2,
            string errorCode3,
            IEnumerable<string> errorCodes
        )
        {
            _errorCode1 = errorCode1;
            _errorCode2 = errorCode2;
            _errorCode3 = errorCode3;
            _errorCodes = errorCodes;
        }

        public bool IsMatch(Error error)
        {
            if (error == null)
            {
                return false;
            }

            return error.Code == _errorCode1
             || error.Code == _errorCode2
             || error.Code == _errorCode3
             || (_errorCodes?.Contains(error.Code) ?? false);
        }
    }
}