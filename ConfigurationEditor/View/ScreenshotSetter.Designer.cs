namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	sealed partial class ScreenshotSetter
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenshotSetter));
			this._screenshotGroup = new System.Windows.Forms.GroupBox();
			this._posttestScreenshot = new System.Windows.Forms.CheckBox();
			this._failScreenshot = new System.Windows.Forms.CheckBox();
			this._pretestScreenshot = new System.Windows.Forms.CheckBox();
			this._screenshotGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// _screenshotGroup
			// 
			resources.ApplyResources(this._screenshotGroup, "_screenshotGroup");
			this._screenshotGroup.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._screenshotGroup.Controls.Add(this._posttestScreenshot);
			this._screenshotGroup.Controls.Add(this._failScreenshot);
			this._screenshotGroup.Controls.Add(this._pretestScreenshot);
			this._screenshotGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._screenshotGroup.Name = "_screenshotGroup";
			this._screenshotGroup.TabStop = false;
			// 
			// _posttestScreenshot
			// 
			resources.ApplyResources(this._posttestScreenshot, "_posttestScreenshot");
			this._posttestScreenshot.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
			this._posttestScreenshot.CausesValidation = false;
			this._posttestScreenshot.Name = "_posttestScreenshot";
			this._posttestScreenshot.UseVisualStyleBackColor = true;
			this._posttestScreenshot.CheckedChanged += new System.EventHandler(this.PosttestScreenshotCheckedChanged);
			// 
			// _failScreenshot
			// 
			resources.ApplyResources(this._failScreenshot, "_failScreenshot");
			this._failScreenshot.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
			this._failScreenshot.CausesValidation = false;
			this._failScreenshot.Checked = true;
			this._failScreenshot.CheckState = System.Windows.Forms.CheckState.Checked;
			this._failScreenshot.Name = "_failScreenshot";
			this._failScreenshot.UseVisualStyleBackColor = true;
			this._failScreenshot.CheckedChanged += new System.EventHandler(this.FailScreenshotCheckedChanged);
			// 
			// _pretestScreenshot
			// 
			resources.ApplyResources(this._pretestScreenshot, "_pretestScreenshot");
			this._pretestScreenshot.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
			this._pretestScreenshot.CausesValidation = false;
			this._pretestScreenshot.Name = "_pretestScreenshot";
			this._pretestScreenshot.UseVisualStyleBackColor = true;
			this._pretestScreenshot.CheckedChanged += new System.EventHandler(this.PretestScreenshotCheckedChanged);
			// 
			// ScreenshotSetter
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._screenshotGroup);
			this.Name = "ScreenshotSetter";
			this._screenshotGroup.ResumeLayout(false);
			this._screenshotGroup.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox _screenshotGroup;
		private System.Windows.Forms.CheckBox _posttestScreenshot;
		private System.Windows.Forms.CheckBox _failScreenshot;
		private System.Windows.Forms.CheckBox _pretestScreenshot;
	}
}
