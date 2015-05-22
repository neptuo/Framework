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
    /// Sets field values to XML element.
    /// Supports 'fields as attributes', 'fields as inner elements' and custom converter from <see cref="XmlFieldValueGetterContext"/> to <see cref="Object"/>.
    /// </summary>
    public class XmlModelValueSetter : DisposableBase, IModelValueSetter
    {
        private readonly IModelDefinition modelDefinition;
        private readonly Dictionary<string, IFieldDefinition> fieldDefinitions;
        private readonly XElement element;

        /// <summary>
        /// Creates new instance for <paramref name="modelDefinition"/> and target to <paramref name="element"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition.</param>
        /// <param name="element">Value target element.</param>
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
                return false;

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
            else if (value.GetType() == typeof(string))
                stringValue = value;
            else if (!Converts.Try(value.GetType(), typeof(string), value, out stringValue))
                return TryGetDefaultValue(identifier, value);

            element.Add(new XAttribute(name, stringValue));
            return true;
        }

        /// <summary>
        /// Called when identifier can't be set.
        /// </summary>
        protected virtual bool TryGetDefaultValue(string identifier, object value)
        {
            return false;
        }
    }
}
