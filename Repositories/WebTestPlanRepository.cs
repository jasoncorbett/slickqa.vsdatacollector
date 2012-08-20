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

using System.Collections.Generic;
using System.Linq;
using SlickQA.DataCollector.Models;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.Repositories
{
	public class WebTestPlanRepository : ITestPlanRepository
	{
		public WebTestPlanRepository()
		{
			RefreshTestPlans = new Dictionary<string, bool>();
			TestPlans = new Dictionary<string, List<TestPlanInfo>>();
		}

		private Dictionary<string, bool> RefreshTestPlans { get; set; }
		private Dictionary<string, List<TestPlanInfo>> TestPlans { get; set; }

		#region ITestPlanRepository Members

		public string AddTestPlan(TestPlanInfo info)
		{
			var project = new Project {Id = info.ProjectId};
			project.Get();

			string createdBy = "Visual Studio Slick Data Collector";
			if (string.IsNullOrWhiteSpace(info.CreatedBy))
			{
				createdBy = info.CreatedBy;
			}

			var testPlan = new TestPlan
			               {
			               	Name = info.Name,
							ProjectReference = project,
							CreatedBy = createdBy,
			               };
			testPlan.Post();

			RefreshTestPlans[info.ProjectId] = true;
			return testPlan.Id;
		}

		IEnumerable<TestPlanInfo> ITestPlanRepository.GetPlans(string projectId)
		{
			if (RefreshTestPlans[projectId])
			{
				Load(projectId);
			}
			return TestPlans[projectId];
		}

		public void Load(string projectId)
		{
			string listUrl = string.Format("testplans?projectid={0}", projectId);
			if (TestPlans.ContainsKey(projectId))
			{
				TestPlans[projectId] = ConvertToTestPlanInfo(TestPlan.GetList(listUrl));
				RefreshTestPlans[projectId] = false;
			}
			else
			{
				TestPlans.Add(projectId, ConvertToTestPlanInfo(TestPlan.GetList(listUrl)));
				RefreshTestPlans.Add(projectId, false);
			}
		}

		#endregion

		private List<TestPlanInfo> ConvertToTestPlanInfo(List<TestPlan> testPlans)
		{
			return testPlans.Select(t => new TestPlanInfo(t.Id, t.Name, t.ProjectReference.Id, t.CreatedBy)).ToList();
		}
	}
}