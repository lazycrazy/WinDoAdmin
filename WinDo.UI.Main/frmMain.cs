using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using WinDoControls.Controls;
using System.Drawing.Drawing2D;
using WinDo.UI.Utilities;
using WinDoControls;
using WinDo.UI.Utilities.DialogForm;
using WinDo.Utilities.PublicResource;
using WinDo.UI.Manage;
using WinDo.Utilities;
using WinDoControls.Forms;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using WinDo.Utilities.PublicLibrary;

namespace WinDo.UI.Main
{
    public partial class frmMain : BaseForm
    {
        UCLoginUserInfo dropDownUser = new UCLoginUserInfo();
        public frmMain()
        {
            InitializeComponent();
            panelTitle.Paint += PanelTitle_Paint;
            panelBottom.Paint += PanelBottom_Paint;
            panelBottom.MouseMove += PanelBottom_MouseMove;
            panelBottom.MouseClick += PanelBottom_MouseClick;

            panelTitle.MouseMove += new MouseEventHandler(Form1_MouseMove);
            panelTitle.MouseDown += new MouseEventHandler(Form1_MouseDown);
            panelTitle.DoubleClick += new EventHandler(panelTitle_DoubleClick);

            FormHelper.TabsPanel = this.panelTabs;
            SetTabMenus();

            this.WindowState = FormWindowState.Normal;
            var wa = Screen.FromHandle(this.Handle).WorkingArea;
            //this.MaximizedBounds = Screen.FromControl(this).WorkingArea;
            //this.MaximumSize = wa.Size;
            this.Size = wa.Size;
            this.Location = wa.Location;

            Load += new EventHandler(Form1_Load);
            backgoundLabel = new Label() { Text = "", Font = WDFonts.BackgroundTextFont, TextAlign = ContentAlignment.MiddleCenter };
            backgoundLabel.Dock = DockStyle.Fill;
            backgoundLabel.Margin = new Padding(3);
            backgoundLabel.BackColor = WDColors.WhiteColor;
            backgoundLabel.Paint += BackgoundLabel_Paint;

            //backgoundLabel.Padding = new Padding(2);
            this.Controls.Add(this.backgoundLabel);
            panelMain.Visible = false;
            backgoundLabel.BringToFront();
            //CreateCover();

            btnRegister.BtnClick += new EventHandler(btnRegister_BtnClick);
            btnHandRegister.BtnClick += new EventHandler(btnHandRegister_BtnClick);
            btnLogoff.BtnClick += new EventHandler(btnLogoff_BtnClick);
            panelClose.Click += new EventHandler(lblClose_Click);
            panelMain.VisibleChanged += new EventHandler(panelMain_VisibleChanged);

            SetLogoutTick();
            FormHelper.MainForm = this;
            FormClosing += new FormClosingEventHandler(frmMain_FormClosing);
            this.SizeChanged += FrmMain_SizeChanged;

            //Thread T = new Thread(new ThreadStart(ReceiveAutoUpdateMsg));
            //T.IsBackground = true;
            //T.Start();
            Microsoft.Win32.SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;


            lblMini.Paint += LblMini_Paint;
            lblMini.MouseEnter += LblMini_MouseHover;
            lblMini.MouseLeave += LblMini_MouseLeave;
            btnLogoff.lbl.MouseEnter += BtnLogoff_LblMouseHover;
            btnLogoff.lbl.MouseLeave += BtnLogoff_LblMouseLevel;
            this.tablessControl1.Margin = new Padding(0);

            this.Text = "WinDo Admin";
            ControlHelper.SetControlsDouble(this);
            ControlHelper.SetCloseBackColor(panelClose, true);
        }

        private void PanelBottom_MouseMove(object sender, MouseEventArgs e)
        {
            var bRect = panelBottom.ClientRectangle;
            bRect.Width = 80;
            bRect.Height = 20;
            bRect.Offset(panelBottom.ClientRectangle.Width - bRect.Width - 5, (panelBottom.ClientRectangle.Height - bRect.Height) / 2);
            if (bRect.Contains(e.Location))
            {
                panelBottom.Cursor = Cursors.Hand;
            }
            else
                panelBottom.Cursor = Cursors.Default;
        }

        private void PanelBottom_MouseClick(object sender, MouseEventArgs e)
        {
            var bRect = panelBottom.ClientRectangle;
            bRect.Width = 80;
            bRect.Height = 20;
            bRect.Offset(panelBottom.ClientRectangle.Width - bRect.Width - 5, (panelBottom.ClientRectangle.Height - bRect.Height) / 2);
            if (bRect.Contains(e.Location))
            {
                FormHelper.ShowTipsSuccess("？？？");
            }
        }

        private void BtnLogoff_LblMouseLevel(object sender, EventArgs e)
        {
            btnLogoff.BtnForeColor = Color.White;
            btnLogoff.Image = WDImages.Logout;
        }

        private void BtnLogoff_LblMouseHover(object sender, EventArgs e)
        {
            btnLogoff.BtnForeColor = WDColors.red5;
            btnLogoff.Image = WDImages.LogoutRed;
        }

        private void LblMini_MouseLeave(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            lbl.BackColor = WDColors.geekblue6;
        }

        private void LblMini_MouseHover(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            lbl.BackColor = Color.FromArgb(200, WDColors.geekblue6);
        }

        private void SystemEvents_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            if (e.Mode == Microsoft.Win32.PowerModes.Resume)
            {
                ReConnectDB();
                ReConnectDB();
                timerLogOff.Start();
            }
            else if (e.Mode == Microsoft.Win32.PowerModes.Suspend)
            {
                timerLogOff.Stop();
            }
        }

        private void ReConnectDB()
        {
            try
            {
                LogHelper.WriteHourLog("ReConnectDB", "电脑睡眠恢复重连数据库激活网络");
            }
            catch (Exception)
            {
            }
        }

        private void BackgoundLabel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("Do Something Amazing", WDFonts.BackgroundTextFont36, Brushes.LightGray, this.backgoundLabel.ClientRectangle, WDList.StringFormatCenter);
        }

        private void PanelBottom_Paint(object sender, PaintEventArgs e)
        {
            using (var sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
            {
                var rect = panelBottom.ClientRectangle;
                rect.Offset(10, 0);
                e.Graphics.DrawString(FormHelper.CurrentVersion, WDFonts.TextFont, Brushes.White, rect, sf);
            }
        }
        private void PanelTitle_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SetGDIHigh();
            e.Graphics.DrawIcon(Properties.Resources.Icon, new Rectangle(2, 2, 20, 20));
            var sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            var rect = panelTitle.ClientRectangle;
            rect.Offset(24, 2);
            e.Graphics.DrawString(this.Text + " - LazyCrazy", WDFonts.TextFont, Brushes.White, rect, sf);
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            panelTitle.Invalidate();
            panelBottom.Invalidate();
        }

        //Panel CoverPanel = new Panel();
        //private void CreateCover()
        //{
        //    CoverPanel.Location = new Point();
        //    CoverPanel.Size = this.Size;
        //    CoverPanel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
        //    CoverPanel.Visible = false;
        //    CoverPanel.BackColor = Color.White;
        //    this.Controls.Add(CoverPanel);
        //}

        //void ShowCover()
        //{
        //    this.panelMain.Visible = false;
        //    CoverPanel.BringToFront();
        //    CoverPanel.Visible = true;
        //}
        //void HideCover()
        //{
        //    CoverPanel.Visible = false;
        //    CoverPanel.SendToBack();
        //}
        #region Tab菜单项功能菜单
        private void SetTabMenus()
        {
            TabBtns = new[] { //WinDoImages.GetBtnIconImage("I_refresh"),
                WDImages.GetBtnIconImage("I_leftarrow_clear",16),
                WDImages.GetBtnIconImage("I_rightarrow_clear",16),
                WDImages.GetBtnIconImage("I_downarrow_clear",16) };

            var panel = this.panelTabMenu;
            panel.BackColor = Color.White;
            panel.Width = 100;
            panel.Paint += TabMenusPanel_Paint;
            panel.MouseClick += TabMenusPanel_MouseClick;
            panel.MouseMove += TabMenusPanel_MouseMove;
            panel.MouseLeave += TabMenusPanel_MouseLeave;
        }

        private void TabMenusPanel_MouseLeave(object sender, EventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;
            panel.Cursor = Cursors.Default;
        }

        private Rectangle EmptyRect = new Rectangle();
        //private Rectangle lastRect;
        private void TabMenusPanel_MouseMove(object sender, MouseEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;
            var rects = new[] { //new Rectangle(10, 5, 20, 20), 
                new Rectangle(10, 5, 20, 20),
                new Rectangle(40, 5, 20, 20),
                new Rectangle(70, 5, 20, 20) };
            var curRect = (rects.FirstOrDefault(r => r.Contains(e.Location)));
            //if (curRect == EmptyRect)
            //{
            //    lastRect = EmptyRect;
            //    panel.Cursor = Cursors.Default;
            //    return;
            //}
            //if (lastRect == curRect) return;
            //lastRect = curRect;
            panel.Cursor = curRect != EmptyRect ? Cursors.Hand : Cursors.Default;

        }

        private void TabMenusPanel_MouseClick(object sender, MouseEventArgs e)
        {
            var panel = sender as Panel;
            var rects = new[] { //new Rectangle(10, 5, 20, 20), 
                new Rectangle(10, 5, 20, 20),
                new Rectangle(40, 5, 20, 20),
                new Rectangle(70, 5, 20, 20) };
            if (rects[0].Contains(e.Location))
            {
                if (frmAnchorTab != null)
                {
                    frmAnchorTab.Close();
                    frmAnchorTab = null;
                }
                var items = panelTabs.Controls.Cast<WDTabItem>().ToList();
                if (items.All(i => i.Visible)) return;
                panelTabs.SuspendLayout();
                foreach (var item in items.Where(i => !i.Visible))
                    item.Visible = true;
                panelTabs.ResumeLayout(true);
                //var curIdx = items.FindIndex(i => i.Selected);
                //if (curIdx > 0)
                //{
                //    items[curIdx - 1].Selected = true;
                //}
            }
            else if (rects[1].Contains(e.Location))
            {
                if (frmAnchorTab != null)
                {
                    frmAnchorTab.Close();
                    frmAnchorTab = null;
                }
                var items = panelTabs.Controls.Cast<WDTabItem>().ToList();
                if (items.Any(i => !i.Visible)) return;
                if (panelTabs.PreferredSize.Width > panelTabs.Width)
                {
                    var swidth = 0;
                    var hideItems = new List<WDTabItem>();
                    foreach (var item in items)
                    {
                        swidth += item.Width;
                        if (swidth <= panelTabs.Width)
                            hideItems.Add(item);
                    }
                    panelTabs.SuspendLayout();
                    foreach (var item in hideItems)
                        item.Visible = false;
                    panelTabs.ResumeLayout(true);
                }

                //var curIdx = items.FindIndex(i => i.Selected);
                //if (curIdx < items.Count - 1)
                //{
                //    items[curIdx + 1].Selected = true;
                //}
            }
            else if (rects[2].Contains(e.Location))
            {
                if (frmAnchorTab == null)
                {
                    var ucPanel = new WDCtrlBase() { IsRadius = true, ConerRadius = 5, Padding = new Padding(2) };
                    ucPanel.IsShowRect = true;
                    ucPanel.Size = new Size(100, 95);
                    ucPanel.BackColor = Color.White;
                    ucPanel.FillColor = Color.White;
                    //弹出下拉框
                    //var cpanel = new FlowLayoutPanel() { FlowDirection = FlowDirection.TopDown, BackColor = Color.White, Dock = DockStyle.Fill };
                    var lbls = new[] {
                    new Label() {  Text = "关闭当前标签页",TextAlign = ContentAlignment.MiddleCenter },
                    new Label() { Text = "关闭其它标签页",TextAlign = ContentAlignment.MiddleCenter },
                    new Label() { Text = "关闭全部标签页",TextAlign = ContentAlignment.MiddleCenter } };
                    foreach (var lbl in lbls)
                    {
                        lbl.Cursor = Cursors.Hand;
                        lbl.Font = WDFonts.TextFont;
                        lbl.Dock = DockStyle.Top;
                        lbl.Height = 30;
                        AutoSize = false;
                        BackColor = Color.White;
                        lbl.MouseClick += (ss, ee) =>
                        {
                            var clbl = ss as Label;
                            if (clbl.Text == "关闭当前标签页")
                            {
                                var items = panelTabs.Controls.Cast<WDTabItem>().ToList();
                                var curTab = items.FirstOrDefault(i => i.Selected);
                                if (curTab != null)
                                    curTab.OnlblClose_Click();
                            }
                            else if (clbl.Text == "关闭其它标签页")
                            {
                                var items = panelTabs.Controls.Cast<WDTabItem>().Where(i => !i.Selected).ToList();
                                foreach (var item in items)
                                {
                                    item.OnlblClose_Click();
                                }
                            }
                            else if (clbl.Text == "关闭全部标签页")
                            {
                                var items = panelTabs.Controls.Cast<WDTabItem>().Where(i => !i.Selected).ToList();
                                foreach (var item in items)
                                {
                                    item.OnlblClose_Click();
                                }
                                items = panelTabs.Controls.Cast<WDTabItem>().ToList();
                                var curTab = items.FirstOrDefault(i => i.Selected);
                                if (curTab != null)
                                    curTab.OnlblClose_Click();
                            }
                            frmAnchorTab.Close();
                            frmAnchorTab = null;
                        };
                    }
                    //cpanel.Controls.AddRange(lbls);
                    //ucPanel.Controls.Add(cpanel);
                    ucPanel.Controls.AddRange(lbls);
                    frmAnchorTab = new FrmAnchor(panel, ucPanel, new Point(-23, 0));
                    frmAnchorTab.Load += (ss, ee) => { frmAnchorTab.Size = new Size(120, 95); };
                    frmAnchorTab.Show(this);
                }
                else
                {
                    frmAnchorTab.Visible = !frmAnchorTab.Visible;
                }
            }
        }
        FrmAnchor frmAnchorTab;
        private Image[] TabBtns;

        private void TabMenusPanel_Paint(object sender, PaintEventArgs e)
        {
            var p = new Point(10, 5);
            foreach (var img in TabBtns)
            {
                //e.Graphics.DrawRectangle(Pens.Pink, new Rectangle(p, new Size(20, 20)));
                e.Graphics.DrawImage(img, p);
                p.Offset(30, 0);
            }
        }
        #endregion

        void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.ApplicationExitCall) return;
            if (FrmShadowDialog.ShowAskDialog(this, "是否确定关闭？", "退出", true, false, true, true) == DialogResult.Cancel)
            {

                e.Cancel = true;
                return;
            }
        }

        #region 设置计时退出

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        //获取键盘和鼠标没有操作的时间  
        private static long GetLastInputTime()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = Marshal.SizeOf(vLastInputInfo);
            // 捕获时间  
            if (!GetLastInputInfo(ref vLastInputInfo))
                return 0;
            else
            {
                //Console.WriteLine("TickCount=" + Environment.TickCount);
                //Console.WriteLine("vLastInputInfo.dwTime=" + vLastInputInfo.dwTime);
                //Console.WriteLine("vLastInputInfo.dwTime & Int32.MaxValue=" + (vLastInputInfo.dwTime & Int32.MaxValue));
                var count = (Environment.TickCount & Int32.MaxValue) - (long)(vLastInputInfo.dwTime & Int32.MaxValue);
                //var icount = count / 1000;
                return count;
                //return Environment.TickCount - (long)vLastInputInfo.dwTime;
            }
        }

        // 创建结构体用于返回捕获时间  
        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            // 设置结构体块容量  
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            // 捕获的时间  
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }


        private void SetLogoutTick()
        {
            //加载时间设置配置

            //当前用户职务，配置的超时时间设置   SysVal2，X分钟后自动注销
            var zwcs = PublicRes.GetDicByTypeAndVal("职务", PublicRes.CurUser.Title_ID.AsString());
            if (zwcs != null && zwcs.SysVal2.AsInt() > 0)
                _tickcount = zwcs.SysVal2.AsInt() * _minute;

            // 客户端登出时间设置 本机客户端ID 
            var t = PublicRes.GetConfig("C_SYSTEM_LOGTIMEOUT", "0");
            if (t.AsInt() > 0)
            {
                _tickcount = t.AsInt() * _minute;
            }

            tickcount = _tickcount;
            //计时
            //this.HandleCreated += frmMain_HandleCreated;
            //this.HandleDestroyed += frmMain_HandleDestroyed;

            timerLogOff = new System.Windows.Forms.Timer();
            timerLogOff.Interval = 1000;//1秒
            timerLogOff.Tick += timer_Tick;
            timerLogOff.Start();
        }
        System.Windows.Forms.Timer timerLogOff;

        void timer_Tick(object sender, EventArgs e)
        {
            long nTime = GetLastInputTime();
            //Console.WriteLine(nTime);
            var InactiveTime = Convert.ToInt32(nTime / 60000);
            //Console.WriteLine(InactiveTime);
            //if (lblTick.Visible)
            //    lblTick.Text = string.Format("{1} 分 {2} 秒后退出", tickcount, tickcount - InactiveTime - 1, ((tickcount * 60000 - nTime) - (tickcount - InactiveTime - 1) * 60000) / 1000);
            if (InactiveTime >= tickcount)
            {
                LogHelper.WriteHourLog("timer_Tick", "自动退出，客户端自动退出时间：" + tickcount);
                Restart();
            }
        }

        public static void Restart()
        {
            //Application.Restart();
            //Environment.Exit(0);

            using (Process p = new Process())
            {
                p.StartInfo.WorkingDirectory = System.Windows.Forms.Application.StartupPath;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序
                var processName = Process.GetCurrentProcess().MainModule.ModuleName;
                var exePath = Application.ExecutablePath;
                var cmd = string.Format(@"taskkill /f /im {0} & start {1} {2} &exit", processName, processName, "WinDo:" + PublicRes.CurUser.LoginName); //说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
                //向cmd窗口写入命令
                p.StandardInput.AutoFlush = true;
                p.StandardInput.WriteLine(cmd);
                LogHelper.WriteHourLog("Restart", cmd);
                //获取cmd窗口的输出信息
                //var output = p.StandardOutput.ReadToEnd();
                //var success = (output.TrimEnd(Environment.NewLine.ToCharArray()).EndsWith("0"));
                //if (!success)
                //{
                //    msg = output.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Last();
                //}
                p.WaitForExit();//等待程序执行完退出进程                 
            }

            //可能没vbs权限？延时
            //            if (!File.Exists("./Update.vbs"))
            //            {
            //                File.WriteAllText("./Update.vbs", @"Wscript.sleep 1000*60
            //Set ws = CreateObject(""WScript.Shell"") 
            //ws.Run ""taskkill /f /im WinDo.UI.Main.exe"", vbhide
            //Wscript.sleep 100
            //ws.Run ""WinDo.UI.Main.exe"", vbhide
            //Wscript.quit");
            //            }
            //            Process.Start("Update.vbs");
            //Environment.Exit(0);

        }
        public static int tickcount = 30 * _minute;
        private int _tickcount = 30 * _minute;
        public static int _minute = 1;

        //public bool PreFilterMessage(ref Message m)
        //{
        //    //如果检测到有鼠标或则键盘的消息，则使计数为0.....
        //    if (m.Msg == 0x100 || m.Msg == 0x101 || (m.Msg > 0x199 && m.Msg < 0x207) || m.Msg == 0x20a)
        //    {
        //        frmMain.tickcount = _tickcount;
        //    }

        //    return false;
        //}

        //private void frmMain_HandleDestroyed(object sender, EventArgs e)
        //{
        //    Application.RemoveMessageFilter(this);
        //}

        //private void frmMain_HandleCreated(object sender, EventArgs e)
        //{
        //    Application.AddMessageFilter(this);
        //}

        #endregion

        private int _BorderSize = 1;
        private Color _BorderColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;

        /// <summary>
        /// 重写OnPaint方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics,
                            this.ClientRectangle,
                            this._BorderColor,
                            this._BorderSize,
                            ButtonBorderStyle.Solid,
                            this._BorderColor,
                            this._BorderSize,
                            ButtonBorderStyle.Solid,
                           this._BorderColor,
                            this._BorderSize,
                            ButtonBorderStyle.Solid,
                            this._BorderColor,
                            this._BorderSize,
                            ButtonBorderStyle.Solid);
        }



        void panelMain_VisibleChanged(object sender, EventArgs e)
        {
            backgoundLabel.Visible = !this.tablessControl1.Visible;
            if (backgoundLabel.Visible)
                backgoundLabel.BringToFront();
            //if (backgoundLabel.Visible)
            //    Text = WinDo.Utilities.PublicRes.logininfo.SysName;
        }
        Label backgoundLabel = null;


        void btnLogoff_BtnClick(object sender, EventArgs e)
        {
            //退出当前用户，重新登录
            if (DialogResult.OK == FrmShadowDialog.ShowAskDialog(this, "是否退出登录？", "注销", true, false, true, true))
            {
                //重启
                Restart();
                ////注销
            }

        }

        /// <summary>
        /// 手工登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnHandRegister_BtnClick(object sender, EventArgs e)
        {
            //直接打开手工登记页面录入客户数据
            var formName = "WinDo.UI.Diagnosis.frmPatient";
            CloseTabWithFormName(formName);
            var form = OpenRegisterForm();
        }


        public override BaseForm OpenNewForm(WDMenuInfo menuInfo, Action<BaseForm> beforeLoadAction = null)
        {
            return NewTab(menuInfo, beforeLoadAction);
        }


        public BaseForm OpenRegisterForm()
        {
            return NewTab(new WDMenuInfo()
            {
                Text = "登记",
                CommandText = "OpenForm",
                FormName = "WinDo.UI.Manage.frmControlDemo",
                AssemblyName = "WinDo.UI.Manage.dll"
            });
        }

        /// <summary>
        /// 登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnRegister_BtnClick(object sender, EventArgs e)
        {
            //接收录入的客户ID
            patIDForm = new FrmTextBoxWithOk();
            //frm.Redraw = false;
            //frm.IsShowRegion = false;
            patIDForm.InputTextBox.valueControl.txtInput.MaxLength = 20;
            patIDForm.Size = new System.Drawing.Size(375, 210);
            patIDForm.InputTextBox.Width = 200;
            patIDForm.InputTextBox.Location = new Point(30, 75);
            patIDForm.InputTextBox.Width = 300;
            patIDForm.MustInput = true;
            patIDForm.Title = "请录入";
            patIDForm.InputTextBox.label.TextValue = "ID：";
            patIDForm.verification.SetVerificationModel(patIDForm.InputTextBox.valueControl, WinDoControls.Controls.VerificationModel.AnyChar);
            patIDForm.BtnOK.BtnText = "提取";
            patIDForm.BtnOK.IconName = "I_upload";
            patIDForm.BtnOK.BtnClick += new EventHandler(BtnOK_BtnClick);

            if (DialogResult.Cancel == patIDForm.ShowDialog(this)) return;

        }
        private FrmTextBoxWithOk patIDForm;
        void BtnOK_BtnClick(object sender, EventArgs e)
        {
            if (!patIDForm.verification.Verification())
            {
                return;
            }

            var ID = patIDForm.InputTextBox.valueControl.InputText.Trim();
            //根据ID加载数据打开界面
        }


        void Form1_Load(object sender, EventArgs e)
        {

            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            //this.MaximumSize = Screen.FromHandle(this.Handle).WorkingArea.Size;
            //this.Location = new Point();
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            //tabControlExt1.HeadSelectedBorderColor = ColorTranslator.FromHtml("#fddcc4");
            SetMenu();
            //tabControlExt1.IsShowCloseBtn = false;

            //PAT_REG001	登记
            btnRegister.Visible = PublicRes.HasPrivilege(PrivilegesEnum.登记);
            //PAT_REG002	手工登记
            btnHandRegister.Visible = PublicRes.HasPrivilege(PrivilegesEnum.手工登记);
            dropDownUser.BtnText = PublicRes.CurUser.RealName;
        }

        private void SetMenu()
        {
            //管理员头像
            dropDownUser.LeftImage = WDImages.PersonFill;
            dropDownUser.Btns = new string[] { "基本资料", "修改密码", "客户端配置" };
            dropDownUser.BtnClick += new EventHandler(dropDownUser_BtnClick);
            dropDownUser.BackColor = this.flowLayoutPanel1.BackColor;
            this.flowLayoutPanel1.Controls.Add(dropDownUser);
            flowLayoutPanel1.Controls.SetChildIndex(dropDownUser, 1);

            btnLogoff.Image = WDImages.Logout;
            btnHandRegister.Image = WDImages.Edit;
            btnRegister.Image = WDImages.Edit;
            LoadMenu();
        }

        private void LblMini_Paint(object sender, PaintEventArgs e)
        {
            var img = WDImages.Mini;
            var x = lblMini.Width - img.Width;
            var y = lblMini.Height - img.Height;
            e.Graphics.DrawImage(img, x / 2, y / 2);
        }

        void dropDownUser_BtnClick(object sender, EventArgs e)
        {
            if (sender.ToString() == dropDownUser.Btns[0])
            {
                var frm = new frmUserInfo();
                frm.ShowDialog(this);
            }
            else if (sender.ToString() == dropDownUser.Btns[1])
            {

                var frm = new frmChangePassword();
                //frm.Location = new Point(this.Location.X + (this.Width - frm.Width) - 2, this.Location.Y + this.panelTitle.Height + this.panelMenu.Height + 2);
                frm.ShowDialog(this);
            }
            else if (sender.ToString() == dropDownUser.Btns[2])
            {

                var frm = new frmClientConfig(true);
                frm.ShowDialog(this);
            }
        }

        /// <summary>
        /// ModuleInfo表新增模块或菜单
        /// 新增模块ModuleType=0,IsMenu=0
        //  新增菜单ModuleType=1,IsMenu=1,RefModeulCode=所属模块的ModuleCode
        //  新增子菜单，ModuleParentCode=父级菜单的ModuleCode
        /// </summary>
        private void LoadMenu()
        {
            this.mainMenuBar.BackColor = WDColors.geekblue6;
            this.mainMenuBar.BorderColor = WDColors.geekblue6;
            this.mainMenuBar.SeparatorColor = Color.Black;
            this.mainMenuBar.EnableBorderDrawing = true;
            this.mainMenuBar.EnableHoverBorderDrawing = true;
            this.mainMenuBar.TextColor = Color.White;
            this.mainMenuBar.HoverBackGradientColor = Color.White;
            this.mainMenuBar.HoverTextColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.mainMenuBar.HoverBorderColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.mainMenuBar.Cursor = Cursors.Hand;
            this.mainMenuBar.AlwaysShowPopupMenu = true;
            LoadMenuItems();
            //this.mainMenuBar.CurrentMenuItemChange += new CurrentMenuItemChangeEventHandler(mainMenuBar_CurrentMenuItemChange);
            OpenHomePage();
        }

        public override void LoadMenuItems()
        {
            this.mainMenuBar.MenuItems.Clear();
            //加载菜单
            var menus = PublicRes.lstUserModule;//用户菜单数据
            foreach (var item in menus.Where(m => string.IsNullOrWhiteSpace(m.ModuleParentCode.AsString())).OrderBy(m => m.SortCode).ToList())
            {
                var cur = new WDMenuItem(new WDMenuInfo()
                {
                    Text = item.ModuleName.Trim(),
                    IconName = item.ImageIndex,
                    CommandText = item.Command,
                    FormName = item.FormName,
                    AssemblyName = item.AssemblyName,
                    ModuleFormCode = item.ModuleFormCode
                });
                var subMenus = menus.Where(m => m.ModuleParentCode == item.ModuleCode).OrderBy(m => m.SortCode).ToList();
                if (item.ModuleType == 2 || subMenus.Count() > 0)//菜单叶子项，或者有子节点的菜单项，
                    this.mainMenuBar.MenuItems.Add(cur);
                if (item.ModuleType == 2)//菜单叶子项注册打开窗体事件
                {
                    cur.Click += new EventHandler(menuItem_Click);
                    continue;
                }
                if (subMenus.Count() == 0)
                    continue;
                AddSubMenu(menus.Where(m => string.IsNullOrWhiteSpace(m.ModuleParentCode.AsString())).ToList(), subMenus, cur);
            }
            //this.mainMenuBar.Invalidate();
            this.mainMenuBar.Show();
        }

        /// <summary>
        /// 打开主页
        /// </summary>
        private void OpenHomePage()
        {
            //用户>客户端>职务             
            var moduleCode = PublicRes.GetConfig(FormHelper.UserHomePageKey, "");
            //if (string.IsNullOrWhiteSpace(moduleCode))
            //{
            //    moduleCode = PublicRes.GetConfig(FormHelper.ClientHomePageKey, "");
            //}
            //if (string.IsNullOrWhiteSpace(moduleCode))
            //{
            //    if (PublicRes.CurUser.Title_ID.HasValue)
            //    {
            //        var dic = PublicRes.GetSystemDic("职务").FirstOrDefault(d => d.SysVal == PublicRes.CurUser.Title_ID.ToString());
            //        if (dic != null && !string.IsNullOrWhiteSpace(dic.DicNote))
            //            moduleCode = dic.DicNote;
            //    }
            //}
            if (string.IsNullOrWhiteSpace(moduleCode))
                return;
            var menu = PublicRes.lstModule.FirstOrDefault(m => m.IsMenu == 1 && m.ModuleCode == moduleCode);
            if (menu == null)
                return;
            if (!PublicRes.HasMenu(menu.ModuleCode))
                return;
            ActionHelper.Delay(10).ContinueWith(t =>
            {
                this.SafeBeginInvoke(() =>
                {
                    OpenNewForm(new WDMenuInfo()
                    {
                        Text = menu.ModuleName,
                        CommandText = "OpenForm",
                        FormName = menu.FormName,
                        AssemblyName = menu.AssemblyName
                    });
                });
            });
        }

        private void AddSubMenu(IEnumerable<Model.V_UserModule> allMenus, IEnumerable<Model.V_UserModule> subMenus, WDMenuItem parent)
        {
            foreach (var item in subMenus)
            {
                var cur = parent.AddChildItem(new WDMenuInfo()
                {
                    Text = item.ModuleName.Trim(),
                    IconName = item.ImageIndex,
                    CommandText = item.Command,
                    FormName = item.FormName,
                    AssemblyName = item.AssemblyName,
                    ModuleFormCode = item.ModuleFormCode
                });
                var childrens = allMenus.Where(m => m.ModuleParentCode == item.ModuleCode).OrderBy(m => m.SortCode).ToList();
                if (childrens.Count() == 0)
                {
                    cur.Click += new EventHandler(menuItem_Click);
                    continue;
                }
                AddSubMenu(allMenus, childrens, cur);
            }
        }




        void mainMenuBar_CurrentMenuItemChange(object sender, CurrentMenuItemChangeEventArgs e)
        {
            //鼠标移动选中的项
            WDMenuItem item = e.CurrentMenuItem;
            if (item == null)
            {
                //Console.WriteLine("null");
            }
            else
            {
                //Console.WriteLine(item.Text);
            }

        }



        private void menuItem_Click(object obj, System.EventArgs e)
        {
            var item = obj as WDMenuItem;
            if (item != null && !string.IsNullOrWhiteSpace(item.MenuInfo.CommandText))
            {
                if (item.MenuInfo.CommandText == "OpenURL")
                {
                    Process.Start(item.MenuInfo.FormName);
                    return;
                }
                if (item.MenuInfo.CommandText == "OpenForm")
                {
                    ActionHelper.Delay(20).ContinueWith(t =>
                    {
                        this.SafeBeginInvoke(() =>
                        {
                            NewTab(item.MenuInfo);
                        });
                    });
                }
            }
        }

        void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string[] Icons = new[] { "A_fa_volume_up", "A_fa_archive", "A_fa_arrow_left" };



        private void pictureEdit5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        int formCount = 0;

        public override bool ExistsTab(string tabText)
        {
            return panelTabs.Controls.Cast<WDTabItem>().Select(i => i.ItemText.Trim()).Contains(tabText.Trim());
        }

        BaseForm NewTab(WDMenuInfo menuInfo, Action<BaseForm> beforeLoadAction = null)
        {
            //检查权限
            if (backgoundLabel != null)
                backgoundLabel.Visible = false;
            if (!this.panelMain.Visible)
            {
                panelMain.Visible = true;
            }
            var formCaption = menuInfo.Text;
            if (!panelTabs.Controls.Cast<WDTabItem>().Select(i => i.ItemText).Contains(formCaption.Trim()))
            {
                //检查最大数
                if (panelTabs.Controls.Count > 30)
                {
                    FrmShadowDialog.ShowErrDialog(this, "打开窗体过多，请关闭不常用窗体", blnShowCancel: false);
                    return null;
                }

                //try
                //{
                //t.SuspendLayout();
                //ControlHelper.FreezeControl(page, true);
                var form = OpenForm(menuInfo.FormName, menuInfo.AssemblyName, menuInfo.ModuleFormCode, menuInfo.RelationForm as BaseForm);
                if (form == null) return null;
                //form.Dock = DockStyle.Fill;
                ////ctrl.SetLayoutControlsSize(t.Size);
                ////ctrl.SuspendLayout();
                ////page.Controls.Add(form);
                //form.Show();
                //ctrl.ResumeLayout(true);
                //t.ResumeLayout(true);
                //Thread.Sleep(100);
                //Application.DoEvents();
                //}
                //finally
                //{
                //    ControlHelper.FreezeControl(page, false);
                //}
                if (beforeLoadAction != null)
                {
                    beforeLoadAction(form);
                }
                ControlHelper.SetControlsDouble(form);
                var page = new TabPage(formCaption);
                page.BackColor = this.tablessControl1.Parent.BackColor;
                this.tablessControl1.TabPages.Add(page);
                page.Controls.Add(form);
                form.Show();
                var tabItem = new WDTabItem(panelTabs, formCaption, form, this.tablessControl1, page);
                tabItem.Selected = true;
                //ControlHelper.SetDouble(form);
                //var asc = new AutoSizeFormClass();
                //asc.controllInitializeSize(form);
                //form.Resize += (s, ee) =>
                //{
                //    asc.controlAutoSize(form);
                //};
                return form;
            }
            else
            {
                var tabItem = panelTabs.Controls.Cast<WDTabItem>().First(i => i.ItemText == formCaption.Trim());
                tabItem.Selected = true;
                return tabItem.Form;
            }
        }

        /// <summary>
        /// 根据窗体关闭对应标签页和窗体
        /// </summary>
        /// <param name="form"></param>
        public override void CloseTab(System.Windows.Forms.Control form)
        {
            var tab = panelTabs.Controls.Cast<WDTabItem>().FirstOrDefault(i => i.Form == form);
            if (tab != null)
                tab.OnlblClose_Click();
        }

        public override void CloseTabWithTabText(string tabText)
        {
            var tab = panelTabs.Controls.Cast<WDTabItem>().FirstOrDefault(i => i.ItemText == tabText);
            if (tab != null)
                tab.OnlblClose_Click();
        }
        public override void CloseTabWithFormName(string formName)
        {
            var tabs = panelTabs.Controls.Cast<WDTabItem>().Where(i =>
                i.Form.GetType().FullName == formName
            );
            foreach (var tab in tabs)
            {
                tab.OnlblClose_Click();
            }
        }

        private BaseForm OpenForm(string formName, string assemblyName, string moduleFormCode, BaseForm relationForm = null)
        {
            try
            {
                Assembly asm = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + assemblyName);////我们要调用的dll文件路径
                                                                                                                                     //加载dll后,需要使用dll中某类.
                Type t = asm.GetType(formName);//获取类名，必须 命名空间+类名  
                                               //实例化类型
                var form = Activator.CreateInstance(t) as BaseForm;
                form.ModuleFormCode = moduleFormCode;
                form.TopLevel = false;
                form.Size = this.tablessControl1.Size;
                form.Dock = DockStyle.Fill;
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                form.MainForm = this;
                form.RelationForm = relationForm;
                return form;
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开菜单报错，" + ex.Message);
            }
            return null;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_MINIMIZEBOX = 0x00020000;  // 在Winuser.h中定义
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | WS_MINIMIZEBOX;   // 允许窗体最小化操作
                return cp;
            }
        }

        private void lblMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private Point mPoint = new Point();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;
        }


        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }

        void panelTitle_DoubleClick(object sender, EventArgs e)
        {
            var state = WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
            if (state == FormWindowState.Maximized)
            {
                var wa = Screen.FromControl(this).WorkingArea;
                this.MaximizedBounds = wa;
                this.MaximumSize = wa.Size;
            }
            this.WindowState = state;
        }






        #region 拖动改变大小

        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;
        const int WM_NCHITTEST = 0x0084;//Windows用来获取鼠标命中在窗体的哪个部分
        const int HTCAPTION = 2;   //标题栏
        const int HTCLIENT = 1;   //客户区
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                                   (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息 用于实现拖动窗口功能
                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标
                    m.LParam = IntPtr.Zero;//默认值
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion


        public static StringFormat SFCenter = new StringFormat()
        {
            FormatFlags = StringFormatFlags.NoWrap,
            Trimming = StringTrimming.EllipsisCharacter,
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center
        };
    }
}
