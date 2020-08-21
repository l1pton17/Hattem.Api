using System;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Execute <paramref name="onSuccess"/> if response is ok
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="onSuccess">Action to be executed</param>
        /// <returns></returns>
        public static ApiResponse<T> OnSuccess<T>(
            this ApiResponse<T> source,
            Action<T> onSuccess
        )
        {
            if (source.IsOk)
            {
                onSuccess(source.Data);
            }

            return source;
        }

        [Obsolete("Use Then instead", error: true)]
        public static ApiResponse<T> OnSuccess<T, TOnSuccess>(
            this ApiResponse<T> source,
            Func<T, ApiResponse<TOnSuccess>> onSuccess
        )
        {
            throw new NotImplementedException();
        }
    }
}
