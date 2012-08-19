namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	sealed partial class ExecutionNaming
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
			this._testPlanLabel = new System.Windows.Forms.Label();
			this._planComboBox = new System.Windows.Forms.ComboBox();
			this._addPlanButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _testPlanLabel
			// 
			this._testPlanLabel.AutoSize = true;
			this._testPlanLabel.Location = new System.Drawing.Point(11, 13);
			this._testPlanLabel.Name = "_testPlanLabel";
			this._testPlanLabel.Size = new System.Drawing.Size(52, 13);
			this._testPlanLabel.TabIndex = 1;
			this._testPlanLabel.Text = "Test Plan";
			// 
			// _planComboBox
			// 
			this._planComboBox.AccessibleName = "Test Plan";
			this._planComboBox.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
			this._planComboBox.CausesValidation = false;
			this._planComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._planComboBox.Enabled = false;
			this._planComboBox.Location = new System.Drawing.Point(69, 10);
			this._planComboBox.Name = "_planComboBox";
			this._planComboBox.Size = new System.Drawing.Size(192, 21);
			this._planComboBox.TabIndex = 3;
			this._planComboBox.SelectedIndexChanged += new System.EventHandler(this.PlanComboBoxSelectedIndexChanged);
			// 
			// _addPlanButton
			// 
			this._addPlanButton.Enabled = false;
			this._addPlanButton.Location = new System.Drawing.Point(267, 10);
			this._addPlanButton.Name = "_addPlanButton";
			this._addPlanButton.Size = new System.Drawing.Size(75, 23);
			this._addPlanButton.TabIndex = 4;
			this._addPlanButton.Text = "Add Plan";
			this._addPlanButton.UseVisualStyleBackColor = true;
			this._addPlanButton.Click += new System.EventHandler(this.AddPlanButtonClick);
			// 
			// ExecutionNaming
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._addPlanButton);
			this.Controls.Add(this._planComboBox);
			this.Controls.Add(this._testPlanLabel);
			this.Name = "ExecutionNaming";
			this.Size = new System.Drawing.Size(353, 43);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label _testPlanLabel;
		private System.Windows.Forms.ComboBox _planComboBox;
		private System.Windows.Forms.Button _addPlanButton;
	}
}
