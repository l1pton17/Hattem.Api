namespace Hattem.Api
{
    public interface IApiResponse<out T>
    {
        T Data { get; }
    }
}