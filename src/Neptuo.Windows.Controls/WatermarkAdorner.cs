using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Neptuo.Windows.Controls
{
    /// <summary>
    /// Adorner for the watermark
    /// </summary>
    internal class WatermarkAdorner : Adorner
    {
        private readonly ContentPresenter contentPresenter;

        public WatermarkAdorner(UIElement adornedElement, object watermark, Thickness margin) :
            base(adornedElement)
        {
            IsHitTestVisible = false;

            contentPresenter = new ContentPresenter();
            contentPresenter.Content = watermark;
            contentPresenter.Opacity = 0.5;
            contentPresenter.Margin = margin;

            if (this.Control is ItemsControl && !(this.Control is ComboBox))
            {
                contentPresenter.VerticalAlignment = VerticalAlignment.Center;
                contentPresenter.HorizontalAlignment = HorizontalAlignment.Center;
            }

            // Hide the control adorner when the adorned element is hidden
            Binding binding = new Binding("IsVisible");
            binding.Source = adornedElement;
            binding.Converter = new BooleanToVisibilityConverter();
            SetBinding(VisibilityProperty, binding);
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        private Control Control
        {
            get { return (Control)this.AdornedElement; }
        }


        protected override Visual GetVisualChild(int index)
        {
            return contentPresenter;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            contentPresenter.Measure(Control.RenderSize);
            return Control.RenderSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            contentPresenter.Arrange(new Rect(finalSize));
            return finalSize;
        }
    }
}
