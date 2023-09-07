namespace km56.VirtualStorage.Api.Test.Http
{
    public interface IWebApiProxy
    {
        Task<TResponse?> GetAsync<TResponse>(string url);

        Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data);
    }
}
