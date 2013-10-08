using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public class ReflectionModelValueProvider : IModelValueProvider
    {
        public Type ModelType { get; private set; }
        public object Model { get; private set; }
        private Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>();

        public ReflectionModelValueProvider(object model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            Model = model;
            ModelType = model.GetType();
        }

        public virtual object GetValue(string identifier)
        {
            return GetPropertyInfo(identifier).GetValue(Model);
        }

        public virtual void SetValue(string identifier, object value)
        {
            GetPropertyInfo(identifier).SetValue(Model, value);
        }

        protected PropertyInfo GetPropertyInfo(string identifier)
        {
            PropertyInfo propertyInfo;
            if (!properties.TryGetValue(identifier, out propertyInfo))
            {
                propertyInfo = ModelType.GetProperty(identifier);
                if (propertyInfo == null)
                    throw new ArgumentOutOfRangeException("identifier", String.Format("'{0}' doesn't contain property named '{1}'.", ModelType.FullName, identifier));

                properties[identifier] = propertyInfo;
            }
            return propertyInfo;
        }
    }
}
