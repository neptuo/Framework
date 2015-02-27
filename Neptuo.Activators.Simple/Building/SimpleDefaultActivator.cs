using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Building
{
    public class SimpleDefaultActivator : IActivator<object, IDependencyActivatorContext>
    {
        public object Create(IDependencyActivatorContext context)
        {
            if (!IsInstantiable(context.ServiceType))
                return null;

            return Activator.CreateInstance(context.ServiceType);
        }

        private bool IsInstantiable(Type type)
        {
            if (type.IsInterface)
                return false;

            if (type.IsAbstract)
                return false;

            return true;
        }
    }
}
