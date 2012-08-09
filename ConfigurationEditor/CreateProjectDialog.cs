using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	public partial class CreateProjectDialog : Form, ICreateProjectView
	{
		public CreateProjectDialog()
		{
			InitializeComponent();
		}

		private void ProjectNameValidating(object sender, CancelEventArgs e)
		{
			if (String.IsNullOrWhiteSpace(_projectName.Text))
			{
				_errorProvider.SetError(_projectName, "Name is required to create a project.");
			}
			else
			{
				_errorProvider.SetError(_projectName, String.Empty);
			}
		}

		private void ReleaseNameValidating(object sender, CancelEventArgs e)
		{
			if (String.IsNullOrWhiteSpace(_releaseName.Text))
			{
				_errorProvider.SetError(_releaseName, "Release is required to create a new project.");
			}
			else
			{
				_errorProvider.SetError(_releaseName, String.Empty);
			}
		}

		public TextBox ProjectName
		{
			get { return _projectName; }
		}

		public TextBox ReleaseName
		{
			get { return _releaseName; }
		}

		public TextBox ProjectDescription
		{
			get { return _projectDescription; }
		}

		public ErrorProvider ErrorProvider
		{
			get { return _errorProvider; }
		}

		public Button CreateButton
		{
			get { return _createButton; }
		}
	}
}
