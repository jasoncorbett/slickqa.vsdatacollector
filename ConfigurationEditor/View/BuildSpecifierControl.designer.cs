namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	sealed partial class BuildSpecifierControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildSpecifierControl));
			this._buildProviderLabel = new System.Windows.Forms.Label();
			this._buildProviderText = new System.Windows.Forms.TextBox();
			this._buildProvider = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _buildProviderLabel
			// 
			resources.ApplyResources(this._buildProviderLabel, "_buildProviderLabel");
			this._buildProviderLabel.Name = "_buildProviderLabel";
			// 
			// _buildProviderText
			// 
			resources.ApplyResources(this._buildProviderText, "_buildProviderText");
			this._buildProviderText.Name = "_buildProviderText";
			// 
			// _buildProvider
			// 
			resources.ApplyResources(this._buildProvider, "_buildProvider");
			this._buildProvider.Name = "_buildProvider";
			this._buildProvider.UseVisualStyleBackColor = true;
			this._buildProvider.Click += new System.EventHandler(this.BuildProviderClick);
			// 
			// BuildSpecifierControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._buildProviderLabel);
			this.Controls.Add(this._buildProviderText);
			this.Controls.Add(this._buildProvider);
			this.Name = "BuildSpecifierControl";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label _buildProviderLabel;
		private System.Windows.Forms.TextBox _buildProviderText;
		private System.Windows.Forms.Button _buildProvider;
	}
}
