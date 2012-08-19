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

using SlickQA.DataCollector.ConfigurationEditor.App;
using SlickQA.DataCollector.ConfigurationEditor.AppController;
using SlickQA.DataCollector.ConfigurationEditor.Commands;
using SlickQA.DataCollector.ConfigurationEditor.Events;
using SlickQA.DataCollector.ConfigurationEditor.Repositories;
using SlickQA.DataCollector.Models;

namespace SlickQA.DataCollector.ConfigurationEditor.Services
{
	class AddNewTestPlanService : ICommand<AddNewTestPlanData>
	{
		public AddNewTestPlanService(IApplicationController appController, IGetNewTestPlanInfo getNewTestPlanInfo, ITestPlanRepository repository)
		{
			AppController = appController;
			GetNewTestPlanInfo = getNewTestPlanInfo;
			TestPlanRepository = repository;
		}

		private IApplicationController AppController { get; set; }
		private IGetNewTestPlanInfo GetNewTestPlanInfo { get; set; }
		private ITestPlanRepository TestPlanRepository { get; set; }

		#region ICommand<AddNewTestPlanData> Members

		public void Execute(AddNewTestPlanData commandData)
		{
			Result<TestPlanInfo> result = GetNewTestPlanInfo.Get();

			if (result.ServiceResult != ServiceResult.Ok)
			{
				return;
			}
			TestPlanInfo info = result.Data;
			info.ProjectId = commandData.ProjectId;

			string planId = TestPlanRepository.AddTestPlan(info);
			info.Id = planId;

			AppController.Raise(new TestPlanAddedEvent(info));
		}

		#endregion
	}
}
