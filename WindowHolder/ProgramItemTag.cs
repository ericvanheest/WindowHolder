using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.Text.RegularExpressions;

namespace WindowHolder
{
    public enum TitleBarOption { UseDefault, Remove, DoNotRemove, DoNotForceSize, Invalid };
    public enum YesNoOption { UseDefault, Yes, No, Invalid };
    public enum DoubleClickAction { None, RunProgram, EditProperties, EditProgramOptions, Invalid };
    public enum ProgramItemType { Program, Folder };

    public struct RememberedWindow
    {
        public string Caption;
        public Rectangle Rect;
    }

    public class ProgramItemTag
    {
        public string FullPath;
        public string FriendlyName;
        public string CommandLine;
        public string WorkingDir;
        public string Comment;
        public TitleBarOption TitleBar;
        public long OriginalWindowStyle;
        public long OriginalWindowExStyle;
        public string ExpectedCaption;
        public int MainWindowWait;
        public ProgramItemType Type;
        public List<TabPage> Tabs;
        public TreeNode TreeOrigin;
        public int PID;
        public YesNoOption CaptureChildren;
        public bool ExpectedCaptionIsRegex;
        public YesNoOption CloseTabIfEmpty;
        public YesNoOption StartMinimized;
        public YesNoOption RememberLocations;
        public YesNoOption ResizeToMaximize;
        public Dictionary<string, RememberedWindow> SavedWindows = null;

        public ProgramItemTag()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            FullPath = null;
            FriendlyName = "<error>";
            CommandLine = "";
            WorkingDir = "";
            Comment = "";
            TitleBar = TitleBarOption.UseDefault;
            OriginalWindowExStyle = 0;
            OriginalWindowExStyle = 0;
            ExpectedCaption = "";
            MainWindowWait = -1;
            Type = ProgramItemType.Program;
            TreeOrigin = null;
            ExpectedCaptionIsRegex = false;
            CloseTabIfEmpty = YesNoOption.UseDefault;
            StartMinimized = YesNoOption.UseDefault;
            RememberLocations = YesNoOption.UseDefault;
            PID = 0;
        }

        public ProgramItemTag Clone()
        {
            ProgramItemTag tag = new ProgramItemTag();
            tag.FullPath = FullPath;
            tag.FriendlyName = FriendlyName;
            tag.CommandLine = CommandLine;
            tag.WorkingDir = WorkingDir;
            tag.Comment = Comment;
            tag.TitleBar = TitleBar;
            tag.OriginalWindowExStyle = OriginalWindowExStyle;
            tag.OriginalWindowExStyle = OriginalWindowExStyle;
            tag.ExpectedCaption = ExpectedCaption;
            tag.MainWindowWait = MainWindowWait;
            tag.Type = Type;
            tag.TreeOrigin = null;
            tag.PID = 0;
            tag.ExpectedCaptionIsRegex = ExpectedCaptionIsRegex;
            tag.CloseTabIfEmpty = CloseTabIfEmpty;
            tag.StartMinimized = StartMinimized;
            tag.RememberLocations = RememberLocations;
            return tag;
        }

        public void SetAttributes(XmlElement e)
        {
            e.SetAttribute("CommandLine", CommandLine);
            e.SetAttribute("Comment", Comment);
            e.SetAttribute("ExpectedCaption", ExpectedCaption);
            e.SetAttribute("FriendlyName", FriendlyName);
            e.SetAttribute("FullPath", FullPath);
            e.SetAttribute("TitleBar", ((int)TitleBar).ToString());
            e.SetAttribute("WorkingDir", WorkingDir);
            e.SetAttribute("PollProcWindows", MainWindowWait.ToString());
            e.SetAttribute("Type", ((int)Type).ToString());
            e.SetAttribute("RegexCaption", ExpectedCaptionIsRegex ? "1" : "0");
            e.SetAttribute("CloseTabIfEmpty", ((int) CloseTabIfEmpty).ToString());
            e.SetAttribute("StartMinimized", ((int)StartMinimized).ToString());
            e.SetAttribute("RememberLocations", ((int)RememberLocations).ToString());
            e.SetAttribute("ResizeToMaximize", ((int)ResizeToMaximize).ToString());
            if ((SavedWindows != null) && (SavedWindows.Count > 0))
            {
                XmlElement savedWindows = (XmlElement) e.SelectSingleNode("SavedWindows");
                if (savedWindows == null)
                {
                    savedWindows = e.OwnerDocument.CreateElement("SavedWindows");
                    e.AppendChild(savedWindows);
                }
                foreach (RememberedWindow window in SavedWindows.Values)
                {
                    XmlElement eNew = e.OwnerDocument.CreateElement("Window");
                    eNew.SetAttribute("Caption", window.Caption);
                    eNew.SetAttribute("x", window.Rect.X.ToString());
                    eNew.SetAttribute("y", window.Rect.Y.ToString());
                    eNew.SetAttribute("width", window.Rect.Width.ToString());
                    eNew.SetAttribute("height", window.Rect.Height.ToString());
                    savedWindows.AppendChild(eNew);
                }
            }
        }

        public ProgramItemTag(XmlElement e)
        {
            SetDefaults();
            int iTemp = 0;
            Utility.GetAttribute(e, "FriendlyName", ref FriendlyName);
            if (e.Name == "Folder")
                Type = ProgramItemType.Folder;
            else if (Utility.GetAttribute(e, "Type", ref iTemp))
                Type = (ProgramItemType)iTemp;

            if (Type == ProgramItemType.Folder)
                return;

            Utility.GetAttribute(e, "FullPath", ref FullPath);
            Utility.GetAttribute(e, "CommandLine", ref CommandLine);
            Utility.GetAttribute(e, "Comment", ref Comment);
            Utility.GetAttribute(e, "ExpectedCaption", ref ExpectedCaption);
            if (Utility.GetAttribute(e, "TitleBar", ref iTemp))
                TitleBar = (TitleBarOption)iTemp;
            Utility.GetAttribute(e, "WorkingDir", ref WorkingDir);
            Utility.GetAttribute(e, "PollProcWindows", ref MainWindowWait);
            Utility.GetAttribute(e, "RegexCaption", ref ExpectedCaptionIsRegex);
            Utility.GetAttribute(e, "CloseTabIfEmpty", ref CloseTabIfEmpty);
            Utility.GetAttribute(e, "StartMinimized", ref StartMinimized);
            Utility.GetAttribute(e, "RememberLocations", ref RememberLocations);
            Utility.GetAttribute(e, "ResizeToMaximize", ref ResizeToMaximize);

            SavedWindows = null;
            XmlElement eSavedWindows = (XmlElement)e.SelectSingleNode("SavedWindows");
            if (eSavedWindows != null)
            {
                SavedWindows = new Dictionary<string, RememberedWindow>();
                foreach (XmlElement node in eSavedWindows.ChildNodes)
                {
                    if (node.Name == "Window")
                    {
                        RememberedWindow window;
                        window.Caption = null;
                        int x, y, width, height;
                        x = y = width = height = 0;
                        Utility.GetAttribute(node, "Caption", ref window.Caption);
                        Utility.GetAttribute(node, "x", ref x);
                        Utility.GetAttribute(node, "y", ref y);
                        Utility.GetAttribute(node, "width", ref width);
                        Utility.GetAttribute(node, "height", ref height);
                        window.Rect = new Rectangle(x, y, width, height);
                        SavedWindows.Add(window.Caption, window);
                    }
                }
            }
        }

        public void SetProgramDefaults()
        {
            string strPathLC = FullPath.ToLower();

            if (strPathLC.EndsWith("\\mstsc.exe"))
            {
                ExpectedCaptionIsRegex = true;
                ExpectedCaption = "^.* - Remote Desktop Connection$";
                TitleBar = TitleBarOption.DoNotRemove;
            }
            else if (strPathLC.EndsWith("\\putty.exe"))
            {
                TitleBar = TitleBarOption.Remove;
                ResizeToMaximize = YesNoOption.No;
            }
            else if (Regex.IsMatch(strPathLC, @"\\gimp-[^\\]*.exe"))
            {
                StartMinimized = YesNoOption.No;
                CaptureChildren = YesNoOption.Yes;
                RememberLocations = YesNoOption.Yes;
                TitleBar = TitleBarOption.DoNotForceSize;
            }
        }

        public static string StringForTitleBar(TitleBarOption option)
        {
            switch (option)
            {
                case TitleBarOption.UseDefault: return "(Use global setting)";
                case TitleBarOption.Remove: return "Remove title bar, keep maximized";
                case TitleBarOption.DoNotRemove: return "Keep title bar, keep maximized";
                case TitleBarOption.DoNotForceSize: return "Keep title bar, do not maximize";
                default: return "<error>";
            }
        }

        private TitleBarOption TitleBarOptionFromString(string s)
        {
            int iOption;
            if (!Int32.TryParse(s, out iOption))
                return TitleBarOption.UseDefault;
            if (iOption >= (int)TitleBarOption.Invalid)
                return TitleBarOption.UseDefault;
            return (TitleBarOption)iOption;
        }

        public static string StringForYesNo(YesNoOption option)
        {
            switch (option)
            {
                case YesNoOption.UseDefault: return "(Use global setting)";
                case YesNoOption.No: return "No";
                case YesNoOption.Yes: return "Yes";
                default: return "<error>";
            }
        }

        private YesNoOption YesNoOptionFromString(string s)
        {
            int iOption;
            if (!Int32.TryParse(s, out iOption))
                return YesNoOption.UseDefault;
            if (iOption >= (int)YesNoOption.Invalid)
                return YesNoOption.UseDefault;
            return (YesNoOption)iOption;
        }

        public TitleBarOption TitleBarOpt(Options opt)
        {
            if (TitleBar == TitleBarOption.UseDefault)
            {
                if (opt.bDefaultForceSize)
                    return opt.bDefaultRemoveTitleBar ? TitleBarOption.Remove : TitleBarOption.DoNotRemove;
                else
                    return TitleBarOption.DoNotForceSize;
            }
            return TitleBar;
        }

        public YesNoOption StartMinimizedOpt(Options opt)
        {
            if (StartMinimized == YesNoOption.UseDefault)
                return opt.bDefaultStartMinimized ? YesNoOption.Yes : YesNoOption.No;
            return StartMinimized;
        }

        public YesNoOption CaptureChildrenOpt(Options opt)
        {
            if (CaptureChildren == YesNoOption.UseDefault)
                return opt.bDefaultCaptureWindows ? YesNoOption.Yes : YesNoOption.No;
            return CaptureChildren;
        }

        public YesNoOption RememberLocationOpt(Options opt)
        {
            if (RememberLocations == YesNoOption.UseDefault)
                return opt.bDefaultRememberCapturedLocations ? YesNoOption.Yes : YesNoOption.No;
            return RememberLocations;
        }

        public YesNoOption EmptyTabOpt(Options opt)
        {
            if (CloseTabIfEmpty == YesNoOption.UseDefault)
                return opt.bDefaultCloseTabIfEmpty ? YesNoOption.Yes : YesNoOption.No;
            return CloseTabIfEmpty;
        }

        public YesNoOption ResizeToMaximizeOpt(Options opt)
        {
            if (ResizeToMaximize == YesNoOption.UseDefault)
                return opt.bDefaultResizeToMaximize ? YesNoOption.Yes : YesNoOption.No;
            return ResizeToMaximize;
        }

        public void RememberWindow(Control ctrlHost, IntPtr hWnd)
        {
            if (SavedWindows == null)
                SavedWindows = new Dictionary<string, RememberedWindow>();

            RememberedWindow window;
            window.Caption = User32.GetWindowText(hWnd);
            window.Rect = ctrlHost.RectangleToClient(User32.GetWindowRectangle(hWnd));

            if (SavedWindows.ContainsKey(window.Caption))
                SavedWindows[window.Caption] = window;
            else
                SavedWindows.Add(window.Caption, window);
        }

    }
}
