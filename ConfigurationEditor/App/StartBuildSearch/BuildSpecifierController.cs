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

using System.Xml;
using SlickQA.DataCollector.ConfigurationEditor.AppController;
using SlickQA.DataCollector.ConfigurationEditor.Commands;
using SlickQA.DataCollector.ConfigurationEditor.Events;
using SlickQA.DataCollector.EventAggregator;
using SlickQA.DataCollector.Models;

namespace SlickQA.DataCollector.ConfigurationEditor.App.StartBuildSearch
{
	public class BuildSpecifierController :
		IEventHandler<BuildProviderSelectedEvent>,
		IEventHandler<SettingsLoadedEvent>,
		IEventHandler<SaveDataEvent>
	{
		public BuildSpecifierController(IBuildSpecifierView view, IApplicationController appController)
		{
			AppController = appController;
			View = view;
			View.Controller = this;
			CurrentProvider = new BuildProviderInfo();
			DefaultProvider = new BuildProviderInfo();
		}

		private BuildProviderInfo DefaultProvider { get; set; }
		private BuildProviderInfo CurrentProvider { get; set; }
		private IApplicationController AppController { get; set; }
		private IBuildSpecifierView View { get; set; }

		#region IEventHandler<BuildProviderSelectedEvent> Members

		public void Handle(BuildProviderSelectedEvent eventData)
		{
			CurrentProvider = eventData.ProviderInfo;
			View.SetProviderText(CurrentProvider.ToString());
		}

		#endregion

		#region IEventHandler<SaveDataEvent> Members

		public void Handle(SaveDataEvent eventData)
		{
			eventData.TestInfo.BuildProvider = CurrentProvider;
		}

		#endregion

		#region IEventHandler<SettingsLoadedEvent> Members

		public void Handle(SettingsLoadedEvent eventData)
		{
			CurrentProvider = BuildProviderInfo.FromXml(eventData.Settings.Configuration);
			DefaultProvider = BuildProviderInfo.FromXml(eventData.Settings.DefaultConfiguration);

			View.SetProviderText(CurrentProvider.ToString());
		}

		#endregion

		public void Select()
		{
			AppController.Execute(new SelectBuildProviderData(CurrentProvider));
		}
	}
}