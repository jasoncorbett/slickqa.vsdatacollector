namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	sealed partial class ChooseBuildProviderForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseBuildProviderForm));
			this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this._methodTreeView = new System.Windows.Forms.TreeView();
			this.directoryLabel = new System.Windows.Forms.Label();
			this._assemblyTextBox = new System.Windows.Forms.TextBox();
			this._cancelButton = new System.Windows.Forms.Button();
			this._okButton = new System.Windows.Forms.Button();
			this._browse = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _openFileDialog
			// 
			resources.ApplyResources(this._openFileDialog, "_openFileDialog");
			// 
			// _methodTreeView
			// 
			resources.ApplyResources(this._methodTreeView, "_methodTreeView");
			this._methodTreeView.Name = "_methodTreeView";
			this._methodTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.MethodTreeViewAfterSelect);
			// 
			// directoryLabel
			// 
			resources.ApplyResources(this.directoryLabel, "directoryLabel");
			this.directoryLabel.Name = "directoryLabel";
			// 
			// _assemblyTextBox
			// 
			resources.ApplyResources(this._assemblyTextBox, "_assemblyTextBox");
			this._assemblyTextBox.Name = "_assemblyTextBox";
			this._assemblyTextBox.TextChanged += new System.EventHandler(this.AssemblyTextBoxTextChanged);
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
			// _browse
			// 
			resources.ApplyResources(this._browse, "_browse");
			this._browse.Name = "_browse";
			this._browse.UseVisualStyleBackColor = true;
			this._browse.Click += new System.EventHandler(this.BrowseClick);
			// 
			// ChooseBuildProviderForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._browse);
			this.Controls.Add(this._okButton);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._assemblyTextBox);
			this.Controls.Add(this.directoryLabel);
			this.Controls.Add(this._methodTreeView);
			this.Name = "ChooseBuildProviderForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog _openFileDialog;
		private System.Windows.Forms.TreeView _methodTreeView;
		private System.Windows.Forms.Label directoryLabel;
		private System.Windows.Forms.TextBox _assemblyTextBox;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _browse;
	}
}