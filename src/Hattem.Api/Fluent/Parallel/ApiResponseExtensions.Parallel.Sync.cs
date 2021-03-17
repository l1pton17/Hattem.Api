using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Execute <paramref name="action"/> in parallel for each item in <paramref name="source"/> with degree of parallelism <paramref name="degreeOfParallelism"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="degreeOfParallelism"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<Unit>> Parallel<T>(
            this IEnumerable<T> source,
            int degreeOfParallelism,
            Func<T, ApiResponse<Unit>> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ConcurrentQueue<ApiResponse<Unit>>? errors = null;

            await Task.WhenAll(
                Partitioner
                    .Create(source)
                    .GetPartitions(degreeOfParallelism)
                    .Select(
                        partition => Task.Run(
                            () =>
                            {
                                using (partition)
                                {
                                    while (partition.MoveNext())
                                    {
                                        var response = action(partition.Current);

                                        if (response.HasErrors)
                                        {
                                            LazyInitializer
                                                .EnsureInitialized(ref errors)
                                                .Enqueue(response);

                                            break;
                                        }
                                    }
                                }
                            })));

            return errors?.First() ?? ApiResponse.Ok();
        }

        /// <summary>
        /// Execute <paramref name="action"/> in parallel for each item in <paramref name="source"/> with default degree of parallelism
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Task<ApiResponse<Unit>> Parallel<T>(
            this IEnumerable<T> source,
            Func<T, ApiResponse<Unit>> action)
        {
            return Parallel(
                source,
                degreeOfParallelism: Environment.ProcessorCount,
                action);
        }
    }
}
