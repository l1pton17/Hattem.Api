namespace Hattem.Api.Tests.Framework
{
    public interface ISyncResponseProvider<in TInput,TOutput>
    {
        ApiResponse<TOutput> Execute(TInput input);
    }
}