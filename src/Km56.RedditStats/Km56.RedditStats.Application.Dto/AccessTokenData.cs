namespace Km56.RedditStats.Application.Dto
{
    /// <summary>
    /// Represents data returned by Reddit API's access_token operation
    /// </summary>
    public class AccessTokenData
    {
        public string? access_token { get; set; }

        public string? token_type { get; set; }

        public int expires_in { get; set; }

        public string? scope { get; set; }
    }
}