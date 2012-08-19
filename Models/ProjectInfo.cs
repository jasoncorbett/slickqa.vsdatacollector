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
using System.Xml;
using System.Xml.Serialization;

namespace SlickQA.DataCollector.Models
{
	public sealed class ProjectInfo
	{
		public const string TAG_NAME = "Project";

		public string Id { get; set; }
		public string Name { get; private set; }

		[XmlIgnore]
		public string Description { get; private set; }
		[XmlIgnore]
		public string ReleaseName { get; private set; }
		[XmlIgnore]
		public List<string> Tags { get; private set; }

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

		private void InitializeWithDefaults()
		{
			Id = String.Empty;
			Name = String.Empty;
			Description = String.Empty;
			ReleaseName = String.Empty;
			Tags = new List<string>();
		}

		public ProjectInfo(XmlNodeList elements)
		{
			try
			{
				var element = elements[0];
				var reader = new XmlNodeReader(element);
				var s = new XmlSerializer(GetType());
				var temp = s.Deserialize(reader) as ProjectInfo;
				Id = temp.Id;
				Name = temp.Name;
				Description = String.Empty;
				ReleaseName = String.Empty;
				Tags = new List<string>();
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

		public static ProjectInfo FromXml(XmlElement configuration)
		{
			return new ProjectInfo(configuration.GetElementsByTagName(TAG_NAME));
		}
	}
}