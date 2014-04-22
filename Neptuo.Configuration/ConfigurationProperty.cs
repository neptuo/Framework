using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public abstract class ConfigurationProperty<T> : IConfigurationProperty<T>, IUpdateableConfigurationProperty
    {
        public string Name { get; protected set; }
        public Type PropertyType { get; protected set; }
        public string ScopeName { get; protected set; }
        public T Value { get; protected set; }

        public ConfigurationProperty(T defaultValue, string scopeName, string name = null)
        {
            PropertyType = typeof(T);
            Value = defaultValue;
            ScopeName = scopeName;
            Name = name ?? GetType().FullName;
        }

        public void SetValue(string value)
        {
            T target;
            if (TryParseValue(value, out target))
                Value = target;
        }

        protected abstract bool TryParseValue(string source, out T target);
    }
}
