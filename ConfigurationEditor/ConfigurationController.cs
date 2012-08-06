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
using System.Xml;
using Microsoft.VisualStudio.TestTools.Execution;
using SlickQA.DataCollector.Configuration;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	//TODO: Need Unit Test Coverage Here
	public sealed class ConfigurationController : IConfigurationController
	{
		private SlickConfig _currentConfig;
		private DataCollectorSettings _dataCollectorSettings;
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

		public void GetProjects()
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
			_dataCollectorSettings = dataCollectorSettings;
			_defaultConfig = SlickConfig.LoadConfig(dataCollectorSettings.DefaultConfiguration);
			_currentConfig = SlickConfig.LoadConfig(dataCollectorSettings.Configuration);

			if (SlickUrlType.IsValid(_currentConfig.Url))
			{
				SlickConfig.SetServerConfig(_currentConfig.Url);
				GetProjects();
			}

			SetValues(_currentConfig);
		}

		public void ApplyDefaultSettings()
		{
			_view.PopulateProjects(new List<Project>());
			SetValues(_defaultConfig);
		}

		public void SetUrl(string scheme, string host, int port, string sitePath)
		{
			_currentConfig.Url = new SlickUrlType(scheme, host, port, sitePath);
			SlickConfig.SetServerConfig(_currentConfig.Url);
		}

		public void SetResultDestination(Project project, Release release)
		{
			_currentConfig.ResultDestination = new ResultDestination(project, release);
		}

		public void SaveSettings(XmlElement configuration)
		{
			configuration.InnerText = String.Empty;

			_currentConfig.ConfigToXml(configuration);
		}

		public void SetScreenshotSettings(bool takePreTestScreenshot, bool takePostTestScreenshot, bool takeScreenshotOnFailure)
		{
			_currentConfig.ScreenshotSettings = new ScreenShotSettings(takePreTestScreenshot, takePostTestScreenshot, takeScreenshotOnFailure);
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
				_view.SelectProjectAndRelease(slickProject);
			}

			_view.SetScreenshotSettings(_currentConfig.ScreenshotSettings);
		}
	}
}