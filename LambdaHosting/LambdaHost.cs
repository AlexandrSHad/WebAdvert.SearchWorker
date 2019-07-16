using LambdaHosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LambdaHosting
{
    public class LambdaHost : ILambdaHost
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IApplicationLifetime _applicationLifeTime;

        public LambdaHost(IServiceProvider serviceProvider, IApplicationLifetime applicationLifeTime)
        {
            _serviceProvider = serviceProvider;
            _applicationLifeTime = applicationLifeTime;
        }

        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            Debug.WriteLine("-----------");
            Debug.WriteLine("LambdaHost.RunAsync - Starting");

            var lambda = _serviceProvider.GetService<ILambda>();

            if (lambda == null)
            {
                new InvalidOperationException("There is no ILambda service.");
            }

            Debug.WriteLine("LambdaHost.RunAsync - Lambda found");
            await lambda.ExecuteAsync();

            _applicationLifeTime.StopApplication();
        }
    }
}
