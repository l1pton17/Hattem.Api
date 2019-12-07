namespace Hattem.Api.Tests.Framework
{
    public interface ISyncExecutionProvider<in TInput>
    {
        void Execute(TInput input);
    }
}
