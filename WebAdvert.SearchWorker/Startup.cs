using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using WebAdvert.SearchWorker.Services;

namespace WebAdvert.SearchWorker
{
    public class Startup
    {
        public static IServiceCollection Container => ConfigureServices(LambdaConfiguration.Configuration);

        public static IServiceCollection ConfigureServices(IConfigurationRoot configuration)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IElasticClient>(new ElasticClient());

            return services;
        }
    }
}
