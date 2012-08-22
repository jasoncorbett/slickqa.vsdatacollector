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
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.Common;
using Microsoft.VisualStudio.TestTools.Execution;
using SlickQA.DataCollector.Attributes;
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

		private Stack<Tuple<Result, Stopwatch>> _results;
		private TestRun _slickRun;

		private DataCollectionSink DataSink { get; set; }

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
			DataSink = dataSink;
			_dataLogger = logger;
			_dataCollectionEnvironmentContext = environmentContext;

			_results = new Stack<Tuple<Result, Stopwatch>>();

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
			Configuration environmentConfiguration = Configuration.GetEnvironmentConfiguration(hostname);
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
			ITestElement testElement = eventArgs.TestElement;
			string testcaseId = testElement.GetAttributeValue<TestCaseIdAttribute>();

			Component component = GetComponent(testElement);
			string name = GetTestName(testElement);

			Testcase testcase = Testcase.GetTestCaseByAutomationKey(testElement.HumanReadableId);
			if (testcase == null)
			{
				testcase = new Testcase
						   {
							   AutomationId = testcaseId,
							   AutomationKey = testElement.HumanReadableId,
							   IsAutomated = true,
							   Name = name,
							   ProjectReference = _slickRun.ProjectReference,
						   };
				testcase.Post();
			}
			testcase.ProjectReference = _slickRun.ProjectReference;
			testcase.ComponentReference = component;
			testcase.Purpose = testElement.Description;

			if (testcase.Attributes == null)
			{
				testcase.Attributes = new LinkedHashMap<string>();
			}

			Hashtable props = testElement.Properties;
			foreach (DictionaryEntry entry in props.Cast<DictionaryEntry>().Where(entry => !testcase.Attributes.ContainsKey(entry.Key.ToString())))
			{
				testcase.Attributes.Add(entry);
			}

			UpdateTags(testcase, testElement);

			testcase.Put();

			var testResult = new Result
								 {
									 TestRunReference = _slickRun,
									 TestcaseReference = testcase,
									 ProjectReference = _slickRun.ProjectReference,
									 ReleaseReference = _slickRun.ReleaseReference,
									 BuildReference = _slickRun.BuildReference,
									 Hostname = Environment.MachineName,
									 Status = ResultStatus.NO_RESULT.ToString(),
									 ComponentReference = testcase.ComponentReference,
								 };
			if (ScreenshotInfo.PreTest)
			{
				StoredFile file = ScreenShot.CaptureScreenShot(String.Format("Pre Test {0}.png", testElement.HumanReadableId));

				testResult.Files = new List<StoredFile> {file};
			}

			testResult.Post();
			var timer = new Stopwatch();
			timer.Start();

			_results.Push(new Tuple<Result, Stopwatch>(testResult, timer));
		}

		private static string GetTestName(ITestElement testElement)
		{
			string testName = testElement.GetAttributeValue<TestNameAttribute>();
			string name = testElement.HumanReadableId;
			if (!string.IsNullOrWhiteSpace(testName))
			{
				name = testName;
			}
			return name;
		}

		private Component GetComponent(ITestElement testElement)
		{
			string testedFeature = testElement.GetAttributeValue<TestedFeatureAttribute>();
			Component component = null;
			if (!string.IsNullOrWhiteSpace(testedFeature))
			{
				component = new Component {Name = testedFeature, ProjectId = _slickRun.ProjectReference.Id};
				component.Get(true);
			}
			return component;
		}

		private static void UpdateTags(Testcase testcase, ITestElement testElement)
		{
			string[] categories = testElement.TestCategories.ToArray();
			if (testcase.Tags != null)
			{
				IEnumerable<string> allTags = testcase.Tags.Union(categories);
				testcase.Tags = allTags.ToList();				
			}
			else
			{
				testcase.Tags = categories.ToList();
			}
		}

		private void OnTestCaseEnd(object sender, TestCaseEndEventArgs eventArgs)
		{
			Tuple<Result,Stopwatch> result = _results.Pop();
			Result testResult = result.Item1;
			Stopwatch timer = result.Item2;
			timer.Stop();

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
			testResult.RunLength = timer.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture);
			testResult.Put();
		}

		private void OnTestCaseFailed(object sender, TestCaseFailedEventArgs eventArgs)
		{
			Tuple<Result,Stopwatch> result = _results.Peek();
			Result testResult = result.Item1;

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