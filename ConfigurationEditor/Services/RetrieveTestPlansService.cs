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

using SlickQA.DataCollector.ConfigurationEditor.AppController;
using SlickQA.DataCollector.ConfigurationEditor.Commands;
using SlickQA.DataCollector.ConfigurationEditor.Events;
using SlickQA.DataCollector.ConfigurationEditor.Repositories;

namespace SlickQA.DataCollector.ConfigurationEditor.Services
{
	class RetrieveTestPlansService : ICommand<RetrieveTestPlansData>
	{
		public RetrieveTestPlansService(IApplicationController appController, ITestPlanRepository repository)
		{
			AppController = appController;
			Repository = repository;
		}

		public ITestPlanRepository Repository { get; set; }
		public IApplicationController AppController { get; set; }

		#region ICommand<RetrieveTestPlansData> Members

		public void Execute(RetrieveTestPlansData commandData)
		{
			Repository.Load(commandData.ProjectId);

			AppController.Raise(new TestPlansLoadedEvent(commandData.ProjectId));
		}

		#endregion
	}
}
