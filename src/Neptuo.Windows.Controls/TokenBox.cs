using Neptuo.Observables.Collections.Features;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UIListView = System.Windows.Controls.ListView;
using UIListViewItem = System.Windows.Controls.ListViewItem;
using UITextBox = System.Windows.Controls.TextBox;

namespace Neptuo.Windows.Controls
{
    [TemplatePart(Name = TokenBox.TextBoxPart, Type = typeof(UITextBox))]
    [TemplatePart(Name = TokenBox.ListViewPart, Type = typeof(UIListView))]
    public class TokenBox : Control
    {
        public const string TextBoxPart = "PART_TextBox";
        public const string ListViewPart = "PART_ListView";

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Value should implement <see cref="IRemoveAtCollection"/> and <see cref="ICountCollection"/>.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(TokenBox), new PropertyMetadata(null));
        
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(TokenBox), new PropertyMetadata(null));


        public Key SeparatorKey
        {
            get { return (Key)GetValue(SeparatorKeyProperty); }
            set { SetValue(SeparatorKeyProperty, value); }
        }

        public static readonly DependencyProperty SeparatorKeyProperty =
            DependencyProperty.Register("SeparatorKey", typeof(Key), typeof(TokenBox), new PropertyMetadata(Key.Enter));




        public static readonly RoutedEvent CreateItemEvent =
            EventManager.RegisterRoutedEvent("CreateItem", RoutingStrategy.Bubble, typeof(CreateItemEventHandler), typeof(TokenBox));

        public event RoutedEventHandler CreateItem
        {
            add { AddHandler(CreateItemEvent, value); }
            remove { RemoveHandler(CreateItemEvent, value); }
        }


        private UITextBox tbxName;
        private UIListView lvwItems;

        static TokenBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TokenBox), new FrameworkPropertyMetadata(typeof(TokenBox)));
        }

        public TokenBox()
        {
            Focusable = true;
            GotFocus += TokenBox_GotFocus;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            tbxName = (UITextBox)base.GetTemplateChild(TextBoxPart);
            tbxName.PreviewKeyUp += tbxName_PreviewKeyUp;

            lvwItems = (UIListView)base.GetTemplateChild(ListViewPart);
            lvwItems.PreviewKeyUp += lvwItems_PreviewKeyUp;
            //lvwItems.ItemTemplate = ItemTemplate;
        }

        private void TokenBox_GotFocus(object sender, RoutedEventArgs e)
        {
            FocusManager.SetFocusedElement(this, tbxName);
        }

        private void tbxName_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            ICountCollection countSource = (ICountCollection)ItemsSource;
            IRemoveAtCollection removeSource = (IRemoveAtCollection)ItemsSource;

            if (e.Key == SeparatorKey && !String.IsNullOrEmpty(tbxName.Text))
            {
                RaiseEvent(new CreateItemEventArgs(CreateItemEvent, this, tbxName.Text));

                //ViewModel.Items.Add(new CompletionItem { Name = textBox.Text });
                tbxName.Text = String.Empty;
            }
            else if (e.Key == Key.Back && String.IsNullOrEmpty(tbxName.Text) && countSource.Count > 0)
            {
                removeSource.RemoveAt(countSource.Count - 1);
            }
            else if (e.Key == Key.Left && tbxName.CaretIndex == 0)
            {
                lvwItems.Focus();
                lvwItems.SelectedIndex = countSource.Count - 1;
                UIListViewItem listViewItem = (UIListViewItem)lvwItems.ItemContainerGenerator.ContainerFromIndex(lvwItems.SelectedIndex);
                listViewItem.Focus();

                e.Handled = true;
            }
        }

        private void lvwItems_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            ICountCollection countSource = (ICountCollection)ItemsSource;
            IRemoveAtCollection removeSource = (IRemoveAtCollection)ItemsSource;

            if ((e.Key == Key.Back || e.Key == Key.Delete) && lvwItems.SelectedItem != null)
            {
                removeSource.RemoveAt(lvwItems.SelectedIndex);
                tbxName.Focus();
            }
            else if (e.Key == Key.Right && lvwItems.SelectedIndex == (countSource.Count - 1))
            {
                tbxName.Focus();
            }
        }
    }

    public delegate void CreateItemEventHandler(object sender, CreateItemEventArgs e);

    public class CreateItemEventArgs : RoutedEventArgs
    {
        public string Value { get; private set; }

        public CreateItemEventArgs(RoutedEvent routedEvent, object source, string value)
            : base(routedEvent, source)
        {
            Value = value;
        }
    }
}
