using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    public class SingletonLifetimeManager : ContainerControlledLifetimeManager
    {
        public SingletonLifetimeManager()
        { }

        public SingletonLifetimeManager(T instance)
        {
            SetValue(instance);
        }
    }
}
