using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace TestRx3.Native
{
	[SuppressUnmanagedCodeSecurity]
	internal static class SafeNativeMethods
	{
		[DllImport("dwmapi.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DwmIsCompositionEnabled();

		[DllImport("gdi32.dll")]
		internal static extern IntPtr CreateSolidBrush(int crColor);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		[DllImport("kernel32.dll")]
		internal static extern IntPtr GetCurrentProcess();

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ChangeWindowMessageFilter(uint msg, ChangeWindowMessageFilterFlags flags);

		[SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist", Justification = "We don't have these entry points on our build servers")]
		[DllImport("shcore.dll")]
		internal static extern uint SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);

		[DllImport("user32.dll")]
		internal static extern int SetProcessDPIAware();

		[SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist", Justification = "We don't have these entry points on our build servers")]
		[DllImport("shcore.dll")]
		internal static extern int GetProcessDpiAwareness(IntPtr hWnd, out PROCESS_DPI_AWARENESS awareness);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

		[DllImport("gdi32.dll")]
		internal static extern int GetDeviceCaps(IntPtr hdc, DeviceCaps nIndex);

		[DllImport("user32.dll")]
		internal static extern IntPtr MonitorFromWindow(IntPtr hwnd, MonitorOpts dwFlags);

		[SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist", Justification = "We don't have these entry points on our build servers")]
		[DllImport("shcore.dll")]
		internal static extern uint GetDpiForMonitor(IntPtr hmonitor, MonitorDpiType dpiType, out uint dpiX, out uint dpiY);
	}
}
