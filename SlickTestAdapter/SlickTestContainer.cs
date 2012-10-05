using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestWindow.Extensibility;
using Microsoft.VisualStudio.TestWindow.Extensibility.Model;

namespace SlickQA.TestAdapter
{
	sealed class SlickTestContainer : ITestContainer
	{
		public SlickTestContainer(ITestContainerDiscoverer discoverer, string source, Uri executorUri)
			: this(discoverer, source, executorUri, Enumerable.Empty<Guid>())
		{
		}

		public SlickTestContainer(ITestContainerDiscoverer discoverer, string source, Uri executorUri, IEnumerable<Guid> debugEngines)
		{
			ValidateArg.NotNullOrEmpty(source, "source");
			ValidateArg.NotNull(executorUri, "executorUri");
			ValidateArg.NotNull(debugEngines, "debugEngines");
			ValidateArg.NotNull(discoverer, "discoverer");

			Source = source;
			ExecutorUri = executorUri;
			TargetFramework = FrameworkVersion.None;
			TargetPlatform = Architecture.AnyCPU;
			DebugEngines = debugEngines;
			_timeStamp = GetTimeStamp();
			_discoverer = discoverer;
		}

		public int CompareTo(ITestContainer other)
		{
			var container = other as SlickTestContainer;
			if (container == null)
			{
				return -1;
			}

			var result = String.Compare(Source, container.Source, StringComparison.OrdinalIgnoreCase);
			if (result != 0)
			{
				return result;
			}
			return _timeStamp.CompareTo(container._timeStamp);
		}

		public ITestContainer Snapshot()
		{
			return new SlickTestContainer(this);
		}


		public override string ToString()
		{
			return string.Format("{0}/{1}", ExecutorUri, Source);
		}

		private DateTime GetTimeStamp()
		{
			if (!string.IsNullOrWhiteSpace(Source) && File.Exists(Source))
			{
				return File.GetLastWriteTime(Source);
			}
			else
			{
				return DateTime.MinValue;
			}
		}

		private SlickTestContainer(SlickTestContainer copy)
			:this(copy.Discoverer, copy.Source, copy.ExecutorUri)
		{
			_timeStamp = copy._timeStamp;
		}

		public Uri ExecutorUri { get; private set; }

		private readonly DateTime _timeStamp;
		private ITestContainerDiscoverer _discoverer;

		public string Source { get; private set; }
		public IEnumerable<Guid> DebugEngines { get; private set; }
		public FrameworkVersion TargetFramework { get; private set; }
		public Architecture TargetPlatform { get; private set; }

		public IDeploymentData DeployAppContainer() { return null; }
		public bool IsAppContainerTestContainer { get { return false; } }
		public ITestContainerDiscoverer Discoverer { get { return _discoverer; } }
	}
}
