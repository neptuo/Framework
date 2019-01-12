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
    /// IF a passed value implements <see cref="IFormattable"/>, it applies <see cref="Format"/> to it.
    /// </summary>
    public class FormatConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets a format to apply to a passed <see cref="IFormattable"/>.
        /// </summary>
        public string Format { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IFormattable formattable = value as IFormattable;
            if (formattable != null)
                return formattable.ToString(Format, culture);

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
