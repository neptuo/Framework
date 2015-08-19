using Neptuo.Activators;
using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Internals
{
    internal class ActivatorFactory : IFactory<IBootstrapHandler, Type>
    {
        public IBootstrapHandler Create(Type handlerType)
        {
            return (IBootstrapHandler)Activator.CreateInstance(handlerType);
        }
    }
}
