using System.Threading.Tasks;

namespace Hattem.Api.Tests.Framework
{
    public interface IAsyncValueTaskExecutionProvider<in TInput>
    {
        ValueTask Execute(TInput input);
    }
}
