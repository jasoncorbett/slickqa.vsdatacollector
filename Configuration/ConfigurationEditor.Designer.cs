namespace SlickQA.DataCollector.Configuration
{
	partial class ConfigurationEditor
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
			this.protocol = new System.Windows.Forms.ComboBox();
			this.hostLabel = new System.Windows.Forms.Label();
			this.host = new System.Windows.Forms.TextBox();
			this.portLabel = new System.Windows.Forms.Label();
			this.sitePath = new System.Windows.Forms.TextBox();
			this.sitePathLabel = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.fullURL = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.project = new System.Windows.Forms.ComboBox();
			this.addProject = new System.Windows.Forms.Button();
			this.getProjects = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// protocol
			// 
			this.protocol.FormattingEnabled = true;
			this.protocol.Items.AddRange(new object[] {
            "http",
            "https"});
			this.protocol.Location = new System.Drawing.Point(80, 18);
			this.protocol.Name = "protocol";
			this.protocol.Size = new System.Drawing.Size(121, 21);
			this.protocol.TabIndex = 0;
			// 
			// hostLabel
			// 
			this.hostLabel.AutoSize = true;
			this.hostLabel.Location = new System.Drawing.Point(207, 18);
			this.hostLabel.Name = "hostLabel";
			this.hostLabel.Size = new System.Drawing.Size(20, 13);
			this.hostLabel.TabIndex = 2;
			this.hostLabel.Text = "://";
			// 
			// host
			// 
			this.host.Location = new System.Drawing.Point(233, 15);
			this.host.Name = "host";
			this.host.Size = new System.Drawing.Size(100, 20);
			this.host.TabIndex = 3;
			// 
			// portLabel
			// 
			this.portLabel.AutoSize = true;
			this.portLabel.Location = new System.Drawing.Point(339, 18);
			this.portLabel.Name = "portLabel";
			this.portLabel.Size = new System.Drawing.Size(10, 13);
			this.portLabel.TabIndex = 4;
			this.portLabel.Text = ":";
			// 
			// sitePath
			// 
			this.sitePath.Location = new System.Drawing.Point(499, 18);
			this.sitePath.Name = "sitePath";
			this.sitePath.Size = new System.Drawing.Size(100, 20);
			this.sitePath.TabIndex = 5;
			// 
			// sitePathLabel
			// 
			this.sitePathLabel.AutoSize = true;
			this.sitePathLabel.Location = new System.Drawing.Point(481, 21);
			this.sitePathLabel.Name = "sitePathLabel";
			this.sitePathLabel.Size = new System.Drawing.Size(12, 13);
			this.sitePathLabel.TabIndex = 6;
			this.sitePathLabel.Text = "/";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.AutoSize = true;
			this.numericUpDown1.Location = new System.Drawing.Point(355, 16);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
			this.numericUpDown1.TabIndex = 7;
			this.numericUpDown1.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 71);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Project";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(55, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Slick URL";
			// 
			// fullURL
			// 
			this.fullURL.Enabled = false;
			this.fullURL.Location = new System.Drawing.Point(80, 45);
			this.fullURL.Name = "fullURL";
			this.fullURL.Size = new System.Drawing.Size(628, 20);
			this.fullURL.TabIndex = 11;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(605, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(12, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "/";
			// 
			// project
			// 
			this.project.FormattingEnabled = true;
			this.project.Location = new System.Drawing.Point(80, 68);
			this.project.Name = "project";
			this.project.Size = new System.Drawing.Size(269, 21);
			this.project.Sorted = true;
			this.project.TabIndex = 13;
			// 
			// addProject
			// 
			this.addProject.Location = new System.Drawing.Point(355, 67);
			this.addProject.Name = "addProject";
			this.addProject.Size = new System.Drawing.Size(109, 20);
			this.addProject.TabIndex = 14;
			this.addProject.Text = "Add Project";
			this.addProject.UseVisualStyleBackColor = true;
			// 
			// getProjects
			// 
			this.getProjects.Location = new System.Drawing.Point(705, 11);
			this.getProjects.Name = "getProjects";
			this.getProjects.Size = new System.Drawing.Size(89, 28);
			this.getProjects.TabIndex = 15;
			this.getProjects.Text = "Get Projects";
			this.getProjects.UseVisualStyleBackColor = true;
			// 
			// ConfigurationEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.getProjects);
			this.Controls.Add(this.addProject);
			this.Controls.Add(this.project);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.fullURL);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.sitePathLabel);
			this.Controls.Add(this.sitePath);
			this.Controls.Add(this.portLabel);
			this.Controls.Add(this.host);
			this.Controls.Add(this.hostLabel);
			this.Controls.Add(this.protocol);
			this.Name = "ConfigurationEditor";
			this.Size = new System.Drawing.Size(890, 118);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox protocol;
		private System.Windows.Forms.Label hostLabel;
		private System.Windows.Forms.TextBox host;
		private System.Windows.Forms.Label portLabel;
		private System.Windows.Forms.TextBox sitePath;
		private System.Windows.Forms.Label sitePathLabel;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox fullURL;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox project;
		private System.Windows.Forms.Button addProject;
		private System.Windows.Forms.Button getProjects;
	}
}
