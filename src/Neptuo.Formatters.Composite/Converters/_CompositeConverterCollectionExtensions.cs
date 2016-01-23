using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    /// <summary>
    /// Common extensions for <see cref="CompositeConverterCollection"/>.
    /// </summary>
    public static class _CompositeConverterCollectionExtensions
    {
        /// <summary>
        /// Adds search handler for types implementing <see cref="ICompositeModel"/> to be serialized/deserialized like composite model on <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">The collection to register handler in.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static CompositeConverterCollection AddCompositeModel(this CompositeConverterCollection collection)
        {
            Ensure.NotNull(collection, "collection");
            return collection.AddSearchHandler(TryGetCompositeModel);
        }

        private static bool TryGetCompositeModel(Type valueType, out ICompositeConverter formatter)
        {
            if (typeof(ICompositeModel).IsAssignableFrom(valueType))
            {
                formatter = new ModelCompositeConverter();
                return true;
            }

            formatter = null;
            return false;
        }

        /// <summary>
        /// Adds search handler for any type as last chance to serialize/deserialize value. This uses build-in type conversion in <see cref="ICompositeStorage"/>.
        /// </summary>
        /// <param name="collection">The collection to register handler in.</param>
        /// <returns><paramref name="collection"/>.</returns>
        public static CompositeConverterCollection AddDefault(this CompositeConverterCollection collection)
        {
            Ensure.NotNull(collection, "collection");
            return collection.AddSearchHandler(TryGetDefault);
        }

        private static bool TryGetDefault(Type valueType, out ICompositeConverter formatter)
        {
            formatter = new DefaultCompositeConverter();
            return true;
        }
    }
}
