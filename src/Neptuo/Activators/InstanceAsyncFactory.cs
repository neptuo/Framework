using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// A singleton activator with support for creating singleton from function (on first call).
    /// </summary>
    /// <typeparam name="T">A type of the service to create.</typeparam>
    public class InstanceAsyncFactory<T> : IAsyncFactory<T>
    {
        private T instance;
        private readonly Func<T> instanceGetter;

        /// <summary>
        /// Creates a new instance from already existing instance.
        /// </summary>
        /// <param name="instance">A singleton object.</param>
        public InstanceAsyncFactory(T instance)
        {
            Ensure.NotNull(instance, "instance");
            this.instance = instance;
        }

        /// <summary>
        /// Creates a new instance from <paramref name="instanceGetter"/>.
        /// </summary>
        /// <param name="instanceGetter">The function to access singleton object.</param>
        public InstanceAsyncFactory(Func<T> instanceGetter)
        {
            Ensure.NotNull(instanceGetter, "instanceGetter");
            this.instanceGetter = instanceGetter;
        }

        public Task<T> Create()
        {
            if (instance == null)
            {
                lock (instanceGetter)
                {
                    if (instance == null)
                        instance = instanceGetter();
                }
            }

            return Task.FromResult(instance);
        }
    }
}
