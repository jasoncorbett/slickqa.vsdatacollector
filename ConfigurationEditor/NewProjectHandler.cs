using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	internal sealed class NewProjectHandlerArgs
	{
		public NewProjectHandlerArgs(Project project)
		{
			Project = project;
		}

		public Project Project { get; set; }
	}

	internal delegate void NewProjectHandler(object sender, NewProjectHandlerArgs args);
}