using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// Serializer for model definitions to XML format as described at <see cref="XmlNameDefinition.XmlnsUri"/>.
    /// Not mapped types in <see cref="XmlTypeMappingCollection"/> are serialized as <see cref="Type.AssemblyQualifiedName"/>.
    /// </summary>
    public class XmlModelDefinitionSerializer
    {
        private readonly XmlTypeMappingCollection typeMappings;

        /// <summary>
        /// Creates new instance with type name mappings.
        /// </summary>
        /// <param name="typeMappings">Type name mappings between XML file and .NET type system.</param>
        public XmlModelDefinitionSerializer(XmlTypeMappingCollection typeMappings)
        {
            Ensure.NotNull(typeMappings, "typeMappings");
            this.typeMappings = typeMappings;
        }

        /// <summary>
        /// Creates new instance with empty type name mappings.
        /// </summary>
        public XmlModelDefinitionSerializer()
            : this(new XmlTypeMappingCollection())
        { }

        /// <summary>
        /// Serializes <paramref name="modelDefinition"/> to the <see cref="targetStream"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition to serialize.</param>
        /// <param name="targetStream">Serialization target.</param>
        public void Serialize(IModelDefinition modelDefinition, Stream targetStream)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(targetStream, "targetStream");
            SerializeModelDefinition(modelDefinition).Save(targetStream);
        }

        /// <summary>
        /// Serializes <paramref name="modelDefinition"/> to the <see cref="targetWriter"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition to serialize.</param>
        /// <param name="targetWriter">Serialization target.</param>
        public void Serialize(IModelDefinition modelDefinition, TextWriter targetWriter)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(targetWriter, "targetWriter");
            SerializeModelDefinition(modelDefinition).Save(targetWriter);
        }

        private string SerializeType(Type type)
        {
            string targetTypeName;
            if (typeMappings.TryGetXmlName(type, out targetTypeName))
                return targetTypeName;

            return type.AssemblyQualifiedName;
        }

        private XDocument SerializeModelDefinition(IModelDefinition modelDefinition)
        {
            XElement result = new XElement(XName.Get(XmlNameDefinition.ModelDefinitionElementName, XmlNameDefinition.XmlnsUri));
            result.Add(new XAttribute(XmlNameDefinition.ModelDefinitionIdentifierAttributeName, modelDefinition.Identifier));
            result.Add(SerializeMetadata(modelDefinition.Metadata));
            result.Add(modelDefinition.Fields.Select(SerializeFieldDefinition));
            
            XDocument document = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), result);
            return document;
        }

        private XElement SerializeFieldDefinition(IFieldDefinition fieldDefinition)
        {
            XElement result = new XElement(XName.Get(XmlNameDefinition.FieldDefinitionElementName, XmlNameDefinition.XmlnsUri));
            result.Add(new XAttribute(XmlNameDefinition.FieldDefinitionIdentifierAttributeName, fieldDefinition.Identifier));
            result.Add(new XAttribute(XmlNameDefinition.FieldDefinitionTypeAttributeName, SerializeType(fieldDefinition.FieldType)));
            result.Add(SerializeMetadata(fieldDefinition.Metadata));
            return result;
        }

        private IEnumerable<XElement> SerializeMetadata(IReadOnlyKeyValueCollection metadata)
        {
            return metadata.Keys.Select(key => new XElement(
                XName.Get(XmlNameDefinition.MetadataElementName, XmlNameDefinition.XmlnsUri),
                new XAttribute(XmlNameDefinition.MetadataKeyAttributeName, key),
                new XAttribute(XmlNameDefinition.MetadataValueAttributeName, metadata.Get<string>(key))
            ));
        }
    }
}
