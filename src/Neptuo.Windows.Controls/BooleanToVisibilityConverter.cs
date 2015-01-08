using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Neptuo.Windows.Controls
{
    public class BooleanToVisibilityConverter : IValueConverter, IMultiValueConverter
    {
        public bool Inversed { get; set; }

        public Visibility? NegativeVisibility { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool targetValue = Inversed ? !(bool)value : (bool)value;

            if (targetValue)
                return Visibility.Visible;

            return NegativeVisibility ?? Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!values.OfType<bool>().Any(value => value == false))
                return Visibility.Visible;

            return NegativeVisibility ?? Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
