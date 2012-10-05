using System;

namespace SlickEventListeners
{
	public interface IFileUpdateWatcher
	{
		void RemoveWatch(string file);
		void AddWatch(string file);
		event EventHandler<FileChangedEventArgs> FileChangedEvent;
	}
}