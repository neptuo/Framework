using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Lifetime;

namespace Neptuo.Activators.Internals.LifetimeManagers
{
    internal class ActivatorLifetimeManager : LifetimeManager
    {
        private readonly IFactory<object> activator;
        private readonly LifetimeManager innerLifetime;

        public ActivatorLifetimeManager(IFactory<object> activator, LifetimeManager innerLifetime)
        {
            Ensure.NotNull(activator, "activator");
            Ensure.NotNull(innerLifetime, "innerLifetime");
            this.activator = activator;
            this.innerLifetime = innerLifetime;
        }

        public override object GetValue(ILifetimeContainer container)
        {
            object value = innerLifetime.GetValue();
            if (value == null)
            {
                value = activator.Create();
                SetValue(value, container);
            }

            return value;
        }

        public override void RemoveValue(ILifetimeContainer container)
            => innerLifetime.RemoveValue();

        public override void SetValue(object newValue, ILifetimeContainer container)
            => innerLifetime.SetValue(newValue);

        protected override LifetimeManager OnCreateLifetimeManager()
            => new ActivatorLifetimeManager(activator, innerLifetime);
    }
}
