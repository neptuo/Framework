using Neptuo.Converters;
using Neptuo.Converters.AutoExports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflection
{
    [Converter]
    public class IntToStringConverter : DefaultConverter<int, string>
    {
        public override bool TryConvert(int sourceValue, out string targetValue)
        {
            targetValue = sourceValue.ToString();
            return true;
        }
    }
}
