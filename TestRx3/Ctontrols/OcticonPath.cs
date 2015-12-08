using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TestRx3.Ctontrols {
    public class OcticonPath : Shape {
        private static readonly Lazy<Dictionary<Octicon, Lazy<Geometry>>> cache = new Lazy<Dictionary<Octicon, Lazy<Geometry>>>(new Func<Dictionary<Octicon, Lazy<Geometry>>>(OcticonPath.PrepareCache));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Octicon), typeof(OcticonPath),
            new FrameworkPropertyMetadata(Octicon.mark_github, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        public Octicon Icon {
            get {
                return (Octicon)base.GetValue(OcticonPath.IconProperty);
            }
            set {
                base.SetValue(OcticonPath.IconProperty, value);
            }
        }

        protected override Geometry DefiningGeometry {
            get {
                return OcticonPath.GetGeometryForIcon(this.Icon);
            }
        }

        public static Geometry GetGeometryForIcon(Octicon icon) {
            Dictionary<Octicon, Lazy<Geometry>> value = OcticonPath.cache.Value;
            Lazy<Geometry> lazy;
            if (value.TryGetValue(icon, out lazy)) {
                return lazy.Value;
            }
            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Unknown Octicon: {0}", new object[]
			{
				icon
			}), "icon");
        }

        private static Dictionary<Octicon, Lazy<Geometry>> PrepareCache() {
            Dictionary<Octicon, Lazy<Geometry>> dictionary = new Dictionary<Octicon, Lazy<Geometry>>();
            LazyThreadSafetyMode mode = LazyThreadSafetyMode.None;
            IEnumerator enumerator = Enum.GetValues(typeof(Octicon)).GetEnumerator();
            try {
                while (enumerator.MoveNext()) {
                    Octicon icon = (Octicon)enumerator.Current;
                    dictionary.Add(icon, new Lazy<Geometry>(() => OcticonPath.LoadGeometry(icon), mode));
                }
            }
            finally {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null) {
                    disposable.Dispose();
                }
            }
            return dictionary;
        }

        private static Geometry LoadGeometry(Octicon icon) {
            string text = Enum.GetName(typeof(Octicon), icon);
            if (text == "lock") {
                text = "_lock";
            }
            string str = OcticonPaths.ResourceManager.GetString(text);
            if (str == null) {
                throw new ArgumentException("Could not find octicon geometry for '" + text + "'");
            }
            PathGeometry pathGeometry = PathGeometry.CreateFromGeometry(Geometry.Parse(str));
            if (pathGeometry.CanFreeze) {
                pathGeometry.Freeze();
            }
            return pathGeometry;
        }
    }
}
