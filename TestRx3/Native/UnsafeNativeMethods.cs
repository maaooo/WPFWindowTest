using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Input;

namespace TestRx3.Native
{
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		[DllImport("User32")]
		internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

		internal static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam, POINT minSize)
		{
			MINMAXINFO mINMAXINFO = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
			IntPtr intPtr = UnsafeNativeMethods.MonitorFromWindow(hwnd, 2);
			if (intPtr != IntPtr.Zero)
			{
				MONITORINFO mONITORINFO = new MONITORINFO();
				UnsafeNativeMethods.GetMonitorInfo(intPtr, mONITORINFO);
				RECT rcWork = mONITORINFO.rcWork;
				RECT rcMonitor = mONITORINFO.rcMonitor;
				mINMAXINFO.ptMaxPosition.x = Math.Abs(rcWork.left - rcMonitor.left);
				mINMAXINFO.ptMaxPosition.y = Math.Abs(rcWork.top - rcMonitor.top);
				mINMAXINFO.ptMaxSize.x = Math.Abs(rcWork.right - rcWork.left);
				mINMAXINFO.ptMaxSize.y = Math.Abs(rcWork.bottom - rcWork.top);
				mINMAXINFO.ptMinTrackSize.x = minSize.x;
				mINMAXINFO.ptMinTrackSize.y = minSize.y;
			}
			Marshal.StructureToPtr(mINMAXINFO, lParam, true);
		}

		internal static MONITORINFO GetMonitorInfo(IntPtr hwnd)
		{
			IntPtr intPtr = UnsafeNativeMethods.MonitorFromWindow(hwnd, 2);
			if (intPtr != IntPtr.Zero)
			{
				MONITORINFO mONITORINFO = new MONITORINFO();
				UnsafeNativeMethods.GetMonitorInfo(intPtr, mONITORINFO);
				return mONITORINFO;
			}
			return null;
		}

		[DllImport("user32.dll")]
		internal static extern IntPtr DefWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		internal static extern IntPtr GetForegroundWindow();

		[DllImport("dwmapi.dll")]
		internal static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

		[DllImport("user32", CharSet = CharSet.Unicode, EntryPoint = "GetMonitorInfoW", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetMonitorInfo([In] IntPtr hMonitor, [Out] MONITORINFO lpmi);

		[DllImport("user32.dll", EntryPoint = "SetClassLong")]
		internal static extern uint SetClassLongPtr32(IntPtr hWnd, int nIndex, uint dwNewLong);

		[SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist", Justification = "This is correct for 64bit Windows.")]
		[DllImport("user32.dll", EntryPoint = "SetClassLongPtr")]
		internal static extern IntPtr SetClassLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		[DllImport("gdi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeleteObject(IntPtr hObject);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SystemParametersInfo(SystemParametersInfoAction uiAction, uint uiParam, ref uint pvParam, uint fWinIni);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

		[DllImport("Kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AttachConsole(int processId);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr LoadLibrary(string lpFileName);

		[DllImport("kernel32.dll")]
		public static extern int WerRegisterMemoryBlock(IntPtr pvAddress, uint dwSize);

		public static bool RegisterHotKey(IntPtr handle, int id, Key key, ModifierKeys modifiers)
		{
			int virtualCode = KeyInterop.VirtualKeyFromKey(key);
			return UnsafeNativeMethods.RegisterHotKey(handle, id, (uint)modifiers, (uint)virtualCode);
		}

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool RegisterHotKey(IntPtr handle, int id, uint modifiers, uint virtualCode);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool UnregisterHotKey(IntPtr handle, int id);
	}
}
