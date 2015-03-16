using Neptuo.ComponentModel;
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
        /// Invoked when service was not found.
        /// Takes typeof requested service, should return <c>true</c> to indicate success; otherwise <c>false</c>.
        /// </summary>
        private OutFuncCollection<Type, object, bool> onSearchService = new OutFuncCollection<Type, object, bool>();

        /// <summary>
        /// Registers <paramref name="service"/> (with optional <paramref name="name"/>) into environment.
        /// </summary>
        /// <typeparam name="T">Type of service.</typeparam>
        /// <param name="service">Service to register.</param>
        /// <param name="name">Optional service name (for registering more services of the same type.</param>
        /// <returns>Self (for fluency).</returns>
        public EngineEnvironment Use<T>(T service, string name = null)
        {
            Ensure.NotNull(service, "instance");

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
        /// Retrieve service of type <typeparamref name="T"/> (with optional <paramref name="name"/>) or throws exception.
        /// </summary>
        /// <typeparam name="T">Type of service.</typeparam>
        /// <param name="name">Optional service name (for registering more services of the same type.</param>
        /// <returns>Retrieved service.</returns>
        public T With<T>(string name = null)
        {
            T service;
            if (TryWith(name, out service))
                return service;

            throw Ensure.Exception.InvalidOperation("Service of type '{0}' is not registered under name '{1}'.", typeof(T).FullName, name);
        }

        /// <summary>
        /// Returns <c>true</c> if there is registered service of type <typeparamref name="T"/> with optional name <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="T">Type of service.</typeparam>
        /// <param name="name">Optional service name (for registering more services of the same type.</param>
        /// <returns><c>true</c> if such a service is registered; <c>false</c> otherwise.</returns>
        public bool Has<T>(string name = null)
        {
            T service;
            return TryWith(name, out service);
        }

        /// <summary>
        /// Tries to retrieve service of type <typeparamref name="T"/> (with optional <paramref name="name"/>).
        /// </summary>
        /// <typeparam name="T">Type of service.</typeparam>
        /// <param name="name">Optional service name (for registering more services of the same type.</param>
        /// <param name="service">Retrieved service or <c>null</c>.</param>
        /// <returns><c>true</c> if service of type <typeparamref name="T"/> can be provided; <c>false</c> otherwise..</returns>
        public bool TryWith<T>(string name, out T service)
        {
            if (name == null)
                name = String.Empty;

            Type serviceType = typeof(T);
            object target;
            Dictionary<string, object> innerStorage;
            if (storage.TryGetValue(serviceType, out innerStorage))
            {
                if (innerStorage.TryGetValue(name, out target))
                {
                    service = (T)target;
                    return true;
                }
            }

            // Execute search handlers.
            if (onSearchService != null)
            {
                if (onSearchService.TryExecute(serviceType, out target))
                {
                    if (innerStorage == null)
                        storage[serviceType] = innerStorage = new Dictionary<string, object>();

                    innerStorage[name] = target;
                    service = (T)target;
                    return true;
                }
            }

            service = default(T);
            return false;
        }

        /// <summary>
        /// Adds handler for searching service for.
        /// </summary>
        /// <param name="handler">Search handler to add.</param>
        /// <returns>Self (for fluency).</returns>
        public EngineEnvironment AddSearchHandler(OutFunc<Type, object, bool> handler)
        {
            Ensure.NotNull(handler, "handler");
            onSearchService.Add(handler);
            return this;
        }
    }
}
