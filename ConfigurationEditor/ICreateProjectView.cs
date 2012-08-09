using System;
using System.Windows.Forms;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	public interface ICreateProjectView : IDisposable
	{
		DialogResult ShowDialog();
		TextBox ProjectName { get; }
		TextBox ReleaseName { get; }
		TextBox ProjectDescription { get; }
		ErrorProvider ErrorProvider { get; }
		Button CreateButton { get; }
	}
}