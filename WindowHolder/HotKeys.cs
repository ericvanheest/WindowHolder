using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Drawing;

namespace WindowHolder
{
    public enum HotKeyableAction
    {
        None,
        SelectPreviousTab,
        SelectNextTab,
        SelectPreviousWindow,
        SelectNextWindow,
        CloseTab,
        ShowHideProgramList,
        CaptureWindow,
        ReleaseWindow,
        ShowHideUI,
        EndOfList
    }

    public struct HotKey
    {
        public HotKeyableAction Action;
        public KeyArray ShortcutKeys;

        public HotKey(HotKeyableAction action, KeyArray keys)
        {
            Action = action;
            ShortcutKeys = keys;
        }
    }

    public class KeyArray
    {
        public class KeyArrayComparer : IComparer
        {
            int IComparer.Compare(object o1, object o2)
            {
                if ((int)o1 < (int)o2)
                    return -1;
                if ((int)o1 > (int)o2)
                    return 1;
                return 0;
            }
        }

        public static KeyArray Null
        {
            get { return new KeyArray(new Keys[] { }); }
        }

        public override int GetHashCode()
        {
            // This hash will be inefficient if there are more than a few keys at once, but that's unlikely
            int iHash = 0;
            foreach (Keys key in m_keys)
            {
                iHash <<= 8;
                iHash |= (int) key;
            }
            return iHash;
        }

        public int Length
        {
            get { return m_keys.Length; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is KeyArray))
                return false;
            KeyArray comp = obj as KeyArray;
            if (comp.Length != m_keys.Length)
                return false;

            for (int i = 0; i < Length; i++)
                if (m_keys[i] != comp.m_keys[i])
                    return false;
            return true;
        }

        public Keys[] m_keys;

        public KeyArray()
        {
            NewKeyArray((Keys)0);
        }

        public KeyArray(Keys keyCurrent)
        {
            NewKeyArray(keyCurrent);
        }

        private void NewKeyArray(Keys keyCurrent)
        {
            byte[] pbKeys = new byte[256];
            List<Keys> list = new List<Keys>();

            //System.Diagnostics.Debugger.Log(0, "", "Keys: ");
            for (int i = 0; i < 256; i++)
            {
                if (HotKeys.PossibleHotKeys[i])
                {
                    //System.Diagnostics.Debugger.Log(0, "", String.Format("{0}:{1:X4} ", ((Keys)i).ToString(), User32.GetAsyncKeyState((Keys)i)));
                    if ((User32.GetAsyncKeyState((Keys)i) & 0x8000) > 0)
                        CombineKey((Keys)i, ref list);
                }
            }
            if (keyCurrent != (Keys)0)
                CombineKey(keyCurrent, ref list);

            //System.Diagnostics.Debugger.Log(0, "", "\n");

            list.Sort();
            m_keys = new Keys[list.Count];
            list.CopyTo(m_keys);
//            System.Diagnostics.Debugger.Log(0, "", String.Format("Keys: {0}\n", Text));
        }

        public KeyArray(byte[] pbKeyboardState)
        {
            List<Keys> list = new List<Keys>();

            for (int i = 0; i < 256; i++)
            {
                if ((pbKeyboardState[i] & 0x80) > 0)
                    CombineKey((Keys)i, ref list);
            }

            list.Sort();
            m_keys = new Keys[list.Count];
            list.CopyTo(m_keys);
        }

        public KeyArray(Keys[] keys)
        {
            List<Keys> list = new List<Keys>();
            foreach (Keys k in keys)
                CombineKey(k, ref list);

            list.Sort();
            m_keys = new Keys[list.Count];
            list.CopyTo(m_keys);
        }

        private void CombineKey(Keys k, ref List<Keys> list)
        {
            switch (k)
            {
                case Keys.LButton:
                case Keys.RButton:
                    break;
                case Keys.LControlKey:
                case Keys.RControlKey:
                case Keys.ControlKey:
                    if (!list.Contains(Keys.ControlKey))
                        list.Add(Keys.ControlKey);
                    break;
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                case Keys.ShiftKey:
                    if (!list.Contains(Keys.ShiftKey))
                        list.Add(Keys.ShiftKey);
                    break;
                case Keys.LMenu:
                case Keys.RMenu:
                case Keys.Menu:
                    if (!list.Contains(Keys.Menu))
                        list.Add(Keys.Menu);
                    break;
                default:
                    list.Add(k);
                    break;
            }
        }

        private void CreateKeyArray(byte[] pbKeys, Keys[] diKeys, bool bUseDI)
        {
        }

        public KeyArray(string keys)
        {
            CSV = keys;
        }

        public string Text
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                string strPlus = "";
                foreach (Keys key in m_keys)
                {
                    switch (key)
                    {
                        case Keys.ControlKey:
                            sb.Append(strPlus);
                            strPlus = "+";
                            sb.Append("Ctrl");
                            break;
                        case Keys.ShiftKey:
                            sb.Append(strPlus);
                            strPlus = "+";
                            sb.Append("Shift");
                            break;
                        case Keys.Menu:
                            sb.Append(strPlus);
                            strPlus = "+";
                            sb.Append("Alt");
                            break;
                        default:
                            break;
                    }
                }
                foreach (Keys key in m_keys)
                {
                    switch (key)
                    {
                        case Keys.LControlKey:
                        case Keys.LShiftKey:
                        case Keys.LMenu:
                        case Keys.RControlKey:
                        case Keys.RShiftKey:
                        case Keys.RMenu:
                        case Keys.ControlKey:
                        case Keys.ShiftKey:
                        case Keys.Menu:
                            break;
                        default:
                            sb.Append(strPlus);
                            strPlus = "+";
                            sb.Append(HotKeys.NameForKey(key));
                            break;
                    }
                }
                return sb.ToString();
            }
        }

        public string CSV
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                string sComma = "";
                foreach (Keys key in m_keys)
                {
                    sb.Append(sComma);
                    sb.Append((int)key);
                    sComma = ",";
                }
                return sb.ToString();
            }
            set
            {
                string[] sNumbers = value.Split(new char[] { ',' });
                List<Keys> list = new List<Keys>();
                foreach (string str in sNumbers)
                {
                    int iKey = 0;
                    if (Int32.TryParse(str, out iKey))
                        list.Add((Keys)iKey);
                }
                list.Sort();
                m_keys = new Keys[list.Count];
                list.CopyTo(m_keys);
            }
        }
    }

    public class HotKeys
    {
        private static User32.HookProc m_hookProc = null;
        private static User32.HookProc m_hookProcMouse = null;
        private static int m_hookHandle = 0;
        private static int m_hookHandleMouse = 0;
        private bool m_bEnabled = false;

        //List<HotKey> m_hotkeys;
        public static Dictionary<KeyArray, HotKeyableAction> HotKeyDict;
        public static bool[] PossibleHotKeys = new bool[256];

        private static IntPtr m_hwndMain;
        private static WindowHolderMain m_mainForm = null;
        private KeyArray m_lastActionKeys = null;
        private bool m_bValid = false;
        private int m_bLastError = 0;

        public bool Valid
        {
            get { return m_bValid; }
        }

        public int LastError
        {
            get { return m_bLastError; }
        }

        private static bool PossibleHotKey(Keys key)
        {
            if ((int)key > 255)
                return false;
            return PossibleHotKeys[(int) key];
        }

        public static void CreatePossibleKeysList()
        {
            for (int i = 0; i < PossibleHotKeys.Length; i++)
                PossibleHotKeys[i] = false;

            foreach (KeyArray keys in HotKeyDict.Keys)
            {
                foreach (Keys k in keys.m_keys)
                {
                    if ((int)k < 256)
                    {
                        PossibleHotKeys[(int)k] = true;
                    }
                }
            }
        }

        private static int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            bool handled = false;

            if (nCode >= 0)
            {
                User32.KeyboardHookStruct khStruct = (User32.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(User32.KeyboardHookStruct));
                if (wParam == User32.WM_KEYDOWN || wParam == User32.WM_SYSKEYDOWN)
                {
                    if (m_mainForm != null)
                        if (PossibleHotKey((Keys) khStruct.VirtualKeyCode))
                            handled = m_mainForm.LLKeyboardEvent(khStruct);
                }
            }

            //if event handled in application do not handoff to other listeners
            if (handled)
                return -1;

            //forward to other application
            return User32.CallNextHookEx(m_hookHandle, nCode, wParam, lParam);
        }

        private static int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            bool handled = false;

            if (nCode >= 0 && m_mainForm != null)
            {
                User32.MouseLLHookStruct mhStruct = (User32.MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(User32.MouseLLHookStruct));
                    handled = m_mainForm.LLMouseEvent((int) wParam, mhStruct);
            }

            //if event handled in application do not handoff to other listeners
            if (handled)
                return -1;

            //forward to other application
            return User32.CallNextHookEx(m_hookHandleMouse, nCode, wParam, lParam);
        }

        public List<HotKey> CombinedHotkeys
        {
            get
            {
                bool[] entries = new bool[(int) HotKeyableAction.EndOfList];
                for (int i = 1; i < entries.Length; i++)
                    entries[i] = false;

                // Return a list that is stripped of left/right duplicates
                List<HotKey> list = new List<HotKey>(HotKeyDict.Count);
                foreach (KeyValuePair<KeyArray, HotKeyableAction> hotkey in HotKeyDict)
                {
                    list.Add(new HotKey(hotkey.Value, hotkey.Key));
                    entries[(int)hotkey.Value] = true;
                }
                // Add shortcuts without keys
                for (int i = 1; i < entries.Length; i++)
                {
                    if (!entries[i])
                        list.Add(new HotKey((HotKeyableAction)i, new KeyArray(new Keys[] { })));
                }
                return list;
            }
        }

        public static string NameForAction(HotKeyableAction action)
        {
            switch(action)
            {
                case HotKeyableAction.SelectPreviousTab: return "Select Previous Tab";
                case HotKeyableAction.SelectNextTab: return "Select Next Tab";
                case HotKeyableAction.SelectPreviousWindow: return "Select Previous Window";
                case HotKeyableAction.SelectNextWindow: return "Select Next Window";
                case HotKeyableAction.CloseTab: return "Close Current Tab";
                case HotKeyableAction.ShowHideProgramList: return "Show/Hide Program List";
                case HotKeyableAction.CaptureWindow: return "Capture the current window";
                case HotKeyableAction.ReleaseWindow: return "Release the current window";
                case HotKeyableAction.ShowHideUI: return "Show/Hide UI";
                default: return action.ToString();
            }
        }

        public static string NameForKey(Keys key)
        {
            switch (key)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                case Keys.ControlKey:
                    return "Ctrl";
                case Keys.LWin:
                    return "LWin";
                case Keys.RWin:
                    return "RWin";
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                case Keys.ShiftKey:
                    return "Shift";
                case Keys.LMenu:
                case Keys.RMenu:
                case Keys.Menu:
                    return "Alt";
                case Keys.D0: return "0";
                case Keys.D1: return "1";
                case Keys.D2: return "2";
                case Keys.D3: return "3";
                case Keys.D4: return "4";
                case Keys.D5: return "5";
                case Keys.D6: return "6";
                case Keys.D7: return "7";
                case Keys.D8: return "8";
                case Keys.D9: return "9";
                case Keys.Left: return "Left";
                case Keys.Right: return "Right";
                case Keys.Up: return "Up";
                case Keys.Down: return "Down";
                case Keys.PageUp: return "PgUp";
                case Keys.Next: return "PgDn";
                case Keys.OemOpenBrackets: return "[";
                case Keys.OemCloseBrackets: return "]";
                case Keys.OemMinus: return "-";
                case Keys.Oemplus: return "=";
                case Keys.Divide: return "NumPad/";
                case Keys.Multiply: return "NumPad*";
                case Keys.Subtract: return "NumPad-";
                case Keys.Add: return "NumPad+";
                case Keys.Oem5: return "\\";
                case Keys.Oem1: return ";";
                case Keys.Oem7: return "'";
                case Keys.Oemcomma: return ",";
                case Keys.OemPeriod: return ".";
                case Keys.OemQuestion: return "/";
                case Keys.Oemtilde: return "`";
                case Keys.Decimal: return "NumPad.";
                default:
                    return key.ToString();
            }
        }

        public void SetDefaults()
        {
            HotKeyDict = new Dictionary<KeyArray, HotKeyableAction>();

            HotKeyDict.Add(new KeyArray(new Keys[] { Keys.ControlKey, Keys.Menu, Keys.PageUp }), HotKeyableAction.SelectPreviousTab);
            HotKeyDict.Add(new KeyArray(new Keys[] { Keys.ControlKey, Keys.Menu, Keys.PageDown }), HotKeyableAction.SelectNextTab);
            HotKeyDict.Add(new KeyArray(new Keys[] { Keys.ControlKey, Keys.Menu, Keys.ShiftKey, Keys.Tab}), HotKeyableAction.SelectPreviousWindow);
            HotKeyDict.Add(new KeyArray(new Keys[] { Keys.ControlKey, Keys.Menu, Keys.Tab}), HotKeyableAction.SelectNextWindow);
            HotKeyDict.Add(new KeyArray(new Keys[] { Keys.ControlKey, Keys.F4 }), HotKeyableAction.CloseTab);
            HotKeyDict.Add(new KeyArray(new Keys[] { Keys.ControlKey, Keys.Menu, Keys.X }), HotKeyableAction.ShowHideProgramList);
            HotKeyDict.Add(new KeyArray(new Keys[] { Keys.ControlKey, Keys.ShiftKey, Keys.Menu, Keys.Add }), HotKeyableAction.CaptureWindow);
            HotKeyDict.Add(new KeyArray(new Keys[] { Keys.ControlKey, Keys.ShiftKey, Keys.Menu, Keys.Subtract }), HotKeyableAction.ReleaseWindow);
            HotKeyDict.Add(new KeyArray(new Keys[] { Keys.ControlKey, Keys.ShiftKey, Keys.Menu, Keys.F11}), HotKeyableAction.ShowHideUI);
            CreatePossibleKeysList();
        }

        public HotKeys(WindowHolderMain main)
        {
            SetDefaults();

            m_hwndMain = main.Handle;
            m_mainForm = main;
            m_bValid = true;
        }

        public void EnableHotkeys()
        {
            if (m_bEnabled)
                return;

            m_bLastError = 0;

            if (m_hookHandle == 0)
            {
                m_hookProc = KeyboardHookProc;
                ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
                m_hookHandle = User32.SetWindowsHookEx(User32.WH_KEYBOARD_LL, m_hookProc, User32.GetModuleHandle(objCurrentModule.ModuleName), 0);
                if (m_hookHandle == 0)
                {
                    m_bValid = false;
                    m_bLastError = Marshal.GetLastWin32Error();
                }
            }

            m_bEnabled = m_bValid;
        }

        public void EnableMouseHook()
        {
            if (m_hookHandleMouse == 0)
            {
                m_hookProcMouse = MouseHookProc;
                ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
                m_hookHandleMouse = User32.SetWindowsHookEx(User32.WH_MOUSE_LL, m_hookProcMouse, User32.GetModuleHandle(objCurrentModule.ModuleName), 0);
                if (m_hookHandleMouse == 0)
                    m_bLastError = Marshal.GetLastWin32Error();
            }
        }

        public void DisableMouseHook()
        {
            if (m_hookHandleMouse != 0)
                User32.UnhookWindowsHookEx(m_hookHandleMouse);
            m_hookHandleMouse = 0;
        }

        public void DisableHotkeys()
        {
            m_bEnabled = false;
            if (m_hookHandle != 0)
                User32.UnhookWindowsHookEx(m_hookHandle);
            m_hookHandle = 0;
        }

        public void Shutdown()
        {
            DisableHotkeys();
            DisableMouseHook();

            m_bValid = false;
        }

        public KeyArray GetCurrentKeys()
        {
            return new KeyArray();
        }

        public KeyArray GetAllCurrentKeys()
        {
            byte[] pbKeys = new byte[256];
            User32.GetKeyboardState(pbKeys);
            return new KeyArray(pbKeys);
        }

        public HotKeyableAction GetHotKeyAction()
        {
            return GetHotKeyAction((Keys)0);
        }

        public HotKeyableAction GetHotKeyAction(Keys keyCurrent)
        {
            if (!m_bEnabled)
                return HotKeyableAction.None;

            KeyArray keys = new KeyArray(keyCurrent);

            if (keys.Length == 0)
            {
                m_lastActionKeys = null;
                return HotKeyableAction.None;
            }

            if (keys.Equals(m_lastActionKeys))
                return HotKeyableAction.None;

            m_lastActionKeys = keys;

            if (!HotKeyDict.ContainsKey(keys))
                return HotKeyableAction.None;

            HotKeyableAction action = HotKeyDict[keys];

            m_lastActionKeys = null;

            return action;
        }
    }


}
