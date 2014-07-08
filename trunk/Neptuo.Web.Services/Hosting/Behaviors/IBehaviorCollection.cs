using Neptuo.Web.Services.Hosting.Behaviors.Providers;
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
        /// Adds provider for behaviors.
        /// </summary>
        /// <param name="provider">Behavior provider.</param>
        void Add(IBehaviorProvider provider);

        /// <summary>
        /// Gets registered behaviors for handler of type <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Handler type.</param>
        /// <returns>Registered behaviors for handler of type <paramref name="handlerType"/>.</returns>
        IEnumerable<Type> GetBehaviors(Type handlerType);
    }

    /// <summary>
    /// Extensions for <see cref="IBehaviorCollection"/>.
    /// </summary>
    public static class BehaviorCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="InterfaceBehaviorProvider"/> with <typeparamref name="TContract"/> as contract interface 
        /// and <typeparamref name="TImplementation"/> as implementation class.
        /// </summary>
        /// <typeparam name="TContract">Contract interface</typeparam>
        /// <typeparam name="TImplementation">Implementation type.</typeparam>
        /// <param name="collection">Collection to insert into.</param>
        /// <returns><paramref name="collection"/> (for fluency).</returns>
        public static IBehaviorCollection Add<TContract, TImplementation>(this IBehaviorCollection collection)
            where TImplementation : IBehavior<TContract>
        {
            Guard.NotNull(collection, "collection");
            collection.Add(new InterfaceBehaviorProvider(typeof(TContract), typeof(TImplementation)));
            return collection;
        }

        public static IBehaviorCollection AddEndpoints(this IBehaviorCollection collection)
        {
            Guard.NotNull(collection, "collection");
            collection.Add(
                new InterfaceBehaviorProvider(typeof(IGet), typeof(GetBehavior))
                    .AddMapping(typeof(IPost), typeof(PostBehavior))
            );
            return collection;
        }
    }
}
