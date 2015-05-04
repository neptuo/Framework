using Neptuo.ComponentModel;
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
        {
            Ensure.NotNull(model, "model");
            Model = model;
            ModelType = model.GetType();
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

                propertyInfo.SetValue(Model, value);
                return true;
            }

            return false;
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
        public ReflectionModelValueProvider(object model)
            : base(model)
        { }
    }
}
