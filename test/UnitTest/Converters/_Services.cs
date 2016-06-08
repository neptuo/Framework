using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    public enum Gender
    {
        Male, Female
    }

    public class TimeSpanConveter : TwoWayConverter<TimeSpan, string>
    {
        public override bool TryConvertFromOneToTwo(TimeSpan sourceValue, out string targetValue)
        {
            targetValue = sourceValue.ToString();
            return true;
        }

        public override bool TryConvertFromTwoToOne(string sourceValue, out TimeSpan targetValue)
        {
            return TimeSpan.TryParse(sourceValue, out targetValue);
        }
    }

}
