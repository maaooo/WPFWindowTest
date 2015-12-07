using System;
using System.Runtime.InteropServices;

namespace TestRx3.Native
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class MONITORINFO
	{
		public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));

		public RECT rcMonitor;

		public RECT rcWork;

		public int dwFlags;
	}
}
