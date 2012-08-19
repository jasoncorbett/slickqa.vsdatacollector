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
			this._createButton = new System.Windows.Forms.Button();
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
			// _createButton
			// 
			resources.ApplyResources(this._createButton, "_createButton");
			this._createButton.CausesValidation = false;
			this._createButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._errorProvider.SetError(this._createButton, resources.GetString("_createButton.Error"));
			this._errorProvider.SetIconAlignment(this._createButton, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("_createButton.IconAlignment"))));
			this._errorProvider.SetIconPadding(this._createButton, ((int)(resources.GetObject("_createButton.IconPadding"))));
			this._createButton.Name = "_createButton";
			this._createButton.UseVisualStyleBackColor = true;
			this._createButton.Click += new System.EventHandler(this.CreateButtonClick);
			// 
			// _cancelButton
			// 
			resources.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._errorProvider.SetError(this._cancelButton, resources.GetString("_cancelButton.Error"));
			this._errorProvider.SetIconAlignment(this._cancelButton, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("_cancelButton.IconAlignment"))));
			this._errorProvider.SetIconPadding(this._cancelButton, ((int)(resources.GetObject("_cancelButton.IconPadding"))));
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			this._cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
			// 
			// _projectName
			// 
			resources.ApplyResources(this._projectName, "_projectName");
			this._errorProvider.SetError(this._projectName, resources.GetString("_projectName.Error"));
			this._errorProvider.SetIconAlignment(this._projectName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("_projectName.IconAlignment"))));
			this._errorProvider.SetIconPadding(this._projectName, ((int)(resources.GetObject("_projectName.IconPadding"))));
			this._projectName.Name = "_projectName";
			this._projectName.TextChanged += new System.EventHandler(this.ProjectNameTextChanged);
			// 
			// _projectDescription
			// 
			resources.ApplyResources(this._projectDescription, "_projectDescription");
			this._projectDescription.CausesValidation = false;
			this._errorProvider.SetError(this._projectDescription, resources.GetString("_projectDescription.Error"));
			this._errorProvider.SetIconAlignment(this._projectDescription, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("_projectDescription.IconAlignment"))));
			this._errorProvider.SetIconPadding(this._projectDescription, ((int)(resources.GetObject("_projectDescription.IconPadding"))));
			this._projectDescription.Name = "_projectDescription";
			this._projectDescription.TextChanged += new System.EventHandler(this.ProjectDescriptionTextChanged);
			// 
			// _releaseName
			// 
			resources.ApplyResources(this._releaseName, "_releaseName");
			this._errorProvider.SetError(this._releaseName, resources.GetString("_releaseName.Error"));
			this._errorProvider.SetIconAlignment(this._releaseName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("_releaseName.IconAlignment"))));
			this._errorProvider.SetIconPadding(this._releaseName, ((int)(resources.GetObject("_releaseName.IconPadding"))));
			this._releaseName.Name = "_releaseName";
			this._releaseName.TextChanged += new System.EventHandler(this.ReleaseNameTextChanged);
			// 
			// _projectTags
			// 
			resources.ApplyResources(this._projectTags, "_projectTags");
			this._projectTags.CausesValidation = false;
			this._errorProvider.SetError(this._projectTags, resources.GetString("_projectTags.Error"));
			this._errorProvider.SetIconAlignment(this._projectTags, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("_projectTags.IconAlignment"))));
			this._errorProvider.SetIconPadding(this._projectTags, ((int)(resources.GetObject("_projectTags.IconPadding"))));
			this._projectTags.Name = "_projectTags";
			this._projectTags.TextChanged += new System.EventHandler(this.ProjectTagsTextChanged);
			// 
			// _nameLabel
			// 
			resources.ApplyResources(this._nameLabel, "_nameLabel");
			this._errorProvider.SetError(this._nameLabel, resources.GetString("_nameLabel.Error"));
			this._errorProvider.SetIconAlignment(this._nameLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("_nameLabel.IconAlignment"))));
			this._errorProvider.SetIconPadding(this._nameLabel, ((int)(resources.GetObject("_nameLabel.IconPadding"))));
			this._nameLabel.Name = "_nameLabel";
			// 
			// _descriptionLabel
			// 
			resources.ApplyResources(this._descriptionLabel, "_descriptionLabel");
			this._errorProvider.SetError(this._descriptionLabel, resources.GetString("_descriptionLabel.Error"));
			this._errorProvider.SetIconAlignment(this._descriptionLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("_descriptionLabel.IconAlignment"))));
			this._errorProvider.SetIconPadding(this._descriptionLabel, ((int)(resources.GetObject("_descriptionLabel.IconPadding"))));
			this._descriptionLabel.Name = "_descriptionLabel";
			// 
			// _releaseLabel
			// 
			resources.ApplyResources(this._releaseLabel, "_releaseLabel");
			this._errorProvider.SetError(this._releaseLabel, resources.GetString("_releaseLabel.Error"));
			this._errorProvider.SetIconAlignment(this._releaseLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("_releaseLabel.IconAlignment"))));
			this._errorProvider.SetIconPadding(this._releaseLabel, ((int)(resources.GetObject("_releaseLabel.IconPadding"))));
			this._releaseLabel.Name = "_releaseLabel";
			// 
			// _tagsLabel
			// 
			resources.ApplyResources(this._tagsLabel, "_tagsLabel");
			this._errorProvider.SetError(this._tagsLabel, resources.GetString("_tagsLabel.Error"));
			this._errorProvider.SetIconAlignment(this._tagsLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("_tagsLabel.IconAlignment"))));
			this._errorProvider.SetIconPadding(this._tagsLabel, ((int)(resources.GetObject("_tagsLabel.IconPadding"))));
			this._tagsLabel.Name = "_tagsLabel";
			// 
			// _errorProvider
			// 
			this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this._errorProvider.ContainerControl = this;
			resources.ApplyResources(this._errorProvider, "_errorProvider");
			// 
			// NewProjectForm
			// 
			this.AcceptButton = this._createButton;
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
			this.Controls.Add(this._createButton);
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

		private System.Windows.Forms.Button _createButton;
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