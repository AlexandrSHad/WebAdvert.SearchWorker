using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;
using WebAdvert.SearchWorker.Services;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace WebAdvert.SearchWorker
{
    public class SearchWorkerFunction
    {
        public async Task FunctionEntryPoint(SNSEvent snsEvent, ILambdaContext context)
        {
            var lambdaHost = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, appConfigBuilder) => {
                    appConfigBuilder.SetBasePath(Directory.GetCurrentDirectory());
                    appConfigBuilder.AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(snsEvent);
                    services.AddSingleton(context);
                    services.AddSingleton<IElasticSearchService, ElasticSearchService>();
                    services.AddHostedService<LambdaHostedService>();
                })
                .Build();

            await lambdaHost.StartAsync();
        }
    }
}
