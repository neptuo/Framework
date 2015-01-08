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
    public class SingletonLifetimeMapper<T> : LifetimeMapperBase<LifetimeManager, SingletonLifetime<T>>
    {
        protected override LifetimeManager Map(SingletonLifetime<T> lifetime)
        {
            if (lifetime.Instance != null)
            {
                if(lifetime.Initialize != null)
                    return new SingletonLifetimeManager<T>(lifetime.Instance, lifetime.Initialize);

                return new SingletonLifetimeManager<T>(lifetime.Instance);
            }

            if(lifetime.Initialize != null)
                return new SingletonLifetimeManager<T>(lifetime.Initialize);

            return new SingletonLifetimeManager<T>();
        }
    }

    public class SingletonLifetimeMapper : SingletonLifetimeMapper<object>
    { }
}
