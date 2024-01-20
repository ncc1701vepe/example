using Km56.Infrastructure.Model;
using Km56.RedditStats.Application.Dto;

namespace Km56.RedditStats.Infrastructure.Service
{
    public interface IAuthService
    {
        public Task<Response<AccessTokenData>> GetAccessToken();
    }
}
