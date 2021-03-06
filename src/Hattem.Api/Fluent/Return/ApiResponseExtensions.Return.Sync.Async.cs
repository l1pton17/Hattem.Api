﻿using System;
using System.Threading.Tasks;
using Hattem.Api.Extensions;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static async Task<ApiResponse<TOutput>> Return<TInput, TOutput>(
            this ApiResponse<TInput> source,
            Func<TInput, Task<TOutput>> valueFactory
        )
        {
            if (source.Error is not null)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            var value = await valueFactory(source.Data!).ConfigureAwait(false);

            return ApiResponse.Ok(value);
        }

        public static async Task<ApiResponse<TOutput>> Return<T1, T2, TOutput>(
            this ApiResponse<(T1, T2)> source,
            Func<T1, T2, Task<TOutput>> valueFactory
        )
        {
            if (source.Error is not null)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            var value = await valueFactory(
                    source.Data!.Item1,
                    source.Data!.Item2)
                .ConfigureAwait(false);

            return ApiResponse.Ok(value);
        }

        public static async Task<ApiResponse<TOutput>> Return<T1, T2, T3, TOutput>(
            this ApiResponse<(T1, T2, T3)> source,
            Func<T1, T2, T3, Task<TOutput>> valueFactory
        )
        {
            if (source.Error is not null)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            var value = await valueFactory(
                    source.Data.Item1,
                    source.Data.Item2,
                    source.Data.Item3)
                .ConfigureAwait(false);

            return ApiResponse.Ok(value);
        }

        public static async Task<ApiResponse<TOutput>> Return<T1, T2, T3, T4, TOutput>(
            this ApiResponse<(T1, T2, T3, T4)> source,
            Func<T1, T2, T3, T4, Task<TOutput>> valueFactory
        )
        {
            if (source.Error is not null)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            var value = await valueFactory(
                    source.Data.Item1,
                    source.Data.Item2,
                    source.Data.Item3,
                    source.Data.Item4)
                .ConfigureAwait(false);

            return ApiResponse.Ok(value);
        }

        public static async Task<ApiResponse<TOutput>> Return<T1, T2, T3, T4, T5, TOutput>(
            this ApiResponse<(T1, T2, T3, T4, T5)> source,
            Func<T1, T2, T3, T4, T5, Task<TOutput>> valueFactory
        )
        {
            if (source.Error is not null)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            var value = await valueFactory(
                    source.Data.Item1,
                    source.Data.Item2,
                    source.Data.Item3,
                    source.Data.Item4,
                    source.Data.Item5)
                .ConfigureAwait(false);

            return ApiResponse.Ok(value);
        }
    }
}
