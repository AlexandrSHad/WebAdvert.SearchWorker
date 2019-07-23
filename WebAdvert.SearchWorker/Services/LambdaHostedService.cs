using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAdvert.AdvertApi.Dto.Messages;
using WebAdvert.SearchWorker.Models;

namespace WebAdvert.SearchWorker.Services
{
    public class LambdaHostedService : IHostedService
    {
        private readonly IApplicationLifetime _applicationLifetime;
        private readonly IElasticSearchService _elasticSearchService;
        private readonly SNSEvent _snsEvent;
        private readonly ILambdaContext _lambdaContext;

        public LambdaHostedService(
            IApplicationLifetime applicationLifetime,
            IElasticSearchService elasticSearchService,
            SNSEvent snsEvent,
            ILambdaContext lambdaContext
        )
        {
            _applicationLifetime = applicationLifetime;
            _elasticSearchService = elasticSearchService;
            _snsEvent = snsEvent;
            _lambdaContext = lambdaContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var record in _snsEvent.Records)
            {
                _lambdaContext.Logger.LogLine(record.Sns.Message);

                var message = JsonConvert.DeserializeObject<AdvertConfirmedMessage>(record.Sns.Message);
                var advertDocument = new AdvertDocument
                {
                    Id = message.Id,
                    Title = message.Title,
                    CreationDateTime = DateTime.UtcNow
                };

                await _elasticSearchService.IndexAdvertDocument(advertDocument);
            }

            _applicationLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
