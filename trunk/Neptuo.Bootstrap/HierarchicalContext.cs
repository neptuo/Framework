using Neptuo.Bootstrap.Constraints;
using Neptuo.Bootstrap.Dependencies;
using Neptuo.Bootstrap.Dependencies.Providers;
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
        public ITaskDependencyProvider DependencyProvider { get; private set; }
        public ITaskDependencyExporter DependencyExporter { get; private set; }

        public HierarchicalContext(Func<Type, IBootstrapTask> activator, IBootstrapConstraintProvider constraintProvider, ITaskDependencyProvider dependencyProvider, ITaskDependencyExporter dependencyExporter)
        {
            Guard.NotNull(activator, "activator");
            Guard.NotNull(constraintProvider, "constraintProvider");
            Guard.NotNull(dependencyProvider, "dependencyProvider");
            Guard.NotNull(dependencyExporter, "dependencyExporter");
            Activator = activator;
            ConstraintProvider = constraintProvider;
            DependencyProvider = dependencyProvider;
            DependencyExporter = dependencyExporter;
        }
    }
}
