using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebAdvert.AdvertApi.Dto.Messages;
using WebAdvert.SearchWorker.Models;
using WebAdvert.SearchWorker.Services;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace WebAdvert.SearchWorker
{
    public class SearchWorkerFunction
    {
        private readonly IServiceProvider _serviceProvider;

        //public SearchWorkerFunction(IServiceProvider serviceProvider)
        //{
        //    _serviceProvider = serviceProvider;
        //}

        //public SearchWorkerFunction() : this(Startup.Container.BuildServiceProvider())
        public SearchWorkerFunction()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        public async Task FunctionHandler(SNSEvent snsEvent, ILambdaContext context)
        {
            var configuration = _serviceProvider.GetService<IConfiguration>();
            var url = configuration.GetSection("ES").GetValue<string>("url");

            var setttings = new ConnectionSettings(new Uri(url))
                .DefaultIndex("adverts")
                .DefaultMappingFor<AdvertDocument>(m => m.IdProperty(x => x.Id));

            var elasticClient = new ElasticClient(setttings);

            context.Logger.LogLine($"Elastic service url: {url}");

            foreach (var record in snsEvent.Records)
            {
                context.Logger.LogLine(record.Sns.Message);

                //var message = JsonConvert.DeserializeObject<AdvertConfirmedMessage>(record.Sns.Message);
                //var advertDocument = new AdvertDocument
                //{
                //    Id = message.Id,
                //    Title = message.Title,
                //    CreationDateTime = DateTime.UtcNow
                //};

                //await elasticClient.IndexDocumentAsync(advertDocument);
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(LambdaConfiguration.Configuration);
            //services.AddSingleton<IElasticClient>(new ElasticClient());
        }
    }
}
