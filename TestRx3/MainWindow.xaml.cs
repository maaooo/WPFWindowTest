using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestRx3 {
    public class PiCalculator {
        private readonly Subject<int> resultStream = new Subject<int>();
        public IObservable<int> ResultStream {
            get { return resultStream; }
        }
        public void Start() {
            // Whatever the algorithm actually is
            for (int i = 0; i < 100000; i++) {
                resultStream.OnNext(i);
            }
        }
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
//             TestProgress.Maximum = 100000;
//             for (int i = 0; i < 100000; i++)
//             {
//                 Thread.Sleep(100);
//                 TestProgress.Value = i;
//                 Console.WriteLine(i);
//             }
        }

        private void testRx_Click(object sender, RoutedEventArgs e)
        {
//             IObservable<Int32> input = Observable.Range(1, 100000);
//             input.Subscribe(x => { 
//                 Console.WriteLine("x:{0} ", x) ;
//                 TestProgress.Value = x;});
            var piCalculator = new PiCalculator();
            piCalculator.ResultStream.Subscribe(n => Console.WriteLine((n)));
            piCalculator.Start();
        }

        private void Windwo_loaded(object sender, RoutedEventArgs e) {
            // 获取窗体句柄  
            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            NativeMethods.SetWindowNoBorder(hwnd);
        }

        private void mouseMove(object sender, MouseEventArgs e) {
            if(e.LeftButton==MouseButtonState.Pressed)
            this.DragMove();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Restore(object sender, RoutedEventArgs e) {
            base.WindowState = ((base.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal);
 
        }

        private void Minimize(object sender, RoutedEventArgs e) {
            base.WindowState = WindowState.Minimized;
        }
        private void UpdateDwmBorder() {
            if (base.WindowState == WindowState.Maximized) {
                this.noDwmBorder.Visibility = Visibility.Hidden;
                return;
            }
           // bool isDwmEnabled = BorderlessWindowBehavior.IsDwmEnabled;
           // bool isDropShadowEnabled = BorderlessWindowBehavior.IsDropShadowEnabled;
           // this.noDwmBorder.Visibility = ((isDwmEnabled && isDropShadowEnabled && base.IsActive) ? Visibility.Hidden : Visibility.Visible);
        }

        [DebuggerStepThrough]
        private void WindowResized(bool maximized) {
            this.MaximizeButton.Visibility = (maximized ? Visibility.Hidden : Visibility.Visible);
            this.RestoreButton.Visibility = (maximized ? Visibility.Visible : Visibility.Hidden);
            this.frameGrid.IsHitTestVisible = !maximized;
            this.UpdateDwmBorder();
        }

        private void HandleRectanglePreviewMouseDown(object sender, MouseButtonEventArgs e) {

        }

        private void HandleRectangleMouseMove(object sender, MouseEventArgs e) {

        }
    }
}
