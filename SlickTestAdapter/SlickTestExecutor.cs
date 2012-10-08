using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestPlatform.Extensions.OrderedTestAdapter;
using Microsoft.VisualStudio.TestPlatform.Extensions.TmiHelper;
using Microsoft.VisualStudio.TestPlatform.Extensions.VSTestIntegration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using SlickQA.DataCollector.Models;

namespace SlickQA.TestAdapter
{
	[ExtensionUri(Constants.EXECUTOR_URI_STRING)]
	public class SlickTestExecutor : ITestExecutor
	{
	    private IFrameworkHandle _frameworkHandle;

		public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
		{
			ValidateArg.NotNull(frameworkHandle, "frameworkHandle");
			ValidateArg.NotNullOrEmpty(tests, "tests");
		    _frameworkHandle = frameworkHandle;


			using (var bridge = new TmiBridge())
			{
			    foreach (var test in tests)
			    {
                    log("Running '{0}'", test.Source);
			        var slickExecutionRecorder = new SlickExecutionRecorder(frameworkHandle, LoadSlickTest(test.Source));
			        _cancellationToken = new TestRunCancellationToken();
			        bridge.RunAllTests(new string[] {slickExecutionRecorder.SlickInfo.OrderedTest}, runContext, slickExecutionRecorder, new Uri(OrderedTestExecutor.ExecutorUriString), _cancellationToken );
			        _cancellationToken = null;
			    }
			}

            /*
            foreach (var result in slickExecutionRecorder.Results)
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
             */
		}

        public void log(string format, params object[] items)
        {
            _frameworkHandle.SendMessage(TestMessageLevel.Informational, String.Format(format, items));
        }

		public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
		{
			ValidateArg.NotNull(frameworkHandle, "frameworkHandle");
			ValidateArg.NotNullOrEmpty(sources, "sources");
		    _frameworkHandle = frameworkHandle;

			using (var bridge = new TmiBridge())
			{
			    foreach (var testSource in sources)
			    {
                    log("Running '{0}'", testSource);
			        var slickExecutionRecorder = new SlickExecutionRecorder(frameworkHandle, LoadSlickTest(testSource));
			        _cancellationToken = new TestRunCancellationToken();
			        bridge.RunAllTests(new string[] {slickExecutionRecorder.SlickInfo.OrderedTest}, runContext, slickExecutionRecorder, new Uri(OrderedTestExecutor.ExecutorUriString), _cancellationToken);
		            _cancellationToken = null;
			    }
			}
		}

        public SlickTest LoadSlickTest(string source)
        {
            SlickTest retval = null;

            try
            {
                var serializer = new XmlSerializer(typeof (SlickTest));
                using (var filestream = new FileStream(source, FileMode.Open))
                {
                    retval = (SlickTest) serializer.Deserialize(filestream);
                }
                log("Loaded slicktest file '{0}'", source);
                retval.OrderedTest = Path.Combine(Path.GetDirectoryName(source), retval.OrderedTest);
            }
            catch (Exception e)
            {
                log("Unable to load slicktest '{0}': {1}", source, e.Message);
                log(e.StackTrace);
                throw;
            }

            return retval;
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
	}
}