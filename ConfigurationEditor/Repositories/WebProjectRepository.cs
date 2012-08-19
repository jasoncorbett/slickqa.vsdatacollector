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
using SlickQA.DataCollector.Models;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.ConfigurationEditor.Repositories
{
	internal class WebProjectRepository : IProjectRepository
	{
		private bool _refreshProjects;
		private List<ProjectInfo> Projects { get; set; }

		#region IProjectRepository Members

		public string AddProject(ProjectInfo info)
		{
			var project = new Project
			              {
			              	Name = info.Name,
							Description = info.Description,
							Tags = info.Tags
			              };
			project.Post();

			_refreshProjects = true;
			return project.Id;
		}

		public IList<ProjectInfo> GetProjects()
		{
			if (_refreshProjects)
			{
				Load();
			}
			return Projects;
		}

		public void Load()
		{
			Projects = ConvertToProjectInfo(Project.GetList());
			_refreshProjects = false;
		}

		#endregion

		private static List<ProjectInfo> ConvertToProjectInfo(IEnumerable<Project> projects)
		{
			return projects.Select(p => new ProjectInfo(p.Id, p.Name, p.Description, string.Empty, p.Tags)).ToList();
		}
	}
}