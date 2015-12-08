using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;

namespace TestRx3 {
    [Localizability(LocalizationCategory.NeverLocalize)]
    public sealed class BooleanToHiddenVisibilityConverter : ValueConverterMarkupExtension<BooleanToHiddenVisibilityConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
