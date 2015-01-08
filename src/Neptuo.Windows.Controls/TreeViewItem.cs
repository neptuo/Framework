using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Neptuo.Windows.Controls
{
    public class TreeViewItem : System.Windows.Controls.TreeViewItem
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return CreateTreeViewItem();
        }

        internal static TreeViewItem CreateTreeViewItem()
        {
            TreeViewItem item = new TreeViewItem();
            SetBinding(item, "IsExpanded", TreeViewItem.IsExpandedProperty);
            SetBinding(item, "IsVisible", TreeViewItem.VisibilityProperty, new BooleanToVisibilityConverter());
            SetBinding(item, "IsSelected", TreeViewItem.IsSelectedProperty);
            return item;
        }

        internal static TreeViewItem SetBinding(TreeViewItem item, string property, DependencyProperty dependencyProperty, IValueConverter converter = null)
        {
            Binding binding = new Binding(property);
            binding.Converter = converter;
            binding.Mode = BindingMode.TwoWay;
            item.SetBinding(dependencyProperty, binding);
            return item;
        }
    }
}
