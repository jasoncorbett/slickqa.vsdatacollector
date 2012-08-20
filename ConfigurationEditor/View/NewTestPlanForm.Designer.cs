namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	partial class NewTestPlanForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewTestPlanForm));
			this._nameLabel = new System.Windows.Forms.Label();
			this._planName = new System.Windows.Forms.TextBox();
			this._createdByLabel = new System.Windows.Forms.Label();
			this._creatorName = new System.Windows.Forms.TextBox();
			this._cancelButton = new System.Windows.Forms.Button();
			this._okButton = new System.Windows.Forms.Button();
			this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// _nameLabel
			// 
			resources.ApplyResources(this._nameLabel, "_nameLabel");
			this._nameLabel.Name = "_nameLabel";
			// 
			// _planName
			// 
			resources.ApplyResources(this._planName, "_planName");
			this._planName.Name = "_planName";
			this._planName.TextChanged += new System.EventHandler(this.PlanNameTextChanged);
			// 
			// _createdByLabel
			// 
			resources.ApplyResources(this._createdByLabel, "_createdByLabel");
			this._createdByLabel.Name = "_createdByLabel";
			// 
			// _creatorName
			// 
			resources.ApplyResources(this._creatorName, "_creatorName");
			this._creatorName.Name = "_creatorName";
			this._creatorName.TextChanged += new System.EventHandler(this.CreatorNameTextChanged);
			// 
			// _cancelButton
			// 
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			this._cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
			// 
			// _okButton
			// 
			this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this._okButton, "_okButton");
			this._okButton.Name = "_okButton";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// _errorProvider
			// 
			this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this._errorProvider.ContainerControl = this;
			// 
			// NewTestPlanForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._okButton);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._creatorName);
			this.Controls.Add(this._createdByLabel);
			this.Controls.Add(this._planName);
			this.Controls.Add(this._nameLabel);
			this.Name = "NewTestPlanForm";
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label _nameLabel;
		private System.Windows.Forms.TextBox _planName;
		private System.Windows.Forms.Label _createdByLabel;
		private System.Windows.Forms.TextBox _creatorName;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.ErrorProvider _errorProvider;
	}
}