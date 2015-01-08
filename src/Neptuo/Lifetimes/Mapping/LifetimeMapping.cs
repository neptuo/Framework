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

        internal LifetimeMapping(Dictionary<Type, ILifetimeMapper<TBaseLifetimeManager>> registry)
        {
            Registry = new Dictionary<Type, ILifetimeMapper<TBaseLifetimeManager>>(registry);
        }

        public LifetimeMapping<TBaseLifetimeManager> Map(Type lifetimeType, ILifetimeMapper<TBaseLifetimeManager> mapper)
        {
            Guard.NotNull(lifetimeType, "lifetimeType");
            Guard.NotNull(mapper, "mapper");
            Registry[lifetimeType] = mapper;
            return this;
        }

        public TBaseLifetimeManager Resolve(object lifetime)
        {
            Guard.NotNull(lifetime, "lifetime");

            Type lifetimeType = lifetime.GetType();
            ILifetimeMapper<TBaseLifetimeManager> mapper;
            if (!Registry.TryGetValue(lifetimeType, out mapper))
                throw Guard.Exception.ArgumentOutOfRange("lifetime", "Unregistered lifetime '{0}'.", lifetimeType.FullName);

            return mapper.Map(lifetime);
        }

        public LifetimeMapping<TBaseLifetimeManager> CreateChildMapping()
        {
            return new LifetimeMapping<TBaseLifetimeManager>(Registry);
        }
    }
}
