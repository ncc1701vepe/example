using Km56.Infrastructure.Http.Header;
using Microsoft.Extensions.Configuration;

namespace Km56.RedditStats.Infrastructure.Service.Http.Header
{
    internal class DefaultUserAgentHeaderFactory : UserAgentHeaderFactory
    {
        protected readonly IConfiguration _configuration;

        public DefaultUserAgentHeaderFactory(IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentException(nameof(configuration));
            _configuration = configuration;
        }

        public override string CreateUserAgentHeader()
        {
            var appId = _configuration.GetRequiredSection("Reddit:AppId").Value;
            var redditUserName = _configuration.GetRequiredSection("Reddit:UserName").Value;

            string userAgentHeader = $"{appId}/v1.0  (by /u/{redditUserName})";

            return userAgentHeader;
        }
    }
}
