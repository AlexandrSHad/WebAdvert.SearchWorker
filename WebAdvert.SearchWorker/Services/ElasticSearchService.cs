using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebAdvert.SearchWorker.Models;

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
                .DefaultIndex("adverts")
                .DefaultMappingFor<AdvertDocument>(m => m.IdProperty(x => x.Id));

            _client = new ElasticClient(setttings);
        }

        public async Task<bool> IndexAdvertDocument(AdvertDocument document)
        {
            var response = await _client.IndexDocumentAsync(document);
            return response.IsValid;
        }
    }
}
