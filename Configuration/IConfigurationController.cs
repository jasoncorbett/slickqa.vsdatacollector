namespace SlickQA.DataCollector.Configuration
{
	public interface IConfigurationController
	{
		void GetProjectsClicked();
		IConfigurationView View { set; }
	}
}