﻿// Copyright 2012 AccessData Group, LLC.
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
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyScreenshotInfo;

namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	public sealed partial class ScreenshotSetter : UserControl, IScreenshotView
	{
		public ScreenshotController Controller { get; set; }

		public ScreenshotSetter()
		{
			InitializeComponent();
		}

		public void Update(bool preTest, bool postTest, bool failedTest)
		{
			_pretestScreenshot.Checked = preTest;
			_posttestScreenshot.Checked = postTest;
			_failScreenshot.Checked = failedTest;
		}

		private void PretestScreenshotCheckedChanged(object sender, EventArgs e)
		{
			Controller.PretestSettingSupplied(_pretestScreenshot.Checked);
		}

		private void PosttestScreenshotCheckedChanged(object sender, EventArgs e)
		{
			Controller.PosttestSettingSupplied(_posttestScreenshot.Checked);
		}

		private void FailScreenshotCheckedChanged(object sender, EventArgs e)
		{
			Controller.FailureSettingSupplied(_failScreenshot.Checked);
		}
	}
}
