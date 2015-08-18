using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    public abstract class BootstrapperBase : IBootstrapper
    {
        private Func<Type, IBootstrapHandler> factory;

        protected List<IBootstrapHandler> Handlers { get; private set; }

        public BootstrapperBase(Func<Type, IBootstrapHandler> factory)
        {
            Ensure.NotNull(factory, "factory");
            this.factory = factory;
            Handlers = new List<IBootstrapHandler>();
        }

        protected IBootstrapHandler CreateInstance(Type type)
        {
            return factory(type);
        }

        protected IBootstrapHandler CreateInstance<T>()
            where T : IBootstrapHandler
        {
            return factory(typeof(T));
        }

        public async virtual Task Initialize()
        {
            foreach (IBootstrapHandler handler in Handlers)
                await InitializeHandler(handler);
        }

        protected virtual Task InitializeHandler(IBootstrapHandler task)
        {
            return task.HandleAsync();
        }
    }
}
