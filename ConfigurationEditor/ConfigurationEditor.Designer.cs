// Copyright 2012 AccessData Group, LLC.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//  http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace SlickQA.DataCollector.ConfigurationEditor
{
	sealed partial class ConfigurationEditor 
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
			this._scheme = new System.Windows.Forms.ComboBox();
			this._hostLabel = new System.Windows.Forms.Label();
			this._host = new System.Windows.Forms.TextBox();
			this.portLabel = new System.Windows.Forms.Label();
			this._sitePath = new System.Windows.Forms.TextBox();
			this._sitePathSlash = new System.Windows.Forms.Label();
			this._port = new System.Windows.Forms.NumericUpDown();
			this._projectLabel = new System.Windows.Forms.Label();
			this._urlLabel = new System.Windows.Forms.Label();
			this._trailingSlash = new System.Windows.Forms.Label();
			this._project = new System.Windows.Forms.ComboBox();
			this._addProject = new System.Windows.Forms.Button();
			this.getProjects = new System.Windows.Forms.Button();
			this._releaseLabel = new System.Windows.Forms.Label();
			this._release = new System.Windows.Forms.ComboBox();
			this._addRelease = new System.Windows.Forms.Button();
			this._screenshotGroup = new System.Windows.Forms.GroupBox();
			this._posttestScreenshot = new System.Windows.Forms.CheckBox();
			this._failScreenshot = new System.Windows.Forms.CheckBox();
			this._pretestScreenshot = new System.Windows.Forms.CheckBox();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this._port)).BeginInit();
			this._screenshotGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// _scheme
			// 
			this._scheme.AccessibleName = "scheme";
			this._scheme.AccessibleRole = System.Windows.Forms.AccessibleRole.DropList;
			this._scheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._scheme.FormattingEnabled = true;
			this._scheme.Location = new System.Drawing.Point(80, 15);
			this._scheme.Name = "_scheme";
			this._scheme.Size = new System.Drawing.Size(65, 21);
			this._scheme.TabIndex = 1;
			// 
			// _hostLabel
			// 
			this._hostLabel.AccessibleName = "hostLabel";
			this._hostLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._hostLabel.AutoSize = true;
			this._hostLabel.Location = new System.Drawing.Point(151, 19);
			this._hostLabel.Name = "_hostLabel";
			this._hostLabel.Size = new System.Drawing.Size(20, 13);
			this._hostLabel.TabIndex = 2;
			this._hostLabel.Text = "://";
			// 
			// _host
			// 
			this._host.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this._host.Location = new System.Drawing.Point(177, 15);
			this._host.Name = "_host";
			this._host.Size = new System.Drawing.Size(100, 20);
			this._host.TabIndex = 3;
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
			// _sitePath
			// 
			this._sitePath.AccessibleName = "sitePath";
			this._sitePath.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this._sitePath.Location = new System.Drawing.Point(443, 15);
			this._sitePath.Name = "_sitePath";
			this._sitePath.Size = new System.Drawing.Size(100, 20);
			this._sitePath.TabIndex = 7;
			// 
			// _sitePathSlash
			// 
			this._sitePathSlash.AccessibleName = "sitePathSlash";
			this._sitePathSlash.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._sitePathSlash.AutoSize = true;
			this._sitePathSlash.Location = new System.Drawing.Point(425, 19);
			this._sitePathSlash.Name = "_sitePathSlash";
			this._sitePathSlash.Size = new System.Drawing.Size(12, 13);
			this._sitePathSlash.TabIndex = 6;
			this._sitePathSlash.Text = "/";
			// 
			// _port
			// 
			this._port.AccessibleName = "Port";
			this._port.AccessibleRole = System.Windows.Forms.AccessibleRole.Dial;
			this._port.AutoSize = true;
			this._port.Location = new System.Drawing.Point(299, 15);
			this._port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this._port.Name = "_port";
			this._port.Size = new System.Drawing.Size(120, 20);
			this._port.TabIndex = 5;
			this._port.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
			// 
			// _projectLabel
			// 
			this._projectLabel.AccessibleName = "projectLabel";
			this._projectLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._projectLabel.AutoSize = true;
			this._projectLabel.Location = new System.Drawing.Point(3, 71);
			this._projectLabel.Name = "_projectLabel";
			this._projectLabel.Size = new System.Drawing.Size(40, 13);
			this._projectLabel.TabIndex = 10;
			this._projectLabel.Text = "Project";
			// 
			// _urlLabel
			// 
			this._urlLabel.AccessibleName = "urlLabel";
			this._urlLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._urlLabel.AutoSize = true;
			this._urlLabel.Location = new System.Drawing.Point(3, 19);
			this._urlLabel.Name = "_urlLabel";
			this._urlLabel.Size = new System.Drawing.Size(55, 13);
			this._urlLabel.TabIndex = 0;
			this._urlLabel.Text = "Slick URL";
			// 
			// _trailingSlash
			// 
			this._trailingSlash.AccessibleName = "trailingSlash";
			this._trailingSlash.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._trailingSlash.AutoSize = true;
			this._trailingSlash.Location = new System.Drawing.Point(549, 19);
			this._trailingSlash.Name = "_trailingSlash";
			this._trailingSlash.Size = new System.Drawing.Size(12, 13);
			this._trailingSlash.TabIndex = 8;
			this._trailingSlash.Text = "/";
			// 
			// _project
			// 
			this._project.AccessibleName = "project";
			this._project.AccessibleRole = System.Windows.Forms.AccessibleRole.DropList;
			this._project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._project.FormattingEnabled = true;
			this._project.Location = new System.Drawing.Point(80, 68);
			this._project.Name = "_project";
			this._project.Size = new System.Drawing.Size(269, 21);
			this._project.Sorted = true;
			this._project.TabIndex = 11;
			this._project.SelectedIndexChanged += new System.EventHandler(this.GetReleases);
			// 
			// _addProject
			// 
			this._addProject.AccessibleName = "addProject";
			this._addProject.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this._addProject.Enabled = false;
			this._addProject.Location = new System.Drawing.Point(355, 67);
			this._addProject.Name = "_addProject";
			this._addProject.Size = new System.Drawing.Size(109, 20);
			this._addProject.TabIndex = 12;
			this._addProject.Text = "Add Project";
			this._addProject.UseVisualStyleBackColor = true;
			this._addProject.Visible = false;
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
			// _releaseLabel
			// 
			this._releaseLabel.AccessibleName = "releaseLabel";
			this._releaseLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._releaseLabel.AutoSize = true;
			this._releaseLabel.Location = new System.Drawing.Point(3, 101);
			this._releaseLabel.Name = "_releaseLabel";
			this._releaseLabel.Size = new System.Drawing.Size(46, 13);
			this._releaseLabel.TabIndex = 13;
			this._releaseLabel.Text = "Release";
			// 
			// _release
			// 
			this._release.AccessibleName = "release";
			this._release.AccessibleRole = System.Windows.Forms.AccessibleRole.DropList;
			this._release.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._release.FormattingEnabled = true;
			this._release.Location = new System.Drawing.Point(80, 101);
			this._release.Name = "_release";
			this._release.Size = new System.Drawing.Size(269, 21);
			this._release.TabIndex = 14;
			// 
			// _addRelease
			// 
			this._addRelease.AccessibleName = "addRelease";
			this._addRelease.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this._addRelease.Enabled = false;
			this._addRelease.Location = new System.Drawing.Point(355, 102);
			this._addRelease.Name = "_addRelease";
			this._addRelease.Size = new System.Drawing.Size(109, 20);
			this._addRelease.TabIndex = 15;
			this._addRelease.Text = "Add Release";
			this._addRelease.UseVisualStyleBackColor = true;
			this._addRelease.Visible = false;
			// 
			// _screenshotGroup
			// 
			this._screenshotGroup.AccessibleName = "screenshotGroup";
			this._screenshotGroup.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._screenshotGroup.Controls.Add(this._posttestScreenshot);
			this._screenshotGroup.Controls.Add(this._failScreenshot);
			this._screenshotGroup.Controls.Add(this._pretestScreenshot);
			this._screenshotGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._screenshotGroup.Location = new System.Drawing.Point(80, 147);
			this._screenshotGroup.Name = "_screenshotGroup";
			this._screenshotGroup.Size = new System.Drawing.Size(159, 104);
			this._screenshotGroup.TabIndex = 16;
			this._screenshotGroup.TabStop = false;
			this._screenshotGroup.Text = "Take a screenshot";
			// 
			// _posttestScreenshot
			// 
			this._posttestScreenshot.AccessibleName = "posttestScreenshot";
			this._posttestScreenshot.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
			this._posttestScreenshot.AutoSize = true;
			this._posttestScreenshot.Location = new System.Drawing.Point(13, 50);
			this._posttestScreenshot.Name = "_posttestScreenshot";
			this._posttestScreenshot.Size = new System.Drawing.Size(94, 17);
			this._posttestScreenshot.TabIndex = 2;
			this._posttestScreenshot.Text = "after each test";
			this._posttestScreenshot.UseVisualStyleBackColor = true;
			this._posttestScreenshot.CheckedChanged += new System.EventHandler(this.CheckedChanged);
			// 
			// _failScreenshot
			// 
			this._failScreenshot.AccessibleName = "failScreenshot";
			this._failScreenshot.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
			this._failScreenshot.AutoSize = true;
			this._failScreenshot.Checked = true;
			this._failScreenshot.CheckState = System.Windows.Forms.CheckState.Checked;
			this._failScreenshot.Location = new System.Drawing.Point(13, 73);
			this._failScreenshot.Name = "_failScreenshot";
			this._failScreenshot.Size = new System.Drawing.Size(89, 17);
			this._failScreenshot.TabIndex = 1;
			this._failScreenshot.Text = "on test failure";
			this._failScreenshot.UseVisualStyleBackColor = true;
			this._failScreenshot.CheckedChanged += new System.EventHandler(this.CheckedChanged);
			// 
			// _pretestScreenshot
			// 
			this._pretestScreenshot.AccessibleName = "pretestScreenshot";
			this._pretestScreenshot.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
			this._pretestScreenshot.AutoSize = true;
			this._pretestScreenshot.Location = new System.Drawing.Point(13, 27);
			this._pretestScreenshot.Name = "_pretestScreenshot";
			this._pretestScreenshot.Size = new System.Drawing.Size(103, 17);
			this._pretestScreenshot.TabIndex = 0;
			this._pretestScreenshot.Text = "before each test";
			this._pretestScreenshot.UseVisualStyleBackColor = true;
			this._pretestScreenshot.CheckedChanged += new System.EventHandler(this.CheckedChanged);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// ConfigurationEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._screenshotGroup);
			this.Controls.Add(this._addRelease);
			this.Controls.Add(this._release);
			this.Controls.Add(this._releaseLabel);
			this.Controls.Add(this.getProjects);
			this.Controls.Add(this._addProject);
			this.Controls.Add(this._project);
			this.Controls.Add(this._trailingSlash);
			this.Controls.Add(this._urlLabel);
			this.Controls.Add(this._projectLabel);
			this.Controls.Add(this._port);
			this.Controls.Add(this._sitePathSlash);
			this.Controls.Add(this._sitePath);
			this.Controls.Add(this.portLabel);
			this.Controls.Add(this._host);
			this.Controls.Add(this._hostLabel);
			this.Controls.Add(this._scheme);
			this.Name = "ConfigurationEditor";
			this.Size = new System.Drawing.Size(670, 408);
			((System.ComponentModel.ISupportInitialize)(this._port)).EndInit();
			this._screenshotGroup.ResumeLayout(false);
			this._screenshotGroup.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label _hostLabel;
		private System.Windows.Forms.Label portLabel;
		private System.Windows.Forms.Label _sitePathSlash;
		private System.Windows.Forms.Label _projectLabel;
		private System.Windows.Forms.Label _urlLabel;
		private System.Windows.Forms.Label _trailingSlash;
		private System.Windows.Forms.Button _addProject;
		private System.Windows.Forms.Button getProjects;
		private System.Windows.Forms.ComboBox _project;
		private System.Windows.Forms.ComboBox _scheme;
		private System.Windows.Forms.TextBox _host;
		private System.Windows.Forms.NumericUpDown _port;
		private System.Windows.Forms.TextBox _sitePath;
		private System.Windows.Forms.Label _releaseLabel;
		private System.Windows.Forms.ComboBox _release;
		private System.Windows.Forms.Button _addRelease;
		private System.Windows.Forms.GroupBox _screenshotGroup;
		private System.Windows.Forms.CheckBox _posttestScreenshot;
		private System.Windows.Forms.CheckBox _failScreenshot;
		private System.Windows.Forms.CheckBox _pretestScreenshot;
		private System.Windows.Forms.ErrorProvider errorProvider1;
	}
}
