﻿using System;
using System.Threading.Tasks;

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
        public static async ValueTask<ApiResponse<T>> OnSuccess<T>(
            this ApiResponse<T> source,
            Func<T, ValueTask> onSuccess
        )
        {
            if (source.Data is not null)
            {
                await onSuccess(source.Data).ConfigureAwait(false);
            }

            return source;
        }

        [Obsolete("Use Then instead", error: true)]
        public static ValueTask<ApiResponse<T>> OnSuccess<T, TOnSuccess>(
            this ApiResponse<T> source,
            Func<T, ValueTask<ApiResponse<TOnSuccess>>> onSuccess
        )
        {
            throw new NotImplementedException();
        }
    }
}
