using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace SlickQA.TestAdapter
{
	public class SlickExecutionRecorder : IFrameworkHandle
	{
		private readonly IFrameworkHandle _handle;
        public List<TestResult> results = new List<TestResult>();

		public SlickExecutionRecorder(IFrameworkHandle handle)
		{
			_handle = handle;
		}

		public void SendMessage(TestMessageLevel testMessageLevel, string message)
		{
			_handle.SendMessage(testMessageLevel, message);
		}

		public void RecordResult(TestResult testResult)
		{
			Console.WriteLine("RecordResult: Test Case Id: {0} Test Case Display Name: {1}", testResult.TestCase.Id, testResult.DisplayName);
            results.Add(testResult);
			_handle.RecordResult(testResult);
		}

		public void RecordStart(TestCase testCase)
		{
			Console.WriteLine("RecordStart: Test Case Id: {0} Test Case Display Name: {1}", testCase.Id, testCase.DisplayName);
			_handle.RecordStart(testCase);
		}

		public void RecordEnd(TestCase testCase, TestOutcome outcome)
		{
			Console.WriteLine("RecordEnd: Test Case Id: {0} Test Case Display Name: {1} OutCome: {2}", testCase.Id, testCase.DisplayName, outcome);

			_handle.RecordEnd(testCase, outcome);
		}

		public void RecordAttachments(IList<AttachmentSet> attachmentSets)
		{
			Console.WriteLine("RecordAttachements: {0}", attachmentSets);
			_handle.RecordAttachments(attachmentSets);
		}

		public int LaunchProcessWithDebuggerAttached(string filePath, string workingDirectory, string arguments, IDictionary<string, string> environmentVariables)
		{
			Console.WriteLine("LaunchProcessWithDebuggerAttached: File Path: {0} Working Directory: {1} Arguments: {2} EnvironmentVariables: {3}", filePath, workingDirectory, arguments, environmentVariables);
			return _handle.LaunchProcessWithDebuggerAttached(filePath, workingDirectory, arguments, environmentVariables);
		}

		public bool EnableShutdownAfterTestRun { get; set; }
	}
}