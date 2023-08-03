// WHHooks.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "WHHooks.h"
#include <stdio.h>
#include <stdlib.h>

#pragma data_seg(".SHRDATA")
HWND hWndDestination = 0;
DWORD pidDestination = 0;
HHOOK hHookShell = NULL;
HHOOK hHookKeyboard = NULL;
HHOOK hHookCBT = NULL;
HHOOK hHookGetMessage = NULL;
HHOOK hHookSysMessage = NULL;
HHOOK hHookCallWndProc = NULL;
HHOOK hHookCallWndRetProc = NULL;
UINT msgShellWindowCreated = 0;
UINT msgKeyboardEvent = 0;
UINT msgCBTWindowCreated = 0;
UINT msgCBTWindowMoved = 0;
UINT msgCBTWindowDestroyed = 0;
UINT msgCWP_ShowWindow = 0;
UINT msgCBTWindowFocused = 0;
UINT msgCWRWindowFocused = 0;
UINT msgEDVRedirectFocus = 0;
#pragma data_seg()

HINSTANCE g_appInstance = NULL;

typedef void (CALLBACK *HookProc)(int code, WPARAM w, LPARAM l);

#define CWIF_PreventMove 0x00000001
#define CWIF_PreventResize 0x00000002
#define CWIF_CaptureChildren 0x00000004

#define MAP_SIZE 32768

typedef struct _CAPTUREDPROCESSINFO {
    unsigned int PID;
    unsigned int ParentHandle;
    unsigned int Flags;
    unsigned int WindowListOffset;
} CAPTUREDPROCESSINFO, *LPCAPTUREDPROCESSINFO;

typedef struct _CAPTUREDWINDOWINFO {
    unsigned int Handle;
    unsigned int Flags;
} CAPTUREDWINDOWINFO, *LPCAPTUREDWINDOWINFO;

typedef struct _FULLWINDOWINFO {
    unsigned int Handle;
    unsigned int PID;
    unsigned int ParentHandle;
    unsigned int Flags;
} FULLWINDOWINFO, *LPFULLWINDOWINFO;

static LRESULT CALLBACK ShellHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK KeyboardHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK CBTHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK GetMessageHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK SysMessageHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK CallWndProcHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK CallWndRetProcHookCallback(int code, WPARAM wparam, LPARAM lparam);

static HANDLE g_hCreateMap = NULL;
static HANDLE g_hSemaphore = NULL;
static int * g_piMap = NULL;
static BOOL g_bMapFail = FALSE;
static BOOL g_bProcessingMove = FALSE;

static int BinaryFind(LPCAPTUREDWINDOWINFO pArray, UINT iSize, UINT hwndMatch)
{
    if (iSize < 1)
        return -1;

    // Start at the middle
    UINT iCurrent = iSize / 2;
    UINT iOffset = 0;

    while(pArray[iCurrent].Handle != hwndMatch)
    {
        if (pArray[iCurrent].Handle < hwndMatch)
        {
            if (iCurrent >= iSize - 1)
                return -1;
            iCurrent++;
            pArray = &pArray[iCurrent];
            iOffset += iCurrent;
            iSize = iSize - iCurrent;
        }
        else if (pArray[iCurrent].Handle > hwndMatch)
        {
            if (iCurrent < 1)
                return -1;
            iSize = iCurrent;
        }

        iCurrent = iSize / 2;
    }

    return iCurrent + iOffset;
}

static int BinaryFind(LPCAPTUREDPROCESSINFO pArray, UINT iSize, UINT pidMatch)
{
    if (iSize < 1)
        return -1;

    // Start at the middle
    UINT iCurrent = iSize / 2;
    UINT iOffset = 0;

    while(pArray[iCurrent].PID != pidMatch)
    {
        if (pArray[iCurrent].PID < pidMatch)
        {
            if (iCurrent >= iSize - 1)
                return -1;
            iCurrent++;
            pArray = &pArray[iCurrent];
            iOffset += iCurrent;
            iSize = iSize - iCurrent;
        }
        else if (pArray[iCurrent].PID > pidMatch)
        {
            if (iCurrent < 1)
                return -1;
            iSize = iCurrent;
        }

        iCurrent = iSize / 2;
    }

    return iCurrent + iOffset;
}

bool InitializeShellHook(int threadID, HWND destination)
{
	if (g_appInstance == NULL)
	{
		return false;
	}

    hWndDestination = destination;
    GetWindowThreadProcessId(destination, &pidDestination);

    if (msgShellWindowCreated == 0)
		msgShellWindowCreated = RegisterWindowMessage(EDVWHHOOK_HSHELL_WINDOWCREATED);
    if (msgCBTWindowCreated == 0)
		msgCBTWindowCreated = RegisterWindowMessage(EDVWHHOOK_HCBT_WINDOWCREATED);
    if (msgCBTWindowDestroyed == 0)
		msgCBTWindowDestroyed = RegisterWindowMessage(EDVWHHOOK_HCBT_WINDOWDESTROYED);
    if (msgCBTWindowMoved == 0)
		msgCBTWindowMoved = RegisterWindowMessage(EDVWHHOOK_HCBT_WINDOWMOVED);
    if (msgKeyboardEvent == 0)
	    msgKeyboardEvent = RegisterWindowMessage(EDVWHHOOK_HKB_KBEVENT);
    if (msgCWP_ShowWindow == 0)
	    msgCWP_ShowWindow = RegisterWindowMessage(EDVWHHOOK_HCWP_SHOWWINDOW);
    if (msgCBTWindowFocused == 0)
	    msgCBTWindowFocused = RegisterWindowMessage(EDVWHHOOK_HCBT_WINDOWFOCUSED);
    if (msgCWRWindowFocused == 0)
	    msgCWRWindowFocused = RegisterWindowMessage(EDVWHHOOK_HCWR_SETFOCUS);
    if (msgEDVRedirectFocus == 0)
        msgEDVRedirectFocus = RegisterWindowMessage(EDVWHHOOK_EDV_REDIRECTFOCUS);

	//hHookShell = SetWindowsHookEx(WH_SHELL, (HOOKPROC)ShellHookCallback, g_appInstance, threadID);
	hHookCBT = SetWindowsHookEx(WH_CBT, (HOOKPROC)CBTHookCallback, g_appInstance, threadID);
	hHookGetMessage = SetWindowsHookEx(WH_GETMESSAGE, (HOOKPROC)GetMessageHookCallback, g_appInstance, threadID);
    hHookCallWndProc = SetWindowsHookEx(WH_CALLWNDPROC, (HOOKPROC)CallWndProcHookCallback, g_appInstance, threadID);
    hHookCallWndRetProc = SetWindowsHookEx(WH_CALLWNDPROCRET, (HOOKPROC)CallWndRetProcHookCallback, g_appInstance, threadID);
    //hHookSysMessage = SetWindowsHookEx(WH_SYSMSGFILTER, (HOOKPROC)SysMessageHookCallback, g_appInstance, threadID);
    //hHookKeyboard = SetWindowsHookEx(WH_KEYBOARD, (HOOKPROC)KeyboardHookCallback, g_appInstance, threadID);

	return (hHookCBT != NULL && hHookCallWndProc != NULL ); // && hHookGetMessage != NULL && hHookSysMessage != NULL && hHookKeyboard != NULL && hHookShell != NULL);
}

void UninitializeShellHook()
{
	if (hHookShell != NULL)
		UnhookWindowsHookEx(hHookShell);
	if (hHookCBT != NULL)
		UnhookWindowsHookEx(hHookCBT);
	if (hHookKeyboard != NULL)
		UnhookWindowsHookEx(hHookKeyboard);
	if (hHookGetMessage != NULL)
		UnhookWindowsHookEx(hHookGetMessage);
	if (hHookSysMessage != NULL)
		UnhookWindowsHookEx(hHookSysMessage);
    if (hHookCallWndProc != NULL)
        UnhookWindowsHookEx(hHookCallWndProc);
    if (hHookCallWndRetProc != NULL)
        UnhookWindowsHookEx(hHookCallWndRetProc);
	hHookShell = NULL;
    hHookCBT = NULL;
    hHookKeyboard = NULL;
    hHookGetMessage = NULL;
    hHookSysMessage = NULL;
    hHookCallWndProc = NULL;
    hHookCallWndRetProc = NULL;

    if (g_piMap != NULL)
        UnmapViewOfFile(g_piMap);
    if (g_hCreateMap != NULL)
        CloseHandle(g_hCreateMap);
    g_piMap = NULL;
    g_hCreateMap = NULL;
}

void CALLBACK EPInitializeShellHook(HWND hwnd, HINSTANCE hinst, LPSTR lpszCmdLine, int nCmdShow)
{
    InitializeShellHook(0, (HWND) atol(lpszCmdLine));

    // Run the calling process until the application exits or UninitializeShellHook is called
    while(hHookCBT != NULL && hHookCallWndProc != NULL /* && hHookGetMessage != NULL && hHookKeyboard != NULL && hHookShell != NULL*/ && IsWindow(hWndDestination))
    {
        Sleep(1000);
    }
}

void CALLBACK EPUninitializeShellHook(HWND hwnd, HINSTANCE hinst, LPSTR lpszCmdLine, int nCmdShow)
{
    UninitializeShellHook();
}

static bool CreateMapping()
{
    TCHAR sz[1024];
    _stprintf_s(sz, 1024, _T("Global\\WindowHolderMap-%d"), hWndDestination);
    g_hCreateMap = CreateFileMapping(INVALID_HANDLE_VALUE, NULL, PAGE_READONLY, 0, 32768, sz);
    _stprintf_s(sz, 1024, _T("Global\\WindowHolderSemaphore-%d"), hWndDestination);
    g_hSemaphore = CreateSemaphore(NULL, 8, 8, sz);

    if (g_hCreateMap == NULL)
        return false;

    g_piMap = (int *) MapViewOfFile(g_hCreateMap, FILE_MAP_READ, 0, 0, MAP_SIZE);
    if (g_piMap == NULL)
        return false;

    return true;
}

// Returns true if the message should be sent to the window, false otherwise
// lpCWI->Handle is NULL if the semaphore could not be locked, meaning we should ignore the flags
static bool GetCWI(HWND hwnd, LPFULLWINDOWINFO lpCWI, LPCAPTUREDPROCESSINFO lpCPI, bool bErrorReturn = true)
{
    if (lpCWI != NULL)
    {
        lpCWI->Flags = 0;
        lpCWI->ParentHandle = NULL;
        lpCWI->Handle = NULL;
        lpCWI->PID = 0;
    }

    if (lpCPI != NULL)
    {
        lpCPI->Flags = 0;
        lpCPI->ParentHandle = NULL;
        lpCPI->PID = 0;
        lpCPI->WindowListOffset = 0;
    }

    if (g_bMapFail)
        return bErrorReturn;

    if (g_piMap == NULL)
    {
        if (!CreateMapping())
        {
            g_bMapFail = TRUE;
            return bErrorReturn;
        }
    }

    int iMapSize = g_piMap[0];

    if (iMapSize == 0)
        return false;

    if ((iMapSize + 1) * sizeof(CAPTUREDWINDOWINFO) > MAP_SIZE)
    {
        // size is most likely corrupted; don't cause access violations trying to read it
        //{FILE *f = fopen("C:\\out.txt", "a"); fprintf(f, "Corrupt size: %d\n", iMapSize); fclose(f); }
        return bErrorReturn;
    }

    LPCAPTUREDPROCESSINFO pCPIStart = (LPCAPTUREDPROCESSINFO) &(g_piMap[1]);

    bool bRet = false;

    switch(WaitForSingleObject(g_hSemaphore, 10))
    {
        case WAIT_OBJECT_0:
            if (lpCPI != NULL)
            {
                DWORD pid;
                if (GetWindowThreadProcessId(hwnd, &pid))
                {
                    //{FILE *f = fopen("C:\\out.txt", "a"); fprintf(f, "Process ID: %d, map size: %d\n", pid, iMapSize); fclose(f); }
                    int iFind = BinaryFind(pCPIStart, iMapSize, pid);
                    //{FILE *f = fopen("C:\\out.txt", "a"); fprintf(f, "iFind: %d\n", iFind); fclose(f); }
                    if (iFind != -1)
                    {
                        lpCPI->PID = pid;
                        lpCPI->Flags = pCPIStart[iFind].Flags;
                        lpCPI->ParentHandle = pCPIStart[iFind].ParentHandle;
                        lpCPI->WindowListOffset = pCPIStart[iFind].WindowListOffset;
                        bRet = true;
                    }
                }
            }
            if (lpCWI != NULL)
            {
                for(int i = 0; i < iMapSize; i++)
                {
                    LPCAPTUREDWINDOWINFO pCWIStart = (LPCAPTUREDWINDOWINFO) (g_piMap + pCPIStart[i].WindowListOffset + 1);
                    int iWindowCount = *(g_piMap + pCPIStart[i].WindowListOffset);
                    //{FILE *f = fopen("C:\\out.txt", "a"); fprintf(f, "Window Count: %d\n", iWindowCount ); fclose(f); }
                    int iFind = BinaryFind(pCWIStart, iWindowCount, (UINT) hwnd);
                    if (iFind != -1)
                    {
                        lpCWI->Handle = (unsigned int) hwnd;
                        lpCWI->ParentHandle = (unsigned int) pCPIStart[i].ParentHandle;
                        lpCWI->Flags = pCWIStart[iFind].Flags;
                        lpCWI->PID = pCPIStart[i].PID;
//                        {FILE *f = fopen("C:\\out.txt", "a"); fprintf(f, "Found CWI[%d]: %8.8x, %8.8x, %8.8x\n", i, lpCWI->Handle, lpCWI->ParentHandle, lpCWI->Flags ); fclose(f); }
                        bRet = true;
                        break;
                    }
                }
            }
            ReleaseSemaphore(g_hSemaphore, 1, NULL);
            return bRet;
        default:
//            {FILE *f = fopen("C:\\out.txt", "a"); fprintf(f, "Semaphore timeout\n"); fclose(f); }
            return bErrorReturn;
    }
}

static LRESULT CALLBACK ShellHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
	if (code >= 0)
	{
        if (code == HSHELL_WINDOWCREATED)
        {
            if (msgShellWindowCreated != 0 && hWndDestination != NULL)
		        SendNotifyMessage(hWndDestination, msgShellWindowCreated, wparam, lparam);
        }
	}

	return CallNextHookEx(hHookShell, code, wparam, lparam);
}

static LRESULT CALLBACK CallWndProcHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
    //{ LPMSG p = (LPMSG) lparam; if (p->message == WM_SHOWWINDOW) {FILE *f = fopen("C:\\out.txt", "a"); fprintf(f, "CWP-Show: code: %d  dest: %8.8x  hwnd: %8.8x  msg: %8.8x  lparam: %8.8x  wparam: %8.8x  pt: %d,%d  time: %8.8x\n", code, hWndDestination, p->hwnd, p->message, p->lParam, p->wParam, p->pt.x, p->pt.y, p->time); fclose(f); }}
    if (code >= 0 && !g_bProcessingMove)
    {
        LPCWPSTRUCT pMsg = (LPCWPSTRUCT) lparam;
        FULLWINDOWINFO cwi;
        switch(pMsg->message)
        {
            case WM_MOVE:
                if (GetCWI(pMsg->hwnd, &cwi, false))
                {
                    //{FILE *f = fopen("C:\\out.txt", "a"); fprintf(f, "cwi.Flags: %8.8x", cwi.Flags); fclose(f); }
                    if ((cwi.Flags & CWIF_PreventMove) > 0)
                    {
                        g_bProcessingMove = TRUE;
                        SetWindowPos(pMsg->hwnd, NULL, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOOWNERZORDER);
                        g_bProcessingMove = FALSE;
                    }
                }
                break;
            //case WM_SHOWWINDOW:
            //    if (GetCWI(pMsg->hwnd, &cwi, false))
            //    {
            //        if (msgCWP_ShowWindow != 0 && hWndDestination != NULL)
            //            SendNotifyMessage(hWndDestination, msgCWP_ShowWindow, (WPARAM) pMsg->hwnd, pMsg->lParam);
            //    }
            //    break;
            default:
                break;
        }
    }

    return CallNextHookEx(hHookCallWndProc, code, wparam, lparam);
}

static LRESULT CALLBACK CallWndRetProcHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
    if (code >= 0 && !g_bProcessingMove)
    {
        LPCWPRETSTRUCT pMsg = (LPCWPRETSTRUCT) lparam;
        FULLWINDOWINFO cwi;
        switch(pMsg->message)
        {
            case WM_SHOWWINDOW:
                if (GetCWI(pMsg->hwnd, &cwi, false))
                {
                    if (msgCWP_ShowWindow != 0 && hWndDestination != NULL)
                        SendNotifyMessage(hWndDestination, msgCWP_ShowWindow, (WPARAM) pMsg->hwnd, pMsg->lParam);
                }
                break;
            //case WM_SETFOCUS:
            //    if (GetCWI(pMsg->hwnd, &cwi, false))
            //    {
            //        SetForegroundWindow(hWndDestination);
            //        AllowSetForegroundWindow(pidDestination);
            //        if (msgCBTWindowFocused != 0 && hWndDestination != NULL)
		          //      SendNotifyMessage(hWndDestination, msgCWRWindowFocused, wparam, lparam);
            //    }
            default:
                break;
        }
    }

    return CallNextHookEx(hHookCallWndProc, code, wparam, lparam);
}

static LRESULT CALLBACK GetMessageHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
    //{ LPMSG p = (LPMSG) lparam; if (p->hwnd == hWndDestination) {FILE *f = fopen("C:\\out.txt", "a"); fprintf(f, "MSG: code: %d  dest: %8.8x  hwnd: %8.8x  msg: %8.8x  lparam: %8.8x  wparam: %8.8x  pt: %d,%d  time: %8.8x\n", code, hWndDestination, p->hwnd, p->message, p->lParam, p->wParam, p->pt.x, p->pt.y, p->time); fclose(f); }}

	if (code >= 0)
	{
        LPMSG pMsg = (LPMSG) lparam;
        if (pMsg->message == msgEDVRedirectFocus)
        {
            //{FILE *f = fopen("C:\\out.txt", "a"); fprintf(f, "Setting foreground window: %8.8x\n", pMsg->wParam); fclose(f); }
            SetForegroundWindow((HWND) pMsg->wParam);
            return CallNextHookEx(hHookCallWndProc, code, wparam, lparam);
        }

        switch(pMsg->message)
        {
            case WM_SHOWWINDOW:
                if (msgCWP_ShowWindow != 0 && hWndDestination != NULL)
                    SendNotifyMessage(hWndDestination, msgCWP_ShowWindow, (WPARAM) pMsg->hwnd, pMsg->lParam);
                break;
            default:
                break;
        }
	}

	return CallNextHookEx(hHookGetMessage, code, wparam, lparam);
}

static LRESULT CALLBACK SysMessageHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
    if (code < 0)
        return CallNextHookEx(hHookGetMessage, code, wparam, lparam);

    return CallNextHookEx(hHookGetMessage, code, wparam, lparam);
}

static bool OnWindowCreation(HWND hwnd, CBT_CREATEWND * pCreateWnd)
{
    // Rule out non-dialog style windows before even bothering with the process list
    LONG_PTR style = GetWindowLongPtr(hwnd, GWL_STYLE);
    if ((style & WS_DLGFRAME) == 0)
        return false;

    CAPTUREDPROCESSINFO cpi;
    if (GetCWI(hwnd, NULL, &cpi, false))
    {
        if ((cpi.Flags & CWIF_CaptureChildren) > 0)
        {
            //{FILE *f = fopen("C:\\out.txt", "a"); fprintf(f, "Capturing 0x%8.8x into 0x%8.8x - hwndParent: %8.8x, x,y: %d, %d, cx,cy: %d,%d\n", hwnd, cpi.ParentHandle, pCreateWnd->lpcs->hwndParent, pCreateWnd->lpcs->x, pCreateWnd->lpcs->y, pCreateWnd->lpcs->cx, pCreateWnd->lpcs->cy); fclose(f); }
            //pCreateWnd->lpcs->hwndParent = (HWND) cpi.ParentHandle;
            //pCreateWnd->lpcs->x = 0;
            //pCreateWnd->lpcs->y = 0;
            //if (hwndOrigParent != NULL)
            //    SetParent(hwnd, hwndOrigParent);
            return true;
        }
        return true;
    }
    return false;
}

static LRESULT CALLBACK CBTHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
	if (code >= 0)
	{
        FULLWINDOWINFO cwi;
        switch(code)
        {
            case HCBT_CREATEWND:
                if (OnWindowCreation((HWND) wparam, (CBT_CREATEWND *) lparam))
                    if (msgCBTWindowCreated != 0 && hWndDestination != NULL)
		                SendNotifyMessage(hWndDestination, msgCBTWindowCreated, wparam, lparam);
                break;
            case HCBT_MOVESIZE:
                if (GetCWI((HWND) wparam, &cwi, NULL))
                {
                    if (msgCBTWindowMoved != 0 && hWndDestination != NULL)
                        SendNotifyMessage(hWndDestination, msgCBTWindowMoved, wparam, lparam);
                }
                break;
            case HCBT_DESTROYWND:
                if (GetCWI((HWND) wparam, &cwi, NULL))
                {
                    if (msgCBTWindowDestroyed != 0 && hWndDestination != NULL)
		                SendNotifyMessage(hWndDestination, msgCBTWindowDestroyed, wparam, lparam);
                }
                break;
            case HCBT_SETFOCUS:
//                {FILE *f = fopen("C:\\out.txt", "a"); fprintf(f, "Current PID: %d\n", GetCurrentProcessId()); fclose(f); }

                if (GetCWI((HWND) wparam, &cwi, NULL))
                {
                    BringWindowToTop(hWndDestination);
                    SetFocus((HWND) wparam);
                    RedrawWindow((HWND) wparam, NULL, NULL, RDW_FRAME | RDW_INVALIDATE);
                    if (msgCBTWindowFocused != 0 && hWndDestination != NULL)
                        SendNotifyMessage(hWndDestination, msgCBTWindowFocused, wparam, lparam);
                }
            default:
                break;
        }
	}

	return CallNextHookEx(hHookCBT, code, wparam, lparam);
}

static LRESULT CALLBACK KeyboardHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
	return CallNextHookEx(hHookKeyboard, code, wparam, lparam);
    if (code >= 0)
    {
        if (GetProcessVersion(pidDestination) > 0)
            AllowSetForegroundWindow(pidDestination);
    }

	return CallNextHookEx(hHookKeyboard, code, wparam, lparam);
}
