using System.Collections.Generic;
using SlickQA.DataCollector.Models;

namespace SlickQA.DataCollector.ConfigurationEditor.Repositories
{
	public interface IReleaseRepository
	{
		string AddRelease(ReleaseInfo info);
		IEnumerable<ReleaseInfo> GetReleases(string projectId);
	}
}