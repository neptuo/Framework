using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Providers
{
    /// <summary>
    /// Common extensions for <see cref="AttributeBehaviorCollection"/>.
    /// </summary>
    public static class _AttributeBehaviorProviderExtensions
    {
        /// <summary>
        /// Adds mapping for handlers of type <typeparamref name="THandler" /> decorated with <typeparamref name="TAttribute"/> to have behavior of type <typeparamref name="TBehaviorImplementation"/>.
        /// </summary>
        /// <typeparam name="TAttribute">Behavior attribute contract.</typeparam>
        /// <typeparam name="THandler">Type of handler</typeparam>
        /// <typeparam name="TBehaviorImplementation">Behavior contract implementor.</typeparam>
        /// <param name="collection">Target behavior collection.</param>
        /// <param name="index">Index to insert mapping at.</param>
        /// <returns>Self (for fluency).</returns>
        public static AttributeBehaviorCollection Add<TAttribute, THandler, TBehaviorImplementation>(this AttributeBehaviorCollection collection)
            where TAttribute : Attribute
            where TBehaviorImplementation : IBehavior<THandler>
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(typeof(TAttribute), typeof(TBehaviorImplementation));
        }

        /// <summary>
        /// Adds mapping for all handlers decorated with <typeparamref name="TAttribute"/> to have behavior of type <typeparamref name="TBehaviorImplementation"/>.
        /// </summary>
        /// <typeparam name="TAttribute">Behavior attribute contract.</typeparam>
        /// <typeparam name="TBehaviorImplementation">Behavior contract implementor.</typeparam>
        /// <param name="collection">Target behavior collection.</param>
        /// <param name="index">Index to insert mapping at.</param>
        /// <returns>Self (for fluency).</returns>
        public static AttributeBehaviorCollection Add<TAttribute, TBehaviorImplementation>(this AttributeBehaviorCollection collection)
            where TAttribute : Attribute
            where TBehaviorImplementation : IBehavior<object>
        {
            return Add<TAttribute, object, TBehaviorImplementation>(collection);
        }

        /// <summary>
        /// Adds mapping for handlers of type <typeparamref name="THandler" /> decorated with <typeparamref name="TAttribute"/> to have behavior of type <typeparamref name="TBehaviorImplementation"/>.
        /// </summary>
        /// <typeparam name="TAttribute">Behavior attribute contract.</typeparam>
        /// <typeparam name="THandler">Type of handler</typeparam>
        /// <typeparam name="TBehaviorImplementation">Behavior contract implementor.</typeparam>
        /// <param name="collection">Target behavior collection.</param>
        /// <param name="index">Index to insert mapping at.</param>
        /// <returns>Self (for fluency).</returns>
        public static AttributeBehaviorCollection Insert<TAttribute, THandler, TBehaviorImplementation>(this AttributeBehaviorCollection collection, int index)
            where TAttribute : Attribute
            where TBehaviorImplementation : class, IBehavior<THandler>
        {
            Ensure.NotNull(collection, "collection");
            return collection.Insert(index, typeof(TAttribute), typeof(TBehaviorImplementation));
        }

        /// <summary>
        /// Adds mapping for all handlers decorated with <typeparamref name="TAttribute"/> to have behavior of type <typeparamref name="TBehaviorImplementation"/>.
        /// </summary>
        /// <typeparam name="TAttribute">Behavior attribute contract.</typeparam>
        /// <typeparam name="TBehaviorImplementation">Behavior contract implementor.</typeparam>
        /// <param name="collection">Target behavior collection.</param>
        /// <param name="index">Index to insert mapping at.</param>
        /// <returns>Self (for fluency).</returns>
        public static AttributeBehaviorCollection Insert<TAttribute, TBehaviorImplementation>(this AttributeBehaviorCollection collection, int index)
            where TAttribute : Attribute
            where TBehaviorImplementation : class, IBehavior<object>
        {
            return Insert<TAttribute, object, TBehaviorImplementation>(collection, index);
        }
    }
}
