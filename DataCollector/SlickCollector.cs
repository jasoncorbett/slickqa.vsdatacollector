﻿// Copyright 2012 AccessData Group, LLC.
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
using SlickQA.DataCollector.Models;
using SlickQA.SlickSharp;
using SlickQA.SlickSharp.Logging;
using SlickQA.SlickSharp.Utility;
using SlickQA.SlickSharp.Web;
using TestRun = SlickQA.SlickSharp.TestRun;

namespace SlickQA.DataCollector
{
	[DataCollectorTypeUri("datacollector://slickqa/SlickDataCollector/0.0.1")]
	[DataCollectorFriendlyName("Slick", false)]
	[DataCollectorConfigurationEditor("configurationeditor://slickqa/SlickDataCollectorConfigurationEditor/0.0.1")]
	public class SlickCollector : Microsoft.VisualStudio.TestTools.Execution.DataCollector
	{
		private DataCollectionEnvironmentContext _dataCollectionEnvironmentContext;

		private DataCollectionEvents _dataEvents;

		private DataCollectionLogger _dataLogger;
		private DataCollectionSink _dataSink;
		private Stack<Result> _results;
		private TestRun _slickRun;
		private ProjectInfo ProjectInfo { get; set; }
		private ReleaseInfo ReleaseInfo { get; set; }
		private BuildProviderInfo BuildProvider { get; set; }
		private ScreenshotInfo ScreenshotInfo { get; set; }
		private TestPlanInfo TestPlanInfo { get; set; }
		private UrlInfo UrlInfo { get; set; }


		public override void Initialize(XmlElement configurationElement, DataCollectionEvents events,
										DataCollectionSink dataSink, DataCollectionLogger logger,
										DataCollectionEnvironmentContext environmentContext)
		{
			ProjectInfo = ProjectInfo.FromXml(configurationElement);
			ReleaseInfo = ReleaseInfo.FromXml(configurationElement);
			BuildProvider = BuildProviderInfo.FromXml(configurationElement);
			ScreenshotInfo = ScreenshotInfo.FromXml(configurationElement);
			TestPlanInfo = TestPlanInfo.FromXml(configurationElement);
			UrlInfo = UrlInfo.FromXml(configurationElement);

			ServerConfig.Scheme = UrlInfo.Scheme;
			ServerConfig.SlickHost = UrlInfo.HostName;
			ServerConfig.Port = UrlInfo.Port;
			ServerConfig.SitePath = UrlInfo.SitePath;
			
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
			var project = new Project {Id = ProjectInfo.Id};
			project.Get();

			var release = new Release
			              {
			              	Id = ReleaseInfo.Id,
							ProjectId = ReleaseInfo.ProjectId
			              };
			release.Get();

			Build build = null;
			if (BuildProvider.Method != null)
			{
				var buildNumber = BuildProvider.Method.Invoke(null, null) as String;

				build = new Build
				        {
				        	Name = buildNumber,
							ProjectId = project.Id,
							ReleaseId = release.Id
				        };
				build.Get(true);
			}


			string hostname = Environment.MachineName;
			var environmentConfiguration = Configuration.GetEnvironmentConfiguration(hostname);
			if (environmentConfiguration == null)
			{
				environmentConfiguration = new Configuration
				{
					Name = hostname,
					ConfigurationType = "ENVIRONMENT"
				};
				environmentConfiguration.Post();
			}

			_slickRun = new TestRun
			            {
			            	Name = TestPlanInfo.Name,
			            	ProjectReference = project,
			            	ReleaseReference = release,
			            	TestPlanId = TestPlanInfo.Id,
			            	BuildReference = build,
							ConfigurationReference = environmentConfiguration,
			            };
			_slickRun.Post();
		}

		private void OnSessionEnd(object sender, SessionEndEventArgs sessionEndEventArgs)
		{
			_slickRun = null;
		}

		private void OnTestCaseStart(object sender, TestCaseStartEventArgs eventArgs)
		{
			Testcase testcase = Testcase.GetTestCaseByAutomationKey(eventArgs.TestElement.HumanReadableId);
			if (testcase == null)
			{
				testcase = new Testcase
						   {
							   AutomationKey = eventArgs.TestElement.HumanReadableId,
							   IsAutomated = true,
							   Name = eventArgs.TestElement.HumanReadableId,
							   //TODO: Figure out a better method of test naming
							   ProjectReference = _slickRun.ProjectReference,
						   };
				testcase.Post();
			}
			testcase.ProjectReference = _slickRun.ProjectReference;
			UpdateTestCaseWithTestData(testcase, eventArgs.TestElement);

			var testResult = new Result
								 {
									 TestRunReference = _slickRun,
									 TestcaseReference = testcase,
									 ProjectReference = _slickRun.ProjectReference,
									 ReleaseReference = _slickRun.ReleaseReference,
									 BuildReference = _slickRun.BuildReference,
									 Hostname = Environment.MachineName,
									 Status = ResultStatus.NO_RESULT.ToString(),
								 };
			if (ScreenshotInfo.PreTest)
			{
				StoredFile file = ScreenShot.CaptureScreenShot(String.Format("Pre Test {0}.png", eventArgs.TestElement.HumanReadableId));

				testResult.Files = new List<StoredFile> {file};
			}

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

			if (testcase.Attributes == null)
			{
				testcase.Attributes = new LinkedHashMap<string>();
			}

			var props = testElement.Properties;
			foreach (var entry in props.Cast<DictionaryEntry>().Where(entry => !testcase.Attributes.ContainsKey(entry.Key.ToString())))
			{
				testcase.Attributes.Add(entry);
			}

			UpdateTags(testcase, testElement);

			testcase.Put();
		}

		private static void UpdateTags(Testcase testcase, ITestElement testElement)
		{
			var categories = testElement.TestCategories.ToArray();
			if (testcase.Tags != null)
			{
				var allTags = testcase.Tags.Union(categories);
				testcase.Tags = allTags.ToList();				
			}
			else
			{
				testcase.Tags = categories.ToList();
			}
		}

		private void OnTestCaseEnd(object sender, TestCaseEndEventArgs eventArgs)
		{
			Result testResult = _results.Pop();

			if (ScreenshotInfo.PostTest)
			{
				StoredFile file = ScreenShot.CaptureScreenShot(String.Format("Post Test {0}.png", eventArgs.TestElement.HumanReadableId));
				if (testResult.Files == null)
				{
					testResult.Files = new List<StoredFile>();
				}
				testResult.Files.Add(file);
			}

			testResult.Status = OutcomeTranslator.Convert(eventArgs.TestOutcome).ToString();
			testResult.RunStatus = RunStatus.FINISHED.ToString();
			testResult.Put();
		}

		private void OnTestCaseFailed(object sender, TestCaseFailedEventArgs eventArgs)
		{
			Result testResult = _results.Peek();

			if (!ScreenshotInfo.FailedTest)
			{
				return;
			}
			StoredFile file = ScreenShot.CaptureScreenShot(String.Format("Test Failure {0}.png", eventArgs.TestElement.HumanReadableId));
			if (testResult.Files == null)
			{
				testResult.Files = new List<StoredFile>();
			}
			testResult.Files.Add(file);
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