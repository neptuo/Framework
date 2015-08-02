using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Providers
{
    /// <summary>
    /// Base implementation using list of providers.
    /// </summary>
    public class BehaviorProviderCollection : IBehaviorProvider
    {
        private readonly object storageLock = new object();
        private readonly List<IBehaviorProvider> storage = new List<IBehaviorProvider>();

        /// <summary>
        /// Adds new provider to the start of the collection.
        /// </summary>
        /// <param name="provider">New behavior provider.</param>
        /// <returns>Self (for fluency).</returns>
        public BehaviorProviderCollection Add(IBehaviorProvider provider)
        {
            Ensure.NotNull(provider, "provider");

            lock (storageLock)
                storage.Insert(0, provider);

            return this;
        }

        /// <summary>
        /// Enumerates all registered behavior providers.
        /// </summary>
        /// <returns>Enumeration of all registered behavior providers.</returns>
        public IEnumerable<IBehaviorProvider> EnumerateProviders()
        {
            return storage;
        }

        /// <summary>
        /// Gets registered behavior types for <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Handler type.</param>
        /// <returns>Registered behavior types for <paramref name="handlerType"/>.</returns>
        public IEnumerable<Type> GetBehaviors(Type handlerType)
        {
            IEnumerable<Type> result = Enumerable.Empty<Type>();
            foreach (IBehaviorProvider provider in storage)
                result = Enumerable.Concat(result, provider.GetBehaviors(handlerType));

            return result;
        }
    }
}
