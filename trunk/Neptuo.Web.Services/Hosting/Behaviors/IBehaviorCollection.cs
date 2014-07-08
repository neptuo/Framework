using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors
{
    /// <summary>
    /// Provides mappings between behavior interface contract and actual implementation type.
    /// </summary>
    public interface IBehaviorCollection
    {
        /// <summary>
        /// Adds mapping of behavior contract <paramref name="behaviorContract"/> to implementation <paramref name="behaviorImplementation"/>.
        /// </summary>
        /// <param name="behaviorContract">Behavior contract type.</param>
        /// <param name="behaviorImplementation">Behavior implementation type.</param>
        void Add(Type behaviorContract, Type behaviorImplementation);

        /// <summary>
        /// Tries to find mapping for behavior contract <paramref name="behaviorContract"/>.
        /// If found, returns true; otherwise false.
        /// </summary>
        /// <param name="behaviorContract">Behavior contract type.</param>
        /// <param name="behaviorImplementation">Behavior implementation type.</param>
        /// <returns>If mapping is found, returns true; otherwise false.</returns>
        bool TryGet(Type behaviorContract, out Type behaviorImplementation);
    }

    /// <summary>
    /// Extensions for <see cref="IBehaviorCollection"/>.
    /// </summary>
    public static class BehaviorCollectionExtensions
    {
        /// <summary>
        /// Generic version of <see cref="IBehaviorCollection.Add"/>.
        /// </summary>
        /// <typeparam name="TContract">Behavior contract type.</typeparam>
        /// <typeparam name="TImplementation">Behavior implementation type.</typeparam>
        /// <param name="collection">Behavior collection to insert into.</param>
        /// <returns><paramref name="collection"/> (for fluency).</returns>
        public static IBehaviorCollection Add<TContract, TImplementation>(this IBehaviorCollection collection)
            where TImplementation : IBehavior<TContract>
        {
            Guard.NotNull(collection, "collection");
            collection.Add(typeof(TContract), typeof(TImplementation));
            return collection;
        }
    }
}
