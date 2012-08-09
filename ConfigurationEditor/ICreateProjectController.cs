using System;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	internal interface ICreateProjectController : IDisposable
	{
		void Show();
		bool IsValid { get; }
		event NewProjectHandler NewProject;
		event NewReleaseHandler NewRelease;
	}
}