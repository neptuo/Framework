using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Providers
{
    /// <summary>
    /// Common extensions for <see cref="InterfaceBehaviorCollection"/>.
    /// </summary>
    public static class _InterfaceBehaviorProviderExtensions
    {
        /// <summary>
        /// Adds mapping with <typeparamref name="TBehaviorContract"/> as contract and <typeparamref name="TBehaviorImplementation"/> as implementation type.
        /// </summary>
        /// <typeparam name="TBehaviorContract">Behavior interface contract.</typeparam>
        /// <typeparam name="TBehaviorImplementation">Behavior contract implementor.</typeparam>
        /// <returns>Self (for fluency).</returns>>
        public static InterfaceBehaviorCollection Add<TBehaviorContract, TBehaviorImplementation>(this InterfaceBehaviorCollection provider)
            where TBehaviorImplementation : IBehavior<TBehaviorContract>
        {
            Ensure.NotNull(provider, "provider");
            return provider.Add(typeof(TBehaviorContract), typeof(TBehaviorImplementation));
        }

        /// <summary>
        /// Adds mapping with <typeparamref name="TBehaviorContract"/> as contract and <typeparamref name="TBehaviorImplementation"/> as implementation type.
        /// </summary>
        /// <typeparam name="TBehaviorContract">Behavior interface contract.</typeparam>
        /// <typeparam name="TBehaviorImplementation">Behavior contract implementor.</typeparam>
        /// <returns>Self (for fluency).</returns>>
        public static InterfaceBehaviorCollection Insert<TBehaviorContract, TBehaviorImplementation>(this InterfaceBehaviorCollection provider, int index)
            where TBehaviorImplementation : IBehavior<TBehaviorContract>
        {
            Ensure.NotNull(provider, "provider");
            return provider.Insert(index, typeof(TBehaviorContract), typeof(TBehaviorImplementation));
        }
    }
}
