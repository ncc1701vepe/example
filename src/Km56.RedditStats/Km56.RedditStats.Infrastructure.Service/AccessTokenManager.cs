using Km56.RedditStats.Domain.Service;

namespace Km56.RedditStats.Infrastructure.Service
{
    internal class AccessTokenManager
    {
        protected const int DefaultExpiration = 3600;

        protected readonly IAuthService _authService;

        protected string? _accessToken;
        protected DateTime? _accessTokenCreated;
        protected int _accessTokenExpiration = DefaultExpiration;

        public AccessTokenManager(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<string?> GetAccessToken()
        {
            bool regenerateAccessToken = false;

            if (_accessToken is not null && _accessTokenCreated is not null)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract((DateTime)_accessTokenCreated);
                if (timeSpan.TotalSeconds > _accessTokenExpiration)
                {
                    regenerateAccessToken = true;
                }
            }
            else
            {
                regenerateAccessToken = true;
            }

            if (regenerateAccessToken)
            {
                _accessTokenCreated = DateTime.Now;
                var response = await _authService.GetAccessToken();
                if (response.IsSuccess)
                {
                    if (string.IsNullOrWhiteSpace(response.Result?.access_token))
                    {
                        _accessToken = null;
                        _accessTokenCreated = null;
                        _accessTokenExpiration = DefaultExpiration;
                    }
                    else
                    {
                        _accessToken = response.Result?.access_token;
                        _accessTokenExpiration = response.Result?.expires_in ?? DefaultExpiration;
                    }
                }
                else
                {
                    _accessToken = null;
                    _accessTokenCreated = null;
                    _accessTokenExpiration = DefaultExpiration;
                }
            }

            return _accessToken;
        }
    }
}
