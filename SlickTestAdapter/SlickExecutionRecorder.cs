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
        public List<TestResult> Results = new List<TestResult>();
        public SlickTest SlickInfo { get; set; }

		public SlickExecutionRecorder(IFrameworkHandle handle, SlickTest slickTest)
		{
			_handle = handle;
		    SlickInfo = slickTest;
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
			log("RecordResult: Test Case Id: {0} Test Case Display Name: {1}", testResult.TestCase.Id, testResult.DisplayName);
            Results.Add(testResult);
			_handle.RecordResult(testResult);
		}

		public void RecordStart(TestCase testCase)
		{
			log("RecordStart: Test Case Id: {0} Test Case Display Name: {1}", testCase.Id, testCase.DisplayName);
			_handle.RecordStart(testCase);
		}

		public void RecordEnd(TestCase testCase, TestOutcome outcome)
		{
			log("RecordEnd: Test Case Id: {0} Test Case Display Name: {1} OutCome: {2}", testCase.Id, testCase.DisplayName, outcome);
			_handle.RecordEnd(testCase, outcome);
		}

		public void RecordAttachments(IList<AttachmentSet> attachmentSets)
		{
			log("RecordAttachements: {0}", attachmentSets);
			_handle.RecordAttachments(attachmentSets);
		}

		public int LaunchProcessWithDebuggerAttached(string filePath, string workingDirectory, string arguments, IDictionary<string, string> environmentVariables)
		{
			log("LaunchProcessWithDebuggerAttached: File Path: {0} Working Directory: {1} Arguments: {2} EnvironmentVariables: {3}", filePath, workingDirectory, arguments, environmentVariables);
			return _handle.LaunchProcessWithDebuggerAttached(filePath, workingDirectory, arguments, environmentVariables);
		}

		public bool EnableShutdownAfterTestRun { get; set; }
	}
}