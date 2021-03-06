﻿using System;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Union results of two responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static ApiResponse<(T1, T2)> Union<T1, T2>(
            this ApiResponse<T1> source,
            Func<T1, ApiResponse<T2>> selector
        )
        {
            if (source.Error is not null)
            {
                return ApiResponse
                    .Error<(T1, T2)>(source.Error)
                    .WithStatusCode(source.StatusCode);
            }

            var output = selector(source.Data!);

            if (output.Error is not null)
            {
                return ApiResponse
                    .Error<(T1, T2)>(output.Error)
                    .WithStatusCode(output.StatusCode);
            }

            return ApiResponse
                .Ok((source.Data!, output.Data!))
                .WithStatusCode(source.StatusCode ?? output.StatusCode);
        }

        /// <summary>
        /// Add response to current assembly of responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static ApiResponse<(T1, T2, T3)> Union<T1, T2, T3>(
            this ApiResponse<(T1, T2)> source,
            Func<ApiResponse<T3>> selector
        )
        {
            return source.Union((_, _) => selector());
        }

        /// <summary>
        /// Add response to current assembly of responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static ApiResponse<(T1, T2, T3)> Union<T1, T2, T3>(
            this ApiResponse<(T1, T2)> source,
            Func<T1, T2, ApiResponse<T3>> selector
        )
        {
            if (source.Error is not null)
            {
                return ApiResponse
                    .Error<(T1, T2, T3)>(source.Error)
                    .WithStatusCode(source.StatusCode);
            }

            var output = selector(source.Data.Item1, source.Data.Item2);

            if (output.Error is not null)
            {
                return ApiResponse
                    .Error<(T1, T2, T3)>(output.Error)
                    .WithStatusCode(output.StatusCode);
            }

            return ApiResponse
                .Ok((source.Data.Item1!, source.Data.Item2!, output.Data!))
                .WithStatusCode(source.StatusCode ?? output.StatusCode);
        }

        /// <summary>
        /// Add response to current assembly of responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static ApiResponse<(T1, T2, T3, T4)> Union<T1, T2, T3, T4>(
            this ApiResponse<(T1, T2, T3)> source,
            Func<ApiResponse<T4>> selector
        )
        {
            return source.Union((_, _, _) => selector());
        }

        /// <summary>
        /// Add response to current assembly of responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static ApiResponse<(T1, T2, T3, T4)> Union<T1, T2, T3, T4>(
            this ApiResponse<(T1, T2, T3)> source,
            Func<T1, T2, T3, ApiResponse<T4>> selector
        )
        {
            if (source.Error is not null)
            {
                return ApiResponse
                    .Error<(T1, T2, T3, T4)>(source.Error)
                    .WithStatusCode(source.StatusCode);
            }

            var output = selector(
                source.Data.Item1,
                source.Data.Item2,
                source.Data.Item3);

            if (output.Error is not null)
            {
                return ApiResponse
                    .Error<(T1, T2, T3, T4)>(output.Error)
                    .WithStatusCode(output.StatusCode);
            }

            return ApiResponse
                .Ok((source.Data.Item1!, source.Data.Item2!, source.Data.Item3!, output.Data!))
                .WithStatusCode(source.StatusCode ?? output.StatusCode);
        }

        /// <summary>
        /// Add response to current assembly of responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static ApiResponse<(T1, T2, T3, T4, T5)> Union<T1, T2, T3, T4, T5>(
            this ApiResponse<(T1, T2, T3, T4)> source,
            Func<ApiResponse<T5>> selector
        )
        {
            return source.Union(
                (
                    _,
                    _,
                    _,
                    _
                ) => selector());
        }

        /// <summary>
        /// Add response to current assembly of responses
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static ApiResponse<(T1, T2, T3, T4, T5)> Union<T1, T2, T3, T4, T5>(
            this ApiResponse<(T1, T2, T3, T4)> source,
            Func<T1, T2, T3, T4, ApiResponse<T5>> selector
        )
        {
            if (source.Error is not null)
            {
                return ApiResponse
                    .Error<(T1, T2, T3, T4, T5)>(source.Error)
                    .WithStatusCode(source.StatusCode);
            }

            var output = selector(
                source.Data.Item1,
                source.Data.Item2,
                source.Data.Item3,
                source.Data.Item4);

            if (output.Error is not null)
            {
                return ApiResponse
                    .Error<(T1, T2, T3, T4, T5)>(output.Error)
                    .WithStatusCode(output.StatusCode);
            }

            return ApiResponse
                .Ok((source.Data.Item1!, source.Data.Item2!, source.Data.Item3!, source.Data.Item4!, output.Data!))
                .WithStatusCode(source.StatusCode ?? output.StatusCode);
        }
    }
}
