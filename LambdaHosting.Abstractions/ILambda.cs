using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LambdaHosting.Abstractions
{
    public interface ILambda<TEvent> where TEvent : class
    {
        Task ExecuteAsync(TEvent lambdaEvent, ILambdaContext context);
    }
}
