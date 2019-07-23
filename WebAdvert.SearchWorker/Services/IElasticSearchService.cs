using System.Threading.Tasks;
using WebAdvert.SearchWorker.Models;

namespace WebAdvert.SearchWorker.Services
{
    public interface IElasticSearchService
    {
        Task<bool> IndexAdvertDocument(AdvertDocument document);
    }
}