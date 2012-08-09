namespace SlickQA.DataCollector.ConfigurationEditor
{
	partial class _createReleaseDialog
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
			this._release = new System.Windows.Forms.TextBox();
			this._okButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._nameLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _release
			// 
			this._release.AccessibleName = "Name";
			this._release.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this._release.Location = new System.Drawing.Point(53, 12);
			this._release.Name = "_release";
			this._release.Size = new System.Drawing.Size(189, 20);
			this._release.TabIndex = 0;
			// 
			// _okButton
			// 
			this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._okButton.Location = new System.Drawing.Point(86, 38);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new System.Drawing.Size(75, 23);
			this._okButton.TabIndex = 1;
			this._okButton.Text = "Ok";
			this._okButton.UseVisualStyleBackColor = true;
			// 
			// _cancelButton
			// 
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Location = new System.Drawing.Point(167, 38);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new System.Drawing.Size(75, 23);
			this._cancelButton.TabIndex = 2;
			this._cancelButton.Text = "Cancel";
			this._cancelButton.UseVisualStyleBackColor = true;
			// 
			// _nameLabel
			// 
			this._nameLabel.AccessibleName = "Name Label";
			this._nameLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._nameLabel.AutoSize = true;
			this._nameLabel.Location = new System.Drawing.Point(12, 15);
			this._nameLabel.Name = "_nameLabel";
			this._nameLabel.Size = new System.Drawing.Size(35, 13);
			this._nameLabel.TabIndex = 3;
			this._nameLabel.Text = "Name";
			// 
			// _createReleaseDialog
			// 
			this.AcceptButton = this._okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancelButton;
			this.ClientSize = new System.Drawing.Size(254, 73);
			this.Controls.Add(this._nameLabel);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._okButton);
			this.Controls.Add(this._release);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "_createReleaseDialog";
			this.Text = "Create Release";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _release;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Label _nameLabel;
	}
}