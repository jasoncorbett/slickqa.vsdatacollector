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

namespace SlickQA.DataCollector.ConfigurationEditor.App.StartBuildSearch
{
	public class BuildSpecifierController : IEventHandler<BuildProviderSelectedEvent>, IEventHandler<SettingsLoadedEvent>
	{
		public BuildSpecifierController(IBuildSpecifierView view, IApplicationController appController)
		{
			AppController = appController;
			View = view;
			View.Controller = this;
		}

		private IApplicationController AppController { get; set; }

		private IBuildSpecifierView View { get; set; }

		public void Select()
		{
			AppController.Execute(new SelectBuildProviderData());
		}

		public void Handle(BuildProviderSelectedEvent eventData)
		{
			var p = eventData.ProviderInfo;

			string fullMethodName;
			if (p.Method.DeclaringType != null)
			{
				fullMethodName = p.Method.DeclaringType.FullName + "." + p.Method.Name;
			}
			else
			{
				fullMethodName = p.Method.Name;
			}

			View.SetProviderText(p.Directory+"\\"+ p.AssemblyName + ":" + fullMethodName);
		}
		public void Handle(SettingsLoadedEvent eventData)
		{
			Config = eventData.Settings.Configuration;
			DefaultConfig = eventData.Settings.DefaultConfiguration;
		}

		private XmlElement DefaultConfig { get; set; }
		private XmlElement Config { get; set; }
	}
}