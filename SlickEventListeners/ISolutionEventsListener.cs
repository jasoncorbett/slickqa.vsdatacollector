using System;

namespace SlickEventListeners
{
	public interface ISolutionEventsListener
	{
		void Start();
		void Stop();

		event EventHandler<SolutionProjectChangedEventArgs> SolutionProjectChanged;
		event EventHandler SolutionUnloaded;
	}
}