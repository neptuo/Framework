using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Constraints
{
    internal class DefaultBootstrapConstraintContext : IBootstrapConstraintContext
    {
        public IBootstrapper Bootstrapper { get; private set; }

        public DefaultBootstrapConstraintContext(IBootstrapper bootstrapper)
        {
            Bootstrapper = bootstrapper;
        }
    }
}
