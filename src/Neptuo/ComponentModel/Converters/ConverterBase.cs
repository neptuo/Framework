using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Converters
{
    public class ConverterBase<TSource, TTarget> : IConverter<TSource, TTarget>
    {
        protected OutFunc<TSource, TTarget, bool> Converter { get; set; }

        public ConverterBase()
        { }

        public ConverterBase(OutFunc<TSource, TTarget, bool> converter)
        {
            Guard.NotNull(converter, "converter");
            Converter = converter;
        }

        public virtual bool TryConvert(TSource sourceValue, out TTarget targetValue)
        {
            if (Converter != null)
                return Converter(sourceValue, out targetValue);

            throw Guard.Exception.InvalidOperation("Override TryConvert method or provider Converter function.");
        }

        public bool TryConvertGeneral(Type sourceType, Type targetType, object sourceValue, out object targetValue)
        {
            TTarget target;
            if (TryConvert((TSource)sourceValue, out target))
            {
                targetValue = target;
                return true;
            }

            targetValue = null;
            return false;
        }
    }
}
