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

namespace SlickQA.DataCollector.Configuration
{
	public sealed class Config
	{
		public ResultDestination ResultDestination = new ResultDestination();
		public ScreenShotSettings ScreenshotSettings = new ScreenShotSettings();
		public SlickUrl Url = new SlickUrl();

		public static Config LoadConfig(XmlElement configuration)
		{
			var retVal = new Config();
			XmlNodeList elements = configuration.GetElementsByTagName("ResultDestination");
			if (elements.Count != 0)
			{
				XmlNode projectElem = elements[0];
				retVal.ResultDestination = new ResultDestination(projectElem);
			}
			elements = configuration.GetElementsByTagName("Url");
			if (elements.Count != 0)
			{
				XmlNode urlElem = elements[0];
				retVal.Url = new SlickUrl(urlElem);
			}
			elements = configuration.GetElementsByTagName("Screenshot");
			if (elements.Count != 0)
			{
				XmlNode screenshotElem = elements[0];
				retVal.ScreenshotSettings = new ScreenShotSettings(screenshotElem);
			}
			return retVal;
		}

		public void ConfigToXml(XmlElement configuration)
		{
			configuration.AppendChild(ResultDestination.ToXml(configuration.OwnerDocument));
			configuration.AppendChild(Url.ToXml(configuration.OwnerDocument));
			configuration.AppendChild(ScreenshotSettings.ToXml(configuration.OwnerDocument));
		}
	}
}