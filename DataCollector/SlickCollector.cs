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
using System.Linq;
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
	[DataCollectorConfigurationEditor("configurationeditor://slickqa/SlickDataCollectorConfigurationEditor/0.0.1")]
	public class SlickCollector : Microsoft.VisualStudio.TestTools.Execution.DataCollector
	{
		private DataCollectionSink DataSink { get; set; }
		private DataCollectionEvents DataEvents { get; set; }
		private DataCollectionEnvironmentContext DataCollectionEnvironmentContext { get; set; }


		internal static readonly Guid OrderedTest = new Guid("{ec4800e8-40e5-4ab3-8510-b8bf29b1904d}");


		public override void Initialize(XmlElement configurationElement, DataCollectionEvents events, DataCollectionSink dataSink,
			DataCollectionLogger logger, DataCollectionEnvironmentContext environmentContext)
		{
			DataEvents = events;
			DataSink = dataSink;
			DataCollectionEnvironmentContext = environmentContext;

			DataEvents.TestCaseStart += OnTestCaseStart;
		}

		private void OnTestCaseStart(object sender, TestCaseStartEventArgs eventArgs)
		{
			ITestElement testElement = eventArgs.TestElement;

			if (testElement.TestType.Id == OrderedTest)
			{
				var orderedTestPath = testElement.Storage;
				var orderedTestDir = Path.GetDirectoryName(orderedTestPath);

				//TODO: Pre-load all Not Run results for tests in an ordered test
				// Read XML
				string xml;
				using (var xmlFile = File.Open(orderedTestPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					using (var reader = new StreamReader(xmlFile))
					{
						xml = reader.ReadToEnd();
					}
				}

				XNamespace vs = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
				XDocument doc = XDocument.Parse(xml);

				var assemblies = doc.Descendants(vs + "TestLink").Select(x => x.Attribute("storage").Value).Distinct().ToList();

				var methodLookupTable = new Dictionary<Guid, SlickInfo>();
				foreach (var path in assemblies)
				{
					// Load dlls referenced in ordered test
					Debug.Assert(orderedTestDir != null, "orderedTestDir != null");
					var dll = Assembly.Load(File.ReadAllBytes(Path.Combine(orderedTestDir, path)));

					// Find all classes that are test classes
					foreach (var type in dll.GetTypes())
					{
						var extensionAttrs = type.GetCustomAttributes(typeof(TestClassExtensionAttribute), true);
						var testclassAttrs = type.GetCustomAttributes(typeof(TestClassAttribute), true);

						if (extensionAttrs.Length != 0 || testclassAttrs.Length != 0)
						{
							// Find all methods with TestMethod attribute
							var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
							foreach (var method in methods)
							{
								if (method.GetCustomAttributes(typeof(TestMethodAttribute), true).Length != 0)
								{
									//Read Slick Attributes for each method
									methodLookupTable.Add(method.GetHash(),
									                      new SlickInfo
									                      {
										                      Id = method.GetTestCaseId(),
										                      AutomationKey = method.GetAutomationKey(),
										                      Name = method.GetTestName(),
										                      Description = method.GetDescription(),
										                      Component = method.GetComponent(),
										                      Tags = method.GetTags(),
										                      Attributes = method.GetAttributes(),
									                      });
								}
							}
						}
					}
				}


				XmlSerializer serializer = new XmlSerializer(typeof(SlickInfo));
				using (var stream = new MemoryStream())
				{
					using (var writer = XmlWriter.Create(stream, new XmlWriterSettings {Indent = true, Encoding = Encoding.UTF8}))
					{
						foreach (var kvp in methodLookupTable)
						{
							var info = kvp.Value;
							serializer.Serialize(writer, info);
						}
					}
					DataSink.SendStreamAsync(DataCollectionEnvironmentContext.SessionDataCollectionContext, stream, "TestInfo.xml",
					                         false);
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