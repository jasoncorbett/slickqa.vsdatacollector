/* Copyright 2012 AccessData Group, LLC.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
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
			this.port = new System.Windows.Forms.NumericUpDown();
			this.projectLabel = new System.Windows.Forms.Label();
			this.urlLabel = new System.Windows.Forms.Label();
			this.trailingSlash = new System.Windows.Forms.Label();
			this.project = new System.Windows.Forms.ComboBox();
			this.addProject = new System.Windows.Forms.Button();
			this.getProjects = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.port)).BeginInit();
			this.SuspendLayout();
			// 
			// protocol
			// 
			this.protocol.AccessibleName = "protocol";
			this.protocol.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
			this.protocol.FormattingEnabled = true;
			this.protocol.Location = new System.Drawing.Point(80, 15);
			this.protocol.Name = "protocol";
			this.protocol.Size = new System.Drawing.Size(65, 21);
			this.protocol.TabIndex = 1;
			// 
			// hostLabel
			// 
			this.hostLabel.AccessibleName = "hostLabel";
			this.hostLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this.hostLabel.AutoSize = true;
			this.hostLabel.Location = new System.Drawing.Point(151, 19);
			this.hostLabel.Name = "hostLabel";
			this.hostLabel.Size = new System.Drawing.Size(20, 13);
			this.hostLabel.TabIndex = 2;
			this.hostLabel.Text = "://";
			// 
			// host
			// 
			this.host.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.host.Location = new System.Drawing.Point(177, 15);
			this.host.Name = "host";
			this.host.Size = new System.Drawing.Size(100, 20);
			this.host.TabIndex = 3;
			// 
			// portLabel
			// 
			this.portLabel.AccessibleName = "portLabel";
			this.portLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this.portLabel.AutoSize = true;
			this.portLabel.Location = new System.Drawing.Point(283, 19);
			this.portLabel.Name = "portLabel";
			this.portLabel.Size = new System.Drawing.Size(10, 13);
			this.portLabel.TabIndex = 4;
			this.portLabel.Text = ":";
			// 
			// sitePath
			// 
			this.sitePath.AccessibleName = "sitePath";
			this.sitePath.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.sitePath.Location = new System.Drawing.Point(443, 15);
			this.sitePath.Name = "sitePath";
			this.sitePath.Size = new System.Drawing.Size(100, 20);
			this.sitePath.TabIndex = 7;
			// 
			// sitePathLabel
			// 
			this.sitePathLabel.AccessibleName = "sitePathLabel";
			this.sitePathLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this.sitePathLabel.AutoSize = true;
			this.sitePathLabel.Location = new System.Drawing.Point(425, 19);
			this.sitePathLabel.Name = "sitePathLabel";
			this.sitePathLabel.Size = new System.Drawing.Size(12, 13);
			this.sitePathLabel.TabIndex = 6;
			this.sitePathLabel.Text = "/";
			// 
			// port
			// 
			this.port.AccessibleName = "port";
			this.port.AccessibleRole = System.Windows.Forms.AccessibleRole.Dial;
			this.port.AutoSize = true;
			this.port.Location = new System.Drawing.Point(299, 15);
			this.port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.port.Name = "port";
			this.port.Size = new System.Drawing.Size(120, 20);
			this.port.TabIndex = 5;
			this.port.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
			// 
			// projectLabel
			// 
			this.projectLabel.AccessibleName = "projectLabel";
			this.projectLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this.projectLabel.AutoSize = true;
			this.projectLabel.Location = new System.Drawing.Point(3, 71);
			this.projectLabel.Name = "projectLabel";
			this.projectLabel.Size = new System.Drawing.Size(40, 13);
			this.projectLabel.TabIndex = 10;
			this.projectLabel.Text = "Project";
			// 
			// urlLabel
			// 
			this.urlLabel.AccessibleName = "urlLabel";
			this.urlLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this.urlLabel.AutoSize = true;
			this.urlLabel.Location = new System.Drawing.Point(3, 19);
			this.urlLabel.Name = "urlLabel";
			this.urlLabel.Size = new System.Drawing.Size(55, 13);
			this.urlLabel.TabIndex = 0;
			this.urlLabel.Text = "Slick URL";
			// 
			// trailingSlash
			// 
			this.trailingSlash.AccessibleName = "trailingSlash";
			this.trailingSlash.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this.trailingSlash.AutoSize = true;
			this.trailingSlash.Location = new System.Drawing.Point(549, 19);
			this.trailingSlash.Name = "trailingSlash";
			this.trailingSlash.Size = new System.Drawing.Size(12, 13);
			this.trailingSlash.TabIndex = 8;
			this.trailingSlash.Text = "/";
			// 
			// project
			// 
			this.project.AccessibleName = "project";
			this.project.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.project.FormattingEnabled = true;
			this.project.Location = new System.Drawing.Point(80, 68);
			this.project.Name = "project";
			this.project.Size = new System.Drawing.Size(269, 21);
			this.project.Sorted = true;
			this.project.TabIndex = 11;
			// 
			// addProject
			// 
			this.addProject.AccessibleName = "addProject";
			this.addProject.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.addProject.Location = new System.Drawing.Point(355, 67);
			this.addProject.Name = "addProject";
			this.addProject.Size = new System.Drawing.Size(109, 20);
			this.addProject.TabIndex = 12;
			this.addProject.Text = "Add Project";
			this.addProject.UseVisualStyleBackColor = true;
			// 
			// getProjects
			// 
			this.getProjects.AccessibleName = "getProjects";
			this.getProjects.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.getProjects.Location = new System.Drawing.Point(567, 11);
			this.getProjects.Name = "getProjects";
			this.getProjects.Size = new System.Drawing.Size(89, 28);
			this.getProjects.TabIndex = 9;
			this.getProjects.Text = "Get Projects";
			this.getProjects.UseVisualStyleBackColor = true;
			this.getProjects.Click += new System.EventHandler(this.GetProjects);
			// 
			// ConfigurationEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.getProjects);
			this.Controls.Add(this.addProject);
			this.Controls.Add(this.project);
			this.Controls.Add(this.trailingSlash);
			this.Controls.Add(this.urlLabel);
			this.Controls.Add(this.projectLabel);
			this.Controls.Add(this.port);
			this.Controls.Add(this.sitePathLabel);
			this.Controls.Add(this.sitePath);
			this.Controls.Add(this.portLabel);
			this.Controls.Add(this.host);
			this.Controls.Add(this.hostLabel);
			this.Controls.Add(this.protocol);
			this.Name = "ConfigurationEditor";
			this.Size = new System.Drawing.Size(670, 118);
			((System.ComponentModel.ISupportInitialize)(this.port)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label hostLabel;
		private System.Windows.Forms.Label portLabel;
		private System.Windows.Forms.Label sitePathLabel;
		private System.Windows.Forms.Label projectLabel;
		private System.Windows.Forms.Label urlLabel;
		private System.Windows.Forms.Label trailingSlash;
		private System.Windows.Forms.Button addProject;
		private System.Windows.Forms.Button getProjects;
		private System.Windows.Forms.ComboBox project;
		private System.Windows.Forms.ComboBox protocol;
		private System.Windows.Forms.TextBox host;
		private System.Windows.Forms.NumericUpDown port;
		private System.Windows.Forms.TextBox sitePath;
	}
}
