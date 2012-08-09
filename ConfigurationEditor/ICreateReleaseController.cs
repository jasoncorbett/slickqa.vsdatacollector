using System;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	internal interface ICreateReleaseController : IDisposable 
	{
		void Show();
		bool IsValid { get; }
		event NewReleaseHandler NewRelease;
	}
}