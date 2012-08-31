using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.Common;
using SlickQA.DataCollector.Attributes;
using SlickQA.SlickSharp;
using SlickQA.SlickSharp.Logging;
using SlickQA.SlickSharp.Utility;

namespace SlickQA.DataCollector
{
	static class TestDetailExtensions
	{
		public static List<string> GetTags(this ITestElement testElement)
		{
			return testElement.TestCategories.Select(c => c.TestCategory).ToList();
		}

		public static Component GetComponent(this ITestElement testElement, string projectId)
		{
			string testedFeature = testElement.GetAttributeValue<TestedFeatureAttribute>();
			Component component = null;
			if (!String.IsNullOrWhiteSpace(testedFeature))
			{
				component = new Component { Name = testedFeature, ProjectId = projectId };
				component.Get(true);
			}
			return component;
		}

		public static void TakeScreenshot(this ITestElement testElement, bool shouldTakeScreenshot, string filePrefix, List<StoredFile> storedFiles)
		{
			if (testElement.TestType.Id == SlickCollector.OrderedTest || !shouldTakeScreenshot)
			{
				return;
			}
			StoredFile file = ScreenShot.CaptureScreenShot(String.Format("{0}_{1}.png",filePrefix, testElement.HumanReadableId));
			storedFiles.Add(file);
		}

		public static string GetTestName(this ITestElement testElement)
		{
			string name = testElement.HumanReadableId;
			string testName = testElement.GetAttributeValue<TestNameAttribute>();
			if (!String.IsNullOrWhiteSpace(testName))
			{
				name = testName;
			}
			return name;
		}

		public static LinkedHashMap<string> GetAttributes(this ITestElement testElement)
		{
			var attributes = new LinkedHashMap<string>();
			foreach (DictionaryEntry entry in testElement.Properties.Cast<DictionaryEntry>())
			{
				attributes.Add(entry);
			}
			return attributes;
		}
	}
}
