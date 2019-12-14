using System.Threading.Tasks;

namespace Hattem.Api.Tests.Framework
{
    public interface IAsyncReturnProvider<in TInput, TOutput>
    {
        Task<TOutput> Execute(TInput input);
    }

    public interface IAsyncReturnProvider<in TInput1, in TInput2, TOutput>
    {
        Task<TOutput> Execute(TInput1 input1, TInput2 input2);
    }

    public interface IAsyncReturnProvider<in TInput1, in TInput2, in TInput3, TOutput>
    {
        Task<TOutput> Execute(TInput1 input1, TInput2 input2, TInput3 input3);
    }

    public interface IAsyncReturnProvider<in TInput1, in TInput2, in TInput3, in TInput4, TOutput>
    {
        Task<TOutput> Execute(
            TInput1 input1,
            TInput2 input2,
            TInput3 input3,
            TInput4 input4
        );
    }

    public interface IAsyncReturnProvider<in TInput1, in TInput2, in TInput3, in TInput4, in TInput5, TOutput>
    {
        Task<TOutput> Execute(
            TInput1 input1,
            TInput2 input2,
            TInput3 input3,
            TInput4 input4,
            TInput5 input5
        );
    }
}