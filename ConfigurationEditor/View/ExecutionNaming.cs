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
using System.ComponentModel;
using System.Windows.Forms;
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyExecutionNaming;
using SlickQA.DataCollector.Models;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	public sealed partial class ExecutionNaming : UserControl, IExecutionNamingView
	{
		private readonly BindingSource _bs;

		public ExecutionNaming()
		{
			InitializeComponent();

			Plans = new BindingList<TestPlan>();

			_bs = new BindingSource {DataSource = Plans};


			_planComboBox.DataSource = _bs;
		}

		private BindingList<TestPlan> Plans { get; set; }

		#region IExecutionNamingView Members

		public ExecutionNamingController Controller { set; get; }

		public void EnableAddPlanButton()
		{
			_addPlanButton.Enabled = true;
		}

		public void EnablePlanComboBox(bool state)
		{
			_planComboBox.Enabled = state;
		}

		public void SelectPlan(TestPlanInfo plan)
		{
			_planComboBox.SelectedItem = plan;
		}

		public void DisplayPlans(IEnumerable<TestPlan> plans)
		{
			Plans.Clear();
			foreach (TestPlan testPlan in plans)
			{
				Plans.Add(testPlan);
			}
			_planComboBox.SelectedIndex = -1;
			if (_planComboBox.Items.Count != 0)
			{
				_planComboBox.SelectedIndex = 0;
			}
		}

		#endregion

		private void PlanComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			var testPlan = _planComboBox.SelectedItem as TestPlan;
			if (testPlan != null)
			{
				Controller.TestPlanSupplied(testPlan);
			}
		}

		private void AddPlanButtonClick(object sender, EventArgs e)
		{
			Controller.AddTestPlan();
		}
	}
}
