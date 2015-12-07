using System;
using System.Diagnostics.CodeAnalysis;

namespace TestRx3.Native
{
	[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Not urgent")]
	[Serializable]
	public struct WINDOWPLACEMENT
	{
		public int length;

		public int flags;

		public int showCmd;

		public POINT minPosition;

		public POINT maxPosition;

		public RECT normalPosition;
	}
}
