using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.ValueUpdates
{
    /// <summary>
    /// Empty implementation of <see cref="IReflectionValueUpdater"/>.
    /// Always returns <c>false</c>.
    /// </summary>
    public class ReflectionEmptyValueUpdater : IReflectionValueUpdater
    {
        public bool TryUpdate(object instance, PropertyInfo propertyInfo, object newValue)
        {
            return false;
        }
    }
}
