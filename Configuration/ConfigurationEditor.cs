using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.Execution;

namespace SlickQA.DataCollector.Configuration
{
	[DataCollectorConfigurationEditorTypeUri("configurationeditor://slickqa/SlickDataCollectorConfigurationEditor/0.0.1")]
	public partial class ConfigurationEditor : UserControl, IDataCollectorConfigurationEditor
	{
		private DataCollectorSettings _collectorSettings;

		public ConfigurationEditor()
		{
			InitializeComponent();
		}

		private IServiceProvider ServiceProvider { get; set; }

		#region IDataCollectorConfigurationEditor Members

		public void Initialize(IServiceProvider serviceProvider, DataCollectorSettings settings)
		{
			ServiceProvider = serviceProvider;
			_collectorSettings = settings;

			UpdateUI();
		}

		public void ResetToAgentDefaults()
		{
			throw new NotImplementedException();
		}

		public DataCollectorSettings SaveData()
		{
			throw new NotImplementedException();
		}

		public bool VerifyData()
		{
			throw new NotImplementedException();
		}

		#endregion

		private void UpdateUI()
		{
		}
	}
}