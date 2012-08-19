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
using SlickQA.DataCollector.Models;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.ConfigurationEditor.App.SelectResultDestination
{
	public interface IResultDestinationView
	{
		ResultDestinationController Controller { set; }
		void LoadProjectList(IEnumerable<ProjectInfo> projectList);
		void SelectProject(ProjectInfo project);
		void LoadReleaseList(IEnumerable<ReleaseInfo> releaseList);
		void SelectRelease(ReleaseInfo release);
		void EnableAddReleaseButton();
		void EnableAddProjectButton();
	}
}