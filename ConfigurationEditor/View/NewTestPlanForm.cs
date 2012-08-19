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
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyTestPlanInfo;

namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	public partial class NewTestPlanForm : Form, INewTestPlanView
	{
		private IWin32Window ParentAppWindow { get; set; }
		public NewTestPlanController Controller { set; get; }

		public NewTestPlanForm(IWin32Window owner)
		{
			InitializeComponent();
			ParentAppWindow = owner;
		}

		public void Run()
		{
			Show(ParentAppWindow);
		}

		private void PlanNameTextChanged(object sender, EventArgs e)
		{
			Controller.PlanNameSupplied(_planName.Text);
		}

		private void CreatorNameTextChanged(object sender, EventArgs e)
		{
			Controller.CreatorSupplied(_creatorName.Text);
		}

		private void OkButtonClick(object sender, EventArgs e)
		{
			Controller.Ok();
			Close();
		}

		private void CancelButtonClick(object sender, EventArgs e)
		{
			Controller.Cancel();
			Close();
		}
	}
}
