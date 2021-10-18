using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{





    [DefaultEvent("TextChanged")]
    public partial class WDTextBoxEx : WDCtrlBase
    {



        private bool m_isShowClearBtn = false;



        int m_intSelectionStart = 0;



        int m_intSelectionLength = 0;






        [Description("是否显示清理按钮"), Category("自定义")]
        public bool IsShowClearBtn
        {
            get { return m_isShowClearBtn; }
            set
            {
                m_isShowClearBtn = value;
                //if (value)
                //{
                //    btnClear.Visible = !(txtInput.Text == "\r\n") && !string.IsNullOrEmpty(txtInput.Text);
                //}
                //else
                //{
                //    btnClear.Visible = false;
                //}
                //btnClear.BringToFront();
                //txtInput.BringToFront();
            }
        }




        private bool m_isShowSearchBtn = false;





        [Description("是否显示查询按钮"), Category("自定义")]
        public bool IsShowSearchBtn
        {
            get { return m_isShowSearchBtn; }
            set
            {
                m_isShowSearchBtn = value;
                btnSearch.Visible = value;
            }
        }


        private bool btnKeybord = false;


        [Description("是否显示键盘"), Category("自定义")]
        public bool IsShowKeyboard
        {
            get
            {
                //return  btnKeybord.Visible;
                return btnKeybord;
            }
            set
            {
                btnKeybord = value;
                //btnKeybord.Visible = value;
            }
        }










        [Description("字体"), Category("自定义")]
        public new Font Font
        {
            get
            {
                return this.txtInput.Font;
            }
            set
            {
                this.txtInput.Font = value;
            }
        }





        [Description("输入类型"), Category("自定义")]
        public TextInputType InputType
        {
            get { return txtInput.InputType; }
            set { txtInput.InputType = value; }
        }





        [Description("水印文字"), Category("自定义")]
        public string PromptText
        {
            get
            {
                return this.txtInput.PromptText;
            }
            set
            {
                this.txtInput.PromptText = value;
            }
        }





        [Description("水印字体"), Category("自定义")]
        public Font PromptFont
        {
            get
            {
                return this.txtInput.PromptFont;
            }
            set
            {
                this.txtInput.PromptFont = value;
            }
        }





        [Description("水印颜色"), Category("自定义")]
        public Color PromptColor
        {
            get
            {
                return this.txtInput.PromptColor;
            }
            set
            {
                this.txtInput.PromptColor = value;
            }
        }





        [Description("获取或设置一个值，该值指示当输入类型InputType=Regex时，使用的正则表达式。")]
        public string RegexPattern
        {
            get
            {
                return this.txtInput.RegexPattern;
            }
            set
            {
                this.txtInput.RegexPattern = value;
            }
        }




        [Description("当InputType为数字类型时，能输入的最大值。")]
        public decimal MaxValue
        {
            get
            {
                return this.txtInput.MaxValue;
            }
            set
            {
                this.txtInput.MaxValue = value;
            }
        }




        [Description("当InputType为数字类型时，能输入的最小值。")]
        public decimal MinValue
        {
            get
            {
                return this.txtInput.MinValue;
            }
            set
            {
                this.txtInput.MinValue = value;
            }
        }




        [Description("当InputType为数字类型时，小数位数。")]
        public int DecLength
        {
            get
            {
                return this.txtInput.DecLength;
            }
            set
            {
                this.txtInput.DecLength = value;
            }
        }











        [Description("查询按钮点击事件"), Category("自定义")]
        public event EventHandler SearchClick;




        [Description("文本改变事件"), Category("自定义")]
        public new event EventHandler TextChanged;



        [Description("键盘按钮点击事件"), Category("自定义")]
        public event EventHandler KeyboardClick;





        [Description("文本"), Category("自定义")]
        public string InputText
        {
            get
            {
                return txtInput.Text;
            }
            set
            {
                txtInput.Text = value;
            }
        }




        private Color focusBorderColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;





        [Description("获取焦点时边框颜色，当IsFocusColor=true有效"), Category("自定义")]
        public Color FocusBorderColor
        {
            get { return focusBorderColor; }
            set { focusBorderColor = value; }
        }




        private bool isFocusColor = false;




        [Description("获取焦点是否变色"), Category("自定义")]
        public bool IsFocusColor
        {
            get { return isFocusColor; }
            set { isFocusColor = value; }
        }

        private bool isErrorColor = false;

        [Description("是否为错误状态"), Category("自定义")]
        public bool IsErrorColor
        {
            get { return isErrorColor; }
            set
            {
                isErrorColor = value;
                this.RectColor = value ? WDColors.ErrorRedColor : this.Focused ? focusBorderColor : defaultRectColor;
            }
        }

        Color defaultRectColor = WDColors.GrayRectColor;

        private Color _FillColor;




        public new Color FillColor
        {
            get
            {
                return _FillColor;
            }
            set
            {
                _FillColor = value;
                base.FillColor = value;
                this.txtInput.BackColor = value;
            }
        }



        public WDTextBoxEx()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            txtInput.SizeChanged += UCTextBoxEx_SizeChanged;
            this.SizeChanged += UCTextBoxEx_SizeChanged;
            txtInput.GotFocus += (a, b) =>
            {
                if (isFocusColor && !isErrorColor)
                    this.RectColor = focusBorderColor;
            };
            txtInput.LostFocus += (a, b) =>
            {
                if (isFocusColor && !isErrorColor)
                    this.RectColor = defaultRectColor;
            };
            btnSearch.BackgroundImage = ImageSearch;
            //btnClear.BackgroundImage = FontImages.GetImage((WD_Controls.FontIcons)Enum.Parse(typeof(WD_Controls.FontIcons), "I_close"), 20, BasisColors.MainColor);
            GotFocus += new EventHandler(UCTextBoxEx_GotFocus);
        }

        internal static Image ImageSearch = FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "I_search"), 14, WDColors.geekblue6);

        void UCTextBoxEx_GotFocus(object sender, EventArgs e)
        {
            txtInput.Focus();
        }






        void UCTextBoxEx_SizeChanged(object sender, EventArgs e)
        {
            if (txtInput.Multiline)
                txtInput.Height = this.Height - 2;
            this.txtInput.Location = new Point(this.txtInput.Location.X, (this.Height - txtInput.Height) / 2);
        }







        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            if (m_isShowClearBtn)
            {
                //btnClear.Visible = !(txtInput.Text == "\r\n") && !string.IsNullOrEmpty(txtInput.Text);
                //btnClear.BringToFront();
                //txtInput.BringToFront();
            }
            if (TextChanged != null)
            {
                //TextChanged(sender, e);
                TextChanged(this, e);
            }
            //var foreColor = txtInput.Enabled ? Color.Black : (txtInput.Text.Contains("请选择") ? YkdBasisColors.GrayTextColor : Color.Black);
            //txtInput.ForeColor = foreColor;
        }






        private void btnClear_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
            txtInput.Clear();
            txtInput.Focus();
        }






        private void btnSearch_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
            if (SearchClick != null)
            {
                SearchClick(sender, e);
            }
        }



        Forms.FrmAnchor m_frmAnchor;





        void m_frmAnchor_Disposed(object sender, EventArgs e)
        {
            if (m_HandAppWin != IntPtr.Zero)
            {
                if (m_HandPWin != null && !m_HandPWin.HasExited)
                    m_HandPWin.Kill();
                m_HandPWin = null;
                m_HandAppWin = IntPtr.Zero;
            }
        }





        IntPtr m_HandAppWin;



        Process m_HandPWin = null;



        string m_HandExeName = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "HandInput\\handinput.exe");






        void m_frmAnchor_VisibleChanged(object sender, EventArgs e)
        {
            if (m_frmAnchor.Visible)
            {
                var lstP = Process.GetProcessesByName("handinput");
                if (lstP.Length > 0)
                {
                    foreach (var item in lstP)
                    {
                        item.Kill();
                    }
                }
                m_HandAppWin = IntPtr.Zero;

                if (m_HandPWin == null)
                {
                    m_HandPWin = null;

                    m_HandPWin = System.Diagnostics.Process.Start(this.m_HandExeName);
                    m_HandPWin.WaitForInputIdle();
                }
                while (m_HandPWin.MainWindowHandle == IntPtr.Zero)
                {
                    Thread.Sleep(10);
                }
                m_HandAppWin = m_HandPWin.MainWindowHandle;
                Control p = m_frmAnchor.Controls.Find("keyborder", false)[0];
                SetParent(m_HandAppWin, p.Handle);
                ControlHelper.SetForegroundWindow(this.FindForm().Handle);
                MoveWindow(m_HandAppWin, -111, -41, 626, 412, true);
            }
            else
            {
                if (m_HandAppWin != IntPtr.Zero)
                {
                    if (m_HandPWin != null && !m_HandPWin.HasExited)
                        m_HandPWin.Kill();
                    m_HandPWin = null;
                    m_HandAppWin = IntPtr.Zero;
                }
            }
        }






        private void UCTextBoxEx_MouseDown(object sender, MouseEventArgs e)
        {
            this.ActiveControl = txtInput;
        }






        private void UCTextBoxEx_Load(object sender, EventArgs e)
        {
            if (!Enabled)
            {
                base.FillColor = Color.FromArgb(240, 240, 240);
                txtInput.BackColor = Color.FromArgb(240, 240, 240);
            }
            else
            {
                FillColor = _FillColor;
                txtInput.BackColor = _FillColor;
            }

            //this.BackColor = txtInput.Enabled ? Color.White : YkdBasisColors.GrayTextBackColor;
            //this.ForeColor = txtInput.Enabled ? Color.Black : YkdBasisColors.GrayTextColor;
            //this.FillColor = txtInput.BackColor;

        }






        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);











        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);






        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);











        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);



        private const int GWL_STYLE = -16;



        private const int WS_CHILD = 0x40000000;







        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hwnd, int nIndex);








        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);






        [DllImport("user32.dll")]
        private extern static IntPtr SetActiveWindow(IntPtr handle);
    }
}
