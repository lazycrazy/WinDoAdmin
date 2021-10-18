using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinDo.Utilities
{
   public class ProcessHelper
    {
        /// <summary>
        /// The FindWindow function retrieves a handle to the top-level 
        /// window whose class name and window name match the specified strings.
        /// This function does not search child windows. This function does not perform a case-sensitive search.
        /// </summary>
        /// <param name="strClassName">the class name for the window to search for</param>
        /// <param name="strWindowName">the name of the window to search for</param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern int FindWindow(string strClassName, string strWindowName);

        /// <summary>
        /// The FindWindowEx function retrieves a handle to a window whose class name
        /// and window name match the specified strings.
        /// The function searches child windows, beginning with the one following the specified child window.
        /// This function does not perform a case-sensitive search.
        /// </summary>
        /// <param name="hwndParent">a handle to the parent window </param>
        /// <param name="hwndChildAfter">a handle to the child window to start search after</param>
        /// <param name="strClassName">the class name for the window to search for</param>
        /// <param name="strWindowName">the name of the window to search for</param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string strClassName, string strWindowName);

        /// <summary>
        /// The FindWindowEx API
        /// </summary>
        /// <param name="parentHandle">a handle to the parent window </param>
        /// <param name="childAfter">a handle to the child window to start search after</param>
        /// <param name="className">the class name for the window to search for</param>
        /// <param name="windowTitle">the name of the window to search for</param>
        /// <returns></returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("User32.dll")]
        public static extern bool GetWindowRect(HandleRef hwnd, out HRect rect);

        [DllImport("User32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out HRect lpRect);

        [DllImport("User32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        /// <summary>
        /// The SendMessage API
        /// </summary>
        /// <param name="hWnd">handle to the required window</param>
        /// <param name="msg">the system/Custom message to send</param>
        /// <param name="wParam">first message parameter</param>
        /// <param name="lParam">second message parameter</param>
        /// <returns></returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(int hWnd, int msg, int wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct HPoint
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HRect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }

        public const int WM_MOUSEMOVE = 0x200;

        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="processName">进程名</param>
        public static void KillProcess(string processName)
        {
            Process[] myproc = Process.GetProcesses();
            foreach (Process item in myproc)
            {
                if (item.ProcessName.Length >= processName.Length)
                {
                    if (item.ProcessName.Substring(0, processName.Length) == processName)
                    {
                        item.Kill();
                        break;
                    }
                }
            }

        }

        public static void Refresh()
        {
            int hwnd = GetSysTrayWnd();
            HRect nr = new HRect();

            GetClientRect((IntPtr)hwnd, out nr);

            for (int x = 0; x < nr.right; x = x + 2)
            {
                for (int y = 0; y < nr.bottom; y = y + 2)
                {
                    SendMessage(hwnd, WM_MOUSEMOVE, 0, MakeLParam(x, y));
                }
            }
        }

        private static int GetSysTrayWnd()
        {
            OSName osn = OS.GetVersion();
            int k = FindWindow("Shell_TrayWnd", null);
            k = FindWindowEx(k, 0, "TrayNotifyWnd", null);

            if (osn == OSName.Win2000)
            {
                k = FindWindowEx(k, 0, "ToolbarWindow32", null);
                return k;
            }
            else
            {
                k = FindWindowEx(k, 0, "SysPager", null);
                k = FindWindowEx(k, 0, "ToolbarWindow32", null);
                return k;
            }
        }
    }
}
