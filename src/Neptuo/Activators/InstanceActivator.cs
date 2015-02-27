using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Singleton activator with support for creating singleton from function (on first call).
    /// </summary>
    /// <typeparam name="T">Type of service to create.</typeparam>
    public class InstanceActivator<T> : IActivator<T>
    {
        private T instance;
        private readonly Func<T> instanceGetter;

        /// <summary>
        /// Create new instance from already created singleton object.
        /// </summary>
        /// <param name="instance">Singleton object.</param>
        public InstanceActivator(T instance)
        {
            Guard.NotNull(instance, "instance");
            this.instance = instance;
        }

        /// <summary>
        /// Creates new instance from <paramref name="innerGetter"/>.
        /// </summary>
        /// <param name="instanceGetter">Function to access singleton object.</param>
        public InstanceActivator(Func<T> instanceGetter)
        {
            Guard.NotNull(instanceGetter, "instanceGetter");
            this.instanceGetter = instanceGetter;
        }

        public T Create()
        {
            if (instance == null)
            {
                lock (instanceGetter)
                {
                    if (instance == null)
                        instance = instanceGetter();
                }
            }

            return instance;
        }
    }
}
