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
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.Execution;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	[DataCollectorConfigurationEditorTypeUri("configurationeditor://slickqa/SlickDataCollectorConfigurationEditor/0.0.1")]
	public sealed partial class ConfigurationEditor : UserControl, IDataCollectorConfigurationEditor, IConfigurationView
	{
		private IConfigurationController _controller;
		private DataCollectorSettings _collectorSettings;

		public ConfigurationEditor()
		{
			FormInitialize(new ConfigurationController(this));
		}

		public ConfigurationEditor(IConfigurationController controller)
		{
			FormInitialize(controller);
		}

		private void FormInitialize(IConfigurationController controller)
		{
			_controller = controller;

			InitializeComponent();
		}

		#region IDataCollectorConfigurationEditor Members

		public void Initialize(IServiceProvider serviceProvider, DataCollectorSettings settings)
		{
			ServiceProvider = serviceProvider;
			_collectorSettings = settings;

			_controller.Initialize(_collectorSettings);
		}

		private IServiceProvider ServiceProvider { get; set; }

		public void ResetToAgentDefaults()
		{
			_controller.ApplyDefaultSettings();
		}

		public DataCollectorSettings SaveData()
		{
			return _controller.SaveData();
		}

		public bool VerifyData()
		{
			return _controller.VerifyData();
		}

		#endregion

		#region IConfigurationView Members

		public ComboBox Scheme
		{
			get { return _scheme; }
		}

		public TextBox Host
		{
			get { return _host; }
		}

		public NumericUpDown Port
		{
			get { return _port; }
		}

		public Button AddProject
		{
			get { return _addProject; }
		}

		public Button AddRelease
		{
			get { return _addRelease; }
		}

		public TextBox SitePath
		{
			get { return _sitePath; }
		}

		public ComboBox Project
		{
			get { return _project; }
		}

		public Button GetProject
		{
			get { return _getProjects; }
		}

		public ComboBox Release
		{
			get { return _release; }
		}

		public CheckBox PreTestScreenshot
		{
			get { return _pretestScreenshot; }
		}

		public CheckBox PostTestScreenshot
		{
			get { return _posttestScreenshot; }
		}

		public CheckBox FailureScreenshot
		{
			get { return _failScreenshot; }
		}

		#endregion

	}
}