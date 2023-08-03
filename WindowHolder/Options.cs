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
using System.Runtime.InteropServices;
using System.Xml;

namespace WindowHolder
{
    public partial class OptionsForm : Form
    {
        private Options m_options;
        public static bool TestOptions = false;
        private KeyArray m_keysPressed = null;
        private bool m_bResetKeysOnNextPress = true;
        private const string DefaultHKText = "Click here and press new hotkey(s)";
        //private IntPtr ptrHook;
        //private User32.LowLevelKeyboardProc objKeyboardProcess; 

        public OptionsForm()
        {
            InitializeComponent();
        }

        public OptionsForm(Options optIn)
        {
            InitializeComponent();
            m_options = optIn;
            if (TestOptions)
                tcOptions.SelectedIndex = 2;
        }

        private void PopulateHotkeys()
        {
            if (m_options.m_hotKeys == null)
                return;
            lvHotkeys.Items.Clear();
            foreach (HotKey key in m_options.m_hotKeys.CombinedHotkeys)
            {
                string strName = HotKeys.NameForAction(key.Action);
                ListViewItem lvi = new ListViewItem(strName);
                lvi.SubItems.Add(key.ShortcutKeys.Text);
                lvi.Tag = key;
                lvHotkeys.Items.Add(lvi);
            }
        }

        private void UpdateHotkeysFromUI()
        {
            if (m_options.m_hotKeys == null)
                return;
            HotKeys.HotKeyDict.Clear();
            foreach (ListViewItem lvi in lvHotkeys.Items)
            {
                HotKey hk = (HotKey)lvi.Tag;
                if (hk.ShortcutKeys.Length > 0)
                    HotKeys.HotKeyDict.Add(hk.ShortcutKeys, hk.Action);
            }
            HotKeys.CreatePossibleKeysList();
        }

        public Options GetOptions()
        {
            return m_options;
        }

        private void SetUIFromOptions()
        {
            cbSelectActivates.Checked = m_options.bSelectActivates;
            cbAutoHideList.Checked = m_options.bAutoHideList;
            cbDisableHotkeys.Checked = m_options.bDisableAllHotkeys;
            cbCaptureWindows.Checked = m_options.bDefaultCaptureWindows;
            nudMainWindowWait.Value = (decimal)m_options.iMainWindowWait;
            cbDoubleClick.SelectedIndex = (int) m_options.iDoubleClickAction;
            cbConfirmOpenMultiple.Checked = m_options.bConfirmOpenMultiple;
            cbDefaultCloseTabIfEmpty.Checked = m_options.bDefaultCloseTabIfEmpty;
            cbDefaultForceSize.Checked = m_options.bDefaultForceSize;
            cbRemoveTitleBar.Checked = m_options.bDefaultRemoveTitleBar && m_options.bDefaultForceSize;
            cbDefaultStartMinimized.Checked = m_options.bDefaultStartMinimized;
            cbDefaultRememberCapturedLocations.Checked = m_options.bDefaultRememberCapturedLocations;
            cbDefaultResizeToMaximize.Checked = m_options.bDefaultResizeToMaximize;
            cbAllowDragCapture.Checked = m_options.bAllowDragCapture;
            cbCaptureOnlyWithAlt.Checked = m_options.bDragCaptureWithAlt;
            PopulateHotkeys();
        }

        private void SetOptionsFromUI()
        {
            m_options.bAutoHideList = cbAutoHideList.Checked;
            m_options.bSelectActivates = cbSelectActivates.Checked;
            m_options.bDisableAllHotkeys = cbDisableHotkeys.Checked;
            m_options.bDefaultCaptureWindows = cbCaptureWindows.Checked;
            m_options.iMainWindowWait = (int)nudMainWindowWait.Value;
            m_options.iDoubleClickAction = (DoubleClickAction)cbDoubleClick.SelectedIndex;
            m_options.bConfirmOpenMultiple = cbConfirmOpenMultiple.Checked;
            m_options.bDefaultCloseTabIfEmpty = cbDefaultCloseTabIfEmpty.Checked;
            m_options.bDefaultForceSize = cbDefaultForceSize.Checked;
            m_options.bDefaultRemoveTitleBar = cbRemoveTitleBar.Checked && cbDefaultForceSize.Checked;
            m_options.bDefaultStartMinimized = cbDefaultStartMinimized.Checked;
            m_options.bDefaultRememberCapturedLocations = cbDefaultRememberCapturedLocations.Checked;
            m_options.bDefaultResizeToMaximize = cbDefaultResizeToMaximize.Checked;
            m_options.bAllowDragCapture = cbAllowDragCapture.Checked;
            m_options.bDragCaptureWithAlt = cbCaptureOnlyWithAlt.Checked;
            UpdateHotkeysFromUI();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tbHotkey.Focused)
                return;

            SetOptionsFromUI();
            timerHotkey.Stop();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            timerHotkey.Stop();
            Close();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < (int)DoubleClickAction.Invalid; i++)
                cbDoubleClick.Items.Add(Utility.StringForDoubleClick((DoubleClickAction)i));
            cbDoubleClick.SelectedIndex = 0;

            SetUIFromOptions();
            if (m_options.m_hotKeys == null)
                return;
            m_options.m_hotKeys.EnableHotkeys();
            timerHotkey.Start();
        }

        private void lvHotkeys_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tbHotkey.Focus();
        }

        private void tbHotkey_Enter(object sender, EventArgs e)
        {
            if (lvHotkeys.FocusedItem == null)
                return;
            //ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
            //objKeyboardProcess = new User32.LowLevelKeyboardProc(captureKey);
            //ptrHook = User32.SetWindowsHookEx(13, objKeyboardProcess, User32.GetModuleHandle(objCurrentModule.ModuleName), 0);
        }

        //private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        //{
        //    if (nCode >= 0)
        //    {
        //        User32.KBDLLHOOKSTRUCT objKeyInfo = (User32.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(User32.KBDLLHOOKSTRUCT));

        //        // Disable all keys while waiting for shortcut keys
        //        return (IntPtr)1;
        //    }
        //    return User32.CallNextHookEx(ptrHook, nCode, wp, lp);
        //}

        private void tbHotkey_Leave(object sender, EventArgs e)
        {
            if (lvHotkeys.FocusedItem == null)
                return;

            //if (ptrHook != IntPtr.Zero)
            //{
            //    User32.UnhookWindowsHookEx(ptrHook);
            //    ptrHook = IntPtr.Zero;
            //}
        }

        private void AcceptKey()
        {
            if (lvHotkeys.FocusedItem == null)
                return;

            if (m_keysPressed == null)
            {
                ClearKey();
                return;
            }

            HotKey hk = (HotKey)lvHotkeys.FocusedItem.Tag;
            hk.ShortcutKeys = m_keysPressed;
            lvHotkeys.FocusedItem.SubItems[1].Text = hk.ShortcutKeys.Text;
            lvHotkeys.FocusedItem.Tag = hk;
        }

        private void ClearKey()
        {
            if (lvHotkeys.FocusedItem == null)
                return;

            HotKey hk = (HotKey)lvHotkeys.FocusedItem.Tag;
            hk.ShortcutKeys = KeyArray.Null;
            lvHotkeys.FocusedItem.SubItems[1].Text = hk.ShortcutKeys.Text;
            tbHotkey.Text = DefaultHKText;
            lvHotkeys.FocusedItem.Tag = hk;
            lvHotkeys.Focus();
        }

        private void timerHotkey_Tick(object sender, EventArgs e)
        {
            if (!tbHotkey.Focused)
                return;

            KeyArray keys = m_options.m_hotKeys.GetAllCurrentKeys();

            if (keys.Length == 1)
            {
                if (keys.m_keys[0] == Keys.Enter || keys.m_keys[0] == Keys.Return)
                {
                    AcceptKey();
                    return;
                }
                else if (keys.m_keys[0] == Keys.Back)
                {
                    ClearKey();
                    return;
                }
            }

            if (m_bResetKeysOnNextPress && keys.Length > 0)
            {
                m_bResetKeysOnNextPress = false;
                m_keysPressed = null;
            }
            else if (m_keysPressed != null)
            {
                if (keys.Length < m_keysPressed.Length)
                {
                    if (keys.Length == 0)
                        m_bResetKeysOnNextPress = true;
                    return;
                }
            }
            
            string strText = keys.Text;

            if (strText == "")
                return;

            tbHotkey.Text = keys.Text;
            m_keysPressed = keys;
        }

        private void lvHotkeys_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Back:
                    ClearKey();
                    break;
                default:
                    break;
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            AcceptKey();
        }

        private void btnResetHotkeys_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset all hotkeys to default values?", "Confirm reset", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                m_options.m_hotKeys.SetDefaults();
                SetUIFromOptions();
            }
        }

        private void lvHotkeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbHotkey.Text = DefaultHKText;
        }

        private void cbForceSize_CheckedChanged(object sender, EventArgs e)
        {
            cbRemoveTitleBar.Enabled = cbDefaultForceSize.Checked;
        }

        private void cbAllowDragCapture_CheckedChanged(object sender, EventArgs e)
        {
            cbCaptureOnlyWithAlt.Enabled = cbAllowDragCapture.Checked;
        }
    }

    public class HotKeyTextBox : TextBox
    {
        public HotKeyTextBox() : base() { }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return true;
        }
    }

    public class Options
    {
        public bool bDefaultRemoveTitleBar;
        public bool bDefaultCaptureWindows;
        public bool bSelectActivates;
        public bool bAutoHideList;
        public bool bDisableAllHotkeys;
        public bool bDefaultForceSize;
        public HotKeys m_hotKeys;
        public int iMainWindowWait;
        public DoubleClickAction iDoubleClickAction;
        public bool bConfirmOpenMultiple;
        public bool bDefaultCloseTabIfEmpty;
        public bool bDefaultStartMinimized;
        public bool bDefaultRememberCapturedLocations;
        public bool bDefaultResizeToMaximize;
        public bool bAllowDragCapture;
        public bool bDragCaptureWithAlt;

        private void SetDefaults()
        {
            bDefaultRemoveTitleBar = false;
            bDefaultStartMinimized = true;
            bDefaultCaptureWindows = true;
            bSelectActivates = false;
            bDisableAllHotkeys = false;
            bDefaultForceSize = false;
            iDoubleClickAction = DoubleClickAction.RunProgram;
            iMainWindowWait = 5;
            bConfirmOpenMultiple = true;
            bAllowDragCapture = true;
            bDefaultCloseTabIfEmpty = false;
            bDefaultRememberCapturedLocations = true;
            bDefaultResizeToMaximize = false;
            bDragCaptureWithAlt = true;
        }

        public Options(WindowHolderMain main)
        {
            SetDefaults();
            m_hotKeys = null;
            try
            {
                m_hotKeys = new HotKeys(main);
                if (!m_hotKeys.Valid)
                {
                    MessageBox.Show("The hotkey object could not be initialized.\n\n" +
                    "Global hotkey functions will not be available.  Error: " + m_hotKeys.LastError,
                        "Error loading HotKeys", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The DirectInput assembly could not be loaded.  Please check to see if the .config file is missing.\n\n" +
                "Global hotkey functions will not be available.\n\nException: " + ex.Message,
                    "Error loading HotKeys", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadFromFile(WindowHolderMain main, string strFile)
        {
            XmlDocument xml = new XmlDocument();
            bool bRetry = false;
            if (!File.Exists(strFile))
                return;
            do
            {
                try
                {
                    xml.Load(strFile);
                    XmlNode nodeDefaults = xml.SelectSingleNode("/Options/Defaults");
                    XmlNode nodeWindow = xml.SelectSingleNode("/Options/Window");
                    XmlNode nodeHotkeys = xml.SelectSingleNode("/Options/Hotkeys");

                    if (nodeDefaults is XmlElement)
                    {
                        int iAttribute = 0;
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptDefRemoveTB, ref bDefaultRemoveTitleBar);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptDefStartMinimized, ref bDefaultRemoveTitleBar);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptAutoHideList, ref bAutoHideList);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptSelectActivates, ref bSelectActivates);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptDefCaptureWindows, ref bDefaultCaptureWindows);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptDefCloseIfEmpty, ref bDefaultCloseTabIfEmpty);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptDefForceSize, ref bDefaultForceSize);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptMainWindowWait, ref iMainWindowWait);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptDisableHotkeys, ref bDisableAllHotkeys);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptDoubleClickRun, ref iAttribute);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptConfirmOpenMultiple, ref bConfirmOpenMultiple);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptAllowDragCapture, ref bAllowDragCapture);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptDragCaptureWithAlt, ref bDragCaptureWithAlt);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptDefRememberLoc, ref bDefaultRememberCapturedLocations);
                        Utility.GetAttribute((XmlElement)nodeDefaults, Strings.OptDefResizeToMaximize, ref bDefaultResizeToMaximize);
                        iDoubleClickAction = (DoubleClickAction)iAttribute;
                    }

                    if (nodeWindow is XmlElement)
                    {
                        int x = 0;
                        int y = 0;
                        if (Utility.GetAttribute((XmlElement)nodeWindow, Strings.WIMainWindowPos, ref x, ref y))
                        {
                            Point ptLocation = new Point(x, y);
                            if (Utility.IsOnScreen(ptLocation))
                                main.Location = ptLocation;
                        }
                        if (Utility.GetAttribute((XmlElement)nodeWindow, Strings.WIMainWindowSize, ref x, ref y))
                        {
                            main.Size = new Size(x, y);
                        }
                        if (Utility.GetAttribute((XmlElement)nodeWindow, Strings.WIMainWindowSplit, ref x))
                        {
                            main.splitContainer1.SplitterDistance = x;
                        }
                    }

                    if (nodeHotkeys != null)
                    {
                        HotKeys.HotKeyDict.Clear();
                        foreach (XmlElement e in nodeHotkeys.ChildNodes)
                        {
                            int action = (int)HotKeyableAction.None;
                            string keys = "";
                            if (Utility.GetAttribute(e, "Action", ref action) && Utility.GetAttribute(e, "Keys", ref keys))
                            {
                                HotKey hk = new HotKey((HotKeyableAction)action, new KeyArray(keys));
                                if (hk.ShortcutKeys.m_keys.Length > 0 && !HotKeys.HotKeyDict.ContainsKey(hk.ShortcutKeys))
                                    HotKeys.HotKeyDict.Add(hk.ShortcutKeys, hk.Action);
                            }
                        }
                        HotKeys.CreatePossibleKeysList();
                    }
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(String.Format("Unable to read options from file \"{0}\":  {1}", strFile, ex.Message), "Error reading file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        bRetry = true;
                    else
                        bRetry = false;
                }

            } while (bRetry);
        }

        public void SaveToFile(WindowHolderMain main, string strFile)
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", null, null);
            xml.AppendChild(dec);// Create the root element
            XmlElement root = xml.CreateElement("Options");
            root.SetAttribute("Version", "1.0");
            xml.AppendChild(root);

            XmlElement nodeDefaults = xml.CreateElement("Defaults");
            nodeDefaults.SetAttribute(Strings.OptDefRemoveTB, bDefaultRemoveTitleBar ? "1" : "0");
            nodeDefaults.SetAttribute(Strings.OptAutoHideList, bAutoHideList ? "1" : "0");
            nodeDefaults.SetAttribute(Strings.OptSelectActivates, bSelectActivates ? "1" : "0");
            nodeDefaults.SetAttribute(Strings.OptDisableHotkeys, bDisableAllHotkeys ? "1" : "0");
            nodeDefaults.SetAttribute(Strings.OptDefCaptureWindows, bDefaultCaptureWindows ? "1" : "0");
            nodeDefaults.SetAttribute(Strings.OptDefCloseIfEmpty, bDefaultCloseTabIfEmpty ? "1" : "0");
            nodeDefaults.SetAttribute(Strings.OptDefStartMinimized, bDefaultStartMinimized ? "1" : "0");
            nodeDefaults.SetAttribute(Strings.OptConfirmOpenMultiple, bConfirmOpenMultiple ? "1" : "0");
            nodeDefaults.SetAttribute(Strings.OptAllowDragCapture, bAllowDragCapture ? "1" : "0");
            nodeDefaults.SetAttribute(Strings.OptDragCaptureWithAlt, bDragCaptureWithAlt ? "1" : "0");
            nodeDefaults.SetAttribute(Strings.OptDefForceSize, bDefaultForceSize ? "1" : "0");
            nodeDefaults.SetAttribute(Strings.OptDoubleClickRun, ((int)iDoubleClickAction).ToString());
            nodeDefaults.SetAttribute(Strings.OptMainWindowWait, iMainWindowWait.ToString());
            nodeDefaults.SetAttribute(Strings.OptDefRememberLoc, bDefaultRememberCapturedLocations ? "1" : "0");
            nodeDefaults.SetAttribute(Strings.OptDefResizeToMaximize, bDefaultResizeToMaximize ? "1" : "0");
            root.AppendChild(nodeDefaults);

            XmlElement nodeWindow = xml.CreateElement("Window");
            nodeWindow.SetAttribute(Strings.WIMainWindowSize, String.Format("{0},{1}", main.Width, main.Height));
            nodeWindow.SetAttribute(Strings.WIMainWindowPos, String.Format("{0},{1}", main.Location.X, main.Location.Y));
            if ((main.splitContainer1.SplitterDistance == main.splitContainer1.Panel1MinSize) && (main.SplitOriginalPos > 0))
                nodeWindow.SetAttribute(Strings.WIMainWindowSplit, main.SplitOriginalPos.ToString());
            else
                nodeWindow.SetAttribute(Strings.WIMainWindowSplit, main.splitContainer1.SplitterDistance.ToString());
            root.AppendChild(nodeWindow);

            if (m_hotKeys != null)
            {
                XmlElement nodeHotkeys = xml.CreateElement("Hotkeys");
                root.AppendChild(nodeHotkeys);
                foreach (HotKey hk in m_hotKeys.CombinedHotkeys)
                {
                    XmlElement key = xml.CreateElement("Hotkey");
                    key.SetAttribute("Action", ((int)hk.Action).ToString());
                    key.SetAttribute("Keys", hk.ShortcutKeys.CSV);
                    nodeHotkeys.AppendChild(key);
                }
            }

            bool bRetry = false;
            do
            {
                try
                {
                    xml.Save(strFile);
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(String.Format("Unable to save options to file \"{0}\":  {1}", strFile, ex.Message), "Error writing file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        bRetry = true;
                    else
                        bRetry = false;
                }
            } while (bRetry);
        }
    }
}
