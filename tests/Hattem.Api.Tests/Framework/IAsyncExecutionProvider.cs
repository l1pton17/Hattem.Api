using System.Threading.Tasks;

namespace Hattem.Api.Tests.Framework
{
    public interface IAsyncExecutionProvider<in TInput>
    {
        Task Execute(TInput input);
    }
}
