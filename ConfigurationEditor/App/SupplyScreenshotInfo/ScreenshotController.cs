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
using SlickQA.DataCollector.ConfigurationEditor.Events;
using SlickQA.DataCollector.EventAggregator;
using SlickQA.DataCollector.Models;

namespace SlickQA.DataCollector.ConfigurationEditor.App.SupplyScreenshotInfo
{
	public class ScreenshotController :
		IEventHandler<SettingsLoadedEvent>,
		IEventHandler<ResetEvent>,
		IEventHandler<SaveDataEvent>
	{
		public ScreenshotController(IScreenshotView view)
		{
			CurrentScreenshot = new ScreenshotInfo();
			View = view;
			View.Controller = this;
		}

		private ScreenshotInfo CurrentScreenshot { get; set; }
		private ScreenshotInfo DefaultScreenshot { get; set; }
		private IScreenshotView View { get; set; }

		#region IEventHandler<ResetEvent> Members

		public void Handle(ResetEvent eventData)
		{
			CurrentScreenshot = new ScreenshotInfo(DefaultScreenshot);

			View.Update(CurrentScreenshot);
		}

		#endregion

		#region IEventHandler<SaveDataEvent> Members

		public void Handle(SaveDataEvent eventData)
		{
			XmlElement config = eventData.Settings.Configuration;
			config.UpdateTagWithNewValue(ScreenshotInfo.TAG_NAME, CurrentScreenshot.ToXmlNode());
		}

		#endregion

		#region IEventHandler<SettingsLoadedEvent> Members

		public void Handle(SettingsLoadedEvent eventData)
		{
			CurrentScreenshot = ScreenshotInfo.FromXml(eventData.Settings.Configuration);
			DefaultScreenshot = ScreenshotInfo.FromXml(eventData.Settings.DefaultConfiguration);

			View.Update(CurrentScreenshot);
		}

		#endregion

		public void PretestSettingSupplied(bool preTestState)
		{
			CurrentScreenshot.PreTest = preTestState;
		}

		public void PosttestSettingSupplied(bool postTestState)
		{
			CurrentScreenshot.PostTest = postTestState;
		}

		public void FailureSettingSupplied(bool failedTestState)
		{
			CurrentScreenshot.FailedTest = failedTestState;
		}
	}
}