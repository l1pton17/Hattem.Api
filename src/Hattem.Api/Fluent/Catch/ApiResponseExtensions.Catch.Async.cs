﻿using System;
using System.Threading.Tasks;
using Hattem.Api.Errors;

// ReSharper disable once CheckNamespace
namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static async Task<ApiResponse<Unit>> Catch(
            this Task source
        )
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

        public static async Task<ApiResponse<T>> Catch<T>(
            this Task<T> source
        )
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

        public static async Task<ApiResponse<T>> Catch<T>(
            this Task<ApiResponse<T>> source
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
