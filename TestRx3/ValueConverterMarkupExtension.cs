using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TestRx3 {
    public abstract class ValueConverterMarkupExtension<T> : MarkupExtension, IValueConverter where T : class, IValueConverter, new() {
        private static T converter;

        public override object ProvideValue(IServiceProvider serviceProvider) {
            T arg_19_0;
            if ((arg_19_0 = ValueConverterMarkupExtension<T>.converter) == null) {
                arg_19_0 = (ValueConverterMarkupExtension<T>.converter = Activator.CreateInstance<T>());
            }
            return arg_19_0;
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return DependencyProperty.UnsetValue;
        }
    }
}
