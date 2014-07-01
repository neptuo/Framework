using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Unity
{
    public class HierarchicalLifetimeManager<T> : Microsoft.Practices.Unity.HierarchicalLifetimeManager
    {
        private Action<T> initialize;

        public HierarchicalLifetimeManager()
        { }

        public HierarchicalLifetimeManager(Action<T> initialize)
        {
            Guard.NotNull(initialize, "initialize");
            this.initialize = initialize;
        }

        protected override void SynchronizedSetValue(object newValue)
        {
            base.SynchronizedSetValue(newValue);

            if (initialize != null)
                initialize((T)newValue);
        }
    }

    public class HierarchicalLifetimeManager : HierarchicalLifetimeManager<object>
    {
        public HierarchicalLifetimeManager()
        { }

        public HierarchicalLifetimeManager(Action<object> initialize)
            : base(initialize)
        { }
    }
}
