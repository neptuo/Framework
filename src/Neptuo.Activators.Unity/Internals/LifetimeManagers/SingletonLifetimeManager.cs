using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace Neptuo.Activators.Internals.LifetimeManagers
{
    public class SingletonLifetimeManager : ContainerControlledLifetimeManager
    {
        private object value;

        public SingletonLifetimeManager()
        { }

        public SingletonLifetimeManager(object instance)
        {
            SetValue(instance);
        }

        protected override object SynchronizedGetValue(ILifetimeContainer container)
            => value;

        protected override void SynchronizedSetValue(object newValue, ILifetimeContainer container)
            => value = newValue;
    }
}
