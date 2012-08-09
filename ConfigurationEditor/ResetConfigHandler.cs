using SlickQA.DataCollector.Configuration;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	public delegate void ResetConfigHandler(object sender, ResetConfigHandlerArgs args);

	public sealed class ResetConfigHandlerArgs
	{
		public ResetConfigHandlerArgs(Config config)
		{
			Config = config;
		}

		public Config Config { get; private set; }
	}
}