using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Configuration
{
    public interface IConfigurationProperty
    {
        string Name { get; }
        Type PropertyType { get; }
        string ScopeName { get; }
    }

    public interface IConfigurationProperty<T> : IConfigurationProperty
    {
        T Value { get; }
    }
}
