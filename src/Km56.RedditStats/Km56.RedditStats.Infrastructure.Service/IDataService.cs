using Km56.Infrastructure.Model;

namespace Km56.RedditStats.Infrastructure.Service
{
    public interface IDataService
    {
        Task<Response<string>> GetSubRedditSort(string subReddit, string sortAction, Dictionary<string, string>? listings = null);
    }
}
