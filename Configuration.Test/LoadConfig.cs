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

namespace SlickQA.DataCollector.Configuration.Test
{
	[TestClass]
	public sealed class LoadConfig
	{
		[TestMethod]
		public void With_default_settings()
		{
			const string DEFAULT_CONFIG =
				"<DefaultConfiguration xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\">\n"
				+ "  <ResultDestination ProjectId=\"\" ReleaseId=\"\" xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\" />"
				+
				"  <Url Scheme=\"http\" Host=\"\" Port=\"8080\" SitePath=\"slick\" xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\" />"
				+ "  <Screenshot PreTest=\"false\" PostTest=\"false\" OnFailure=\"true\" />"
				+ "</DefaultConfiguration>";

			var doc = new XmlDocument();
			doc.LoadXml(DEFAULT_CONFIG);
			XmlElement defaultConfigElement = doc.DocumentElement;

			var expectedProject = new ResultDestination();
			var expectedUrl = new SlickUrl();
			var expectedScreenshot = new ScreenShotSettings();

			Config config = Config.LoadConfig(defaultConfigElement);

			Assert.AreEqual(expectedProject, config.ResultDestination);
			Assert.AreEqual(expectedUrl, config.Url);
			Assert.AreEqual(expectedScreenshot, config.ScreenshotSettings);
		}

		[TestMethod]
		public void With_unconfigured_testsettings_file()
		{
			const string CONFIG =
				"<Configuration xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\">\n"
				+ "  <ResultDestination ProjectId=\"\" ReleaseId=\"\" xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\" />"
				+
				"  <Url Scheme=\"http\" Host=\"\" Port=\"8080\" SitePath=\"slick\" xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\" />"
				+ "  <Screenshot PreTest=\"false\" PostTest=\"false\" OnFailure=\"true\" />"
				+ "</Configuration>";

			var doc = new XmlDocument();
			doc.LoadXml(CONFIG);
			XmlElement defaultConfigElement = doc.DocumentElement;

			var expectedProject = new ResultDestination();
			var expectedUrl = new SlickUrl();
			var expectedScreenshot = new ScreenShotSettings();

			Config config = Config.LoadConfig(defaultConfigElement);

			Assert.AreEqual(expectedProject, config.ResultDestination);
			Assert.AreEqual(expectedUrl, config.Url);
			Assert.AreEqual(expectedScreenshot, config.ScreenshotSettings);
		}
	}
}