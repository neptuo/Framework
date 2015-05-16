using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neptuo.PresentationModels.Serialization
{
    public class XmlModelValueSetter : DisposableBase, IModelValueSetter
    {
        private readonly IModelDefinition modelDefinition;
        private readonly Dictionary<string, IFieldDefinition> fieldDefinitions;
        private readonly XElement element;

        public XmlModelValueSetter(IModelDefinition modelDefinition, XElement element)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(element, "element");
            this.modelDefinition = modelDefinition;
            this.fieldDefinitions = modelDefinition.FieldsByIdentifier();
            this.element = element;
        }

        public bool TrySetValue(string identifier, object value)
        {
            IFieldDefinition fieldDefinition;
            if (!fieldDefinitions.TryGetValue(identifier, out fieldDefinition))
            {
                value = null;
                return false;
            }

            XName name = XName.Get(identifier, element.Name.NamespaceName);

            IEnumerable<XElement> childElements;
            if (Converts.Try(new XmlFieldValueSetterContext(fieldDefinitions[identifier], value), out childElements))
            {
                element.Add(childElements);
                return true;
            }

            object stringValue;
            if (value == null)
                stringValue = null;
            else if (!Converts.Try(value.GetType(), typeof(string), value, out stringValue))
                return false;

            element.Add(new XAttribute(name, stringValue));
            return true;
        }
    }
}
