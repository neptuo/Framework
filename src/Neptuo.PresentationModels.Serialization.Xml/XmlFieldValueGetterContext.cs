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
        /// Matched element.
        /// </summary>
        public XElement Element { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="fieldDefinition">Target field definition.</param>
        /// <param name="element">Matched element.</param>
        public XmlFieldValueGetterContext(IFieldDefinition fieldDefinition, XElement element)
        {
            Ensure.NotNull(fieldDefinition, "fieldDefinition");
            Ensure.NotNull(element, "element");
            FieldDefinition = fieldDefinition;
            Element = element;
        }
    }
}
