using System;
using Hattem.Api.Fluent.ErrorPredicates;

namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Return ok response and suppress errors that match <paramref name="errorPredicate"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Predicate for errors</param>
        /// <returns></returns>
        public static ApiResponse<Unit> SuppressErrors<T>(
            this ApiResponse<T> source,
            IErrorPredicate errorPredicate)
        {
            if (errorPredicate == null)
            {
                throw new ArgumentNullException(nameof(errorPredicate));
            }

            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return ApiResponse.Ok();
            }

            return source.AsUnit();
        }

        /// <summary>
        /// Return ok response and suppress errors that match <paramref name="errorPredicate"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Predicate for errors</param>
        /// <returns></returns>
        public static ApiResponse<Unit> SuppressErrors<T>(
            this ApiResponse<T> source,
            CodeErrorPredicate errorPredicate)
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return ApiResponse.Ok();
            }

            return source.AsUnit();
        }

        /// <summary>
        /// Return ok response and suppress errors that match <paramref name="errorPredicate"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Predicate for errors</param>
        /// <returns></returns>
        public static ApiResponse<Unit> SuppressErrors<T>(
            this ApiResponse<T> source,
            TypeErrorPredicate errorPredicate)
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return ApiResponse.Ok();
            }

            return source.AsUnit();
        }

        /// <summary>
        /// Return ok response and suppress errors that match <paramref name="errorPredicate"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Predicate for errors</param>
        /// <returns></returns>
        public static ApiResponse<Unit> SuppressErrors<T, TError>(
            this ApiResponse<T> source,
            ExactTypeErrorPredicate<TError> errorPredicate)
            where TError : Error
        {
            if (source.HasErrors && errorPredicate.IsMatch(source.Error))
            {
                return ApiResponse.Ok();
            }

            return source.AsUnit();
        }
    }
}
