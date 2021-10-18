using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinDo.UI.Main;
using System.Threading;
using WinDo.UI.Utilities.DialogForm;
using System.Runtime.InteropServices;
using System.Diagnostics;
using WinDo.Utilities;
using System.IO;
using WinDo.UI.Utilities;
using WinDo.UI.Manage;
using Newtonsoft.Json;
using WinDoControls.Forms;
using WinDo.UI;

namespace WinDo.WinForm.Main
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new ThreadExceptionEventHandler(CommonExceptionHandlingMethod);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            System.Net.ServicePointManager.DefaultConnectionLimit = 100;
            Process process = RuningInstance();
            if (process == null)//启动
            {
                Utilities.Mock.MockData.LoadData();
                if (!frmClientConfig.Exists())
                {
                    var clientManger = new frmClientConfig();
                    if (!clientManger.HadRegister)// 没有注册
                    {
                        clientManger.ShowDialog();
                        Application.Exit();
                        return;
                    }
                }
                LogHelper.WriteHourLog("Main", "Show Login Form:" + DateTime.Now);
                var login = frmLogin.GetInstance();
                if (args != null && args.Length > 0)
                {
                    var arg1 = args[0];
                    //WinDo注销登录
                    if (arg1.StartsWith("WinDo:"))
                    {
                        login.SetUserName(arg1.Substring(6, arg1.Length - 6));
                    }
                }
                if (login.ShowDialog() == DialogResult.Cancel)
                    return;

                Application.Run(new frmMain());
                LogHelper.WriteHourLog("Main", "结束校验客户端配置:" + DateTime.Now);
            }
            else//退出
            {
                if (process.MainWindowHandle.ToInt32() == 0)
                {
                    var hWnd = FindWindow(null, frmLoginBackground.Guid);
                    uint id = 0;
                    GetWindowThreadProcessId(hWnd, out id);
                    if (id == process.Id)
                        BringProcessToFront(hWnd);
                }
                else
                    BringProcessToFront(process.MainWindowHandle);
                Application.Exit();
            }
        }


        internal static void BringProcessToFront(IntPtr handle)
        {

            //ShowWindowAsync(handle, SW_SHOWNOMAL);//显示 
            //ShowWindow(handle, SW_MINIMIZE); //显示，可以注释掉
            SetActiveWindow(handle);
            SetWindowPos(handle, -1, 0, 0, 0, 0, 1 | 2 | 40);
            SetWindowPos(handle, -2, 0, 0, 0, 0, 1 | 2 | 40);
            ShowWindow(handle, SW_RESTORE); //显示，可以注释掉
            SetForegroundWindow(handle);

            uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
            uint appThread = GetCurrentThreadId();
            const uint SW_SHOW = 5;
            if (foreThread != appThread)
            {
                AttachThreadInput(foreThread, appThread, true);
                BringWindowToTop(handle);
                ShowWindow(handle, SW_SHOW);
                AttachThreadInput(foreThread, appThread, false);
            }
            else
            {
                BringWindowToTop(handle);
                ShowWindow(handle, SW_SHOW);
            }
        }

        private static Process RuningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in Processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    if (process.MainModule.FileName == currentProcess.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }


        #region 异常处理
        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                LogHelper.WriteLog("CurrentDomain_UnhandledException", "ReadProcessMemory");
                LogHelper.WriteException(ex);
                if (ex.Message.Contains("ReadProcessMemory"))
                    return;
                FrmShadowDialog.ShowErrDialog(FormHelper.MainForm, ex.Message, "错误", false, false, true);
            }
            catch (Exception exc)
            {
                try
                {
                    LogHelper.WriteException(exc);
                    MessageBox.Show("Fatal exception happend inside UnhadledExceptionHandler: \n\n"
                        + exc.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }
        public static void CommonExceptionHandlingMethod(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                LogHelper.WriteException(e.Exception);
                FrmShadowDialog.ShowErrDialog(FormHelper.MainForm, "未处理的异常，" + e.Exception.Message, "错误", false, false, true);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal exception happend inside UIThreadException handler",
                        "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            //Application.Exit();

        }

        #endregion



        #region Win32 API

        /// <summary>
        /// 根据窗口标题查找窗体
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        private const int SW_SHOWNOMAL = 1;
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        //HWND_TOP = 0; {在前面}
        //HWND_BOTTOM = 1; {在后面}
        //HWND_TOPMOST = HWND(-1); {在前面, 位于任何顶部窗口的前面}
        //HWND_NOTOPMOST = HWND(-2); {在前面, 位于其他顶部窗口的后面}

        //uFlags 参数可选值:
        //SWP_NOSIZE = 1; {忽略 cx、cy, 保持大小}
        //SWP_NOMOVE = 2; {忽略 X、Y, 不改变位置}
        //SWP_NOZORDER = 4; {忽略 hWndInsertAfter, 保持 Z 顺序}
        //SWP_NOREDRAW = 8; {不重绘}
        //SWP_NOACTIVATE = $10; {不激活}
        //SWP_FRAMECHANGED = $20; {强制发送 WM_NCCALCSIZE 消息, 一般只是在改变大小时才发送此消息}
        //SWP_SHOWWINDOW = $40; {显示窗口}
        //SWP_HIDEWINDOW = $80; {隐藏窗口}

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd,
            out uint lpdwProcessId);
        // When you don't want the ProcessId, use this overload and pass 
        // IntPtr.Zero for the second parameter
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd,
            IntPtr ProcessId);
        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();
        /// The GetForegroundWindow function returns a handle to the 
        /// foreground window.
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach,
            uint idAttachTo, bool fAttach);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);


        [DllImport("user32.dll")]
        static extern bool SetActiveWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);


        /// <summary>
        /// 该函数设置由不同线程产生的窗口的显示状态。
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        private const int SW_HIDE = 0; 　　//从任务栏隐藏
        private const int SW_NORMAL = 1;    //正常弹出窗体 
        private const int SW_MAXIMIZE = 3;    //最大化弹出窗体 
        private const int SW_SHOWNOACTIVATE = 4; 　　//激活窗体/恢复窗体/还原窗体
        private const int SW_SHOW = 5; 　　//显示窗体，最小化时不会最大化
        private const int SW_MINIMIZE = 6; 　　//最小化
        private const int SW_RESTORE = 9;
        private const int SW_SHOWDEFAULT = 10;

        #endregion
    }
}
