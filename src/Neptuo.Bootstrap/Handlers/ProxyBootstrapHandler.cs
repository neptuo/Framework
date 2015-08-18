using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Handlers
{
    [IgnoreAutomatic]
    internal class ProxyBootstrapHandler : IBootstrapHandler
    {
        private Func<IBootstrapHandler> factory;

        public ProxyBootstrapHandler(Func<IBootstrapHandler> factory)
        {
            Ensure.NotNull(factory, "factory");
            this.factory = factory;
        }

        public Task HandleAsync()
        {
            IBootstrapHandler handler = factory();
            if (handler != null)
                return handler.HandleAsync();

            return Task.FromResult(false);
        }
    }
}
