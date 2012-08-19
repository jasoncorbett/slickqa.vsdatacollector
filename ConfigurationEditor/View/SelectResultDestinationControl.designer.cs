namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	sealed partial class SelectResultDestinationControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectResultDestinationControl));
			this._addRelease = new System.Windows.Forms.Button();
			this._release = new System.Windows.Forms.ComboBox();
			this._releaseLabel = new System.Windows.Forms.Label();
			this._addProject = new System.Windows.Forms.Button();
			this._project = new System.Windows.Forms.ComboBox();
			this._projectLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _addRelease
			// 
			resources.ApplyResources(this._addRelease, "_addRelease");
			this._addRelease.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this._addRelease.Name = "_addRelease";
			this._addRelease.UseVisualStyleBackColor = true;
			this._addRelease.Click += new System.EventHandler(this.AddReleaseClick);
			// 
			// _release
			// 
			resources.ApplyResources(this._release, "_release");
			this._release.AccessibleRole = System.Windows.Forms.AccessibleRole.DropList;
			this._release.CausesValidation = false;
			this._release.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._release.FormattingEnabled = true;
			this._release.Name = "_release";
			this._release.SelectedIndexChanged += new System.EventHandler(this.ReleaseSelectedIndexChanged);
			// 
			// _releaseLabel
			// 
			resources.ApplyResources(this._releaseLabel, "_releaseLabel");
			this._releaseLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._releaseLabel.Name = "_releaseLabel";
			// 
			// _addProject
			// 
			resources.ApplyResources(this._addProject, "_addProject");
			this._addProject.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this._addProject.Name = "_addProject";
			this._addProject.UseVisualStyleBackColor = true;
			this._addProject.Click += new System.EventHandler(this.AddProjectClick);
			// 
			// _project
			// 
			resources.ApplyResources(this._project, "_project");
			this._project.AccessibleRole = System.Windows.Forms.AccessibleRole.DropList;
			this._project.CausesValidation = false;
			this._project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._project.Name = "_project";
			this._project.Sorted = true;
			this._project.SelectedIndexChanged += new System.EventHandler(this.ProjectSelectedIndexChanged);
			// 
			// _projectLabel
			// 
			resources.ApplyResources(this._projectLabel, "_projectLabel");
			this._projectLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._projectLabel.Name = "_projectLabel";
			// 
			// SelectResultDestinationControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._addRelease);
			this.Controls.Add(this._release);
			this.Controls.Add(this._releaseLabel);
			this.Controls.Add(this._addProject);
			this.Controls.Add(this._project);
			this.Controls.Add(this._projectLabel);
			this.Name = "SelectResultDestinationControl";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _addRelease;
		private System.Windows.Forms.ComboBox _release;
		private System.Windows.Forms.Label _releaseLabel;
		private System.Windows.Forms.Button _addProject;
		private System.Windows.Forms.ComboBox _project;
		private System.Windows.Forms.Label _projectLabel;
	}
}
