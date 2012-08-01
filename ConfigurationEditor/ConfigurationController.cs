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

			if (IsValid(_currentConfig.Url))
			{
				SetServerConfig(_currentConfig.Url);
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

		public static void SetServerConfig(string scheme, string host, int port, string sitePath)
		{
			ServerConfig.Scheme = scheme;
			ServerConfig.SlickHost = host;
			ServerConfig.Port = port;
			ServerConfig.SitePath = sitePath;
		}

		private static bool IsValid(SlickUrlType url)
		{
			return Uri.CheckSchemeName(url.Scheme) && !String.IsNullOrWhiteSpace(url.Host);
		}

		private static void SetServerConfig(SlickUrlType url)
		{
			SetServerConfig(url.Scheme, url.Host, url.Port, url.SitePath);
		}

		private void SetValues(SlickConfig config)
		{
			SlickUrlType slickUrl = config.Url;
			if (slickUrl != null)
			{
				_view.SetUrl(slickUrl);
			}
			ProjectType slickProject = config.Project;
			if (slickProject != null)
			{
				_view.SelectProject(slickProject);
			}
		}
	}
}