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
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace SlickQA.DataCollector.Models
{
	[XmlRoot(TAG_NAME)]
	public sealed class ProjectInfo
	{
		public const string TAG_NAME = "Project";

		public ProjectInfo(string id, string name, string description, string releaseName, List<string> tags)
		{
			Id = id;
			Name = name;
			Description = description;
			ReleaseName = releaseName;
			Tags = tags;
		}

		public ProjectInfo()
		{
			InitializeWithDefaults();
		}

		public ProjectInfo(XmlNodeList elements)
		{
			try
			{
				XmlNode element = elements[0];
				if (element != null)
				{
					var reader = new XmlNodeReader(element);
					var s = new XmlSerializer(GetType());
					var temp = s.Deserialize(reader) as ProjectInfo;
					Debug.Assert(temp != null, "temp != null");
					Id = temp.Id;
					Name = temp.Name;
					Description = String.Empty;
					ReleaseName = String.Empty;
					Tags = new List<string>();
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

		public ProjectInfo(ProjectInfo other)
			:this(other.Id,other.Name,other.Description,other.ReleaseName,new List<string>(other.Tags))
		{
		}

		public string Id { get; set; }
		public string Name { get; set; }

		[XmlIgnore]
		public string Description { get; set; }
		[XmlIgnore]
		public string ReleaseName { get; set; }
		[XmlIgnore]
		public List<string> Tags { get; set; }

		private void InitializeWithDefaults()
		{
			Id = String.Empty;
			Name = String.Empty;
			Description = String.Empty;
			ReleaseName = String.Empty;
			Tags = new List<string>();
		}

		public static ProjectInfo FromXml(XmlElement configuration)
		{
			return new ProjectInfo(configuration.GetElementsByTagName(TAG_NAME));
		}

		public XmlNode ToXmlNode()
		{
			var doc = new XmlDocument();

			XPathNavigator nav = doc.CreateNavigator();
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

		public override bool Equals(object obj)
		{
			var other = obj as ProjectInfo;
			return other != null && Id.Equals(other.Id);
		}
	}
}