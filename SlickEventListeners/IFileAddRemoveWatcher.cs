using System;

namespace SlickEventListeners
{
	public interface IFileAddRemoveWatcher
	{
		void Start();
		void Stop();
		event EventHandler<FileChangedEventArgs> FileChanged;
	}
}