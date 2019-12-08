namespace Hattem.Api.Tests.Framework
{
    public interface ISyncResponseProvider<in TInput, TOutput>
    {
        ApiResponse<TOutput> Execute(TInput input);
    }

    public interface ISyncResponseProvider<in T1, in T2, TOutput>
    {
        ApiResponse<TOutput> Execute(T1 input1, T2 input2);
    }

    public interface ISyncResponseProvider<in T1, in T2, in T3, TOutput>
    {
        ApiResponse<TOutput> Execute(
            T1 input1,
            T2 input2,
            T3 input3
        );
    }

    public interface ISyncResponseProvider<in T1, in T2, in T3, in T4, TOutput>
    {
        ApiResponse<TOutput> Execute(
            T1 input1,
            T2 input2,
            T3 input3,
            T4 input4
        );
    }

    public interface ISyncResponseProvider<in T1, in T2, in T3, in T4, in T5, TOutput>
    {
        ApiResponse<TOutput> Execute(
            T1 input1,
            T2 input2,
            T3 input3,
            T4 input4,
            T5 input5
        );
    }
}