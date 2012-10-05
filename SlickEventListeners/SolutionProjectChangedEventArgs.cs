using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace SlickEventListeners
{
	public class SolutionProjectChangedEventArgs : EventArgs
	{
		public SolutionProjectChangedEventArgs(IVsProject project, SolutionChangeReason changedReason)
		{
			ChangedReason = changedReason;
			Project = project;
		}

		public IVsProject Project { get; private set; }
		public SolutionChangeReason ChangedReason { get; private set; }
	}

	public enum SolutionChangeReason
	{
		Load,
		Unload,
	}
}