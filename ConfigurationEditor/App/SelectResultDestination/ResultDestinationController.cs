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
using System.Xml;
using SlickQA.DataCollector.ConfigurationEditor.AppController;
using SlickQA.DataCollector.ConfigurationEditor.Commands;
using SlickQA.DataCollector.ConfigurationEditor.Events;
using SlickQA.DataCollector.EventAggregator;
using SlickQA.DataCollector.Models;
using SlickQA.DataCollector.Repositories;

namespace SlickQA.DataCollector.ConfigurationEditor.App.SelectResultDestination
{
	public class ResultDestinationController :
		IEventHandler<ProjectsLoadedEvent>,
		IEventHandler<ProjectAddedEvent>,
		IEventHandler<ProjectSelectedEvent>, 
		IEventHandler<ReleaseAddedEvent>,
		IEventHandler<SettingsLoadedEvent>,
		IEventHandler<SaveDataEvent>
	{
		public ResultDestinationController(IResultDestinationView view, IApplicationController appController, IProjectRepository projectRepository, IReleaseRepository releaseRepository)
		{
			View = view;
			View.Controller = this;
			AppController = appController;
			ProjectRepository = projectRepository;
			ReleaseRepository = releaseRepository;
			CurrentProject = new ProjectInfo();
			CurrentRelease = new ReleaseInfo();
		}

		private IResultDestinationView View { get; set; }
		private IApplicationController AppController { get; set; }
		private IProjectRepository ProjectRepository { get; set; }
		private IReleaseRepository ReleaseRepository { get; set; }
		private ProjectInfo DefaultProject { get; set; }
		private ProjectInfo CurrentProject { get; set; }
		private ReleaseInfo DefaultRelease { get; set; }
		private ReleaseInfo CurrentRelease { get; set; }

		#region IEventHandler<ProjectAddedEvent> Members

		public void Handle(ProjectAddedEvent eventData)
		{
			IEnumerable<ProjectInfo> projectList = ProjectRepository.GetProjects();
			View.LoadProjectList(projectList);

			
			View.SelectProject(eventData.Project);
		}

		#endregion

		#region IEventHandler<ProjectSelectedEvent> Members

		public void Handle(ProjectSelectedEvent eventData)
		{
			AppController.Execute(new RetrieveReleasesData(eventData.Project.Id));

			View.LoadReleaseList(ReleaseRepository.GetReleases(eventData.Project.Id));
			View.EnableAddReleaseButton();
		}

		#endregion

		#region IEventHandler<ProjectsLoadedEvent> Members

		public void Handle(ProjectsLoadedEvent eventData)
		{
			View.LoadProjectList(ProjectRepository.GetProjects());
			View.EnableAddProjectButton();
		}

		#endregion

		#region IEventHandler<ReleaseAddedEvent> Members

		public void Handle(ReleaseAddedEvent eventData)
		{
			View.LoadReleaseList(ReleaseRepository.GetReleases(CurrentProject.Id));

			View.SelectRelease(eventData.Release);
		}

		#endregion

		#region IEventHandler<SaveDataEvent> Members

		public void Handle(SaveDataEvent eventData)
		{
			eventData.TestInfo.Project = CurrentProject;
			eventData.TestInfo.Release = CurrentRelease;
		}

		#endregion

		#region IEventHandler<SettingsLoadedEvent> Members

		public void Handle(SettingsLoadedEvent eventData)
		{
			CurrentProject = ProjectInfo.FromXml(eventData.Settings.Configuration);
			DefaultProject = ProjectInfo.FromXml(eventData.Settings.DefaultConfiguration);

			ReleaseInfo release = ReleaseInfo.FromXml(eventData.Settings.Configuration);
			DefaultRelease = ReleaseInfo.FromXml(eventData.Settings.DefaultConfiguration);

			View.SelectProject(CurrentProject);
			View.SelectRelease(release);
		}

		#endregion

		public void ProjectSupplied(ProjectInfo project)
		{
			CurrentProject = project;
			AppController.Raise(new ProjectSelectedEvent(CurrentProject));
		}

		public void ReleaseSupplied(ReleaseInfo release)
		{
			CurrentRelease = release;
		}

		public void AddProject()
		{
			AppController.Execute(new AddNewProjectData());
		}

		public void AddRelease(ProjectInfo currentProject)
		{
			AppController.Execute(new AddNewReleaseData(currentProject.Id));
		}
	}
}