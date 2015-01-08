using Microsoft.Practices.Unity;
using Neptuo.Lifetimes.Mapping;
using Neptuo.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Unity.Web.Lifetimes.Mapping
{
    public class PerRequestLifetimeMapper : LifetimeMapperBase<LifetimeManager, PerRequestLifetime>
    {
        protected override LifetimeManager Map(PerRequestLifetime lifetime)
        {
            return new PerRequestLifetimeManager();
        }
    }
}
