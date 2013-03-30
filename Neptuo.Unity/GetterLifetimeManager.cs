using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Unity
{
    public class GetterLifetimeManager : TransientLifetimeManager
    {
        private Func<object> factory;

        public GetterLifetimeManager(Func<object> factory)
        {
            this.factory = factory;
        }

        public override object GetValue()
        {
            return factory();
        }
    }
}
