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
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.Common;
using Microsoft.VisualStudio.TestTools.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SlickQA.DataCollector
{
	[DataCollectorTypeUri("datacollector://slickqa/SlickDataCollector/0.0.1")]
	[DataCollectorFriendlyName("Slick", false)]
	public class SlickCollector : Microsoft.VisualStudio.TestTools.Execution.DataCollector
	{
		private DataCollectionSink DataSink { get; set; }
		private DataCollectionEvents DataEvents { get; set; }
		private DataCollectionEnvironmentContext EnvironmentContext { get; set; }

		internal static readonly Guid OrderedTest = new Guid("{ec4800e8-40e5-4ab3-8510-b8bf29b1904d}");


		private List<SlickInfo> _methodLookupList;
		private bool _firstTest = true;


		public override void Initialize(XmlElement configurationElement, DataCollectionEvents events,
										DataCollectionSink dataSink, DataCollectionLogger logger,
										DataCollectionEnvironmentContext environmentContext)
		{
			DataEvents = events;
			DataSink = dataSink;
			EnvironmentContext = environmentContext;
			_methodLookupList = new List<SlickInfo>();


			DataEvents.TestCaseStart += OnTestCaseStart;
		}

		private void ParseOrderedTestXmlToSlickInfoList(string orderedTestPath)
		{
			var orderedTestDir = Path.GetDirectoryName(orderedTestPath);

			// Read XML
			string xml;

			using (var reader = new StreamReader(File.Open(orderedTestPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
			{
				xml = reader.ReadToEnd();
			}


			XNamespace vs = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
			XDocument doc = XDocument.Parse(xml);

			var testlinks = doc.Descendants(vs + "TestLink");
			foreach (var testlink in testlinks)
			{
				var id = Guid.Parse(testlink.Attribute("id").Value);
				var storagePath = testlink.Attribute("storage").Value;

				Debug.Assert(orderedTestDir != null, "orderedTestDir != null");
				var fullPath = Path.Combine(orderedTestDir, storagePath);

				if (storagePath.Contains(".orderedtest"))
				{
					ParseOrderedTestXmlToSlickInfoList(fullPath);
				}
				else
				{
					// Load dlls referenced in ordered test
					var dll = Assembly.Load(File.ReadAllBytes(fullPath));

					// Find all classes that are test classes
					foreach (var type in dll.GetTypes())
					{
						var extensionAttrs = type.GetCustomAttributes(typeof(TestClassExtensionAttribute), true);
						var testclassAttrs = type.GetCustomAttributes(typeof(TestClassAttribute), true);

						bool found = false;
						if (extensionAttrs.Length != 0 || testclassAttrs.Length != 0)
						{
							// Find all methods with TestMethod attribute
							var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
							foreach (var method in methods)
							{
								if (method.GetCustomAttributes(typeof(TestMethodAttribute), true).Length != 0)
								{
									if (method.GetHash() == id)
									{
										_methodLookupList.Add(new SlickInfo
										{
											Id = method.GetTestCaseId(),
											AutomationKey = method.GetAutomationKey(),
											Name = method.GetTestName(),
											Description = method.GetDescription(),
											Component = method.GetComponent(),
											Tags = method.GetTags(),
											Attributes = method.GetAttributes(),
										});
										found = true;
										break;
									}
								}
							}
						}
						if (found)
						{
							break;
						}
					}
				}
			}
		}

		private void OnTestCaseStart(object sender, TestCaseStartEventArgs eventArgs)
		{
			ITestElement testElement = eventArgs.TestElement;

			if (_firstTest && testElement.TestType.Id == OrderedTest)
			{
				_firstTest = false;
				var orderedTestPath = testElement.Storage;
				ParseOrderedTestXmlToSlickInfoList(orderedTestPath);

				var serializer = new XmlSerializer(typeof(List<SlickInfo>));
				MemoryStream stream = null;
				try
				{
					stream = new MemoryStream();

					using (var writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
					{
							serializer.Serialize(writer, _methodLookupList);

					}

					stream.Position = 0;
					DataSink.SendStreamAsync(EnvironmentContext.SessionDataCollectionContext, stream, "TestInfo.xml", false);
				}
				finally
				{
					if (stream != null)
					{
						stream.Dispose();
					}
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}

			DataEvents.TestCaseStart -= OnTestCaseStart;
		}
	}
}