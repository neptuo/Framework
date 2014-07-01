using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Lifetimes
{
    public class HierarchicalLifetime<T>
    {
        public Action<T> Initialize { get; private set; }
        
        public HierarchicalLifetime()
        { }

        public HierarchicalLifetime(Action<T> initialize)
        {
            Guard.NotNull(initialize, "initialize");
            this.Initialize = initialize;
        }
    }

    public class HierarchicalLifetime : HierarchicalLifetime<object>
    {
        public HierarchicalLifetime()
        { }

        public HierarchicalLifetime(Action<object> initialize)
            : base(initialize)
        { }
    }
}
