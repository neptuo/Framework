using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using UITextBox = System.Windows.Controls.TextBox;
using UIPasswordBox = System.Windows.Controls.PasswordBox;
using System.Windows.Documents;

namespace Neptuo.Windows.Controls
{
    public static class TextBox
    {
        #region EnterCommand

        public static ICommand GetEnterCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(EnterCommandProperty);
        }

        public static void SetEnterCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(EnterCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnterCommandProperty =
            DependencyProperty.RegisterAttached("EnterCommand", typeof(ICommand), typeof(TextBox), new PropertyMetadata(null, OnEnterCommandChanged));

        private static void OnEnterCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UITextBox textBox = d as UITextBox;
            ICommand newCommand = ((ICommand)e.NewValue);

            if (d == null)
                return;

            if (newCommand != null)
                textBox.PreviewKeyDown += OnEnterCommandPreviewKeyDown;
            else
                textBox.PreviewKeyDown -= OnEnterCommandPreviewKeyDown;
        }

        private static void OnEnterCommandPreviewKeyDown(object sender, KeyEventArgs e)
        {
            UITextBox textBox = sender as UITextBox;
            ICommand newCommand = GetEnterCommand(textBox);

            if (textBox == null || newCommand == null)
                return;

            if (e.Key == Key.Enter)
            {
                BindingExpression binding = textBox.GetBindingExpression(UITextBox.TextProperty);
                if (binding != null)
                    binding.UpdateSource();

                object parameter = GetEnterCommandParameter(textBox);
                if (newCommand.CanExecute(parameter))
                    newCommand.Execute(parameter);
            }
        }


        public static object GetEnterCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(EnterCommandParameterProperty);
        }

        public static void SetEnterCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(EnterCommandParameterProperty, value);
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnterCommandParameterProperty =
            DependencyProperty.RegisterAttached("EnterCommandParameter", typeof(object), typeof(TextBox), new PropertyMetadata(null));

        #endregion

        #region EscapeCommand

        public static ICommand GetEscapeCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(EscapeCommandProperty);
        }

        public static void SetEscapeCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(EscapeCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EscapeCommandProperty =
            DependencyProperty.RegisterAttached("EscapeCommand", typeof(ICommand), typeof(TextBox), new PropertyMetadata(null, OnEscapeCommandChanged));

        private static void OnEscapeCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UITextBox textBox = d as UITextBox;
            ICommand newCommand = ((ICommand)e.NewValue);

            if (d == null)
                return;

            if (newCommand != null)
                textBox.PreviewKeyDown += OnEscapeCommandPreviewKeyDown;
            else
                textBox.PreviewKeyDown -= OnEscapeCommandPreviewKeyDown;
        }

        private static void OnEscapeCommandPreviewKeyDown(object sender, KeyEventArgs e)
        {
            UITextBox textBox = sender as UITextBox;
            ICommand newCommand = GetEnterCommand(textBox);

            if (textBox == null || newCommand == null)
                return;

            if (e.Key == Key.Escape)
            {
                BindingExpression binding = textBox.GetBindingExpression(UITextBox.TextProperty);
                if (binding != null)
                    binding.UpdateSource();

                object parameter = GetEscapeCommandParameter(textBox);
                if (newCommand.CanExecute(parameter))
                    newCommand.Execute(parameter);
            }
        }


        public static object GetEscapeCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(EscapeCommandParameterProperty);
        }

        public static void SetEscapeCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(EscapeCommandParameterProperty, value);
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EscapeCommandParameterProperty =
            DependencyProperty.RegisterAttached("EscapeCommandParameter", typeof(object), typeof(TextBox), new PropertyMetadata(null));

        #endregion


        #region Watermark



        public static object GetWatermark(DependencyObject obj)
        {
            return (object)obj.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, object value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.RegisterAttached("Watermark", typeof(object), typeof(TextBox), new PropertyMetadata(OnWatermarkChanged));

        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Control control = (Control)d;
            control.Loaded += Control_Loaded;

            control.GotKeyboardFocus += Control_GotKeyboardFocus;
            control.LostKeyboardFocus += Control_Loaded;
        }



        /// <summary>
        /// Handle the GotFocus event on the control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
        private static void Control_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            Control c = (Control)sender;
            if (ShouldShowWatermark(c))
            {
                RemoveWatermark(c);
            }
        }

        /// <summary>
        /// Handle the Loaded and LostFocus event on the control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
        private static void Control_Loaded(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            if (ShouldShowWatermark(control))
            {
                ShowWatermark(control);
            }
        }

        /// <summary>
        /// Remove the watermark from the specified element
        /// </summary>
        /// <param name="control">Element to remove the watermark from</param>
        private static void RemoveWatermark(UIElement control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);

            // layer could be null if control is no longer in the visual tree
            if (layer != null)
            {
                Adorner[] adorners = layer.GetAdorners(control);
                if (adorners == null)
                {
                    return;
                }

                foreach (Adorner adorner in adorners)
                {
                    if (adorner is WatermarkAdorner)
                    {
                        adorner.Visibility = Visibility.Hidden;
                        layer.Remove(adorner);
                    }
                }
            }
        }

        /// <summary>
        /// Show the watermark on the specified control
        /// </summary>
        /// <param name="control">Control to show the watermark on</param>
        private static void ShowWatermark(Control control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);

            // layer could be null if control is no longer in the visual tree
            if (layer != null)
            {
                object watermark = GetWatermark(control);
                if (watermark is String)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.FontStyle = FontStyles.Italic;
                    textBlock.Text = watermark.ToString();
                    watermark = textBlock;
                }

                layer.Add(new WatermarkAdorner(control, watermark, new Thickness(4, 0, 4, 0)));
            }
        }

        /// <summary>
        /// Indicates whether or not the watermark should be shown on the specified control
        /// </summary>
        /// <param name="c"><see cref="Control"/> to test</param>
        /// <returns>true if the watermark should be shown; false otherwise</returns>
        private static bool ShouldShowWatermark(Control c)
        {
            if (c is UITextBox)
            {
                return (c as UITextBox).Text == string.Empty;
            }
            else if (c is UIPasswordBox)
            {
                return (c as UIPasswordBox).Password == string.Empty;
            }
            else
            {
                return false;
            }
        }


        #endregion
    }
}
