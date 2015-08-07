using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Features
{
    /// <summary>
    /// Common extensions for <see cref="CollectionFeatureModel"/>.
    /// </summary>
    public static class _CollectionFeatureModelExtensions
    {
        /// <summary>
        /// Adds feature to the collection.
        /// </summary>
        /// <param name="featureType">Type of feature.</param>
        /// <param name="feature">Feature instance.</param>
        /// <returns>Self (for fluency).</returns>
        public static CollectionFeatureModel Add<T>(this CollectionFeatureModel collection, T feature)
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(typeof(T), feature);
        }

        /// <summary>
        /// Adds feature getter to the collection.
        /// </summary>
        /// <param name="featureType">Type of feature.</param>
        /// <param name="featureGetter">Feature getter.</param>
        /// <returns>Self (for fluency).</returns>
        public static CollectionFeatureModel AddGetter<T>(this CollectionFeatureModel collection, Func<object> featureGetter)
        {
            Ensure.NotNull(collection, "collection");
            return collection.AddGetter(typeof(T), featureGetter);
        }

        /// <summary>
        /// Adds feature factory to the collection.
        /// </summary>
        /// <param name="featureType">Type of feature.</param>
        /// <param name="featureFactory">Feature factory.</param>
        /// <returns>Self (for fluency).</returns>
        public static CollectionFeatureModel AddFactory<T>(this CollectionFeatureModel collection, IFactory<T> featureFactory)
            where T : class
        {
            Ensure.NotNull(collection, "collection");
            return collection.AddGetter(typeof(T), (Func<object>)featureFactory.Create);
        }
    }
}
