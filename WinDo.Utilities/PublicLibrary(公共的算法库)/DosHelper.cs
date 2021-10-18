using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinDo.Utilities
{
    /// <summary>
    /// 
    /// DosHelper
    /// Dos常用操作类
    /// 
    /// </summary>
    public class DosHelper
    {
        //引入API函数
        [DllImportAttribute("user32.dll")]
        private static extern int FindWindow(string ClassName, string WindowName);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int handle, int cmdShow);
        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnstring, int uReturnLength, int hwndCallback);

        private const int SW_HIDE = 0;//API参数表示隐藏窗口
        private const int SW_SHOW = 5;//API参数表示用当前的大小和位置显示窗口

        /// <summary>
        /// 弹出光驱
        /// </summary>
        public void 弹出光驱()
        {
            mciSendString("set CDAudio door open", null, 127, 0);
        }

        /// <summary>
        /// 关闭光驱
        /// </summary>
        public void 关闭光驱()
        {
            mciSendString("set CDAudio door closed", null, 127, 0);
        }

        /// <summary>
        /// 打开C盘
        /// </summary>
        public void 打开C盘()
        {
            Process.Start("c:\\");
        }

        /// <summary>
        /// 打开D盘
        /// </summary>
        public void 打开D盘()
        {
            Process.Start("d:\\");
        }

        /// <summary>
        /// 打开E盘
        /// </summary>
        public void 打开E盘()
        {
            Process.Start("e:\\");
        }

        /// <summary>
        /// 打开F盘
        /// </summary>
        public void 打开F盘()
        {
            Process.Start("f:\\");
        }

        /// <summary>
        /// 打开指定盘
        /// </summary>
        /// <param name="hardpath"></param>
        public void 打开指定盘(string hardpath)
        {
            Process.Start(hardpath);
        }

        /// <summary>
        /// 打开Word
        /// </summary>
        public void 打开Word()
        {
            Process.Start(@"C:\Program Files\Microsoft Office\OFFICE11\winword.exe");
        }

        /// <summary>
        /// 打开Excel
        /// </summary>
        public void 打开Excel()
        {
            Process.Start(@"C:\Program Files\Microsoft Office\OFFICE11\excel.exe");
        }

        /// <summary>
        /// 打开Access
        /// </summary>
        public void 打开Access()
        {
            Process.Start(@"C:\Program Files\Microsoft Office\OFFICE11\msaccess.exe");
        }

        /// <summary>
        /// 打开PowerPoint
        /// </summary>
        public void 打开PowerPoint()
        {
            Process.Start(@"C:\Program Files\Microsoft Office\OFFICE11\powerpnt.exe");
        }

        public void 打开OutLook()
        {
            Process.Start(@"C:\Program Files\Microsoft Office\OFFICE11\outlook.exe");
        }

        /// <summary>
        /// 打开记事本
        /// </summary>
        public void 打开记事本()
        {
            Process.Start("notepad.exe");
        }

        /// <summary>
        /// 打开计算器
        /// </summary>
        public void 打开计算器()
        {
            Process.Start("calc.exe");
        }

        /// <summary>
        /// 打开DOS命令窗口
        /// </summary>
        public void 打开DOS命令窗口()
        {
            Process.Start("cmd.exe");
        }

        /// <summary>
        /// 打开注册表
        /// </summary>
        public void 打开注册表()
        {
            Process.Start("regedit.exe");
        }

        /// <summary>
        /// 打开画图板
        /// </summary>
        public void 打开画图板()
        {
            Process.Start("mspaint.exe");
        }

        /// <summary>
        /// 打开写字板
        /// </summary>
        public void 打开写字板()
        {
            Process.Start("write.exe");
        }

        /// <summary>
        /// 打开播放器
        /// </summary>
        public void 打开播放器()
        {
            Process.Start("mplayer2.exe");
        }

        /// <summary>
        /// 打开资源管理器
        /// </summary>
        public void 打开资源管理器()
        {
            Process.Start("explorer.exe");
        }

        /// <summary>
        /// 打开任务管理器
        /// </summary>
        public void 打开任务管理器()
        {
            Process.Start("taskmgr.exe");
        }

        /// <summary>
        /// 打开事件查看器
        /// </summary>
        public void 打开事件查看器()
        {
            Process.Start("eventvwr.exe");
        }

        /// <summary>
        /// 打开系统信息
        /// </summary>
        public void 打开系统信息()
        {
            Process.Start("winmsd.exe");
        }

        /// <summary>
        /// 打开备份还原
        /// </summary>
        public void 打开备份还原()
        {
            Process.Start("ntbackup.exe");
        }

        /// <summary>
        /// 打开Windows版本
        /// </summary>
        public void 打开Windows版本()
        {
            Process.Start("winver.exe");
        }

        /// <summary>
        /// 打开控制面板
        /// </summary>
        public void 打开控制面板()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL");
        }

        /// <summary>
        /// 打开控制面板辅助选项键盘
        /// </summary>
        public void 打开控制面板辅助选项键盘()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL access.cpl,,1");
        }

        /// <summary>
        /// 打开控制面板辅助选项声音
        /// </summary>
        public void 打开控制面板辅助选项声音()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL access.cpl,,2");
        }

        /// <summary>
        /// 打开控制面板辅助选项显示
        /// </summary>
        public void 打开控制面板辅助选项显示()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL access.cpl,,3");
        }

        /// <summary>
        /// 打开控制面板辅助选项鼠标
        /// </summary>
        public void 打开控制面板辅助选项鼠标()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL access.cpl,,4");
        }

        /// <summary>
        /// 打开控制面板辅助选项常规
        /// </summary>
        public void 打开控制面板辅助选项常规()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL access.cpl,,5");
        }

        /// <summary>
        /// 打开控制面板添加新硬件向导
        /// </summary>
        public void 打开控制面板添加新硬件向导()
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL sysdm.cpl @1");
        }

        /// <summary>
        /// 打开控制面板添加新打印机向导
        /// </summary>
        public void 打开控制面板添加新打印机向导()
        {
            Process.Start("rundll32.exe", "shell32.dll,SHHelpShortcuts_RunDLL AddPrinter");
        }

        /// <summary>
        /// 打开控制面板添加删除程序安装卸载面板
        /// </summary>
        public void 打开控制面板添加删除程序安装卸载面板()
        {
            Process.Start("rundll32.exe", "shell32.dll,shell32.dll,Control_RunDLL appwiz.cpl,,1");
        }

        /// <summary>
        /// 打开控制面板添加删除程序安装Windows面板
        /// </summary>
        public void 打开控制面板添加删除程序安装Windows面板()
        {
            Process.Start("rundll32.exe", "shell32.dll,shell32.dll,Control_RunDLL appwiz.cpl,,2");
        }

        /// <summary>
        /// 打开控制面板添加删除程序启动盘面板
        /// </summary>
        public void 打开控制面板添加删除程序启动盘面板()
        {
            Process.Start("rundll32.exe", "shell32.dll,shell32.dll,Control_RunDLL appwiz.cpl,,3");
        }

        /// <summary>
        /// 打开建立快捷方式对话框
        /// </summary>
        public void 打开建立快捷方式对话框()
        {
            Process.Start("rundll32.exe", " appwiz.cpl,NewLinkHere %1");
        }

        /// <summary>
        /// 打开日期时间选项
        /// </summary>
        public void 打开日期时间选项()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL timedate.cpl,,0");
        }

        /// <summary>
        /// 打开时区选项
        /// </summary>
        public void 打开时区选项()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL timedate.cpl,,1");
        }

        /// <summary>
        /// 建立公文包
        /// </summary>
        public void 建立公文包()
        {
            Process.Start("rundll32.exe", " syncui.dll,Briefcase_Create");
        }

        /// <summary>
        /// 打开复制软碟窗口
        /// </summary>
        public void 打开复制软碟窗口()
        {
            Process.Start("rundll32.exe", " diskcopy.dll,DiskCopyRunDll");
        }

        /// <summary>
        /// 打开新建拨号连接
        /// </summary>
        public void 打开新建拨号连接()
        {
            Process.Start("rundll32.exe", " rnaui.dll,RnaWizard");
        }

        /// <summary>
        /// 打开显示属性背景
        /// </summary>
        public void 打开显示属性背景()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL desk.cpl,,0");
        }

        /// <summary>
        /// 打开显示属性屏幕保护
        /// </summary>
        public void 打开显示属性屏幕保护()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL desk.cpl,,1");
        }

        /// <summary>
        /// 打开显示属性外观
        /// </summary>
        public void 打开显示属性外观()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL desk.cpl,,2");
        }

        /// <summary>
        /// 打开显示属性属性
        /// </summary>
        public void 打开显示属性属性()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL desk.cpl,,3");
        }

        /// <summary>
        /// 打开格式化对话框
        /// </summary>
        public void 打开格式化对话框()
        {
            Process.Start("rundll32.exe", " shell32.dll,SHFormatDrive");
        }

        /// <summary>
        /// 打开控制面板游戏控制器一般
        /// </summary>
        public void 打开控制面板游戏控制器一般()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL joy.cpl,,0");
        }

        /// <summary>
        /// 打开控制面板游戏控制器进阶
        /// </summary>
        public void 打开控制面板游戏控制器进阶()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL joy.cpl,,1");
        }

        /// <summary>
        /// 打开控制面板键盘属性速度
        /// </summary>
        public void 打开控制面板键盘属性速度()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL main.cpl @1");
        }

        /// <summary>
        /// 打开控制面板键盘属性语言
        /// </summary>
        public void 打开控制面板键盘属性语言()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL main.cpl @1,,1");
        }

        /// <summary>
        /// 打开Windows打印机档案夹
        /// </summary>
        public void 打开Windows打印机档案夹()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL main.cpl @2");
        }

        /// <summary>
        /// 打开Windows字体档案夹
        /// </summary>
        public void 打开Windows字体档案夹()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL main.cpl @3");
        }

        /// <summary>
        /// 打开控制面板输入法属性
        /// </summary>
        public void 打开控制面板输入法属性()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL main.cpl @4");
        }

        /// <summary>
        /// 打开添加新调制解调器向导
        /// </summary>
        public void 打开添加新调制解调器向导()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL modem.cpl,,add");
        }

        /// <summary>
        /// 打开控制面板多媒体属性音频
        /// </summary>
        public void 打开控制面板多媒体属性音频()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL mmsys.cpl,,0");
        }

        /// <summary>
        /// 打开控制面板多媒体属性视频
        /// </summary>
        public void 打开控制面板多媒体属性视频()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL mmsys.cpl,,1");
        }

        /// <summary>
        /// 打开控制面板多媒体属性MIDI
        /// </summary>
        public void 打开控制面板多媒体属性MIDI()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL mmsys.cpl,,2");
        }

        /// <summary>
        /// 打开控制面板多媒体属性CD音乐
        /// </summary>
        public void 打开控制面板多媒体属性CD音乐()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL mmsys.cpl,,3");
        }

        /// <summary>
        /// 打开控制面板多媒体属性设备
        /// </summary>
        public void 打开控制面板多媒体属性设备()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL mmsys.cpl,,4");
        }

        /// <summary>
        /// 打开控制面板声音
        /// </summary>
        public void 打开控制面板声音()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL mmsys.cpl @1");
        }

        /// <summary>
        /// 打开控制面板网络
        /// </summary>
        public void 打开控制面板网络()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL netcpl.cpl");
        }

        /// <summary>
        /// 打开控制面板密码
        /// </summary>
        public void 打开控制面板密码()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL password.cpl");
        }

        /// <summary>
        /// 打开控制面板电源管理
        /// </summary>
        public void 打开控制面板电源管理()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL powercfg.cpl");
        }

        /// <summary>
        /// 打开控制面板区域设置属性区域设置
        /// </summary>
        public void 打开控制面板区域设置属性区域设置()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL intl.cpl,,0");
        }

        /// <summary>
        /// 打开控制面板区域设置属性数字选项
        /// </summary>
        public void 打开控制面板区域设置属性数字选项()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL intl.cpl,,1");
        }

        public void 打开控制面板区域设置属性货币选项()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL intl.cpl,,2");
        }

        /// <summary>
        /// 打开控制面板区域设置属性时间选项
        /// </summary>
        public void 打开控制面板区域设置属性时间选项()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL intl.cpl,,3");
        }

        /// <summary>
        /// 打开控制面板区域设置属性日期选项
        /// </summary>
        public void 打开控制面板区域设置属性日期选项()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL intl.cpl,,4");
        }

        /// <summary>
        /// 打开ODBC数据源管理器
        /// </summary>
        public void 打开ODBC数据源管理器()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL odbccp32.cpl");
        }

        /// <summary>
        /// 打开控制面板系统属性常规
        /// </summary>
        public void 打开控制面板系统属性常规()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL sysdm.cpl,,0");
        }

        /// <summary>
        /// 打开控制面板系统属性设备管理器
        /// </summary>
        public void 打开控制面板系统属性设备管理器()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL sysdm.cpl,,1");
        }

        /// <summary>
        /// 打开控制面板系统属性硬件配置
        /// </summary>
        public void 打开控制面板系统属性硬件配置()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL sysdm.cpl,,2");
        }

        /// <summary>
        /// 打开控制面板系统属性性能
        /// </summary>
        public void 打开控制面板系统属性性能()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL sysdm.cpl,,3");
        }

        /*shutdown -s -t 3600 -f 
        一小时后强行关机 用强行主要怕有些程序卡住 关不了机 
        -s 关机 
        -r重启 
        -f强行 
        -t 时间 
        -a 取消关机 
        -l 注销 
        -i 显示用户界面 具体是什么试试就知道了*/

        /// <summary>
        /// 关闭并重启计算机
        /// </summary>
        public void 关闭并重启计算机()
        {
            Process.Start("shutdown.exe", "-r");
        }

        /// <summary>
        /// 关闭计算机
        /// </summary>
        public void 关闭计算机()
        {
            Process.Start("shutdown.exe", "-s -f");
        }
        

        /// <summary>
        /// 重载关闭计算机函数，可以设定倒计时
        /// </summary>
        /// <param name="time">倒计时</param>
        public void 关闭计算机(string time)
        {
            string s = "-s -t " + time;
            Process.Start("shutdown.exe", s);
        }

        /// <summary>
        /// 注销计算机
        /// </summary>
        public void 注销计算机()
        {
            Process.Start("shutdown.exe", "-l");
        }

        /// <summary>
        /// 撤销关闭计算机
        /// </summary>
        public void 撤销关闭计算机()
        {
            Process.Start("shutdown.exe", "-a");
        }

        /// <summary>
        /// 打开桌面主旨面板
        /// </summary>
        public void 打开桌面主旨面板()
        {
            Process.Start("rundll32.exe", " shell32.dll,Control_RunDLL themes.cpl");
        }

        /// <summary>
        /// 打开网址
        /// </summary>
        /// <param name="address">网址</param>
        public void 打开网址(string address)
        {
            Process.Start(address);
        }

        /// <summary>
        /// 运行程序
        /// </summary>
        /// <param name="name">程序名称</param>
        public void 运行程序(string name)
        {
            Process.Start(name);
        }

        /// <summary>
        /// 显示任务栏
        /// </summary>
        public void 显示任务栏()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", null), SW_SHOW);
        }

        /// <summary>
        /// 隐藏任务栏
        /// </summary>
        public void 隐藏任务栏()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", null), SW_HIDE);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="address">邮件地址</param>
        public void 发送邮件(string address)
        {
            string s = "mailto:" + address;
            Process.Start(s);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        public void 发送邮件()
        {
            Process.Start("mailto:406590790@qq.com");
        }

        /// <summary>
        /// 获取系统文件夹
        /// </summary>
        /// <returns></returns>
        public string 获取系统文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.System);
            return s;
        }

        /// <summary>
        /// 打开系统文件夹
        /// </summary>
        public void 打开系统文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.System);
            Process.Start(s);
        }

        /// <summary>
        /// 获取ProgramFiles目录
        /// </summary>
        /// <returns></returns>
        public string 获取ProgramFiles目录()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            return s;
        }

        /// <summary>
        /// 打开ProgramFiles目录
        /// </summary>
        public void 打开ProgramFiles目录()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            Process.Start(s);
        }

        /// <summary>
        /// 获取逻辑桌面
        /// </summary>
        /// <returns></returns>
        public string 获取逻辑桌面()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            return s;
        }

        /// <summary>
        /// 打开逻辑桌面
        /// </summary>
        public void 打开逻辑桌面()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Process.Start(s);
        }

        /// <summary>
        /// 获取启动程序组
        /// </summary>
        /// <returns></returns>
        public string 获取启动程序组()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            return s;
        }

        /// <summary>
        /// 打开启动程序组
        /// </summary>
        public void 打开启动程序组()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            Process.Start(s);
        }

        /// <summary>
        /// 获取Cookies文件夹
        /// </summary>
        /// <returns></returns>
        public string 获取Cookies文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
            return s;
        }

        /// <summary>
        /// 打开Cookies文件夹
        /// </summary>
        public void 打开Cookies文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
            Process.Start(s);
        }

        /// <summary>
        /// 获取Internet历史文件夹
        /// </summary>
        /// <returns></returns>
        public string 获取Internet历史文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.History);
            return s;
        }

        /// <summary>
        /// 打开Internet历史文件夹
        /// </summary>
        public void 打开Internet历史文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.History);
            Process.Start(s);
        }

        /// <summary>
        /// 获取我的电脑文件夹
        /// </summary>
        /// <returns></returns>
        public string 获取我的电脑文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            return s;
        }

        /// <summary>
        /// 打开我的电脑文件夹
        /// </summary>
        public void 打开我的电脑文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            Process.Start(s);
        }

        /// <summary>
        /// 获取MyMusic文件夹
        /// </summary>
        /// <returns></returns>
        public string 获取MyMusic文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            return s;
        }

        /// <summary>
        /// 打开MyMusic文件夹
        /// </summary>
        public void 打开MyMusic文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            Process.Start(s);
        }

        /// <summary>
        /// 获取MyPictures文件夹
        /// </summary>
        /// <returns></returns>
        public string 获取MyPictures文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            return s;
        }

        /// <summary>
        /// 打开MyPictures文件夹
        /// </summary>
        public void 打开MyPictures文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            Process.Start(s);
        }

        /// <summary>
        /// 获取StartMenu文件夹
        /// </summary>
        /// <returns></returns>
        public string 获取StartMenu文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            return s;
        }

        /// <summary>
        /// 打开StartMenu文件夹
        /// </summary>
        public void 打开StartMenu文件夹()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            Process.Start(s);
        }  
    }
}
