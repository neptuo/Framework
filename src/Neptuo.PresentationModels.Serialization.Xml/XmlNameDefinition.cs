using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Serialization
{
    public static class XmlNameDefinition
    {
        public static string XmlnsUri = "http://schemas.neptuo.com/xsd/neptuo-presentationmodels-modeldefinition.xsd";

        public static string ModelDefinitionElementName = "ModelDefinition";
        public static string ModelDefinitionIdentifierAttributeName = "Identifier";

        public static string FieldDefinitionElementName = "FieldDefinition";
        public static string FieldDefinitionIdentifierAttributeName = "Identifier";
        public static string FieldDefinitionTypeAttributeName = "FieldType";

        public static string MetadataElementName = "Metadata";
        public static string MetadataKeyAttributeName = "Key";
        public static string MetadataValueAttributeName = "Value";
    }
}
