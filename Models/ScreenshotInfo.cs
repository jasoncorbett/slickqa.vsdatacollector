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
using System.Xml;
using System.Xml.Serialization;

namespace SlickQA.DataCollector.Models
{
	public class ScreenshotInfo
	{
		public const string TAG_NAME = "Screenshot";

		public ScreenshotInfo()
		{
			InitializeWithDefaults();
		}

		private ScreenshotInfo(XmlNodeList elements)
		{
			try
			{
				var element = elements[0];
				if (element != null)
				{
					var reader = new XmlNodeReader(element);
					var s = new XmlSerializer(GetType());
					var temp = s.Deserialize(reader) as ScreenshotInfo;
					PreTest = temp.PreTest;
					PostTest = temp.PostTest;
					FailedTest = temp.FailedTest;
				}
				else
				{
					InitializeWithDefaults();
				}
			}
			catch (InvalidOperationException)
			{
				InitializeWithDefaults();
			}
		}

		public ScreenshotInfo(ScreenshotInfo other)
			:this(other.PreTest, other.PostTest, other.FailedTest)
		{
		}

		private ScreenshotInfo(bool preTest, bool postTest, bool failedTest)
		{
			PreTest = preTest;
			PostTest = postTest;
			FailedTest = failedTest;
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

		public static ScreenshotInfo FromXml(XmlElement configuration)
		{
			return new ScreenshotInfo(configuration.GetElementsByTagName(TAG_NAME));
		}

		public XmlNode ToXmlNode()
		{
			XmlNode node = new XmlDocument();
			var writer = node.CreateNavigator().AppendChild();

			var s = new XmlSerializer(GetType());
			s.Serialize(writer, this);

			return node;
		}
	}
}
