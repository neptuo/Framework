using Neptuo.Bootstrap.Constraints;
using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Constraints
{
    /// <summary>
    /// Constraint, where is not satified if used with <see cref="AutomaticBootstrapper"/>.
    /// </summary>
    public class IgnoreAutomaticConstraintAttribute : ConstraintAttribute, IBootstrapConstraint
    {
        public bool IsSatisfied(IBootstrapHandler task, IBootstrapConstraintContext context)
        {
            return !(context.Bootstrapper is AutomaticBootstrapper);
        }
    }
}
