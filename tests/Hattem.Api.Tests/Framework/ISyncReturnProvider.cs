namespace Hattem.Api.Tests.Framework
{
    public interface ISyncReturnProvider<in TInput, out TOutput>
    {
        TOutput Execute(TInput input);
    }

    public interface ISyncReturnProvider<in TInput1, in TInput2, out TOutput>
    {
        TOutput Execute(TInput1 input1, TInput2 input2);
    }

    public interface ISyncReturnProvider<in TInput1, in TInput2, in TInput3, out TOutput>
    {
        TOutput Execute(TInput1 input1, TInput2 input2, TInput3 input3);
    }

    public interface ISyncReturnProvider<in TInput1, in TInput2, in TInput3, in TInput4, out TOutput>
    {
        TOutput Execute(
            TInput1 input1,
            TInput2 input2,
            TInput3 input3,
            TInput4 input4
        );
    }

    public interface ISyncReturnProvider<in TInput1, in TInput2, in TInput3, in TInput4, in TInput5, out TOutput>
    {
        TOutput Execute(
            TInput1 input1,
            TInput2 input2,
            TInput3 input3,
            TInput4 input4,
            TInput5 input5
        );
    }
}