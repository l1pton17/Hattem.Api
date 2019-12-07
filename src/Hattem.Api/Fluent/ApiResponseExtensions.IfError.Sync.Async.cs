using System;
using System.Threading.Tasks;
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
        public static async Task<ApiResponse<T>> IfError<T>(
            this ApiResponse<T> source,
            IErrorPredicate errorPredicate,
            Func<Error, Task<ApiResponse<T>>> ifError)
        {
            if (errorPredicate == null)
            {
                throw new ArgumentNullException(nameof(errorPredicate));
            }

            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return await ifError(source.Error).ConfigureAwait(false);
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
        public static async Task<ApiResponse<T>> IfError<T>(
            this ApiResponse<T> source,
            CodeErrorPredicate errorPredicate,
            Func<Error, Task<ApiResponse<T>>> ifError)
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return await ifError(source.Error).ConfigureAwait(false);
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
        public static async Task<ApiResponse<T>> IfError<T, TError>(
            this ApiResponse<T> source,
            ExactTypeErrorPredicate<TError> errorPredicate,
            Func<TError, Task<ApiResponse<T>>> ifError)
            where TError : Error
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return await ifError((TError) source.Error).ConfigureAwait(false);
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
        public static async Task<ApiResponse<T>> IfError<T>(
            this ApiResponse<T> source,
            TypeErrorPredicate errorPredicate,
            Func<Error, Task<ApiResponse<T>>> ifError)
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return await ifError(source.Error).ConfigureAwait(false);
            }

            return source;
        }
    }
}
