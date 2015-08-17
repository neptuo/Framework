using Neptuo.Bootstrap.Dependencies.Providers.Targets;
using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies.Providers
{
    public class PropertyExportDescriptor : IDependencyExportDescriptor
    {
        public IDependencyTarget Target { get; private set; }
        public PropertyInfo TargetProperty { get; private set; }

        public PropertyExportDescriptor(IDependencyTarget target, PropertyInfo targetProperty)
        {
            Ensure.NotNull(target, "target");
            Ensure.NotNull(targetProperty, "targetProperty");
            Target = target;
            TargetProperty = targetProperty;
        }

        public object GetValue(IBootstrapHandler task)
        {
            return TargetProperty.GetValue(task);
        }
    }
}
