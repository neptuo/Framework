using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ObjectBuilder.Client
{
    public class SingletonLifetimeManager : IDependencyLifetime
    {
        private object value;

        public SingletonLifetimeManager(object value)
        {
            this.value = value;
        }

        public object GetValue()
        {
            return value;
        }

        public void RemoveValue()
        { }

        public void SetValue(object newValue)
        { }
    }
}
