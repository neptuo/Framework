using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Lifetimes.Mapping
{
    public class LifetimeMapping<TBaseLifetimeManager>
    {
        protected Dictionary<Type, ILifetimeMapper<TBaseLifetimeManager>> Registry { get; private set; }

        public LifetimeMapping()
        {
            Registry = new Dictionary<Type, ILifetimeMapper<TBaseLifetimeManager>>();
        }

        public LifetimeMapping<TBaseLifetimeManager> Map(Type lifetimeType, ILifetimeMapper<TBaseLifetimeManager> mapper)
        {
            if (lifetimeType == null)
                throw new ArgumentNullException("lifetimeType");

            if (mapper == null)
                throw new ArgumentNullException("mapper");

            Registry[lifetimeType] = mapper;
            return this;
        }

        public TBaseLifetimeManager Resolve(object lifetime)
        {
            if (lifetime == null)
                throw new ArgumentNullException("lifetime");

            Type lifetimeType = lifetime.GetType();
            ILifetimeMapper<TBaseLifetimeManager> mapper;
            if (!Registry.TryGetValue(lifetimeType, out mapper))
                throw new ArgumentOutOfRangeException("lifetime", "Unregistered lifetime.");

            return mapper.Map(lifetime);
        }
    }
}
