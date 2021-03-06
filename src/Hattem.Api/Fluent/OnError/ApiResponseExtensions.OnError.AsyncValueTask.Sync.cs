﻿using System;
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
            this ValueTask<ApiResponse<T>> source,
            IErrorPredicate errorPredicate,
            Action<Error> onError
        )
        {
            if (errorPredicate == null)
            {
                throw new ArgumentNullException(nameof(errorPredicate));
            }

            var response = await source.ConfigureAwait(false);

            return response.OnError(errorPredicate, onError);
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
            this ValueTask<ApiResponse<T>> source,
            CodeErrorPredicate errorPredicate,
            Action<Error> onError
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.OnErrorRef(ref errorPredicate, onError);
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
            this ValueTask<ApiResponse<T>> source,
            ExactTypeErrorPredicate<TError> errorPredicate,
            Action<TError> onError
        )
            where TError : Error
        {
            var response = await source.ConfigureAwait(false);

            return response.OnErrorRef(ref errorPredicate, onError);
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
            this ValueTask<ApiResponse<T>> source,
            TypeErrorPredicate errorPredicate,
            Action<Error> onError
        )
        {
            var response = await source.ConfigureAwait(false);

            return response.OnErrorRef(ref errorPredicate, onError);
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
        public static ValueTask<ApiResponse<T>> OnError<T, TOnError>(
            this ValueTask<ApiResponse<T>> source,
            IErrorPredicate errorPredicate,
            Func<Error, ApiResponse<TOnError>> onError
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
        public static ValueTask<ApiResponse<T>> OnError<T, TOnError>(
            this ValueTask<ApiResponse<T>> source,
            CodeErrorPredicate errorPredicate,
            Func<Error, ApiResponse<TOnError>> onError
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
        public static ValueTask<ApiResponse<T>> OnError<T, TError, TOnError>(
            this ValueTask<ApiResponse<T>> source,
            ExactTypeErrorPredicate<TError> errorPredicate,
            Func<TError, ApiResponse<TOnError>> onError
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
        public static ValueTask<ApiResponse<T>> OnError<T, TOnError>(
            this ValueTask<ApiResponse<T>> source,
            TypeErrorPredicate errorPredicate,
            Func<Error, ApiResponse<TOnError>> onError
        )
        {
            throw new NotImplementedException();
        }
    }
}
