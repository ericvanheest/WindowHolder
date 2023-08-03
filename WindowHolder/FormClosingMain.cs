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
    public partial class FormClosingMain : Form
    {
        public enum WHCloseOption { None, Close, Release, Terminate, Cancel };

        public WHCloseOption CloseOption = WHCloseOption.None;
        private bool bFocused = false;

        public FormClosingMain()
        {
            InitializeComponent();
        }

        public FormClosingMain(WindowHolderMain main)
        {
            InitializeComponent();
            labelRunningApps.Text = String.Format("You appear to have {0} running application{1} in {2} tab{3}.", 
                main.m_capturedProcess.Count,
                main.m_capturedProcess.Count != 1 ? "s" : "",
                main.TabCount,
                main.TabCount != 1 ? "s" : ""
                );
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseOption = WHCloseOption.Close;
            Close();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            CloseOption = WHCloseOption.Release;
            Close();
        }

        private void btnTerminate_Click(object sender, EventArgs e)
        {
            CloseOption = WHCloseOption.Terminate;
            Close();
        }

        protected override void OnActivated(EventArgs e)
        {
            if (!bFocused)
            {
                bFocused = true;
                btnClose.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseOption = WHCloseOption.Cancel;
            Close();
        }
    }
}
