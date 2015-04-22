using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    /// <summary>
    /// Attribute type based collection of registered attribute metadata readers.
    /// </summary>
    public class AttributeMetadataReaderCollection
    {
        protected Dictionary<Type, IAttributeMetadataReader> Values { get; private set; }

        public AttributeMetadataReaderCollection()
        {
            Values = new Dictionary<Type, IAttributeMetadataReader>();
        }

        /// <summary>
        /// Registers <paramref name="attributeType"/> to be read by <paramref name="reader"/>.
        /// </summary>
        /// <param name="attributeType">Type of attribute.</param>
        /// <param name="reader">Reader to process attributes of type <paramref name="attributeType"/>.</param>
        /// <returns>Self (for fluency).</returns>
        public AttributeMetadataReaderCollection Add(Type attributeType, IAttributeMetadataReader reader)
        {
            Ensure.NotNull(attributeType, "attributeType");
            Ensure.NotNull(reader, "reader");
            Values[attributeType] = reader;
            return this;
        }

        /// <summary>
        /// Tries to get metadata reader for attribute of type <paramref name="attributeType"/>.
        /// </summary>
        /// <param name="attributeType">Type of attribute.</param>
        /// <param name="reader">Registered reader for attributes of type <paramref name="attributeType"/>.</param>
        /// <returns><c>true</c> if reader is registered; <c>false</c> otherwise.</returns>
        public bool TryGet(Type attributeType, out IAttributeMetadataReader reader)
        {
            Ensure.NotNull(attributeType, "attributeType");
            return Values.TryGetValue(attributeType, out reader);
        }
    }
}
