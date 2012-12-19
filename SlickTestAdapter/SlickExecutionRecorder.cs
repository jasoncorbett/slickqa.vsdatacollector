using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using SlickQA.DataCollector.Models;

namespace SlickQA.TestAdapter
{
    public interface ISimpleLogger
    {
        void Log(string message, params object[] items);
    }

	public class SlickExecutionRecorder : IFrameworkHandle, ISimpleLogger
	{
	    private readonly IFrameworkHandle _handle;
        public SlickTest SlickInfo { get; set; }
        public SlickReporter Reporter { get; set; }
        public bool ReportToSlick { get; set; }
        public Regex IsOrderedTest { get; set; }

		public SlickExecutionRecorder(IFrameworkHandle handle, SlickTest slickTest)
		{
			_handle = handle;
		    SlickInfo = slickTest;
            Reporter = new SlickReporter(this, slickTest);
		    ReportToSlick = true;
		    try
		    {
                Reporter.Initialize();
                Reporter.RecordEmptyResults(slickTest.Tests);
		    }
		    catch (Exception e)
		    {
                Log("Unable to report to slick: {0}", e.Message);
                Log(e.StackTrace);
		        ReportToSlick = false;
		    }
		}

        public void Log(string format, params object[] items)
        {
            _handle.SendMessage(TestMessageLevel.Informational, String.Format(format, items));
        }

		public void SendMessage(TestMessageLevel testMessageLevel, string message)
		{
			_handle.SendMessage(testMessageLevel, message);
		}

		public void RecordResult(TestResult testResult)
		{
            if (ReportToSlick)
            {
                Reporter.UpdateResult(testResult);
            }
		    _handle.RecordResult(testResult);
		}

		public void RecordStart(TestCase testCase)
		{
            Log("Testcase starting: {0}", testCase.DisplayName);
			_handle.RecordStart(testCase);
		}

		public void RecordEnd(TestCase testCase, TestOutcome outcome)
		{
            Log("Test Ended with outcome: {0}", outcome);
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