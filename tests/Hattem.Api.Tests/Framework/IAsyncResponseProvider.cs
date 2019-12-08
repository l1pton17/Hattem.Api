using System.Threading.Tasks;

namespace Hattem.Api.Tests.Framework
{
    public interface IAsyncResponseProvider<in TInput,TOutput>
    {
        Task<ApiResponse<TOutput>> Execute(TInput input);
    }

    public interface IAsyncResponseProvider<in T1, in T2, TOutput>
    {
        Task<ApiResponse<TOutput>> Execute(T1 input1, T2 input2);
    }

    public interface IAsyncResponseProvider<in T1, in T2, in T3, TOutput>
    {
        Task<ApiResponse<TOutput>> Execute(
            T1 input1,
            T2 input2,
            T3 input3
        );
    }

    public interface IAsyncResponseProvider<in T1, in T2, in T3, in T4, TOutput>
    {
        Task<ApiResponse<TOutput>> Execute(
            T1 input1,
            T2 input2,
            T3 input3,
            T4 input4
        );
    }

    public interface IAsyncResponseProvider<in T1, in T2, in T3, in T4, in T5, TOutput>
    {
        Task<ApiResponse<TOutput>> Execute(
            T1 input1,
            T2 input2,
            T3 input3,
            T4 input4,
            T5 input5
        );
    }
}