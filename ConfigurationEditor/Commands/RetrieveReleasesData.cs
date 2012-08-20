namespace SlickQA.DataCollector.ConfigurationEditor.Commands
{
	public class RetrieveReleasesData
	{
		public RetrieveReleasesData(string projectId)
		{
			ProjectId = projectId;
		}

		public string ProjectId { get; set; }
	}
}