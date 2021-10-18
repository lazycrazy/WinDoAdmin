using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Forms;
using WinDo.UI.Utilities;
using System.IO;
using System.Drawing.Imaging;
using WinDoControls.Controls;
using WinDo.UI.Main;
using WinDo.UI.Utilities.DialogForm;
using System.Threading.Tasks;
using WinDo.Utilities.PublicResource;
using System.Runtime.InteropServices;
using WinDo.Utilities;
using System.Threading;
using WinDoControls;
using Newtonsoft.Json;
using WinDo.Utilities.Mock;

namespace WinDo.UI.Main
{
    public partial class frmLogin : FrmBase
    {

        public static frmLogin LoginForm = null;

        public static frmLogin GetInstance()
        {
            if (LoginForm == null)
                LoginForm = new frmLogin();
            return LoginForm;
        }

        public frmLogin()
        {
            InitializeComponent();
            InitFormMove(this);
            ControlHelper.SetControlsDouble(this);
            WindowState = FormWindowState.Normal;
            StartPosition = FormStartPosition.CenterScreen;
            this.Paint += FrmLogin_Paint;
            //TopMost = true;
            Load += new EventHandler(frmLogin_Load);
            panelClose.Click += new EventHandler(panelClose_Click);
            btnLogin.MouseClick += (s, e) => btnLogin_BtnClick(s, e);
            txtPassword.PasswordChar = '●';
            txtUserName.MaxLength = 20;
            txtPassword.MaxLength = 20;
            txtUserName.TabStop = true;
            txtPassword.TabStop = true;
            btnLogin.TabStop = true;
            ClearErrorInfo();
            //txtUserName.KeyUp += new KeyEventHandler(txtUserName_KeyUp);
            txtPassword.KeyDown += new KeyEventHandler(txtPassword_KeyUp);
            txtUserName.TextChanged += new EventHandler(txtUserName_TextChanged);
            txtPassword.TextChanged += new EventHandler(txtPassword_TextChanged);
            btnLogin.KeyDown += new KeyEventHandler(btnLogin_KeyDown);
            FormHelper.SwitchToLanguageMode("en-US");
            txtUserName.ImeMode = System.Windows.Forms.ImeMode.Disable;
            txtPassword.ImeMode = System.Windows.Forms.ImeMode.Disable;

            this.FormClosing += new FormClosingEventHandler(frmLogin_FormClosing);
            //登录后记载数据，显示等待窗体
            Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }
        void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (e.Reason == Microsoft.Win32.SessionSwitchReason.SessionLock)
            {
                // 屏幕锁定  
            }
            else if (e.Reason == Microsoft.Win32.SessionSwitchReason.SessionUnlock)
            {
                LogHelper.WriteHourLog("屏幕解锁", "登录窗口前置");
                if (this.Owner != null)
                    WinForm.Main.Program.BringProcessToFront(this.Owner.Handle);
                else
                    WinForm.Main.Program.BringProcessToFront(this.Handle);
            }
        }

        private void FrmLogin_Paint(object sender, PaintEventArgs e)
        {
            if (Err1)
            {
                var err1 = new Rectangle(46, 72, 162, 35);
                ControlHelper.DrawRoundRectangle(e.Graphics, Pens.Red, err1, 4);
            }
            if (Err2)
            {
                var err2 = new Rectangle(46, 125, 162, 35);
                ControlHelper.DrawRoundRectangle(e.Graphics, Pens.Red, err2, 4);
            }
        }

        void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                ClearResource();
                return;
            }
            //if (e.CloseReason == CloseReason.ApplicationExitCall) return;
            if (DialogResult.Cancel == FrmShadowDialog.ShowAskDialog(this, "是否确定关闭？", "退出", true, false, true, true))
            {
                e.Cancel = true;
                return;
            }
            ClearResource();
        }

        private void ClearResource()
        {
            Microsoft.Win32.SystemEvents.SessionSwitch -= new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            if (this.Owner != null && !this.Owner.IsDisposed)
            {
                var owner = this.Owner;
                this.Owner = null;
                owner.Close();
            }
        }


        bool Err1 = false;
        bool Err2 = false;
        void txtUserName_TextChanged(object sender, EventArgs e)
        {
            Err1 = string.IsNullOrWhiteSpace(txtUserName.Text);
            this.Invalidate();
        }
        void txtPassword_TextChanged(object sender, EventArgs e)
        {
            Err2 = string.IsNullOrWhiteSpace(txtPassword.Text);
            this.Invalidate();
        }

        public void SetUserName(string name)
        {
            txtUserName.Text = name;
        }

        void btnLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None
                && (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter))
            {
                btnLogin_BtnClick(btnLogin, EventArgs.Empty);
            }
            base.OnKeyDown(e);
        }

        void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Tab)
            //    btnLogin.Focus();

            //if (e.KeyCode != Keys.Enter) return;
            //btnLogin_BtnClick(btnLogin, EventArgs.Empty);

        }

        bool CheckLogin()
        {
            if (!verification.Verification())
                return false;
            try
            {
                var rs = MockApi.Login(txtUserName.Text.Trim(), txtPassword.Text);
                if (rs.HasError)
                {
                    FrmShadowDialog.ShowErrDialog(this, rs.Msg, "登录失败", false, false, false, true, new Size(-111, -3), IsLogin: true);
                    txtUserName.Focus();
                    return false;
                }
                //设置全局当前登录用户信息 
                ClientCache.Instance.SetLoginUserInfo(rs.Entity);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("SQL Server"))
                    FrmShadowDialog.ShowErrDialog(this, "连接数据库错误，请检查网络配置", "登录失败", false, false, false, true, new Size(-111, -3), IsLogin: true);
                else
                    FrmShadowDialog.ShowErrDialog(this, "发生异常，请联系管理员", "登录失败", false, false, false, true, new Size(-111, -3), IsLogin: true);
                LogHelper.WriteException(ex);
                return false;
            }
        }


        void btnLogin_BtnClick(object sender, EventArgs e)
        {
            ClearErrorInfo();
            txtUserName.Text = txtUserName.Text.Trim();
            if (!CheckLogin()) return;
            InitData();
        }

        void InitData()
        {
            frmWaiting frm = new frmWaiting(() =>
            {
                //加载数据
                ClientCache.Instance.InitResource();
                Thread.Sleep(300);//loading 效果
            });
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(this.Location.X + (this.Width - 40) / 2, this.Location.Y + (this.Height - 40) / 2);
            frm.ShowDialog(this);
            LoginScreen = Screen.FromControl(this);
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        public static Screen LoginScreen;

        void ClearErrorInfo()
        {
            Err1 = false;
            Err2 = false;
        }
        VerificationComponent verification = new VerificationComponent();

        void frmLogin_Load(object sender, EventArgs e)
        {
            string version = "Version 1.00";

            FormHelper.CurrentVersion = version;
            //设置版本号
            SetShadow();
            SetVerificationControl();
            this.Activate();
            //自动登录
            if (System.Diagnostics.Debugger.IsAttached)
            {
                txtUserName.Text = "do";
                txtPassword.Text = "123";
                btnLogin_BtnClick(btnLogin, EventArgs.Empty);
            }
            LogHelper.WriteHourLog("frmLogin_Load", "Login Form Load:" + DateTime.Now);
        }

        private void SetVerificationControl()
        {
            //ToFormat32bppArgb(File.ReadAllBytes(@"C:\Users\DELL\Desktop\1111.bmp"), @"C:\Users\DELL\Desktop\1111bak.bmp");
            verification.Verificationed += new VerificationComponent.VerificationedHandle(verification_Verificationed);
            //verification.SetVerificationCustomRegex(this.txtUserName, @"^\d+$");//自定义正则表达式校验
            //verification.SetVerificationErrorMsg(this.txtUserName, "格式不正确");//校验的错误提示信息
            verification.SetVerificationModel(this.txtUserName, WinDoControls.Controls.VerificationModel.AnyChar);
            verification.SetVerificationRequired(this.txtUserName, true, "请输入用户名");
            verification.SetVerificationModel(this.txtPassword, WinDoControls.Controls.VerificationModel.Custom);
            verification.SetVerificationCustomRegex(this.txtPassword, @"^.*$");
            verification.SetVerificationErrorMsg(this.txtPassword, "请输入字母数字或特殊字符");
            verification.SetVerificationRequired(this.txtPassword, true, "请输入密码");
        }


        public static bool IsWin10
        {
            get
            {
                try
                {
                    var reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                    string productName = (string)reg.GetValue("ProductName");
                    return productName.StartsWith("Windows 10");
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        private void SetShadow()
        {
            if (!IsWin10)
            {
                var shadow = new Dropshadow(this)
                {
                    ShadowH = 2,
                    ShadowV = 12,
                    ShadowBlur = 35,
                    ShadowSpread = -18,
                    ShadowColor = ColorTranslator.FromHtml("#6F9F9F"),
                    //ShadowBitmap = new Bitmap(@"C:\Users\DELL\Desktop\1111bak.bmp")

                };
                shadow.RefreshShadow();
                return;
            }
            var owner = this;
            var c = ColorTranslator.FromHtml("#6F9F9F");
            owner.BackColor = c;
            owner.TransparencyKey = c;
            owner.Opacity = 1;
            owner.BackgroundImage = null;
            //this.Size = Properties.Resources.login_shadow.Size;
            var frm = new frmLoginBackground();
            var p = owner.Location;
            //p.Offset(-40, -30);
            owner.Size = frm.Size;
            frm.Location = p;
            //frm.Size = this.Size;
            owner.LocationChanged += (s, e) =>
            {
                if (frm == null || frm.IsDisposed) return;
                var p1 = owner.Location;
                //p1.Offset(-40, -30);
                frm.Location = p1;
            };
            owner.VisibleChanged += (s, e) =>
            {
                if (frm == null || frm.IsDisposed) return;
                frm.Visible = owner.Visible;
            };
            owner.Activated += (s, e) =>
            {
                owner.BringToFront();
                owner.Invalidate();
            };
            frm.Activated += (s, e) =>
            {
                owner.BringToFront();
                owner.Invalidate();
            };
            frm.Load += (s, e) =>
            {
                owner.BringToFront();
                owner.Invalidate();
            };
            owner.Owner = frm;
            WinForm.Main.Program.BringProcessToFront(frm.Handle);
            frm.Activate();
            owner.BringToFront();
            owner.Focus();
            this.txtUserName.Focus();
        }


        void verification_Verificationed(VerificationEventArgs e)
        {
            if (e.VerificationControl == txtUserName)
            {
                Err1 = !e.IsVerifySuccess;
            }
            else if (e.VerificationControl == txtPassword)
            {
                Err2 = !e.IsVerifySuccess;
            }
        }


        void panelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void DoEnter()
        {
            btnLogin_BtnClick(btnLogin, EventArgs.Empty);
        }



        //void ToFormat32bppArgb(byte[] bs, string path)
        //{
        //    MemoryStream ms = new MemoryStream(bs);
        //    Image imgr = Image.FromStream(ms);
        //    Bitmap bmp = new Bitmap(imgr.Width, imgr.Height, PixelFormat.Format32bppArgb);
        //    Graphics g = Graphics.FromImage(bmp);
        //    g.DrawImage(imgr, new Point(0, 0));
        //    g.Dispose();
        //    bmp.Save(path);
        //}

    }



}
