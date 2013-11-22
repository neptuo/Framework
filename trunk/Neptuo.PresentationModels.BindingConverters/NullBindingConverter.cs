using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.BindingConverters
{
    public class NullBindingConverter<T> : BindingConverterBase<T?>
        where T : struct
    {
        protected BindingConverterBase<T> Converter { get; private set; }

        public NullBindingConverter(BindingConverterBase<T> converter)
        {
            if (converter == null)
                throw new ArgumentNullException("converter");

            Converter = converter;
        }

        public override bool TryConvert(string sourceValue, IFieldDefinition targetField, out T? targetValue)
        {
            if (String.IsNullOrEmpty(sourceValue))
            {
                targetValue = null;
                return true;
            }

            T notNullValue;
            if(Converter.TryConvert(sourceValue, targetField, out notNullValue))
            {
                targetValue = notNullValue;
                return true;
            }

            targetValue = null;
            return false;
        }
    }
}