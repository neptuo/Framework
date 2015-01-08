using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Unity
{
    public abstract class ContainerLifetimeManagerBase : LifetimeManager
    {
        protected virtual IDictionary Container { get; private set; }
        protected Guid Guid { get; private set; }

        public ContainerLifetimeManagerBase()
        {
            Guid = Guid.NewGuid();
        }

        public ContainerLifetimeManagerBase(IDictionary container)
            : this()
        {
            if (container == null)
                throw new ArgumentNullException("container");

            Container = container;
        }

        public override object GetValue()
        {
            if (Container == null)
                throw new InvalidOperationException("Container is null.");

            if (Container.Contains(Guid))
                return Container[Guid];

            return GetDefaultValue();
        }

        protected virtual object GetDefaultValue()
        {
            return null;
        }

        public override void RemoveValue()
        {
            if (Container == null)
                throw new InvalidOperationException("Container is null.");

            if (Container.Contains(Guid))
                Container.Remove(Guid);
        }

        public override void SetValue(object newValue)
        {
            if (Container == null)
                throw new InvalidOperationException("Container is null.");

            Container[Guid] = newValue;
        }
    }
}
