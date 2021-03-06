﻿using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static ValueTask<ApiResponse<TInput>> Filter<TInput>(
            this ApiResponse<TInput> source,
            Func<TInput, ValueTask<ApiResponse<Unit>>> predicate
        )
        {
            async ValueTask<ApiResponse<TInput>> Async()
            {
                var predicateResponse = await predicate(source.Data!).ConfigureAwait(false);

                if (predicateResponse.HasErrors)
                {
                    return predicateResponse.Cast(To<TInput>.Type);
                }

                return source;
            }

            return source.HasErrors ? source.AsValueTask() : Async();
        }
    }
}
