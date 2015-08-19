using Neptuo.Activators;
using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    /// <summary>
    /// Simple manual bootstrapper.
    /// Tasks are run synchronously and registered manually to <see cref="IBootstrapHandlerCollection"/>.
    /// </summary>
    public class ManualBootstrapper : IBootstrapper, IBootstrapHandlerCollection
    {
        private readonly Dictionary<Type, IFactory<IBootstrapHandler>> storage = new Dictionary<Type, IFactory<IBootstrapHandler>>();

        public IBootstrapHandlerCollection Add<T>(IFactory<T> factory) 
            where T : class, IBootstrapHandler
        {
            Ensure.NotNull(factory, "factory");
            storage[typeof(T)] = factory;
            return this;
        }

        public bool TryGet<T>(out IFactory<T> factory) 
            where T : class, IBootstrapHandler
        {
            IFactory<IBootstrapHandler> innerFactory;
            if (storage.TryGetValue(typeof(T), out innerFactory))
            {
                factory = (IFactory<T>)innerFactory;
                return true;
            }

            factory = null;
            return true;
        }

        public void Initialize()
        {
            foreach (IFactory<IBootstrapHandler> handlerFactory in storage.Values)
            {
                IBootstrapHandler handler = handlerFactory.Create();
                handler.Handle();
            }
        }
    }
}
