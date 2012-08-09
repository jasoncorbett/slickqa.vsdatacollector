using System;
using System.Windows.Forms;
using SlickQA.DataCollector.Configuration;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	public sealed class ScreenshotController : IDisposable 
	{
		private readonly CheckBox _preTestCheckBox;
		private readonly CheckBox _postTestCheckBox;
		private readonly CheckBox _failureCheckBox;
		private ScreenShotSettings _settings;

		public ScreenshotController(IConfigurationView view)
		{
			_preTestCheckBox = view.PreTestScreenshot;
			_postTestCheckBox = view.PostTestScreenshot;
			_failureCheckBox = view.FailureScreenshot;
		}

		public void SetConfig(object sender, ResetConfigHandlerArgs args)
		{
			ClearBindings();

			_settings = args.Config.ScreenshotSettings;
			_preTestCheckBox.DataBindings.Add("Checked", _settings, "PreTest", false, DataSourceUpdateMode.OnPropertyChanged);
			_postTestCheckBox.DataBindings.Add("Checked", _settings, "PostTest", false, DataSourceUpdateMode.OnPropertyChanged);
			_failureCheckBox.DataBindings.Add("Checked", _settings, "OnFailure", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		public void Dispose()
		{
			ClearBindings();
		}

		private void ClearBindings()
		{
			_preTestCheckBox.DataBindings.Clear();
			_postTestCheckBox.DataBindings.Clear();
			_failureCheckBox.DataBindings.Clear();
		}
	}
}