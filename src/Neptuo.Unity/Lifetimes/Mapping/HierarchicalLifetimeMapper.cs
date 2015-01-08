using Microsoft.Practices.Unity;
using Neptuo.Lifetimes;
using Neptuo.Lifetimes.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Unity.Lifetimes.Mapping
{
    public class HierarchicalLifetimeMapper<T> : LifetimeMapperBase<LifetimeManager, HierarchicalLifetime<T>>
    {
        protected override LifetimeManager Map(HierarchicalLifetime<T> lifetime)
        {
            if (lifetime.Initialize != null)
                return new HierarchicalLifetimeManager<T>(lifetime.Initialize);

            return new HierarchicalLifetimeManager<T>();
        }
    }

    public class HierarchicalLifetimeMapper : HierarchicalLifetimeMapper<object>
    { }
}
