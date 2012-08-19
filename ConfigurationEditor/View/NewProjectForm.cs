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
		private IWin32Window ParentAppWindow { get; set; }
		public NewProjectController Controller { private get; set; }

		public NewProjectForm(IWin32Window owner)
		{
			InitializeComponent();
			ParentAppWindow = owner;
		}

		public void Run()
		{
			ShowDialog(ParentAppWindow);
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
