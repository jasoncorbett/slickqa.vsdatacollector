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
using SlickQA.DataCollector.ConfigurationEditor.AppController;
using SlickQA.DataCollector.ConfigurationEditor.Events;
using SlickQA.DataCollector.EventAggregator;

namespace SlickQA.DataCollector.ConfigurationEditor.App
{
	public class MainEditorController :
		IEventHandler<UrlValidatedEvent>,
		IEventHandler<UrlInvalidatedEvent>,
		IEventHandler<TestPlanValidatedEvent>,
		IEventHandler<TestPlanInvalidatedEvent>
	{
		public MainEditorController(IEditorView view, IApplicationController appController)
		{
			View = view;
			AppController = appController;
			View.Controller = this;
		}

		private IEditorView View { get; set; }
		private IApplicationController AppController { get; set; }
		private IServiceProvider ServiceProvider { get; set; }
		private DataCollectorSettings Settings { get; set; }
		private bool UrlIsValid { get; set; }
		private bool TestPlanIsValid { get; set; }

		#region IEventHandler<TestPlanInvalidatedEvent> Members

		public void Handle(TestPlanInvalidatedEvent eventData)
		{
			TestPlanIsValid = false;
			View.SetTestPlanError();
		}

		#endregion

		#region IEventHandler<TestPlanValidatedEvent> Members

		public void Handle(TestPlanValidatedEvent eventData)
		{
			TestPlanIsValid = true;
			View.ClearTestPlanError();
		}

		#endregion

		#region IEventHandler<UrlInvalidatedEvent> Members

		public void Handle(UrlInvalidatedEvent eventData)
		{
			UrlIsValid = false;
			View.SetUrlError();
		}

		#endregion

		#region IEventHandler<UrlValidatedEvent> Members

		public void Handle(UrlValidatedEvent eventData)
		{
			UrlIsValid = true;
			View.ClearUrlError();
		}

		#endregion

		public void Initialize(IServiceProvider serviceProvider, DataCollectorSettings settings)
		{
			AppController.Raise(new ServiceProviderLoadedEvent(serviceProvider));
			AppController.Raise(new SettingsLoadedEvent(settings));

			ServiceProvider = serviceProvider;
			Settings = settings;
		}

		public bool VerifyData()
		{
			return UrlIsValid && TestPlanIsValid;
		}

		public DataCollectorSettings SaveData()
		{
			AppController.Raise(new SaveDataEvent(Settings));
			return Settings;
		}

		public void ResetToAgentDefaults()
		{
			AppController.Raise(new ResetEvent());
		}
	}
}
