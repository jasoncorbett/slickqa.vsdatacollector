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
using SlickQA.DataCollector.Repositories;

namespace SlickQA.DataCollector.ConfigurationEditor.Services
{
	public class RetrieveReleasesService : ICommand<RetrieveReleasesData>
	{
		public RetrieveReleasesService(IApplicationController appController, IReleaseRepository releaseRepository)
		{
			AppController = appController;
			ReleaseRepository = releaseRepository;
		}

		private IApplicationController AppController { get; set; }
		private IReleaseRepository ReleaseRepository { get; set; }

		#region ICommand<RetrieveReleasesData> Members

		public void Execute(RetrieveReleasesData commandData)
		{
			ReleaseRepository.Load(commandData.ProjectId);
			AppController.Raise(new ReleasesLoadedEvent());
		}

		#endregion
	}
}