namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	sealed partial class MainEditorControl
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainEditorControl));
			this._executionNaming = new SlickQA.DataCollector.ConfigurationEditor.View.ExecutionNaming();
			this._screenshotSetter = new SlickQA.DataCollector.ConfigurationEditor.View.ScreenshotSetter();
			this._buildSpecifierControl = new SlickQA.DataCollector.ConfigurationEditor.View.BuildSpecifierControl();
			this._resultDestinationControl = new SlickQA.DataCollector.ConfigurationEditor.View.SelectResultDestinationControl();
			this._urlSelector = new SlickQA.DataCollector.ConfigurationEditor.View.UrlSelector();
			this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// _executionNaming
			// 
			this._executionNaming.Controller = null;
			resources.ApplyResources(this._executionNaming, "_executionNaming");
			this._executionNaming.Name = "_executionNaming";
			// 
			// _screenshotSetter
			// 
			this._screenshotSetter.Controller = null;
			resources.ApplyResources(this._screenshotSetter, "_screenshotSetter");
			this._screenshotSetter.Name = "_screenshotSetter";
			// 
			// _buildSpecifierControl
			// 
			this._buildSpecifierControl.Controller = null;
			resources.ApplyResources(this._buildSpecifierControl, "_buildSpecifierControl");
			this._buildSpecifierControl.Name = "_buildSpecifierControl";
			// 
			// _resultDestinationControl
			// 
			this._resultDestinationControl.Controller = null;
			resources.ApplyResources(this._resultDestinationControl, "_resultDestinationControl");
			this._resultDestinationControl.Name = "_resultDestinationControl";
			// 
			// _urlSelector
			// 
			this._urlSelector.BackColor = System.Drawing.SystemColors.Control;
			this._urlSelector.Controller = null;
			resources.ApplyResources(this._urlSelector, "_urlSelector");
			this._urlSelector.Name = "_urlSelector";
			// 
			// _errorProvider
			// 
			this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this._errorProvider.ContainerControl = this;
			// 
			// MainEditorControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._executionNaming);
			this.Controls.Add(this._screenshotSetter);
			this.Controls.Add(this._buildSpecifierControl);
			this.Controls.Add(this._resultDestinationControl);
			this.Controls.Add(this._urlSelector);
			this.Name = "MainEditorControl";
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private UrlSelector _urlSelector;
		private SelectResultDestinationControl _resultDestinationControl;
		private BuildSpecifierControl _buildSpecifierControl;
		private ScreenshotSetter _screenshotSetter;
		private ExecutionNaming _executionNaming;
		private System.Windows.Forms.ErrorProvider _errorProvider;

	}
}
