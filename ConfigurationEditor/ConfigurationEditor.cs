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
	[DataCollectorConfigurationEditorTypeUri("configurationeditor://slickqa/SlickDataCollectorConfigurationEditor/0.0.1")]
	public sealed partial class ConfigurationEditor : UserControl, IDataCollectorConfigurationEditor, IConfigurationView
	{
		private readonly IConfigurationController _controller;
		private readonly BindingList<Project> _projects;
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

			InitializeComponent();

			project.ValueMember = null;
			project.DisplayMember = null;
			project.DataSource = _projects;
			project.SelectedItem = null;

			protocol.ValueMember = null;
			protocol.DisplayMember = null;
			protocol.DataSource = _schemes;
		}

		private IServiceProvider ServiceProvider { get; set; }

		public void GetProjects(object sender, EventArgs e)
		{
			SlickConfig.SetServerConfig(protocol.Text, host.Text, (int)port.Value, sitePath.Text);
			_controller.GetProjectsClicked();
		}

		public void PopulateProjects(IEnumerable<Project> projects)
		{
			_projects.Clear();
			foreach (Project proj in projects)
			{
				_projects.Add(proj);
			}
		}

		public void SetUrl(SlickUrlType slickUrl)
		{
			SuspendLayout();
			protocol.SelectedItem = slickUrl.Scheme;
			host.Text = slickUrl.Host;
			port.Value = slickUrl.Port;
			sitePath.Text = slickUrl.SitePath;
			ResumeLayout();
		}

		public void SelectProject(ProjectType slickProject)
		{
			SuspendLayout();
			var searchProject = new Project {Name = slickProject.Name};
			searchProject.Get();
			project.SelectedItem = searchProject;
			ResumeLayout();
		}

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
			var projectType = new ProjectType(project.SelectedItem as Project);
			var url = new SlickUrlType(protocol.SelectedItem.ToString(), host.Text, (int)port.Value, sitePath.Text);
			_collectorSettings.Configuration.InnerText = String.Empty;

			_collectorSettings.Configuration.AppendChild(projectType.ToXml(_collectorSettings.Configuration.OwnerDocument));
			_collectorSettings.Configuration.AppendChild(url.ToXml(_collectorSettings.Configuration.OwnerDocument));
			return _collectorSettings;
		}

		public bool VerifyData()
		{
			return protocol.SelectedItem != null && !String.IsNullOrWhiteSpace(host.Text) && project.SelectedItem != null;
		}
	}
}