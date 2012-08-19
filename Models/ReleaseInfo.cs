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

namespace SlickQA.DataCollector.Models
{
	public sealed class ReleaseInfo
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ProjectId { get; set; }

		public ReleaseInfo(string id, string name, string projectId)
		{
			Id = id;
			Name = name;
			ProjectId = projectId;
		}

		public ReleaseInfo(ReleaseInfo other)
			:this(other.Id, other.Name, other.ProjectId)
		{
		}

		public ReleaseInfo(XmlNodeList elements)
		{
			try
			{
				XmlReader reader = new XmlNodeReader(elements[0]);
				var serializer = new XmlSerializer(typeof(ReleaseInfo));
				var release = serializer.Deserialize(reader) as ReleaseInfo;
				Debug.Assert(release != null, "release != null");
				Id = release.Id;
				Name = release.Name;
				ProjectId = release.ProjectId;
			}
			catch (IndexOutOfRangeException)
			{
				InitializeWithDefaults();
			}
			catch(InvalidOperationException)
			{
				InitializeWithDefaults();
			}
		}

		private void InitializeWithDefaults()
		{
			Id = string.Empty;
			Name = string.Empty;
			ProjectId = string.Empty;
		}

		public XmlNode ToXmlNode()
		{
			var serializer = new XmlSerializer(GetType());
			XmlNode node = new XmlDocument();
			serializer.Serialize(node.CreateNavigator().AppendChild(), this);
			return node;
		}
	}
}