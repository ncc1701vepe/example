using Km56.Infrastructure.Http.Extension;
using Km56.Infrastructure.Http.Header;
using Km56.Infrastructure.Http.Request;
using Km56.Infrastructure.Model;
using Km56.RedditStats.Application.Dto;
using Km56.RedditStats.Infrastructure.Service.Http.Header;
using Km56.RedditStats.Infrastructure.Service.Http.Request;
using Microsoft.Extensions.Configuration;

namespace Km56.RedditStats.Infrastructure.Service
{
    /// <summary>
    /// Defines Reddit API authorization operations 
    /// </summary>
    public class AuthService : ApiServiceBase, IAuthService
    {
        public AuthService(IConfiguration configuration)
            : base(configuration)
        {
            string baseUri = _configuration.GetRequiredSection("RedditAuthApiUri").Value;
            _httpClient.BaseAddress = new Uri(baseUri);
        }

        /// <summary>
        /// Get the access token for the Reddit API
        /// </summary>
        /// <returns>The access token data</returns>
        public async Task<Response<AccessTokenData>> GetAccessToken()
        {
            RequestUriFactory requestUriFactory = new RequestUriFactory(templateUri: "api/v1/access_token");
            DefaultUserAgentHeaderFactory userAgentHeaderFactory = new DefaultUserAgentHeaderFactory(_configuration);
            AuthorizationHeaderFactory authorizationHeaderFactory = new AccessTokenAuthorizationHeaderFactory(_configuration);
            HttpContentFactory requestContentFactory = new AccessTokenHttpContentFactory(_configuration);

            RequestMessageBuilder requestMessageTemplate = new RequestMessageBuilder(httpMethod: HttpMethod.Post,
                                                                                       requestUriFactory,
                                                                                       userAgentHeaderFactory,
                                                                                       authorizationHeaderFactory, 
                                                                                       requestContentFactory);
            var httpRequestMessage = requestMessageTemplate.CreateRequestMessage();

            var responseMessage = await _httpClient.SendAsync(httpRequestMessage);

            return await responseMessage.ToResponse<AccessTokenData>();
        }
    }
}
