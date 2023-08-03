using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace WindowHolder
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void About_Load(object sender, EventArgs e)
        {
            labelVersion.Text = String.Format("WindowHolder, version {0}", Assembly.GetExecutingAssembly().GetName().Version);
        }
    }
}
