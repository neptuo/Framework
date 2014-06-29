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
    public class HierarchicalLifetimeMapper : LifetimeMapperBase<HierarchicalLifetimeManager, HierarchicalLifetime>
    {
        protected override HierarchicalLifetimeManager Map(HierarchicalLifetime lifetime)
        {
            return new HierarchicalLifetimeManager();
        }
    }
}
