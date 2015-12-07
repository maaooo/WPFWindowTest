using System;
using System.Diagnostics.CodeAnalysis;

namespace TestRx3.Native
{
	[SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Native shit be cray. Best not to fuck with it."), Flags]
	public enum SetWindowPosFlags : uint
	{
		AsynchronousWindowPosition = 16384u,
		DeferErase = 8192u,
		DrawFrame = 32u,
		FrameChanged = 32u,
		HideWindow = 128u,
		DoNotActivate = 16u,
		DoNotCopyBits = 256u,
		IgnoreMove = 2u,
		DoNotChangeOwnerZOrder = 512u,
		DoNotRedraw = 8u,
		DoNotReposition = 512u,
		DoNotSendChangingEvent = 1024u,
		IgnoreResize = 1u,
		IgnoreZOrder = 4u,
		ShowWindow = 64u
	}
}
