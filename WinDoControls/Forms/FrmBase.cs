














using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinDoControls.Controls;

namespace WinDoControls.Forms
{





    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class FrmBase : Form
    {




        [Description("定义的热键列表"), Category("自定义")]
        public Dictionary<int, string> HotKeys { get; set; }





        public delegate bool HotKeyEventHandler(string strHotKey);



        [Description("热键事件"), Category("自定义")]
        public event HotKeyEventHandler HotKeyDown;
        #region 字段属性






        private bool _redraw = false;



        private bool _isShowRegion = false;



        private int _regionRadius = 10;



        private Color _borderStyleColor;



        private int _borderStyleSize;



        private ButtonBorderStyle _borderStyleType;



        private bool _isShowMaskDialog = false;




        [Description("是否显示蒙版窗体")]
        public bool IsShowMaskDialog
        {
            get
            {
                return this._isShowMaskDialog;
            }
            set
            {
                this._isShowMaskDialog = value;
            }
        }




        [Description("边框宽度")]
        public int BorderStyleSize
        {
            get
            {
                return this._borderStyleSize;
            }
            set
            {
                this._borderStyleSize = value;
            }
        }




        [Description("边框颜色")]
        public Color BorderStyleColor
        {
            get
            {
                return this._borderStyleColor;
            }
            set
            {
                this._borderStyleColor = value;
            }
        }




        [Description("边框样式")]
        public ButtonBorderStyle BorderStyleType
        {
            get
            {
                return this._borderStyleType;
            }
            set
            {
                this._borderStyleType = value;
            }
        }




        [Description("边框圆角")]
        public int RegionRadius
        {
            get
            {
                return this._regionRadius;
            }
            set
            {
                this._regionRadius = Math.Max(value, 1);
            }
        }




        [Description("是否显示自定义绘制内容")]
        public bool IsShowRegion
        {
            get
            {
                return this._isShowRegion;
            }
            set
            {
                this._isShowRegion = value;
            }
        }




        [Description("是否显示重绘边框")]
        public bool Redraw
        {
            get
            {
                return this._redraw;
            }
            set
            {
                this._redraw = value;
            }
        }




        private bool _isFullSize = false;




        [Description("是否全屏")]
        public bool IsFullSize
        {
            get { return _isFullSize; }
            set { _isFullSize = value; }
        }




        #endregion





        private bool IsDesingMode
        {
            get
            {
                bool ReturnFlag = false;
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    ReturnFlag = true;
                else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                    ReturnFlag = true;
                return ReturnFlag;
            }
        }

        #region 初始化



        public FrmBase()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();


            this.KeyDown += FrmBase_KeyDown;
        }











        private bool isShowShadowForm = false;
        public bool IsShowShadowForm
        {
            get { return isShowShadowForm; }
            set { isShowShadowForm = value; }
        }
        protected Dropshadow shadow;
        private void FrmBase_Load(object sender, EventArgs e)
        {
            if (!IsDesingMode)
            {
                if (_isFullSize)
                    SetFullSize();
                if (isShowShadowForm)
                {
                    if (!IsShowMaskDialog)
                    {
                        shadow = new Dropshadow(this)
                        {
                            ShadowV = 3,
                            ShadowBlur = 15,
                            ShadowSpread = -7,
                            ShadowColor = Color.DimGray

                        };
                        shadow.RefreshShadow();
                    }
                }
            }
        }

        #endregion

        #region 方法区



        public void SetFullSize()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }



        protected virtual void DoEsc()
        {
            base.Close();
        }




        protected virtual void DoEnter()
        {
        }




        public void SetWindowRegion()
        {
            return;
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(-1, -1, base.Width + 1, base.Height + 1);
            path = this.GetRoundedRectPath(rect, this._regionRadius);
            base.Region = new Region(path);
        }






        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            Rectangle rect2 = new Rectangle(rect.Location, new Size(radius, radius));
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect2, 180f, 90f);
            rect2.X = rect.Right - radius;
            graphicsPath.AddArc(rect2, 270f, 90f);
            rect2.Y = rect.Bottom - radius;
            rect2.Width += 1;
            rect2.Height += 1;
            graphicsPath.AddArc(rect2, 360f, 90f);
            rect2.X = rect.Left;
            graphicsPath.AddArc(rect2, 90f, 90f);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }







        public Task<DialogResult> ShowDialogAsync(IWin32Window owner)
        {
            try
            {
                var ownerControl = owner as Control;
                var ownerForm = ControlHelper.GetThisTabForm(ownerControl);
                if (ownerForm != null)
                    ownerControl = ownerForm;

                var mask = new TransparentPanel();
                mask.Size = ownerControl.Size;
                mask.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                ownerControl.Controls.Add(mask);
                mask.BringToFront();

                //ownerControl.Activated += (ss, ee) => { base.BringToFront(); };

                var tcs = new TaskCompletionSource<DialogResult>();
                ownerControl.Disposed += (ss, ee) => { this.Close(); };
                ownerControl.VisibleChanged += (s, e) =>
                {
                    if (this.IsDisposed || !this.IsHandleCreated) return;
                    this.Visible = ownerControl.Visible;
                    this.BringToFront();
                };
                base.FormClosed += (ss, ee) =>
                {
                    mask.Dispose();
                    tcs.SetResult(this.DialogResult);
                };
                base.Show(ownerControl);
                return tcs.Task;//Result 阻塞了主线程，导致卡死了，只能用异步
            }
            catch (NullReferenceException)
            {
                var tcs = new TaskCompletionSource<DialogResult>();
                tcs.SetResult(System.Windows.Forms.DialogResult.None);
                return tcs.Task;
            }
        }







        public new DialogResult ShowDialog(IWin32Window owner)
        {
            try
            {
                if (this._isShowMaskDialog && owner != null)
                {
                    var frmOwner = (Control)owner;
                    FrmTransparent _frmTransparent = new FrmTransparent();
                    _frmTransparent.Width = frmOwner.Width;
                    _frmTransparent.Height = frmOwner.Height;
                    Point location = frmOwner.PointToScreen(new Point(0, 0));
                    _frmTransparent.Location = location;
                    _frmTransparent.frmchild = this;
                    _frmTransparent.IsShowMaskDialog = false;
                    return _frmTransparent.ShowDialog(owner);
                }
                else
                {
                    return base.ShowDialog(owner);
                }
            }
            catch (NullReferenceException)
            {
                return System.Windows.Forms.DialogResult.None;
            }
        }












        public new DialogResult ShowDialog()
        {
            return base.ShowDialog();
        }
        #endregion

        #region 事件区






        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (base.Owner != null && base.Owner is FrmTransparent)
            {
                (base.Owner as FrmTransparent).Close();
            }
        }




        protected bool ProcessDoEnter = true;


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int num = 256;
            int num2 = 260;
            bool result;
            if (msg.Msg == num | msg.Msg == num2)
            {
                if (keyData == (Keys)262259)
                {
                    result = true;
                    return result;
                }
                if (keyData != Keys.Enter)
                {
                    if (keyData == Keys.Escape)
                    {
                        this.DoEsc();
                    }
                }
                else if (ProcessDoEnter)
                {
                    this.DoEnter();
                    result = true;
                    return result;
                }
            }
            result = false;
            if (result)
                return result;
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }






        protected void FrmBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (HotKeyDown != null && HotKeys != null)
            {
                bool blnCtrl = false;
                bool blnAlt = false;
                bool blnShift = false;
                if (e.Control)
                    blnCtrl = true;
                if (e.Alt)
                    blnAlt = true;
                if (e.Shift)
                    blnShift = true;
                if (HotKeys.ContainsKey(e.KeyValue))
                {
                    string strKey = string.Empty;
                    if (blnCtrl)
                    {
                        strKey += "Ctrl+";
                    }
                    if (blnAlt)
                    {
                        strKey += "Alt+";
                    }
                    if (blnShift)
                    {
                        strKey += "Shift+";
                    }
                    strKey += HotKeys[e.KeyValue];

                    if (HotKeyDown(strKey))
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
            }
        }





        protected override void OnPaint(PaintEventArgs e)
        {
            if (this._isShowRegion)
            {
                this.SetWindowRegion();
            }
            base.OnPaint(e);
            if (this._redraw)
            {
                ControlPaint.DrawBorder(e.Graphics, base.ClientRectangle, this._borderStyleColor, this._borderStyleSize, this._borderStyleType, this._borderStyleColor, this._borderStyleSize, this._borderStyleType, this._borderStyleColor, this._borderStyleSize, this._borderStyleType, this._borderStyleColor, this._borderStyleSize, this._borderStyleType);
            }
        }
        #endregion


        #region 窗体拖动    English:Form drag




        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();








        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);




        public const int WM_SYSCOMMAND = 0x0112;



        public const int SC_MOVE = 0xF010;



        public const int HTCAPTION = 0x0002;





        public static void MouseDown(IntPtr hwnd)
        {
            ReleaseCapture();
            SendMessage(hwnd, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion





        protected void InitFormMove(params Control[] cs)
        {
            foreach (Control c in cs)
            {
                if (c != null && !c.IsDisposed)
                    c.MouseDown += c_MouseDown;
            }
        }






        void c_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.TopLevel)
                return;
            MouseDown(this.Handle);
        }
    }
}
