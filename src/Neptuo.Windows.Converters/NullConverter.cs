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
    /// Based on a passed value equal to <c>null</c>, it returns either <see cref="TrueValue"/> or <see cref="FalseValue"/>.
    /// If the passed value is <see cref="String.Empty"/>, the <see cref="TrueValue"/> is returned.
    /// </summary>
    public class NullConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets a value to return when the passed value is <c>null</c>.
        /// A default value is <c>true</c>.
        /// </summary>
        public object TrueValue { get; set; } = true;

        /// <summary>
        /// Gets or sets a value to return when the passed value is not <c>null</c>.
        /// A default value is <c>false</c>.
        /// </summary>
        public object FalseValue { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return TrueValue;

            if (value is string stringValue)
            {
                if (stringValue == String.Empty)
                    return TrueValue;
            }

            return FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
