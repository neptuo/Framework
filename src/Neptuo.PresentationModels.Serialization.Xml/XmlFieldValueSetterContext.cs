using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// Context for converting field value to the XML element.
    /// </summary>
    public class XmlFieldValueSetterContext
    {
        /// <summary>
        /// Target field definition.
        /// </summary>
        public IFieldDefinition FieldDefinition { get; private set; }

        /// <summary>
        /// Field value.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Creates new instance of the context.
        /// </summary>
        /// <param name="fieldDefinition">Target field definition.</param>
        /// <param name="value">Field value.</param>
        public XmlFieldValueSetterContext(IFieldDefinition fieldDefinition, object value)
        {
            Ensure.NotNull(fieldDefinition, "fieldDefinition");
            Ensure.NotNull(value, "value");
            FieldDefinition = fieldDefinition;
            Value = value;
        }
    }
}
