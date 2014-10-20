using Neptuo.Bootstrap.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Constraints
{
    public class IgnoreAutomaticConstraintAttribute : ConstraintAttribute, IBootstrapConstraint
    {
        public bool Satisfies(IBootstrapConstraintContext context)
        {
            return !(context.Bootstrapper is AutomaticBootstrapper);
        }
    }
}
