using Km56.Infrastructure.Http.Header;
using System.Net.Http.Headers;

namespace Km56.RedditStats.Infrastructure.Service.Http.Header
{
    internal class RedditApiAuthorizationHeaderFactory : AuthorizationHeaderFactory
    {
        protected readonly string _accessToken;

        public RedditApiAuthorizationHeaderFactory(string accessToken)
        {
            _accessToken = accessToken;
        }

        public override AuthenticationHeaderValue? CreateHeaderAuthorization()
        {
            return new AuthenticationHeaderValue("Bearer", _accessToken);
        }
    }
}
