using SlickQA.DataCollector.ConfigurationEditor.App;

namespace SlickQA.TestFileEditor
{
    partial class MyEditor
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
			this._buildProvider = new SlickQA.DataCollector.ConfigurationEditor.View.BuildSpecifierControl();
			this._testPlan = new SlickQA.DataCollector.ConfigurationEditor.View.ExecutionNaming();
			this._resultDestination = new SlickQA.DataCollector.ConfigurationEditor.View.SelectResultDestinationControl();
			this._urlSelector = new SlickQA.DataCollector.ConfigurationEditor.View.UrlSelector();
			this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// _buildProvider
			// 
			this._buildProvider.Location = new System.Drawing.Point(33, 172);
			this._buildProvider.Name = "_buildProvider";
			this._buildProvider.Size = new System.Drawing.Size(620, 40);
			this._buildProvider.TabIndex = 0;
			// 
			// _testPlan
			// 
			this._testPlan.Location = new System.Drawing.Point(33, 123);
			this._testPlan.Name = "_testPlan";
			this._testPlan.Size = new System.Drawing.Size(353, 43);
			this._testPlan.TabIndex = 1;
			// 
			// _resultDestination
			// 
			this._resultDestination.Location = new System.Drawing.Point(33, 48);
			this._resultDestination.Name = "_resultDestination";
			this._resultDestination.Size = new System.Drawing.Size(458, 69);
			this._resultDestination.TabIndex = 2;
			// 
			// _urlSelector
			// 
			this._urlSelector.BackColor = System.Drawing.SystemColors.Control;
			this._urlSelector.Location = new System.Drawing.Point(33, 3);
			this._urlSelector.Name = "_urlSelector";
			this._urlSelector.Size = new System.Drawing.Size(617, 39);
			this._urlSelector.TabIndex = 3;
			// 
			// _errorProvider
			// 
			this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this._errorProvider.ContainerControl = this;
			// 
			// MyEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._urlSelector);
			this.Controls.Add(this._resultDestination);
			this.Controls.Add(this._testPlan);
			this.Controls.Add(this._buildProvider);
			this.Name = "MyEditor";
			this.Size = new System.Drawing.Size(814, 361);
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DataCollector.ConfigurationEditor.View.BuildSpecifierControl _buildProvider;
		private DataCollector.ConfigurationEditor.View.ExecutionNaming _testPlan;
		private DataCollector.ConfigurationEditor.View.SelectResultDestinationControl _resultDestination;
		private DataCollector.ConfigurationEditor.View.UrlSelector _urlSelector;
		private System.Windows.Forms.ErrorProvider _errorProvider;

	}
}
