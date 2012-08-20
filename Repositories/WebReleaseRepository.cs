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

namespace SlickQA.DataCollector.Repositories
{
	public class WebReleaseRepository : IReleaseRepository
	{
		public WebReleaseRepository()
		{
			RefreshReleases = new Dictionary<string, bool>();
			Releases = new Dictionary<string, List<ReleaseInfo>>();
		}

		private Dictionary<string, bool> RefreshReleases { get; set; }
		private Dictionary<string, List<ReleaseInfo>> Releases { get; set; }

		#region IReleaseRepository Members

		public IEnumerable<ReleaseInfo> GetReleases(string projectId)
		{
			if (RefreshReleases[projectId])
			{
				Load(projectId);
			}
			return Releases[projectId];
		}

		public void MakeDefault(ReleaseInfo releaseInfo)
		{
			var release = new Release {Id = releaseInfo.Id, ProjectId = releaseInfo.ProjectId};
			release.Get();
			release.SetAsDefault();
		}

		public string AddRelease(ReleaseInfo info)
		{
			var release = new Release
			{
				Name = info.Name,
				ProjectId = info.ProjectId
			};
			release.Post();
			RefreshReleases[info.ProjectId] = true;
			return release.Id;
		}

		#endregion

		public void Load(string projectId)
		{
			string listUrl = string.Format("projects/{0}/releases", projectId);
			if (Releases.ContainsKey(projectId))
			{
				Releases[projectId] = ConvertToReleaseInfo(Release.GetList(listUrl), projectId);
				RefreshReleases[projectId] = false;
			}
			else
			{
				Releases.Add(projectId, ConvertToReleaseInfo(Release.GetList(listUrl), projectId));
				RefreshReleases.Add(projectId, false);
			}
		}

		private static List<ReleaseInfo> ConvertToReleaseInfo(IEnumerable<Release> releases, string projectId)
		{
			return releases.Select(release => new ReleaseInfo(release.Id, release.Name, projectId)).ToList();
		}
	}
}