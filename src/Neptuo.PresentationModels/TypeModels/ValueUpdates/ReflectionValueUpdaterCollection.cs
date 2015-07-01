using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.ValueUpdates
{
    /// <summary>
    /// Collection implementation of <see cref="IReflectionValueUpdater"/>.
    /// Provides ability to delegate value update based on property type.
    /// </summary>
    public class ReflectionValueUpdaterCollection : IReflectionValueUpdater
    {
        private readonly Dictionary<Type, IReflectionValueUpdater> storage = new Dictionary<Type, IReflectionValueUpdater>();

        /// <summary>
        /// Adds mapping of property of type <paramref name="propertyType"/> to <paramref name="updater"/>.
        /// </summary>
        /// <param name="propertType">Type of property to be updated by <paramref name="updater"/>.</param>
        /// <param name="updater">Updater to be used at properties of type <paramref name="propertyType"/>.</param>
        /// <returns>Self (for fluency).</returns>
        public ReflectionValueUpdaterCollection Add(Type propertType, IReflectionValueUpdater updater)
        {
            Ensure.NotNull(propertType, "propertType");
            Ensure.NotNull(updater, "updater");
            storage[propertType] = updater;
            return this;
        }

        public bool TryUpdate(object instance, PropertyInfo propertyInfo, object newValue)
        {
            Type propertyType = propertyInfo.GetType();
            IReflectionValueUpdater updater;
            if (storage.TryGetValue(propertyType, out updater))
                return updater.TryUpdate(instance, propertyInfo, newValue);

            return false;
        }
    }
}