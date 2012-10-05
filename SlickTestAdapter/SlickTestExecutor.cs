using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.Extensions.OrderedTestAdapter;
using Microsoft.VisualStudio.TestPlatform.Extensions.TmiHelper;
using Microsoft.VisualStudio.TestPlatform.Extensions.VSTestIntegration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace SlickQA.TestAdapter
{
	[ExtensionUri(Constants.EXECUTOR_URI_STRING)]
	public class SlickTestExecutor : ITestExecutor
	{
		public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
		{
			var testCases = tests as List<TestCase> ?? tests.ToList();

			ValidateArg.NotNull(frameworkHandle, "frameworkHandle");
			ValidateArg.NotNullOrEmpty(testCases, "tests");

			_cancellationToken = new TestRunCancellationToken();

		    var sources = new List<string>();
		    foreach (var testCase in testCases)
		    {
                frameworkHandle.SendMessage(TestMessageLevel.Informational, String.Format("In RunTests({0})", testCase.Source));
		        sources.Add(testCase.Source.Replace(".slicktest", ".orderedtest"));
		    }

			var slickExecutionRecorder = new SlickExecutionRecorder(frameworkHandle);
			using (var bridge = new TmiBridge())
			{
			    bridge.RunAllTests(sources, runContext, slickExecutionRecorder, new Uri(OrderedTestExecutor.ExecutorUriString), _cancellationToken );
			}
		    frameworkHandle.SendMessage(TestMessageLevel.Informational,
		                                String.Format("We have {0} results", slickExecutionRecorder.results.Count));
		    _iFrameworkHandle = frameworkHandle;
            
            foreach (var result in slickExecutionRecorder.results)
            {
                log("Result for test {0}: {1}", result.DisplayName, result.Outcome);

                foreach(var attachgroup in result.Attachments)
                {
                    log(" {0}:", attachgroup.DisplayName);
                    foreach (var attachment in attachgroup.Attachments)
                    {
                        log("  {0}: {1}", attachment.Description, attachment.Uri);
                    }
                }

                foreach (var property in result.Properties)
                {
                    log(" {0}: {1}", property.Label, result.GetPropertyValue(property));
                }

                log(" {0} test properties:", result.TestCase.DisplayName);
                foreach (var property in result.TestCase.Properties)
                {
                    if(!String.IsNullOrEmpty(result.TestCase.GetPropertyValue(property).ToString()))
                        log("  {0}: {1}", property.Label, result.TestCase.GetPropertyValue(property));
                }
            }
			_cancellationToken = null;
		}

        public void log(string format, params object[] items)
        {
            _iFrameworkHandle.SendMessage(TestMessageLevel.Informational, String.Format(format, items));
        }

		public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
		{
		    var testSources = new List<string>();
		    foreach (var source in sources)
		    {
                frameworkHandle.SendMessage(TestMessageLevel.Informational, String.Format("In RunTestsSources({0})", source));
		        testSources.Add(source.Replace(".slicktest", ".orderedtest"));
		    }

			ValidateArg.NotNull(frameworkHandle, "frameworkHandle");
			ValidateArg.NotNullOrEmpty(testSources, "sources");

			_cancellationToken = new TestRunCancellationToken();
	
			var slickExecutionRecorder = new SlickExecutionRecorder(frameworkHandle);
			using (var bridge = new TmiBridge())
			{
			    bridge.RunAllTests(testSources, runContext, slickExecutionRecorder, new Uri(OrderedTestExecutor.ExecutorUriString), _cancellationToken);
			}
		    frameworkHandle.SendMessage(TestMessageLevel.Informational,
		                                String.Format("We have {0} results", slickExecutionRecorder.results.Count));
		    _cancellationToken = null;
		}

		public void Cancel()
		{
			TestRunCancellationToken cancellationToken = _cancellationToken;
			if (cancellationToken != null)
			{
				cancellationToken.Cancel();
			}
		}

		private TestRunCancellationToken _cancellationToken;
	    private IFrameworkHandle _iFrameworkHandle;
	}
}