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
using System.Xml;
using System.Drawing.Imaging;
using System.Threading;

namespace WindowHolder
{
    public partial class WindowHolderMain : Form
    {
        public enum TabClickEvent { None, CloseTab, ShowContextMenu }

        private bool m_bNeedNewCursor = false;
        private int m_iSplitOriginalPos = 0;
        private TabClickEvent m_tabClickEvent = TabClickEvent.None;
        private Point ptTabImageOffset = new Point(6, 2);
        private DateTime dtBailClosingProgram;
        List<TabPage> pagesRemoving;
        private bool m_bIgnoreScroll = false;

        public int SplitOriginalPos
        {
            get { return m_iSplitOriginalPos; }
            set { m_iSplitOriginalPos = value; }
        }

        public IntPtr GetMainWindow(TabPage page)
        {
            if (page == tpNoProgram)
                return IntPtr.Zero;

            IntPtr[] windows = GetCapturedWindows(page, false);
            if (windows.Length < 1)
                return IntPtr.Zero;

            foreach (IntPtr window in windows)
            {
                uint exStyle = (uint)User32.GetWindowLongPtr32(window, User32.GWL_EXSTYLE);
                if ((exStyle & User32.WS_EX_TOOLWINDOW) > 0)
                    continue;       // Tool windows are not generally main windows

                return window;
            }

            return windows[0];
        }

        private void CloseTab(TabPage page, bool bWait)
        {
            if (page != null)
            {
                // Try closing the main window first, in case it controls the others
                IntPtr ptrMain = GetMainWindow(page);
                if (ptrMain != IntPtr.Zero)
                    User32.PostMessage(ptrMain, User32.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

                IntPtr[] childWindows = GetCapturedWindows(page, false);

                foreach (IntPtr ptrChild in childWindows)
                {
                    if (ptrChild != IntPtr.Zero && User32.IsWindow(ptrChild))
                        User32.PostMessage(ptrChild, User32.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }
                pagesRemoving.Add(page);
            }

            if (bWait)
                return;

            if (pagesRemoving.Count > 0)
            {
                dtBailClosingProgram = DateTime.Now;
                timerCloseProgram.Start();
            }
            else if (page != null)
                RemoveTabPage(page);
        }

        private void timerCloseProgram_Tick(object sender, EventArgs e)
        {
            TabPage[] pages = new TabPage[pagesRemoving.Count];
            pagesRemoving.CopyTo(pages);

            foreach (TabPage page in pages)
            {
                if (GetMainWindow(page) == IntPtr.Zero)
                {
                    pagesRemoving.Remove(page);
                    RemoveTabPage(page);
                }
            }

            if ((DateTime.Now - dtBailClosingProgram).TotalSeconds > 5)
            {
                timerCloseProgram.Stop();
                string strMessage = (pagesRemoving.Count > 1 ? "Some tabs" : "The tab") + " could not be closed gracefully.  Force close?";
                if (MessageBox.Show(strMessage, "Confirm Close", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    foreach (TabPage page in pagesRemoving)
                    {
                        IntPtr ptrChild = GetMainWindow(page);
                        if (ptrChild != IntPtr.Zero)
                        {
                            int id = User32.GetWindowPID(ptrChild);
                            if (id != 0)
                            {
                                Process proc = Process.GetProcessById(id);
                                proc.Kill();
                            }
                        }
                        RemoveTabPage(page);
                    }
                    if (m_bClosing)
                        Close();
                }
                pagesRemoving.Clear();
                m_bClosing = false;
                return;
            }

            if (pagesRemoving.Count < 1)
            {
                timerCloseProgram.Stop();
                if (m_bClosing)
                    Close();
            }
        }

        private void RemoveTabPage(TabPage page)
        {
            ProgramItemTag tag = (ProgramItemTag)page.Tag;
            tcMain.TabPages.Remove(page);
            if (tcMain.TabPages.Count == 0)
            {
                tcMain.TabPages.Add(tpNoProgram);
                ShowUI();
                ShowProgramList();
            }
        }

        private void cmClose_Click(object sender, EventArgs e)
        {
            CloseTab(m_tpClicked, false);
        }

        private void tcMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (tcMain.Capture)
            {
                // Are we dragging a tab off of the control?
                if (!tcMain.Bounds.Contains(e.Location))
                {
                    // Yes, the cursor is no longer on the tab control
                    tcMain.Capture = false;
                    Cursor = Cursors.Default;
                    ReleaseApplication(m_tpClicked, Cursor.Position);
                    return;
                }
                TCHITTESTINFO HTI = new TCHITTESTINFO(e.X, e.Y);
                int index = User32.SendTCMessage(tcMain.Handle, User32.TCM_HITTEST, IntPtr.Zero, ref HTI);
                int iClickedIndex = GetTabIndex(m_tpClicked);
                if (index == -1)
                {
                    SuspendLayout();
                    tcMain.TabPages.Remove(m_tpClicked);
                    tcMain.TabPages.Add(m_tpClicked);
                    ResumeLayout();
                }
                else if (index != iClickedIndex)
                {
                    SuspendLayout();
                    if (iClickedIndex < index)
                        index--;
                    if (index >= 0)
                    {
                        tcMain.TabPages.Remove(m_tpClicked);
                        tcMain.TabPages.Insert(index, m_tpClicked);
                    }
                    ResumeLayout();
                }
            }
            tcMain.Capture = false;
            Cursor = Cursors.Default;

            if (m_tabClickEvent == TabClickEvent.CloseTab)
            {
                m_tabClickEvent = TabClickEvent.None;
                if (m_tpClicked == null)
                    return;
                // Close the tab only if the mouse was clicked and released inside the close image
                int iTabIndex = GetTabIndex(m_tpClicked);
                if (iTabIndex != -1)
                {
                    Point ptTab = tcMain.GetTabRect(iTabIndex).Location;
                    ptTab.Offset(ptTabImageOffset);
                    Rectangle rc = new Rectangle(ptTab, ilTabs.Images[m_tpClicked.ImageIndex].Size);
                    if (rc.Contains(new Point(e.X, e.Y)))
                        CloseTab(m_tpClicked, false);
                }
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                m_tpClicked = null;
                TCHITTESTINFO HTI = new TCHITTESTINFO(e.X, e.Y);
                int index = User32.SendTCMessage(tcMain.Handle, User32.TCM_HITTEST, IntPtr.Zero, ref HTI);
                if (index != -1)
                {
                    m_tpClicked = tcMain.TabPages[index];
                    cmTabs.Show(tcMain, e.Location);
                }
            }
        }

        private int GetTabIndex(TabPage page)
        {
            for(int index = 0; index < tcMain.TabPages.Count; index++)
                if (tcMain.TabPages[index] == page)
                    return index;
            return -1;
        }

        private void tcMain_SizeChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedTab == null)
                return;

            EnsureVisible(tcMain.SelectedTab);

            CheckScrollBars();
        }

        private void cmRelease_Click(object sender, EventArgs e)
        {
            ReleaseApplication(m_tpClicked);
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bClosing)
                return;

            if (tcMain.SelectedTab == null)
                return;

            ProgramItemTag tag = (ProgramItemTag)tcMain.SelectedTab.Tag;
            if (tag == null)
                return;

            if (m_panelFullScreen != null)
            {
                SuspendLayout();
                UnfullscreenPanel();
                FullscreenPanel(tcMain.SelectedTab);
                ResumeLayout();
            }

            FocusMainWindow(tcMain.SelectedTab, IntPtr.Zero);
            CheckScrollBars();
        }

        private void FocusMainWindow(TabPage page, IntPtr hwndMain)
        {
            bool bWindowFocused = false;

            if (hwndMain == IntPtr.Zero)
                hwndMain = GetMainWindow(page);

            if (hwndMain != IntPtr.Zero && User32.IsWindow(hwndMain))
            {
                //User32.BringWindowToTop(hwndMain);
                //User32.ShowWindow(tag.WindowHandle, ShowWindowCommands.Normal);
                IntPtr hwndForeground = User32.GetForegroundWindow();
                //System.Diagnostics.Debugger.Log(0, "", String.Format("FG Window: {0} ({1}), new window: {2} ({3})\n", hwndForeground, User32.GetWindowText(hwndForeground), hwndMain, User32.GetWindowText(hwndMain)));
                if (m_capturedWindows.ContainsKey((int)hwndForeground))
                {
                    //System.Diagnostics.Debugger.Log(0, "", String.Format("Sending custom message ...\n"));
                    User32.PostMessage(hwndForeground, (uint)m_shellHook._MsgID_EDV_RedirectFocus, hwndMain, IntPtr.Zero);
                }
                else
                    User32.SetForegroundWindow(hwndMain);
                bWindowFocused = true;
            }

            if (!bWindowFocused)
                tcMain.SelectedTab.Focus();
        }

        private void tcMain_MouseDown(object sender, MouseEventArgs e)
        {
            tcMain.Capture = false;
            Cursor = Cursors.Default;

            m_tabClickEvent = TabClickEvent.None;
            if (e.Button == MouseButtons.Left)
            {
                m_tpClicked = null;
                TCHITTESTINFO HTI = new TCHITTESTINFO(e.X, e.Y);
                int index = User32.SendTCMessage(tcMain.Handle, User32.TCM_HITTEST, IntPtr.Zero, ref HTI);
                if (index == -1)
                    return;

                m_tpClicked = tcMain.TabPages[index];

                if (m_tpClicked == tpNoProgram || m_tpClicked == null)
                    return;
                if (m_tpClicked.ImageIndex != -1)
                {
                    Point ptTab = tcMain.GetTabRect(index).Location;
                    ptTab.Offset(ptTabImageOffset);
                    Rectangle rc = new Rectangle(ptTab, ilTabs.Images[m_tpClicked.ImageIndex].Size);
                    if (rc.Contains(new Point(e.X, e.Y)))
                    {
                        m_tabClickEvent = TabClickEvent.CloseTab;
                        return;
                    }
                }

                tcMain.Capture = true;
                m_bNeedNewCursor = true;
            } 
        }

        private int IndexOfTab(TabPage page)
        {
            for (int i = 0; i < tcMain.TabCount; i++)
            {
                if (tcMain.TabPages[i] == page)
                    return i;
            }
            return -1;
        }

        private void tcMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (!tcMain.Capture)
                return;

            if (m_bNeedNewCursor)
            {
                m_bNeedNewCursor = false;

                int indexTab = IndexOfTab(m_tpClicked);
                if (indexTab == -1)
                    return;

                Point ptCursor = Cursor.Position;
                Point ptHotspot = Cursor.HotSpot;
                Rectangle rcTab = tcMain.RectangleToScreen(tcMain.GetTabRect(indexTab));
                rcTab.Inflate(2, 2);
                rcTab.Offset(-1, -1);

                Rectangle rcCursor = new Rectangle(ptCursor, Cursor.Current.Size);
                rcCursor.Offset(-Cursor.Current.HotSpot.X, -Cursor.Current.HotSpot.Y);

                Point ptLR = new Point(Math.Max(rcTab.Right, rcCursor.Right), Math.Max(rcTab.Bottom, rcCursor.Bottom));
                Rectangle rcFull = new Rectangle(Math.Min(rcTab.Left, rcCursor.Left), Math.Min(rcTab.Top, rcCursor.Top), 0, 0);
                rcFull.Width = ptLR.X - rcFull.X;
                rcFull.Height = ptLR.Y - rcFull.Y;

                Bitmap bmp = new Bitmap(rcFull.Width, rcFull.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(rcTab.Left, rcTab.Top, rcTab.Left - rcFull.Left, rcTab.Top - rcFull.Top, new Size(rcTab.Width, rcTab.Height), CopyPixelOperation.SourceCopy);
                ptCursor = new Point(rcCursor.Left - rcFull.Left, rcCursor.Top - rcFull.Top);
                ptCursor.Offset(ptHotspot);

                Rectangle rcNewCursor = new Rectangle(rcCursor.Left - rcFull.Left, rcCursor.Top - rcFull.Top, rcCursor.Width, rcCursor.Height); 
                Cursor.Current.Draw(g, rcNewCursor);
                Cursor = Utility.CreateCursor(bmp, ptCursor.X, ptCursor.Y);
            }
        }

        private void tcMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (tcMain.Capture)
                    {
                        Capture = false;
                        Cursor = Cursors.Default;
                    }
                    break;
                default:
                    break;
            }
        }

        private void page_Enter(object sender, EventArgs e)
        {
            AutoHideList();
        }

        private void AutoHideList()
        {
            if (m_options.bAutoHideList)
                if (splitContainer1.SplitterDistance > splitContainer1.Panel1MinSize)
                    HideProgramList();
        }

        private void HideProgramList()
        {
            if (splitContainer1.SplitterDistance > splitContainer1.Panel1MinSize)
                m_iSplitOriginalPos = splitContainer1.SplitterDistance;
            splitContainer1.SplitterDistance = splitContainer1.Panel1MinSize;
        }

        private void EnsureVisibleCurrentTab()
        {
            if (tcMain.SelectedTab == null)
                return;

            EnsureVisible(tcMain.SelectedTab);
        }

        private void EnsureVisibleAllTabs()
        {
            foreach (TabPage page in tcMain.TabPages)
                EnsureVisible(page);
        }

        private void EnsureVisible(TabPage page)
        {
            IntPtr[] windows = GetCapturedWindows(page, false);
            foreach(IntPtr window in windows)
                EnsureWindowVisible(window, page, User32.IsMainWindow(window));
        }

        private void EnsureWindowVisible(IntPtr ptrWindow, TabPage page, bool bMainWindow)
        {
            if (!User32.IsWindowVisibleStyle(ptrWindow))
                return;

            ScrollableControl ctrlParent = page;
            if (m_panelFullScreen != null)
                ctrlParent = this;

            if (bMainWindow)
            {
                Panel panel = GetPanel(page);
                ProgramItemTag tag = (ProgramItemTag)page.Tag;
                TitleBarOption titleBar = tag.TitleBarOpt(m_options);
                if (tag.OriginalWindowStyle == 0)
                {
                    tag.OriginalWindowStyle = (long)User32.GetWindowLongPtr32(ptrWindow, User32.GWL_STYLE);
                    tag.OriginalWindowExStyle = (long)User32.GetWindowLongPtr32(ptrWindow, User32.GWL_EXSTYLE);
                }
                switch (titleBar)
                {
                    case TitleBarOption.DoNotForceSize:
                        if (panel != null)
                            panel.Dock = DockStyle.None;
                        break;
                    case TitleBarOption.DoNotRemove:
                        if (panel != null)
                            panel.Dock = DockStyle.Fill;
                        User32.SetWindowPos(ptrWindow, IntPtr.Zero, 0, 0, ctrlParent.ClientRectangle.Width, ctrlParent.ClientRectangle.Height, SetWindowPosFlags.IgnoreZOrder);
                        return;
                    case TitleBarOption.Remove:
                        if (panel != null)
                            panel.Dock = DockStyle.Fill;
                        User32.RemoveTitleBar(ptrWindow, tag);
                        User32.ShowWindow(ptrWindow, ShowWindowCommands.Maximize);
                        return;
                    default:
                        break;
                }
            }

            Rectangle rc = User32.GetWindowRectangle(ptrWindow);
            if (rc != Rectangle.Empty)
            {
                rc = ctrlParent.RectangleToClient(rc);

                Point ptNew = rc.Location;

                if (ctrlParent.ClientRectangle.Right < rc.Right)
                    ptNew.X -= (rc.Right - ctrlParent.ClientRectangle.Right);
                if (ctrlParent.ClientRectangle.Bottom < rc.Bottom)
                    ptNew.Y -= (rc.Bottom - ctrlParent.ClientRectangle.Bottom);

                if (ctrlParent.ClientRectangle.Left > ptNew.X)
                    ptNew.X = ctrlParent.ClientRectangle.Left;
                if (ctrlParent.ClientRectangle.Top > ptNew.Y)
                    ptNew.Y = ctrlParent.ClientRectangle.Top;



                if (ptNew != rc.Location)
                    User32.SetWindowPos(ptrWindow, IntPtr.Zero, ptNew.X, ptNew.Y, 0, 0, SetWindowPosFlags.IgnoreResize | SetWindowPosFlags.IgnoreZOrder);
            }
        }

        public static Control m_scrollCtrl = new Control();

        private void CheckScrollBars()
        {
            CheckScrollBars(tcMain.SelectedTab);
        }

        private void CheckScrollBars(TabPage page)
        {
            if (m_bIgnoreScroll)
                return;

            if (page == null)
                page = tcMain.SelectedTab;

            if (page == null)
                return;

            IntPtr[] windows = GetCapturedWindows(page, false);

            Panel panel = GetPanel(page);
            if (panel == null)
                return;

            ScrollableControl ctrlParent = page;
            if (panel == m_panelFullScreen)
                ctrlParent = this;

            if (windows.Length < 1)
            {
                panel.Size = ctrlParent.ClientRectangle.Size;
                ctrlParent.AutoScroll = false;
                ProgramItemTag tag = (ProgramItemTag)page.Tag;
                if (tag != null)
                    if (tag.EmptyTabOpt(m_options) == YesNoOption.Yes)
                        CloseTab(page, false);
                return;
            }

            m_bProcessingMove = true;

            if (panel.Dock != DockStyle.Fill)
            {
                Rectangle rcBounds = User32.GetWindowRectangle(windows[0]);
                for (int i = 1; i < windows.Length; i++)
                    rcBounds = Utility.BoundingRect(rcBounds, User32.GetWindowRectangle(windows[i]));
                rcBounds = panel.RectangleToClient(rcBounds);

                // I don't remember why I was using ScrollControlIntoView for this, but it makes the fullscreen mode not work
                //ctrlParent.Controls.Add(m_scrollCtrl);

                //panel.BorderStyle = BorderStyle.Fixed3D;

                // Move all of the windows so that the bounding rectangle location is positive
                Point ptOffset = rcBounds.Location;
                if (ptOffset.X > 0)
                    ptOffset.X = 0;
                if (ptOffset.Y > 0)
                    ptOffset.Y = 0;
                rcBounds.Width -= ptOffset.X;
                rcBounds.Height -= ptOffset.Y;
//                m_scrollCtrl.Location = new Point(ctrlParent.ClientRectangle.Right - 1 - ptOffset.X, ctrlParent.ClientRectangle.Bottom - 1 - ptOffset.Y);
                if (ptOffset != Point.Empty)
                {
                    foreach (IntPtr ptr in windows)
                    {
                        Rectangle rc = panel.RectangleToClient(User32.GetWindowRectangle(ptr));
                        rc.Offset(-ptOffset.X, -ptOffset.Y);
                        User32.SetWindowPos(ptr, IntPtr.Zero, rc.X, rc.Y, 0, 0, SetWindowPosFlags.IgnoreResize | SetWindowPosFlags.IgnoreZOrder);
                    }
                }

                //ctrlParent.ScrollControlIntoView(m_scrollCtrl);
                //ctrlParent.Controls.Remove(m_scrollCtrl);

                // Set the panel size to be large enough to contain all of the windows
                panel.Width = rcBounds.Right;
                panel.Height = rcBounds.Bottom;

                if (panel.Width < ctrlParent.ClientRectangle.Width)
                    panel.Width = ctrlParent.ClientRectangle.Width;
                if (panel.Height < ctrlParent.ClientRectangle.Height)
                    panel.Height = ctrlParent.ClientRectangle.Height;

                ctrlParent.AutoScroll = !ctrlParent.ClientRectangle.Contains(rcBounds);
            }
            else
            {
                //// Fill the window to the screen or maximize it
                ProgramItemTag tag = (ProgramItemTag)page.Tag;
                bool bMaximize = (tag.ResizeToMaximizeOpt(m_options) == YesNoOption.Yes);
                if (tag != null)
                {
                    foreach (IntPtr ptr in windows)
                    {
                        IntPtr ptrParent = User32.GetParent(ptr);
                        if (ptrParent == panel.Handle || ptrParent == IntPtr.Zero)
                        {
                            if (bMaximize)
                                User32.ShowWindow(ptr, ShowWindowCommands.Maximize);
                            else
                                User32.SetWindowPos(ptr, IntPtr.Zero, 0, 0, ctrlParent.ClientRectangle.Width, ctrlParent.ClientRectangle.Height, SetWindowPosFlags.IgnoreZOrder);
                        }
                    }
                }
                ctrlParent.AutoScroll = false;
            }

            m_bProcessingMove = false;
        }

        public struct WindowSize
        {
            public IntPtr Handle;
            public Rectangle Rect;
        }

        private void NormalizeWindows(TabPage page)
        {
            IntPtr[] windows = GetCapturedWindows(page, false);
            Point ptMin = new Point(Int32.MaxValue, Int32.MaxValue);
            List<WindowSize> rects = new List<WindowSize>(windows.Length);

            foreach (IntPtr ptr in windows)
            {
                WindowSize sz;
                sz.Handle = ptr;
                sz.Rect = User32.GetWindowRectangle(ptr);
                if (ptMin.X > sz.Rect.X)
                    ptMin.X = sz.Rect.X;
                if (ptMin.Y > sz.Rect.Y)
                    ptMin.Y = sz.Rect.Y;
                rects.Add(sz);
            }

            if (ptMin == Point.Empty)
                return;

            m_bIgnoreScroll = true;

            foreach (WindowSize sz in rects)
                User32.SetWindowPos(sz.Handle, IntPtr.Zero, sz.Rect.X - ptMin.X, sz.Rect.Y - ptMin.Y, 0, 0, SetWindowPosFlags.IgnoreResize | SetWindowPosFlags.IgnoreZOrder);

            m_bIgnoreScroll = false;

            CheckScrollBars();
        }

        private void SelectNextWindow()
        {
            SelectWindow(true);
        }

        private void SelectPreviousWindow()
        {
            SelectWindow(false);
        }

        private void SelectWindow(bool bNext)
        {
            IntPtr hwnd = User32.GetForegroundWindow();
            TabPage page = tcMain.SelectedTab;
            IntPtr[] windows = GetCapturedWindows(page, false);
            for(int i = 0; i < windows.Length; i++)
            {
                if (windows[i] == hwnd)
                {
                    i = (bNext ? i + 1 : i - 1);
                    if (i < 0)
                        i = windows.Length - 1;
                    else if (i >= windows.Length)
                        i = 0;
                    FocusMainWindow(page, windows[i]);
                    return;
                }
            }
            FocusMainWindow(page, IntPtr.Zero);
        }
    }
}
