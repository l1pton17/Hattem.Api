using System;
using Hattem.Api.Extensions;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static ApiResponse<TOutput> Return<TInput, TOutput>(
            this ApiResponse<TInput> source,
            TOutput value
        )
        {
            if (source.HasErrors)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            return ApiResponse.Ok(value);
        }

        public static ApiResponse<TOutput> Return<TInput, TOutput>(
            this ApiResponse<TInput> source,
            Func<TInput, TOutput> valueFactory
        )
        {
            if (source.HasErrors)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            return ApiResponse.Ok(valueFactory(source.Data));
        }

        public static ApiResponse<TOutput> Return<T1, T2, TOutput>(
            this ApiResponse<(T1, T2)> source,
            Func<T1, T2, TOutput> valueFactory
        )
        {
            if (source.HasErrors)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            var value = valueFactory(source.Data.Item1, source.Data.Item2);

            return ApiResponse.Ok(value);
        }

        public static ApiResponse<TOutput> Return<T1, T2, T3, TOutput>(
            this ApiResponse<(T1, T2, T3)> source,
            Func<T1, T2, T3, TOutput> valueFactory
        )
        {
            if (source.HasErrors)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            var value = valueFactory(
                source.Data.Item1,
                source.Data.Item2,
                source.Data.Item3);

            return ApiResponse.Ok(value);
        }

        public static ApiResponse<TOutput> Return<T1, T2, T3, T4, TOutput>(
            this ApiResponse<(T1, T2, T3, T4)> source,
            Func<T1, T2, T3, T4, TOutput> valueFactory
        )
        {
            if (source.HasErrors)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            var value = valueFactory(
                source.Data.Item1,
                source.Data.Item2,
                source.Data.Item3,
                source.Data.Item4);

            return ApiResponse.Ok(value);
        }

        public static ApiResponse<TOutput> Return<T1, T2, T3, T4, T5, TOutput>(
            this ApiResponse<(T1, T2, T3, T4, T5)> source,
            Func<T1, T2, T3, T4, T5, TOutput> valueFactory
        )
        {
            if (source.HasErrors)
            {
                return source.Error.ToResponse(To<TOutput>.Type);
            }

            var value = valueFactory(
                source.Data.Item1,
                source.Data.Item2,
                source.Data.Item3,
                source.Data.Item4,
                source.Data.Item5);

            return ApiResponse.Ok(value);
        }
    }
}
