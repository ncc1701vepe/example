using Km56.Infrastructure.Http.Extension;
using Km56.Infrastructure.Http.Header;
using Km56.Infrastructure.Http.Request;
using Km56.Infrastructure.Model;
using Km56.RedditStats.Infrastructure.Service.Http.Header;
using Microsoft.Extensions.Configuration;

namespace Km56.RedditStats.Infrastructure.Service
{
    /// <summary>
    /// Provides operations to retrieve data from Reddit API
    /// </summary>
    public class DataService : ApiServiceBase, IDataService
    {
        protected const int DefaultExpiration = 3600;

        protected string? _accessToken;
        protected DateTime? _accessTokenCreated;
        protected int _accessTokenExpiration = DefaultExpiration;

        private readonly AccessTokenManager _accessTokenManager;

        public DataService(IConfiguration configuration,
                           IAuthService authService)
            : base(configuration)
        {
            string baseUri = _configuration.GetRequiredSection("RedditDataApiUri").Value;
            _httpClient.BaseAddress = new Uri(baseUri);
            _accessTokenManager = new AccessTokenManager(authService);
        }

        /// <summary>
        /// Get Sub Reddit Data using given criteria
        /// </summary>
        /// <param name="subReddit">Sub Reddit Name</param>
        /// <param name="sortAction">controversial|top</param>
        /// <param name="listings"></param>
        /// <returns>a string json formatted</returns>
        public async Task<Response<string>> GetSubRedditSort(string subReddit, string sortAction, Dictionary<string, string>? listings = null)
        {
            if (sortAction != "controversial" && sortAction != "top") throw new ArgumentException($"Invalid Sort Action: {sortAction}", nameof(sortAction));

            RequestUriFactory requestUriFactory = new RequestUriFactory(templateUri: $"r/{subReddit}/{sortAction}", listings);

            DefaultUserAgentHeaderFactory userAgentHeaderFactory = new DefaultUserAgentHeaderFactory(_configuration);

            string? accessToken = await _accessTokenManager.GetAccessToken();
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentException("Unable to get Access Token", nameof(accessToken));
            AuthorizationHeaderFactory authorizationHeaderFactory = new RedditApiAuthorizationHeaderFactory(accessToken);

            RequestMessageBuilder requestMessageTemplate = new RequestMessageBuilder(httpMethod: HttpMethod.Get,
                                                                                     requestUriFactory, 
                                                                                     userAgentHeaderFactory, 
                                                                                     authorizationHeaderFactory);
            var httpRequestMessage = requestMessageTemplate.CreateRequestMessage();

            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

            return await httpResponseMessage.ToResponseAsString();
        }
    }
}
