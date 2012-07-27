using System;
using System.Collections.Generic;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.Configuration
{
	public interface IConfigurationView
	{
		void PopulateProjects(IEnumerable<Project> projects);
		void GetProjects(object sender, EventArgs e);
	}
}