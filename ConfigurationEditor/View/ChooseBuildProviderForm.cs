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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyBuildProvider;

namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	public sealed partial class ChooseBuildProviderForm : Form, IChooseBuildProviderView
	{
		private IWin32Window ParentAppWindow { get; set; }
		public ChooseBuildProviderController Controller { private get; set; }

		public ChooseBuildProviderForm(IWin32Window owner)
		{
			InitializeComponent();
			ParentAppWindow = owner;
		}

		public void Run()
		{
			ShowDialog(ParentAppWindow);
		}

		public void ShowOpenFileDialog()
		{
			DialogResult result = _openFileDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				_assemblyTextBox.Text = _openFileDialog.FileName;
			}
		}

		public void PopulateTree(Dictionary<Type, List<MethodInfo>> candidateTypes)
		{
			_methodTreeView.Nodes.Clear();
			foreach (KeyValuePair<Type, List<MethodInfo>> kvp in candidateTypes)
			{
				TreeNode node = GetNode(kvp.Key);
				IEnumerable<TreeNode> children = GetChildren(kvp.Value);
				foreach (TreeNode child in children)
				{
					node.Nodes.Add(child);
				}
				_methodTreeView.Nodes.Add(node);
			}

			_methodTreeView.ExpandAll();
		}

		private IEnumerable<TreeNode> GetChildren(IEnumerable<MethodInfo> methodInfos)
		{
			return methodInfos.Select(info => new TreeNode(info.Name) {Tag = info}).ToList();
		}

		private TreeNode GetNode(Type key)
		{
			var node = new TreeNode(key.FullName) {Tag = key};
			return node;
		}

		private void BrowseClick(object sender, EventArgs e)
		{
			Controller.Browse();
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

		private void MethodTreeViewAfterSelect(object sender, TreeViewEventArgs e)
		{
			var method = e.Node.Tag as MethodInfo;
			if (method != null)
			{
				Controller.MethodSelected(method);
			}
		}

		private void AssemblyTextBoxTextChanged(object sender, EventArgs e)
		{
			Controller.FilePathSupplied(_assemblyTextBox.Text);
		}
	}
}
