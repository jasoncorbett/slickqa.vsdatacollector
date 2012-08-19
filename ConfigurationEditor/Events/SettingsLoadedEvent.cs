using Microsoft.VisualStudio.TestTools.Execution;

namespace SlickQA.DataCollector.ConfigurationEditor.Events
{
	public class SettingsLoadedEvent
	{
		public SettingsLoadedEvent(DataCollectorSettings settings)
		{
			Settings = settings;
		}

		public DataCollectorSettings Settings { get; set; }
	}
}