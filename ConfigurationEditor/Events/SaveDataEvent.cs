using Microsoft.VisualStudio.TestTools.Execution;

namespace SlickQA.DataCollector.ConfigurationEditor.Events
{
	public class SaveDataEvent
	{
		public SaveDataEvent(DataCollectorSettings settings)
		{
			Settings = settings;
		}

		public DataCollectorSettings Settings { get; set; }
	}
}