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
using Microsoft.VisualStudio.TestTools.Execution;
using SlickQA.DataCollector.Configuration;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	//TODO: Need Unit Test Coverage Here
	[DataCollectorConfigurationEditorTypeUri("configurationeditor://slickqa/SlickDataCollectorConfigurationEditor/0.0.1")]
	public sealed partial class ConfigurationEditor : UserControl, IDataCollectorConfigurationEditor, IConfigurationView
	{
		private readonly IConfigurationController _controller;
		private readonly BindingList<Project> _projects;
		private readonly BindingList<Release> _releases;
		private readonly BindingList<String> _schemes;
		private DataCollectorSettings _collectorSettings;

		public ConfigurationEditor() : this(new ConfigurationController())
		{
		}

		public ConfigurationEditor(IConfigurationController controller)
		{
			_controller = controller;
			_controller.View = this;

			_schemes = new BindingList<string> {Uri.UriSchemeHttp, Uri.UriSchemeHttps};
			_projects = new BindingList<Project> {AllowNew = true, AllowEdit = false, AllowRemove = false};
			_releases = new BindingList<Release> {AllowNew = true, AllowEdit = false, AllowRemove = false};

			InitializeComponent();

			_scheme.ValueMember = null;
			_scheme.DisplayMember = null;
			_scheme.DataSource = _schemes;

			_project.ValueMember = null;
			_project.DisplayMember = null;
			_project.DataSource = _projects;
			_project.SelectedItem = null;

			_release.ValueMember = null;
			_release.DisplayMember = null;
			_release.DataSource = _releases;
			_release.SelectedItem = null;
		}

		private IServiceProvider ServiceProvider { get; set; }

		#region IConfigurationView Members

		public void GetProjects(object sender, EventArgs e)
		{
			_controller.SetUrl(_scheme.Text, _host.Text, (int)_port.Value, _sitePath.Text);
			_controller.GetProjects();
		}

		public void PopulateProjects(IEnumerable<Project> projects)
		{
			_projects.Clear();
			_projects.Add(new NullProject());
			foreach (Project proj in projects)
			{
				_projects.Add(proj);
			}
		}

		public void SetUrl(SlickUrlType slickUrl)
		{
			SuspendLayout();
			_scheme.SelectedItem = slickUrl.Scheme;
			_host.Text = slickUrl.Host;
			_port.Value = slickUrl.Port;
			_sitePath.Text = slickUrl.SitePath;
			ResumeLayout();
		}

		public void SelectProjectAndRelease(ResultDestination destination)
		{
			SuspendLayout();
			//Handle the releases on our own because we are loading from the config.
			_project.SelectedIndexChanged -= GetReleases;

			var searchProject = new Project {Name = destination.ProjectName};
			searchProject.Get();
			var searchRelease = new Release {ProjectId = searchProject.Id, Name = destination.ReleaseName};
			searchRelease.Get();

			_project.SelectedItem = searchProject;
			GetReleases(this, null);

			_release.SelectedItem = searchRelease;

			_project.SelectedIndexChanged += GetReleases;
			ResumeLayout();
		}

		public void SetScreenshotSettings(ScreenShotSettings screenshotSettings)
		{
			_pretestScreenshot.Checked = screenshotSettings.PreTest;
			_posttestScreenshot.Checked = screenshotSettings.PostTest;
			_failScreenshot.Checked = screenshotSettings.OnFailure;
		}

		#endregion

		#region IDataCollectorConfigurationEditor Members

		public void Initialize(IServiceProvider serviceProvider, DataCollectorSettings settings)
		{
			ServiceProvider = serviceProvider;
			_collectorSettings = settings;

			_controller.InitializeSettings(_collectorSettings);
		}

		public void ResetToAgentDefaults()
		{
			_controller.ApplyDefaultSettings();
		}

		public DataCollectorSettings SaveData()
		{
			_controller.SetUrl(_scheme.Text, _host.Text, (int)_port.Value, _sitePath.Text);
			_controller.SetResultDestination(_project.SelectedItem as Project, _release.SelectedItem as Release);
			_controller.SetScreenshotSettings(_pretestScreenshot.Checked, _posttestScreenshot.Checked, _failScreenshot.Checked);
			
			_controller.SaveSettings(_collectorSettings.Configuration);
			return _collectorSettings;
		}

		public bool VerifyData()
		{
			return _scheme.SelectedItem != null && !String.IsNullOrWhiteSpace(_host.Text) 
				&& _project.SelectedItem != null && _release.SelectedItem != null;
		}

		#endregion

		private void GetReleases(object sender, EventArgs e)
		{
			_releases.Clear();
			var project = _project.SelectedItem as Project;
			if (project == null)
			{
				return;
			}

			foreach (Release rel in project.Releases)
			{
				_releases.Add(rel);
			}
		}

		private void CheckedChanged(object sender, EventArgs e)
		{
			_controller.SetScreenshotSettings(_pretestScreenshot.Checked, _posttestScreenshot.Checked, _failScreenshot.Checked);
		}
	}
}