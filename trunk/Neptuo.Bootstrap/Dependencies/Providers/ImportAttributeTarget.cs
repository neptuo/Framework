using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies.Providers
{
    public class ImportAttributeTarget : ITaskDependencyTarget
    {
        public Type TargetType { get; private set; }
        public string Name { get; private set; }

        public ImportAttributeTarget(PropertyInfo propertyInfo, ImportAttribute attribute)
        {
            Guard.NotNull(propertyInfo, "propertyInfo");
            Guard.NotNull(attribute, "attribute");
            TargetType = propertyInfo.PropertyType;
            Name = attribute.Name;
        }

        public bool Equals(ITaskDependencyTarget other)
        {
            ImportAttributeTarget target = other as ImportAttributeTarget;
            if (target == null)
                return false;

            return TargetType == target.TargetType && Name == target.Name;
        }
    }
}
