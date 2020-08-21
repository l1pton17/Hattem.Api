using System.Threading.Tasks;

namespace Hattem.Api.Tests.Framework
{
    public interface IAsyncValueTaskReturnProvider<in TInput, TOutput>
    {
        ValueTask<TOutput> Execute(TInput input);
    }

    public interface IAsyncValueTaskReturnProvider<in TInput1, in TInput2, TOutput>
    {
        ValueTask<TOutput> Execute(TInput1 input1, TInput2 input2);
    }

    public interface IAsyncValueTaskReturnProvider<in TInput1, in TInput2, in TInput3, TOutput>
    {
        ValueTask<TOutput> Execute(TInput1 input1, TInput2 input2, TInput3 input3);
    }

    public interface IAsyncValueTaskReturnProvider<in TInput1, in TInput2, in TInput3, in TInput4, TOutput>
    {
        ValueTask<TOutput> Execute(
            TInput1 input1,
            TInput2 input2,
            TInput3 input3,
            TInput4 input4
        );
    }

    public interface IAsyncValueTaskReturnProvider<in TInput1, in TInput2, in TInput3, in TInput4, in TInput5, TOutput>
    {
        ValueTask<TOutput> Execute(
            TInput1 input1,
            TInput2 input2,
            TInput3 input3,
            TInput4 input4,
            TInput5 input5
        );
    }
}
