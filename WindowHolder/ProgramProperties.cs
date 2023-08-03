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
    public partial class ProgramProperties : Form
    {
        public ProgramProperties()
        {
            InitializeComponent();
        }

        public string FriendlyName
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }

        public string Application
        {
            get { return tbApplication.Text; }
            set { tbApplication.Text = value; }
        }

        public string CommandLine
        {
            get { return tbCommandLine.Text; }
            set { tbCommandLine.Text = value; }
        }

        private void ProgramProperties_Load(object sender, EventArgs e)
        {
            tbName.SelectAll();
            tbName.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (ofdBrowse.ShowDialog() == DialogResult.OK)
            {
                if (ofdBrowse.FileName.EndsWith(".lnk"))
                {
                    ShortcutInfo si = new ShortcutInfo(ofdBrowse.FileName);
                    tbApplication.Text = si.Application;
                    tbCommandLine.Text = si.CommandLine;
                    tbName.Text = si.FriendlyName;
                    tbWorkingDir.Text = si.WorkingDir;
                }
                tbApplication.Text = ofdBrowse.FileName;
            }
        }

        private void btnBrowseWD_Click(object sender, EventArgs e)
        {
            if (fbdWorkingDir.ShowDialog() == DialogResult.OK)
            {
                tbWorkingDir.Text = fbdWorkingDir.SelectedPath;
            }
        }
    }
}
