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

using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SlickQA.DataCollector.ConfigurationEditor.Test
{
	[TestClass]
	public sealed class LoadConfig
	{
		[TestMethod]
		public void With_default_settings()
		{
			const string DEFAULT_CONFIG =
				"<DefaultConfiguration xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\">\n"
				+ "  <Project Name=\"\" xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\" />"
				+
				"  <Url Scheme=\"http\" Host=\"\" Port=\"8080\" SitePath=\"slick\" xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\" />"
				+ "</DefaultConfiguration>";

			var doc = new XmlDocument();
			doc.LoadXml(DEFAULT_CONFIG);
			XmlElement defaultConfigElement = doc.DocumentElement;

			var expectedProject = new ProjectType();
			var expectedUrl = new SlickUrlType();

			SlickConfig config = ConfigurationController.LoadConfig(defaultConfigElement);

			Assert.AreEqual(expectedProject, config.Project);
			Assert.AreEqual(expectedUrl, config.Url);
		}

		[TestMethod]
		public void With_unconfigured_testsettings_file()
		{
			const string CONFIG =
				"<Configuration xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\">\n"
				+ "  <Project Name=\"\" xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\" />"
				+
				"  <Url Scheme=\"http\" Host=\"\" Port=\"8080\" SitePath=\"slick\" xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\" />"
				+ "</Configuration>";

			var doc = new XmlDocument();
			doc.LoadXml(CONFIG);
			XmlElement defaultConfigElement = doc.DocumentElement;

			var expectedProject = new ProjectType();
			var expectedUrl = new SlickUrlType();

			SlickConfig config = ConfigurationController.LoadConfig(defaultConfigElement);

			Assert.AreEqual(expectedProject, config.Project);
			Assert.AreEqual(expectedUrl, config.Url);
		}

		[TestMethod]
		public void With_configured_testsettings_file()
		{
			const string TEST_SETTINGS_CONFIG =
				"<Configuration xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\">\n"
				+ "  <Project Name=\"Bar\" xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\" />"
				+
				"  <Url Scheme=\"http\" Host=\"foo.baz.com\" Port=\"80\" SitePath=\"slickij\" xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\" />"
				+ "</Configuration>";

			var doc = new XmlDocument();
			doc.LoadXml(TEST_SETTINGS_CONFIG);
			XmlElement defaultConfigElement = doc.DocumentElement;

			var expectedProject = new ProjectType("Bar");
			var expectedUrl = new SlickUrlType("http", "foo.baz.com", 80, "slickij");

			SlickConfig config = ConfigurationController.LoadConfig(defaultConfigElement);

			Assert.AreEqual(expectedProject, config.Project);
			Assert.AreEqual(expectedUrl, config.Url);
		}
	}
}