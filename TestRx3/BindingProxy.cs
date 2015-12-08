using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;

namespace TestRx3 {
    public class BindingProxy : Freezable {
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

        public object Data {
            get {
                return base.GetValue(BindingProxy.DataProperty);
            }
            set {
                base.SetValue(BindingProxy.DataProperty, value);
            }
        }

        protected override Freezable CreateInstanceCore() {
            return new BindingProxy();
        }

        public override string ToString() {
            return "BindingProxy: " + Convert.ToString(this.Data, CultureInfo.CurrentCulture);
        }
    }
}
