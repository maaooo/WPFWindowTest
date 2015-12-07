using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TestRx3 {

    
    public class Win32
    {
        // Posted when the user presses the left mouse button while the cursor is within the nonclient area of a window  
        public const int WM_NCLBUTTONDOWN = 0x00A1;

        // Sends the specified message to a window or windows  
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);  
        // Sent to a window in order to determine what part of the window corresponds to a particular screen coordinate  
        public const int WM_NCHITTEST = 0x0084;

        /// <summary>  
        /// Indicates the position of the cursor hot spot.  
        /// </summary>  
        public enum HitTest : int
        {
            /// <summary>  
            /// On the screen background or on a dividing line between windows (same as HTNOWHERE, except that the DefWindowProc function produces a system beep to indicate an error).  
            /// </summary>  
            HTERROR = -2,

            /// <summary>  
            /// In a window currently covered by another window in the same thread (the message will be sent to underlying windows in the same thread until one of them returns a code that is not HTTRANSPARENT).  
            /// </summary>  
            HTTRANSPARENT = -1,

            /// <summary>  
            /// On the screen background or on a dividing line between windows.  
            /// </summary>  
            HTNOWHERE = 0,

            /// <summary>  
            /// In a client area.  
            /// </summary>  
            HTCLIENT = 1,

            /// <summary>  
            /// In a title bar.  
            /// </summary>  
            HTCAPTION = 2,

            /// <summary>  
            /// In a window menu or in a Close button in a child window.  
            /// </summary>  
            HTSYSMENU = 3,

            /// <summary>  
            /// In a size box (same as HTSIZE).  
            /// </summary>  
            HTGROWBOX = 4,

            /// <summary>  
            /// In a size box (same as HTGROWBOX).  
            /// </summary>  
            HTSIZE = 4,

            /// <summary>  
            /// In a menu.  
            /// </summary>  
            HTMENU = 5,

            /// <summary>  
            /// In a horizontal scroll bar.  
            /// </summary>  
            HTHSCROLL = 6,

            /// <summary>  
            /// In the vertical scroll bar.  
            /// </summary>  
            HTVSCROLL = 7,

            /// <summary>  
            /// In a Minimize button.  
            /// </summary>  
            HTMINBUTTON = 8,

            /// <summary>  
            /// In a Minimize button.  
            /// </summary>  
            HTREDUCE = 8,

            /// <summary>  
            /// In a Maximize button.  
            /// </summary>  
            HTMAXBUTTON = 9,

            /// <summary>  
            /// In a Maximize button.  
            /// </summary>  
            HTZOOM = 9,

            /// <summary>  
            /// In the left border of a resizable window (the user can click the mouse to resize the window horizontally).  
            /// </summary>  
            HTLEFT = 10,

            /// <summary>  
            /// In the right border of a resizable window (the user can click the mouse to resize the window horizontally).  
            /// </summary>  
            HTRIGHT = 11,

            /// <summary>  
            /// In the upper-horizontal border of a window.  
            /// </summary>  
            HTTOP = 12,

            /// <summary>  
            /// In the upper-left corner of a window border.  
            /// </summary>  
            HTTOPLEFT = 13,

            /// <summary>  
            /// In the upper-right corner of a window border.  
            /// </summary>  
            HTTOPRIGHT = 14,

            /// <summary>  
            /// In the lower-horizontal border of a resizable window (the user can click the mouse to resize the window vertically).  
            /// </summary>  
            HTBOTTOM = 15,

            /// <summary>  
            /// In the lower-left corner of a border of a resizable window (the user can click the mouse to resize the window diagonally).  
            /// </summary>  
            HTBOTTOMLEFT = 16,

            /// <summary>  
            /// In the lower-right corner of a border of a resizable window (the user can click the mouse to resize the window diagonally).  
            /// </summary>  
            HTBOTTOMRIGHT = 17,

            /// <summary>  
            /// In the border of a window that does not have a sizing border.  
            /// </summary>  
            HTBORDER = 18,

            /// <summary>  
            /// In a Close button.  
            /// </summary>  
            HTCLOSE = 20,

            /// <summary>  
            /// In a Help button.  
            /// </summary>  
            HTHELP = 21,
        }; 
        // Sent to a window when the size or position of the window is about to change  
        public const int WM_GETMINMAXINFO = 0x0024;

        // Retrieves a handle to the display monitor that is nearest to the window  
        public const int MONITOR_DEFAULTTONEAREST = 2;

        // Retrieves a handle to the display monitor  
        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, int dwFlags);

        // RECT structure, Rectangle used by MONITORINFOEX  
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        // MONITORINFOEX structure, Monitor information used by GetMonitorInfo function  
        [StructLayout(LayoutKind.Sequential)]
        public class MONITORINFOEX
        {
            public int cbSize;
            public RECT rcMonitor; // The display monitor rectangle  
            public RECT rcWork; // The working area rectangle  
            public int dwFlags;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x20)]
            public char[] szDevice;
        }

        // Point structure, Point information used by MINMAXINFO structure  
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        // MINMAXINFO structure, Window's maximum size and position information  
        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize; // The maximized size of the window  
            public POINT ptMaxPosition; // The position of the maximized window  
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        // Get the working area of the specified monitor  
        [DllImport("user32.dll")]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] MONITORINFOEX monitorInfo);
    }  
    public class NativeMethods {

        /// <summary> 

        /// 带有外边框和标题的windows的样式 

        /// </summary> 

        public const long WS_CAPTION = 0x00C00000L;

        public const long WS_CAPTION_2 = 0X00C0000L;

        // public const long WS_BORDER = 0X0080000L; 

        /// <summary> 

        /// window 扩展样式 分层显示 

        /// </summary> 

        public const long WS_EX_LAYERED = 0x00080000L;

        public const long WS_CHILD = 0x40000000L;

        /// <summary> 

        /// 带有alpha的样式 

        /// </summary> 

        public const long LWA_ALPHA = 0x00000002L;

        /// <summary> 

        /// 颜色设置 

        /// </summary> 

        public const long LWA_COLORKEY = 0x00000001L;

        /// <summary> 

        /// window的基本样式 

        /// </summary> 

        public const int GWL_STYLE = -16;

        /// <summary> 

        /// window的扩展样式 

        /// </summary> 

        public const int GWL_EXSTYLE = -20;

        /// <summary> 

        /// 设置窗体的样式 

        /// </summary> 

        /// <param name="handle">操作窗体的句柄</param> 

        /// <param name="oldStyle">进行设置窗体的样式类型.</param> 

        /// <param name="newStyle">新样式</param> 

        [System.Runtime.InteropServices.DllImport("User32.dll")]

        public static extern void SetWindowLong(IntPtr handle, int oldStyle, long newStyle);

        /// <summary> 

        /// 获取窗体指定的样式. 

        /// </summary> 

        /// <param name="handle">操作窗体的句柄</param> 

        /// <param name="style">要进行返回的样式</param> 

        /// <returns>当前window的样式</returns> 

        [System.Runtime.InteropServices.DllImport("User32.dll")]

        public static extern long GetWindowLong(IntPtr handle, int style);

        /// <summary> 

        /// 设置窗体的工作区域. 

        /// </summary> 

        /// <param name="handle">操作窗体的句柄.</param> 

        /// <param name="handleRegion">操作窗体区域的句柄.</param> 

        /// <param name="regraw">if set to <c>true</c> [regraw].</param> 

        /// <returns>返回值</returns> 

        [System.Runtime.InteropServices.DllImport("User32.dll")]

        public static extern int SetWindowRgn(IntPtr handle, IntPtr handleRegion, bool regraw);

        /// <summary> 

        /// 创建带有圆角的区域. 

        /// </summary> 

        /// <param name="x1">左上角坐标的X值.</param> 

        /// <param name="y1">左上角坐标的Y值.</param> 

        /// <param name="x2">右下角坐标的X值.</param> 

        /// <param name="y2">右下角坐标的Y值.</param> 

        /// <param name="width">圆角椭圆的width.</param> 

        /// <param name="height">圆角椭圆的height.</param> 

        /// <returns>hRgn的句柄</returns> 

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]

        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int width, int height);

        /// <summary> 

        /// Sets the layered window attributes. 

        /// </summary> 

        /// <param name="handle">要进行操作的窗口句柄</param> 

        /// <param name="colorKey">RGB的值</param> 

        /// <param name="alpha">Alpha的值，透明度</param> 

        /// <param name="flags">附带参数</param> 

        /// <returns>true or false</returns> 

        [System.Runtime.InteropServices.DllImport("User32.dll")]

        public static extern bool SetLayeredWindowAttributes(IntPtr handle, ulong colorKey, byte alpha, long flags);

        //=================================================================================

        /// <summary>
        /// 设置窗体为无边框风格
        /// </summary>
        /// <param name="hWnd"></param>
        public static void SetWindowNoBorder(IntPtr hWnd) {
            long oldstyle = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE);

            SetWindowLong(hWnd, GWL_STYLE, oldstyle & (~(WS_CAPTION | WS_CAPTION_2)));
            //SetWindowLong(hWnd, GWL_EXSTYLE, WS_CHILD);
        }

    }
}
