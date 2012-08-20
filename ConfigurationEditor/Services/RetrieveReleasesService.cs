using SlickQA.DataCollector.ConfigurationEditor.AppController;
using SlickQA.DataCollector.ConfigurationEditor.Commands;
using SlickQA.DataCollector.ConfigurationEditor.Events;
using SlickQA.DataCollector.Repositories;

namespace SlickQA.DataCollector.ConfigurationEditor.Services
{
	internal class RetrieveReleasesService : ICommand<RetrieveReleasesData>
	{
		private IApplicationController AppController { get; set; }
		private IReleaseRepository ReleaseRepository { get; set; }

		public RetrieveReleasesService(IApplicationController appController, IReleaseRepository releaseRepository)
		{
			AppController = appController;
			ReleaseRepository = releaseRepository;
		}

		public void Execute(RetrieveReleasesData commandData)
		{
			ReleaseRepository.Load(commandData.ProjectId);
			AppController.Raise(new ReleasesLoadedEvent());
		}
	}
}