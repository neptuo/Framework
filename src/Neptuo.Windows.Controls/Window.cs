using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UIWindow = System.Windows.Window;

namespace Neptuo.Windows.Controls
{
    public static class Window
    {
        public static Key GetCloseKey(DependencyObject obj)
        {
            return (Key)obj.GetValue(CloseKeyProperty);
        }

        public static void SetCloseKey(DependencyObject obj, Key value)
        {
            obj.SetValue(CloseKeyProperty, value);
        }

        // Using a DependencyProperty as the backing store for CloseKey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseKeyProperty =
            DependencyProperty.RegisterAttached("CloseKey", typeof(Key), typeof(Window), new PropertyMetadata(OnCloseKeyChanged));

        private static void OnCloseKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIWindow window = (UIWindow)d;
            window.PreviewKeyDown += window_PreviewKeyDown;
        }

        private static void window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            UIWindow window = (UIWindow)sender;
            if (e.Key == GetCloseKey(window))
                window.Close();
        }
    }
}