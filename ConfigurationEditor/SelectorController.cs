using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SlickQA.DataCollector.Configuration;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	public class SelectorController : IDisposable, INotifyPropertyChanged
	{
		private ResultDestination _resultDestination;
		private readonly Button _getProjectButton;
		private readonly ComboBox _projectComboBox;
		private readonly ComboBox _releaseComboBox;
		private readonly Button _addProjectButton;
		private readonly Button _addReleaseButton;

		public SelectorController(IConfigurationView view)
		{
			Releases = new BindingList<Release>
			           {AllowNew = true, AllowEdit = false, AllowRemove = false, RaiseListChangedEvents = true};
			Projects = new BindingList<Project> { AllowNew = true, AllowEdit = false, AllowRemove = false, RaiseListChangedEvents = true};

			_getProjectButton = view.GetProject;
			_projectComboBox = view.Project;
			_releaseComboBox = view.Release;
			_addProjectButton = view.AddProject;
			_addReleaseButton = view.AddRelease;
		}

		public BindingList<Project> Projects { get; set; }
		private BindingList<Release> Releases { get; set; }

		public bool EnableProject
		{
			get { return Projects.Count != 0; }
		}

		public bool EnableRelease
		{
			get { return _resultDestination.Project != null; }
		}

		public void SetConfig(object sender, ResetConfigHandlerArgs args)
		{
			ClearEventHandlers();
			ClearBindings();

			_resultDestination = args.Config.ResultDestination;

			Projects.Clear();
			Releases.Clear();

			_projectComboBox.DataSource = Projects;
			_projectComboBox.DisplayMember = null;
			_projectComboBox.ValueMember = null;
			_projectComboBox.DataBindings.Add("Enabled", this, "EnableProject", false, DataSourceUpdateMode.OnPropertyChanged);
			_projectComboBox.SelectedIndexChanged += OnSelectedProjectChanged;

			_releaseComboBox.DataSource = Releases;
			_releaseComboBox.DisplayMember = null;
			_releaseComboBox.ValueMember = null;
			_releaseComboBox.DataBindings.Add("Enabled", this, "EnableRelease", false, DataSourceUpdateMode.OnPropertyChanged);
			_releaseComboBox.SelectedIndexChanged += OnSelectedReleaseChanged;
			
			_getProjectButton.Click += GetProjects;
			_addProjectButton.Click += CreateProject;

			_addReleaseButton.DataBindings.Add("Enabled", this, "EnableRelease", false, DataSourceUpdateMode.OnPropertyChanged);
			_addReleaseButton.Click += CreateRelease;
		}

		private void OnSelectedReleaseChanged(object sender, EventArgs eventArgs)
		{
			var releaseBox = sender as ComboBox;

			if (releaseBox.SelectedIndex != -1)
			{
				var release = releaseBox.SelectedItem as Release;
				_resultDestination.Release = release;
			}
		}

		private void OnSelectedProjectChanged(object sender, EventArgs eventArgs)
		{
			var projectBox = sender as ComboBox;

			var project = projectBox.SelectedItem as Project;

			if (project != null)
			{
				Releases.Clear();
				foreach (var release in project.Releases.OrderBy(release => release.Name))
				{
					release.ProjectId = project.Id;
					Releases.Add(release);
				}

				if (project == _resultDestination.Project && _resultDestination.Release != null)
				{
					var release = _resultDestination.Release;

					_releaseComboBox.SelectedIndex = -1;
					_releaseComboBox.SelectedItem = release;
				}
				else
				{
					_resultDestination.Project = project;

					_releaseComboBox.SelectedIndex = -1;
					_releaseComboBox.SelectedIndex = 0;
				}
			}
			NotifyPropertyChanged("EnableRelease");
		}

		private void CreateProject(object sender, EventArgs eventArgs)
		{
			ICreateProjectController projectController = new CreateProjectController();
			projectController.NewProject += AddNewProject;
			projectController.NewRelease += AddNewRelease;
			projectController.Show();
		}

		private void AddNewProject(object sender, NewProjectHandlerArgs args)
		{
			var project = args.Project;
			Projects.Add(project);

			_projectComboBox.SelectedItem = project;

			NotifyPropertyChanged("EnableProject");
			NotifyPropertyChanged("EnableRelease");
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private void CreateRelease(object sender, EventArgs eventArgs)
		{
			ICreateReleaseController releaseController = new CreateReleaseController(_resultDestination.Project);
			releaseController.NewRelease += AddNewRelease;
			releaseController.Show();
		}

		private void AddNewRelease(object sender, NewReleaseHandlerArgs args)
		{
			var release = args.Release;
			Releases.Add(release);
			

			//Refresh the project object so it contains the full list of releases.
			var project = _projectComboBox.SelectedItem as Project;
			project.Get();

			_releaseComboBox.SelectedItem = release;
			NotifyPropertyChanged("EnableRelease");
		}

		public void GetProjects(object sender, EventArgs eventArgs)
		{
			var projects = JsonObject<Project>.GetList().OrderBy(project => project.Name);
			Projects.Clear();
			foreach (var proj in projects)
			{
				Projects.Add(proj);
			}
			if (_resultDestination.Project != null)
			{
				_projectComboBox.SelectedItem = _resultDestination.Project;
			}
			else
			{
				_projectComboBox.SelectedIndex = 0;
			}
			OnSelectedProjectChanged(_projectComboBox, new EventArgs());
			NotifyPropertyChanged("EnableProject");
		}

		public void Dispose()
		{
			ClearEventHandlers();

			ClearBindings();
		}

		private void ClearBindings()
		{
			if (_resultDestination != null)
			{
				_resultDestination.Project = null;
				_resultDestination.Release = null;
			}

			_projectComboBox.DataBindings.Clear();
			_projectComboBox.DataSource = null;

			_releaseComboBox.DataBindings.Clear();
			_releaseComboBox.DataSource = null;

			_addReleaseButton.DataBindings.Clear();
		}

		private void ClearEventHandlers()
		{
			_addReleaseButton.Click -= CreateRelease;
			_addProjectButton.Click -= CreateProject;
			_getProjectButton.Click -= GetProjects;
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
