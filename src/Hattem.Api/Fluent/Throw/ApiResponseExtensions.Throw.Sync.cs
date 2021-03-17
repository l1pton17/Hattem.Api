using Hattem.Api.Extensions;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        /// <summary>
        /// Throws exception if <paramref name="source"/> has errors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ApiResponse<T> Throw<T>(
            this ApiResponse<T> source
        )
        {
            if (source.Error is not null)
            {
                throw new HattemApiException(source.Error);
            }

            return source;
        }
    }
}
