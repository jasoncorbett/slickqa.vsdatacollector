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
using System.ComponentModel;
using System.Windows.Forms;
using SlickQA.DataCollector.ConfigurationEditor.App.SelectResultDestination;
using SlickQA.DataCollector.Models;

namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	public sealed partial class SelectResultDestinationControl : UserControl, IResultDestinationView
	{
		public SelectResultDestinationControl()
		{
			InitializeComponent();

			Projects = new BindingList<ProjectInfo>();
			_project.DataSource = Projects;

			Releases = new BindingList<ReleaseInfo>();
			_release.DataSource = Releases;
		}

		private BindingList<ProjectInfo> Projects { get; set; }

		private BindingList<ReleaseInfo> Releases { get; set; }

		#region IResultDestinationView Members

		public ResultDestinationController Controller { private get; set; }

		public void LoadProjectList(IEnumerable<ProjectInfo> projectList)
		{
			Projects.Clear();
			foreach (ProjectInfo project in projectList)
			{
				Projects.Add(project);
			}
			_project.Enabled = true;
			_project.SelectedIndex = -1;
			_project.SelectedIndex = 0;
		}

		public void SelectProject(ProjectInfo project)
		{
			int index = _project.Items.IndexOf(project);
			_project.SelectedIndex = -1;
			_project.SelectedIndex = index;
		}

		public void LoadReleaseList(IEnumerable<ReleaseInfo> releaseList)
		{
			Releases.Clear();
			foreach (ReleaseInfo release in releaseList)
			{
				Releases.Add(release);
			}
			_release.Enabled = true;
			_release.SelectedIndex = -1;
			_release.SelectedIndex = 0;
		}

		public void SelectRelease(ReleaseInfo release)
		{
			int index = _release.Items.IndexOf(release);
			_release.SelectedIndex = -1;
			_release.SelectedIndex = index;
		}

		public void EnableAddReleaseButton()
		{
			_addRelease.Enabled = true;
		}

		public void EnableAddProjectButton()
		{
			_addProject.Enabled = true;
		}

		#endregion

		private void ProjectSelectedIndexChanged(object sender, EventArgs e)
		{
			var project = _project.SelectedItem as ProjectInfo;
			if (project != null)
			{
				Controller.ProjectSupplied(project);
			}
		}

		private void ReleaseSelectedIndexChanged(object sender, EventArgs e)
		{
			var release = _release.SelectedItem as ReleaseInfo;
			if (release != null)
			{
				Controller.ReleaseSupplied(release);
			}
		}

		private void AddProjectClick(object sender, EventArgs e)
		{
			Controller.AddProject();
		}

		private void AddReleaseClick(object sender, EventArgs e)
		{
			Controller.AddRelease(_project.SelectedItem as ProjectInfo);
		}
	}
}
