using km56.VirtualStorage.Application.Common;
using km56.VirtualStorage.Application.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace km56.VirtualStorage.Application.Extension
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds all the services available in Cloudmersive Storage Application
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddStorageServices(this IServiceCollection services)
        {
            // Adding Implementations of IVirtualStorageService
            services.AddTransient<AzureCloudStorageService>();
            services.AddTransient<AwsS3Service>();

            // Adding the implementation of the service that exposes available operations for storing in a Virtual Storage Repository
            services.AddScoped<IVirtualStorageRepository, VirtualStorageRepository>();

            // Defines a FUNC delegate that returns the specific implementation of IVirtualStoreageService using the given connectionName
            services.AddTransient<Func<string, IVirtualStorageService>>(serviceProvider => connectionName => {
                switch (connectionName)
                {
                    case VirtualStorageIdentifier.Azure:
                        return serviceProvider.GetRequiredService<AzureCloudStorageService>();
                    case VirtualStorageIdentifier.AWS:
                        return serviceProvider.GetRequiredService<AwsS3Service>();
                    default:
                        throw new NotImplementedException($"No Virtual Storage Service implemented for '{connectionName}'");
                }
            });

            return services;
        }
    }
}
