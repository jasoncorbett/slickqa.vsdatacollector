using System;
using System.Windows.Forms;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	internal interface ICreateReleaseView : IDisposable
	{
		DialogResult ShowDialog();
		TextBox ReleaseName { get; }
	}
}