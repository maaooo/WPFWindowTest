using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace TestRx3 {
    public class ImageButton : DependencyObject {
        public static readonly DependencyProperty ImagePressedProperty = DependencyProperty.Register("ImagePressed", typeof(ImageSource), typeof(ImageButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ImageMouseOverProperty = DependencyProperty.Register("ImageMouseOver", typeof(ImageSource), typeof(ImageButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static ImageSource GetImage(DependencyObject obj) {
            return (ImageSource)obj.GetValue(ImageButton.ImageProperty);
        }

        public static ImageSource GetImageMouseOver(DependencyObject obj) {
            return (ImageSource)obj.GetValue(ImageButton.ImageMouseOverProperty);
        }

        public static ImageSource GetImagePressed(DependencyObject obj) {
            return (ImageSource)obj.GetValue(ImageButton.ImagePressedProperty);
        }

        public static void SetImage(DependencyObject obj, ImageSource value) {
            obj.SetValue(ImageButton.ImageProperty, value);
        }

        public static void SetImageMouseOver(DependencyObject obj, ImageSource value) {
            obj.SetValue(ImageButton.ImageMouseOverProperty, value);
        }

        public static void SetImagePressed(DependencyObject obj, ImageSource value) {
            obj.SetValue(ImageButton.ImagePressedProperty, value);
        }
    }
}
