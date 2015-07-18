using Neptuo.ComponentModel.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections
{
    [Converter]
    public class IntToStringConverter : ConverterBase<int, string>
    {
        public override bool TryConvert(int sourceValue, out string targetValue)
        {
            targetValue = sourceValue.ToString();
            return true;
        }
    }
}
