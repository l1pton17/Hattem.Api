using System;
using System.Threading.Tasks;
using Hattem.Api.Fluent.ErrorPredicates;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Execute <paramref name="onError"/> if response has errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Error predicate</param>
        /// <param name="onError"></param>
        /// <returns></returns>
        public static async ValueTask<ApiResponse<T>> OnError<T>(
            this ApiResponse<T> source,
            IErrorPredicate errorPredicate,
            Func<Error, ValueTask> onError
        )
        {
            if (errorPredicate == null)
            {
                throw new ArgumentNullException(nameof(errorPredicate));
            }

            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                await onError(source.Error).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// Execute <paramref name="onError"/> if response has errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Error predicate</param>
        /// <param name="onError"></param>
        /// <returns></returns>
        public static async ValueTask<ApiResponse<T>> OnError<T>(
            this ApiResponse<T> source,
            CodeErrorPredicate errorPredicate,
            Func<Error, ValueTask> onError
        )
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                await onError(source.Error).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// Execute <paramref name="onError"/> if response has errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Error predicate</param>
        /// <param name="onError"></param>
        /// <returns></returns>
        public static async ValueTask<ApiResponse<T>> OnError<T, TError>(
            this ApiResponse<T> source,
            ExactTypeErrorPredicate<TError> errorPredicate,
            Func<TError, ValueTask> onError
        )
            where TError : Error
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                await onError((TError) source.Error).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// Execute <paramref name="onError"/> if response has errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Error predicate</param>
        /// <param name="onError"></param>
        /// <returns></returns>
        public static async ValueTask<ApiResponse<T>> OnError<T>(
            this ApiResponse<T> source,
            TypeErrorPredicate errorPredicate,
            Func<Error, ValueTask> onError
        )
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                await onError(source.Error).ConfigureAwait(false);
            }

            return source;
        }

        /// <summary>
        /// Execute <paramref name="onError"/> if response has errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOnError"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Error predicate</param>
        /// <param name="onError"></param>
        /// <returns></returns>
        [Obsolete("Use " + nameof(IfError) + " instead", error: true)]
        public static ApiResponse<T> OnError<T, TOnError>(
            this ApiResponse<T> source,
            IErrorPredicate errorPredicate,
            Func<Error, ValueTask<ApiResponse<TOnError>>> onError
        )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute <paramref name="onError"/> if response has errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOnError"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Error predicate</param>
        /// <param name="onError"></param>
        /// <returns></returns>
        [Obsolete("Use " + nameof(IfError) + " instead", error: true)]
        public static ApiResponse<T> OnError<T, TOnError>(
            this ApiResponse<T> source,
            CodeErrorPredicate errorPredicate,
            Func<Error, ValueTask<ApiResponse<TOnError>>> onError
        )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute <paramref name="onError"/> if response has errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <typeparam name="TOnError"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Error predicate</param>
        /// <param name="onError"></param>
        /// <returns></returns>
        [Obsolete("Use " + nameof(IfError) + " instead", error: true)]
        public static ApiResponse<T> OnError<T, TError, TOnError>(
            this ApiResponse<T> source,
            ExactTypeErrorPredicate<TError> errorPredicate,
            Func<TError, ValueTask<ApiResponse<TOnError>>> onError
        )
            where TError : Error
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute <paramref name="onError"/> if response has errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOnError"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Error predicate</param>
        /// <param name="onError"></param>
        /// <returns></returns>
        [Obsolete("Use " + nameof(IfError) + " instead", error: true)]
        public static ApiResponse<T> OnError<T, TOnError>(
            this ApiResponse<T> source,
            TypeErrorPredicate errorPredicate,
            Func<Error, ValueTask<ApiResponse<TOnError>>> onError
        )
        {
            throw new NotImplementedException();
        }
    }
}
