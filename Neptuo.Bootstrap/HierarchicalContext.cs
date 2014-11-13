using Neptuo.Bootstrap.Constraints;
using Neptuo.Bootstrap.Dependencies;
using Neptuo.Bootstrap.Dependencies.Providers;
using Neptuo.Bootstrap.Dependencies.Providers.Exporters;
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
        public IDependencyDescriptorProvider DescriptorProvider { get; private set; }
        public IDependencyExporter DependencyExporter { get; private set; }

        public HierarchicalContext(Func<Type, IBootstrapTask> activator, IBootstrapConstraintProvider constraintProvider, IDependencyDescriptorProvider descriptorProvider, IDependencyExporter dependencyExporter)
        {
            Guard.NotNull(activator, "activator");
            Guard.NotNull(constraintProvider, "constraintProvider");
            Guard.NotNull(descriptorProvider, "dependencyProvider");
            Guard.NotNull(dependencyExporter, "dependencyExporter");
            Activator = activator;
            ConstraintProvider = constraintProvider;
            DescriptorProvider = descriptorProvider;
            DependencyExporter = dependencyExporter;
        }
    }
}
