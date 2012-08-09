using System;
using System.ComponentModel;
using System.Windows.Forms;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	sealed class CreateProjectController : ICreateProjectController, INotifyPropertyChanged
	{
		private readonly ICreateProjectView _view;
		private readonly Project _project;
		private readonly Release _release;

		public CreateProjectController()
			:this(new CreateProjectDialog())
		{
		}

		public CreateProjectController(ICreateProjectView view)
		{
			_view = view;
			
			_project = new Project();
			_release = new Release();
		}

		public event NewReleaseHandler NewRelease;
		public event NewProjectHandler NewProject;

		public String ProjectName
		{
			get { return _project.Name; }
			set
			{
				_project.Name = value;
				NotifyPropertyChanged("ProjectName");
			}
		}

		public String ProjectDescription
		{
			get { return _project.Description; }
			set
			{
				_project.Description = value;
				NotifyPropertyChanged("ProjectDescription");
			}
		}

		public String ReleaseName
		{
			get { return _release.Name; }
			set
			{
				_release.Name = value;
				NotifyPropertyChanged("ReleaseName");
			}
		}

		public void Show()
		{
			_view.ProjectName.Validating += ProjectNameValidating;
			_view.ProjectName.DataBindings.Add("Text", this, "ProjectName", false, DataSourceUpdateMode.OnPropertyChanged);

			_view.ProjectDescription.DataBindings.Add("Text", this, "ProjectDescription", false,
			                                          DataSourceUpdateMode.OnPropertyChanged);

			_view.ReleaseName.Validating += ReleaseNameValidating;
			_view.ReleaseName.DataBindings.Add("Text", this, "ReleaseName", false, DataSourceUpdateMode.OnPropertyChanged);
			_view.CreateButton.DataBindings.Add("Enabled", this, "IsValid", false, DataSourceUpdateMode.OnPropertyChanged);

			DialogResult result = _view.ShowDialog();
			if (DialogResult.OK == result)
			{
				_project.Post();
				_release.ProjectId = _project.Id;
				_release.Post();

				//Refresh the project with the new release
				_project.Get();

				if (NewProject != null)
				{
					NewProject(this, new NewProjectHandlerArgs(_project));					
				}
				if (NewRelease != null)
				{
					NewRelease(this, new NewReleaseHandlerArgs(_release));
				}
			}
		}

		public bool IsValid
		{
			get
			{
				return String.IsNullOrWhiteSpace(_view.ErrorProvider.GetError(_view.ProjectName))
				&& String.IsNullOrWhiteSpace(_view.ErrorProvider.GetError(_view.ReleaseName));
			}
		}

		private void ProjectNameValidating(object sender, CancelEventArgs e)
		{
			if (String.IsNullOrWhiteSpace(_view.ProjectName.Text))
			{
				_view.ErrorProvider.SetError(_view.ProjectName, "Name is required to create a project.");
			}
			else
			{
				_view.ErrorProvider.SetError(_view.ProjectName, String.Empty);
			}
			NotifyPropertyChanged("IsValid");
		}

		private void ReleaseNameValidating(object sender, CancelEventArgs e)
		{
			if (String.IsNullOrWhiteSpace(_view.ReleaseName.Text))
			{
				_view.ErrorProvider.SetError(_view.ReleaseName, "Release is required to create a new project.");
			}
			else
			{
				_view.ErrorProvider.SetError(_view.ReleaseName, String.Empty);
			}
			NotifyPropertyChanged("IsValid");
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public void Dispose()
		{
			_view.ProjectName.Validating -= ProjectNameValidating;
			_view.ProjectName.DataBindings.Clear();

			_view.ProjectDescription.DataBindings.Clear();

			_view.ReleaseName.Validating -= ReleaseNameValidating;
			_view.ReleaseName.DataBindings.Clear();
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
