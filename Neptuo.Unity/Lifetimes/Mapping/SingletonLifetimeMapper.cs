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
    public class SingletonLifetimeMapper : LifetimeMapperBase<LifetimeManager, SingletonLifetime>
    {
        protected override LifetimeManager Map(SingletonLifetime lifetime)
        {
            if (lifetime.Instance != null)
                return new SingletonLifetimeManager(lifetime.Instance);

            return new SingletonLifetimeManager();
        }
    }
}
