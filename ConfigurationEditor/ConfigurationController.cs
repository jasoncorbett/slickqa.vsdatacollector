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
using Microsoft.VisualStudio.TestTools.Execution;
using SlickQA.DataCollector.Configuration;
using SlickQA.SlickSharp;
using SlickQA.SlickSharp.Web;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	public sealed class ConfigurationController : IConfigurationController
	{
		private SlickConfig _currentConfig;
		private SlickConfig _defaultConfig;
		private IConfigurationView _view;

		public ConfigurationController()
		{
			_view = null;
		}

		public ConfigurationController(IConfigurationView configurationEditor)
		{
			_view = configurationEditor;
		}

		#region IConfigurationController Members

		public void GetProjectsClicked()
		{
			List<Project> projects = JsonObject<Project>.GetList();

			_view.PopulateProjects(projects);
		}

		public IConfigurationView View
		{
			set { _view = value; }
		}

		public void InitializeSettings(DataCollectorSettings dataCollectorSettings)
		{
			_defaultConfig = SlickConfig.LoadConfig(dataCollectorSettings.DefaultConfiguration);
			_currentConfig = SlickConfig.LoadConfig(dataCollectorSettings.Configuration);

			if (SlickUrlType.IsValid(_currentConfig.Url))
			{
				SlickConfig.SetServerConfig(_currentConfig.Url);
				GetProjectsClicked();
			}

			SetValues(_currentConfig);
		}

		public void ApplyDefaultSettings()
		{
			_view.PopulateProjects(new List<Project>());
			SetValues(_defaultConfig);
		}

		#endregion

		private void SetValues(SlickConfig config)
		{
			SlickUrlType slickUrl = config.Url;
			if (slickUrl != null)
			{
				_view.SetUrl(slickUrl);
			}
			ResultDestination slickProject = config.ResultDestination;
			if (slickProject.IsValid())
			{
				_view.SelectProject(slickProject);
			}
		}
	}
}