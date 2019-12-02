using System;
using Hattem.Api.Fluent.ErrorPredicates;

namespace Hattem.Api.Fluent
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
            IErrorPredicate errorPredicate,
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
        /// <typeparam name="TError"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Predicate for error to execute <paramref name="ifError"/></param>
        /// <param name="ifError">Flow to execute in case of error</param>
        /// <returns></returns>
        public static ApiResponse<T> IfError<T, TError>(
            this ApiResponse<T> source,
            ExactTypeErrorPredicate<TError> errorPredicate,
            Func<TError, ApiResponse<T>> ifError)
            where TError : Error
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return ifError((TError) source.Error);
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

        private static ApiResponse<T> IfErrorRef<T>(
            this ApiResponse<T> source,
            ref CodeErrorPredicate errorPredicate,
            Func<Error, ApiResponse<T>> ifError)
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return ifError(source.Error);
            }

            return source;
        }

        private static ApiResponse<T> IfErrorRef<T, TError>(
            this ApiResponse<T> source,
            ref ExactTypeErrorPredicate<TError> errorPredicate,
            Func<TError, ApiResponse<T>> ifError)
            where TError : Error
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return ifError((TError) source.Error);
            }

            return source;
        }

        private static ApiResponse<T> IfErrorRef<T>(
            this ApiResponse<T> source,
            ref TypeErrorPredicate errorPredicate,
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
