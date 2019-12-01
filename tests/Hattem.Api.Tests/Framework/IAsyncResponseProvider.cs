using System.Threading.Tasks;

namespace Hattem.Api.Tests.Framework
{
    public interface IAsyncResponseProvider<in TInput,TOutput>
    {
        Task<ApiResponse<TOutput>> Execute(TInput input);
    }
}