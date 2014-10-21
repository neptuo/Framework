using Neptuo.Bootstrap.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    public class HierarchicalContext
    {
        public Func<Type, IBootstrapTask> Activator { get; private set; }
        public IBootstrapConstraintProvider ConstraintProvider { get; private set; }

        public HierarchicalContext(Func<Type, IBootstrapTask> activator, IBootstrapConstraintProvider constraintProvider)
        {
            Guard.NotNull(activator, "activator");
            Guard.NotNull(constraintProvider, "constraintProvider");
            Activator = activator;
            ConstraintProvider = constraintProvider;
        }
    }
}
