namespace WindowHolder
{
    partial class TemplatePuTTY
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
            this.rbLoadSaved = new System.Windows.Forms.RadioButton();
            this.tbSaved = new System.Windows.Forms.TextBox();
            this.rbStartNew = new System.Windows.Forms.RadioButton();
            this.tbNew = new System.Windows.Forms.TextBox();
            this.rbSSH = new System.Windows.Forms.RadioButton();
            this.rbTelnet = new System.Windows.Forms.RadioButton();
            this.rbRlogin = new System.Windows.Forms.RadioButton();
            this.rbRaw = new System.Windows.Forms.RadioButton();
            this.cbVerbose = new System.Windows.Forms.CheckBox();
            this.cbUsername = new System.Windows.Forms.CheckBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbForwardLocal = new System.Windows.Forms.TextBox();
            this.cbForwardLocal = new System.Windows.Forms.CheckBox();
            this.tbForwardRemote = new System.Windows.Forms.TextBox();
            this.cbForwardRemote = new System.Windows.Forms.CheckBox();
            this.cbRemoteCommand = new System.Windows.Forms.CheckBox();
            this.tbRemoteCommand = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.cbPort = new System.Windows.Forms.CheckBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.cbPassword = new System.Windows.Forms.CheckBox();
            this.cbCompression = new System.Windows.Forms.CheckBox();
            this.cbAgentForwarding = new System.Windows.Forms.CheckBox();
            this.rbAgentOn = new System.Windows.Forms.RadioButton();
            this.rbAgentOff = new System.Windows.Forms.RadioButton();
            this.rbX11On = new System.Windows.Forms.RadioButton();
            this.rbX11Off = new System.Windows.Forms.RadioButton();
            this.cbX11Forwarding = new System.Windows.Forms.CheckBox();
            this.rbTerminalOn = new System.Windows.Forms.RadioButton();
            this.rbTerminalOff = new System.Windows.Forms.RadioButton();
            this.cbPseudoTerminal = new System.Windows.Forms.CheckBox();
            this.tbPrivateKey = new System.Windows.Forms.TextBox();
            this.cbPrivateKey = new System.Windows.Forms.CheckBox();
            this.rbSSH1 = new System.Windows.Forms.RadioButton();
            this.rbSSH2 = new System.Windows.Forms.RadioButton();
            this.cbSSHLevel = new System.Windows.Forms.CheckBox();
            this.cbType = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(262, 380);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 37;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(343, 380);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // rbLoadSaved
            // 
            this.rbLoadSaved.AutoCheck = false;
            this.rbLoadSaved.AutoSize = true;
            this.rbLoadSaved.Location = new System.Drawing.Point(12, 12);
            this.rbLoadSaved.Name = "rbLoadSaved";
            this.rbLoadSaved.Size = new System.Drawing.Size(90, 17);
            this.rbLoadSaved.TabIndex = 0;
            this.rbLoadSaved.Text = "&Load session:";
            this.rbLoadSaved.UseVisualStyleBackColor = true;
            this.rbLoadSaved.CheckedChanged += new System.EventHandler(this.rbLoadSaved_CheckedChanged);
            this.rbLoadSaved.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // tbSaved
            // 
            this.tbSaved.Enabled = false;
            this.tbSaved.Location = new System.Drawing.Point(128, 11);
            this.tbSaved.Name = "tbSaved";
            this.tbSaved.Size = new System.Drawing.Size(278, 20);
            this.tbSaved.TabIndex = 1;
            // 
            // rbStartNew
            // 
            this.rbStartNew.AutoCheck = false;
            this.rbStartNew.AutoSize = true;
            this.rbStartNew.Checked = true;
            this.rbStartNew.Location = new System.Drawing.Point(12, 35);
            this.rbStartNew.Name = "rbStartNew";
            this.rbStartNew.Size = new System.Drawing.Size(111, 17);
            this.rbStartNew.TabIndex = 2;
            this.rbStartNew.Text = "&Start new session:";
            this.rbStartNew.UseVisualStyleBackColor = true;
            this.rbStartNew.CheckedChanged += new System.EventHandler(this.rbStartNew_CheckedChanged);
            this.rbStartNew.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // tbNew
            // 
            this.tbNew.Location = new System.Drawing.Point(128, 34);
            this.tbNew.Name = "tbNew";
            this.tbNew.Size = new System.Drawing.Size(278, 20);
            this.tbNew.TabIndex = 3;
            // 
            // rbSSH
            // 
            this.rbSSH.AutoCheck = false;
            this.rbSSH.AutoSize = true;
            this.rbSSH.Checked = true;
            this.rbSSH.Enabled = false;
            this.rbSSH.Location = new System.Drawing.Point(128, 58);
            this.rbSSH.Name = "rbSSH";
            this.rbSSH.Size = new System.Drawing.Size(47, 17);
            this.rbSSH.TabIndex = 5;
            this.rbSSH.Text = "SS&H";
            this.rbSSH.UseVisualStyleBackColor = true;
            this.rbSSH.CheckedChanged += new System.EventHandler(this.rbSSH_CheckedChanged);
            this.rbSSH.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // rbTelnet
            // 
            this.rbTelnet.AutoCheck = false;
            this.rbTelnet.AutoSize = true;
            this.rbTelnet.Enabled = false;
            this.rbTelnet.Location = new System.Drawing.Point(181, 58);
            this.rbTelnet.Name = "rbTelnet";
            this.rbTelnet.Size = new System.Drawing.Size(55, 17);
            this.rbTelnet.TabIndex = 6;
            this.rbTelnet.Text = "&Telnet";
            this.rbTelnet.UseVisualStyleBackColor = true;
            this.rbTelnet.CheckedChanged += new System.EventHandler(this.rbTelnet_CheckedChanged);
            this.rbTelnet.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // rbRlogin
            // 
            this.rbRlogin.AutoCheck = false;
            this.rbRlogin.AutoSize = true;
            this.rbRlogin.Enabled = false;
            this.rbRlogin.Location = new System.Drawing.Point(242, 58);
            this.rbRlogin.Name = "rbRlogin";
            this.rbRlogin.Size = new System.Drawing.Size(55, 17);
            this.rbRlogin.TabIndex = 7;
            this.rbRlogin.Text = "&Rlogin";
            this.rbRlogin.UseVisualStyleBackColor = true;
            this.rbRlogin.CheckedChanged += new System.EventHandler(this.rbRlogin_CheckedChanged);
            this.rbRlogin.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // rbRaw
            // 
            this.rbRaw.AutoCheck = false;
            this.rbRaw.AutoSize = true;
            this.rbRaw.Enabled = false;
            this.rbRaw.Location = new System.Drawing.Point(303, 58);
            this.rbRaw.Name = "rbRaw";
            this.rbRaw.Size = new System.Drawing.Size(47, 17);
            this.rbRaw.TabIndex = 8;
            this.rbRaw.Text = "Ra&w";
            this.rbRaw.UseVisualStyleBackColor = true;
            this.rbRaw.CheckedChanged += new System.EventHandler(this.rbRaw_CheckedChanged);
            this.rbRaw.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // cbVerbose
            // 
            this.cbVerbose.AutoSize = true;
            this.cbVerbose.Location = new System.Drawing.Point(12, 83);
            this.cbVerbose.Name = "cbVerbose";
            this.cbVerbose.Size = new System.Drawing.Size(65, 17);
            this.cbVerbose.TabIndex = 9;
            this.cbVerbose.Text = "&Verbose";
            this.cbVerbose.UseVisualStyleBackColor = true;
            // 
            // cbUsername
            // 
            this.cbUsername.AutoSize = true;
            this.cbUsername.Location = new System.Drawing.Point(12, 128);
            this.cbUsername.Name = "cbUsername";
            this.cbUsername.Size = new System.Drawing.Size(80, 17);
            this.cbUsername.TabIndex = 11;
            this.cbUsername.Text = "&User name:";
            this.cbUsername.UseVisualStyleBackColor = true;
            this.cbUsername.CheckedChanged += new System.EventHandler(this.cbUsername_CheckedChanged);
            // 
            // tbUsername
            // 
            this.tbUsername.Enabled = false;
            this.tbUsername.Location = new System.Drawing.Point(128, 126);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(290, 20);
            this.tbUsername.TabIndex = 12;
            // 
            // tbForwardLocal
            // 
            this.tbForwardLocal.Enabled = false;
            this.tbForwardLocal.Location = new System.Drawing.Point(128, 149);
            this.tbForwardLocal.Name = "tbForwardLocal";
            this.tbForwardLocal.Size = new System.Drawing.Size(290, 20);
            this.tbForwardLocal.TabIndex = 14;
            // 
            // cbForwardLocal
            // 
            this.cbForwardLocal.AutoSize = true;
            this.cbForwardLocal.Location = new System.Drawing.Point(12, 151);
            this.cbForwardLocal.Name = "cbForwardLocal";
            this.cbForwardLocal.Size = new System.Drawing.Size(92, 17);
            this.cbForwardLocal.TabIndex = 13;
            this.cbForwardLocal.Text = "&Forward local:";
            this.cbForwardLocal.UseVisualStyleBackColor = true;
            this.cbForwardLocal.CheckedChanged += new System.EventHandler(this.cbForwardLocal_CheckedChanged);
            // 
            // tbForwardRemote
            // 
            this.tbForwardRemote.Enabled = false;
            this.tbForwardRemote.Location = new System.Drawing.Point(128, 172);
            this.tbForwardRemote.Name = "tbForwardRemote";
            this.tbForwardRemote.Size = new System.Drawing.Size(290, 20);
            this.tbForwardRemote.TabIndex = 16;
            // 
            // cbForwardRemote
            // 
            this.cbForwardRemote.AutoSize = true;
            this.cbForwardRemote.Location = new System.Drawing.Point(12, 174);
            this.cbForwardRemote.Name = "cbForwardRemote";
            this.cbForwardRemote.Size = new System.Drawing.Size(102, 17);
            this.cbForwardRemote.TabIndex = 15;
            this.cbForwardRemote.Text = "Forw&ard remote:";
            this.cbForwardRemote.UseVisualStyleBackColor = true;
            this.cbForwardRemote.CheckedChanged += new System.EventHandler(this.cbForwardRemote_CheckedChanged);
            // 
            // cbRemoteCommand
            // 
            this.cbRemoteCommand.AutoSize = true;
            this.cbRemoteCommand.Location = new System.Drawing.Point(12, 197);
            this.cbRemoteCommand.Name = "cbRemoteCommand";
            this.cbRemoteCommand.Size = new System.Drawing.Size(115, 17);
            this.cbRemoteCommand.TabIndex = 17;
            this.cbRemoteCommand.Text = "Remote command:";
            this.cbRemoteCommand.UseVisualStyleBackColor = true;
            this.cbRemoteCommand.CheckedChanged += new System.EventHandler(this.cbRemoteCommand_CheckedChanged);
            // 
            // tbRemoteCommand
            // 
            this.tbRemoteCommand.Enabled = false;
            this.tbRemoteCommand.Location = new System.Drawing.Point(128, 195);
            this.tbRemoteCommand.Name = "tbRemoteCommand";
            this.tbRemoteCommand.Size = new System.Drawing.Size(290, 20);
            this.tbRemoteCommand.TabIndex = 18;
            // 
            // tbPort
            // 
            this.tbPort.Enabled = false;
            this.tbPort.Location = new System.Drawing.Point(128, 218);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(290, 20);
            this.tbPort.TabIndex = 20;
            // 
            // cbPort
            // 
            this.cbPort.AutoSize = true;
            this.cbPort.Location = new System.Drawing.Point(12, 220);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(48, 17);
            this.cbPort.TabIndex = 19;
            this.cbPort.Text = "&Port:";
            this.cbPort.UseVisualStyleBackColor = true;
            this.cbPort.CheckedChanged += new System.EventHandler(this.cbPort_CheckedChanged);
            // 
            // tbPassword
            // 
            this.tbPassword.Enabled = false;
            this.tbPassword.Location = new System.Drawing.Point(128, 241);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(290, 20);
            this.tbPassword.TabIndex = 22;
            // 
            // cbPassword
            // 
            this.cbPassword.AutoSize = true;
            this.cbPassword.Location = new System.Drawing.Point(12, 243);
            this.cbPassword.Name = "cbPassword";
            this.cbPassword.Size = new System.Drawing.Size(75, 17);
            this.cbPassword.TabIndex = 21;
            this.cbPassword.Text = "Password:";
            this.cbPassword.UseVisualStyleBackColor = true;
            this.cbPassword.CheckedChanged += new System.EventHandler(this.cbPassword_CheckedChanged);
            // 
            // cbCompression
            // 
            this.cbCompression.AutoSize = true;
            this.cbCompression.Location = new System.Drawing.Point(12, 105);
            this.cbCompression.Name = "cbCompression";
            this.cbCompression.Size = new System.Drawing.Size(86, 17);
            this.cbCompression.TabIndex = 10;
            this.cbCompression.Text = "Compression";
            this.cbCompression.UseVisualStyleBackColor = true;
            // 
            // cbAgentForwarding
            // 
            this.cbAgentForwarding.AutoSize = true;
            this.cbAgentForwarding.Location = new System.Drawing.Point(12, 288);
            this.cbAgentForwarding.Name = "cbAgentForwarding";
            this.cbAgentForwarding.Size = new System.Drawing.Size(109, 17);
            this.cbAgentForwarding.TabIndex = 25;
            this.cbAgentForwarding.Text = "Agent forwarding:";
            this.cbAgentForwarding.UseVisualStyleBackColor = true;
            this.cbAgentForwarding.CheckedChanged += new System.EventHandler(this.cbAgentForwarding_CheckedChanged);
            // 
            // rbAgentOn
            // 
            this.rbAgentOn.AutoCheck = false;
            this.rbAgentOn.AutoSize = true;
            this.rbAgentOn.Checked = true;
            this.rbAgentOn.Enabled = false;
            this.rbAgentOn.Location = new System.Drawing.Point(162, 288);
            this.rbAgentOn.Name = "rbAgentOn";
            this.rbAgentOn.Size = new System.Drawing.Size(39, 17);
            this.rbAgentOn.TabIndex = 26;
            this.rbAgentOn.Text = "On";
            this.rbAgentOn.UseVisualStyleBackColor = true;
            this.rbAgentOn.CheckedChanged += new System.EventHandler(this.rbAgentOn_CheckedChanged);
            this.rbAgentOn.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // rbAgentOff
            // 
            this.rbAgentOff.AutoCheck = false;
            this.rbAgentOff.AutoSize = true;
            this.rbAgentOff.Enabled = false;
            this.rbAgentOff.Location = new System.Drawing.Point(248, 288);
            this.rbAgentOff.Name = "rbAgentOff";
            this.rbAgentOff.Size = new System.Drawing.Size(39, 17);
            this.rbAgentOff.TabIndex = 27;
            this.rbAgentOff.Text = "Off";
            this.rbAgentOff.UseVisualStyleBackColor = true;
            this.rbAgentOff.CheckedChanged += new System.EventHandler(this.rbAgentOff_CheckedChanged);
            this.rbAgentOff.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // rbX11On
            // 
            this.rbX11On.AutoCheck = false;
            this.rbX11On.AutoSize = true;
            this.rbX11On.Checked = true;
            this.rbX11On.Enabled = false;
            this.rbX11On.Location = new System.Drawing.Point(162, 310);
            this.rbX11On.Name = "rbX11On";
            this.rbX11On.Size = new System.Drawing.Size(39, 17);
            this.rbX11On.TabIndex = 29;
            this.rbX11On.Text = "On";
            this.rbX11On.UseVisualStyleBackColor = true;
            this.rbX11On.CheckedChanged += new System.EventHandler(this.rbX11On_CheckedChanged);
            this.rbX11On.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // rbX11Off
            // 
            this.rbX11Off.AutoCheck = false;
            this.rbX11Off.AutoSize = true;
            this.rbX11Off.Enabled = false;
            this.rbX11Off.Location = new System.Drawing.Point(248, 310);
            this.rbX11Off.Name = "rbX11Off";
            this.rbX11Off.Size = new System.Drawing.Size(39, 17);
            this.rbX11Off.TabIndex = 30;
            this.rbX11Off.Text = "Off";
            this.rbX11Off.UseVisualStyleBackColor = true;
            this.rbX11Off.CheckedChanged += new System.EventHandler(this.rbX11Off_CheckedChanged);
            this.rbX11Off.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // cbX11Forwarding
            // 
            this.cbX11Forwarding.AutoSize = true;
            this.cbX11Forwarding.Location = new System.Drawing.Point(12, 310);
            this.cbX11Forwarding.Name = "cbX11Forwarding";
            this.cbX11Forwarding.Size = new System.Drawing.Size(100, 17);
            this.cbX11Forwarding.TabIndex = 28;
            this.cbX11Forwarding.Text = "X11 forwarding:";
            this.cbX11Forwarding.UseVisualStyleBackColor = true;
            this.cbX11Forwarding.CheckedChanged += new System.EventHandler(this.cbX11Forwarding_CheckedChanged);
            // 
            // rbTerminalOn
            // 
            this.rbTerminalOn.AutoCheck = false;
            this.rbTerminalOn.AutoSize = true;
            this.rbTerminalOn.Checked = true;
            this.rbTerminalOn.Enabled = false;
            this.rbTerminalOn.Location = new System.Drawing.Point(162, 332);
            this.rbTerminalOn.Name = "rbTerminalOn";
            this.rbTerminalOn.Size = new System.Drawing.Size(39, 17);
            this.rbTerminalOn.TabIndex = 32;
            this.rbTerminalOn.Text = "On";
            this.rbTerminalOn.UseVisualStyleBackColor = true;
            this.rbTerminalOn.CheckedChanged += new System.EventHandler(this.rbTerminalOn_CheckedChanged);
            this.rbTerminalOn.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // rbTerminalOff
            // 
            this.rbTerminalOff.AutoCheck = false;
            this.rbTerminalOff.AutoSize = true;
            this.rbTerminalOff.Enabled = false;
            this.rbTerminalOff.Location = new System.Drawing.Point(248, 332);
            this.rbTerminalOff.Name = "rbTerminalOff";
            this.rbTerminalOff.Size = new System.Drawing.Size(39, 17);
            this.rbTerminalOff.TabIndex = 33;
            this.rbTerminalOff.Text = "Off";
            this.rbTerminalOff.UseVisualStyleBackColor = true;
            this.rbTerminalOff.CheckedChanged += new System.EventHandler(this.rbTerminalOff_CheckedChanged);
            this.rbTerminalOff.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // cbPseudoTerminal
            // 
            this.cbPseudoTerminal.AutoSize = true;
            this.cbPseudoTerminal.Location = new System.Drawing.Point(12, 332);
            this.cbPseudoTerminal.Name = "cbPseudoTerminal";
            this.cbPseudoTerminal.Size = new System.Drawing.Size(146, 17);
            this.cbPseudoTerminal.TabIndex = 31;
            this.cbPseudoTerminal.Text = "Request pseudo-terminal:";
            this.cbPseudoTerminal.UseVisualStyleBackColor = true;
            this.cbPseudoTerminal.CheckedChanged += new System.EventHandler(this.cbPseudoTerminal_CheckedChanged);
            // 
            // tbPrivateKey
            // 
            this.tbPrivateKey.Enabled = false;
            this.tbPrivateKey.Location = new System.Drawing.Point(128, 264);
            this.tbPrivateKey.Name = "tbPrivateKey";
            this.tbPrivateKey.Size = new System.Drawing.Size(290, 20);
            this.tbPrivateKey.TabIndex = 24;
            // 
            // cbPrivateKey
            // 
            this.cbPrivateKey.AutoSize = true;
            this.cbPrivateKey.Location = new System.Drawing.Point(12, 266);
            this.cbPrivateKey.Name = "cbPrivateKey";
            this.cbPrivateKey.Size = new System.Drawing.Size(82, 17);
            this.cbPrivateKey.TabIndex = 23;
            this.cbPrivateKey.Text = "Private key:";
            this.cbPrivateKey.UseVisualStyleBackColor = true;
            this.cbPrivateKey.CheckedChanged += new System.EventHandler(this.cbPrivateKey_CheckedChanged);
            // 
            // rbSSH1
            // 
            this.rbSSH1.AutoCheck = false;
            this.rbSSH1.AutoSize = true;
            this.rbSSH1.Checked = true;
            this.rbSSH1.Enabled = false;
            this.rbSSH1.Location = new System.Drawing.Point(162, 354);
            this.rbSSH1.Name = "rbSSH1";
            this.rbSSH1.Size = new System.Drawing.Size(78, 17);
            this.rbSSH1.TabIndex = 35;
            this.rbSSH1.TabStop = true;
            this.rbSSH1.Text = "SSH-1 only";
            this.rbSSH1.UseVisualStyleBackColor = true;
            this.rbSSH1.CheckedChanged += new System.EventHandler(this.rbSSH1_CheckedChanged);
            this.rbSSH1.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // rbSSH2
            // 
            this.rbSSH2.AutoCheck = false;
            this.rbSSH2.AutoSize = true;
            this.rbSSH2.Enabled = false;
            this.rbSSH2.Location = new System.Drawing.Point(248, 354);
            this.rbSSH2.Name = "rbSSH2";
            this.rbSSH2.Size = new System.Drawing.Size(78, 17);
            this.rbSSH2.TabIndex = 36;
            this.rbSSH2.Text = "SSH-2 only";
            this.rbSSH2.UseVisualStyleBackColor = true;
            this.rbSSH2.CheckedChanged += new System.EventHandler(this.rbSSH2_CheckedChanged);
            this.rbSSH2.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // cbSSHLevel
            // 
            this.cbSSHLevel.AutoSize = true;
            this.cbSSHLevel.Location = new System.Drawing.Point(12, 354);
            this.cbSSHLevel.Name = "cbSSHLevel";
            this.cbSSHLevel.Size = new System.Drawing.Size(76, 17);
            this.cbSSHLevel.TabIndex = 34;
            this.cbSSHLevel.Text = "SSH level:";
            this.cbSSHLevel.UseVisualStyleBackColor = true;
            this.cbSSHLevel.CheckedChanged += new System.EventHandler(this.cbSSHLevel_CheckedChanged);
            // 
            // cbType
            // 
            this.cbType.AutoSize = true;
            this.cbType.Location = new System.Drawing.Point(12, 58);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(50, 17);
            this.cbType.TabIndex = 4;
            this.cbType.Text = "&Type";
            this.cbType.UseVisualStyleBackColor = true;
            this.cbType.CheckedChanged += new System.EventHandler(this.cbType_CheckedChanged);
            // 
            // TemplatePuTTY
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(430, 415);
            this.Controls.Add(this.cbPrivateKey);
            this.Controls.Add(this.cbPassword);
            this.Controls.Add(this.cbPort);
            this.Controls.Add(this.cbRemoteCommand);
            this.Controls.Add(this.cbForwardRemote);
            this.Controls.Add(this.cbForwardLocal);
            this.Controls.Add(this.cbUsername);
            this.Controls.Add(this.tbPrivateKey);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.cbSSHLevel);
            this.Controls.Add(this.cbPseudoTerminal);
            this.Controls.Add(this.cbX11Forwarding);
            this.Controls.Add(this.cbAgentForwarding);
            this.Controls.Add(this.cbCompression);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.cbVerbose);
            this.Controls.Add(this.tbRemoteCommand);
            this.Controls.Add(this.tbForwardRemote);
            this.Controls.Add(this.tbForwardLocal);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.tbNew);
            this.Controls.Add(this.tbSaved);
            this.Controls.Add(this.rbStartNew);
            this.Controls.Add(this.rbSSH2);
            this.Controls.Add(this.rbRaw);
            this.Controls.Add(this.rbTerminalOff);
            this.Controls.Add(this.rbRlogin);
            this.Controls.Add(this.rbSSH1);
            this.Controls.Add(this.rbX11Off);
            this.Controls.Add(this.rbTerminalOn);
            this.Controls.Add(this.rbTelnet);
            this.Controls.Add(this.rbX11On);
            this.Controls.Add(this.rbAgentOff);
            this.Controls.Add(this.rbAgentOn);
            this.Controls.Add(this.rbSSH);
            this.Controls.Add(this.rbLoadSaved);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TemplatePuTTY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PuTTY Options";
            this.Load += new System.EventHandler(this.TemplatePuTTY_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbLoadSaved;
        private System.Windows.Forms.TextBox tbSaved;
        private System.Windows.Forms.RadioButton rbStartNew;
        private System.Windows.Forms.TextBox tbNew;
        private System.Windows.Forms.RadioButton rbSSH;
        private System.Windows.Forms.RadioButton rbTelnet;
        private System.Windows.Forms.RadioButton rbRlogin;
        private System.Windows.Forms.RadioButton rbRaw;
        private System.Windows.Forms.CheckBox cbVerbose;
        private System.Windows.Forms.CheckBox cbUsername;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbForwardLocal;
        private System.Windows.Forms.CheckBox cbForwardLocal;
        private System.Windows.Forms.TextBox tbForwardRemote;
        private System.Windows.Forms.CheckBox cbForwardRemote;
        private System.Windows.Forms.CheckBox cbRemoteCommand;
        private System.Windows.Forms.TextBox tbRemoteCommand;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.CheckBox cbPort;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.CheckBox cbPassword;
        private System.Windows.Forms.CheckBox cbCompression;
        private System.Windows.Forms.CheckBox cbAgentForwarding;
        private System.Windows.Forms.RadioButton rbAgentOn;
        private System.Windows.Forms.RadioButton rbAgentOff;
        private System.Windows.Forms.RadioButton rbX11On;
        private System.Windows.Forms.RadioButton rbX11Off;
        private System.Windows.Forms.CheckBox cbX11Forwarding;
        private System.Windows.Forms.RadioButton rbTerminalOn;
        private System.Windows.Forms.RadioButton rbTerminalOff;
        private System.Windows.Forms.CheckBox cbPseudoTerminal;
        private System.Windows.Forms.TextBox tbPrivateKey;
        private System.Windows.Forms.CheckBox cbPrivateKey;
        private System.Windows.Forms.RadioButton rbSSH1;
        private System.Windows.Forms.RadioButton rbSSH2;
        private System.Windows.Forms.CheckBox cbSSHLevel;
        private System.Windows.Forms.CheckBox cbType;
    }
}