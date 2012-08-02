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
	public sealed class ResultDestination
	{
		public readonly string ProjectName;
		public readonly string ReleaseName;

		public ResultDestination() : this(String.Empty, String.Empty)
		{
		}

		public ResultDestination(string projectName, string release)
		{
			ProjectName = projectName;
			ReleaseName = release;
		}

		public ResultDestination(XmlNode projectElem)
		{
			if (projectElem.Attributes == null)
			{
				return;
			}
			XmlAttribute nameAttr = projectElem.Attributes["ProjectName"];
			ProjectName = nameAttr.Value;
			XmlAttribute releaseAttr = projectElem.Attributes["ReleaseName"];
			ReleaseName = releaseAttr.Value;
		}

		public ResultDestination(Project selectedProject, Release selectedRelease)
		{
			ProjectName = selectedProject.Name;
			ReleaseName = selectedRelease.Name;
		}

		public override string ToString()
		{
			return !String.IsNullOrWhiteSpace(ProjectName) ? ProjectName : String.Format("No Project Name {0}", GetHashCode());
		}

		public override int GetHashCode()
		{
			return ProjectName.GetHashCode() + ReleaseName.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as ResultDestination;
			if (other == null)
			{
				return false;
			}
			return ProjectName.Equals(other.ProjectName) && ReleaseName.Equals(other.ReleaseName);
		}

		public XmlNode ToXml(XmlDocument doc)
		{
			XmlNode node = doc.CreateNode(XmlNodeType.Element, "ResultDestination", String.Empty);
			XmlAttributeCollection attrCol = node.Attributes;

			XmlAttribute nameAttr = doc.CreateAttribute("ProjectName");
			nameAttr.Value = ProjectName;
			XmlAttribute releaseAttr = doc.CreateAttribute("ReleaseName");
			releaseAttr.Value = ReleaseName;

			Debug.Assert(attrCol != null, "attrCol != null");
			attrCol.Append(nameAttr);
			attrCol.Append(releaseAttr);

			return node;
		}

		public bool IsValid()
		{
			return !String.IsNullOrWhiteSpace(ProjectName);
		}
	}
}