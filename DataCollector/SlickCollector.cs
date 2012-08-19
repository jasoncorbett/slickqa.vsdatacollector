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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.Common;
using Microsoft.VisualStudio.TestTools.Execution;
using SlickQA.SlickSharp;
using SlickQA.SlickSharp.Logging;
using TestRun = SlickQA.SlickSharp.TestRun;

namespace SlickQA.DataCollector
{
	[DataCollectorTypeUri("datacollector://slickqa/SlickDataCollector/0.0.1")]
	[DataCollectorFriendlyName("Slick", false)]
	[DataCollectorConfigurationEditor("configurationeditor://slickqa/SlickDataCollectorConfigurationEditor/0.0.1")]
	public class SlickCollector : Microsoft.VisualStudio.TestTools.Execution.DataCollector
	{
		//private Config _config;
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
			//_config = Config.LoadConfig(configurationElement);

			//if (!_config.Url.IsValid)
			//{
			//    throw new UriInvalidException();
			//}
			
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
			//var project = _config.ResultDestination.Project;
			//Release release = _config.ResultDestination.Release;
			//if (release == null || String.IsNullOrWhiteSpace(release.Id))
			//{
			//    throw new ConfigurationErrorsException(String.Format(
			//        "Specified release name is not valid for the \"{0}\" project.", project));
			//}

			//_slickRun = new TestRun
			//            {
			//                ProjectReference = project as Project,
			//                ReleaseReference = release,
			//                Name = DateTime.Now.ToString("f")
			//            };
			//_slickRun.Post();
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
			UpdateTestCaseWithTestData(testcase, eventArgs.TestElement);

			var testResult = new Result
								 {
									 Hostname = Environment.MachineName,
									 ProjectReference = _slickRun.ProjectReference,
									 ReleaseReference = _slickRun.ReleaseReference,
									 TestRunReference = _slickRun,
									 TestcaseReference = testcase,
									 Status = ResultStatus.NO_RESULT.ToString(),
									 RunStatus = RunStatus.RUNNING.ToString(),
									 Files = new List<StoredFile>(),
								 };
			//if (_config.ScreenshotSettings.PreTest)
			//{
			//    StoredFile file = ScreenShot.CaptureScreenShot(String.Format("Pre Test {0}.png", automationKey));
			//    testResult.Files.Add(file);
			//}

			testResult.Post();
			_results.Push(testResult);
		}

		private void UpdateTestCaseWithTestData(Testcase testcase, ITestElement testElement)
		{
			//testcase.Attributes;
			//testcase.Author;
			//testcase.AutomationId;
			//testcase.AutomationTool;
			//testcase.ComponentReference;
			//testcase.Configuration;
			//testcase.DataDrivenProperties;
			//testcase.IsDeleted;
			//testcase.Name;
			//testcase.Priority;
			//testcase.ProjectReference;
			//testcase.Requirements;
			//testcase.StabilityRating;
			//testcase.Steps;

			//testElement.AbortRunOnAgentFailure;
			//testElement.Adapter;
			//testElement.AgentAttributes;
			//testElement.CanBeAggregated;
			//testElement.CategoryId;
			//testElement.ControllerPlugin;
			//testElement.Copy;
			//testElement.CreatedByUI;
			//testElement.CssIteration;
			//testElement.CssProjectStructure;
			//testElement.DeploymentItems;
			//testElement.Enabled;
			//testElement.ErrorMessageForNonRunnable;
			//testElement.ExecutionId;
			//testElement.Groups;
			//testElement.HumanReadableId;
			//testElement.Id;
			//testElement.IsAutomated;
			//testElement.IsGroupable;
			//testElement.IsModified;
			//testElement.IsRunOnRestart;
			//testElement.IsRunnable;
			//testElement.Link;
			//testElement.Name;
			//testElement.Owner;
			//testElement.ParentExecId;
			//testElement.Priority;
			//testElement.ProjectData;
			//testElement.projectId;
			//testElement.ProjectRelativePath;
			
			//testElement.ReadOnly;
			//testElement.SolutionName;
			//testElement.SourceFileName;
			//testElement.Storage;
			//testElement.TestCategories;
			//testElement.TestType;
			//testElement.Timeout;
			//testElement.UserData;
			//testElement.VisibleProperties;
			//testElement.WorkItemIds;

			testcase.Priority = testElement.Priority;
			testcase.Purpose = testElement.Description;

			var props = testElement.Properties;
			foreach (DictionaryEntry entry in props)
			{
				if (!testcase.Attributes.ContainsKey(entry.Key.ToString()))
				{
					testcase.Attributes.Add(entry);
				}
			}

			UpdateTags(testcase, testElement);

			testcase.Put();
		}

		private static void UpdateTags(Testcase testcase, ITestElement testElement)
		{
			var categories = testElement.TestCategories.ToArray();
			var allTags = testcase.Tags.Union(categories);
			testcase.Tags = allTags.ToList();
		}

		private void OnTestCaseEnd(object sender, TestCaseEndEventArgs eventArgs)
		{
			Result testResult = _results.Pop();

			//if (_config.ScreenshotSettings.PostTest)
			//{
			//    StoredFile file = ScreenShot.CaptureScreenShot(String.Format("Post Test {0}.png", eventArgs.TestElement.HumanReadableId));
			//    testResult.Files.Add(file);
			//}

			testResult.Status = OutcomeTranslator.Convert(eventArgs.TestOutcome).ToString();
			testResult.RunStatus = RunStatus.FINISHED.ToString();
			testResult.Put();
		}

		private void OnTestCaseFailed(object sender, TestCaseFailedEventArgs eventArgs)
		{
			Result testResult = _results.Peek();

			//if (_config.ScreenshotSettings.OnFailure)
			//{
			//    StoredFile file = ScreenShot.CaptureScreenShot(String.Format("Test Failure {0}.png", eventArgs.TestElement.HumanReadableId));
			//    testResult.Files.Add(file);
			//}
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