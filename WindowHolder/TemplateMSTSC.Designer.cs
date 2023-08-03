namespace WindowHolder
{
    partial class TemplateMSTSC
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbConnectionFile = new System.Windows.Forms.TextBox();
            this.tbRemoteComputer = new System.Windows.Forms.TextBox();
            this.cbAdmin = new System.Windows.Forms.CheckBox();
            this.cbRemoteComputer = new System.Windows.Forms.CheckBox();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.cbWidth = new System.Windows.Forms.CheckBox();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.cbHeight = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPublic = new System.Windows.Forms.CheckBox();
            this.cbSpan = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.ofdBrowseConnection = new System.Windows.Forms.OpenFileDialog();
            this.cbFullscreen = new System.Windows.Forms.CheckBox();
            this.cbConsole = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(344, 226);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(425, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbConnectionFile
            // 
            this.tbConnectionFile.Location = new System.Drawing.Point(128, 11);
            this.tbConnectionFile.Name = "tbConnectionFile";
            this.tbConnectionFile.Size = new System.Drawing.Size(291, 20);
            this.tbConnectionFile.TabIndex = 1;
            // 
            // tbRemoteComputer
            // 
            this.tbRemoteComputer.Enabled = false;
            this.tbRemoteComputer.Location = new System.Drawing.Point(128, 34);
            this.tbRemoteComputer.Name = "tbRemoteComputer";
            this.tbRemoteComputer.Size = new System.Drawing.Size(372, 20);
            this.tbRemoteComputer.TabIndex = 4;
            // 
            // cbAdmin
            // 
            this.cbAdmin.AutoSize = true;
            this.cbAdmin.Location = new System.Drawing.Point(12, 106);
            this.cbAdmin.Name = "cbAdmin";
            this.cbAdmin.Size = new System.Drawing.Size(120, 17);
            this.cbAdmin.TabIndex = 11;
            this.cbAdmin.Text = "&Administration mode";
            this.cbAdmin.UseVisualStyleBackColor = true;
            // 
            // cbRemoteComputer
            // 
            this.cbRemoteComputer.AutoSize = true;
            this.cbRemoteComputer.Location = new System.Drawing.Point(12, 34);
            this.cbRemoteComputer.Name = "cbRemoteComputer";
            this.cbRemoteComputer.Size = new System.Drawing.Size(113, 17);
            this.cbRemoteComputer.TabIndex = 3;
            this.cbRemoteComputer.Text = "&Remote computer:";
            this.cbRemoteComputer.UseVisualStyleBackColor = true;
            this.cbRemoteComputer.CheckedChanged += new System.EventHandler(this.cbRemoteComputer_CheckedChanged);
            // 
            // tbWidth
            // 
            this.tbWidth.Enabled = false;
            this.tbWidth.Location = new System.Drawing.Point(128, 58);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(85, 20);
            this.tbWidth.TabIndex = 6;
            // 
            // cbWidth
            // 
            this.cbWidth.AutoSize = true;
            this.cbWidth.Location = new System.Drawing.Point(12, 60);
            this.cbWidth.Name = "cbWidth";
            this.cbWidth.Size = new System.Drawing.Size(96, 17);
            this.cbWidth.TabIndex = 5;
            this.cbWidth.Text = "Window &width:";
            this.cbWidth.UseVisualStyleBackColor = true;
            this.cbWidth.CheckedChanged += new System.EventHandler(this.cbWidth_CheckedChanged);
            // 
            // tbHeight
            // 
            this.tbHeight.Enabled = false;
            this.tbHeight.Location = new System.Drawing.Point(128, 81);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(85, 20);
            this.tbHeight.TabIndex = 9;
            // 
            // cbHeight
            // 
            this.cbHeight.AutoSize = true;
            this.cbHeight.Location = new System.Drawing.Point(12, 83);
            this.cbHeight.Name = "cbHeight";
            this.cbHeight.Size = new System.Drawing.Size(100, 17);
            this.cbHeight.TabIndex = 8;
            this.cbHeight.Text = "Window &height:";
            this.cbHeight.UseVisualStyleBackColor = true;
            this.cbHeight.CheckedChanged += new System.EventHandler(this.cbHeight_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "pixels";
            // 
            // cbPublic
            // 
            this.cbPublic.AutoSize = true;
            this.cbPublic.Location = new System.Drawing.Point(12, 176);
            this.cbPublic.Name = "cbPublic";
            this.cbPublic.Size = new System.Drawing.Size(84, 17);
            this.cbPublic.TabIndex = 14;
            this.cbPublic.Text = "&Public mode";
            this.cbPublic.UseVisualStyleBackColor = true;
            // 
            // cbSpan
            // 
            this.cbSpan.AutoSize = true;
            this.cbSpan.Location = new System.Drawing.Point(12, 199);
            this.cbSpan.Name = "cbSpan";
            this.cbSpan.Size = new System.Drawing.Size(413, 17);
            this.cbSpan.TabIndex = 15;
            this.cbSpan.Text = "&Span (Matches the remote desktop width and height with the local virtual desktop" +
                ")";
            this.cbSpan.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "pixels";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Connection file:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(425, 11);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 20);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // ofdBrowseConnection
            // 
            this.ofdBrowseConnection.DefaultExt = "rdp";
            this.ofdBrowseConnection.Filter = "Remote Desktop files|*.rdp|All files|*.*";
            this.ofdBrowseConnection.Title = "Select the connection file";
            // 
            // cbFullscreen
            // 
            this.cbFullscreen.AutoSize = true;
            this.cbFullscreen.Location = new System.Drawing.Point(12, 152);
            this.cbFullscreen.Name = "cbFullscreen";
            this.cbFullscreen.Size = new System.Drawing.Size(106, 17);
            this.cbFullscreen.TabIndex = 13;
            this.cbFullscreen.Text = "&Full-screen mode";
            this.cbFullscreen.UseVisualStyleBackColor = true;
            // 
            // cbConsole
            // 
            this.cbConsole.AutoSize = true;
            this.cbConsole.Location = new System.Drawing.Point(12, 129);
            this.cbConsole.Name = "cbConsole";
            this.cbConsole.Size = new System.Drawing.Size(93, 17);
            this.cbConsole.TabIndex = 12;
            this.cbConsole.Text = "&Console mode";
            this.cbConsole.UseVisualStyleBackColor = true;
            // 
            // TemplateMSTSC
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(512, 261);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbHeight);
            this.Controls.Add(this.cbWidth);
            this.Controls.Add(this.cbRemoteComputer);
            this.Controls.Add(this.cbSpan);
            this.Controls.Add(this.cbPublic);
            this.Controls.Add(this.cbFullscreen);
            this.Controls.Add(this.cbConsole);
            this.Controls.Add(this.cbAdmin);
            this.Controls.Add(this.tbHeight);
            this.Controls.Add(this.tbWidth);
            this.Controls.Add(this.tbRemoteComputer);
            this.Controls.Add(this.tbConnectionFile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TemplateMSTSC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MSTSC Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbConnectionFile;
        private System.Windows.Forms.TextBox tbRemoteComputer;
        private System.Windows.Forms.CheckBox cbAdmin;
        private System.Windows.Forms.CheckBox cbRemoteComputer;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.CheckBox cbWidth;
        private System.Windows.Forms.TextBox tbHeight;
        private System.Windows.Forms.CheckBox cbHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbPublic;
        private System.Windows.Forms.CheckBox cbSpan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog ofdBrowseConnection;
        private System.Windows.Forms.CheckBox cbFullscreen;
        private System.Windows.Forms.CheckBox cbConsole;
    }
}