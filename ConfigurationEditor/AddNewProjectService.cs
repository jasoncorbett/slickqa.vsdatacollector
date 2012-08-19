using ClassLibrary1.App;
using ClassLibrary1.AppController;
using ClassLibrary1.Commands;
using ClassLibrary1.Events;
using ClassLibrary1.Repositories;
using ClassLibrary3;

namespace ClassLibrary1.Services
{
	internal class AddNewProjectService : ICommand<AddNewProjectData>
	{
		private IGetNewProjectInfo GetNewProjectInfo { get; set; }
		private IProjectRepository ProjectRepository { get; set; }
		private IApplicationController AppController { get; set; }

		public AddNewProjectService(IGetNewProjectInfo getNewProjectInfo, IProjectRepository projectRepository, IApplicationController appController)
		{
			GetNewProjectInfo = getNewProjectInfo;
			ProjectRepository = projectRepository;
			AppController = appController;
		}

		public void Execute(AddNewProjectData commandData)
		{
			Result<ProjectInfo> result = GetNewProjectInfo.Get();
			if (result.ServiceResult != ServiceResult.Ok)
			{
				return;
			}

			ProjectInfo info = result.Data;

			string projectId = ProjectRepository.AddProject(info);
			string releaseId = ProjectRepository.AddRelease(new ReleaseInfo {ProjectId = projectId, Name = info.ReleaseName});

			AppController.Raise(new ProjectAddedEvent(projectId));
			AppController.Raise(new ReleaseAddedEvent(releaseId));
		}
	}
}