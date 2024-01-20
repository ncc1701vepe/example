using Km56.RedditStats.Infrastructure.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Km56.RedditStats.Infrastructure.Service.Test
{
    internal static class ServiceCollectionFactory
    {
        public static IServiceProvider BuildProvider()
        {
            IConfiguration configuration = ConfigurationFactory.BuildConfiguration();

            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IDataService, DataService>();

            return services.BuildServiceProvider();
        }
    }
}
