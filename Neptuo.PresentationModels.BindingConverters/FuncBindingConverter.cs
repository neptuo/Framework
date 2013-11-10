using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.BindingConverters
{
    public class FuncBindingConverter<T> : BindingConverterBase<T>
    {
        public OutFunc<string, T, bool> Converter { get; private set; }

        public FuncBindingConverter(OutFunc<string, T, bool> converter)
        {
            if (converter == null)
                throw new ArgumentNullException("converter");

            Converter = converter;
        }

        protected override bool TryConvert(string sourceValue, IFieldDefinition targetField, out T targetValue)
        {
            return Converter(sourceValue, out targetValue);
        }
    }
}
