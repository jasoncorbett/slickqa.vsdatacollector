namespace SlickQA.DataCollector.ConfigurationEditor
{
	partial class CreateProjectDialog
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
			this._createButton.CausesValidation = false;
			this._createButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._createButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this._createButton.Location = new System.Drawing.Point(157, 116);
			this._createButton.Name = "_createButton";
			this._createButton.Size = new System.Drawing.Size(75, 23);
			this._createButton.TabIndex = 8;
			this._createButton.Text = "Create";
			this._createButton.UseVisualStyleBackColor = true;
			// 
			// _cancelButton
			// 
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Location = new System.Drawing.Point(238, 116);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new System.Drawing.Size(75, 23);
			this._cancelButton.TabIndex = 9;
			this._cancelButton.Text = "Cancel";
			this._cancelButton.UseVisualStyleBackColor = true;
			// 
			// _projectName
			// 
			this._errorProvider.SetError(this._projectName, "Name is required to create a project.");
			this._projectName.Location = new System.Drawing.Point(95, 12);
			this._projectName.Name = "_projectName";
			this._projectName.Size = new System.Drawing.Size(218, 20);
			this._projectName.TabIndex = 1;
			this._projectName.Validating += new System.ComponentModel.CancelEventHandler(this.ProjectNameValidating);
			// 
			// _projectDescription
			// 
			this._projectDescription.CausesValidation = false;
			this._projectDescription.Location = new System.Drawing.Point(95, 38);
			this._projectDescription.Name = "_projectDescription";
			this._projectDescription.Size = new System.Drawing.Size(218, 20);
			this._projectDescription.TabIndex = 3;
			// 
			// _releaseName
			// 
			this._errorProvider.SetError(this._releaseName, "Release is required to create a new project.");
			this._releaseName.Location = new System.Drawing.Point(95, 64);
			this._releaseName.Name = "_releaseName";
			this._releaseName.Size = new System.Drawing.Size(218, 20);
			this._releaseName.TabIndex = 5;
			this._releaseName.Validating += new System.ComponentModel.CancelEventHandler(this.ReleaseNameValidating);
			// 
			// _projectTags
			// 
			this._projectTags.CausesValidation = false;
			this._projectTags.Location = new System.Drawing.Point(95, 90);
			this._projectTags.Name = "_projectTags";
			this._projectTags.Size = new System.Drawing.Size(218, 20);
			this._projectTags.TabIndex = 7;
			// 
			// _nameLabel
			// 
			this._nameLabel.AutoSize = true;
			this._nameLabel.Location = new System.Drawing.Point(29, 15);
			this._nameLabel.Name = "_nameLabel";
			this._nameLabel.Size = new System.Drawing.Size(35, 13);
			this._nameLabel.TabIndex = 0;
			this._nameLabel.Text = "Name";
			// 
			// _descriptionLabel
			// 
			this._descriptionLabel.AutoSize = true;
			this._descriptionLabel.Location = new System.Drawing.Point(29, 41);
			this._descriptionLabel.Name = "_descriptionLabel";
			this._descriptionLabel.Size = new System.Drawing.Size(60, 13);
			this._descriptionLabel.TabIndex = 2;
			this._descriptionLabel.Text = "Description";
			// 
			// _releaseLabel
			// 
			this._releaseLabel.AutoSize = true;
			this._releaseLabel.Location = new System.Drawing.Point(29, 67);
			this._releaseLabel.Name = "_releaseLabel";
			this._releaseLabel.Size = new System.Drawing.Size(46, 13);
			this._releaseLabel.TabIndex = 4;
			this._releaseLabel.Text = "Release";
			// 
			// _tagsLabel
			// 
			this._tagsLabel.AutoSize = true;
			this._tagsLabel.Location = new System.Drawing.Point(29, 93);
			this._tagsLabel.Name = "_tagsLabel";
			this._tagsLabel.Size = new System.Drawing.Size(31, 13);
			this._tagsLabel.TabIndex = 6;
			this._tagsLabel.Text = "Tags";
			// 
			// _errorProvider
			// 
			this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this._errorProvider.ContainerControl = this;
			// 
			// CreateProjectDialog
			// 
			this.AcceptButton = this._createButton;
			this.AccessibleName = "New Project Dialog";
			this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			this.CancelButton = this._cancelButton;
			this.ClientSize = new System.Drawing.Size(342, 160);
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
			this.Name = "CreateProjectDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Create Project";
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