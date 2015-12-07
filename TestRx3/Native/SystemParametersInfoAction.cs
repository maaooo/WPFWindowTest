using System;
using System.Diagnostics.CodeAnalysis;

namespace TestRx3.Native
{
	[SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Native shit be cray. Best not to fuck with it."), SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Native shit be cray. Best not to fuck with it.")]
	public enum SystemParametersInfoAction : uint
	{
		SPI_GETDROPSHADOW = 4132u,
		SPI_SETDROPSHADOW
	}
}
