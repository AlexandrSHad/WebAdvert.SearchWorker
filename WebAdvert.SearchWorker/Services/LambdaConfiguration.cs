using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebAdvert.SearchWorker.Services
{
    public class LambdaConfiguration : ILambdaConfiguration
    {
        public static IConfigurationRoot Configuration =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        IConfigurationRoot ILambdaConfiguration.Configuration => Configuration;
    }
}
