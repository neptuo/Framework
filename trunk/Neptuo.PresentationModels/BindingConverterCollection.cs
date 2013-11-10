using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public class BindingConverterCollection : IBindingConverterCollection
    {
        protected Dictionary<IFieldType, List<IBindingConverter>> Storage { get; private set; }

        public BindingConverterCollection()
        {
            Storage = new Dictionary<IFieldType, List<IBindingConverter>>();
        }

        public void Add(IFieldType fieldType, IBindingConverter converter)
        {
            if (fieldType == null)
                throw new ArgumentNullException("fieldType");

            if (converter == null)
                throw new ArgumentNullException("converter");

            List<IBindingConverter> list;
            if (!Storage.TryGetValue(fieldType, out list))
            {
                list = new List<IBindingConverter>();
                Storage.Add(fieldType, list);
            }
            list.Add(converter);
        }

        public bool TryConvert(string sourceValue, IFieldDefinition targetField, out object targetValue)
        {
            List<IBindingConverter> converters;
            if (Storage.TryGetValue(targetField.FieldType, out converters))
            {
                foreach (IBindingConverter converter in converters)
                {
                    if (converter.TryConvert(sourceValue, targetField, out targetValue))
                        return true;
                }
            }

            targetValue = null;
            return false;
        }
    }
}
