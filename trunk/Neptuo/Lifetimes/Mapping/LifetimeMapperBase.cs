using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Lifetimes.Mapping
{
    public abstract class LifetimeMapperBase<TBaseLifetimeManager, TLifetime> : ILifetimeMapper<TBaseLifetimeManager>
    {
        public TBaseLifetimeManager Map(object lifetime)
        {
            return Map((TLifetime)lifetime);
        }

        protected abstract TBaseLifetimeManager Map(TLifetime lifetime);
    }

}
