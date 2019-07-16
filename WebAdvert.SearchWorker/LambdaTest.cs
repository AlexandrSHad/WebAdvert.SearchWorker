using LambdaHosting.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WebAdvert.SearchWorker.Services;

namespace WebAdvert.SearchWorker
{
    public class LambdaTest : ILambda
    {
        private readonly IElasticSearchService _elasticSearchService;

        public LambdaTest(IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        public Task ExecuteAsync()
        {
            Debug.WriteLine("LambdaTest.ExecuteAsync");

            return Task.Delay(2000);
        }
    }
}
