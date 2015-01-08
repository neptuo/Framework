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
    public class GetterLifetimeMapper : LifetimeMapperBase<LifetimeManager, GetterLifetime>
    {
        protected override LifetimeManager Map(GetterLifetime lifetime)
        {
            return new GetterLifetimeManager(lifetime.Factory);
        }
    }
}
