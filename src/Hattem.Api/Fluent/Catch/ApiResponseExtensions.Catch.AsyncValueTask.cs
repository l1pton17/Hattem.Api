using System;
using System.Threading.Tasks;
using Hattem.Api.Errors;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static async ValueTask<ApiResponse<Unit>> Catch(
            this ValueTask source)
        {
            try
            {
                await source.ConfigureAwait(false);

                return ApiResponse.Ok();
            }
            catch (Exception e)
            {
                var error = new ExceptionError(e);

                return ApiResponse.Error(error);
            }
        }

        public static async ValueTask<ApiResponse<T>> Catch<T>(
            this ValueTask<T> source)
        {
            try
            {
                var data = await source.ConfigureAwait(false);

                return ApiResponse.Ok(data);
            }
            catch (Exception e)
            {
                var error = new ExceptionError(e);

                return ApiResponse.Error<T>(error);
            }
        }

        public static async ValueTask<ApiResponse<T>> Catch<T>(
            this ValueTask<ApiResponse<T>> source
        )
        {
            try
            {
                return await source.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var error = new ExceptionError(e);

                return ApiResponse.Error<T>(error);
            }
        }
    }
}
