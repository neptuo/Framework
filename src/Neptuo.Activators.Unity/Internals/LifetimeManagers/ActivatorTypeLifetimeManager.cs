using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

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

        public override object GetValue(ILifetimeContainer container)
        {
            object value = innerLifetime.GetValue();
            if (value == null)
            {
                value = ((IFactory<object>)unityContainer.Resolve(activatorType)).Create();
                SetValue(value, container);
            }

            return value;
        }

        public override void RemoveValue(ILifetimeContainer container)
            => innerLifetime.RemoveValue();

        public override void SetValue(object newValue, ILifetimeContainer container)
            => innerLifetime.SetValue(newValue);

        protected override LifetimeManager OnCreateLifetimeManager()
            => new ActivatorTypeLifetimeManager(unityContainer, activatorType, innerLifetime);
    }
}
