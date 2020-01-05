namespace Hattem.Api
{
    public interface IApiResponse
    {
        Error Error { get; }

        int? StatusCode { get; }

        bool HasErrors { get; }

        bool IsOk { get; }
    }

    public interface IApiResponse<out T> : IApiResponse
    {
        T Data { get; }
    }
}