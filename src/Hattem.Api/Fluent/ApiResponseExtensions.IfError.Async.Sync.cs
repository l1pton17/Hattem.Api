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
            this Task<ApiResponse<T>> source,
            IErrorPredicate errorPredicate,
            Func<Error, ApiResponse<T>> ifError)
        {
            if (errorPredicate == null)
            {
                throw new ArgumentNullException(nameof(errorPredicate));
            }

            var response = await source.ConfigureAwait(false);

            return response.IfError(errorPredicate, ifError);
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
            this Task<ApiResponse<T>> source,
            CodeErrorPredicate errorPredicate,
            Func<Error, ApiResponse<T>> ifError)
        {
            var response = await source.ConfigureAwait(false);

            return response.IfErrorRef(ref errorPredicate, ifError);
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
            this Task<ApiResponse<T>> source,
            ExactTypeErrorPredicate<TError> errorPredicate,
            Func<TError, ApiResponse<T>> ifError)
            where TError : Error
        {
            var response = await source.ConfigureAwait(false);

            return response.IfErrorRef(ref errorPredicate, ifError);
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
            this Task<ApiResponse<T>> source,
            TypeErrorPredicate errorPredicate,
            Func<Error, ApiResponse<T>> ifError)
        {
            var response = await source.ConfigureAwait(false);

            return response.IfErrorRef(ref errorPredicate, ifError);
        }
    }
}
