using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// Reads field values from XML element.
    /// Supports 'fields at attributes', 'fields as inner elements' and custom converter from <see cref="XmlFieldValueGetterContext"/> to <see cref="Object"/>.
    /// </summary>
    public class XmlModelValueGetter : DisposableBase, IModelValueGetter
    {
        private readonly IModelDefinition modelDefinition;
        private readonly Dictionary<string, IFieldDefinition> fieldDefinitions;
        private readonly XElement element;

        public XmlModelValueGetter(IModelDefinition modelDefinition, XElement element)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(element, "element");
            this.modelDefinition = modelDefinition;
            this.fieldDefinitions = modelDefinition.FieldsByIdentifier();
            this.element = element;
        }

        public bool TryGetValue(string identifier, out object value)
        {
            // If model definition doesn't contain field definition, do nothing.
            IFieldDefinition fieldDefinition;
            if (!fieldDefinitions.TryGetValue(identifier, out fieldDefinition))
            {
                value = null;
                return false;
            }

            XName name = XName.Get(identifier, element.Name.NamespaceName);

            // 1) Try parse from attribute value.
            XAttribute attribute = element.Attribute(name);
            if (attribute != null)
            {
                string stringValue = attribute.Value;
                if(Converts.Try(typeof(string), fieldDefinition.FieldType, stringValue, out value))
                    return true;
            }

            // 2) Try find single inner element...
            IEnumerable<XElement> childElements = element.Elements(name);
            if (childElements.Count() != 1)
            {
                value = null;
                return false;
            }

            // ... and try to convert to field type.
            XElement childElement = childElements.First();
            if (Converts.Try(new XmlFieldValueGetterContext(fieldDefinitions[identifier], childElement), out value))
                return true;

            // ... otherwise try to convert inner text value.
            if (!childElement.Attributes().Any() && !childElement.Elements().Any())
            {
                string stringValue = childElement.Value;
                if (Converts.Try(typeof(string), fieldDefinition.FieldType, stringValue, out value))
                    return true;
            }

            // ... 
            value = null;
            return false;
        }
    }
}
