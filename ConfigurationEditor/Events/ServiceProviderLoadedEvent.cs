using System;

namespace SlickQA.DataCollector.ConfigurationEditor.Events
{
	public class ServiceProviderLoadedEvent
	{
		public ServiceProviderLoadedEvent(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
		}

		public IServiceProvider ServiceProvider { get; set; }
	}
}