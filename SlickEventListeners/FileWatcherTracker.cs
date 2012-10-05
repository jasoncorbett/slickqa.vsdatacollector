using System;
using System.Diagnostics;
using System.IO;

namespace SlickEventListeners
{
	internal sealed class FileWatcherTracker : IDisposable
	{
		private FileSystemWatcher _watcher;
		private DateTime _lastEventTime;

		public FileWatcherTracker(string file)
		{
			var dir = Path.GetDirectoryName(file);
			var filename = Path.GetFileName(file);

			Debug.Assert(dir != null, "dir != null");
			Debug.Assert(filename != null, "filename != null");

			_watcher = new FileSystemWatcher(dir, filename);
			_lastEventTime = DateTime.MinValue;
		}

		public void Start(FileSystemEventHandler onChanged)
		{
			_watcher.Changed += onChanged;
			_watcher.EnableRaisingEvents = true;
		}

		public void Stop()
		{
			_watcher.EnableRaisingEvents = false;
		}

		public bool ShouldFireEvent(DateTime updateTime)
		{
			if (updateTime.Subtract(_lastEventTime).TotalMilliseconds > 500)
			{
				_lastEventTime = updateTime;
				return true;
			}
			return false;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			_watcher.Dispose();
			_watcher = null;
		}
	}
}