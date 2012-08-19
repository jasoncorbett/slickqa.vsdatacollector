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

using System.Linq;
using SlickQA.DataCollector.Models;

namespace SlickQA.DataCollector.ConfigurationEditor.App.SupplyProjectInfo
{
	public class NewProjectController : IGetNewProjectInfo
	{
		public NewProjectController(INewProjectView view)
		{
			View = view;
			View.Controller = this;
		}

		private ServiceResult ServiceResult { get; set; }
		private string ProjectName { get; set; }
		private string ProjectDescription { get; set; }
		private string ReleaseName { get; set; }
		private string ProjectTags { get; set; }
		private INewProjectView View { get; set; }

		#region IGetNewProjectInfo Members

		public Result<ProjectInfo> Get()
		{
			View.Run();
			ProjectInfo project = null;
			if (ServiceResult == ServiceResult.Ok)
			{
				project = new ProjectInfo(string.Empty, ProjectName, ProjectDescription, ReleaseName, ProjectTags.Split(',').ToList());
			}
			return new Result<ProjectInfo>(ServiceResult, project);
		}

		#endregion

		public void ProjectNameSupplied(string projectName)
		{
			ProjectName = projectName;
		}

		public void ProjectDescriptionSupplied(string projectDescription)
		{
			ProjectDescription = projectDescription;
		}

		public void ReleaseNameSupplied(string releaseName)
		{
			ReleaseName = releaseName;
		}

		public void ProjectTagsSupplied(string projectTags)
		{
			ProjectTags = projectTags;
		}

		public void Create()
		{
			ServiceResult = ServiceResult.Ok;
		}

		public void Cancel()
		{
			ServiceResult = ServiceResult.Cancel;
		}
	}
}
