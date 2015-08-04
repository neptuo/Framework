using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Binding
{
    public class BindingConverterCollection : IBindingConverterProvider
    {
        protected BindingConverterCollection PreviousCollection { get; private set; }
        protected Dictionary<string, List<IBindingConverter>> Storage { get; private set; }

        public BindingConverterCollection(BindingConverterCollection previousCollection = null)
        {
            Storage = new Dictionary<string, List<IBindingConverter>>();
            PreviousCollection = previousCollection;
        }

        public BindingConverterCollection Add(Type fieldType, IBindingConverter converter)
        {
            Ensure.NotNull(fieldType, "fieldType");
            Ensure.NotNull(converter, "converter");

            string key = GetKey(fieldType); 
            List<IBindingConverter> list;
            if (!Storage.TryGetValue(key, out list))
            {
                list = new List<IBindingConverter>();
                Storage.Add(key, list);
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
            string key = GetKey(targetField.FieldType);
            List<IBindingConverter> storageValue;
            if(Storage.TryGetValue(key, out storageValue))
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

        protected string GetKey(Type fieldType)
        {
            return fieldType.ToString();
        }
    }
}
