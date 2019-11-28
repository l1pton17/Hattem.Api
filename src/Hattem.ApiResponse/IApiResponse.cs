namespace Hattem.ApiResponse
{
    public interface IApiResponse<out T>
    {
        T Data { get; }
    }
}