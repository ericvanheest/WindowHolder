using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowHolder
{
    public partial class TemplatePuTTY : TemplateBase
    {
        private string m_sCommandLine;
        private string m_sUnknownParameters;

        public TemplatePuTTY()
        {
            InitializeComponent();
        }

        public override string CommandLine
        {
            get
            {
                CreateCommandLine();
                return m_sCommandLine;
            }

            set
            {
                m_sCommandLine = value;
                ParseCommandLine();
            }
        }

        private void CreateCommandLine()
        {
            StringBuilder sb = new StringBuilder();
            string strSpace = "";
            if (tbNew.Text != "")
            {
                sb.Append(tbNew.Text);
                strSpace = " ";
            }
            sb.Append(AppendOption(ref strSpace, "-load", rbLoadSaved, tbSaved));
            sb.Append(AppendOption(ref strSpace, "-v", cbVerbose));
            sb.Append(AppendOption(ref strSpace, "-C", cbCompression));
            sb.Append(AppendOption(ref strSpace, "-l", cbUsername, tbUsername));
            sb.Append(AppendOption(ref strSpace, "-L", cbForwardLocal, tbForwardLocal));
            sb.Append(AppendOption(ref strSpace, "-R", cbForwardRemote, tbForwardRemote));
            sb.Append(AppendOption(ref strSpace, "-m", cbRemoteCommand, tbRemoteCommand));
            sb.Append(AppendOption(ref strSpace, "-P", cbPort, tbPort));
            sb.Append(AppendOption(ref strSpace, "-pw", cbPassword, tbPassword));
            sb.Append(AppendOption(ref strSpace, "-i", cbPrivateKey, tbPrivateKey));
            sb.Append(AppendOption(ref strSpace, "-ssh", cbType, rbSSH));
            sb.Append(AppendOption(ref strSpace, "-telnet", cbType, rbTelnet));
            sb.Append(AppendOption(ref strSpace, "-rlogin", cbType, rbRlogin));
            sb.Append(AppendOption(ref strSpace, "-raw", cbType, rbRaw));
            sb.Append(AppendOption(ref strSpace, "-a", cbAgentForwarding, rbAgentOff));
            sb.Append(AppendOption(ref strSpace, "-A", cbAgentForwarding, rbAgentOn));
            sb.Append(AppendOption(ref strSpace, "-x", cbX11Forwarding, rbX11Off));
            sb.Append(AppendOption(ref strSpace, "-X", cbX11Forwarding, rbX11On));
            sb.Append(AppendOption(ref strSpace, "-t", cbPseudoTerminal, rbTerminalOff));
            sb.Append(AppendOption(ref strSpace, "-T", cbPseudoTerminal, rbTerminalOn));
            sb.Append(AppendOption(ref strSpace, "-1", cbSSHLevel, rbSSH1));
            sb.Append(AppendOption(ref strSpace, "-2", cbSSHLevel, rbSSH2));
            sb.Append(strSpace + m_sUnknownParameters);
            m_sCommandLine = sb.ToString();
        }

        private void ParseCommandLine()
        {
            StringBuilder sbUnknown = new StringBuilder();
            string strSpace = "";

            if (m_sCommandLine.StartsWith("@"))
            {
                tbNew.Text = m_sCommandLine.Substring(1);
                return;
            }
            else
            {
                string[] args = Utility.SplitArgsWithQuotes(m_sCommandLine);
                int iMainArg = 0;
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i][0] == '-' || args[i][0] == '/')
                    {
                        string sArg = args[i].Substring(1);
                        switch (sArg)
                        {
                            case "ssh":
                                cbType.Checked = true;
                                rbSSH.Checked = true;
                                break;
                            case "telnet":
                                cbType.Checked = true;
                                rbTelnet.Checked = true;
                                break;
                            case "rlogin":
                                cbType.Checked = true;
                                rbRlogin.Checked = true;
                                break;
                            case "raw":
                                cbType.Checked = true;
                                rbRaw.Checked = true;
                                break;
                            case "load":
                                rbLoadSaved.Checked = true;
                                i++;
                                if (i < args.Length)
                                    tbSaved.Text = args[i];
                                break;
                            case "v":
                                cbVerbose.Checked = true;
                                break;
                            case "l":
                                cbUsername.Checked = true;
                                i++;
                                if (i < args.Length)
                                    tbUsername.Text = args[i];
                                break;
                            case "L":
                                cbForwardLocal.Checked = true;
                                i++;
                                if (i < args.Length)
                                    tbForwardLocal.Text = args[i];
                                break;
                            case "R":
                                cbForwardRemote.Checked = true;
                                i++;
                                if (i < args.Length)
                                    tbForwardRemote.Text = args[i];
                                break;
                            case "m":
                                cbRemoteCommand.Checked = true;
                                i++;
                                if (i < args.Length)
                                    tbRemoteCommand.Text = args[i];
                                break;
                            case "P":
                                cbPort.Checked = true;
                                i++;
                                if (i < args.Length)
                                    tbPort.Text = args[i];
                                break;
                            case "pw":
                                cbPassword.Checked = true;
                                i++;
                                if (i < args.Length)
                                    tbPassword.Text = args[i];
                                break;
                            case "a":
                                cbAgentForwarding.Checked = true;
                                rbAgentOff.Checked = true;
                                break;
                            case "A":
                                cbAgentForwarding.Checked = true;
                                rbAgentOn.Checked = true;
                                break;
                            case "x":
                                cbX11Forwarding.Checked = true;
                                rbX11Off.Checked = true;
                                break;
                            case "X":
                                cbX11Forwarding.Checked = true;
                                rbX11On.Checked = true;
                                break;
                            case "t":
                                cbPseudoTerminal.Checked = true;
                                rbTerminalOff.Checked = true;
                                break;
                            case "T":
                                cbPseudoTerminal.Checked = true;
                                rbTerminalOn.Checked = true;
                                break;
                            case "C":
                                cbCompression.Checked = true;
                                break;
                            case "1":
                                cbSSHLevel.Checked = true;
                                rbSSH1.Checked = true;
                                break;
                            case "2":
                                cbSSHLevel.Checked = true;
                                rbSSH2.Checked = true;
                                break;
                            case "i":
                                cbPrivateKey.Checked = true;
                                i++;
                                if (i < args.Length)
                                    tbPrivateKey.Text = args[i];
                                break;
                            default:
                                sbUnknown.Append(strSpace);
                                strSpace = " ";
                                sbUnknown.Append(args[i]);
                                break;
                        }
                    }
                    else
                    {
                        switch (iMainArg)
                        {
                            case 0:
                                tbNew.Text = args[i];
                                break;
                            default:
                                sbUnknown.Append(strSpace);
                                strSpace = " ";
                                sbUnknown.Append(args[i]);
                                break;
                        }
                        iMainArg++;
                    }
                }
            }
            m_sUnknownParameters = sbUnknown.ToString();
        }

        private void rbLoadSaved_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLoadSaved.Checked)
                rbStartNew.Checked = !rbLoadSaved.Checked;

            tbSaved.Enabled = rbLoadSaved.Checked;
        }

        private void rbStartNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStartNew.Checked)
                rbLoadSaved.Checked = !rbStartNew.Checked;

            tbNew.Enabled = rbStartNew.Checked;
        }

        private void rbSSH_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSSH.Checked)
            {
                rbTelnet.Checked = false;
                rbRlogin.Checked = false;
                rbRaw.Checked = false;
            }
        }

        private void rbTelnet_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTelnet.Checked)
            {
                rbSSH.Checked = false;
                rbRlogin.Checked = false;
                rbRaw.Checked = false;
            }
        }

        private void rbRlogin_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRlogin.Checked)
            {
                rbTelnet.Checked = false;
                rbSSH.Checked = false;
                rbRaw.Checked = false;
            }
        }

        private void rbRaw_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRaw.Checked)
            {
                rbTelnet.Checked = false;
                rbRlogin.Checked = false;
                rbSSH.Checked = false;
            }
        }

        private void cbUsername_CheckedChanged(object sender, EventArgs e)
        {
            tbUsername.Enabled = cbUsername.Checked;
        }

        private void cbForwardLocal_CheckedChanged(object sender, EventArgs e)
        {
            tbForwardLocal.Enabled = cbForwardLocal.Checked;
        }

        private void cbForwardRemote_CheckedChanged(object sender, EventArgs e)
        {
            tbForwardRemote.Enabled = cbForwardRemote.Checked;
        }

        private void cbRemoteCommand_CheckedChanged(object sender, EventArgs e)
        {
            tbRemoteCommand.Enabled = cbRemoteCommand.Checked;
        }

        private void cbPort_CheckedChanged(object sender, EventArgs e)
        {
            tbPort.Enabled = cbPort.Checked;
        }

        private void cbPassword_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.Enabled = cbPassword.Enabled;
        }

        private void cbPrivateKey_CheckedChanged(object sender, EventArgs e)
        {
            tbPrivateKey.Enabled = cbPrivateKey.Checked;
        }

        private void TemplatePuTTY_Load(object sender, EventArgs e)
        {
        }

        private void cbSSHLevel_CheckedChanged(object sender, EventArgs e)
        {
            rbSSH1.Enabled = cbSSHLevel.Checked;
            rbSSH2.Enabled = cbSSHLevel.Checked;
        }

        private void rbSSH1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSSH1.Checked)
                rbSSH2.Checked = false;
        }

        private void rbSSH2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSSH2.Checked)
                rbSSH1.Checked = false;
        }

        private void cbAgentForwarding_CheckedChanged(object sender, EventArgs e)
        {
            rbAgentOn.Enabled = cbAgentForwarding.Checked;
            rbAgentOff.Enabled = cbAgentForwarding.Checked;
        }

        private void cbX11Forwarding_CheckedChanged(object sender, EventArgs e)
        {
            rbX11On.Enabled = cbX11Forwarding.Checked;
            rbX11Off.Enabled = cbX11Forwarding.Checked;
        }

        private void cbPseudoTerminal_CheckedChanged(object sender, EventArgs e)
        {
            rbTerminalOn.Enabled = cbPseudoTerminal.Checked;
            rbTerminalOff.Enabled = cbPseudoTerminal.Checked;
        }

        private void rbAgentOn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAgentOn.Checked)
                rbAgentOff.Checked = false;
        }

        private void rbAgentOff_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAgentOff.Checked)
                rbAgentOn.Checked = false;
        }

        private void rbX11On_CheckedChanged(object sender, EventArgs e)
        {
            if (rbX11On.Checked)
                rbX11Off.Checked = false;
        }

        private void rbX11Off_CheckedChanged(object sender, EventArgs e)
        {
            if (rbX11Off.Checked)
                rbX11On.Checked = false;
        }

        private void rbTerminalOn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTerminalOn.Checked)
                rbTerminalOff.Checked = false;
        }

        private void rbTerminalOff_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTerminalOff.Checked)
                rbTerminalOn.Checked = false;
        }

        private void RadioButton_Click(object sender, EventArgs e)
        {
            if (sender is RadioButton)
                ((RadioButton)sender).Checked = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cbType_CheckedChanged(object sender, EventArgs e)
        {
            rbSSH.Enabled = cbType.Checked;
            rbTelnet.Enabled = cbType.Checked;
            rbRlogin.Enabled = cbType.Checked;
            rbRaw.Enabled = cbType.Checked;
        }

    }
}
