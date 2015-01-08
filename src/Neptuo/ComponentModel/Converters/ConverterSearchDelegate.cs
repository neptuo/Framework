using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Converters
{
    /// <summary>
    /// Delegate for searching for converter.
    /// </summary>
    /// <param name="sourceType">Source type.</param>
    /// <param name="targetType">Targe type.</param>
    /// <returns>Converter for conversion from <paramref name="sourceType"/> to <paramref name="targetType"/>.</returns>
    public delegate IConverter ConverterSearchDelegate(Type sourceType, Type targetType);
}
