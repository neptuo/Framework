using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Lifetimes.Mapping
{
    public interface ILifetimeMapping<TBaseLifetimeManager>
    {
        void Map(Type lifetimeType, ILifetimeMapper<TBaseLifetimeManager> mapper);
    }
}
