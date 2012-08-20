namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	sealed partial class NewProjectForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectForm));
			this._okButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._projectName = new System.Windows.Forms.TextBox();
			this._projectDescription = new System.Windows.Forms.TextBox();
			this._releaseName = new System.Windows.Forms.TextBox();
			this._projectTags = new System.Windows.Forms.TextBox();
			this._nameLabel = new System.Windows.Forms.Label();
			this._descriptionLabel = new System.Windows.Forms.Label();
			this._releaseLabel = new System.Windows.Forms.Label();
			this._tagsLabel = new System.Windows.Forms.Label();
			this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// _okButton
			// 
			this._okButton.CausesValidation = false;
			this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this._okButton, "_okButton");
			this._okButton.Name = "_okButton";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new System.EventHandler(this.CreateButtonClick);
			// 
			// _cancelButton
			// 
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			this._cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
			// 
			// _projectName
			// 
			this._errorProvider.SetError(this._projectName, resources.GetString("_projectName.Error"));
			resources.ApplyResources(this._projectName, "_projectName");
			this._projectName.Name = "_projectName";
			this._projectName.TextChanged += new System.EventHandler(this.ProjectNameTextChanged);
			// 
			// _projectDescription
			// 
			this._projectDescription.CausesValidation = false;
			resources.ApplyResources(this._projectDescription, "_projectDescription");
			this._projectDescription.Name = "_projectDescription";
			this._projectDescription.TextChanged += new System.EventHandler(this.ProjectDescriptionTextChanged);
			// 
			// _releaseName
			// 
			this._errorProvider.SetError(this._releaseName, resources.GetString("_releaseName.Error"));
			resources.ApplyResources(this._releaseName, "_releaseName");
			this._releaseName.Name = "_releaseName";
			this._releaseName.TextChanged += new System.EventHandler(this.ReleaseNameTextChanged);
			// 
			// _projectTags
			// 
			this._projectTags.CausesValidation = false;
			resources.ApplyResources(this._projectTags, "_projectTags");
			this._projectTags.Name = "_projectTags";
			this._projectTags.TextChanged += new System.EventHandler(this.ProjectTagsTextChanged);
			// 
			// _nameLabel
			// 
			resources.ApplyResources(this._nameLabel, "_nameLabel");
			this._nameLabel.Name = "_nameLabel";
			// 
			// _descriptionLabel
			// 
			resources.ApplyResources(this._descriptionLabel, "_descriptionLabel");
			this._descriptionLabel.Name = "_descriptionLabel";
			// 
			// _releaseLabel
			// 
			resources.ApplyResources(this._releaseLabel, "_releaseLabel");
			this._releaseLabel.Name = "_releaseLabel";
			// 
			// _tagsLabel
			// 
			resources.ApplyResources(this._tagsLabel, "_tagsLabel");
			this._tagsLabel.Name = "_tagsLabel";
			// 
			// _errorProvider
			// 
			this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this._errorProvider.ContainerControl = this;
			// 
			// NewProjectForm
			// 
			this.AcceptButton = this._okButton;
			resources.ApplyResources(this, "$this");
			this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			this.CancelButton = this._cancelButton;
			this.Controls.Add(this._tagsLabel);
			this.Controls.Add(this._releaseLabel);
			this.Controls.Add(this._descriptionLabel);
			this.Controls.Add(this._nameLabel);
			this.Controls.Add(this._projectTags);
			this.Controls.Add(this._releaseName);
			this.Controls.Add(this._projectDescription);
			this.Controls.Add(this._projectName);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewProjectForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.TextBox _projectName;
		private System.Windows.Forms.TextBox _projectDescription;
		private System.Windows.Forms.TextBox _releaseName;
		private System.Windows.Forms.TextBox _projectTags;
		private System.Windows.Forms.Label _nameLabel;
		private System.Windows.Forms.Label _descriptionLabel;
		private System.Windows.Forms.Label _releaseLabel;
		private System.Windows.Forms.Label _tagsLabel;
		private System.Windows.Forms.ErrorProvider _errorProvider;
	}
}