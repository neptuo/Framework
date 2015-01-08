using Neptuo.Lifetimes;
using Neptuo.Lifetimes.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ObjectBuilder.Lifetimes.Mapping
{
    public class SingletonLifetimeMapper : ILifetimeMapper<IDependencyLifetime>
    {
        public IDependencyLifetime Map(object lifetime)
        {
            SingletonLifetime singletionLifetime = lifetime as SingletonLifetime;
            Guard.NotNull(singletionLifetime, "lifetime");

            return new SingletonLifetimeManager(singletionLifetime.Instance);
        }
    }
}
