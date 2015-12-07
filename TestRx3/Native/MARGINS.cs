using System;
using System.Diagnostics.CodeAnalysis;

namespace TestRx3.Native
{
	[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Not urgent")]
	public struct MARGINS
	{
		public int leftWidth;

		public int rightWidth;

		public int topHeight;

		public int bottomHeight;
	}
}
