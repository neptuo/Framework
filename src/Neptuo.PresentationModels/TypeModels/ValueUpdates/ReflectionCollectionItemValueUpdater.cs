using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.ValueUpdates
{
    /// <summary>
    /// When value of property is of type <see cref="Collection{T}"/> and new value is of type <see cref="IEnumerable{T}"/> or <c>null</c>,
    /// clears collection and adds (if possible) new items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReflectionCollectionItemValueUpdater<T> : IReflectionValueUpdater
    {
        public bool TryUpdate(object instance, PropertyInfo propertyInfo, object newValue)
        {
            ICollection<T> collection = propertyInfo.GetValue(instance) as ICollection<T>;
            if (collection == null)
                return false;
            
            if (newValue == null)
            {
                collection.Clear();
                return true;
            }

            IEnumerable<T> newValues = newValue as IEnumerable<T>;
            if (newValues == null)
                return false;

            collection.Clear();
            foreach (T item in newValues)
                collection.Add(item);

            return true;
        }
    }
}
