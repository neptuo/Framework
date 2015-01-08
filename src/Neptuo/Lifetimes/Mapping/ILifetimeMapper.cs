using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Lifetimes.Mapping
{
    public interface ILifetimeMapper<TBaseLifetimeManager>
    {
        TBaseLifetimeManager Map(object lifetime);
    }
}
