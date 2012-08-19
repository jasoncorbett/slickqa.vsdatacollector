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
using SlickQA.DataCollector.Models;
using SlickQA.DataCollector.Repositories;

namespace SlickQA.DataCollector.ConfigurationEditor.Services
{
	internal class AddNewProjectService : ICommand<AddNewProjectData>
	{
		public AddNewProjectService(IGetNewProjectInfo getNewProjectInfo, IProjectRepository projectRepository, IReleaseRepository releaseRepository, IApplicationController appController)
		{
			GetNewProjectInfo = getNewProjectInfo;
			ProjectRepository = projectRepository;
			ReleaseRepository = releaseRepository;
			AppController = appController;
		}

		private IGetNewProjectInfo GetNewProjectInfo { get; set; }
		private IProjectRepository ProjectRepository { get; set; }
		private IReleaseRepository ReleaseRepository { get; set; }
		private IApplicationController AppController { get; set; }


		public void Execute(AddNewProjectData commandData)
		{
			Result<ProjectInfo> result = GetNewProjectInfo.Get();
			if (result.ServiceResult != ServiceResult.Ok)
			{
				return;
			}

			ProjectInfo info = result.Data;

			string projectId = ProjectRepository.AddProject(info);
			info.Id = projectId;

			var releaseInfo = new ReleaseInfo(string.Empty, info.ReleaseName, projectId);

			string releaseId = ReleaseRepository.AddRelease(releaseInfo);
			releaseInfo.Id = releaseId;

			AppController.Raise(new ProjectAddedEvent(info));
			AppController.Raise(new ReleaseAddedEvent(releaseInfo));
		}
	}
}