using Neptuo.ComponentModel;
using Neptuo.PresentationModels.TypeModels.ValueUpdates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    /// <summary>
    /// Reflection based implementation of <see cref="IModelValueProvider"/> that operates over single object instance.
    /// </summary>
    /// <typeparam name="TModel">Type of model.</typeparam>
    public class ReflectionModelValueProvider<TModel> : DisposableBase, IModelValueProvider
    {
        private readonly Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>();

        protected IReflectionValueUpdater ValueUpdater { get; private set; }

        /// <summary>
        /// Type of model.
        /// </summary>
        public Type ModelType { get; private set; }

        /// <summary>
        /// Instance of model.
        /// </summary>
        public TModel Model { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="model">Instance of model. Can't be <c>null</c>.</param>
        public ReflectionModelValueProvider(TModel model)
            : this(model, new EmptyReflectionValueUpdater())
        { }

        /// <summary>
        /// Creates new instance with support for updating values of readonly properties.
        /// </summary>
        /// <param name="model">Instance of model. Can't be <c>null</c>.</param>
        /// <param name="valueUpdater">Readonly property value updater. Can't be <c>null</c>.</param>
        public ReflectionModelValueProvider(TModel model, IReflectionValueUpdater valueUpdater)
        {
            Ensure.NotNull(model, "model");
            Ensure.NotNull(valueUpdater, "valueUpdater");
            Model = model;
            ModelType = model.GetType();
            ValueUpdater = valueUpdater;
        }

        public bool TryGetValue(string identifier, out object value)
        {
            Ensure.NotNullOrEmpty(identifier, "identifier");
            
            PropertyInfo propertyInfo;
            if (TryGetPropertyInfo(identifier, out propertyInfo))
            {
                value = propertyInfo.GetValue(Model);
                return true;
            }

            value = null;
            return false;
        }

        public virtual bool TrySetValue(string identifier, object value)
        {
            Ensure.NotNullOrEmpty(identifier, "identifier");

            PropertyInfo propertyInfo;
            if (TryGetPropertyInfo(identifier, out propertyInfo))
            {
                if (value != null && !propertyInfo.PropertyType.IsAssignableFrom(value.GetType()))
                {
                    TypeConverter typeConverter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
                    if (typeConverter != null)
                        value = typeConverter.ConvertFrom(value);
                }

                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(Model, value);
                    return true;
                }
                
                return TrySetValueOnReadOnlyProperty(identifier, propertyInfo, value);
            }

            return false;
        }

        /// <summary>
        /// Called when <paramref name="propertyInfo"/> is readonly and caller tries to set value to it.
        /// </summary>
        /// <param name="identifier">Field identifier.</param>
        /// <param name="propertyInfo">Reflection property info.</param>
        /// <param name="value">New value.</param>
        /// <returns><c>true</c> if setting value was possible; <c>false</c> otherwise.</returns>
        protected virtual bool TrySetValueOnReadOnlyProperty(string identifier, PropertyInfo propertyInfo, object value)
        {
            return ValueUpdater.TryUpdate(Model, propertyInfo, value);
        }

        /// <summary>
        /// Tries to get property info for identifier <paramref name="identier"/>.
        /// </summary>
        /// <param name="identifier">Property identifier.</param>
        /// <param name="propertyInfo">Target property info.</param>
        /// <returns><c>true</c> if such property exists; <c>false</c> otherwise.</returns>
        protected virtual bool TryGetPropertyInfo(string identifier, out PropertyInfo propertyInfo)
        {
            if (properties.TryGetValue(identifier, out propertyInfo))
                return true;

            propertyInfo = ModelType.GetProperty(identifier);
            if (propertyInfo != null)
                properties[identifier] = propertyInfo;

            return propertyInfo != null;
        }
    }

    /// <summary>
    /// Reflection based implementation of <see cref="IModelValueProvider"/> that operates over single object instance.
    /// </summary>
    public class ReflectionModelValueProvider : ReflectionModelValueProvider<object>
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="model">Instance of model. Can't be <c>null</c>.</param>
        public ReflectionModelValueProvider(object model)
            : base(model)
        { }

        /// <summary>
        /// Creates new instance with support for updating values of readonly properties.
        /// </summary>
        /// <param name="model">Instance of model. Can't be <c>null</c>.</param>
        /// <param name="valueUpdater">Readonly property value updater. Can't be <c>null</c>.</param>
        public ReflectionModelValueProvider(object model, IReflectionValueUpdater valueUpdater)
            : base(model, valueUpdater)
        { }
    }
}
