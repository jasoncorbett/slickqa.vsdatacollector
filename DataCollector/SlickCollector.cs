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
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
		public const string SLICK_FILE_STAGE = "slick";
		private DataCollectionSink DataSink { get; set; }
		private ProjectInfo ProjectInfo { get; set; }
		private ReleaseInfo ReleaseInfo { get; set; }
		private BuildProviderInfo BuildProvider { get; set; }
		private ScreenshotInfo ScreenshotInfo { get; set; }
		private TestPlanInfo TestPlanInfo { get; set; }
		private UrlInfo UrlInfo { get; set; }
		private TestRun SlickRun { get; set; }
		private Dictionary<Guid, Tuple<Result, Stopwatch>> Results { get; set; }
		private DataCollectionLogger DataLogger { get; set; }
		private DataCollectionEvents DataEvents { get; set; }
		private DataCollectionEnvironmentContext DataCollectionEnvironmentContext { get; set; }
		private bool SessionActive { get; set; }

		public override void Initialize(XmlElement configurationElement, DataCollectionEvents events, DataCollectionSink dataSink,
			DataCollectionLogger logger, DataCollectionEnvironmentContext environmentContext)
		{
			LoadConfig(configurationElement);
			SetupServerConfig();

			DataEvents = events;
			DataSink = dataSink;
			DataLogger = logger;
			DataCollectionEnvironmentContext = environmentContext;

			Results = new Dictionary<Guid, Tuple<Result, Stopwatch>>();
			SessionActive = false;

			DataEvents.TestCaseStart += OnTestCaseStart;
			DataEvents.TestCaseEnd += OnTestCaseEnd;
			DataEvents.TestCaseFailed += OnTestCaseFailed;

			DataEvents.SessionStart += OnSessionStart;
			DataEvents.SessionEnd += OnSessionEnd;
		}

		private void OnSessionStart(object sender, SessionStartEventArgs eventArgs)
		{
			SessionActive = true;

			var project = new Project { Id = ProjectInfo.Id };
			try
			{
				project.Get();
			}
			catch (Exception e)
			{
				DataLogger.LogError(DataCollectionEnvironmentContext.SessionDataCollectionContext, e);
			}

			var release = new Release
						  {
							  Id = ReleaseInfo.Id,
							  ProjectId = ReleaseInfo.ProjectId
						  };
			try
			{
				release.Get();
			}
			catch (Exception e)
			{
				DataLogger.LogError(DataCollectionEnvironmentContext.SessionDataCollectionContext, e);
			}

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
				try
				{
					build.Get(true);
				}
				catch (Exception e)
				{
					DataLogger.LogError(DataCollectionEnvironmentContext.SessionDataCollectionContext, e);
				}
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
				try
				{
					environmentConfiguration.Post();
				}
				catch (Exception e)
				{
					DataLogger.LogError(DataCollectionEnvironmentContext.SessionDataCollectionContext, e);
				}
			}

			SlickRun = new TestRun
						{
							Name = TestPlanInfo.Name,
							ProjectReference = project,
							ReleaseReference = release,
							TestPlanId = TestPlanInfo.Id,
							BuildReference = build,
							ConfigurationReference = environmentConfiguration,
						};
			try
			{
				SlickRun.Post();
			}
			catch (Exception e)
			{
				DataLogger.LogError(DataCollectionEnvironmentContext.SessionDataCollectionContext, e);
			}
			Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, SLICK_FILE_STAGE));
		}

		private void OnSessionEnd(object sender, SessionEndEventArgs sessionEndEventArgs)
		{
			SessionActive = false;
			SlickRun = null;
		}

		private void OnTestCaseStart(object sender, TestCaseStartEventArgs eventArgs)
		{
			ITestElement testElement = eventArgs.TestElement;
			var storedFiles = new List<StoredFile> {};

			testElement.TakeScreenshot(ScreenshotInfo.PreTest, "Pre Test", storedFiles);
			Result result = CreateTestResult(testElement, SlickRun, storedFiles);
			Stopwatch timer = StartTimer();

			Results.Add(eventArgs.TestCaseId, new Tuple<Result, Stopwatch>(result, timer));
		}

		private void OnTestCaseEnd(object sender, TestCaseEndEventArgs eventArgs)
		{
			ITestElement testElement = eventArgs.TestElement;

			Tuple<Result, Stopwatch> result = Results[eventArgs.TestCaseId];
			Result testResult = result.Item1;
			Stopwatch timer = result.Item2;
			timer.Stop();

			testElement.TakeScreenshot(ScreenshotInfo.PostTest, "Post Test", testResult.Files);


			SendFiles(Path.Combine(Environment.CurrentDirectory, SLICK_FILE_STAGE), testResult.Files);

			testResult.Status = OutcomeTranslator.Convert(eventArgs.TestOutcome).ToString();
			testResult.RunLength = timer.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture);
			testResult.Put();
		}

		private void OnTestCaseFailed(object sender, TestCaseFailedEventArgs eventArgs)
		{
			ITestElement testElement = eventArgs.TestElement;
			Tuple<Result, Stopwatch> result = Results[eventArgs.TestCaseId];

			testElement.TakeScreenshot(ScreenshotInfo.FailedTest, "Test Failure", result.Item1.Files);
		}

		protected override void Dispose(bool disposing)
		{
			if (SessionActive)
			{
				DataLogger.LogError(DataCollectionEnvironmentContext.SessionDataCollectionContext, "Slick Data Collector disposed while there was still an active test session.");
			}

			if (!disposing)
			{
				return;
			}
			DataEvents.SessionStart -= OnSessionStart;
			DataEvents.SessionEnd -= OnSessionEnd;

			DataEvents.TestCaseStart -= OnTestCaseStart;
			DataEvents.TestCaseEnd -= OnTestCaseEnd;
			DataEvents.TestCaseFailed -= OnTestCaseFailed;
		}
		
		private void SetupServerConfig()
		{
			ServerConfig.Scheme = UrlInfo.Scheme;
			ServerConfig.SlickHost = UrlInfo.HostName;
			ServerConfig.Port = UrlInfo.Port;
			ServerConfig.SitePath = UrlInfo.SitePath;
		}

		private void LoadConfig(XmlElement configurationElement)
		{
			ProjectInfo = ProjectInfo.FromXml(configurationElement);
			ReleaseInfo = ReleaseInfo.FromXml(configurationElement);
			BuildProvider = BuildProviderInfo.FromXml(configurationElement);
			ScreenshotInfo = ScreenshotInfo.FromXml(configurationElement);
			TestPlanInfo = TestPlanInfo.FromXml(configurationElement);
			UrlInfo = UrlInfo.FromXml(configurationElement);
		}

		internal static readonly Guid OrderedTest = new Guid("{ec4800e8-40e5-4ab3-8510-b8bf29b1904d}");
		private Result CreateTestResult(ITestElement testElement, TestRun testRun, List<StoredFile> storedFiles)
		{
			string testcaseId = null;
			string name = null;
			string description = null;

			Testcase testcase = null;
			Component component = null;
			LinkedHashMap<string> attributes = null;
			List<string> tags = null;

			if (testElement.TestType.Id != OrderedTest)
			{
				description = testElement.Description;
				testcaseId = testElement.GetAttributeValue<TestCaseIdAttribute>();
				component = testElement.GetComponent(SlickRun.ProjectReference.Id);
				attributes = testElement.GetAttributes();
				name = testElement.GetTestName();
				tags = testElement.GetTags();
			}

			try
			{
				testcase = Testcase.GetTestCaseByAutomationKey(testElement.HumanReadableId);
			}
			catch (Exception e)
			{
				DataLogger.LogError(DataCollectionEnvironmentContext.SessionDataCollectionContext, e);
			}

			if (testcase == null)
			{
				testcase = new Testcase
				{
					AutomationId = testcaseId,
					AutomationKey = testElement.HumanReadableId,
					IsAutomated = true,
					Name = name,
					ProjectReference = SlickRun.ProjectReference
				};
				try
				{
					testcase.Post();
				}
				catch (Exception e)
				{
					DataLogger.LogError(DataCollectionEnvironmentContext.SessionDataCollectionContext, e);
				}
			}

			UpdateTestcase(testcase, component, attributes, description, tags);

			var testResult = new Result
			{
				TestRunReference = testRun,
				TestcaseReference = testcase,
				ProjectReference = SlickRun.ProjectReference,
				ReleaseReference = SlickRun.ReleaseReference,
				BuildReference = SlickRun.BuildReference,
				Hostname = Environment.MachineName,
				Status = ResultStatus.NO_RESULT.ToString(),
				ComponentReference = component,
				Files = storedFiles,
				//Recorded = ,
			};

			try
			{
				testResult.Post();
			}
			catch (Exception e)
			{
				DataLogger.LogError(DataCollectionEnvironmentContext.SessionDataCollectionContext, e);
			}

			return testResult;
		}

		private void UpdateTestcase(Testcase testcase, Component component, LinkedHashMap<string> attributes, string description, List<string> tags)
		{
			testcase.ProjectReference = SlickRun.ProjectReference;
			testcase.ComponentReference = component;
			testcase.Attributes = attributes;
			testcase.Purpose = description;
			testcase.Tags = tags;

			try
			{
				testcase.Put();
			}
			catch (Exception e)
			{
				DataLogger.LogError(DataCollectionEnvironmentContext.SessionDataCollectionContext, e);
			}
		}

		private static Stopwatch StartTimer()
		{
			var timer = new Stopwatch();
			timer.Start();
			return timer;
		}

		private void SendFiles(string currentDirectory, ICollection<StoredFile> files)
		{
			var d = new DirectoryInfo(currentDirectory);
			foreach (var file in d.EnumerateFiles().Where(file => file.Extension != "dll"))
			{
				SendFile(files, file);
				File.Delete(file.FullName);
			}
		}

		private void SendFile(ICollection<StoredFile> files, FileInfo file)
		{
			var sf = new StoredFile { FileName = file.Name, Mimetype = GetMimeType(file) };
			byte[] screenBytes;
			using (var s = file.OpenRead())
			{
				screenBytes = new byte[file.Length];
				s.Read(screenBytes, 0, Convert.ToInt32(file.Length));
			}
			sf.Post();
			sf.PostContent(screenBytes);
			files.Add(sf);
		}

		private static string GetMimeType(FileSystemInfo file)
		{
			var retVal = "text/plain";
			switch (file.Extension)
			{
				case "png":
					retVal = "image/png";
					break;
				case "jpg":
					retVal = "image/jpeg";
					break;
				case "gif":
					retVal = "image/gif";
					break;
				case "trx":
				case "xml":
					retVal = "text/xml";
					break;
			}
			return retVal;
		}
	}
}