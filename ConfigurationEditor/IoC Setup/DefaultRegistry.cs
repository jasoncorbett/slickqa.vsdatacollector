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
using SlickQA.DataCollector.ConfigurationEditor.App.SelectResultDestination;
using SlickQA.DataCollector.ConfigurationEditor.App.StartBuildSearch;
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyBuildProvider;
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyExecutionNaming;
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyProjectInfo;
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyReleaseInfo;
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyTestPlanInfo;
using SlickQA.DataCollector.ConfigurationEditor.App.SupplyUrlInfo;
using SlickQA.DataCollector.ConfigurationEditor.AppController;
using SlickQA.DataCollector.ConfigurationEditor.Commands;
using SlickQA.DataCollector.ConfigurationEditor.Services;
using SlickQA.DataCollector.ConfigurationEditor.View;
using SlickQA.DataCollector.EventAggregator;
using SlickQA.DataCollector.Repositories;
using StructureMap.Configuration.DSL;

namespace SlickQA.DataCollector.ConfigurationEditor.IoC_Setup
{
	sealed class DefaultRegistry : Registry
	{
		public DefaultRegistry()
		{
			// Views
			For<IResultDestinationView>().Use<SelectResultDestinationControl>();
			For<IBuildSpecifierView>().Use<BuildSpecifierControl>();
			For<IChooseBuildProviderView>().Use<ChooseBuildProviderForm>();
			For<INewProjectView>().Use<NewProjectForm>();
			For<INewReleaseView>().Use<NewReleaseForm>();
			For<ISetUrlView>().Use<UrlSelector>();
			For<IExecutionNamingView>().Use<ExecutionNaming>();
			For<INewTestPlanView>().Use<NewTestPlanForm>();

			//Controllers
			For<IGetBuildProviderInfo>().Use<ChooseBuildProviderController>();
			For<IGetNewProjectInfo>().Use<NewProjectController>();
			For<IGetNewReleaseInfo>().Use<NewReleaseController>();
			For<IGetUrlInfo>().Use<UrlController>();
			For<IGetNewTestPlanInfo>().Use<NewTestPlanController>();


			//Services
			For<ICommand<AddNewProjectData>>().Use<AddNewProjectService>();
			For<ICommand<AddNewReleaseData>>().Use<AddNewReleaseService>();
			For<ICommand<RetrieveProjectsData>>().Use<RetrieveProjectsService>();
			For<ICommand<SelectBuildProviderData>>().Use<SelectBuildProviderService>();
			For<ICommand<GetAssemblyInfoData>>().Use<GetAssemblyInfoService>();
			For<ICommand<RetrieveTestPlansData>>().Use<RetrieveTestPlansService>();
			For<ICommand<AddNewTestPlanData>>().Use<AddNewTestPlanService>();
			For<ICommand<RetrieveReleasesData>>().Use<RetrieveReleasesService>();

			//Repositories
			For<IUrlRepository>().Singleton().Use<StaticUrlRepository>();
			For<IProjectRepository>().Singleton().Use<WebProjectRepository>();
			For<IReleaseRepository>().Singleton().Use<WebReleaseRepository>();
			For<ITestPlanRepository>().Singleton().Use<WebTestPlanRepository>();
			
			// Application Level Config
			For<IApplicationController>().Use<ApplicationController>();
			For<IEventPublisher>().Singleton().Use<EventPublisher>();

			RegisterInterceptor(new EventInterceptor());
		}
	}
}
