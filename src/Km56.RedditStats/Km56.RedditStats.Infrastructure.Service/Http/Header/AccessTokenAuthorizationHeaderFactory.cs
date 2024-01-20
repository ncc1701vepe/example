using Km56.Infrastructure.Http.Header;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;

namespace Km56.RedditStats.Infrastructure.Service.Http.Header
{
    internal class AccessTokenAuthorizationHeaderFactory : AuthorizationHeaderFactory
    {
        private readonly string _authUserName;
        private readonly string _authPassword;

        public AccessTokenAuthorizationHeaderFactory(IConfiguration configuration)
        {
            _authUserName = configuration.GetRequiredSection("Reddit:AppId").Value;
            _authPassword = configuration.GetRequiredSection("Reddit:Secret").Value;
        }

        public override AuthenticationHeaderValue? CreateHeaderAuthorization()
        {
            var authenticationString = $"{_authUserName}:{_authPassword}";
            var base64Encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
            var authenticationHeaderValue = new AuthenticationHeaderValue("Basic", base64Encoded);

            return authenticationHeaderValue;
        }
    }
}
