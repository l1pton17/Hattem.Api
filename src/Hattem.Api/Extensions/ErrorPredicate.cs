using System;

namespace Hattem.Api.Extensions
{
    public static class ErrorPredicate
    {
        public static CodeErrorPredicate ByCode(
            string errorCode1,
            string errorCode2 = null,
            string errorCode3 = null
        )
        {
            return new CodeErrorPredicate(
                errorCode1,
                errorCode2,
                errorCode3,
                null);
        }

        public static CodeErrorPredicate ByCode(params string[] errorCodes)
        {
            switch (errorCodes.Length)
            {
                case 0:
                    throw new ArgumentException("Should consist of some error codes", nameof(errorCodes));

                case 1:
                    return ByCode(errorCodes[0]);

                case 2:
                    return ByCode(errorCodes[0], errorCodes[1]);

                case 3:
                    return ByCode(errorCodes[0], errorCodes[1], errorCodes[2]);

                default:
                    return new CodeErrorPredicate(
                        null,
                        null,
                        null,
                        errorCodes);
            }
        }

        public static TypeErrorPredicate ByType<T1>()
        {
            return ByType(typeof(T1));
        }

        public static TypeErrorPredicate ByType<T1, T2>()
        {
            return ByType(typeof(T1), typeof(T2));
        }

        public static TypeErrorPredicate ByType<T1, T2, T3>()
        {
            return ByType(typeof(T1), typeof(T2), typeof(T3));
        }

        public static TypeErrorPredicate ByType(
            Type errorType1,
            Type errorType2 = null,
            Type errorType3 = null)
        {
            return new TypeErrorPredicate(
                errorType1,
                errorType2,
                errorType3,
                null);
        }

        public static TypeErrorPredicate ByType(params Type[] errorTypes)
        {
            switch (errorTypes.Length)
            {
                case 0:
                    throw new ArgumentException("Should consist of some error types", nameof(errorTypes));

                case 1:
                    return ByType(errorTypes[0]);

                case 2:
                    return ByType(errorTypes[0], errorTypes[1]);

                case 3:
                    return ByType(errorTypes[0], errorTypes[1], errorTypes[2]);

                default:
                    return new TypeErrorPredicate(
                        null,
                        null,
                        null,
                        errorTypes);
            }
        }
    }
}
