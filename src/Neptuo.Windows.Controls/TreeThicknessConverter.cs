using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Neptuo.Windows.Controls
{
    public class TreeThicknessConverter : IValueConverter
    {
        public Thickness Thickness { get; set; }
        public Thickness BaseThickness { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TreeViewItem item = value as TreeViewItem;
            if (item == null)
                return 0;

            Thickness result = new Thickness();
            if (BaseThickness != null)
            {
                result.Left += BaseThickness.Left;
                result.Top += BaseThickness.Top;
                result.Right += BaseThickness.Right;
                result.Bottom += BaseThickness.Bottom;
            }

            int depth = GetDepth(item);

            result.Left += Thickness.Left * depth;
            result.Top += Thickness.Top * depth;
            result.Right += Thickness.Right * depth;
            result.Bottom += Thickness.Bottom * depth;
            
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static int GetDepth(TreeViewItem item)
        {
            FrameworkElement elem = item;
            var parent = VisualTreeHelper.GetParent(item);
            var count = 0;
            while (parent != null && !(parent is TreeView))
            {
                var tvi = parent as TreeViewItem;
                if (parent is TreeViewItem)
                    count++;
                parent = VisualTreeHelper.GetParent(parent);
            }
            return count;
        }
    }
}
