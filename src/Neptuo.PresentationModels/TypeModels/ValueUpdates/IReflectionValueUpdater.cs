using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.ValueUpdates
{
    /// <summary>
    /// Property value updater.
    /// Used when only update of current value (e.g.: properties of property value) should be executed.
    /// </summary>
    public interface IReflectionValueUpdater
    {
        /// <summary>
        /// Tries to update value of property <paramref name="propertyInfo"/> at <paramref name="instance"/>
        /// with values of <paramref name="newValue"/>.
        /// </summary>
        /// <param name="instance">Target instance, where property should be updated.</param>
        /// <param name="propertyInfo">Reflection property info.</param>
        /// <param name="newValue">New value that should be used.</param>
        /// <returns><c>true</c> when update was possible; <c>false</c> otherwise.</returns>
        bool TryUpdate(object instance, PropertyInfo propertyInfo, object newValue);
    }
}
