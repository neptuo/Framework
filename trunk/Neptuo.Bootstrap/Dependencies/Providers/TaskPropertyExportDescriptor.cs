using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies.Providers
{
    public class TaskPropertyExportDescriptor : ITaskExportDescriptor
    {
        public ITaskDependencyTarget Target { get; private set; }
        public PropertyInfo TargetProperty { get; private set; }

        public TaskPropertyExportDescriptor(ITaskDependencyTarget target, PropertyInfo targetProperty)
        {
            Guard.NotNull(target, "target");
            Guard.NotNull(targetProperty, "targetProperty");
            Target = target;
            TargetProperty = targetProperty;
        }

        public object GetValue(IBootstrapTask task)
        {
            return TargetProperty.GetValue(task);
        }
    }
}
