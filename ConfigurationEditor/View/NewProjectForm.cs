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
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyProjectInfo;

namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	public sealed partial class NewProjectForm : Form, INewProjectView
	{
		public NewProjectForm(IWin32Window owner)
		{
			InitializeComponent();
			ParentAppWindow = owner;
		}

		private IWin32Window ParentAppWindow { get; set; }

		#region INewProjectView Members

		public NewProjectController Controller { private get; set; }

		public void Run()
		{
			ShowDialog(ParentAppWindow);
		}

		public void ClearReleaseNameError()
		{
			_errorProvider.SetError(_releaseName, string.Empty);
		}

		public void SetReleaseNameError()
		{
			_errorProvider.SetError(_releaseName, "Please provide a name for the first release in the project.");
		}

		public void SetProjectNameError()
		{
			_errorProvider.SetError(_projectName, "Please provide a project name.");
		}

		public void ClearProjectNameError()
		{
			_errorProvider.SetError(_projectName, string.Empty);
		}

		public void UpdateOkEnabledState()
		{
			_okButton.Enabled = !ErrorsOnPage();
		}

		#endregion

		private bool ErrorsOnPage()
		{
			return !string.IsNullOrWhiteSpace(_errorProvider.GetError(_projectName)) || !string.IsNullOrWhiteSpace(_errorProvider.GetError(_releaseName));
		}

		private void ProjectNameTextChanged(object sender, EventArgs e)
		{
			Controller.ProjectNameSupplied(_projectName.Text);
		}

		private void ProjectDescriptionTextChanged(object sender, EventArgs e)
		{
			Controller.ProjectDescriptionSupplied(_projectDescription.Text);
		}

		private void ReleaseNameTextChanged(object sender, EventArgs e)
		{
			Controller.ReleaseNameSupplied(_releaseName.Text);
		}

		private void ProjectTagsTextChanged(object sender, EventArgs e)
		{
			Controller.ProjectTagsSupplied(_projectTags.Text);
		}

		private void CreateButtonClick(object sender, EventArgs e)
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
