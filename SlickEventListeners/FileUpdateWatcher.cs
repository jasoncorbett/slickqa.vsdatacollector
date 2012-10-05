using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace SlickEventListeners
{
	[Export(typeof(IFileUpdateWatcher))]
	class FileUpdateWatcher : IFileUpdateWatcher, IDisposable
	{
		private Dictionary<string, FileWatcherTracker> FileWatchers { get; set; }
		public event EventHandler<FileChangedEventArgs> FileChangedEvent;

		public FileUpdateWatcher()
		{
			FileWatchers = new Dictionary<string, FileWatcherTracker>(StringComparer.OrdinalIgnoreCase);
		}
		public void RemoveWatch(string file)
		{
			ValidateArg.NotNullOrEmpty(file, "file");

			FileWatcherTracker watcherInfo;
			if (FileWatchers.TryGetValue(file, out watcherInfo))
			{
				watcherInfo.Stop();
				watcherInfo.Dispose();
			}
		}

		public void AddWatch(string file)
		{
			ValidateArg.NotNullOrEmpty(file, "file");

			FileWatcherTracker watcherInfo;
			if (FileWatchers.TryGetValue(file, out watcherInfo))
			{
				return;
			}
			watcherInfo = new FileWatcherTracker(file);
			FileWatchers.Add(file, watcherInfo);
			watcherInfo.Start(OnChanged);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!disposing || FileWatchers == null)
			{
				return;
			}

			foreach (var fileWatcherTracker in FileWatchers)
			{
				fileWatcherTracker.Value.Stop();
			}
			FileWatchers.Clear();
			FileWatchers = null;
		}

		private void OnChanged(object sender, FileSystemEventArgs eventArgs)
		{
			FileWatcherTracker watcherInfo;
			if (FileChangedEvent == null || !FileWatchers.TryGetValue(eventArgs.FullPath, out watcherInfo))
			{
				return;
			}

			var updateTime = File.GetLastWriteTimeUtc(eventArgs.FullPath);

			if (watcherInfo.ShouldFireEvent(updateTime))
			{
				FileChangedEvent(sender, new FileChangedEventArgs(eventArgs.FullPath, FileChangedReason.Changed));
			}
		}
	}
}