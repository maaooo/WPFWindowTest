using System;
using System.Diagnostics.CodeAnalysis;

namespace TestRx3.Native
{
	[SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Native shit be cray. Best not to fuck with it.")]
	public enum PROCESS_DPI_AWARENESS
	{
		PROCESS_DPI_UNAWARE,
		PROCESS_SYSTEM_DPI_AWARE,
		PROCESS_PER_MONITOR_DPI_AWARE
	}
}
