using System;

namespace Hattem.Api.Extensions
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Switch to <paramref name="ifError"/> flow if response returns error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Predicate for error to execute <paramref name="ifError"/></param>
        /// <param name="ifError">Flow to execute in case of error</param>
        /// <returns></returns>
        public static ApiResponse<T> IfError<T>(
            this ApiResponse<T> source,
            CodeErrorPredicate errorPredicate,
            Func<Error, ApiResponse<T>> ifError)
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return ifError(source.Error);
            }

            return source;
        }

        /// <summary>
        /// Switch to <paramref name="ifError"/> flow if response returns error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Predicate for error to execute <paramref name="ifError"/></param>
        /// <param name="ifError">Flow to execute in case of error</param>
        /// <returns></returns>
        public static ApiResponse<T> IfError<T>(
            this ApiResponse<T> source,
            TypeErrorPredicate errorPredicate,
            Func<Error, ApiResponse<T>> ifError)
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return ifError(source.Error);
            }

            return source;
        }
    }
}
