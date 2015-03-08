using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public interface IConfiguration
    {
        void MapProperty<T>();
        T GetProperty<T>() where T : IConfigurationProperty, new();
    }

    #region Implementation

    public class ConfigurationBase : IConfiguration
    {
        private IConfigurationScopeRegistry scopeRegistry;
        private IConfigurationScope defaultScope;
        private HashSet<Type> properties;

        public ConfigurationBase(IConfigurationScopeRegistry scopeRegistry, IConfigurationScope defaultScope)
        {
            Ensure.NotNull(scopeRegistry, "scopeRegistry");
            Ensure.NotNull(defaultScope, "defaultScope");
            this.scopeRegistry = scopeRegistry;
            this.defaultScope = defaultScope;
            this.properties = new HashSet<Type>();
        }

        public void MapProperty<T>()
        {
            properties.Add(typeof(T));
        }

        public T GetProperty<T>() where T : IConfigurationProperty, new()
        {
            if (properties.Contains(typeof(T)))
            {
                IConfigurationProperty property = new T();
                IUpdateableConfigurationProperty updateable = property as IUpdateableConfigurationProperty;
                if (updateable != null)
                {
                    IConfigurationScope scope = null;
                    if (property.ScopeName == null || !scopeRegistry.TryGet(property.ScopeName, out scope))
                        scope = defaultScope;

                    IConfigurationStorage storage = scope.GetStorage();
                    
                    string propertyValue;
                    if (storage.TryGetValue(property.Name, out propertyValue))
                        updateable.SetValue(propertyValue);
                }
                return (T)property;
            }
            return new T();
        }
    }

    



    #endregion

}
