using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using Neptuo.FileSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// XML source based implementation of model definition builder.
    /// </summary>
    public class XmlModelDefinitionBuilder : IActivator<IModelDefinition>
    {
        private readonly IReadOnlyFile xmlFile;

        public XmlModelDefinitionBuilder(IReadOnlyFile xmlFile)
        {
            Ensure.NotNull(xmlFile, "xmlFile");
            this.xmlFile = xmlFile;
        }

        public IModelDefinition Create()
        {
            if (xmlFile.Extension.ToLowerInvariant() != ".xml")
                Ensure.Exception.FileSystem("Only xml files are supported, but got file named '{0}{1}'.", xmlFile.Name, xmlFile.Extension);

            XDocument document = XDocument.Load(xmlFile.GetContentAsStream());
            return BuildModelDefinition(document.Root);
        }

        private string BuildIdentifier(XElement element)
        {
            XAttribute attribute = element.Attribute("Identifier");
            if(attribute == null)
                Ensure.Exception.InvalidOperation("Element must have attribute 'Identifier'.");

            return attribute.Value;
        }

        private IKeyValueCollection BuildMetadata(XElement parent)
        {
            KeyValueCollection result = new KeyValueCollection();
            foreach (XElement element in parent.Elements("Metadata"))
            {
                XAttribute key = element.Attribute("Key");
                XAttribute value = element.Attribute("Value");
                if (key == null || value == null)
                    Ensure.Exception.InvalidOperation("Element 'Metadata' must have attributes 'Key' and 'Value'.");

                result.Set(key.Value, value.Value);
            }

            return result;
        }

        private Type BuildType(XAttribute attribute)
        {
            Ensure.NotNull(attribute, "attribute");
            string typeName = attribute.Value;
            return Type.GetType(typeName);
        }

        private IModelDefinition BuildModelDefinition(XElement element)
        {
            if (element.Name != "ModelDefinition")
                Ensure.Exception.InvalidOperation("Model definition must be created from element 'ModelDefinition'.");

            string identifier = BuildIdentifier(element);
            IKeyValueCollection metadata = BuildMetadata(element);

            List<IFieldDefinition> fields = new List<IFieldDefinition>();
            foreach (XElement fieldElement in element.Elements("FieldDefinition"))
                fields.Add(BuildFieldDefinition(fieldElement));

            return new ModelDefinition(identifier, fields, metadata);
        }

        private IFieldDefinition BuildFieldDefinition(XElement element)
        {
            if (element.Name != "FieldDefinition")
                Ensure.Exception.InvalidOperation("Field definition must be created from element 'FieldDefinition'.");

            string identifier = BuildIdentifier(element);
            IKeyValueCollection metadata = BuildMetadata(element);
            Type fieldType = BuildType(element.Attribute("Type"));

            return new FieldDefinition(identifier, fieldType, metadata);
        }
    }
}
