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
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.Configuration
{
	public sealed class ResultDestination : INotifyPropertyChanged

	{
		private const string PROJECT_ATTRIBUTE_NAME = "ProjectId";
		private const string RELEASE_ATTRIBUTE_NAME = "ReleaseId";

		public ResultDestination()
		{
		}

		public ResultDestination(XmlNode projectElem)
		{
			if (projectElem.Attributes == null)
			{
				return;
			}
			XmlAttribute nameAttr = projectElem.Attributes[PROJECT_ATTRIBUTE_NAME];
			Project = String.IsNullOrWhiteSpace(nameAttr.Value) ? null : new Project {Id = nameAttr.Value};

			XmlAttribute releaseAttr = projectElem.Attributes[RELEASE_ATTRIBUTE_NAME];
			Release = String.IsNullOrWhiteSpace(releaseAttr.Value) ? null : new Release {Id = releaseAttr.Value, ProjectId = nameAttr.Value};
		}

		private Release _release;
		private Project _project;

		public Release Release
		{
			get {
				if (_release != null)
				{
					_release.Get();
				}

				return _release;
			}
			set
			{
				_release = value;
				NotifyPropertyChanged("Release");
			}
		}

		public Project Project
		{
			get
			{
				if (_project != null)
				{
					_project.Get();
				}
				return _project;
			}
			set
			{
				_project = value;
				NotifyPropertyChanged("Project");
			}
		}

		public override bool Equals(object obj)
		{
			var other = obj as ResultDestination;
			if (other == null)
			{
				return false;
			}
			return Project.Equals(other.Project) && Release.Equals(other.Release);
		}

		public XmlNode ToXml(XmlDocument doc)
		{
			XmlNode node = doc.CreateNode(XmlNodeType.Element, "ResultDestination", String.Empty);
			XmlAttributeCollection attrCol = node.Attributes;

			Debug.Assert(Project != null, "proj != null");
			XmlAttribute projectAttr = doc.CreateAttribute(PROJECT_ATTRIBUTE_NAME);
			projectAttr.Value = _project.Id;

			XmlAttribute releaseAttr = doc.CreateAttribute(RELEASE_ATTRIBUTE_NAME);
			releaseAttr.Value = Release.Id;

			Debug.Assert(attrCol != null, "attrCol != null");
			attrCol.Append(projectAttr);
			attrCol.Append(releaseAttr);

			return node;
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}