using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Binding.Converters
{
    public class FuncBindingConverter<T> : BindingConverterBase<T>
    {
        public OutFunc<string, T, bool> Converter { get; private set; }

        public FuncBindingConverter(OutFunc<string, T, bool> converter)
        {
            Ensure.NotNull(converter, "converter");
            Converter = converter;
        }

        public override bool TryConvertTo(string sourceValue, IFieldDefinition targetField, out T targetValue)
        {
            return Converter(sourceValue, out targetValue);
        }
    }
}
