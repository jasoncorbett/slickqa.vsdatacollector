/* Copyright 2012 AccessData Group, LLC.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.Execution;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.Configuration
{
	[DataCollectorConfigurationEditorTypeUri("configurationeditor://slickqa/SlickDataCollectorConfigurationEditor/0.0.1")]
	public partial class ConfigurationEditor : UserControl, IDataCollectorConfigurationEditor, IConfigurationView
	{
		private DataCollectorSettings _collectorSettings;
		private readonly IConfigurationController _controller;
		private readonly BindingList<Project> _projects; 

		public ConfigurationEditor()
			:this(new ConfigurationController())
		{

		}

		public ConfigurationEditor(IConfigurationController controller)
		{
			_controller = controller;
			_controller.View = this;
			_projects = new BindingList<Project>
			            {
			            	AllowNew = true,
							AllowEdit = false,
							AllowRemove = false
			            };

			InitializeComponent();

			project.ValueMember = null;
			project.DisplayMember = null;
			project.DataSource = _projects;
		}

		private IServiceProvider ServiceProvider { get; set; }

		#region IDataCollectorConfigurationEditor Members

		public void Initialize(IServiceProvider serviceProvider, DataCollectorSettings settings)
		{
			ServiceProvider = serviceProvider;
			_collectorSettings = settings;
		}

		public void ResetToAgentDefaults()
		{
			throw new NotImplementedException();
		}

		public DataCollectorSettings SaveData()
		{
			throw new NotImplementedException();
		}

		public bool VerifyData()
		{
			throw new NotImplementedException();
		}

		#endregion

		public void GetProjects(object sender, EventArgs e)
		{
			_controller.GetProjectsClicked();
		}

		public void PopulateProjects(IEnumerable<Project> projects)
		{
			_projects.RaiseListChangedEvents = false;
			_projects.Clear();
			foreach (var proj in projects)
			{
				_projects.Add(proj);
			}
			_projects.RaiseListChangedEvents = true;
		}
	}
}