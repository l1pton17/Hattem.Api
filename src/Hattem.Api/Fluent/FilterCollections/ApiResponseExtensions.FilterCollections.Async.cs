﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static async Task<ApiResponse<ImmutableArray<TInput>>> Filter<TInput>(
            this IEnumerable<TInput> source,
            Func<TInput, Task<ApiResponse<Unit>>> predicate,
            Action<TInput, Error>? onError = null
        )
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var builder = ImmutableArray.CreateBuilder<TInput>();

            foreach (var item in source)
            {
                var predicateResponse = await predicate(item).ConfigureAwait(false);

                if (predicateResponse.Error is not null)
                {
                    onError?.Invoke(item, predicateResponse.Error);
                }
                else
                {
                    builder.Add(item);
                }
            }

            return ApiResponse.Ok(builder.ToImmutable());
        }

        public static async Task<ApiResponse<ImmutableArray<TInput>>> Filter<TInput>(
            this IEnumerable<TInput> source,
            Func<TInput, Task<ApiResponse<Unit>>> predicate,
            Func<TInput, Error, Task> onError
        )
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var builder = ImmutableArray.CreateBuilder<TInput>();

            foreach (var item in source)
            {
                var predicateResponse = await predicate(item).ConfigureAwait(false);

                if (predicateResponse.Error is not null)
                {
                    if (onError != null)
                    {
                        await onError(item, predicateResponse.Error).ConfigureAwait(false);
                    }
                }
                else
                {
                    builder.Add(item);
                }
            }

            return ApiResponse.Ok(builder.ToImmutable());
        }

        public static async Task<ApiResponse<ImmutableArray<TInput>>> Filter<TInput>(
            this IEnumerable<TInput> source,
            Func<TInput, Task<ApiResponse<bool>>> predicate
        )
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var builder = ImmutableArray.CreateBuilder<TInput>();

            foreach (var item in source)
            {
                var predicateResponse = await predicate(item).ConfigureAwait(false);

                if (predicateResponse.HasErrors)
                {
                    return predicateResponse.Cast(To<ImmutableArray<TInput>>.Type);
                }
                else if (predicateResponse.Data)
                {
                    builder.Add(item);
                }
            }

            return ApiResponse.Ok(builder.ToImmutable());
        }
    }
}
