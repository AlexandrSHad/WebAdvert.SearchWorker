using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using LambdaHosting;
using LambdaHosting.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using WebAdvert.AdvertApi.Dto.Messages;
using WebAdvert.SearchWorker.Models;
using WebAdvert.SearchWorker.Services;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace WebAdvert.SearchWorker
{
    public class SearchWorkerFunction
    {
        //private readonly IServiceProvider _serviceProvider;

        //public SearchWorkerFunction()
        //{
        //    var services = new ServiceCollection();
        //    ConfigureServices(services);
        //    _serviceProvider = services.BuildServiceProvider();
        //}

        public async Task FunctionHandler(SNSEvent snsEvent, ILambdaContext context)
        {
            var lambdaHost = new LambdaHostBuilder()
                .ConfigureAppConfiguration((hostContext, appConfigBuilder) => {
                    appConfigBuilder.SetBasePath(Directory.GetCurrentDirectory());
                    appConfigBuilder.AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IElasticSearchService, ElasticSearchService>();
                    services.AddTransient<ILambda<SNSEvent>, LambdaTest>();
                })
                .Build();

            //HERE // TODO: pass different parameters SNSEvent or another
            //list of avaliable lambdas with available parameters
            await lambdaHost.RunAsync(snsEvent, context);

            //var configuration = _serviceProvider.GetService<IConfiguration>();
            //var url = configuration.GetSection("ES").GetValue<string>("url");

            //var setttings = new ConnectionSettings(new Uri(url))
            //    .DefaultIndex("adverts")
            //    .DefaultMappingFor<AdvertDocument>(m => m.IdProperty(x => x.Id));

            //var elasticClient = new ElasticClient(setttings);

            //context.Logger.LogLine($"Elastic service url: {url}");

            //foreach (var record in snsEvent.Records)
            //{
            //    context.Logger.LogLine(record.Sns.Message);

            //    //var message = JsonConvert.DeserializeObject<AdvertConfirmedMessage>(record.Sns.Message);
            //    //var advertDocument = new AdvertDocument
            //    //{
            //    //    Id = message.Id,
            //    //    Title = message.Title,
            //    //    CreationDateTime = DateTime.UtcNow
            //    //};

            //    //await elasticClient.IndexDocumentAsync(advertDocument);
            //}
        }

        //private void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddSingleton<IConfiguration>(LambdaConfiguration.Configuration);
        //    //services.AddSingleton<IElasticClient>(new ElasticClient());
        //}
    }
}
