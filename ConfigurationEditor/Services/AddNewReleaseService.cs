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
	class AddNewReleaseService : ICommand<AddNewReleaseData>
	{
		public AddNewReleaseService(IGetNewReleaseInfo getNewReleaseInfo, IProjectRepository projectRepository, IApplicationController appController, IReleaseRepository releaseRepository)
		{
			GetNewReleaseInfo = getNewReleaseInfo;
			ProjectRepository = projectRepository;
			AppController = appController;
			ReleaseRepository = releaseRepository;
		}

		private IReleaseRepository ReleaseRepository { get; set; }
		private IGetNewReleaseInfo GetNewReleaseInfo { get; set; }
		private IProjectRepository ProjectRepository { get; set; }
		private IApplicationController AppController { get; set; }

		#region ICommand<AddNewReleaseData> Members

		public void Execute(AddNewReleaseData commandData)
		{
			Result<ReleaseInfo> result = GetNewReleaseInfo.Get(commandData.ProjectId);
			if (result.ServiceResult != ServiceResult.Ok)
			{
				return;
			}

			ReleaseInfo info = result.Data;

			string releaseId = ReleaseRepository.AddRelease(info);
			info.Id = releaseId;

			AppController.Raise(new ReleaseAddedEvent(info));
		}

		#endregion
	}
}
