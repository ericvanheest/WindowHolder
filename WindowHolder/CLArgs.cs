using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowHolder
{
    public partial class CLArgs : Form
    {
        public string InitialLaunch = null;
        public bool HideProgramList = false;
        public bool ShowHelp = false;
        public bool HideUI = false;

        public CLArgs()
        {
            InitializeComponent();
        }

        public CLArgs(string[] args)
        {
            InitializeComponent();
            for (int i = 1; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-?":
                    case "-h":
                    case "-H":
                    case "/?":
                    case "/h":
                    case "/H":
                        ShowHelp = true;
                        break;
                    default:
                        switch (args[i].ToLower())
                        {
                            case "--help":
                                ShowHelp = true;
                                break;
                            case "--run":
                                if (i < args.Length - 1)
                                    InitialLaunch = args[++i];
                                break;
                            case "--hidelist":
                                HideProgramList = true;
                                break;
                            case "--hideui":
                                HideUI = true;
                                break;
                            default:
                                ShowHelp = true;
                                break;
                        }
                        break;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
