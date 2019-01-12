using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Neptuo.Windows.Converters
{
    /// <summary>
    /// Based on coparison of a passed value with a <see cref="EdgeValue"/>, it returns either <see cref="LowerValue"/>, <see cref="EqualValue"/> or <see cref="GreaterValue"/>.
    /// </summary>
    public class NumberCompareConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets a decision value.
        /// </summary>
        public int EdgeValue { get; set; }

        /// <summary>
        /// Gets or sets what to return when a passed value is lower than the <see cref="EdgeValue"/>.
        /// </summary>
        public object LowerValue { get; set; }

        /// <summary>
        /// Gets or sets what to return when a passed value is equal to the <see cref="EdgeValue"/>.
        /// </summary>
        public object EqualValue { get; set; }

        /// <summary>
        /// Gets or sets what to return when a passed value is greater than the <see cref="EdgeValue"/>.
        /// </summary>
        public object GreaterValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            if (intValue > EdgeValue)
                return GreaterValue;
            else if (intValue < EdgeValue)
                return LowerValue;
            else
                return EqualValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
