using Neptuo.Bootstrap.Constraints;
using Neptuo.Bootstrap.Dependencies;
using Neptuo.Bootstrap.Dependencies.Providers;
using Neptuo.Bootstrap.Dependencies.Providers.Exporters;
using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    public class HierarchicalContext
    {
        public Func<Type, IBootstrapHandler> Activator { get; private set; }
        public IBootstrapConstraintProvider ConstraintProvider { get; private set; }
        public IDependencyDescriptorProvider DescriptorProvider { get; private set; }
        public IDependencyExporter DependencyExporter { get; private set; }

        public HierarchicalContext(Func<Type, IBootstrapHandler> activator, IBootstrapConstraintProvider constraintProvider, IDependencyDescriptorProvider descriptorProvider, IDependencyExporter dependencyExporter)
        {
            Ensure.NotNull(activator, "activator");
            Ensure.NotNull(constraintProvider, "constraintProvider");
            Ensure.NotNull(descriptorProvider, "dependencyProvider");
            Ensure.NotNull(dependencyExporter, "dependencyExporter");
            Activator = activator;
            ConstraintProvider = constraintProvider;
            DescriptorProvider = descriptorProvider;
            DependencyExporter = dependencyExporter;
        }
    }
}
