using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ObjectBuilder.Client
{
    public class GetterLifetimeManager : IDependencyLifetime
    {
        private Func<object> getter;

        public GetterLifetimeManager(Func<object> getter)
        {
            if (getter == null)
                throw new ArgumentNullException("getter");

            this.getter = getter;
        }

        public object GetValue()
        {
            return getter();
        }

        public void RemoveValue()
        {
            getter = () => null;
        }

        public void SetValue(object newValue)
        {
            getter = () => newValue;
        }
    }
}
