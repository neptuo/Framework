using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public class BindingConverterCollection : IBindingConverterCollection
    {
        protected BindingConverterCollection PreviousCollection { get; private set; }
        protected Dictionary<IFieldType, List<IBindingConverter>> Storage { get; private set; }

        public BindingConverterCollection(BindingConverterCollection previousCollection = null)
        {
            Storage = new Dictionary<IFieldType, List<IBindingConverter>>();
            PreviousCollection = previousCollection;
        }

        public BindingConverterCollection Add(IFieldType fieldType, IBindingConverter converter)
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
            return this;
        }

        public bool TryConvert(string sourceValue, IFieldDefinition targetField, out object targetValue)
        {
            IEnumerable<IBindingConverter> converters;
            if (TryGetConverters(targetField, out converters))
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

        public virtual bool TryGetConverters(IFieldDefinition targetField, out IEnumerable<IBindingConverter> converters)
        {
            List<IBindingConverter> storageValue;
            if(Storage.TryGetValue(targetField.FieldType, out storageValue))
            {
                if (PreviousCollection != null)
                {
                    IEnumerable<IBindingConverter> previousConverters;
                    if (PreviousCollection.TryGetConverters(targetField, out previousConverters))
                        storageValue.AddRange(previousConverters);
                }

                converters = storageValue;
                return true;
            }

            if (PreviousCollection != null)
                return PreviousCollection.TryGetConverters(targetField, out converters);

            converters = null;
            return false;
        }
    }
}
