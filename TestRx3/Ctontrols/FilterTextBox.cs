using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace TestRx3.Ctontrols {
    public class FilterTextBox : TextBox {
        public static readonly DependencyProperty PromptTextProperty = DependencyProperty.Register("PromptText", typeof(string), typeof(FilterTextBox), new UIPropertyMetadata("Filter"));

        [DefaultValue("Filter"), Localizability(LocalizationCategory.Text)]
        public string PromptText {
            get {
                return (string)base.GetValue(FilterTextBox.PromptTextProperty);
            }
            set {
                base.SetValue(FilterTextBox.PromptTextProperty, value);
            }
        }

        public ICommand ClearCommand {
            get;
            private set;
        }

        public FilterTextBox() {
            base.AddHandler(UIElement.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(FilterTextBox.SelectivelyIgnoreMouseButton), true);
            base.AddHandler(UIElement.GotKeyboardFocusEvent, new RoutedEventHandler(FilterTextBox.SelectAllText), true);
            base.AddHandler(Control.MouseDoubleClickEvent, new RoutedEventHandler(FilterTextBox.SelectAllText), true);
            base.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(this.ClearButtonClick), true);
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.Key == Key.Escape && !string.IsNullOrEmpty(base.Text)) {
                base.Clear();
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e) {
            base.Clear();
            e.Handled = true;
        }

        private static void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e) {
            TextBox textBox = FilterTextBox.FindTextBoxInAncestors(e.OriginalSource as UIElement);
            if (textBox != null && !textBox.IsKeyboardFocusWithin) {
                textBox.Focus();
                e.Handled = true;
            }
        }

        private static TextBox FindTextBoxInAncestors(DependencyObject current) {
            while (current != null) {
                TextBox textBox = current as TextBox;
                if (textBox != null) {
                    return textBox;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }

        private static void SelectAllText(object sender, RoutedEventArgs e) {
            TextBox textBox = e.OriginalSource as TextBox;
            if (textBox != null) {
                textBox.SelectAll();
            }
        }
    }
}
