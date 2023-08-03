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
    public partial class TemplateMSTSC : TemplateBase
    {
        private string m_sCommandLine;
        private string m_sUnknownParameters;

        public TemplateMSTSC()
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
            if (tbConnectionFile.Text != "")
            {
                sb.Append(tbConnectionFile.Text);
                strSpace = " ";
            }
            sb.Append(AppendOption(ref strSpace, "/v:", cbRemoteComputer, tbRemoteComputer));
            sb.Append(AppendOption(ref strSpace, "/w:", cbWidth, tbWidth));
            sb.Append(AppendOption(ref strSpace, "/h:", cbHeight, tbHeight));
            sb.Append(AppendOption(ref strSpace, "/admin", cbAdmin));
            sb.Append(AppendOption(ref strSpace, "/console", cbConsole));
            sb.Append(AppendOption(ref strSpace, "/f", cbFullscreen));
            sb.Append(AppendOption(ref strSpace, "/public", cbPublic));
            sb.Append(AppendOption(ref strSpace, "/span", cbSpan));
            sb.Append(strSpace + m_sUnknownParameters);
            m_sCommandLine = sb.ToString();
        }

        private void ParseCommandLine()
        {
            StringBuilder sbUnknown = new StringBuilder();
            string strSpace = "";

            string[] args = Utility.SplitArgsWithQuotes(m_sCommandLine);
            int iMainArg = 0;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i][0] == '-' || args[i][0] == '/')
                {
                    string sArg = args[i].Substring(1);
                    switch (sArg.ToLower())
                    {
                        case "admin":
                            cbAdmin.Checked = true;
                            break;
                        case "f":
                        case "fullscreen":
                            cbFullscreen.Checked = true;
                            break;
                        case "public":
                            cbPublic.Checked = true;
                            break;
                        case "console":
                            cbConsole.Checked = true;
                            break;
                        case "span":
                            cbSpan.Checked = true;
                            break;
                        default:
                            if (sArg.StartsWith("v:"))
                            {
                                cbRemoteComputer.Checked = true;
                                tbRemoteComputer.Text = sArg.Substring(2);
                            }
                            else if (sArg.StartsWith("h:"))
                            {
                                cbHeight.Checked = true;
                                tbHeight.Text = sArg.Substring(2);
                            }
                            else if (sArg.StartsWith("w:"))
                            {
                                cbWidth.Checked = true;
                                tbWidth.Text = sArg.Substring(2);
                            }
                            else
                            {
                                sbUnknown.Append(strSpace);
                                strSpace = " ";
                                sbUnknown.Append(args[i]);
                            }
                            break;
                    }
                }
                else
                {
                    switch (iMainArg)
                    {
                        case 0:
                            tbConnectionFile.Text = args[i];
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
            m_sUnknownParameters = sbUnknown.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (ofdBrowseConnection.ShowDialog() == DialogResult.OK)
            {
                tbConnectionFile.Text = ofdBrowseConnection.FileName;
            }
        }

        private void cbRemoteComputer_CheckedChanged(object sender, EventArgs e)
        {
            tbRemoteComputer.Enabled = cbRemoteComputer.Checked;
        }

        private void cbWidth_CheckedChanged(object sender, EventArgs e)
        {
            tbWidth.Enabled = cbWidth.Checked;
        }

        private void cbHeight_CheckedChanged(object sender, EventArgs e)
        {
            tbHeight.Enabled = cbHeight.Checked;
        }

    }
}
