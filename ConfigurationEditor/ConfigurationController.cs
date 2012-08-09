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
using Microsoft.VisualStudio.TestTools.Execution;
using SlickQA.DataCollector.Configuration;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	public sealed class ConfigurationController : IConfigurationController
	{
		private Config _currentConfig;
		private DataCollectorSettings _dataCollectorSettings;
		private Config _defaultConfig;
		private readonly IConfigurationView _view;
		private SelectorController _selectorController;
		private UrlController _urlController;
		private ScreenshotController _screenshotController;


		public ConfigurationController(IConfigurationView configurationEditor)
		{
			_view = configurationEditor;
			_view.VisibleChanged += ViewOnVisibleChanged;
		}

		private void ViewOnVisibleChanged(object sender, EventArgs eventArgs)
		{
			var form = sender as IConfigurationView;
			if (form.Visible)
			{
				ResetWithConfig(_currentConfig);
			}
		}

		#region IConfigurationController Members

		public void Initialize(DataCollectorSettings dataCollectorSettings)
		{
			_dataCollectorSettings = dataCollectorSettings;
			_defaultConfig = Config.LoadConfig(dataCollectorSettings.DefaultConfiguration);
			_currentConfig = Config.LoadConfig(dataCollectorSettings.Configuration);

			_selectorController = new SelectorController(_view);
			_urlController = new UrlController(_view);
			_screenshotController = new ScreenshotController(_view);

			ResetConfig += _urlController.SetConfig;
			ResetConfig += _selectorController.SetConfig;
			ResetConfig += _screenshotController.SetConfig;
		}

		private void ResetWithConfig(Config config)
		{
			if (ResetConfig != null)
			{
				ResetConfig(this, new ResetConfigHandlerArgs(config));
				if (_currentConfig.Url.IsValid)
				{
					_selectorController.GetProjects(_view.GetProject, new EventArgs());
				}
			}
		}

		public void ApplyDefaultSettings()
		{
			_currentConfig = _defaultConfig;
			ResetWithConfig(_currentConfig);
		}

		public event ResetConfigHandler ResetConfig;

		public DataCollectorSettings SaveData()
		{
			_dataCollectorSettings.Configuration.InnerText = String.Empty;

			_currentConfig.ConfigToXml(_dataCollectorSettings.Configuration);
			return _dataCollectorSettings;
		}

		public bool VerifyData()
		{
			return _currentConfig.Url.Scheme != null && !String.IsNullOrWhiteSpace(_currentConfig.Url.Host)
			       && _currentConfig.ResultDestination.Project != null && _currentConfig.ResultDestination.Release != null;
		}

		#endregion

		public void Dispose()
		{
			ResetConfig -= _selectorController.SetConfig;
			ResetConfig -= _urlController.SetConfig;
			ResetConfig -= _screenshotController.SetConfig;
		}
	}
}