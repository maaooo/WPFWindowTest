using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TestRx3 {
    public class FixedAspectRatioPanel : Panel {
        public static readonly DependencyProperty AspectRatioProperty;

        [TypeConverter(typeof(AspectRatioConverter))]
        public double AspectRatio {
            get {
                return (double)base.GetValue(FixedAspectRatioPanel.AspectRatioProperty);
            }
            set {
                base.SetValue(FixedAspectRatioPanel.AspectRatioProperty, value);
            }
        }

        public HorizontalAlignment HorizontalContentAlignment {
            get {
                return (HorizontalAlignment)base.GetValue(Control.HorizontalContentAlignmentProperty);
            }
            set {
                base.SetValue(Control.HorizontalContentAlignmentProperty, value);
            }
        }

        public VerticalAlignment VerticalContentAlignment {
            get {
                return (VerticalAlignment)base.GetValue(Control.VerticalContentAlignmentProperty);
            }
            set {
                base.SetValue(Control.VerticalContentAlignmentProperty, value);
            }
        }

        static FixedAspectRatioPanel() {
            FixedAspectRatioPanel.AspectRatioProperty = DependencyProperty.Register("AspectRatio", typeof(double), typeof(FixedAspectRatioPanel), new FrameworkPropertyMetadata(1.0) {
                AffectsArrange = true,
                AffectsMeasure = true,
                AffectsRender = true
            });
            Control.HorizontalContentAlignmentProperty.AddOwner(typeof(FixedAspectRatioPanel));
            Control.VerticalContentAlignmentProperty.AddOwner(typeof(FixedAspectRatioPanel));
        }

        private static Size GetMaxSize(Size availableSize, double constraintAspectRatio) {
            double num = availableSize.Height;
            double num2 = availableSize.Width;
            double num3 = num2 / num;
            if (constraintAspectRatio >= num3) {
                num = num2 / constraintAspectRatio;
            }
            else {
                num2 = num * constraintAspectRatio;
            }
            return new Size(num2, num);
        }

        protected override Size MeasureOverride(Size availableSize) {
            Size maxSize = FixedAspectRatioPanel.GetMaxSize(availableSize, this.AspectRatio);
            double num = 0.0;
            double num2 = 0.0;
            foreach (UIElement uIElement in base.InternalChildren) {
                uIElement.Measure(maxSize);
                num = Math.Max(uIElement.DesiredSize.Width, num);
                num2 = Math.Max(uIElement.DesiredSize.Height, num2);
            }
            return new Size(num, num2);
        }

        protected override Size ArrangeOverride(Size finalSize) {
            Size maxSize = FixedAspectRatioPanel.GetMaxSize(finalSize, this.AspectRatio);
            foreach (UIElement uIElement in base.InternalChildren) {
                Point position = FixedAspectRatioPanel.GetPosition(finalSize, maxSize, this.HorizontalContentAlignment, this.VerticalContentAlignment);
                uIElement.Arrange(new Rect(position, maxSize));
            }
            return finalSize;
        }

        private static Point GetPosition(Size outer, Size inner, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment) {
            double x = 0.0;
            double y = 0.0;
            switch (horizontalAlignment) {
                case HorizontalAlignment.Center:
                case HorizontalAlignment.Stretch:
                    x = outer.Width / 2.0 - inner.Width / 2.0;
                    break;
                case HorizontalAlignment.Right:
                    x = outer.Width - inner.Width;
                    break;
            }
            switch (verticalAlignment) {
                case VerticalAlignment.Center:
                case VerticalAlignment.Stretch:
                    y = outer.Height / 2.0 - inner.Height / 2.0;
                    break;
                case VerticalAlignment.Bottom:
                    x = outer.Height - inner.Height;
                    break;
            }
            return new Point(x, y);
        }
    }
}
