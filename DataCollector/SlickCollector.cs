// Copyright 2012 AccessData Group, LLC.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//  http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.Execution;
using SlickQA.DataCollector.Configuration;
using SlickQA.SlickSharp;
using SlickQA.SlickSharp.Logging;

namespace SlickQA.DataCollector
{
	//TODO: Need Unit Test Coverage Here
	[DataCollectorTypeUri("datacollector://slickqa/SlickDataCollector/0.0.1")]
	[DataCollectorFriendlyName("Slick", false)]
	[DataCollectorConfigurationEditor("configurationeditor://slickqa/SlickDataCollectorConfigurationEditor/0.0.1")]
	public class SlickCollector : Microsoft.VisualStudio.TestTools.Execution.DataCollector
	{
		private SlickConfig _config;
		private DataCollectionEnvironmentContext _dataCollectionEnvironmentContext;

		private DataCollectionEvents _dataEvents;

		private DataCollectionLogger _dataLogger;
		private DataCollectionSink _dataSink;
		private Stack<Result> _results;
		private TestRun _slickRun;

		public override void Initialize(XmlElement configurationElement, DataCollectionEvents events,
		                                DataCollectionSink dataSink, DataCollectionLogger logger,
		                                DataCollectionEnvironmentContext environmentContext)
		{
			_config = SlickConfig.LoadConfig(configurationElement);

			SlickConfig.SetServerConfig(_config.Url);

			_dataEvents = events;
			_dataSink = dataSink;
			_dataLogger = logger;
			_dataCollectionEnvironmentContext = environmentContext;

			_results = new Stack<Result>();

			_dataEvents.TestCaseStart += OnTestCaseStart;
			_dataEvents.TestCaseEnd += OnTestCaseEnd;
			_dataEvents.TestCaseFailed += OnTestCaseFailed;

			_dataEvents.SessionStart += OnSessionStart;
			_dataEvents.SessionEnd += OnSessionEnd;

			//_dataEvents.CustomNotification += OnCustomNotification;
			//_dataEvents.DataRequest += OnDataRequest;
			//_dataEvents.SessionPause += OnSessionPause;
			//_dataEvents.SessionResume += OnSessionResume;
			//_dataEvents.TestCasePause += OnTestCasePause;
			//_dataEvents.TestCaseResume += OnTestCaseResume;
			//_dataEvents.TestCaseReset += OnTestCaseReset;
			//_dataEvents.TestStepStart += OnTestStepStart;
			//_dataEvents.TestStepEnd += OnTestStepEnd;
		}

		protected override void Dispose(bool disposing)
		{
			_dataLogger.LogWarning(_dataCollectionEnvironmentContext.SessionDataCollectionContext, "Slick Data Collector: Dispose");
			if (!disposing)
			{
				return;
			}
			_dataEvents.TestCaseStart -= OnTestCaseStart;
			_dataEvents.TestCaseEnd -= OnTestCaseEnd;
			_dataEvents.TestCaseFailed -= OnTestCaseFailed;


			_dataEvents.SessionStart -= OnSessionStart;

			_dataEvents.SessionEnd -= OnSessionEnd;

			//_dataEvents.TestCasePause -= OnTestCasePause;
			//_dataEvents.CustomNotification -= OnCustomNotification;
			//_dataEvents.DataRequest -= OnDataRequest;
			//_dataEvents.SessionPause -= OnSessionPause;
			//_dataEvents.SessionResume -= OnSessionResume;
			//_dataEvents.TestCaseResume -= OnTestCaseResume;
			//_dataEvents.TestCaseReset -= OnTestCaseReset;
			//_dataEvents.TestStepStart -= OnTestStepStart;
			//_dataEvents.TestStepEnd -= OnTestStepEnd;
		}

		private void OnSessionStart(object sender, SessionStartEventArgs eventArgs)
		{
			var project = new Project { Name = _config.ResultDestination.ProjectName };
			project.Get();
			Release release = project.Releases.FirstOrDefault(r => r.Name == _config.ResultDestination.ReleaseName);
			if (release == null)
			{
				throw new ConfigurationErrorsException(String.Format(
					"Specified release name is not valid for the \"{0}\" project.", project));
		}

			_slickRun = new TestRun
		{
							ProjectReference = project,
							ReleaseReference = release,
							Name = DateTime.Now.ToString("f")
						};
			_slickRun.Post();
		}

		private void OnSessionEnd(object sender, SessionEndEventArgs sessionEndEventArgs)
		{
			_slickRun = null;
		}

		private void OnTestCaseStart(object sender, TestCaseStartEventArgs eventArgs)
		{
			string automationKey = eventArgs.TestElement.HumanReadableId;
			Testcase testcase = Testcase.GetTestCaseByAutomationKey(automationKey);
			if (testcase == null)
			{
				testcase = new Testcase
						   {
							   
							   AutomationKey = automationKey,
							   IsAutomated = true,
							   Name = automationKey,
							   ProjectReference = _slickRun.ProjectReference,
						   };
				testcase.Post();
			}

			var testResult = new Result
			                     {
			                     	Hostname = Environment.MachineName,
									ProjectReference = _slickRun.ProjectReference,
									ReleaseReference = _slickRun.ReleaseReference,
									TestRunReference = _slickRun,
									TestcaseReference = testcase,
									Status = Status.NO_RESULT.ToString(),
									RunStatus = RunStatus.RUNNING.ToString(),
									Files = new List<StoredFile>(),
			                     };
			if (_config.ScreenshotSettings.PreTest)
			{
				StoredFile file = ScreenShot.CaptureScreenShot(String.Format("Pre Test {0}.png", automationKey));
				testResult.Files.Add(file);
			}

			testResult.Post();
			_results.Push(testResult);
		}

		private void OnTestCaseEnd(object sender, TestCaseEndEventArgs eventArgs)
		{
			Result testResult = _results.Pop();

			if (_config.ScreenshotSettings.PostTest)
			{
				StoredFile file = ScreenShot.CaptureScreenShot(String.Format("Post Test {0}.png", eventArgs.TestElement.HumanReadableId));
				testResult.Files.Add(file);
			}

			testResult.Status = OutcomeTranslator.Convert(eventArgs.TestOutcome).ToString();
			testResult.RunStatus = RunStatus.FINISHED.ToString();
			testResult.Put();
		}

		private void OnTestCaseFailed(object sender, TestCaseFailedEventArgs eventArgs)
		{
			Result testResult = _results.Peek();

			if (_config.ScreenshotSettings.OnFailure)
			{
				StoredFile file = ScreenShot.CaptureScreenShot(String.Format("Test Failure {0}.png", eventArgs.TestElement.HumanReadableId));
				testResult.Files.Add(file);
			}
		}

		//private void OnTestCasePause(object sender, TestCasePauseEventArgs testCasePauseEventArgs)
		//{
		//}

		//private void OnTestCaseResume(object sender, TestCaseResumeEventArgs testCaseResumeEventArgs)
		//{
		//}

		//private void OnCustomNotification(object sender, CustomNotificationEventArgs customNotificationEventArgs)
		//{
		//}

		//private void OnDataRequest(object sender, DataRequestEventArgs dataRequestEventArgs)
		//{
		//}
		//private void OnSessionPause(object sender, SessionPauseEventArgs sessionPauseEventArgs)
		//{
		//}

		//private void OnSessionResume(object sender, SessionResumeEventArgs sessionResumeEventArgs)
		//{
		//}
	}
}