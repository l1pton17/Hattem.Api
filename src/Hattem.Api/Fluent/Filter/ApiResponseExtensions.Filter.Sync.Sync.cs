using System;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static ApiResponse<TInput> Filter<TInput>(
            this ApiResponse<TInput> source,
            Func<TInput, ApiResponse<Unit>> predicate
        )
        {
            if (source.HasErrors)
            {
                return source;
            }

            var predicateResponse = predicate(source.Data);

            if (predicateResponse.HasErrors)
            {
                return predicateResponse.Cast(To<TInput>.Type);
            }

            return source;
        }
    }
}
