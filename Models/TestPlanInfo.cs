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
	public class TestPlanInfo
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ProjectId { get; set; }
		[XmlIgnore]
		public string CreatedBy { get; set; }

		public TestPlanInfo(string id, string name, string projectId, string createdBy)
		{
			Id = id;
			Name = name;
			ProjectId = projectId;
			CreatedBy = createdBy;
		}

		public TestPlanInfo(TestPlanInfo other)
			:this(other.Id, other.Name, other.ProjectId, other.CreatedBy)
		{
		}

		public TestPlanInfo(XmlNodeList elements)
		{
			try
			{
				XmlReader reader = new XmlNodeReader(elements[0]);
				var serializer = new XmlSerializer(GetType());
				var info = serializer.Deserialize(reader) as TestPlanInfo;
				Debug.Assert(info != null, "info != null");
				Id = info.Id;
				Name = info.Name;
				ProjectId = info.ProjectId;
				CreatedBy = String.Empty;
			}
			catch (IndexOutOfRangeException e)
			{
				//Create a default UrlInfo since none was specified in the xml
				InitializeWithDefaults();
			}
			catch (InvalidOperationException e)
			{
				//Create a default UrlInfo since we can't read from the Xml
				InitializeWithDefaults();
			}
		}

		private void InitializeWithDefaults()
		{
			Id = String.Empty;
			Name = String.Empty;
			ProjectId = String.Empty;
			CreatedBy = String.Empty;
		}

		public XmlNode ToXmlNode()
		{
			var serializer = new XmlSerializer(GetType());
			XmlNode node = new XmlDocument();
			serializer.Serialize(node.CreateNavigator().AppendChild(), this);
			return node;
		}

		public const string TAG_NAME = "Plan";
	}
}