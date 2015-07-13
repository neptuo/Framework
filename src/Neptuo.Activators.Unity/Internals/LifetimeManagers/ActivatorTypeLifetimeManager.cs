using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals.LifetimeManagers
{
    internal class ActivatorTypeLifetimeManager : LifetimeManager
    {
        private readonly IUnityContainer unityContainer;
        private readonly Type activatorType;
        private readonly LifetimeManager innerLifetime;

        public ActivatorTypeLifetimeManager(IUnityContainer unityContainer, Type activatorType, LifetimeManager innerLifetime)
        {
            Ensure.NotNull(unityContainer, "unityContainer");
            Ensure.NotNull(activatorType, "activatorType");
            Ensure.NotNull(innerLifetime, "innerLifetime");
            this.unityContainer = unityContainer;
            this.activatorType = activatorType;
            this.innerLifetime = innerLifetime;
        }

        public override object GetValue()
        {
            object value = innerLifetime.GetValue();
            if (value == null)
            {
                value = ((IActivator<object>)unityContainer.Resolve(activatorType)).Create();
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
