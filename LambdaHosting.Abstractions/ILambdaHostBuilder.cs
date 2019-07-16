using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LambdaHosting.Abstractions
{
    public interface ILambdaHostBuilder
    {
        ILambdaHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate);
        // or move this to the LambdaHostBuilderExtensions
        ILambdaHostBuilder UseStartup(Action<HostBuilderContext, IServiceCollection> configureDelegate);
        ILambdaHost Build();
    }
}
