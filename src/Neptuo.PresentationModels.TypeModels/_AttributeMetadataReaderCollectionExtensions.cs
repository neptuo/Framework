using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    /// <summary>
    /// Common extensions for <see cref="AttributeMetadataReaderCollection"/>.
    /// </summary>
    public static class _AttributeMetadataReaderCollectionExtensions
    {
        /// <summary>
        /// Registers <typeparamref name="TAttribute"/> to be read by <paramref name="reader"/>.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute.</typeparam>
        /// <param name="reader">Reader to process attributes of type <typeparamref name="TAttribute"/>.</param>
        /// <returns>Self (for fluency).</returns>
        public static AttributeMetadataReaderCollection Add<TAttribute>(this AttributeMetadataReaderCollection collection, IAttributeMetadataReader reader)
            where TAttribute : Attribute
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(typeof(TAttribute), reader);
        }

        /// <summary>
        /// Registers <typeparamref name="TAttribute"/> to be read by <paramref name="reader"/>.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute.</typeparam>
        /// <param name="reader">Reader to process attributes of type <typeparamref name="TAttribute"/>.</param>
        /// <returns>Self (for fluency).</returns>
        public static AttributeMetadataReaderCollection Add<TAttribute>(this AttributeMetadataReaderCollection collection, AttributeMetadataReaderBase<TAttribute> reader)
            where TAttribute : Attribute
        {
            Ensure.NotNull(collection, "collection");
            return collection.Add(typeof(TAttribute), reader);
        }
    }
}
