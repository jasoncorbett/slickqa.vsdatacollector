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
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.Configuration
{
	public sealed class ProjectType
	{
		public readonly string Name;

		public ProjectType() : this(String.Empty)
		{
		}

		public ProjectType(string name)
		{
			Name = name;
		}

		public ProjectType(XmlNode projectElem)
		{
			if (projectElem.Attributes == null)
			{
				return;
			}
			XmlAttribute nameAttr = projectElem.Attributes["Name"];
			Name = nameAttr.Value;
		}

		public ProjectType(Project selectedProject)
		{
			Name = selectedProject.Name;
		}

		public override string ToString()
		{
			return !String.IsNullOrWhiteSpace(Name) ? Name : String.Format("No Name {0}", GetHashCode());
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as ProjectType;
			return other != null && Name.Equals(other.Name);
		}

		public XmlNode ToXml(XmlDocument doc)
		{
			XmlNode node = doc.CreateNode(XmlNodeType.Element, "Project", String.Empty);
			XmlAttributeCollection attrCol = node.Attributes;

			XmlAttribute nameAttr = doc.CreateAttribute("Name");
			nameAttr.Value = Name;

			Debug.Assert(attrCol != null, "attrCol != null");
			attrCol.Append(nameAttr);

			return node;
		}

		public bool IsValid()
		{
			return !String.IsNullOrWhiteSpace(Name);
		}
	}
}