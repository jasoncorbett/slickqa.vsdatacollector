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
using System.Windows.Forms;
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyUrlInfo;
using SlickQA.DataCollector.Models;

namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	public sealed partial class UrlSelector : UserControl, ISetUrlView
	{
		public UrlSelector()
		{
			InitializeComponent();
		}

		#region ISetUrlView Members

		public UrlController Controller { get; set; }

		public void EnableButton(bool state)
		{
			_getProjects.Enabled = state;
		}

		public void LoadSchemes(List<string> schemes)
		{
			_scheme.DataSource = schemes;
			_scheme.DisplayMember = null;
			_scheme.ValueMember = null;
		}

		public void SetPort(int portNumber)
		{
			_port.Value = portNumber;
		}

		public void Update(UrlInfo url)
		{
			_scheme.SelectedItem = url.Scheme;
			_host.Text = url.HostName;
			_port.Value = url.Port;
			_sitePath.Text = url.SitePath;
		}

		#endregion

		private void SchemeSelectedIndexChanged(object sender, EventArgs e)
		{
			Controller.SchemeSupplied(_scheme.SelectedItem as string);
		}

		private void HostTextChanged(object sender, EventArgs e)
		{
			
		}

		private void PortValueChanged(object sender, EventArgs e)
		{
			Controller.PortSupplied(Convert.ToInt32(_port.Value));
		}

		private void SitePathTextChanged(object sender, EventArgs e)
		{
			Controller.SitePathSupplied(_sitePath.Text);
		}

		private void GetProjectsClick(object sender, EventArgs e)
		{
			Controller.GetProjects();
		}

		private void UrlSelectorLoad(object sender, EventArgs e)
		{
			if (Controller != null)
			{
				Controller.Load();
			}
		}

		private void HostLeave(object sender, EventArgs e)
		{
			Controller.HostSupplied(_host.Text);
		}
	}
}
