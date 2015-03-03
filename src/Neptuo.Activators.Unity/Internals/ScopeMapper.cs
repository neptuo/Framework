using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class ScopeMapper
    {
        public LifetimeManager CreateLifetimeManager(DependencyLifetime lifetime)
        {
            if (lifetime.IsTransient)
                return new TransientLifetimeManager();

            if (lifetime.IsSingleton)
                return new SingletonLifetimeManager();   

            if (lifetime.IsScoped)
                return new HierarchicalLifetimeManager();

            //TODO: Create lifetime manager for named scopes.
            throw Guard.Exception.NotImplemented();
        }
    }
}
