using Neptuo.Bootstrap.Constraints;
using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    [IgnoreAutomaticConstraint]
    internal class ProxyBootstrapTask : IBootstrapHandler
    {
        private Func<IBootstrapHandler> factory;

        public ProxyBootstrapTask(Func<IBootstrapHandler> factory)
        {
            Ensure.NotNull(factory, "factory");
            this.factory = factory;
        }

        public void Initialize()
        {
            IBootstrapHandler task = factory();
            if (task != null)
                task.Initialize();
        }
    }
}
