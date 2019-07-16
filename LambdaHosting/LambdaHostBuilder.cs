using LambdaHosting.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace LambdaHosting
{
    public class LambdaHostBuilder : ILambdaHostBuilder
    {
        private List<Action<HostBuilderContext, IConfigurationBuilder>> _configureAppConfigActions = new List<Action<HostBuilderContext, IConfigurationBuilder>>();
        private List<Action<HostBuilderContext, IServiceCollection>> _configureServicesActions = new List<Action<HostBuilderContext, IServiceCollection>>();
        private IConfiguration _appConfiguration;
        private IServiceProvider _appServices;

        public ILambdaHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            _configureAppConfigActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        public ILambdaHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            _configureServicesActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        public ILambdaHost Build()
        {
            BuildAppConfiguration();
            CreateServiceProvider();

            return _appServices.GetRequiredService<ILambdaHost>();
        }

        public ILambdaHostBuilder UseStartup(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            throw new NotImplementedException();
        }

        private void BuildAppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            //configBuilder.AddConfiguration(_hostConfiguration);
            foreach (var buildAction in _configureAppConfigActions)
            {
                buildAction(null, configBuilder); // TODO: create host context
            }
            _appConfiguration = configBuilder.Build();
            //_hostBuilderContext.Configuration = _appConfiguration;
        }

        private void CreateServiceProvider()
        {
            var services = new ServiceCollection();
            //services.AddSingleton(_hostingEnvironment);
            //services.AddSingleton(_hostBuilderContext);
            services.AddSingleton(_appConfiguration);
            services.AddSingleton<IApplicationLifetime, ApplicationLifetime>();
            //services.AddSingleton<IHostLifetime, ConsoleLifetime>();
            services.AddSingleton<ILambdaHost, LambdaHost>();
            //services.AddOptions();
            services.AddLogging();

            foreach (var configureServicesAction in _configureServicesActions)
            {
                configureServicesAction(null, services); // TODO: create host context
            }

            _appServices = services.BuildServiceProvider();
        }
    }
}
