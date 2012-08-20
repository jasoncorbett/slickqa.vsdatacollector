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

using System.IO;
using System.Reflection;
using SlickQA.DataCollector.ConfigurationEditor.AppController;
using SlickQA.DataCollector.ConfigurationEditor.Commands;
using SlickQA.DataCollector.ConfigurationEditor.Events;
using SlickQA.DataCollector.EventAggregator;
using SlickQA.DataCollector.Models;

namespace SlickQA.DataCollector.ConfigurationEditor.App.SupplyBuildProvider
{
	public class ChooseBuildProviderController : IGetBuildProviderInfo, IEventHandler<AssemblyParsedEvent>
	{
		public ChooseBuildProviderController(IChooseBuildProviderView view, IApplicationController appController)
		{
			View = view;
			View.Controller = this;
			AppController = appController;
		}

		private string AssemblyPath { get; set; }
		private MethodInfo Method { get; set; }
		private ServiceResult ServiceResult { get; set; }
		private IChooseBuildProviderView View { get; set; }
		private IApplicationController AppController { get; set; }

		#region IGetBuildProviderInfo Members

		public Result<BuildProviderInfo> Get(BuildProviderInfo provider)
		{
			View.SetFilePath(provider);
			FilePathSupplied(Path.Combine(provider.Directory, provider.AssemblyName));
			if (provider.Method != null)
			{
				View.Select(provider.Method);
			}
			View.Run();
			BuildProviderInfo info = null;
			if (ServiceResult == ServiceResult.Ok)
			{
				string fileName = AssemblyPath;
				string assemblyName = Path.GetFileName(fileName);
				string directory = Path.GetDirectoryName(fileName);

				info = new BuildProviderInfo(assemblyName, directory, Method);
			}
			return new Result<BuildProviderInfo>(ServiceResult, info);
		}

		#endregion

		public void Browse()
		{
			View.ShowOpenFileDialog();
		}

		public void Ok()
		{
			ServiceResult = ServiceResult.Ok;
		}

		public void Cancel()
		{
			ServiceResult = ServiceResult.Cancel;
		}

		public void MethodSelected(MethodInfo method)
		{
			Method = method;
		}

		public void FilePathSupplied(string filePath)
		{
			AssemblyPath = filePath;
			AppController.Execute(new GetAssemblyInfoData(filePath));
		}

		public void Handle(AssemblyParsedEvent eventData)
		{
			View.PopulateTree(eventData.CandidateTypes);
		}
	}
}
