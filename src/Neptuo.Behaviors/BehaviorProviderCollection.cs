using Neptuo.ComponentModel.Behaviors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors
{
    /// <summary>
    /// Base implementation using list of providers.
    /// </summary>
    public class BehaviorProviderCollection : IBehaviorProvider
    {
        /// <summary>
        /// List of registered behavior providers.
        /// </summary>
        private List<IBehaviorProvider> providers = new List<IBehaviorProvider>();

        /// <summary>
        /// Adds new provider.
        /// </summary>
        /// <param name="provider">New behavior provider.</param>
        /// <returns>Self (for fluency).</returns>
        public BehaviorProviderCollection Add(IBehaviorProvider provider)
        {
            Ensure.NotNull(provider, "provider");
            providers.Insert(0, provider);
            return this;
        }

        /// <summary>
        /// Gets registered behavior types for <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Handler type.</param>
        /// <returns>Registered behavior types for <paramref name="handlerType"/>.</returns>
        public IEnumerable<Type> GetBehaviors(Type handlerType)
        {
            IEnumerable<Type> result = Enumerable.Empty<Type>();
            foreach (IBehaviorProvider provider in providers)
                result = Enumerable.Concat(result, provider.GetBehaviors(handlerType));

            return result;
        }
    }
}
