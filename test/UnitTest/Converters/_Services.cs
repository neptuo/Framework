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

    public class StringToListConverter<T> : DefaultConverter<IConverterContext<string>, List<T>>
    {
        public override bool TryConvert(IConverterContext<string> context, out List<T> targetValue)
        {
            if (String.IsNullOrEmpty(context.SourceValue))
            {
                targetValue = new List<T>();
                return true;
            }

            targetValue = new List<T>();

            string[] parts = context.SourceValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                T item;
                if (context.Repository.TryConvert(part, out item))
                    targetValue.Add(item);
            }

            return true;
        }
    }
}
