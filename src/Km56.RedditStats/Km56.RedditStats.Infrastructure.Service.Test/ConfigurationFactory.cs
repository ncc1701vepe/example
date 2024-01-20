using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Km56.RedditStats.Infrastructure.Service.Test
{
    internal static class ConfigurationFactory
    {
        public static IConfiguration BuildConfiguration()
        {
            var inMemorySettings = new Dictionary<string, string?> {
                {"RedditDataApiUri", "https://oauth.reddit.com"},
                {"RedditAuthApiUri", "https://www.reddit.com"},
                {"Reddit:GrantType", "password"},
                {"Reddit:UserName", "ncc1701vepe"},
                {"Reddit:Password", "2ca5@22D3cfc"},
                {"Reddit:AppId", "gwXc7UMODk5NVPgTmWQaPA"},
                {"Reddit:Secret", "MClINlLJ_Y7-SdFVfYLKWaUnAQtDHQ"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            return configuration;
        }
    }
}
