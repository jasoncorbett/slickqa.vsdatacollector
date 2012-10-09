using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using SlickQA.DataCollector.Models;

namespace SlickQA.TestAdapter
{
	public class SlickExecutionRecorder : IFrameworkHandle
	{
		private readonly IFrameworkHandle _handle;
        public SlickTest SlickInfo { get; set; }
        public SlickReporter Reporter { get; set; }
        public bool ReportToSlick { get; set; }

		public SlickExecutionRecorder(IFrameworkHandle handle, SlickTest slickTest)
		{
			_handle = handle;
		    SlickInfo = slickTest;
            Reporter = new SlickReporter(slickTest);
		    ReportToSlick = true;
		    try
		    {
                Reporter.Initialize();
                Reporter.RecordEmptyResults();
		    }
		    catch (Exception e)
		    {
                log("Unable to report to slick: {0}", e.Message);
                log(e.StackTrace);
		        ReportToSlick = false;
		    }
		}

        public void log(string format, params object[] items)
        {
            _handle.SendMessage(TestMessageLevel.Informational, String.Format(format, items));
        }

		public void SendMessage(TestMessageLevel testMessageLevel, string message)
		{
			_handle.SendMessage(testMessageLevel, message);
		}

		public void RecordResult(TestResult testResult)
		{
            if (!String.IsNullOrWhiteSpace(testResult.DisplayName) && ReportToSlick)
            {
                Reporter.UpdateResult(testResult);
            }
		    _handle.RecordResult(testResult);
		}

		public void RecordStart(TestCase testCase)
		{
			_handle.RecordStart(testCase);
		}

		public void RecordEnd(TestCase testCase, TestOutcome outcome)
		{
			_handle.RecordEnd(testCase, outcome);
		}

		public void RecordAttachments(IList<AttachmentSet> attachmentSets)
		{
			_handle.RecordAttachments(attachmentSets);
		}

		public int LaunchProcessWithDebuggerAttached(string filePath, string workingDirectory, string arguments, IDictionary<string, string> environmentVariables)
		{
			return _handle.LaunchProcessWithDebuggerAttached(filePath, workingDirectory, arguments, environmentVariables);
		}

		public bool EnableShutdownAfterTestRun { get; set; }

        public void AllDone()
        {
            if (ReportToSlick)
                Reporter.AllDone();
        }
	}
}