using Km56.RedditStats.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Km56.RedditStats.Infrastructure.Service.Test
{
    [TestClass]
    public class DataServiceTests
    {
        private IServiceProvider _serviceProvider; 

        public DataServiceTests()
        {
            _serviceProvider = ServiceCollectionFactory.BuildProvider();
        }

        [TestMethod]
        public async Task SuccessfullyGotSubRedditData()
        {
            Dictionary<string, string>? listings = new Dictionary<string, string>
            {
                { "t", "week" }
            };

            IDataService dataService = _serviceProvider.GetRequiredService<IDataService>();
            var result = await dataService.GetSubRedditSort("csharp", "top", listings);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Result), "Results expected not empty");
        }
    }
}