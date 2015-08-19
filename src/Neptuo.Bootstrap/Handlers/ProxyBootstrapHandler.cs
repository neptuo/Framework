using Neptuo.Bootstrap.Handlers.Behaviors;
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

        public void Handle()
        {
            IBootstrapHandler handler = factory();
            if (handler != null)
                handler.Handle();
        }
    }
}
