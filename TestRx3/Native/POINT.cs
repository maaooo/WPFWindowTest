using System;
using System.Diagnostics.CodeAnalysis;

namespace TestRx3.Native
{
	[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Not urgent")]
	[Serializable]
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
}
