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
using SlickQA.SlickSharp.Web;

namespace SlickQA.DataCollector.Configuration
{
	public sealed class SlickConfig
	{
		public ResultDestination ResultDestination = new ResultDestination();
		public SlickUrlType Url = new SlickUrlType();
		public ScreenShotSettings ScreenshotSettings = new ScreenShotSettings();

		public static SlickConfig LoadConfig(XmlElement configuration)
		{
			var retVal = new SlickConfig();
			XmlNodeList elements = configuration.GetElementsByTagName("ResultDestination");
			if (elements.Count != 0)
			{
				var projectElem = elements[0];
				retVal.ResultDestination = new ResultDestination(projectElem);
			}
			elements = configuration.GetElementsByTagName("Url");
			if (elements.Count != 0)
			{
				var urlElem = elements[0];
				retVal.Url = new SlickUrlType(urlElem);
			}
			elements = configuration.GetElementsByTagName("Screenshot");
			if (elements.Count != 0)
			{
				var screenshotElem = elements[0];
				retVal.ScreenshotSettings = new ScreenShotSettings(screenshotElem);
			}
			return retVal;
		}

		public static void SetServerConfig(string scheme, string host, int port, string sitePath)
		{
			ServerConfig.Scheme = scheme;
			ServerConfig.SlickHost = host;
			ServerConfig.Port = port;
			ServerConfig.SitePath = sitePath;
		}

		public static void SetServerConfig(SlickUrlType url)
		{
			SetServerConfig(url.Scheme, url.Host, url.Port, url.SitePath);
		}
	}
}