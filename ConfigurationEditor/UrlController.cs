using System;
using System.ComponentModel;
using System.Windows.Forms;
using SlickQA.DataCollector.Configuration;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	public sealed class UrlController : IDisposable
	{
		private readonly BindingList<string> _schemes;

		private SlickUrl _url;
		private readonly ComboBox _schemeComboBox;
		private readonly TextBox _hostTextBox;
		private readonly NumericUpDown _portNumericUpDown;
		private readonly TextBox _sitePathTextBox;
		private readonly Button _getProjectButton;
		private readonly Button _addProjectButton;

		public UrlController(IConfigurationView view)
		{
			_schemes = new BindingList<string> { Uri.UriSchemeHttp, Uri.UriSchemeHttps };

			_schemeComboBox = view.Scheme;
			_hostTextBox = view.Host;
			_portNumericUpDown = view.Port;
			_sitePathTextBox = view.SitePath;
			_getProjectButton = view.GetProject;
			_addProjectButton = view.AddProject;
		}

		public void SetConfig(object sender, ResetConfigHandlerArgs args)
		{
			ClearDataBindings();

			_url = args.Config.Url;

			_schemeComboBox.DataSource = _schemes;
			_schemeComboBox.DisplayMember = null;
			_schemeComboBox.ValueMember = null;
			_schemeComboBox.DataBindings.Add("SelectedItem", _url, "Scheme", false, DataSourceUpdateMode.OnPropertyChanged);

			_hostTextBox.DataBindings.Add("Text", _url, "Host", false, DataSourceUpdateMode.OnPropertyChanged);
			_portNumericUpDown.DataBindings.Add("Value", _url, "Port", false, DataSourceUpdateMode.OnPropertyChanged);
			_sitePathTextBox.DataBindings.Add("Text", _url, "SitePath", false, DataSourceUpdateMode.OnPropertyChanged);

			_getProjectButton.DataBindings.Add("Enabled", _url, "IsValid", false, DataSourceUpdateMode.OnPropertyChanged);
			_addProjectButton.DataBindings.Add("Enabled", _url, "IsValid", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		public void Dispose()
		{
			ClearDataBindings();
		}

		private void ClearDataBindings()
		{
			_schemeComboBox.DataSource = null;
			_schemeComboBox.DataBindings.Clear();

			_hostTextBox.DataBindings.Clear();
			_portNumericUpDown.DataBindings.Clear();
			_sitePathTextBox.DataBindings.Clear();
			_getProjectButton.DataBindings.Clear();
			_addProjectButton.DataBindings.Clear();
		}
	}
}