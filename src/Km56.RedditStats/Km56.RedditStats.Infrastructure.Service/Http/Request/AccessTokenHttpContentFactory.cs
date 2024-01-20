using Km56.Infrastructure.Http.Request;
using Microsoft.Extensions.Configuration;

namespace Km56.RedditStats.Infrastructure.Service.Http.Request
{
    internal class AccessTokenHttpContentFactory : HttpContentFactory
    {
        protected readonly IConfiguration _configuration;

        private readonly string _grantType;
        private readonly string _redditUserName;
        private readonly string _redditPassword;

        public AccessTokenHttpContentFactory(IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentException(nameof(configuration));
            _configuration = configuration;

            _grantType = configuration.GetRequiredSection("Reddit:GrantType").Value;
            _redditUserName = configuration.GetRequiredSection("Reddit:UserName").Value;
            _redditPassword = configuration.GetRequiredSection("Reddit:Password").Value;
        }

        public override HttpContent CreateHttpContent()
        {
            var nameValueCollection = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string> ("grant_type", _grantType),
                new KeyValuePair<string, string> ("username", _redditUserName),
                new KeyValuePair<string, string> ("password", _redditPassword)
            };
            var content = new FormUrlEncodedContent(nameValueCollection);

            return content;
        }
    }
}
