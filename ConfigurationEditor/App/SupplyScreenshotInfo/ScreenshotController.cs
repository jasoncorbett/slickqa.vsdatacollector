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
	public class ScreenshotController : IEventHandler<SettingsLoadedEvent>, IEventHandler<ResetEvent>, IEventHandler<SaveDataEvent>
	{
		private ScreenshotInfo DefaultScreenshot { get; set; }
		private ScreenshotInfo CurrentScreenshot { get; set; }
		private IScreenshotView View { get; set; }

		public ScreenshotController(IScreenshotView view)
		{
			View = view;
			View.Controller = this;
		}

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

		public void Handle(SettingsLoadedEvent eventData)
		{
			CurrentScreenshot = GetScreenshotInfoFromConfig(eventData.Settings.Configuration);
			DefaultScreenshot = GetScreenshotInfoFromConfig(eventData.Settings.DefaultConfiguration);

			View.Update(CurrentScreenshot.PreTest, CurrentScreenshot.PostTest, CurrentScreenshot.FailedTest);
		}


		public void Handle(ResetEvent eventData)
		{
			CurrentScreenshot = new ScreenshotInfo(DefaultScreenshot);
			View.Update(CurrentScreenshot.PreTest, CurrentScreenshot.PostTest, CurrentScreenshot.FailedTest);
		}


		public void Handle(SaveDataEvent eventData)
		{
			var config = eventData.Settings.Configuration;

			var node = CurrentScreenshot.ToXmlNode();

			XmlNodeList elements = config.GetElementsByTagName("Screenshot");
			if (elements.Count != 0)
			{
				config.ReplaceChild(node, elements[0]);
			}
			else
			{
				config.AppendChild(node);
			}
		}

		private static ScreenshotInfo GetScreenshotInfoFromConfig(XmlElement xmlElement)
		{
			return new ScreenshotInfo(xmlElement.GetElementsByTagName("Screenshot"));
		}
	}
}