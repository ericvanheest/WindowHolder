using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace WindowHolder
{
    public partial class WindowHolderMain : Form
    {
        private TabPage m_tpClicked = null;

        public WindowHolderMain()
        {
            InitializeComponent();
        }

        private void AddFiles(string[] files)
        {
            foreach (string str in files)
            {
                ProgramItemTag tag = new ProgramItemTag();
                tag.FullPath = str;
                tag.FriendlyName = Path.GetFileNameWithoutExtension(str);
                ListViewItem lvi = lvPrograms.Items.Add(tag.FriendlyName);
                lvi.Tag = tag;
            }
        }

        private void lvPrograms_DragDrop(object sender, DragEventArgs e)
        {
            object o = e.Data.GetData(DataFormats.FileDrop, false);
            if (o is string[])
            {
                AddFiles((string[])o);
            }
        }

        private void lvPrograms_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private bool ConfirmDelete()
        {
            if (MessageBox.Show("Remove the selected items from the list?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                return true;
            return false;
        }

        private void lvPrograms_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if (lvPrograms.SelectedItems.Count > 0)
                        DeleteSelectedPrograms();
                    break;
                case Keys.A:
                    if (e.Control && !e.Shift && !e.Alt)
                        foreach (ListViewItem lvi in lvPrograms.Items)
                            lvi.Selected = true;
                    break;
                case Keys.Enter:
                    RunSelectedProgram();
                    break;
                case Keys.F2:
                    RenameSelectedItem();
                    break;
                default:
                    break;
            }
        }

        private void lvPrograms_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RunSelectedProgram();
        }

        private void RunSelectedProgram()
        {
            if (lvPrograms.SelectedItems.Count < 1)
                return;

            ProgramItemTag tag = (ProgramItemTag)lvPrograms.SelectedItems[0].Tag;
            Process proc;
            try
            {
                proc = Process.Start(tag.FullPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Unable to launch process \"{0}\" - {1}", tag.FullPath, ex.Message), "Error launching program", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Thread.Sleep(50);
            DateTime dtStart = DateTime.Now;

            while (proc.MainWindowHandle == IntPtr.Zero)
            {
                Thread.Sleep(50);
                if ((DateTime.Now - dtStart).TotalSeconds > 5)
                {
                    MessageBox.Show(String.Format("Unable to find main window for process \"{0}\", PID {1}", tag.FullPath, proc.Id), "Error launching program", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            tcMain.TabPages.Add(tag.FriendlyName);
            TabPage page = tcMain.TabPages[tcMain.TabPages.Count - 1];

            User32.AddWindowToControl(proc.MainWindowHandle, page);
        }

        private void CloseTab(TabPage page)
        {
            IntPtr ptrChild = User32.GetWindow(page.Handle, GetWindow_Cmd.GW_CHILD);
            if (ptrChild != IntPtr.Zero)
            {
                User32.SendMessage(ptrChild, User32.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                ptrChild = User32.GetWindow(page.Handle, GetWindow_Cmd.GW_CHILD);
                if (ptrChild != IntPtr.Zero)
                {
                    if (MessageBox.Show("The tab could not be closed gracefully.  Force close?", "Confirm Close", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        int id;
                        User32.GetWindowThreadProcessId(ptrChild, out id);
                        if (id != 0)
                        {
                            Process proc = Process.GetProcessById(id);
                            proc.Kill();
                        }
                        tcMain.TabPages.Remove(page);
                    }
                    else
                        return;
                }
            }

            tcMain.TabPages.Remove(page);
        }

        private void cmClose_Click(object sender, EventArgs e)
        {
            CloseTab(m_tpClicked);
        }

        private void tcMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_tpClicked = null;
                TCHITTESTINFO HTI = new TCHITTESTINFO(e.X, e.Y);
                m_tpClicked = tcMain.TabPages[User32.SendTCMessage(tcMain.Handle, User32.TCM_HITTEST, IntPtr.Zero, ref HTI)];
                cmTabs.Show(tcMain, e.Location);
            }
        }

        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            About formAbout = new About();
            formAbout.ShowDialog();
        }

        private void ReleaseApplications()
        {
        }

        private void TerminateApplications()
        {
        }

        private void SaveApplicationList()
        {
            string strPath = GetUserDataPath();
            TextWriter writer = null;
            bool bRetry = false;
            do
            {
                try
                {
                    writer = new StreamWriter(Path.Combine(strPath, Strings.FNProgramList)) as TextWriter;
                    writer.WriteLine("1.0.0");
                    foreach (ListViewItem lvi in lvPrograms.Items)
                    {
                        ProgramItemTag tag = (ProgramItemTag)lvi.Tag;
                        writer.WriteLine(tag.SerializeToString());
                    }
                    writer.Close();
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(String.Format("Unable to save application list to file \"{0}\":  {1}", Strings.FNProgramList, ex.Message), "Error writing file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        bRetry = true;
                    else
                        bRetry = false;
                    if (writer != null)
                        writer.Close();
                }
            } while (bRetry);
            if (writer != null)
                writer.Close();
        }

        private void SaveWindowInformation()
        {
            string strPath = GetUserDataPath();
            TextWriter writer = null;
            bool bRetry = false;
            do
            {
                try
                {
                    writer = new StreamWriter(Path.Combine(strPath, Strings.FNWindowInfo)) as TextWriter;
                    writer.WriteLine("1.0.0");
                    writer.WriteLine("{0}:{1},{2}", Strings.WIMainWindowSize, Width, Height);
                    writer.WriteLine("{0}:{1},{2}", Strings.WIMainWindowPos, Location.X, Location.Y);
                    writer.WriteLine("{0}:{1}", Strings.WIMainWindowSplit, splitContainer1.SplitterDistance);
                    writer.Close();
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(String.Format("Unable to save window information to file \"{0}\":  {1}", Strings.FNWindowInfo, ex.Message), "Error writing file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        bRetry = true;
                    else
                        bRetry = false;
                    if (writer != null)
                        writer.Close();
                }
            } while (bRetry);
            if (writer != null)
                writer.Close();
        }

        private void LoadWindowInformation()
        {
            string strPath = GetUserDataPath();
            TextReader reader = null;
            bool bRetry = false;
            string strFile = Path.Combine(strPath, Strings.FNWindowInfo);
            if (!File.Exists(strFile))
                return;
            do
            {
                try
                {
                    reader = new StreamReader(strFile) as TextReader;
                    string strVersion = reader.ReadLine();
                    string strInfo;
                    do
                    {
                        strInfo = reader.ReadLine();
                        if (strInfo != null)
                        {
                            string[] items = strInfo.Split(new char[] { ':' });
                            if (items.Length >= 2)
                            {
                                if (items[0] == Strings.WIMainWindowPos)
                                {
                                    string[] pos = items[1].Split(new char[] { ',' });
                                    int x, y;
                                    if (Int32.TryParse(pos[0], out x) && Int32.TryParse(pos[1], out y))
                                    {
                                        Point ptLocation = new Point(x,y);
                                        if (Utility.IsOnScreen(ptLocation))
                                            Location = ptLocation;
                                    }
                                }
                                else if (items[0] == Strings.WIMainWindowSize)
                                {
                                    string[] pos = items[1].Split(new char[] { ',' });
                                    int w, h;
                                    if (Int32.TryParse(pos[0], out w) && Int32.TryParse(pos[1], out h))
                                        Size = new Size(w, h);
                                }
                                else if (items[0] == Strings.WIMainWindowSplit)
                                {
                                    int x;
                                    if (Int32.TryParse(items[1], out x))
                                        splitContainer1.SplitterDistance = x;
                                }
                            }
                        }
                    } while (strInfo != null);
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(String.Format("Unable to read window information from file \"{0}\":  {1}", Strings.FNWindowInfo, ex.Message), "Error reading file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        bRetry = true;
                    else
                        bRetry = false;
                    if (reader != null)
                        reader.Close();
                }

            } while (bRetry);

            if (reader != null)
                reader.Close();
        }

        private void LoadApplicationList()
        {
            string strPath = GetUserDataPath();
            TextReader reader = null;
            bool bRetry = false;
            string strFile = Path.Combine(strPath, Strings.FNProgramList);
            if (!File.Exists(strFile))
                return;
            do
            {
                try
                {
                    reader = new StreamReader(strFile) as TextReader;
                    string strVersion = reader.ReadLine();
                    lvPrograms.Items.Clear();
                    string strProgram;
                    do
                    {
                        strProgram = reader.ReadLine();
                        if (strProgram != null)
                        {
                            ProgramItemTag tag = new ProgramItemTag(strProgram);
                            ListViewItem lvi = new ListViewItem(tag.FriendlyName);
                            lvi.Tag = tag;
                            lvPrograms.Items.Add(lvi);
                        }
                    } while (strProgram != null);
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(String.Format("Unable to read application list from file \"{0}\":  {1}", Strings.FNProgramList, ex.Message), "Error reading file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        bRetry = true;
                    else
                        bRetry = false;
                    if (reader != null)
                        reader.Close();
                }

            } while (bRetry);

            if (reader != null)
                reader.Close();
        }

        private string GetUserDataPath()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            dir = System.IO.Path.Combine(dir, "WindowHolder");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WindowHolderMain_Load(object sender, EventArgs e)
        {
            LoadApplicationList();
            LoadWindowInformation();
        }

        private void WindowHolderMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tcMain.TabPages.Count > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to release the open applications from their containers?\n\nChoosing \"no\" will terminate the processes!", "Confirm exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                switch (result)
                {
                    case DialogResult.Yes:
                        ReleaseApplications();
                        break;
                    case DialogResult.No:
                        TerminateApplications();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }
            }
            SaveApplicationList();
            SaveWindowInformation();
        }

        private void miProgramProperties_Click(object sender, EventArgs e)
        {

        }

        private void miProgramRename_Click(object sender, EventArgs e)
        {
            RenameSelectedItem();
        }

        private void miProgramDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedPrograms();
        }

        private void RenameSelectedItem()
        {
            if (lvPrograms.SelectedItems.Count < 1)
                return;
            lvPrograms.SelectedItems[0].BeginEdit();
        }

        private void DeleteSelectedPrograms()
        {
            if (ConfirmDelete())
                foreach (ListViewItem lvi in lvPrograms.SelectedItems)
                    lvi.Remove();
        }

        private void cmPrograms_Opening(object sender, CancelEventArgs e)
        {
            miProgramDelete.Enabled = lvPrograms.SelectedItems.Count > 0;
            miProgramProperties.Enabled = lvPrograms.SelectedItems.Count > 0;
            miProgramRename.Enabled = lvPrograms.SelectedItems.Count > 0;
            miProgramNew.Enabled = lvPrograms.SelectedItems.Count == 0;
        }

        private void lvPrograms_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            ProgramItemTag tag = (ProgramItemTag)lvPrograms.Items[e.Item].Tag;
            tag.FriendlyName = e.Label;
        }

        private void miProgramNew_Click(object sender, EventArgs e)
        {
            ProgramProperties prop = new ProgramProperties();
            prop.FriendlyName = "New Application";
            if (prop.ShowDialog() == DialogResult.OK)
            {
                ProgramItemTag tag = new ProgramItemTag();
                tag.FriendlyName = prop.FriendlyName;
                tag.FullPath = prop.Application;
                tag.CommandLine = prop.CommandLine;
                ListViewItem lvi = new ListViewItem(tag.FriendlyName);
                lvi.Tag = tag;
                lvPrograms.Items.Add(lvi);
            }
        }
    }
}
