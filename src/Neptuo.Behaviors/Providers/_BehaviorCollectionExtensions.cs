using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Providers
{
    /// <summary>
    /// Extensions for <see cref="IBehaviorCollection"/>.
    /// </summary>
    public static class _BehaviorCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="InterfaceBehaviorProvider"/> with <typeparamref name="TContract"/> as contract interface 
        /// and <typeparamref name="TImplementation"/> as implementation class.
        /// </summary>
        /// <typeparam name="TContract">Contract interface</typeparam>
        /// <typeparam name="TImplementation">Implementation type.</typeparam>
        /// <param name="collection">Collection to insert into.</param>
        /// <returns><paramref name="collection"/> (for fluency).</returns>
        public static BehaviorProviderCollection AddInterface<TContract, TImplementation>(this BehaviorProviderCollection collection)
            where TImplementation : IBehavior<TContract>
        {
            Ensure.NotNull(collection, "collection");
            collection.Add(new InterfaceBehaviorProvider(typeof(TContract), typeof(TImplementation)));
            return collection;
        }
    }
}
