using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies
{
    public class PropertyTaskDependency : ITaskDependency
    {
        public Type TargetType { get; private set; }
        public PropertyInfo TargetProperty { get; private set; }

        public PropertyTaskDependency(Type targetType, PropertyInfo targetProperty)
        {
            Guard.NotNull(targetType, "targetType");
            Guard.NotNull(targetProperty, "targetProperty");
            TargetType = targetType;
            TargetProperty = targetProperty;
        }
    }
}
