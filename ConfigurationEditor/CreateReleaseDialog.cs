using System.Windows.Forms;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	public partial class _createReleaseDialog : Form, ICreateReleaseView
	{
		public _createReleaseDialog()
		{
			InitializeComponent();
		}

		public TextBox ReleaseName
		{
			get { return _release; }
		}
	}
}
