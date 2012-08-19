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

using SlickQA.DataCollector.Models;

namespace SlickQA.DataCollector.ConfigurationEditor.App.SupplyTestPlanInfo
{
	public class NewTestPlanController : IGetNewTestPlanInfo
	{
		public NewTestPlanController(INewTestPlanView view)
		{
			View = view;
			View.Controller = this;
		}

		private ServiceResult ServiceResult { get; set; }
		private string PlanName { get; set; }
		private string Creator { get; set; }
		private INewTestPlanView View { get; set; }

		#region IGetNewTestPlanInfo Members

		public Result<TestPlanInfo> Get()
		{
			View.Run();
			TestPlanInfo testPlan = null;
			if (ServiceResult == ServiceResult.Ok)
			{
				testPlan = new TestPlanInfo(string.Empty, PlanName, string.Empty, Creator);
			}
			return new Result<TestPlanInfo>(ServiceResult, testPlan);
		}

		#endregion

		public void Ok()
		{
			ServiceResult = ServiceResult.Ok;
		}

		public void Cancel()
		{
			ServiceResult = ServiceResult.Cancel;
		}

		public void PlanNameSupplied(string planName)
		{
			PlanName = planName;
		}

		public void CreatorSupplied(string creator)
		{
			Creator = creator;
		}
	}
}