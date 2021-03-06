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
                FormHelper.ShowTipsSuccess("?????????");
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
                LogHelper.WriteHourLog("ReConnectDB", "?????????????????????????????????????????????");
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
        #region Tab?????????????????????
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
                    //???????????????
                    //var cpanel = new FlowLayoutPanel() { FlowDirection = FlowDirection.TopDown, BackColor = Color.White, Dock = DockStyle.Fill };
                    var lbls = new[] {
                    new Label() {  Text = "?????????????????????",TextAlign = ContentAlignment.MiddleCenter },
                    new Label() { Text = "?????????????????????",TextAlign = ContentAlignment.MiddleCenter },
                    new Label() { Text = "?????????????????????",TextAlign = ContentAlignment.MiddleCenter } };
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
                            if (clbl.Text == "?????????????????????")
                            {
                                var items = panelTabs.Controls.Cast<WDTabItem>().ToList();
                                var curTab = items.FirstOrDefault(i => i.Selected);
                                if (curTab != null)
                                    curTab.OnlblClose_Click();
                            }
                            else if (clbl.Text == "?????????????????????")
                            {
                                var items = panelTabs.Controls.Cast<WDTabItem>().Where(i => !i.Selected).ToList();
                                foreach (var item in items)
                                {
                                    item.OnlblClose_Click();
                                }
                            }
                            else if (clbl.Text == "?????????????????????")
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
            if (FrmShadowDialog.ShowAskDialog(this, "?????????????????????", "??????", true, false, true, true) == DialogResult.Cancel)
            {

                e.Cancel = true;
                return;
            }
        }

        #region ??????????????????

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        //??????????????????????????????????????????  
        private static long GetLastInputTime()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = Marshal.SizeOf(vLastInputInfo);
            // ????????????  
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

        // ???????????????????????????????????????  
        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            // ????????????????????????  
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            // ???????????????  
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }


        private void SetLogoutTick()
        {
            //????????????????????????

            //????????????????????????????????????????????????   SysVal2???X?????????????????????
            var zwcs = PublicRes.GetDicByTypeAndVal("??????", PublicRes.CurUser.Title_ID.AsString());
            if (zwcs != null && zwcs.SysVal2.AsInt() > 0)
                _tickcount = zwcs.SysVal2.AsInt() * _minute;

            // ??????????????????????????? ???????????????ID 
            var t = PublicRes.GetConfig("C_SYSTEM_LOGTIMEOUT", "0");
            if (t.AsInt() > 0)
            {
                _tickcount = t.AsInt() * _minute;
            }

            tickcount = _tickcount;
            //??????
            //this.HandleCreated += frmMain_HandleCreated;
            //this.HandleDestroyed += frmMain_HandleDestroyed;

            timerLogOff = new System.Windows.Forms.Timer();
            timerLogOff.Interval = 1000;//1???
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
            //    lblTick.Text = string.Format("{1} ??? {2} ????????????", tickcount, tickcount - InactiveTime - 1, ((tickcount * 60000 - nTime) - (tickcount - InactiveTime - 1) * 60000) / 1000);
            if (InactiveTime >= tickcount)
            {
                LogHelper.WriteHourLog("timer_Tick", "?????????????????????????????????????????????" + tickcount);
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
                p.StartInfo.UseShellExecute = false;        //????????????????????????shell??????
                p.StartInfo.RedirectStandardInput = true;   //???????????????????????????????????????
                p.StartInfo.RedirectStandardOutput = true;  //?????????????????????????????????
                p.StartInfo.RedirectStandardError = true;   //???????????????????????????
                p.StartInfo.CreateNoWindow = true;          //?????????????????????
                p.Start();//????????????
                var processName = Process.GetCurrentProcess().MainModule.ModuleName;
                var exePath = Application.ExecutablePath;
                var cmd = string.Format(@"taskkill /f /im {0} & start {1} {2} &exit", processName, processName, "WinDo:" + PublicRes.CurUser.LoginName); //??????????????????????????????????????????exit????????????????????????ReadToEnd()?????????????????????????????????
                //???cmd??????????????????
                p.StandardInput.AutoFlush = true;
                p.StandardInput.WriteLine(cmd);
                LogHelper.WriteHourLog("Restart", cmd);
                //??????cmd?????????????????????
                //var output = p.StandardOutput.ReadToEnd();
                //var success = (output.TrimEnd(Environment.NewLine.ToCharArray()).EndsWith("0"));
                //if (!success)
                //{
                //    msg = output.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Last();
                //}
                p.WaitForExit();//?????????????????????????????????                 
            }

            //?????????vbs???????????????
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
        //    //???????????????????????????????????????????????????????????????0.....
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
        /// ??????OnPaint??????
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
            //?????????????????????????????????
            if (DialogResult.OK == FrmShadowDialog.ShowAskDialog(this, "?????????????????????", "??????", true, false, true, true))
            {
                //??????
                Restart();
                ////??????
            }

        }

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnHandRegister_BtnClick(object sender, EventArgs e)
        {
            //????????????????????????????????????????????????
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
                Text = "??????",
                CommandText = "OpenForm",
                FormName = "WinDo.UI.Manage.frmControlDemo",
                AssemblyName = "WinDo.UI.Manage.dll"
            });
        }

        /// <summary>
        /// ??????
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnRegister_BtnClick(object sender, EventArgs e)
        {
            //?????????????????????ID
            patIDForm = new FrmTextBoxWithOk();
            //frm.Redraw = false;
            //frm.IsShowRegion = false;
            patIDForm.InputTextBox.valueControl.txtInput.MaxLength = 20;
            patIDForm.Size = new System.Drawing.Size(375, 210);
            patIDForm.InputTextBox.Width = 200;
            patIDForm.InputTextBox.Location = new Point(30, 75);
            patIDForm.InputTextBox.Width = 300;
            patIDForm.MustInput = true;
            patIDForm.Title = "?????????";
            patIDForm.InputTextBox.label.TextValue = "ID???";
            patIDForm.verification.SetVerificationModel(patIDForm.InputTextBox.valueControl, WinDoControls.Controls.VerificationModel.AnyChar);
            patIDForm.BtnOK.BtnText = "??????";
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
            //??????ID????????????????????????
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

            //PAT_REG001	??????
            btnRegister.Visible = PublicRes.HasPrivilege(PrivilegesEnum.??????);
            //PAT_REG002	????????????
            btnHandRegister.Visible = PublicRes.HasPrivilege(PrivilegesEnum.????????????);
            dropDownUser.BtnText = PublicRes.CurUser.RealName;
        }

        private void SetMenu()
        {
            //???????????????
            dropDownUser.LeftImage = WDImages.PersonFill;
            dropDownUser.Btns = new string[] { "????????????", "????????????", "???????????????" };
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
        /// ModuleInfo????????????????????????
        /// ????????????ModuleType=0,IsMenu=0
        //  ????????????ModuleType=1,IsMenu=1,RefModeulCode=???????????????ModuleCode
        //  ??????????????????ModuleParentCode=???????????????ModuleCode
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
            //????????????
            var menus = PublicRes.lstUserModule;//??????????????????
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
                if (item.ModuleType == 2 || subMenus.Count() > 0)//???????????????????????????????????????????????????
                    this.mainMenuBar.MenuItems.Add(cur);
                if (item.ModuleType == 2)//???????????????????????????????????????
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
        /// ????????????
        /// </summary>
        private void OpenHomePage()
        {
            //??????>?????????>??????             
            var moduleCode = PublicRes.GetConfig(FormHelper.UserHomePageKey, "");
            //if (string.IsNullOrWhiteSpace(moduleCode))
            //{
            //    moduleCode = PublicRes.GetConfig(FormHelper.ClientHomePageKey, "");
            //}
            //if (string.IsNullOrWhiteSpace(moduleCode))
            //{
            //    if (PublicRes.CurUser.Title_ID.HasValue)
            //    {
            //        var dic = PublicRes.GetSystemDic("??????").FirstOrDefault(d => d.SysVal == PublicRes.CurUser.Title_ID.ToString());
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
            //????????????????????????
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
            //????????????
            if (backgoundLabel != null)
                backgoundLabel.Visible = false;
            if (!this.panelMain.Visible)
            {
                panelMain.Visible = true;
            }
            var formCaption = menuInfo.Text;
            if (!panelTabs.Controls.Cast<WDTabItem>().Select(i => i.ItemText).Contains(formCaption.Trim()))
            {
                //???????????????
                if (panelTabs.Controls.Count > 30)
                {
                    FrmShadowDialog.ShowErrDialog(this, "?????????????????????????????????????????????", blnShowCancel: false);
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
        /// ??????????????????????????????????????????
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
                Assembly asm = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + assemblyName);////??????????????????dll????????????
                                                                                                                                     //??????dll???,????????????dll?????????.
                Type t = asm.GetType(formName);//????????????????????? ????????????+??????  
                                               //???????????????
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
                MessageBox.Show("?????????????????????" + ex.Message);
            }
            return null;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_MINIMIZEBOX = 0x00020000;  // ???Winuser.h?????????
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | WS_MINIMIZEBOX;   // ???????????????????????????
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






        #region ??????????????????

        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;
        const int WM_NCHITTEST = 0x0084;//Windows????????????????????????????????????????????????
        const int HTCAPTION = 2;   //?????????
        const int HTCLIENT = 1;   //?????????
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
                case 0x0201://??????????????????????????? ??????????????????????????????
                    m.Msg = 0x00A1;//???????????????????????????????????????
                    m.LParam = IntPtr.Zero;//?????????
                    m.WParam = new IntPtr(2);//????????????????????????
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
