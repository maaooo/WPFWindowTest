using System;
using System.Diagnostics.CodeAnalysis;

namespace TestRx3.Native
{
	[SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Native shit be cray. Best not to fuck with it.")]
	public enum MonitorOpts : uint
	{
		MONITOR_DEFAULTTONULL,
		MONITOR_DEFAULTTOPRIMARY,
		MONITOR_DEFAULTTONEAREST
	}
}
