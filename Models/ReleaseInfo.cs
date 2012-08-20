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
	[XmlRoot(TAG_NAME)]
	public sealed class ReleaseInfo
	{
		public const string TAG_NAME = "Release";

		public string Id { get; set; }
		public string Name { get; set; }
		public string ProjectId { get; set; }

		public ReleaseInfo(string id, string name, string projectId)
		{
			Id = id;
			Name = name;
			ProjectId = projectId;
		}

		public ReleaseInfo()
		{
			InitializeWithDefaults();
		}

		private void InitializeWithDefaults()
		{
			Id = String.Empty;
			Name = String.Empty;
			ProjectId = String.Empty;
		}

		public ReleaseInfo(XmlNodeList elements)
		{
			try
			{
				var element = elements[0];
				if (element != null)
				{
					var reader = new XmlNodeReader(element);
					var s = new XmlSerializer(GetType());
					var temp = s.Deserialize(reader) as ReleaseInfo;
					Id = temp.Id;
					Name = temp.Name;
					ProjectId = temp.ProjectId;
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

		public ReleaseInfo(ReleaseInfo other)
			:this(other.Id, other.Name, other.ProjectId)
		{
		}

		public static ReleaseInfo FromXml(XmlElement configuration)
		{
			return new ReleaseInfo(configuration.GetElementsByTagName(TAG_NAME));
		}

		public XmlNode ToXmlNode()
		{
			var doc = new XmlDocument();

			var nav = doc.CreateNavigator();
			using (XmlWriter writer = nav.AppendChild())
			{
				var ser = new XmlSerializer(GetType());
				ser.Serialize(writer, this);
			}
			XmlNode retVal = doc.FirstChild;
			doc.RemoveChild(retVal);

			return retVal;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}