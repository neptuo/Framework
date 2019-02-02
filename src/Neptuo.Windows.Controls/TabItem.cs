using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using UITabItem = System.Windows.Controls.TabItem;

namespace Neptuo.Windows.Controls
{
    public static class TabItem
    {
        public static Control GetFocus(DependencyObject obj)
        {
            return (Control)obj.GetValue(FocusProperty);
        }

        public static void SetFocus(DependencyObject obj, Control value)
        {
            obj.SetValue(FocusProperty, value);
        }

        public static readonly DependencyProperty FocusProperty =
            DependencyProperty.RegisterAttached("Focus", typeof(Control), typeof(TabItem), new PropertyMetadata(OnFocusChanged));


        private static void OnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d))
                return;

            if (d is UITabItem tabItem)
            {
                TabControl tabControl = ControlHelper.FindVisualParent<TabControl>(tabItem);
                SelectionChangedEventHandler selectionChanged = (sender, args) =>
                {
                    if (tabItem.IsSelected)
                        Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => GetFocus(tabItem).Focus()));
                };

                if (e.NewValue != null)
                    tabControl.SelectionChanged += selectionChanged;
                else
                    tabControl.SelectionChanged -= selectionChanged;
            }
        }
    }
}
