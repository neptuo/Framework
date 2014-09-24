using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// Environment container.
    /// </summary>
    public class EngineEnvironment
    {
        /// <summary>
        /// Internal storage.
        /// </summary>
        private Dictionary<Type, Dictionary<string, object>> storage = new Dictionary<Type, Dictionary<string, object>>();

        /// <summary>
        /// Registers <paramref name="service"/> (with optional <paramref name="name"/>) into environment.
        /// </summary>
        /// <typeparam name="T">Type of service.</typeparam>
        /// <param name="service">Service to register.</param>
        /// <param name="name">Optional service name (for registering more services of the same type.</param>
        /// <returns>Self (for fluency).</returns>
        public EngineEnvironment Use<T>(T service, string name = null)
        {
            Guard.NotNull(service, "instance");

            if (name == null)
                name = String.Empty;

            Type serviceType = typeof(T);
            Dictionary<string, object> innerStorage;
            if(!storage.TryGetValue(serviceType, out innerStorage))
                innerStorage = storage[serviceType] = new Dictionary<string, object>();

            innerStorage[name] = service;
            return this;
        }

        /// <summary>
        /// Tries to retrieve service of type <typeparamref name="T"/> (with optional <paramref name="name"/>).
        /// </summary>
        /// <typeparam name="T">Type of service.</typeparam>
        /// <param name="name">Optional service name (for registering more services of the same type.</param>
        /// <returns>Retrieved service.</returns>
        public T With<T>(string name = null)
        {
            if (name == null)
                name = String.Empty;
            
            Type serviceType = typeof(T);
            Dictionary<string, object> innerStorage;
            if (!storage.TryGetValue(serviceType, out innerStorage))
                throw new InvalidOperationException(String.Format("Service of type '{0}' is not registered.", serviceType.FullName));

            object service;
            if(!innerStorage.TryGetValue(name, out service))
                throw new InvalidOperationException(String.Format("Service of type '{0}' is not registered under name '{1}'.", serviceType.FullName, name));

            return (T)service;
        }

        /// <summary>
        /// Returns <c>true</c> if there is registered service of type <typeparamref name="T"/> with optional name <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="T">Type of service.</typeparam>
        /// <param name="name">Optional service name (for registering more services of the same type.</param>
        /// <returns><c>true</c> if such a service is registered; <c>false</c> otherwise.</returns>
        public bool Is<T>(string name = null)
        {
            if (name == null)
                name = String.Empty;
            
            Type serviceType = typeof(T);
            Dictionary<string, object> innerStorage;
            if (!storage.TryGetValue(serviceType, out innerStorage))
                return false;

            object service;
            if (!innerStorage.TryGetValue(name, out service))
                return false;

            return true;
        }
    }
}
