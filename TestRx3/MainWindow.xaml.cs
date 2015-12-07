using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
            this.SourceInitialized += MainWindow_SourceInitialized;
            this.StateChanged += MainWindow_StateChanged;
            this.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
           // this.BorderThickness = new System.Windows.Thickness(customBorderThickness);
        }
        void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Grid || e.OriginalSource is Border || e.OriginalSource is Window)
            {
                WindowInteropHelper wih = new WindowInteropHelper(this);
                Win32.SendMessage(wih.Handle, Win32.WM_NCLBUTTONDOWN, (int)Win32.HitTest.HTCAPTION, 0);
                return;
            }
        }
        private readonly int customBorderThickness = 7;
        void MainWindow_StateChanged(object sender, EventArgs e)
        {
//             if (WindowState == WindowState.Maximized)
//             {
//                 this.BorderThickness = new System.Windows.Thickness(0);
//             }
//             else
//             {
//                 this.BorderThickness = new System.Windows.Thickness(0);
//             }
        }
        void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            if (source == null)
                // Should never be null  
                throw new Exception("Cannot get HwndSource instance.");

            source.AddHook(new HwndSourceHook(this.WndProc));
        }
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case Win32.WM_GETMINMAXINFO: // WM_GETMINMAXINFO message  
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
                case Win32.WM_NCHITTEST: // WM_NCHITTEST message  
                    return WmNCHitTest(lParam, ref handled); 
            }

            return IntPtr.Zero;
        }
        private readonly int cornerWidth = 8;  
  
        /// <summary>  
        /// Mouse point used by HitTest  
        /// </summary>  
        private Point mousePoint = new Point();
        private IntPtr WmNCHitTest(IntPtr lParam, ref bool handled)
        {
            // Update cursor point  
            // The low-order word specifies the x-coordinate of the cursor.  
            // #define GET_X_LPARAM(lp) ((int)(short)LOWORD(lp))  
            this.mousePoint.X = (int)(short)(lParam.ToInt32() & 0xFFFF);
            // The high-order word specifies the y-coordinate of the cursor.  
            // #define GET_Y_LPARAM(lp) ((int)(short)HIWORD(lp))  
            this.mousePoint.Y = (int)(short)(lParam.ToInt32() >> 16);

            // Do hit test  
            handled = true;
            if (Math.Abs(this.mousePoint.Y - this.Top) <= this.cornerWidth
                && Math.Abs(this.mousePoint.X - this.Left) <= this.cornerWidth)
            { // Top-Left  
                return new IntPtr((int)Win32.HitTest.HTTOPLEFT);
            }
            else if (Math.Abs(this.ActualHeight + this.Top - this.mousePoint.Y) <= this.cornerWidth
                && Math.Abs(this.mousePoint.X - this.Left) <= this.cornerWidth)
            { // Bottom-Left  
                return new IntPtr((int)Win32.HitTest.HTBOTTOMLEFT);
            }
            else if (Math.Abs(this.mousePoint.Y - this.Top) <= this.cornerWidth
                && Math.Abs(this.ActualWidth + this.Left - this.mousePoint.X) <= this.cornerWidth)
            { // Top-Right  
                return new IntPtr((int)Win32.HitTest.HTTOPRIGHT);
            }
            else if (Math.Abs(this.ActualWidth + this.Left - this.mousePoint.X) <= this.cornerWidth
                && Math.Abs(this.ActualHeight + this.Top - this.mousePoint.Y) <= this.cornerWidth)
            { // Bottom-Right  
                return new IntPtr((int)Win32.HitTest.HTBOTTOMRIGHT);
            }
            else if (Math.Abs(this.mousePoint.X - this.Left) <= this.customBorderThickness)
            { // Left  
                return new IntPtr((int)Win32.HitTest.HTLEFT);
            }
            else if (Math.Abs(this.ActualWidth + this.Left - this.mousePoint.X) <= this.customBorderThickness)
            { // Right  
                return new IntPtr((int)Win32.HitTest.HTRIGHT);
            }
            else if (Math.Abs(this.mousePoint.Y - this.Top) <= this.customBorderThickness)
            { // Top  
                return new IntPtr((int)Win32.HitTest.HTTOP);
            }
            else if (Math.Abs(this.ActualHeight + this.Top - this.mousePoint.Y) <= this.customBorderThickness)
            { // Bottom  
                return new IntPtr((int)Win32.HitTest.HTBOTTOM);
            }
            else
            {
                handled = false;
                return IntPtr.Zero;
            }
        }
        private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            // MINMAXINFO structure  
            Win32.MINMAXINFO mmi = (Win32.MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(Win32.MINMAXINFO));

            // Get handle for nearest monitor to this window  
            WindowInteropHelper wih = new WindowInteropHelper(this);
            IntPtr hMonitor = Win32.MonitorFromWindow(wih.Handle, Win32.MONITOR_DEFAULTTONEAREST);

            // Get monitor info  
            Win32.MONITORINFOEX monitorInfo = new Win32.MONITORINFOEX();
            monitorInfo.cbSize = Marshal.SizeOf(monitorInfo);
            Win32.GetMonitorInfo(new HandleRef(this, hMonitor), monitorInfo);

            // Get HwndSource  
            HwndSource source = HwndSource.FromHwnd(wih.Handle);
            if (source == null)
                // Should never be null  
                throw new Exception("Cannot get HwndSource instance.");
            if (source.CompositionTarget == null)
                // Should never be null  
                throw new Exception("Cannot get HwndTarget instance.");

            // Get transformation matrix  
            Matrix matrix = source.CompositionTarget.TransformFromDevice;

            // Convert working area  
            Win32.RECT workingArea = monitorInfo.rcWork;
            Point dpiIndependentSize =
                matrix.Transform(new Point(
                        workingArea.Right - workingArea.Left,
                        workingArea.Bottom - workingArea.Top
                        ));

            // Convert minimum size  
            Point dpiIndenpendentTrackingSize = matrix.Transform(new Point(
                this.MinWidth,
                this.MinHeight
                ));

            // Set the maximized size of the window  
            mmi.ptMaxSize.x = (int)dpiIndependentSize.X;
            mmi.ptMaxSize.y = (int)dpiIndependentSize.Y;

            // Set the position of the maximized window  
            mmi.ptMaxPosition.x = 0;
            mmi.ptMaxPosition.y = 0;

            // Set the minimum tracking size  
            mmi.ptMinTrackSize.x = (int)dpiIndenpendentTrackingSize.X;
            mmi.ptMinTrackSize.y = (int)dpiIndenpendentTrackingSize.Y;

            Marshal.StructureToPtr(mmi, lParam, true);
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

        private void MouseLeftButtonDown_Click(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount==2)
                base.WindowState = ((base.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal);
        }
    }
}
