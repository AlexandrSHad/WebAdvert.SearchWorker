using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WebAdvert.SearchWorker.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        private static IElasticClient _client = null;

        public ElasticSearchService(IConfiguration configuration)
        {
            var url = configuration.GetSection("ES").GetValue<string>("url");

            Debug.WriteLine($"ElasticSearchService.ctor - ES URL: {url}");

            var setttings = new ConnectionSettings(new Uri(url))
                .DefaultIndex("adverts");

            _client = new ElasticClient(setttings);
        }
    }
}
