using System.Collections.Generic;
using System.IO;

using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace SlickQA.TestAdapter
{
    using JetBrains.Annotations;

    [DefaultExecutorUri(Constants.EXECUTOR_URI_STRING)]
	[FileExtension(Constants.FILE_EXTENSION)]
    [UsedImplicitly]
	public class SlickTestDiscoverer : ITestDiscoverer
	{
		public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
		{
			logger.SendMessage(TestMessageLevel.Informational, "SlickTestDiscoverer discovering tests for:");

			foreach (var source in sources)
			{
				logger.SendMessage(TestMessageLevel.Informational, source);
                var slicktest = new TestCase(Path.GetFileName(source), Constants.ExecutorUri, source);
                logger.SendMessage(TestMessageLevel.Informational, string.Format("slicktest.Source = {0}", slicktest.Source));
                discoverySink.SendTestCase(slicktest);
			}

		}
	}
}
