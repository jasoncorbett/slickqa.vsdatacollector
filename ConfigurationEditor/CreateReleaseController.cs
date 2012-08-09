using System;
using System.ComponentModel;
using System.Windows.Forms;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	internal class CreateReleaseController : ICreateReleaseController, INotifyPropertyChanged
	{
		private readonly ICreateReleaseView _view;
		private readonly Release _release;

		public event NewReleaseHandler NewRelease;

		public CreateReleaseController(Project project)
			:this(new _createReleaseDialog(), project)
		{
		}

		public CreateReleaseController(ICreateReleaseView view, Project project)
		{
			_view = view;
			_release = new Release { ProjectId = project.Id };
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
			_view.ReleaseName.DataBindings.Add("Text", this, "ReleaseName", false, DataSourceUpdateMode.OnPropertyChanged);
			if (_view.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			_release.Post();
			if (NewRelease != null)
			{
				NewRelease(this, new NewReleaseHandlerArgs(_release));
			}
		}

		public bool IsValid
		{
			get { return !String.IsNullOrWhiteSpace(_view.ReleaseName.Text); }
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
			_view.ReleaseName.DataBindings.Clear();
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}