using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Singleton instance activator.
    /// </summary>
    /// <typeparam name="T">Type of service to wrap.</typeparam>
    public class InstanceActivator<T> : IActivator<T>
    {
        private readonly T instance;

        /// <summary>
        /// Creates new instance for singleton value <paramref name="instance"/>.
        /// </summary>
        /// <param name="instance">Instance to wrap.</param>
        public InstanceActivator(T instance)
        {
            Guard.NotNull(instance, "instance");
            this.instance = instance;
        }

        public T Create()
        {
            return instance;
        }
    }
}
