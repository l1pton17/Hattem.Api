namespace Hattem.Api
{
    public interface IApiResponse<out T>
    {
        T Data { get; }

        Error Error { get; }

        int? StatusCode { get; }

        bool HasErrors { get; }

        bool IsOk { get; }
    }
}