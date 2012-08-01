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
	public sealed class SlickConfig
	{
		public ProjectType Project = new ProjectType();
		public SlickUrlType Url = new SlickUrlType();

		public static SlickConfig LoadConfig(XmlElement configuration)
		{
			var retVal = new SlickConfig();
			XmlNodeList elements = configuration.GetElementsByTagName("Project");
			if (elements.Count != 0)
			{
				XmlNode projectElem = elements[0];
				retVal.Project = new ProjectType(projectElem);
			}
			elements = configuration.GetElementsByTagName("Url");
			if (elements.Count != 0)
			{
				XmlNode urlElem = elements[0];
				retVal.Url = new SlickUrlType(urlElem);
			}
			return retVal;
		}
	}
}