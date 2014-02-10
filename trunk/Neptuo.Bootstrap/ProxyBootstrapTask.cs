using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    [IgnoreAutomaticConstraint]
    internal class ProxyBootstrapTask : IBootstrapTask
    {
        private Func<IBootstrapTask> factory;

        public ProxyBootstrapTask(Func<IBootstrapTask> factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            this.factory = factory;
        }

        public void Initialize()
        {
            IBootstrapTask task = factory();
            if (task != null)
                task.Initialize();
        }
    }
}
