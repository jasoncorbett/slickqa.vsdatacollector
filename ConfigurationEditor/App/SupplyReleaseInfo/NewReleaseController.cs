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

using SlickQA.DataCollector.Models;

namespace SlickQA.DataCollector.ConfigurationEditor.App.SupplyReleaseInfo
{
	public class NewReleaseController : IGetNewReleaseInfo
	{
		public NewReleaseController(INewReleaseView view)
		{
			View = view;
			View.Controller = this;
		}

		private INewReleaseView View { get; set; }
		private string ReleaseName { get; set; }
		private ServiceResult ServiceResult { get; set; }

		#region IGetNewReleaseInfo Members

		public Result<ReleaseInfo> Get(string projectId)
		{
			View.Run();
			ReleaseInfo release = null;
			if (ServiceResult == ServiceResult.Ok)
			{
				release = new ReleaseInfo(string.Empty, ReleaseName, projectId);
			}
			return new Result<ReleaseInfo>(ServiceResult, release);
		}

		#endregion

		public void ReleaseNameSupplied(string releaseName)
		{
			ReleaseName = releaseName;
			if (string.IsNullOrWhiteSpace(ReleaseName))
			{
				View.SetReleaseNameError();
			}
			else
			{
				View.ClearReleaseNameError();
			}
			View.UpdateOkEnabledState();
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
