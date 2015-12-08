using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TestRx3 {
    public class AspectRatioConverter : DoubleConverter {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            string text = value as string;
            if (text != null && text.Contains(':')) {
                string[] array = text.Split(new char[]
				{
					':'
				}, 2);
                if (array.Length == 2) {
                    CultureInfo invariantCulture = CultureInfo.InvariantCulture;
                    double num;
                    double num2;
                    if (double.TryParse(array[0], NumberStyles.Float, invariantCulture, out num) && double.TryParse(array[1], NumberStyles.Float, invariantCulture, out num2)) {
                        return num / num2;
                    }
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
