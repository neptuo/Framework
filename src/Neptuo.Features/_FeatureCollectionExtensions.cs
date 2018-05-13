using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Features
{
    /// <summary>
    /// A common extensions for <see cref="FeatureCollection"/>.
    /// </summary>
    public static class _FeatureCollectionExtensions
    {
        /// <summary>
        /// Adds feature to the collection.
        /// </summary>
        /// <typeparam name="T">A type of feature.</typeparam>
        /// <param name="collection">A collection of features.</param>
        /// <param name="feature">A feature instance.</param>
        /// <returns>Self (for fluency).</returns>
        public static FeatureCollection Add<T>(this FeatureCollection collection, T feature)
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(typeof(T), feature);
        }

        /// <summary>
        /// Adds feature getter to the collection.
        /// </summary>
        /// <typeparam name="T">A type of feature.</typeparam>
        /// <param name="collection">A collection of features.</param>
        /// <param name="featureGetter">A feature getter.</param>
        /// <returns>Self (for fluency).</returns>
        public static FeatureCollection AddGetter<T>(this FeatureCollection collection, Func<object> featureGetter)
        {
            Ensure.NotNull(collection, "collection");
            return collection.AddGetter(typeof(T), featureGetter);
        }

        /// <summary>
        /// Adds feature factory to the collection.
        /// </summary>
        /// <typeparam name="T">A type of feature.</typeparam>
        /// <param name="collection">A collection of features.</param>
        /// <param name="featureFactory">A feature factory.</param>
        /// <returns>Self (for fluency).</returns>
        public static FeatureCollection AddFactory<T>(this FeatureCollection collection, IFactory<T> featureFactory)
            where T : class
        {
            Ensure.NotNull(collection, "collection");
            return collection.AddGetter(typeof(T), featureFactory.Create);
        }
    }
}
