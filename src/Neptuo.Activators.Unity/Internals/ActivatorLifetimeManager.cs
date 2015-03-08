using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class ActivatorLifetimeManager : LifetimeManager
    {
        private readonly IActivator<object> activator;
        private readonly LifetimeManager innerLifetime;

        public ActivatorLifetimeManager(IActivator<object> activator, LifetimeManager innerLifetime)
        {
            Guard.NotNull(activator, "activator");
            Guard.NotNull(innerLifetime, "innerLifetime");
            this.activator = activator;
            this.innerLifetime = innerLifetime;
        }

        public override object GetValue()
        {
            object value = innerLifetime.GetValue();
            if (value == null)
            {
                value = activator.Create();
                SetValue(value);
            }

            return value;
        }

        public override void RemoveValue()
        {
            innerLifetime.RemoveValue();
        }

        public override void SetValue(object newValue)
        {
            innerLifetime.SetValue(newValue);
        }
    }
}
