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
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyReleaseInfo;

namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	public sealed partial class NewReleaseForm : Form, INewReleaseView
	{
		public NewReleaseForm(IWin32Window owner)
		{
			InitializeComponent();
			ParentAppWindow = owner;
		}

		private IWin32Window ParentAppWindow { get; set; }

		#region INewReleaseView Members

		public NewReleaseController Controller { private get; set; }

		public void Run()
		{
			ShowDialog(ParentAppWindow);
		}

		public void SetReleaseNameError()
		{
			_errorProvider.SetError(_release, "Please provide a release name.");
		}

		public void ClearReleaseNameError()
		{
			_errorProvider.SetError(_release, string.Empty);
		}

		public void UpdateOkEnabledState()
		{
			_okButton.Enabled = !ErrorsOnPage();
		}

		#endregion

		private bool ErrorsOnPage()
		{
			return !string.IsNullOrWhiteSpace(_errorProvider.GetError(_release));
		}

		private void ReleaseTextChanged(object sender, EventArgs e)
		{
			Controller.ReleaseNameSupplied(_release.Text);
		}

		private void OkButtonClick(object sender, EventArgs e)
		{
			Controller.Create();
			Close();
		}

		private void CancelButtonClick(object sender, EventArgs e)
		{
			Controller.Cancel();
			Close();
		}
	}
}
