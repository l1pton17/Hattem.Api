using System;
using System.Threading.Tasks;
using Hattem.Api.Fluent.ErrorPredicates;

// ReSharper disable once CheckNamespace
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
        public static async ValueTask<ApiResponse<Unit>> SuppressErrors<T>(
            this ValueTask<ApiResponse<T>> source,
            IErrorPredicate errorPredicate)
        {
            if (errorPredicate == null)
            {
                throw new ArgumentNullException(nameof(errorPredicate));
            }

            var response = await source.ConfigureAwait(false);

            return response.SuppressErrors(errorPredicate);
        }

        /// <summary>
        /// Return ok response and suppress errors that match <paramref name="errorPredicate"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Predicate for errors</param>
        /// <returns></returns>
        public static async ValueTask<ApiResponse<Unit>> SuppressErrors<T>(
            this ValueTask<ApiResponse<T>> source,
            CodeErrorPredicate errorPredicate)
        {
            var response = await source.ConfigureAwait(false);

            if (response.HasErrors && errorPredicate.IsMatch(response.Error))
            {
                return ApiResponse.Ok();
            }

            return response.AsUnit();
        }

        /// <summary>
        /// Return ok response and suppress errors that match <paramref name="errorPredicate"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Predicate for errors</param>
        /// <returns></returns>
        public static async ValueTask<ApiResponse<Unit>> SuppressErrors<T>(
            this ValueTask<ApiResponse<T>> source,
            TypeErrorPredicate errorPredicate)
        {
            var response = await source.ConfigureAwait(false);

            if (response.Error is not null && errorPredicate.IsMatch(response.Error))
            {
                return ApiResponse.Ok();
            }

            return response.AsUnit();
        }

        /// <summary>
        /// Return ok response and suppress errors that match <paramref name="errorPredicate"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="source"></param>
        /// <param name="errorPredicate">Predicate for errors</param>
        /// <returns></returns>
        public static async ValueTask<ApiResponse<Unit>> SuppressErrors<T, TError>(
            this ValueTask<ApiResponse<T>> source,
            ExactTypeErrorPredicate<TError> errorPredicate)
            where TError : Error
        {
            var response = await source.ConfigureAwait(false);

            if (response.Error is not null && errorPredicate.IsMatch(response.Error))
            {
                return ApiResponse.Ok();
            }

            return response.AsUnit();
        }
    }
}
