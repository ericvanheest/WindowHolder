using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace WindowHolder
{
    public delegate void HookReplacedEventHandler();
    public delegate void WindowEventHandler(IntPtr Handle);
    public delegate void SysCommandEventHandler(int SysCommand, int lParam);
    public delegate void ActivateShellWindowEventHandler();
    public delegate void TaskmanEventHandler();
    public delegate void BasicHookEventHandler(IntPtr Handle1, IntPtr Handle2);
    public delegate void WndProcEventHandler(IntPtr Handle, IntPtr Message, IntPtr wParam, IntPtr lParam);

    public enum Bitness { Running32on32, Running64on64, Running32on64 };

    public abstract class Hook
    {
        protected bool _IsActive = false;
        protected IntPtr _Handle;
        protected string System32Bit = null;
        protected string System64Bit = null;
        protected string RunDLL32Bit = null;
        protected string RunDLL64Bit = null;
        protected Bitness bitness;

        public Hook(IntPtr Handle)
        {
            _Handle = Handle;

            bitness = Bitness.Running32on32;

            bool bWow64 = false;
            if (!User32.IsWow64Process(System.Diagnostics.Process.GetCurrentProcess().Handle, out bWow64))
                bWow64 = false;

            if (bWow64)
            {
                // This is a 32-bit process running on a 64-bit OS
                bitness = Bitness.Running32on64;
                System64Bit = Environment.ExpandEnvironmentVariables("%SystemRoot%\\sysnative");
                System32Bit = Environment.ExpandEnvironmentVariables("%SystemRoot%\\system32");
            }
            else if (Directory.Exists(Environment.ExpandEnvironmentVariables("%SystemRoot%\\syswow64")))
            {
                // This is a 64-bit process running on a 64-bit OS
                bitness = Bitness.Running64on64;
                System64Bit = Environment.ExpandEnvironmentVariables("%SystemRoot%\\system32");
                System32Bit = Environment.ExpandEnvironmentVariables("%SystemRoot%\\syswow64");
            }
            else
            {
                // This is a 32-bit process running on a 32-bit OS
                bitness = Bitness.Running32on32;
                System64Bit = null;
                System32Bit = Environment.ExpandEnvironmentVariables("%SystemRoot%\\system32");
            }

            RunDLL32Bit = Path.Combine(System32Bit, "rundll32.exe");
            if (System64Bit != null)
                RunDLL64Bit = Path.Combine(System64Bit, "rundll32.exe");
        }

        public void Start()
        {
            if (!_IsActive)
            {
                _IsActive = true;
                OnStart();
            }
        }

        public void Stop()
        {
            if (_IsActive)
            {
                OnStop();
                _IsActive = false;
            }
        }

        ~Hook()
        {
            Stop();
        }

        public bool IsActive
        {
            get { return _IsActive; }
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
    }

    public class ShellHook : Hook
    {
        [DllImport("WHHooks.dll")]
        private static extern bool InitializeShellHook(int threadID, IntPtr DestWindow);
        [DllImport("WHHooks.dll")]
        private static extern void UninitializeShellHook();
        [DllImport("WHHooks64.dll", EntryPoint = "InitializeShellHook")]
        private static extern bool InitializeShellHook64(int threadID, IntPtr DestWindow);
        [DllImport("WHHooks64.dll", EntryPoint = "UninitializeShellHook")]
        private static extern void UninitializeShellHook64();

        // Values retreived with RegisterWindowMessage
        //public int _MsgID_Shell_ActivateShellWindow;
        //public int _MsgID_Shell_GetMinRect;
        //public int _MsgID_Shell_Language;
        //public int _MsgID_Shell_Redraw;
        //public int _MsgID_Shell_Taskman;
        //public int _MsgID_Shell_HookReplaced;
        //public int _MsgID_Shell_WindowActivated;
        public int _MsgID_Shell_WindowCreated;
        public int _MsgID_CBT_WindowDestroyed;
        public int _MsgID_CBT_WindowCreated;
        public int _MsgID_CBT_WindowMoved;
        public int _MsgID_CBT_WindowFocus;
        public int _MsgID_KB_KBEvent;
        public int _MsgID_CWP_ShowWindow;
        public int _MsgID_CWR_WindowFocus;
        public int _MsgID_EDV_RedirectFocus;

        private bool m_bValid = false;
        private int m_iLastError = 0;

        public bool Valid
        {
            get { return m_bValid; }
            set { m_bValid = value; }
        }

        public int LastError
        {
            get { return m_iLastError; }
        }

        public ShellHook(IntPtr Handle)
            : base(Handle)
        {
        }

        protected override void OnStart()
        {
            // Retreive the message IDs that we'll look for in WndProc
            //_MsgID_Shell_HookReplaced = User32.RegisterWindowMessage("EDVWHHOOK_SHELL_REPLACED");
            //_MsgID_Shell_ActivateShellWindow = User32.RegisterWindowMessage("EDVWHHOOK_HSHELL_ACTIVATESHELLWINDOW");
            //_MsgID_Shell_GetMinRect = User32.RegisterWindowMessage("EDVWHHOOK_HSHELL_GETMINRECT");
            //_MsgID_Shell_Language = User32.RegisterWindowMessage("EDVWHHOOK_HSHELL_LANGUAGE");
            //_MsgID_Shell_Redraw = User32.RegisterWindowMessage("EDVWHHOOK_HSHELL_REDRAW");
            //_MsgID_Shell_Taskman = User32.RegisterWindowMessage("EDVWHHOOK_HSHELL_TASKMAN");
            //_MsgID_Shell_WindowActivated = User32.RegisterWindowMessage("EDVWHHOOK_HSHELL_WINDOWACTIVATED");
            _MsgID_Shell_WindowCreated = User32.RegisterWindowMessage("EDVWHHOOK_HSHELL_WINDOWCREATED");
            //_MsgID_Shell_WindowDestroyed = User32.RegisterWindowMessage("EDVWHHOOK_HSHELL_WINDOWDESTROYED");
            _MsgID_CBT_WindowCreated = User32.RegisterWindowMessage("EDVWHHOOK_HCBT_WINDOWCREATED");
            _MsgID_CBT_WindowMoved = User32.RegisterWindowMessage("EDVWHHOOK_HCBT_WINDOWMOVED");
            _MsgID_CBT_WindowDestroyed = User32.RegisterWindowMessage("EDVWHHOOK_HCBT_WINDOWDESTROYED");
            _MsgID_KB_KBEvent = User32.RegisterWindowMessage("EDVWHHOOK_HKB_KBEVENT");
            _MsgID_CWP_ShowWindow = User32.RegisterWindowMessage("EDVWHHOOK_HCWP_SHOWWINDOW");
            _MsgID_CBT_WindowFocus = User32.RegisterWindowMessage("EDVWHHOOK_HCBT_WINDOWFOCUSED");
            _MsgID_CWR_WindowFocus = User32.RegisterWindowMessage("EDVWHHOOK_HCWR_SETFOCUS");
            _MsgID_EDV_RedirectFocus = User32.RegisterWindowMessage("EDVWHHOOK_EDV_REDIRECTFOCUS");
            
            // Start the hook
            switch (bitness)
            {
                case Bitness.Running32on32:
                    InitializeShellHook(0, _Handle);
                    break;
                case Bitness.Running32on64:
                    InitializeShellHook(0, _Handle);
                    if (RunDLL64Bit != null && File.Exists(RunDLL64Bit))
                        Process.Start(RunDLL64Bit, "WHHooks64.dll,EPInitializeShellHook " + (long)_Handle);
                    break;
                case Bitness.Running64on64:
                    InitializeShellHook64(0, _Handle);
                    if (File.Exists(RunDLL32Bit))
                        Process.Start(RunDLL32Bit, "WHHooks.dll,EPInitializeShellHook " + (long)_Handle);
                    break;
            }

            m_bValid = true;
        }

        protected override void OnStop()
        {
            if (!m_bValid)
                return;

            switch (bitness)
            {
                case Bitness.Running32on32:
                    UninitializeShellHook();
                    break;
                case Bitness.Running32on64:
                    UninitializeShellHook();
                    if (RunDLL64Bit != null && File.Exists(RunDLL64Bit))
                        Process.Start(RunDLL64Bit, "WHHooks64.dll,EPUninitializeShellHook");
                    break;
                case Bitness.Running64on64:
                    UninitializeShellHook64();
                    if (File.Exists(RunDLL32Bit))
                        Process.Start(RunDLL32Bit, "WHHooks.dll,EPUninitializeShellHook");
                    break;
            }
        }

    }
}

/*
 nCode
    [in] Specifies the hook code. If nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx. This parameter can be one of the following values. 
    HSHELL_ACCESSIBILITYSTATE
    Windows 2000/XP: The accessibility state has changed. 
    HSHELL_ACTIVATESHELLWINDOW
    The shell should activate its main window.
    HSHELL_APPCOMMAND
    Windows 2000/XP: The user completed an input event (for example, pressed an application command button on the mouse or an application command key on the keyboard), and the application did not handle the WM_APPCOMMAND message generated by that input. 
    If the Shell procedure handles the WM_COMMAND message, it should not call CallNextHookEx. See the Return Value section for more information.

    HSHELL_GETMINRECT
    A window is being minimized or maximized. The system needs the coordinates of the minimized rectangle for the window. 
    HSHELL_LANGUAGE
    Keyboard language was changed or a new keyboard layout was loaded.
    HSHELL_REDRAW
    The title of a window in the task bar has been redrawn. 
    HSHELL_TASKMAN
    The user has selected the task list. A shell application that provides a task list should return TRUE to prevent Microsoft Windows from starting its task list.
    HSHELL_WINDOWACTIVATED
    The activation has changed to a different top-level, unowned window. 
    HSHELL_WINDOWCREATED
    A top-level, unowned window has been created. The window exists when the system calls this hook.
    HSHELL_WINDOWDESTROYED
    A top-level, unowned window is about to be destroyed. The window still exists when the system calls this hook.
    HSHELL_WINDOWREPLACED
    Windows XP: A top-level window is being replaced. The window exists when the system calls this hook. 
          
wParam
    [in] The value depends on the value of the nCode parameter, as shown in the following table. nCode wParam 
    HSHELL_ACCESSIBILITYSTATE Windows 2000/XP: Indicates which accessibility feature has changed state. This value is one of the following: ACCESS_FILTERKEYS, ACCESS_MOUSEKEYS, or ACCESS_STICKYKEYS.  
    HSHELL_APPCOMMAND Windows 2000/XP: Where the WM_APPCOMMAND message was originally sent; for example, the handle to a window. For more information, see cmd parameter in WM_APPCOMMAND. 
    HSHELL_GETMINRECT Handle to the minimized or maximized window. 
    HSHELL_LANGUAGE Handle to the window. 
    HSHELL_REDRAW Handle to the redrawn window. 
    HSHELL_WINDOWACTIVATED Handle to the activated window. 
    HSHELL_WINDOWCREATED Handle to the created window. 
    HSHELL_WINDOWDESTROYED Handle to the destroyed window. 
    HSHELL_WINDOWREPLACED Windows XP: Handle to the window being replaced. 

lParam
    [in] The value depends on the value of the nCode parameter, as shown in the following table. nCode lParam 
    HSHELL_APPCOMMAND Windows 2000/XP:GET_APPCOMMAND_LPARAM(lParam) is the application command corresponding to the input event. 
    GET_DEVICE_LPARAM(lParam) indicates what generated the input event; for example, the mouse or keyboard. For more information, see the uDevice parameter description under WM_APPCOMMAND. 

    GET_FLAGS_LPARAM(lParam) depends on the value of cmd in WM_APPCOMMAND. For example, it might indicate which virtual keys were held down when the WM_APPCOMMAND message was originally sent. For more information, see the dwCmdFlags description parameter under WM_APPCOMMAND.
 
    HSHELL_GETMINRECT Pointer to a RECT structure.  
    HSHELL_LANGUAGE Handle to a keyboard layout.  
    HSHELL_REDRAW The value is TRUE if the window is flashing, or FALSE otherwise.  
    HSHELL_WINDOWACTIVATED The value is TRUE if the window is in full-screen mode, or FALSE otherwise.  
    HSHELL_WINDOWREPLACED Windows XP: Handle to the new window. 
*/