using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TestRx3.Ctontrols {
    public class OcticonCircleButton : OcticonButton {
        public static readonly DependencyProperty ShowSpinnerProperty;

        public static readonly DependencyProperty IconForegroundProperty;

        public static readonly DependencyProperty ActiveBackgroundProperty;

        public static readonly DependencyProperty ActiveForegroundProperty;

        public static readonly DependencyProperty PressedBackgroundProperty;

        public static readonly DependencyProperty IconSizeProperty;

        public static readonly DependencyProperty IconProperty;

        public bool ShowSpinner {
            get {
                return (bool)base.GetValue(OcticonCircleButton.ShowSpinnerProperty);
            }
            set {
                base.SetValue(OcticonCircleButton.ShowSpinnerProperty, value);
            }
        }

        public Brush IconForeground {
            get {
                return (Brush)base.GetValue(OcticonCircleButton.IconForegroundProperty);
            }
            set {
                base.SetValue(OcticonCircleButton.IconForegroundProperty, value);
            }
        }

        public Brush ActiveBackground {
            get {
                return (Brush)base.GetValue(OcticonCircleButton.ActiveBackgroundProperty);
            }
            set {
                base.SetValue(OcticonCircleButton.ActiveBackgroundProperty, value);
            }
        }

        public Brush ActiveForeground {
            get {
                return (Brush)base.GetValue(OcticonCircleButton.ActiveForegroundProperty);
            }
            set {
                base.SetValue(OcticonCircleButton.ActiveForegroundProperty, value);
            }
        }

        public Brush PressedBackground {
            get {
                return (Brush)base.GetValue(OcticonCircleButton.PressedBackgroundProperty);
            }
            set {
                base.SetValue(OcticonCircleButton.PressedBackgroundProperty, value);
            }
        }

        public double IconSize {
            get {
                return (double)base.GetValue(OcticonCircleButton.IconSizeProperty);
            }
            set {
                base.SetValue(OcticonCircleButton.IconSizeProperty, value);
            }
        }

        public Octicon Icon {
            get {
                return (Octicon)base.GetValue(OcticonPath.IconProperty);
            }
            set {
                base.SetValue(OcticonPath.IconProperty, value);
            }
        }

        public Geometry Data {
            get {
                return (Geometry)base.GetValue(Path.DataProperty);
            }
            set {
                base.SetValue(Path.DataProperty, value);
            }
        }

        static OcticonCircleButton() {
            OcticonCircleButton.ShowSpinnerProperty = DependencyProperty.Register("ShowSpinner", typeof(bool), typeof(OcticonCircleButton));
            OcticonCircleButton.IconForegroundProperty = DependencyProperty.Register("IconForeground", typeof(Brush), typeof(OcticonCircleButton));
            OcticonCircleButton.ActiveBackgroundProperty = DependencyProperty.Register("ActiveBackground", typeof(Brush), typeof(OcticonCircleButton));
            OcticonCircleButton.ActiveForegroundProperty = DependencyProperty.Register("ActiveForeground", typeof(Brush), typeof(OcticonCircleButton));
            OcticonCircleButton.PressedBackgroundProperty = DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(OcticonCircleButton));
            OcticonCircleButton.IconSizeProperty = DependencyProperty.Register("IconSize", typeof(double), typeof(OcticonCircleButton), new FrameworkPropertyMetadata(16.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));
            OcticonCircleButton.IconProperty = DependencyProperty.Register("Icon", typeof(Octicon), typeof(OcticonCircleButton), new FrameworkPropertyMetadata(Octicon.mark_github, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OcticonCircleButton.OnIconChanged)));
            Path.DataProperty.AddOwner(typeof(OcticonCircleButton));
        }

        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            d.SetValue(Path.DataProperty, OcticonPath.GetGeometryForIcon((Octicon)e.NewValue));
        }
    }
}
