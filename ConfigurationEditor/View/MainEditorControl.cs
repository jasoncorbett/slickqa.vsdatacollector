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
using SlickQA.DataCollector.ConfigurationEditor.App;
using SlickQA.DataCollector.ConfigurationEditor.App.SelectResultDestination;
using SlickQA.DataCollector.ConfigurationEditor.App.StartBuildSearch;
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyExecutionNaming;
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyScreenshotInfo;
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyUrlInfo;
using SlickQA.DataCollector.ConfigurationEditor.IoC_Setup;
using StructureMap;

namespace SlickQA.DataCollector.ConfigurationEditor.View
{

	[DataCollectorConfigurationEditorTypeUri("configurationeditor://slickqa/SlickDataCollectorConfigurationEditor/0.0.1")]
	public partial class MainEditorControl : UserControl, IEditorView, IDataCollectorConfigurationEditor
	{
		public MainEditorController Controller { get; set; }
		public void ClearUrlError()
		{
			_errorProvider.SetError(_urlSelector, string.Empty);
		}

		public void SetUrlError()
		{
			_errorProvider.SetError(_urlSelector, "Please supply a valid Slick webserver URL.");
		}

		public void ClearTestPlanError()
		{
			_errorProvider.SetError(_executionNaming, string.Empty);
		}

		public void SetTestPlanError()
		{
			_errorProvider.SetError(_executionNaming, "Please add a test plan.");
		}

		private IContainer IocContainer { get; set; }

		public MainEditorControl()
		{
			InitializeComponent();

			ConfigureIoCContainer();

			Controller = IocContainer.GetInstance<MainEditorController>();
		}

		private void ConfigureIoCContainer()
		{
			IocContainer = new Container();
			new BootStrapper(IocContainer);
			IocContainer.Inject<IEditorView>(this);
			IocContainer.Inject<IWin32Window>(this);
			IocContainer.Inject<ISetUrlView>(_urlSelector);
			IocContainer.Inject<IResultDestinationView>(_resultDestinationControl);
			IocContainer.Inject<IBuildSpecifierView>(_buildSpecifierControl);
			IocContainer.Inject<IScreenshotView>(_screenshotSetter);
			IocContainer.Inject<IExecutionNamingView>(_executionNaming);

			_urlSelector.Controller = IocContainer.GetInstance<UrlController>();
			_resultDestinationControl.Controller = IocContainer.GetInstance<ResultDestinationController>();
			_buildSpecifierControl.Controller = IocContainer.GetInstance<BuildSpecifierController>();
			_screenshotSetter.Controller = IocContainer.GetInstance<ScreenshotController>();
			_executionNaming.Controller = IocContainer.GetInstance<ExecutionNamingController>();
		}

		public void Initialize(IServiceProvider serviceProvider, DataCollectorSettings settings)
		{
			Controller.Initialize(serviceProvider, settings);
		}

		public bool VerifyData()
		{
			return Controller.VerifyData();
		}

		public DataCollectorSettings SaveData()
		{
			return Controller.SaveData();
		}

		public void ResetToAgentDefaults()
		{
			Controller.ResetToAgentDefaults();
		}
	}
}
