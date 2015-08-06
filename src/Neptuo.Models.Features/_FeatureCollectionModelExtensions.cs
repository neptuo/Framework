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
    public static class _FeatureCollectionModelExtensions
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
        /// Adds feature provider to the collection.
        /// </summary>
        /// <param name="featureType">Type of feature.</param>
        /// <param name="featureGetter">Feature provider.</param>
        /// <returns>Self (for fluency).</returns>
        public static CollectionFeatureModel Add<T>(this CollectionFeatureModel collection, Func<object> featureGetter)
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(typeof(T), featureGetter);
        }
    }
}
