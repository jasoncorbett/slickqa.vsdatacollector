namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	partial class MainEditorControl
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
			this._executionNaming = new ExecutionNaming();
			this._screenshotSetter = new ScreenshotSetter();
			this._buildSpecifierControl = new BuildSpecifierControl();
			this._resultDestinationControl = new SelectResultDestinationControl();
			this._urlSelector = new UrlSelector();
			this.SuspendLayout();
			// 
			// _executionNaming
			// 
			this._executionNaming.Controller = null;
			this._executionNaming.Location = new System.Drawing.Point(0, 116);
			this._executionNaming.Name = "_executionNaming";
			this._executionNaming.Size = new System.Drawing.Size(353, 43);
			this._executionNaming.TabIndex = 4;
			// 
			// _screenshotSetter
			// 
			this._screenshotSetter.Controller = null;
			this._screenshotSetter.Location = new System.Drawing.Point(487, 45);
			this._screenshotSetter.Name = "_screenshotSetter";
			this._screenshotSetter.Size = new System.Drawing.Size(133, 105);
			this._screenshotSetter.TabIndex = 3;
			// 
			// _buildSpecifierControl
			// 
			this._buildSpecifierControl.Controller = null;
			this._buildSpecifierControl.Location = new System.Drawing.Point(0, 168);
			this._buildSpecifierControl.Name = "_buildSpecifierControl";
			this._buildSpecifierControl.Size = new System.Drawing.Size(620, 40);
			this._buildSpecifierControl.TabIndex = 2;
			// 
			// _resultDestinationControl
			// 
			this._resultDestinationControl.Controller = null;
			this._resultDestinationControl.Location = new System.Drawing.Point(3, 44);
			this._resultDestinationControl.Name = "_resultDestinationControl";
			this._resultDestinationControl.Size = new System.Drawing.Size(458, 69);
			this._resultDestinationControl.TabIndex = 1;
			// 
			// _urlSelector
			// 
			this._urlSelector.BackColor = System.Drawing.SystemColors.Control;
			this._urlSelector.Controller = null;
			this._urlSelector.Location = new System.Drawing.Point(3, 0);
			this._urlSelector.Name = "_urlSelector";
			this._urlSelector.Size = new System.Drawing.Size(617, 39);
			this._urlSelector.TabIndex = 0;
			// 
			// MainEditorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._executionNaming);
			this.Controls.Add(this._screenshotSetter);
			this.Controls.Add(this._buildSpecifierControl);
			this.Controls.Add(this._resultDestinationControl);
			this.Controls.Add(this._urlSelector);
			this.Name = "MainEditorControl";
			this.Size = new System.Drawing.Size(682, 292);
			this.ResumeLayout(false);

		}

		#endregion

		private UrlSelector _urlSelector;
		private SelectResultDestinationControl _resultDestinationControl;
		private BuildSpecifierControl _buildSpecifierControl;
		private ScreenshotSetter _screenshotSetter;
		private ExecutionNaming _executionNaming;

	}
}
