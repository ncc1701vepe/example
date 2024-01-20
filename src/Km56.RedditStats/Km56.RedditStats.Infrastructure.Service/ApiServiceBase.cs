using Km56.Infrastructure.Common;
using Km56.Infrastructure.Http;
using Microsoft.Extensions.Configuration;

namespace Km56.RedditStats.Infrastructure.Service
{
    /// <summary>
    /// Provides a base behavior for a service in Reddit Stas app
    /// </summary>
    public class ApiServiceBase : WebApiProxy
    {
        protected readonly IConfiguration _configuration;
        protected readonly ObjectMapper _mapper;

        public ApiServiceBase(IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentException(nameof(configuration));
            _configuration = configuration;

            _mapper = new ObjectMapper();
        }
    }
}
