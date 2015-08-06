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
        public static CollectionFeatureModel Add<T>(this CollectionFeatureModel collection, Func<object> featureGetter)
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(typeof(T), featureGetter);
        }

        /// <summary>
        /// Adds feature factory to the collection.
        /// </summary>
        /// <param name="featureType">Type of feature.</param>
        /// <param name="featureFactory">Feature factory.</param>
        /// <returns>Self (for fluency).</returns>
        public static CollectionFeatureModel Add<T>(this CollectionFeatureModel collection, IActivator<object> featureFactory)
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(typeof(T), featureFactory);
        }
    }
}
