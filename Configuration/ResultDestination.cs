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
			//TODO: Need Unit Test Coverage Here
			if (projectElem.Attributes == null)
			{
				return;
			}
			XmlAttribute nameAttr = projectElem.Attributes["ProjectName"];
			ProjectName = nameAttr.Value;
			XmlAttribute releaseAttr = projectElem.Attributes["ReleaseName"];
			ReleaseName = releaseAttr.Value;
		}

		//TODO: Need Unit Test Coverage Here
		public ResultDestination(Project selectedProject, Release selectedRelease)
		{
			ProjectName = selectedProject.Name;
			ReleaseName = selectedRelease.Name;
		}

		//TODO: Need Unit Test Coverage Here
		public override string ToString()
		{
			return !String.IsNullOrWhiteSpace(ProjectName) ? ProjectName : String.Format("No Project Name {0}", GetHashCode());
		}

		//TODO: Need Unit Test Coverage Here
		public override int GetHashCode()
		{
			return ProjectName.GetHashCode() + ReleaseName.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as ResultDestination;
			//TODO: Need Unit Test Coverage Here
			if (other == null)
			{
				return false;
			}
			return ProjectName.Equals(other.ProjectName) && ReleaseName.Equals(other.ReleaseName);
		}

		//TODO: Need Unit Test Coverage Here
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

		//TODO: Need Unit Test Coverage Here
		public bool IsValid()
		{
			return !String.IsNullOrWhiteSpace(ProjectName);
		}
	}
}