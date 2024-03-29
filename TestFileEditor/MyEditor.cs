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

using System.Windows.Forms;
using SlickQA.DataCollector.ConfigurationEditor.View;

namespace SlickQA.TestFileEditor
{
	public partial class MyEditor : UserControl, IEditorView
	{
		public MyEditor()
		{
			InitializeComponent();
		}

		#region IEditorView Members

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
			_errorProvider.SetError(_testPlan, string.Empty);
		}

		public void SetTestPlanError()
		{
			_errorProvider.SetError(_testPlan, "Please add a test plan.");
		}

		public IWin32Window Window
		{
			get { return this; }
		}

		#endregion
	}
}
