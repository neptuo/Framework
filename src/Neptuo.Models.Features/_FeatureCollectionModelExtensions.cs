using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Features
{
    /// <summary>
    /// Common extensions for <see cref="FeatureCollectionModel"/>.
    /// </summary>
    public static class _FeatureCollectionModelExtensions
    {
        /// <summary>
        /// Adds feature to the collection.
        /// </summary>
        /// <typeparam name="T">Tyoe if feature.</typeparam>
        /// <param name="feature">Feature instance.</param>
        /// <returns>Self (for fluency).</returns>
        public static FeatureCollectionModel Add<T>(this FeatureCollectionModel collection, T feature)
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(typeof(T), feature);
        }
    }
}
