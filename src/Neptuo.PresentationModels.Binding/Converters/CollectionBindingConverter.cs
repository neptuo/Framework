using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Binding.Converters
{
    public class CollectionBindingConverter<T> : BindingConverterBase<ICollection<T>>
    {
        protected string Separator { get; private set; }
        protected BindingConverterBase<T> Converter { get; private set; }

        public CollectionBindingConverter(string separator, BindingConverterBase<T> converter)
        {
            Ensure.NotNullOrEmpty(separator, "separator");
            Ensure.NotNull(converter, "converter");
            Separator = separator;
            Converter = converter;
        }

        public override bool TryConvertTo(string sourceValue, IFieldDefinition targetField, out ICollection<T> targetValue)
        {
            if (String.IsNullOrEmpty(sourceValue))
            {
                targetValue = null;
                return true;
            }

            string[] parts = sourceValue.Split(new string[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
            List<T> result = new List<T>();
            foreach (string part in parts)
            {
                T notNullValue;
                if (Converter.TryConvertTo(part, targetField, out notNullValue))
                    result.Add(notNullValue);
            }

            targetValue = result;
            return true;
        }
    }
}
