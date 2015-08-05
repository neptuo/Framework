using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using Neptuo.FileSystems;
using Neptuo.FileSystems.Features;
using Neptuo.Models.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// XML based factory pro creating model definitions from XML files.
    /// XML format must valid againts <see cref="XmlNameDefinition.XmlnsUri"/>.
    /// </summary>
    public class XmlModelDefinitionFactory : IActivator<IModelDefinition, IFile>
    {
        private readonly XmlTypeMappingCollection typeMappings;

        /// <summary>
        /// Creates new instance with type name mappings.
        /// </summary>
        /// <param name="typeMappings">Type name mappings between XML file and .NET type system.</param>
        public XmlModelDefinitionFactory(XmlTypeMappingCollection typeMappings)
        {
            Ensure.NotNull(typeMappings, "typeMappings");
            this.typeMappings = typeMappings;
        }

        /// <summary>
        /// Creates new instance with empty type name mappings.
        /// </summary>
        public XmlModelDefinitionFactory()
            : this(new XmlTypeMappingCollection())
        { }

        /// <summary>
        /// Creates model definition from XML <paramref name="xmlFile"/>.
        /// </summary>
        /// <param name="xmlFile">XML file to create model definition from.</param>
        /// <returns>Model definition from XML <paramref name="xmlFile"/>.</returns>
        public IModelDefinition Create(IFile xmlFile)
        {
            Ensure.Condition.HasFeature<IFileContentReader>(xmlFile);
            if (xmlFile.Extension.ToLowerInvariant() != ".xml")
                Ensure.Exception.FileSystem("Only xml files are supported, but got file named '{0}{1}'.", xmlFile.Name, xmlFile.Extension);

            XDocument document = XDocument.Load(xmlFile.With<IFileContentReader>().GetContentAsStream());
            return BuildModelDefinition(document.Root);
        }

        private string BuildIdentifier(XAttribute attribute)
        {
            if (attribute == null)
                Ensure.Exception.InvalidOperation("Element must have attribute 'Identifier'.");

            return attribute.Value;
        }

        private IKeyValueCollection BuildMetadata(XElement parent)
        {
            KeyValueCollection result = new KeyValueCollection();
            foreach (XElement element in parent.Elements(XName.Get(XmlNameDefinition.MetadataElementName, XmlNameDefinition.XmlnsUri)))
            {
                XAttribute key = element.Attribute(XmlNameDefinition.MetadataKeyAttributeName);
                XAttribute value = element.Attribute(XmlNameDefinition.MetadataValueAttributeName);
                if (key == null || value == null)
                    Ensure.Exception.InvalidOperation("Element 'Metadata' must have attributes 'Key' and 'Value'.");

                result.Add(key.Value, value.Value);
            }

            return result;
        }

        private Type BuildType(XAttribute attribute)
        {
            Ensure.NotNull(attribute, "attribute");
            string typeName = attribute.Value;

            Type targetType;
            if (!typeMappings.TryGetMappedType(typeName, out targetType))
                targetType = Type.GetType(typeName);

            if (targetType == null)
                throw Ensure.Exception.InvalidOperation("Unnable to resolve type.");

            return targetType;
        }

        private IModelDefinition BuildModelDefinition(XElement element)
        {
            if (element.Name.LocalName != XmlNameDefinition.ModelDefinitionElementName)
                Ensure.Exception.InvalidOperation("Model definition must be created from element 'ModelDefinition'.");

            string identifier = BuildIdentifier(element.Attribute(XmlNameDefinition.ModelDefinitionIdentifierAttributeName));
            IKeyValueCollection metadata = BuildMetadata(element);

            List<IFieldDefinition> fields = new List<IFieldDefinition>();
            foreach (XElement fieldElement in element.Elements(XName.Get(XmlNameDefinition.FieldDefinitionElementName, XmlNameDefinition.XmlnsUri)))
                fields.Add(BuildFieldDefinition(fieldElement));

            return new ModelDefinition(identifier, fields, metadata);
        }

        private IFieldDefinition BuildFieldDefinition(XElement element)
        {
            if (element.Name.LocalName != XmlNameDefinition.FieldDefinitionElementName)
                Ensure.Exception.InvalidOperation("Field definition must be created from element 'FieldDefinition'.");

            string identifier = BuildIdentifier(element.Attribute(XmlNameDefinition.FieldDefinitionIdentifierAttributeName));
            IKeyValueCollection metadata = BuildMetadata(element);
            Type fieldType = BuildType(element.Attribute(XmlNameDefinition.FieldDefinitionTypeAttributeName));

            return new FieldDefinition(identifier, fieldType, metadata);
        }
    }
}
