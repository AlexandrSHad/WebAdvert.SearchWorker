using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using LambdaHosting.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WebAdvert.SearchWorker.Services;

namespace WebAdvert.SearchWorker
{
    public class LambdaTest : ILambda<SNSEvent>
    {
        private readonly IElasticSearchService _elasticSearchService;

        public LambdaTest(IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        public Task ExecuteAsync(SNSEvent snsEvent, ILambdaContext context)
        {
            Debug.WriteLine("LambdaTest.ExecuteAsync");

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

            return Task.Delay(2000);
        }
    }
}
