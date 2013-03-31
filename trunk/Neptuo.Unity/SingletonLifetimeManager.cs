using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Unity
{
    public class SingletonLifetimeManager<T> : ContainerControlledLifetimeManager
    {
        private Action<T> initialize;

        public SingletonLifetimeManager()
        { }

        public SingletonLifetimeManager(T instance)
        {
            SetValue(instance);
        }

        public SingletonLifetimeManager(Action<T> initialize)
        {
            this.initialize = initialize;
        }

        public SingletonLifetimeManager(T instance, Action<T> initialize)
            : this(instance)
        {
            this.initialize = initialize;
        }

        protected override void SynchronizedSetValue(object newValue)
        {
            base.SynchronizedSetValue(newValue);

            if (initialize != null)
                initialize((T)newValue);
        }
    }

    public class SingletonLifetimeManager : SingletonLifetimeManager<object>
    {
        public SingletonLifetimeManager()
        { }

        public SingletonLifetimeManager(object instance)
            : base(instance)
        { }

        public SingletonLifetimeManager(Action<object> initialize)
            : base(initialize)
        { }

        public SingletonLifetimeManager(object instance, Action<object> initialize)
            : base(instance, initialize)
        { }
    }
}
