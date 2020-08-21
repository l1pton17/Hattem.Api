using System;
using System.Threading.Tasks;
using Hattem.Api.Extensions;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Execute <paramref name="next"/> if <paramref name="source"/> is ok
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="source"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public static ValueTask<ApiResponse<TOutput>> Then<TInput, TOutput>(
            this ApiResponse<TInput> source,
            Func<TInput, ValueTask<ApiResponse<TOutput>>> next
        )
        {
            if (source.HasErrors)
            {
                var errorResponse = source.Error.ToResponse(source.StatusCode, To<TOutput>.Type);

                return new ValueTask<ApiResponse<TOutput>>(errorResponse);
            }

            return next(source.Data);
        }

        /// <summary>
        /// Execute <paramref name="next"/> if <paramref name="source"/> is ok
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public static ValueTask<ApiResponse<TOutput>> Then<T1, T2, TOutput>(
            this ApiResponse<(T1, T2)> source,
            Func<T1, T2, ValueTask<ApiResponse<TOutput>>> next
        )
        {
            if (source.HasErrors)
            {
                var errorResponse = source.Error.ToResponse(source.StatusCode, To<TOutput>.Type);

                return new ValueTask<ApiResponse<TOutput>>(errorResponse);
            }

            return next(source.Data.Item1, source.Data.Item2);
        }

        /// <summary>
        /// Execute <paramref name="next"/> if <paramref name="source"/> is ok
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="source"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public static ValueTask<ApiResponse<TOutput>> Then<T1, T2, T3, TOutput>(
            this ApiResponse<(T1, T2, T3)> source,
            Func<T1, T2, T3, ValueTask<ApiResponse<TOutput>>> next
        )
        {
            if (source.HasErrors)
            {
                var errorResponse = source.Error.ToResponse(source.StatusCode, To<TOutput>.Type);

                return new ValueTask<ApiResponse<TOutput>>(errorResponse);
            }

            return next(
                source.Data.Item1,
                source.Data.Item2,
                source.Data.Item3);
        }

        /// <summary>
        /// Execute <paramref name="next"/> if <paramref name="source"/> is ok
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="source"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public static ValueTask<ApiResponse<TOutput>> Then<T1, T2, T3, T4, TOutput>(
            this ApiResponse<(T1, T2, T3, T4)> source,
            Func<T1, T2, T3, T4, ValueTask<ApiResponse<TOutput>>> next
        )
        {
            if (source.HasErrors)
            {
                var errorResponse = source.Error.ToResponse(source.StatusCode, To<TOutput>.Type);

                return new ValueTask<ApiResponse<TOutput>>(errorResponse);
            }

            return next(
                source.Data.Item1,
                source.Data.Item2,
                source.Data.Item3,
                source.Data.Item4);
        }

        /// <summary>
        /// Execute <paramref name="next"/> if <paramref name="source"/> is ok
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="source"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public static ValueTask<ApiResponse<TOutput>> Then<T1, T2, T3, T4, T5, TOutput>(
            this ApiResponse<(T1, T2, T3, T4, T5)> source,
            Func<T1, T2, T3, T4, T5, ValueTask<ApiResponse<TOutput>>> next
        )
        {
            if (source.HasErrors)
            {
                var errorResponse = source.Error.ToResponse(source.StatusCode, To<TOutput>.Type);

                return new ValueTask<ApiResponse<TOutput>>(errorResponse);
            }

            return next(
                source.Data.Item1,
                source.Data.Item2,
                source.Data.Item3,
                source.Data.Item4,
                source.Data.Item5);
        }
    }
}
