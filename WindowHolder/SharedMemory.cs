using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace WindowHolder
{
    public class SharedMemory
    {
        private IntPtr m_handleCreate = IntPtr.Zero;
        private IntPtr m_handleMap = IntPtr.Zero;
        private uint m_size = 0;
        private Semaphore m_semaphore = null;
        private const int m_iSemaphoreMax = 8;
        private WindowHolderMain m_main = null;

        public SharedMemory(IntPtr handle, uint size, WindowHolderMain main)
        {
            m_main = main;
            m_semaphore = new Semaphore(0, m_iSemaphoreMax, String.Format("Global\\WindowHolderSemaphore-{0}", (int)handle));
            m_handleCreate = User32.CreateFileMapping(User32.INVALID_HANDLE_VALUE,
                IntPtr.Zero,
                User32.FileMapProtection.PageReadWrite,
                0,
                size,
                String.Format("Global\\WindowHolderMap-{0}", (int)handle));
            m_size = size;

            if (m_handleCreate != IntPtr.Zero)
                m_handleMap = User32.MapViewOfFile(m_handleCreate, User32.FileMapAccess.FileMapAllAccess, 0, 0, size);

            if (m_handleMap != IntPtr.Zero)
                ZeroMap();

            m_semaphore.Release(m_iSemaphoreMax);
        }

        private unsafe void ZeroMap()
        {
            int *pi = (int*)m_handleMap;
            for(int i = 0; i < (m_size / sizeof(int)); i++)
            {
                *pi = 0;
                pi++;
            }
        }

        public void Dispose()
        {
            if (m_handleMap != IntPtr.Zero)
                User32.UnmapViewOfFile(m_handleMap);

            if (m_handleCreate != IntPtr.Zero)
                User32.CloseHandle(m_handleCreate);

            m_handleCreate = IntPtr.Zero;
            m_handleMap = IntPtr.Zero;
        }

        public unsafe bool WriteArray(SortedDictionary<int, CapturedProcessInfo> processes)
        {
            if (!Valid)
                return false;

            int size = processes.Count;

            // Obtain all of the semaphore references to block out readers during write
            for(int i = 0; i < m_iSemaphoreMax; i++)
                m_semaphore.WaitOne();

            int* pi = (int*)m_handleMap;
            *pi = size;
            pi++;

            int iOffset = processes.Count * 4 + 1;    // PID, Parent, Flags, Offset for each, plus the initial size int

            foreach (CapturedProcessInfo info in processes.Values)
            {
                if ((pi - (int*)m_handleMap) + (5* sizeof(int)) > m_size)
                    break;  // Too many processes!

                *pi = (int)info.PID;
                pi++;
                *pi = (int)m_main.GetPanel(info.Page).Handle;
                pi++;
                *pi = (int)info.Flags;
                pi++;

                SortedDictionary<int, CapturedWindowInfo>.ValueCollection windows = processes[info.PID].Windows.Values;
                int * pWindows = ((int*) m_handleMap) + iOffset;

                if ((pWindows + (windows.Count * sizeof(int))) + sizeof(int) - (int*)m_handleMap > m_size)
                {
                    *pi = 0;
                    break;  // Too much data!
                }

                *pi = iOffset;
                pi++;

                *pWindows = windows.Count;
                pWindows++;
                foreach(CapturedWindowInfo winInfo in windows)
                {
                    *pWindows = (int) winInfo.Handle;
                    pWindows++;
                    *pWindows = (int) winInfo.Flags;
                    pWindows++;
                }

                iOffset += (windows.Count * 2 + 1);  // Handle, Flags for each window, plus the initial count int
            }

            m_semaphore.Release(m_iSemaphoreMax);

            return true;
        }

        ~SharedMemory()
        {
            Dispose();
        }

        public bool Valid
        {
            get { return (m_handleCreate != IntPtr.Zero && m_handleMap != IntPtr.Zero); }
        }
    }
}
