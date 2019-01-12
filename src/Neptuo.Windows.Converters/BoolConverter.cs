using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Neptuo.Windows.Converters
{
    /// <summary>
    /// Based on a passed value of a type <see cref="Boolean"/> (or nullable equivalent), it uses <see cref="Test"/> to compare the value and returns either a <see cref="TrueValue"/> or a <see cref="FalseValue"/>.
    /// Before returning <see cref="TrueValue"/> or <see cref="FalseValue"/> it tries to convert the value to a target type using <see cref="TypeConverter"/>.
    /// </summary>
    public class BoolConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets a value to compare to.
        /// A default value is <c>true</c>.
        /// </summary>
        [DefaultValue(true)]
        public bool Test { get; set; } = true;

        /// <summary>
        /// Gets or sets a value to return when the passed value is equal to the <see cref="Test"/>.
        /// </summary>
        public object TrueValue { get; set; }

        /// <summary>
        /// Gets or sets a value to return when the passed value is not equal to the <see cref="Test"/>.
        /// </summary>
        public object FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? boolValue = value as bool?;
            if (boolValue == null)
                boolValue = false;

            object result = null;
            if (Test == boolValue.Value)
                result = TrueValue;
            else
                result = FalseValue;

            if (targetType != null && result != null)
            {
                Type resultType = result.GetType();
                TypeConverter converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null && converter.CanConvertFrom(resultType))
                    result = converter.ConvertFrom(null, CultureInfo.InvariantCulture, result);
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
