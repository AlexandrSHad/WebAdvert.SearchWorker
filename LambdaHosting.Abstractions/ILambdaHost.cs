using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LambdaHosting.Abstractions
{
    public interface ILambdaHost
    {
        // use IApplicationLifetime to stop app after work is completed
        Task RunAsync(SNSEvent snsEvent, ILambdaContext context, CancellationToken cancellationToken = default);
    }
}
