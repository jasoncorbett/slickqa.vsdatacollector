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
using SlickQA.DataCollector.ConfigurationEditor.App.StartBuildSearch;

namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	public sealed partial class BuildSpecifierControl : UserControl, IBuildSpecifierView
	{
		public BuildSpecifierControl()
		{
			InitializeComponent();
		}

		#region IBuildSpecifierView Members

		public BuildSpecifierController Controller { get; set; }
		public void SetProviderText(string provider)
		{
			_buildProviderText.Text = provider;
		}

		#endregion

		private void BuildProviderClick(object sender, EventArgs e)
		{
			Controller.Select();
		}
	}
}