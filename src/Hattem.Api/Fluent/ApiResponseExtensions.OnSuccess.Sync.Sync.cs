using System;
using System.Collections.Generic;
using System.Text;

namespace Hattem.Api.Fluent
{
    partial class ApiResponseExtensions
    {
        public static ApiResponse<T> OnSuccess<T>(
            this ApiResponse<T> source,
            Action<T> onSuccess)
        {
            if (source.IsOk)
            {
                onSuccess(source.Data);
            }

            return source;
        }
    }
}
