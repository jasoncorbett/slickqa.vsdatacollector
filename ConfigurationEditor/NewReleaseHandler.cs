using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	internal class NewReleaseHandlerArgs
	{
		public NewReleaseHandlerArgs(Release release)
		{
			Release = release;
		}

		public Release Release { get; private set; }
	}

	internal delegate void NewReleaseHandler(object sender, NewReleaseHandlerArgs args);
}