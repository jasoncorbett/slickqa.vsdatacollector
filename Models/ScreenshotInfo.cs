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
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace SlickQA.DataCollector.Models
{
	public class ScreenshotInfo
	{
		public ScreenshotInfo(bool preTest, bool postTest, bool failedTest)
		{
			PreTest = preTest;
			PostTest = postTest;
			FailedTest = failedTest;
		}

		public ScreenshotInfo(ScreenshotInfo other)
			:this(other.PreTest, other.PostTest, other.FailedTest)
		{
			
		}

		public ScreenshotInfo(XmlNodeList elements)
		{
			try
			{
				XmlReader reader = new XmlNodeReader(elements[0]);
				var serializer = new XmlSerializer(typeof(ScreenshotInfo));
				var settings = serializer.Deserialize(reader) as ScreenshotInfo;
				Debug.Assert(settings != null, "settings != null");
				PreTest = settings.PreTest;
				PostTest = settings.PostTest;
				FailedTest = settings.FailedTest;
			}
			catch (IndexOutOfRangeException)
			{
				InitializeWithDefaults();
			}
			catch (InvalidOperationException)
			{
				InitializeWithDefaults();
			}
		}

		private void InitializeWithDefaults()
		{
			PreTest = false;
			PostTest = false;
			FailedTest = true;
		}

		public bool PreTest { get; set; }
		public bool PostTest { get; set; }
		public bool FailedTest { get; set; }

		public XmlNode ToXmlNode()
		{
			var serializer = new XmlSerializer(GetType());
			XmlNode node = new XmlDocument();
			serializer.Serialize(node.CreateNavigator().AppendChild(), this);
			return node;
		}
	}
}
