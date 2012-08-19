namespace SlickQA.DataCollector.ConfigurationEditor.View
{
	sealed partial class UrlSelector
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UrlSelector));
			this._getProjects = new System.Windows.Forms.Button();
			this._trailingSlash = new System.Windows.Forms.Label();
			this._urlLabel = new System.Windows.Forms.Label();
			this._port = new System.Windows.Forms.NumericUpDown();
			this._sitePathSlash = new System.Windows.Forms.Label();
			this._sitePath = new System.Windows.Forms.TextBox();
			this.portLabel = new System.Windows.Forms.Label();
			this._host = new System.Windows.Forms.TextBox();
			this._hostLabel = new System.Windows.Forms.Label();
			this._scheme = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this._port)).BeginInit();
			this.SuspendLayout();
			// 
			// _getProjects
			// 
			resources.ApplyResources(this._getProjects, "_getProjects");
			this._getProjects.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this._getProjects.Name = "_getProjects";
			this._getProjects.UseVisualStyleBackColor = true;
			this._getProjects.Click += new System.EventHandler(this.GetProjectsClick);
			// 
			// _trailingSlash
			// 
			resources.ApplyResources(this._trailingSlash, "_trailingSlash");
			this._trailingSlash.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._trailingSlash.Name = "_trailingSlash";
			// 
			// _urlLabel
			// 
			resources.ApplyResources(this._urlLabel, "_urlLabel");
			this._urlLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._urlLabel.Name = "_urlLabel";
			// 
			// _port
			// 
			resources.ApplyResources(this._port, "_port");
			this._port.AccessibleRole = System.Windows.Forms.AccessibleRole.Dial;
			this._port.Maximum = new decimal(new int[] {
			65535,
			0,
			0,
			0});
			this._port.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this._port.Name = "_port";
			this._port.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this._port.ValueChanged += new System.EventHandler(this.PortValueChanged);
			// 
			// _sitePathSlash
			// 
			resources.ApplyResources(this._sitePathSlash, "_sitePathSlash");
			this._sitePathSlash.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._sitePathSlash.Name = "_sitePathSlash";
			// 
			// _sitePath
			// 
			resources.ApplyResources(this._sitePath, "_sitePath");
			this._sitePath.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this._sitePath.Name = "_sitePath";
			this._sitePath.TextChanged += new System.EventHandler(this.SitePathTextChanged);
			// 
			// portLabel
			// 
			resources.ApplyResources(this.portLabel, "portLabel");
			this.portLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this.portLabel.Name = "portLabel";
			// 
			// _host
			// 
			resources.ApplyResources(this._host, "_host");
			this._host.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this._host.Name = "_host";
			this._host.Leave += new System.EventHandler(this.HostLeave);
			// 
			// _hostLabel
			// 
			resources.ApplyResources(this._hostLabel, "_hostLabel");
			this._hostLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
			this._hostLabel.Name = "_hostLabel";
			// 
			// _scheme
			// 
			resources.ApplyResources(this._scheme, "_scheme");
			this._scheme.AccessibleRole = System.Windows.Forms.AccessibleRole.DropList;
			this._scheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._scheme.FormattingEnabled = true;
			this._scheme.Name = "_scheme";
			this._scheme.SelectedIndexChanged += new System.EventHandler(this.SchemeSelectedIndexChanged);
			// 
			// UrlSelector
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this._getProjects);
			this.Controls.Add(this._trailingSlash);
			this.Controls.Add(this._urlLabel);
			this.Controls.Add(this._port);
			this.Controls.Add(this._sitePathSlash);
			this.Controls.Add(this._sitePath);
			this.Controls.Add(this.portLabel);
			this.Controls.Add(this._host);
			this.Controls.Add(this._hostLabel);
			this.Controls.Add(this._scheme);
			this.Name = "UrlSelector";
			this.Load += new System.EventHandler(this.UrlSelectorLoad);
			((System.ComponentModel.ISupportInitialize)(this._port)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _getProjects;
		private System.Windows.Forms.Label _trailingSlash;
		private System.Windows.Forms.Label _urlLabel;
		private System.Windows.Forms.NumericUpDown _port;
		private System.Windows.Forms.Label _sitePathSlash;
		private System.Windows.Forms.TextBox _sitePath;
		private System.Windows.Forms.Label portLabel;
		private System.Windows.Forms.TextBox _host;
		private System.Windows.Forms.Label _hostLabel;
		private System.Windows.Forms.ComboBox _scheme;

	}
}
