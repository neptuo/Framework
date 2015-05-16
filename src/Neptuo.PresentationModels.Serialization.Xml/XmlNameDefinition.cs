using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// Used XML node names list.
    /// </summary>
    public static class XmlNameDefinition
    {
        /// <summary>
        /// Target XSD definition URI that every XML file must be valid againt.
        /// </summary>
        public static string XmlnsUri = "http://schemas.neptuo.com/xsd/neptuo-presentationmodels-modeldefinition.xsd";

        /// <summary>
        /// Name of the model definition element.
        /// </summary>
        public static string ModelDefinitionElementName = "ModelDefinition";

        /// <summary>
        /// Name of the model definition identifier attribute.
        /// </summary>
        public static string ModelDefinitionIdentifierAttributeName = "Identifier";

        /// <summary>
        /// Name of the field definition element.
        /// </summary>
        public static string FieldDefinitionElementName = "FieldDefinition";

        /// <summary>
        /// Name of the field definition identifier attribute.
        /// </summary>
        public static string FieldDefinitionIdentifierAttributeName = "Identifier";

        /// <summary>
        /// Name of the field definition type name attribute.
        /// </summary>
        public static string FieldDefinitionTypeAttributeName = "FieldType";

        /// <summary>
        /// Name of the metadata element.
        /// </summary>
        public static string MetadataElementName = "Metadata";

        /// <summary>
        /// Name of the metadata key attribute.
        /// </summary>
        public static string MetadataKeyAttributeName = "Key";

        /// <summary>
        /// Name of the metadata value attribute.
        /// </summary>
        public static string MetadataValueAttributeName = "Value";
    }
}
