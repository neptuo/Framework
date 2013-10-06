using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Lifetimes.Mapping
{
    public static class LifetimeMappingExtensions
    {
        public static void Map<TBaseLifetimeManager, TLifetime>(this ILifetimeMapping<TBaseLifetimeManager> mapping, LifetimeMapperBase<TBaseLifetimeManager, TLifetime> mapper)
        {
            mapping.Map(typeof(TLifetime), mapper);
        }
    }
}
