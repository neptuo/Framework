using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// Context for converting XML element to the field value.
    /// </summary>
    public class XmlFieldValueGetterContext
    {
        /// <summary>
        /// Target field definition.
        /// </summary>
        public IFieldDefinition FieldDefinition { get; private set; }

        /// <summary>
        /// Collection of matched elements.
        /// </summary>
        public IEnumerable<XElement> Elements { get; private set; }

        public XmlFieldValueGetterContext(IFieldDefinition fieldDefinition, IEnumerable<XElement> elements)
        {
            Ensure.NotNull(fieldDefinition, "fieldDefinition");
            Ensure.NotNull(elements, "elements");
            FieldDefinition = fieldDefinition;
            Elements = elements;
        }
    }
}
