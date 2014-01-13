using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ObjectBuilder.Client
{
    public class TransientLifetimeManager : IDependencyLifetime
    {
        public object GetValue()
        {
            return null;
        }

        public void RemoveValue()
        { }

        public void SetValue(object newValue)
        { }
    }
}
