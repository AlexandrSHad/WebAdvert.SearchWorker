using Microsoft.Extensions.Configuration;
namespace WebAdvert.SearchWorker.Services
{
    public interface ILambdaConfiguration
    {
        IConfigurationRoot Configuration { get; }
    }
}
