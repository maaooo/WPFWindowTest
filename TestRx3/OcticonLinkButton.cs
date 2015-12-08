using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using TestRx3.Ctontrols;

namespace TestRx3 {
    public class OcticonLinkButton : OcticonButton {
        public static readonly DependencyProperty IconHeightProperty;

        public static readonly DependencyProperty IconWidthProperty;

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

        public int IconHeight {
            get {
                return (int)base.GetValue(OcticonLinkButton.IconHeightProperty);
            }
            set {
                base.SetValue(OcticonLinkButton.IconHeightProperty, value);
            }
        }

        public int IconWidth {
            get {
                return (int)base.GetValue(OcticonLinkButton.IconWidthProperty);
            }
            set {
                base.SetValue(OcticonLinkButton.IconWidthProperty, value);
            }
        }

        static OcticonLinkButton() {
            OcticonLinkButton.IconHeightProperty = DependencyProperty.Register("IconHeight", typeof(int), typeof(OcticonLinkButton), new PropertyMetadata(16));
            OcticonLinkButton.IconWidthProperty = DependencyProperty.Register("IconWidth", typeof(int), typeof(OcticonLinkButton), new PropertyMetadata(16));
            OcticonPath.IconProperty.AddOwner(typeof(OcticonLinkButton), new FrameworkPropertyMetadata(new PropertyChangedCallback(OcticonLinkButton.OnIconChanged)));
            Path.DataProperty.AddOwner(typeof(OcticonLinkButton));
        }

        public OcticonLinkButton() {
            base.SetValue(Path.DataProperty, OcticonPath.GetGeometryForIcon(this.Icon));
        }

        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            d.SetValue(Path.DataProperty, OcticonPath.GetGeometryForIcon((Octicon)e.NewValue));
        }
    }
}
