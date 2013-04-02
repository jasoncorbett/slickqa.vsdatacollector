using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestWindow.Extensibility;
using SlickEventListeners;

namespace SlickQA.TestAdapter
{
	[Export(typeof(ITestContainerDiscoverer))]
	class SlickTestContainerDiscoverer : ITestContainerDiscoverer, IDisposable
	{
		[ImportingConstructor]
		public SlickTestContainerDiscoverer(
			[Import(typeof(SVsServiceProvider))]
			IServiceProvider serviceProvider,
			ISolutionEventsListener solutionListener,
			IFileUpdateWatcher fileUpdateWatcher,
			IFileAddRemoveWatcher fileAddRemoveWatcher)
		{

			Trace.WriteLine("Slick Test Container Discoverer constructor");


			_initialLoad = true;
			_cachedContainers = new List<ITestContainer>();
			_serviceProvider = serviceProvider;

			logger = new Logger(_serviceProvider);

			
			_solutionListener = solutionListener;
			_fileUpdateWatcher = fileUpdateWatcher;
			_fileAddRemoveWatcher = fileAddRemoveWatcher;
	
			_fileAddRemoveWatcher.FileChanged += OnProjectItemChanged;
			_fileAddRemoveWatcher.Start();

			_solutionListener.SolutionUnloaded += OnSolutionUnloaded;
			_solutionListener.SolutionProjectChanged += OnSolutionProjectChanged;
			_solutionListener.Start();

			_fileUpdateWatcher.FileChangedEvent += OnProjectItemChanged;
		}

		public Uri ExecutorUri
		{
			get { return Constants.ExecutorUri; }
		}

		public IEnumerable<ITestContainer> TestContainers
		{
			get { return GetTestContainers(); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}

			if (_fileUpdateWatcher != null)
			{
				_fileUpdateWatcher.FileChangedEvent -= OnProjectItemChanged;
				((IDisposable)_fileUpdateWatcher).Dispose();
				_fileUpdateWatcher = null;
			}

			if(_fileAddRemoveWatcher != null)
			{
				_fileAddRemoveWatcher.FileChanged -= OnProjectItemChanged;
				_fileAddRemoveWatcher.Stop();
				_fileAddRemoveWatcher = null;
			}

			if (_solutionListener != null)
			{
				_solutionListener.SolutionProjectChanged -= OnSolutionProjectChanged;
				_solutionListener.SolutionUnloaded -= OnSolutionUnloaded;
				_solutionListener.Stop();
			}
		}

		private void OnSolutionUnloaded(object sender, EventArgs eventArgs)
		{
			_initialLoad = true;
		}

		private void OnSolutionProjectChanged(object sender, SolutionProjectChangedEventArgs eventArgs)
		{
			if (eventArgs == null)
			{
				return;
			}

			var files = FindTestFiles(eventArgs.Project);
			if (eventArgs.ChangedReason == SolutionChangeReason.Load)
			{
				UpdateFileWatcher(files, true);
			}
			else if(eventArgs.ChangedReason == SolutionChangeReason.Unload)
			{
				UpdateFileWatcher(files, false);
			}

			// Do not fire OnContainersChanged here.
			// This will cause us to fire this event too early before the UTE is ready to process containers and will result in an exception.
			// The UTE will query all the TestContainerDiscoverers once the solution is loaded.
		}

		private void OnProjectItemChanged(object sender, FileChangedEventArgs eventArgs)
		{
			if (eventArgs == null)
			{
				return;
			}
			if (!IsTestFile(eventArgs.FullPath))
			{
				return;
			}

			switch (eventArgs.Changed)
			{
				case FileChangedReason.Added:
					_fileUpdateWatcher.AddWatch(eventArgs.FullPath);
					AddContainerIfTestFile(eventArgs.FullPath);
					break;
				case FileChangedReason.Removed:
					_fileUpdateWatcher.RemoveWatch(eventArgs.FullPath);
					RemoveContainer(eventArgs.FullPath);
					break;
				case FileChangedReason.Changed:
					AddContainerIfTestFile(eventArgs.FullPath);
					break;
			}

			OnContainersChanged();
		}
		
		private void OnContainersChanged()
		{
			if (TestContainersUpdated != null && !_initialLoad)
			{
				TestContainersUpdated(this, EventArgs.Empty);
			}
		}

		private void UpdateFileWatcher(IEnumerable<string> testFiles, bool add)
		{
			foreach (var testFile in testFiles)
			{
				if (add)
				{
					_fileUpdateWatcher.AddWatch(testFile);
					AddContainerIfTestFile(testFile);
				}
				else
				{
					_fileUpdateWatcher.RemoveWatch(testFile);
					RemoveContainer(testFile);
				}
			}
		}

		private void AddContainerIfTestFile(string testFile)
		{
			var isTestFile = IsTestFile(testFile);
			RemoveContainer(testFile);

			if (isTestFile)
			{
				var container1 = new SlickTestContainer(this, testFile, Constants.ExecutorUri);
                
				_cachedContainers.Add(container1);
			}
		}

		private void RemoveContainer(string testFile)
		{
			var containerIndex = _cachedContainers.FindIndex(c => c.Source.Equals(testFile, StringComparison.OrdinalIgnoreCase));
			if (containerIndex >= 0)
			{
				_cachedContainers.RemoveAt(containerIndex);
			}
		}

		private IEnumerable<string> FindTestFiles()
		{
			var solution = (IVsSolution)_serviceProvider.GetService(typeof(SVsSolution));
			var loadedProjects = solution.EnumerateLoadedProjects(__VSENUMPROJFLAGS.EPF_LOADEDINSOLUTION).OfType<IVsProject>();

			return loadedProjects.SelectMany(FindTestFiles).ToList();
		}

		private IEnumerable<string> FindTestFiles(IVsProject project)
		{
			return VsSolutionHelper.GetProjectItems(project).Where(HasTestExtension);
		}

		private IEnumerable<ITestContainer> GetTestContainers()
		{
			if (_initialLoad)
			{
				_cachedContainers.Clear();
				var slickFiles = FindTestFiles();
				UpdateFileWatcher(slickFiles, true);
				_initialLoad = false;
			}

			return _cachedContainers;
		}

		private static bool HasTestExtension(string path)
		{
			return Constants.FILE_EXTENSION.Equals(Path.GetExtension(path), StringComparison.OrdinalIgnoreCase);
		}

		private bool IsTestFile(string path)
		{
			try
			{
				return HasTestExtension(path);
			}
			catch (IOException e)
			{
				logger.Log("IO error when detecting a test file", "Test Container Discovery", e);
			}

			return false;
		}

		public event EventHandler TestContainersUpdated;
		
		private bool _initialLoad;
		private readonly List<ITestContainer> _cachedContainers;
		private readonly ISolutionEventsListener _solutionListener;
		private IFileUpdateWatcher _fileUpdateWatcher;
		private IFileAddRemoveWatcher _fileAddRemoveWatcher;
		private readonly IServiceProvider _serviceProvider;
		private ILogger logger;
	}
}
