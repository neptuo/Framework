using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Neptuo.Windows.Controls
{
    public static class PasswordBox
    {
        public static readonly DependencyProperty Password = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBox), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static readonly DependencyProperty Bind = DependencyProperty.RegisterAttached("Bind", typeof(bool), typeof(PasswordBox), new PropertyMetadata(false, OnBindPasswordChanged));

        private static readonly DependencyProperty Updating =
            DependencyProperty.RegisterAttached("Updating", typeof(bool), typeof(PasswordBox), new PropertyMetadata(false));

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            System.Windows.Controls.PasswordBox box = d as System.Windows.Controls.PasswordBox;

            // only handle this event when the property is attached to a PasswordBox
            // and when the BindPassword attached property has been set to true
            if (d == null || !GetBind(d))
                return;

            // avoid recursive updating by ignoring the box's changed event
            box.PasswordChanged -= HandlePasswordChanged;

            string newPassword = (string)e.NewValue;

            if (!GetUpdating(box))
                box.Password = newPassword;

            box.PasswordChanged += HandlePasswordChanged;
        }

        private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            // when the BindPassword attached property is set on a PasswordBox,
            // start listening to its PasswordChanged event

            System.Windows.Controls.PasswordBox box = dp as System.Windows.Controls.PasswordBox;

            if (box == null)
                return;

            bool wasBound = (bool)(e.OldValue);
            bool needToBind = (bool)(e.NewValue);

            if (wasBound)
                box.PasswordChanged -= HandlePasswordChanged;

            if (needToBind)
                box.PasswordChanged += HandlePasswordChanged;
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.PasswordBox box = sender as System.Windows.Controls.PasswordBox;

            // set a flag to indicate that we're updating the password
            SetUpdating(box, true);
            // push the new password into the BoundPassword property
            SetPassword(box, box.Password);
            SetUpdating(box, false);
        }

        public static void SetBind(DependencyObject dp, bool value)
        {
            dp.SetValue(Bind, value);
        }

        public static bool GetBind(DependencyObject dp)
        {
            return (bool)dp.GetValue(Bind);
        }

        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(Password);
        }

        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(Password, value);
        }

        private static bool GetUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(Updating);
        }

        private static void SetUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(Updating, value);
        }
    }
}
