namespace Km56.Infrastructure.Http
{
    public interface IWebApiProxy
    {
        Task<TResponse?> GetAsync<TResponse>(string url);

        Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data);
    }
}
