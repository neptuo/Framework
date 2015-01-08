using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Converters
{
    public interface IConverter
    {
        bool TryConvertGeneral(Type sourceType, Type targetType, object sourceValue, out object targetValue);
    }

    public interface IConverter<TSource, TTarget> : IConverter
    {
        bool TryConvert(TSource sourceValue, out TTarget targetValue);
    }
}
