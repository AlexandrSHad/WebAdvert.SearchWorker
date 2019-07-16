using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LambdaHosting.Abstractions
{
    public interface ILambda
    {
        Task ExecuteAsync();
    }
}
