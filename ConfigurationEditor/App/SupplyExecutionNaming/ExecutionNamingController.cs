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
using SlickQA.DataCollector.ConfigurationEditor.AppController;
using SlickQA.DataCollector.ConfigurationEditor.Commands;
using SlickQA.DataCollector.ConfigurationEditor.Events;
using SlickQA.DataCollector.EventAggregator;
using SlickQA.DataCollector.Models;
using SlickQA.DataCollector.Repositories;

namespace SlickQA.DataCollector.ConfigurationEditor.App.SupplyExecutionNaming
{
	public class ExecutionNamingController :
		IEventHandler<ProjectSelectedEvent>,
		IEventHandler<TestPlansLoadedEvent>,
		IEventHandler<TestPlanAddedEvent>,
		IEventHandler<SettingsLoadedEvent>,
		IEventHandler<ResetEvent>,
		IEventHandler<SaveDataEvent>
	{
		private IApplicationController AppController { get; set; }
		private ITestPlanRepository Repository { get; set; }
		private ProjectInfo Project { get; set; }
		private IExecutionNamingView View { get; set; }
		private TestPlanInfo CurrentTestPlan { get; set; }
		private TestPlanInfo DefaultTestPlan { get; set; }

		public ExecutionNamingController(IExecutionNamingView view, IApplicationController appController, ITestPlanRepository repository)
		{
			View = view;
			View.Controller = this;
			AppController = appController;
			Repository = repository;
			CurrentTestPlan = new TestPlanInfo();
			DefaultTestPlan = new TestPlanInfo();
		}

		public void TestPlanSupplied(TestPlanInfo testPlan)
		{
			CurrentTestPlan = testPlan;
		}

		public void AddTestPlan()
		{
			AppController.Execute(new AddNewTestPlanData(Project.Id));
		}

		public void Handle(ProjectSelectedEvent eventData)
		{
			Project = eventData.Project;
			AppController.Execute(new RetrieveTestPlansData(Project.Id));
		}

		public void Handle(TestPlansLoadedEvent eventData)
		{
			var testPlans = Repository.GetPlans(eventData.ProjectId).ToList();
			
			View.DisplayPlans(testPlans);
			View.EnableAddPlanButton();
			View.EnablePlanComboBox(testPlans.Count != 0);
			
			if (testPlans.Count == 0)
			{
				AppController.Raise(new TestPlanInvalidatedEvent());
			}
			else
			{
				AppController.Raise(new TestPlanValidatedEvent());
			}
		}

		public void Handle(TestPlanAddedEvent eventData)
		{
			List<TestPlanInfo> testPlans = Repository.GetPlans(Project.Id).ToList();
			View.DisplayPlans(testPlans);

			View.SelectPlan(eventData.TestPlan);
		}

		public void Handle(SettingsLoadedEvent eventData)
		{
			CurrentTestPlan = TestPlanInfo.FromXml(eventData.Settings.Configuration);
			DefaultTestPlan = TestPlanInfo.FromXml(eventData.Settings.DefaultConfiguration);
		}

		public void Handle(ResetEvent eventData)
		{
			CurrentTestPlan = new TestPlanInfo(DefaultTestPlan);

			View.SelectPlan(CurrentTestPlan);
		}

		public void Handle(SaveDataEvent eventData)
		{
			var config = eventData.Settings.Configuration;

			config.UpdateTagWithNewValue(TestPlanInfo.TAG_NAME, CurrentTestPlan.ToXmlNode());
		}
	}
}
