using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WinDoControls.Forms
{

    public partial class FrmAnchorTips : Form, IMessageFilter
    {
        /// <summary>
        /// The m string MSG
        /// </summary>
        private string m_strMsg = string.Empty;

        /// <summary>
        /// Gets or sets the string MSG.
        /// </summary>
        /// <value>The string MSG.</value>
        public string StrMsg
        {
            get { return m_strMsg; }
            set
            {
                m_strMsg = value;
                if (string.IsNullOrEmpty(value))
                    return;
                ResetForm(value);
            }
        }
        /// <summary>
        /// The have handle
        /// </summary>
        protected bool haveHandle = false;
        /// <summary>
        /// The m rect control
        /// </summary>
        protected Rectangle m_rectControl;
        /// <summary>
        /// Gets or sets the rect control.
        /// </summary>
        /// <value>The rect control.</value>
        public Rectangle RectControl
        {
            get { return m_rectControl; }
            set
            {
                m_rectControl = value;
            }
        }
        /// <summary>
        /// The m location
        /// </summary>
        protected AnchorTipsLocation m_location;
        /// <summary>
        /// The m background
        /// </summary>
        protected Color? m_background = null;
        /// <summary>
        /// The m fore color
        /// </summary>
        protected Color? m_foreColor = null;
        /// <summary>
        /// The m font size
        /// </summary>
        protected int m_fontSize = 14;
        #region 构造函数    English:Constructor
        protected FrmAnchorTips()
        {
            InitializeComponent();

            this.HandleCreated += FrmAnchorTips_HandleCreated;
            this.HandleDestroyed += FrmAnchorTips_HandleDestroyed;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmAnchorTips"/> class.
        /// </summary>
        /// <param name="rectControl">The rect control.</param>
        /// <param name="strMsg">The string MSG.</param>
        /// <param name="location">The location.</param>
        /// <param name="background">The background.</param>
        /// <param name="foreColor">Color of the fore.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <param name="autoCloseTime">The automatic close time.</param>
        private FrmAnchorTips(
            Rectangle rectControl,
            string strMsg,
            AnchorTipsLocation location = AnchorTipsLocation.RIGHT,
            Color? background = null,
            Color? foreColor = null,
            int fontSize = 14,
            int autoCloseTime = 5000,
            StringAlignment alignment = System.Drawing.StringAlignment.Near
            ) : this()
        {
            m_rectControl = rectControl;
            m_location = location;
            m_background = background;
            m_foreColor = foreColor;
            m_fontSize = fontSize;
            StringAlignment = alignment;
            StrMsg = strMsg;
            if (autoCloseTime > 0)
            {
                Timer t = new Timer();
                t.Interval = autoCloseTime;
                t.Tick += (a, b) =>
                {
                    if (!this.Disposing)
                        this.Close();
                };
                t.Enabled = true;
            }
        }

        /// <summary>
        /// Resets the form.
        /// </summary>
        /// <param name="strMsg">The string MSG.</param>
        private void ResetForm(string strMsg)
        {
            Graphics g = this.CreateGraphics();
            Font _font = new Font("微软雅黑", m_fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            Color _background = m_background == null ? Color.FromArgb(100, 255, 77, 58) : m_background.Value;
            Color _foreColor = m_foreColor == null ? Color.White : m_foreColor.Value;
            var trimSF = new StringFormat(StringFormat.GenericTypographic) { FormatFlags = StringFormatFlags.MeasureTrailingSpaces };
            System.Drawing.SizeF sizeText = g.MeasureString(strMsg, _font, new Point(), trimSF);
            var height14 = g.MeasureString(" ", _font, new Point(), trimSF).Height;
            g.Dispose();
            var formSize = new Size((int)sizeText.Width + 20, (int)sizeText.Height + 10);
            if (formSize.Width < 20)
                formSize.Width = 20;
            if (formSize.Height < 20)
                formSize.Height = 20;
            if (m_location == AnchorTipsLocation.LEFT || m_location == AnchorTipsLocation.RIGHT)
            {
                formSize.Width += 10;
            }
            else
            {
                formSize.Height += 10;
            }

            #region 获取窗体path    English:Get the form path
            GraphicsPath path = new GraphicsPath();
            Rectangle rect;
            switch (m_location)
            {
                case AnchorTipsLocation.TOP:
                    rect = new Rectangle(1, 1, formSize.Width - 2, formSize.Height - 10 - 1);
                    this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Y - rect.Height - 10);
                    break;
                case AnchorTipsLocation.RIGHT:
                    rect = new Rectangle(10, 1, formSize.Width - 10 - 1, formSize.Height - 2);
                    this.Location = new Point(m_rectControl.Right, m_rectControl.Y + (m_rectControl.Height - rect.Height) / 2);
                    break;
                case AnchorTipsLocation.BOTTOM:
                    rect = new Rectangle(1, 10, formSize.Width - 2, formSize.Height - 10 - 1);
                    this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Bottom);
                    break;
                default:
                    rect = new Rectangle(1, 1, formSize.Width - 10 - 1, formSize.Height - 2);
                    this.Location = new Point(m_rectControl.X - rect.Width - 10, m_rectControl.Y + (m_rectControl.Height - rect.Height) / 2);
                    break;
            }
            int cornerRadius = 2;

            //计算是否超出下边界
            var curScreen = Screen.FromPoint(m_rectControl.Location);
            if (this.Top + formSize.Height > curScreen.WorkingArea.Bottom && m_location == AnchorTipsLocation.BOTTOM)
            {
                m_location = AnchorTipsLocation.TOP;
                rect = new Rectangle(1, 1, formSize.Width - 2, formSize.Height - 10 - 1);
                this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Y - rect.Height - 10);
            }
            if (this.Left + formSize.Width > curScreen.WorkingArea.Right)
            {
                m_location = AnchorTipsLocation.LEFT;
                rect = new Rectangle(1, 1, formSize.Width - 10 - 1, formSize.Height - 2);
                this.Location = new Point(m_rectControl.X - rect.Width - 10, m_rectControl.Y + (m_rectControl.Height - rect.Height) / 2);
            }

            path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);//左上角
            #region 上边
            if (m_location == AnchorTipsLocation.BOTTOM)
            {
                path.AddLine(rect.X + cornerRadius, rect.Y, rect.Left + rect.Width / 2 - 5, rect.Y);//上
                path.AddLine(rect.Left + rect.Width / 2 - 5, rect.Y, rect.Left + rect.Width / 2, rect.Y - 9);//上
                path.AddLine(rect.Left + rect.Width / 2, rect.Y - 9, rect.Left + rect.Width / 2 + 5, rect.Y);//上
                path.AddLine(rect.Left + rect.Width / 2 + 5, rect.Y, rect.Right - cornerRadius * 2, rect.Y);//上
            }
            else
            {
                path.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);//上
            }
            #endregion
            path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);//右上角
            #region 右边
            if (m_location == AnchorTipsLocation.LEFT)
            {
                path.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height / 2 - 5);//右
                path.AddLine(rect.Right, rect.Y + rect.Height / 2 - 5, rect.Right + 9, rect.Y + rect.Height / 2);//右
                path.AddLine(rect.Right + 9, rect.Y + rect.Height / 2, rect.Right, rect.Y + rect.Height / 2 + 5);//右
                path.AddLine(rect.Right, rect.Y + rect.Height / 2 + 5, rect.Right, rect.Y + rect.Height - cornerRadius * 2);//右            
            }
            else
            {
                path.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);//右
            }
            #endregion
            path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);//右下角
            #region 下边
            if (m_location == AnchorTipsLocation.TOP)
            {
                path.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.Left + rect.Width / 2 + 5, rect.Bottom);
                path.AddLine(rect.Left + rect.Width / 2 + 5, rect.Bottom, rect.Left + rect.Width / 2, rect.Bottom + 9);
                path.AddLine(rect.Left + rect.Width / 2, rect.Bottom + 9, rect.Left + rect.Width / 2 - 5, rect.Bottom);
                path.AddLine(rect.Left + rect.Width / 2 - 5, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            }
            else
            {
                path.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            }
            #endregion
            path.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);//左下角
            #region 左边
            if (m_location == AnchorTipsLocation.RIGHT)
            {
                path.AddLine(rect.Left, rect.Y + cornerRadius * 2, rect.Left, rect.Y + rect.Height / 2 - 5);//左
                path.AddLine(rect.Left, rect.Y + rect.Height / 2 - 5, rect.Left - 9, rect.Y + rect.Height / 2);//左
                path.AddLine(rect.Left - 9, rect.Y + rect.Height / 2, rect.Left, rect.Y + rect.Height / 2 + 5);//左
                path.AddLine(rect.Left, rect.Y + rect.Height / 2 + 5, rect.Left, rect.Y + rect.Height - cornerRadius * 2);//左          
            }
            else
            {
                path.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);//左
            }
            #endregion
            path.CloseFigure();
            #endregion

            Bitmap bit = new Bitmap(formSize.Width, formSize.Height);
            this.Size = formSize;

            #region 画图    English:Drawing
            Graphics gBit = Graphics.FromImage(bit);
            gBit.SetGDIHigh();
            gBit.FillPath(new SolidBrush(_background), path);
            //StringFormat sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            trimSF.Alignment = StringAlignment.Near;
            trimSF.LineAlignment = StringAlignment.Center;
            if (height14 == sizeText.Height)
                trimSF.Alignment = StringAlignment.Center;
            else
                rect.Offset((formSize.Width - (int)sizeText.Width) / 2, 0);
            gBit.DrawString(strMsg, _font, new SolidBrush(_foreColor), rect, trimSF);
            gBit.Dispose();
            #endregion

            SetBits(bit);
        }

        public StringAlignment StringAlignment = StringAlignment.Near;
        #endregion

        #region 显示一个提示    English:Show a hint
        /// <summary>
        /// Shows the tips.
        /// </summary>
        /// <param name="anchorControl">The parent control.</param>
        /// <param name="strMsg">The string MSG.</param>
        /// <param name="location">The location.</param>
        /// <param name="background">The background.</param>
        /// <param name="foreColor">Color of the fore.</param>
        /// <param name="deviation">The deviation.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <param name="autoCloseTime">The automatic close time.</param>
        /// <param name="blnTopMost">是否置顶</param>
        /// <returns>FrmAnchorTips.</returns>
        public static FrmAnchorTips ShowTips(
            Control anchorControl,
            string strMsg,
            AnchorTipsLocation location = AnchorTipsLocation.RIGHT,
            Color? background = null,
            Color? foreColor = null,
            Size? deviation = null,
            int fontSize = 14,
            int autoCloseTime = 5000,
            bool blnTopMost = true,
            StringAlignment alignment = StringAlignment.Near
            )
        {
            Point p;
            if (anchorControl is Form)
            {
                p = anchorControl.Location;
            }
            else
            {
                p = anchorControl.Parent.PointToScreen(anchorControl.Location);
            }
            if (deviation != null)
            {
                p = p + deviation.Value;
            }
            return ShowTips(new Rectangle(p, anchorControl.Size), strMsg, location, background, foreColor, fontSize, autoCloseTime, anchorControl.Parent, blnTopMost, alignment: alignment);
        }
        #endregion

        #region 显示一个提示    English:Show a hint
        /// <summary>
        /// Shows the tips.
        /// </summary>
        /// <param name="rectControl">The rect control.</param>
        /// <param name="strMsg">The string MSG.</param>
        /// <param name="location">The location.</param>
        /// <param name="background">The background.</param>
        /// <param name="foreColor">Color of the fore.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <param name="autoCloseTime">The automatic close time.</param>
        /// <param name="parentForm">父窗体</param>
        /// <param name="blnTopMost">是否置顶</param>
        /// <returns>FrmAnchorTips.</returns>
        public static FrmAnchorTips ShowTips(
            Rectangle rectControl,
            string strMsg,
            AnchorTipsLocation location = AnchorTipsLocation.RIGHT,
            Color? background = null,
            Color? foreColor = null,
            int fontSize = 14,
            int autoCloseTime = 5000,
            Control parentControl = null,
            bool blnTopMost = true,
            StringAlignment alignment = StringAlignment.Near
            )
        {
            FrmAnchorTips frm = new FrmAnchorTips(rectControl, strMsg, location, background, foreColor, fontSize, autoCloseTime, alignment);
            frm.TopMost = blnTopMost;
            frm.Show(parentControl);
            if (parentControl != null)
            {
                var dFrm = parentControl.FindForm();
                if (dFrm != null)
                {
                    if (dFrm.TopLevel)
                    {
                        dFrm.LocationChanged += (ss, ee) =>
                       {
                           if (frm != null && !frm.IsDisposed)
                               frm.Close();
                       };
                    }
                    else
                    {
                        var topForm = dFrm.Parent;
                        while (topForm != null && topForm.Parent != null)
                            topForm = topForm.Parent;
                        if (topForm != null)
                        {
                            topForm.LocationChanged += (ss, ee) =>
                            {
                                if (frm != null && !frm.IsDisposed)
                                    frm.Close();
                            };
                        }
                    }
                }
                parentControl.VisibleChanged += (a, b) =>
                {
                    try
                    {
                        Control c = a as Control;
                        if (CheckControlClose(c))
                        {
                            frm.Close();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                };
            }
            return frm;
        }


        private static bool CheckControlClose(Control c)
        {
            if (c.IsDisposed || !c.Visible)
                return true;
            else if (c.Parent != null)
                return CheckControlClose(c.Parent);
            else
            {
                if (c is Form)
                    return false;
                else
                    return true;
            }
        }
        #endregion

        #region Override

        /// <summary>
        /// 引发 <see cref="E:System.Windows.Forms.Form.Closing" /> 事件。
        /// </summary>
        /// <param name="e">一个包含事件数据的 <see cref="T:System.ComponentModel.CancelEventArgs" />。</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            haveHandle = false;
            this.Dispose();
        }

        /// <summary>
        /// Handles the <see cref="E:HandleCreated" /> event.
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.EventArgs" />。</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            InitializeStyles();
            base.OnHandleCreated(e);
            haveHandle = true;
        }

        /// <summary>
        /// Gets the create parameters.
        /// </summary>
        /// <value>The create parameters.</value>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cParms = base.CreateParams;
                cParms.ExStyle |= 0x00080000; // WS_EX_LAYERED
                return cParms;
            }
        }

        #endregion

        /// <summary>
        /// Initializes the styles.
        /// </summary>
        private void InitializeStyles()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            UpdateStyles();
        }



        #region 根据图片显示窗体    English:Display Forms Based on Pictures

        /// </summary>
        /// <param name="bitmap">bitmap</param>
        /// <exception cref="System.ApplicationException">The picture must be 32bit picture with alpha channel.</exception>
        /// <exception cref="ApplicationException">The picture must be 32bit picture with alpha channel.</exception>
        protected void SetBits(Bitmap bitmap)
        {
            if (!haveHandle) return;

            if (!Bitmap.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Bitmap.IsAlphaPixelFormat(bitmap.PixelFormat))
                throw new ApplicationException("The picture must be 32bit picture with alpha channel.");

            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);

            try
            {
                Win32.Point topLoc = new Win32.Point(Left, Top);
                Win32.Size bitMapSize = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
                Win32.Point srcLoc = new Win32.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                Win32.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }
        }
        #endregion

        #region 无焦点窗体处理


        private void FrmAnchorTips_HandleDestroyed(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
        }

        private void FrmAnchorTips_HandleCreated(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);
        }

        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int WM_HSCROLL = 0x0114;
        private const int WM_VSCROLL = 0x0115;
        private const int WM_MOUSEWHEEL = 0x020A;
        //private const int WM_MOVE = 0x0003;
        //private const int WM_LBUTTONDOWN = 0x0201;
        //private const int WM_MOUSEMOVE = 0x0200;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;


        public bool PreFilterMessage(ref Message m)
        {
            if (!this.Visible)
                return false;
            if (m.Msg != WM_NCLBUTTONDOWN && m.Msg != WM_HSCROLL && m.Msg != WM_VSCROLL && m.Msg != WM_MOUSEWHEEL)
                return false;
            this.Close();
            return false;
        }

        /// <summary>
        /// Sets the active window.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns>IntPtr.</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private extern static IntPtr SetActiveWindow(IntPtr handle);
        /// <summary>
        /// The wm activate
        /// </summary>
        private const int WM_ACTIVATE = 0x006;
        /// <summary>
        /// The wm activateapp
        /// </summary>
        private const int WM_ACTIVATEAPP = 0x01C;
        /// <summary>
        /// The wm ncactivate
        /// </summary>
        private const int WM_NCACTIVATE = 0x086;
        /// <summary>
        /// The wa inactive
        /// </summary>
        private const int WA_INACTIVE = 0;
        /// <summary>
        /// The wm mouseactivate
        /// </summary>
        private const int WM_MOUSEACTIVATE = 0x21;
        /// <summary>
        /// The ma noactivate
        /// </summary>
        private const int MA_NOACTIVATE = 3;
        /// <summary>
        /// WNDs the proc.
        /// </summary>
        /// <param name="m">要处理的 Windows <see cref="T:System.Windows.Forms.Message" />。</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = new IntPtr(MA_NOACTIVATE);
                return;
            }
            else if (m.Msg == WM_NCACTIVATE)
            {
                if (((int)m.WParam & 0xFFFF) != WA_INACTIVE)
                {
                    if (m.LParam != IntPtr.Zero)
                    {
                        SetActiveWindow(m.LParam);
                    }
                    else
                    {
                        SetActiveWindow(IntPtr.Zero);
                    }
                }
            }
            base.WndProc(ref m);
        }

        #endregion
    }



    /// <summary>
    /// 进度条Tips
    /// </summary>
    public class FrmAnchorTips_Bar : FrmAnchorTips
    {
        private List<string> strs;

        /// <summary>
        /// Gets or sets the string MSG.
        /// </summary>
        /// <value>The string MSG.</value>
        public List<string> Strs
        {
            get { return strs; }
            set
            {
                strs = value;
                ResetForm();
            }
        }

        public bool HasInfo { get; private set; }

        private FrmAnchorTips_Bar(
            Rectangle rectControl,
            List<string> strs,
            AnchorTipsLocation location = AnchorTipsLocation.RIGHT,
            Color? background = null,
            Color? foreColor = null,
            int fontSize = 10,
            int autoCloseTime = 5000,
            StringAlignment alignment = System.Drawing.StringAlignment.Near,
            Action<Graphics, string, Font, SolidBrush, Rectangle, StringFormat> drawLine = null
            )
        {
            m_rectControl = rectControl;
            m_location = location;
            m_background = background;
            m_foreColor = foreColor;
            m_fontSize = fontSize;
            StringAlignment = alignment;
            DrawLine = drawLine;
            Strs = strs;
            if (autoCloseTime > 0)
            {
                Timer t = new Timer();
                t.Interval = autoCloseTime;
                t.Tick += (a, b) =>
                {
                    this.Close();
                };
                t.Enabled = true;
            }
        }


        public static FrmAnchorTips_Bar ShowBarTips(
            System.Windows.Forms.Control anchorControl,
            List<string> strs,
            AnchorTipsLocation location = AnchorTipsLocation.RIGHT,
            Color? background = null,
            Color? foreColor = null,
            Size? deviation = null,
            int fontSize = 10,
            int autoCloseTime = 5000,
            bool blnTopMost = false,
            StringAlignment alignment = StringAlignment.Near,
            Action<Graphics, string, Font, SolidBrush, Rectangle, StringFormat> drawLine = null
            )
        {
            Point p;
            if (anchorControl is Form)
            {
                p = anchorControl.Location;
            }
            else
            {
                p = anchorControl.Parent.PointToScreen(anchorControl.Location);
            }
            if (deviation != null)
            {
                p = p + deviation.Value;
            }
            return ShowBarTips(new Rectangle(p, anchorControl.Size), strs, location, background, foreColor, fontSize, autoCloseTime, anchorControl.Parent, blnTopMost, alignment: alignment, drawLine: drawLine);
        }

        public static FrmAnchorTips_Bar ShowBarTips(
            Rectangle rectControl,
            List<string> strs,
            AnchorTipsLocation location = AnchorTipsLocation.RIGHT,
            Color? background = null,
            Color? foreColor = null,
            int fontSize = 10,
            int autoCloseTime = 5000,
            System.Windows.Forms.Control parentControl = null,
            bool blnTopMost = false,
            StringAlignment alignment = StringAlignment.Near,
            Action<Graphics, string, Font, SolidBrush, Rectangle, StringFormat> drawLine = null
            )
        {
            FrmAnchorTips_Bar frm = new FrmAnchorTips_Bar(rectControl, strs, location, background, foreColor, fontSize, autoCloseTime, alignment, drawLine);
            if (!frm.HasInfo) return null;
            frm.TopMost = blnTopMost;
            frm.Show(parentControl);
            return frm;
        }

        private void ResetForm()
        {
            HasInfo = true;
            var infos = strs;
            if (infos == null || infos.Count == 0)
            {
                HasInfo = false;
                Load += (s, e) =>
                Close();
                return;
            }
            Color _background = m_background == null ? Color.FromArgb(100, 255, 77, 58) : m_background.Value;

            Font _font = WinDo.Utilities.PublicResource.WDFonts.TextFont;
            var str = string.Join(System.Environment.NewLine, strs);
            var textWidth = TextRenderer.MeasureText(str, _font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Width;
            var textHeight = TextRenderer.MeasureText("A", _font).Height;
            var formSize = new Size(Math.Max(textWidth, 30) + 10, textHeight * strs.Count + 30);
            #region 获取窗体path    English:Get the form path
            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect;
                switch (m_location)
                {
                    case AnchorTipsLocation.TOP:
                        rect = new Rectangle(1, 1, formSize.Width - 2, formSize.Height - 20 - 1);
                        this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Y - rect.Height - 10);
                        break;
                    case AnchorTipsLocation.RIGHT:
                        rect = new Rectangle(1, 1, formSize.Width - 20 - 1, formSize.Height - 2);
                        this.Location = new Point(m_rectControl.Right, m_rectControl.Y + (m_rectControl.Height - rect.Height) / 2);
                        break;
                    case AnchorTipsLocation.BOTTOM:
                        rect = new Rectangle(1, 10, formSize.Width - 2, formSize.Height - 20 - 1);
                        this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Bottom);
                        break;
                    default:
                        rect = new Rectangle(1, 1, formSize.Width - 20 - 1, formSize.Height - 2);
                        this.Location = new Point(m_rectControl.X - rect.Width - 10, m_rectControl.Y + (m_rectControl.Height - rect.Height) / 2);
                        break;
                }
                int cornerRadius = 2;
                var curScreen = Screen.FromPoint(m_rectControl.Location);
                bool overRight = false;
                if (this.Left - curScreen.WorkingArea.Left + formSize.Width > curScreen.WorkingArea.Width)
                {
                    this.Left = curScreen.WorkingArea.Left + (curScreen.WorkingArea.Width - formSize.Width - 10);
                    overRight = true;
                }

                path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);//左上角
                #region 上边
                if (m_location == AnchorTipsLocation.BOTTOM && !overRight)
                {
                    path.AddLine(rect.X + cornerRadius, rect.Y, rect.Left + rect.Width / 2 - 10, rect.Y);//上
                    path.AddLine(rect.Left + rect.Width / 2 - 10, rect.Y, rect.Left + rect.Width / 2, rect.Y - 10);//上
                    path.AddLine(rect.Left + rect.Width / 2, rect.Y - 10, rect.Left + rect.Width / 2 + 10, rect.Y);//上
                    path.AddLine(rect.Left + rect.Width / 2 + 10, rect.Y, rect.Right - cornerRadius * 2, rect.Y);//上
                }
                else
                {
                    path.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);//上
                }
                #endregion
                path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);//右上角
                #region 右边
                if (m_location == AnchorTipsLocation.LEFT)
                {
                    path.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height / 2 - 10);//右
                    path.AddLine(rect.Right, rect.Y + rect.Height / 2 - 10, rect.Right + 19, rect.Y + rect.Height / 2);//右
                    path.AddLine(rect.Right + 10, rect.Y + rect.Height / 2, rect.Right, rect.Y + rect.Height / 2 + 10);//右
                    path.AddLine(rect.Right, rect.Y + rect.Height / 2 + 10, rect.Right, rect.Y + rect.Height - cornerRadius * 2);//右            
                }
                else
                {
                    path.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);//右
                }
                #endregion
                path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);//右下角
                #region 下边
                if (m_location == AnchorTipsLocation.TOP)
                {
                    path.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.Left + rect.Width / 2 + 10, rect.Bottom);
                    path.AddLine(rect.Left + rect.Width / 2 + 10, rect.Bottom, rect.Left + rect.Width / 2, rect.Bottom + 10);
                    path.AddLine(rect.Left + rect.Width / 2, rect.Bottom + 10, rect.Left + rect.Width / 2 - 10, rect.Bottom);
                    path.AddLine(rect.Left + rect.Width / 2 - 10, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
                }
                else
                {
                    path.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
                }
                #endregion
                path.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);//左下角
                #region 左边
                if (m_location == AnchorTipsLocation.RIGHT)
                {
                    path.AddLine(rect.Left, rect.Y + cornerRadius * 2, rect.Left, rect.Y + rect.Height / 2 - 10);//左
                    path.AddLine(rect.Left, rect.Y + rect.Height / 2 - 10, rect.Left - 10, rect.Y + rect.Height / 2);//左
                    path.AddLine(rect.Left - 10, rect.Y + rect.Height / 2, rect.Left, rect.Y + rect.Height / 2 + 10);//左
                    path.AddLine(rect.Left, rect.Y + rect.Height / 2 + 10, rect.Left, rect.Y + rect.Height - cornerRadius * 2);//左          
                }
                else
                {
                    path.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);//左
                }
                #endregion
                path.CloseFigure();
                #endregion

                this.Size = formSize;
                using (Bitmap bit = new Bitmap(formSize.Width, formSize.Height))
                {
                    #region 画图    English:Drawing
                    using (Graphics gBit = Graphics.FromImage(bit))
                    {
                        gBit.SetGDIHigh();
                        gBit.FillPath(new SolidBrush(_background), path);
                        Color _foreColor = m_foreColor == null ? Color.White : m_foreColor.Value;
                        int i = 0;
                        var sf = new StringFormat() { FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces, Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                        using (var brush = new SolidBrush(_foreColor))
                            foreach (var info in infos)
                            {
                                var trect = new Rectangle(10, rect.Y + 5 + (int)(i++ * textHeight), textWidth, textHeight);
                                if (DrawLine == null)
                                    gBit.DrawString(info, _font, brush, trect, sf);
                                else DrawLine(gBit, info, _font, brush, trect, sf);
                            }
                    }
                    #endregion
                    using (var g = this.CreateGraphics())
                        SetBits(bit);
                }
            }
        }
        private Action<Graphics, string, Font, SolidBrush, Rectangle, StringFormat> DrawLine;
    }

    /// <summary>
    /// 自绘tips
    /// </summary>
    public class FrmAnchorTips_CustomDraw : FrmAnchorTips
    {
        private FrmAnchorTips_CustomDraw(
            Rectangle rectControl,
            AnchorTipsLocation location = AnchorTipsLocation.RIGHT,
            Color? background = null,
            Color? foreColor = null,
            int fontSize = 10,
            int autoCloseTime = 5000,
            Func<Size> getSize = null,
            Action<Graphics> draw = null
            )
        {
            m_rectControl = rectControl;
            m_location = location;
            m_background = background;
            m_foreColor = foreColor;
            m_fontSize = fontSize;
            GetFormSize = getSize;
            Draw = draw;
            if (autoCloseTime > 0)
            {
                Timer t = new Timer();
                t.Interval = autoCloseTime;
                t.Tick += (a, b) =>
                {
                    this.Close();
                };
                t.Enabled = true;
            }
        }


        public static FrmAnchorTips_CustomDraw ShowTips(
            System.Windows.Forms.Control anchorControl,
            AnchorTipsLocation location = AnchorTipsLocation.RIGHT,
            Color? background = null,
            Color? foreColor = null,
            Size? deviation = null,
            int fontSize = 10,
            int autoCloseTime = 5000,
            bool blnTopMost = false,
            Func<Size> getSize = null,
            Action<Graphics> draw = null
            )
        {
            Point p;
            if (anchorControl is Form)
            {
                p = anchorControl.Location;
            }
            else
            {
                p = anchorControl.Parent.PointToScreen(anchorControl.Location);
            }
            if (deviation != null)
            {
                p = p + deviation.Value;
            }
            return ShowTips(new Rectangle(p, anchorControl.Size), location, background, foreColor, fontSize, autoCloseTime, anchorControl.Parent, blnTopMost, getSize, draw);
        }

        public static FrmAnchorTips_CustomDraw ShowTips(
            Rectangle rectControl,
            AnchorTipsLocation location = AnchorTipsLocation.RIGHT,
            Color? background = null,
            Color? foreColor = null,
            int fontSize = 10,
            int autoCloseTime = 5000,
            System.Windows.Forms.Control parentControl = null,
            bool blnTopMost = false,
            Func<Size> getSize = null,
            Action<Graphics> draw = null
            )
        {
            FrmAnchorTips_CustomDraw frm = new FrmAnchorTips_CustomDraw(rectControl, location, background, foreColor, fontSize, autoCloseTime, getSize, draw);
            frm.TopMost = blnTopMost;
            frm.Show(parentControl);
            return frm;
        }

        private Action<Graphics> Draw;
        private Func<Size> GetFormSize;


        public void ResetForm()
        {
            Color _background = m_background == null ? Color.FromArgb(100, 255, 77, 58) : m_background.Value;

            Font _font = WinDo.Utilities.PublicResource.WDFonts.TextFont;
            var formSize = new Size(100, 100);
            if (GetFormSize != null)
                formSize = GetFormSize();

            #region 获取窗体path    English:Get the form path
            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect;
                switch (m_location)
                {
                    case AnchorTipsLocation.TOP:
                        rect = new Rectangle(1, 1, formSize.Width - 2, formSize.Height - 20 - 1);
                        this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Y - rect.Height - 10);
                        break;
                    case AnchorTipsLocation.RIGHT:
                        rect = new Rectangle(1, 1, formSize.Width - 20 - 1, formSize.Height - 2);
                        this.Location = new Point(m_rectControl.Right, m_rectControl.Y + (m_rectControl.Height - rect.Height) / 2);
                        break;
                    case AnchorTipsLocation.BOTTOM:
                        rect = new Rectangle(1, 10, formSize.Width - 2, formSize.Height - 20 - 1);
                        this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Bottom);
                        break;
                    default:
                        rect = new Rectangle(1, 1, formSize.Width - 20 - 1, formSize.Height - 2);
                        this.Location = new Point(m_rectControl.X - rect.Width - 10, m_rectControl.Y + (m_rectControl.Height - rect.Height) / 2);
                        break;
                }
                int cornerRadius = 2;

                //计算是否超出下边界
                var curScreen = Screen.FromPoint(m_rectControl.Location);
                if (this.Top + formSize.Height > curScreen.WorkingArea.Bottom && m_location == AnchorTipsLocation.BOTTOM)
                {
                    m_location = AnchorTipsLocation.TOP;
                    rect = new Rectangle(1, 1, formSize.Width - 2, formSize.Height - 10 - 1);
                    this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Y - rect.Height - 10);
                }

                path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);//左上角
                #region 上边
                if (m_location == AnchorTipsLocation.BOTTOM)
                {
                    path.AddLine(rect.X + cornerRadius, rect.Y, rect.Left + rect.Width / 2 - 10, rect.Y);//上
                    path.AddLine(rect.Left + rect.Width / 2 - 10, rect.Y, rect.Left + rect.Width / 2, rect.Y - 10);//上
                    path.AddLine(rect.Left + rect.Width / 2, rect.Y - 10, rect.Left + rect.Width / 2 + 10, rect.Y);//上
                    path.AddLine(rect.Left + rect.Width / 2 + 10, rect.Y, rect.Right - cornerRadius * 2, rect.Y);//上
                }
                else
                {
                    path.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);//上
                }
                #endregion
                path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);//右上角
                #region 右边
                if (m_location == AnchorTipsLocation.LEFT)
                {
                    path.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height / 2 - 10);//右
                    path.AddLine(rect.Right, rect.Y + rect.Height / 2 - 10, rect.Right + 19, rect.Y + rect.Height / 2);//右
                    path.AddLine(rect.Right + 10, rect.Y + rect.Height / 2, rect.Right, rect.Y + rect.Height / 2 + 10);//右
                    path.AddLine(rect.Right, rect.Y + rect.Height / 2 + 10, rect.Right, rect.Y + rect.Height - cornerRadius * 2);//右            
                }
                else
                {
                    path.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);//右
                }
                #endregion
                path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);//右下角
                #region 下边
                if (m_location == AnchorTipsLocation.TOP)
                {
                    path.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.Left + rect.Width / 2 + 10, rect.Bottom);
                    path.AddLine(rect.Left + rect.Width / 2 + 10, rect.Bottom, rect.Left + rect.Width / 2, rect.Bottom + 10);
                    path.AddLine(rect.Left + rect.Width / 2, rect.Bottom + 10, rect.Left + rect.Width / 2 - 10, rect.Bottom);
                    path.AddLine(rect.Left + rect.Width / 2 - 10, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
                }
                else
                {
                    path.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
                }
                #endregion
                path.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);//左下角
                #region 左边
                if (m_location == AnchorTipsLocation.RIGHT)
                {
                    path.AddLine(rect.Left, rect.Y + cornerRadius * 2, rect.Left, rect.Y + rect.Height / 2 - 10);//左
                    path.AddLine(rect.Left, rect.Y + rect.Height / 2 - 10, rect.Left - 10, rect.Y + rect.Height / 2);//左
                    path.AddLine(rect.Left - 10, rect.Y + rect.Height / 2, rect.Left, rect.Y + rect.Height / 2 + 10);//左
                    path.AddLine(rect.Left, rect.Y + rect.Height / 2 + 10, rect.Left, rect.Y + rect.Height - cornerRadius * 2);//左          
                }
                else
                {
                    path.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);//左
                }
                #endregion
                path.CloseFigure();
                #endregion

                this.Size = formSize;
                using (Bitmap bit = new Bitmap(formSize.Width, formSize.Height))
                {
                    #region 画图    English:Drawing
                    using (Graphics gBit = Graphics.FromImage(bit))
                    {
                        gBit.SetGDIHigh();
                        gBit.FillPath(new SolidBrush(_background), path);
                        if (Draw != null)
                            Draw(gBit);
                    }
                    #endregion
                    using (var g = this.CreateGraphics())
                        SetBits(bit);
                }
            }
        }



        public void ResetFormNoArrow(Point initPoint)
        {
            Color _background = m_background == null ? Color.FromArgb(100, 255, 77, 58) : m_background.Value;

            Font _font = WinDo.Utilities.PublicResource.WDFonts.TextFont;
            var formSize = new Size(100, 100);
            if (GetFormSize != null)
                formSize = GetFormSize();

            #region 获取窗体path    English:Get the form path
            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect;
                switch (m_location)
                {
                    case AnchorTipsLocation.TOP:
                        rect = new Rectangle(1, 1, formSize.Width - 2, formSize.Height - 1);
                        this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Y - rect.Height);
                        break;
                    case AnchorTipsLocation.RIGHT:
                        rect = new Rectangle(1, 1, formSize.Width - 1, formSize.Height - 2);
                        this.Location = new Point(m_rectControl.Right, m_rectControl.Y + (m_rectControl.Height - rect.Height) / 2);
                        break;
                    case AnchorTipsLocation.BOTTOM:
                        rect = new Rectangle(1, 1, formSize.Width - 2, formSize.Height - 1);
                        this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Bottom);
                        break;
                    default:
                        rect = new Rectangle(1, 1, formSize.Width - 1, formSize.Height - 2);
                        this.Location = new Point(m_rectControl.X - rect.Width, m_rectControl.Y + (m_rectControl.Height - rect.Height) / 2);
                        break;
                }
                if (initPoint != Point.Empty)
                {
                    this.Location = initPoint;
                }
                int cornerRadius = 2;

                //计算是否超出下边界
                var curScreen = Screen.FromPoint(m_rectControl.Location);
                if (this.Top + formSize.Height > curScreen.WorkingArea.Bottom && m_location == AnchorTipsLocation.BOTTOM)
                {
                    m_location = AnchorTipsLocation.TOP;
                    rect = new Rectangle(1, 1, formSize.Width - 2, formSize.Height - 1);
                    this.Location = new Point(m_rectControl.X + (m_rectControl.Width - rect.Width) / 2, m_rectControl.Y - rect.Height);
                }

                path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);//左上角
                #region 上边

                path.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);//上
                #endregion
                path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);//右上角
                #region 右边
                path.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);//右
                #endregion
                path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);//右下角
                #region 下边
                path.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
                #endregion
                path.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);//左下角
                #region 左边
                path.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);//左
                #endregion
                path.CloseFigure();
                #endregion

                this.Size = formSize;
                using (Bitmap bit = new Bitmap(formSize.Width, formSize.Height))
                {
                    #region 画图    English:Drawing
                    using (Graphics gBit = Graphics.FromImage(bit))
                    {
                        gBit.SetGDIHigh();
                        gBit.FillPath(new SolidBrush(_background), path);
                        if (Draw != null)
                            Draw(gBit);
                    }
                    #endregion
                    using (var g = this.CreateGraphics())
                        SetBits(bit);
                }
            }
        }
    }


    /// <summary>
    /// Enum AnchorTipsLocation
    /// </summary>
    public enum AnchorTipsLocation
    {
        /// <summary>
        /// The left
        /// </summary>
        LEFT,
        /// <summary>
        /// The top
        /// </summary>
        TOP,
        /// <summary>
        /// The right
        /// </summary>
        RIGHT,
        /// <summary>
        /// The bottom
        /// </summary>
        BOTTOM
    }

    /// <summary>
    /// Class Win32.
    /// </summary>
    class Win32
    {
        /// <summary>
        /// Struct Size
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            /// <summary>
            /// The cx
            /// </summary>
            public Int32 cx;
            /// <summary>
            /// The cy
            /// </summary>
            public Int32 cy;

            /// <summary>
            /// Initializes a new instance of the <see cref="Size" /> struct.
            /// </summary>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            public Size(Int32 x, Int32 y)
            {
                cx = x;
                cy = y;
            }
        }

        /// <summary>
        /// Struct BLENDFUNCTION
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            /// <summary>
            /// The blend op
            /// </summary>
            public byte BlendOp;
            /// <summary>
            /// The blend flags
            /// </summary>
            public byte BlendFlags;
            /// <summary>
            /// The source constant alpha
            /// </summary>
            public byte SourceConstantAlpha;
            /// <summary>
            /// The alpha format
            /// </summary>
            public byte AlphaFormat;
        }

        /// <summary>
        /// Struct Point
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            /// <summary>
            /// The x
            /// </summary>
            public Int32 x;
            /// <summary>
            /// The y
            /// </summary>
            public Int32 y;

            /// <summary>
            /// Initializes a new instance of the <see cref="Point" /> struct.
            /// </summary>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            public Point(Int32 x, Int32 y)
            {
                this.x = x;
                this.y = y;
            }
        }

        /// <summary>
        /// The ac source over
        /// </summary>
        public const byte AC_SRC_OVER = 0;
        /// <summary>
        /// The ulw alpha
        /// </summary>
        public const Int32 ULW_ALPHA = 2;
        /// <summary>
        /// The ac source alpha
        /// </summary>
        public const byte AC_SRC_ALPHA = 1;

        /// <summary>
        /// Creates the compatible dc.
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        /// <summary>
        /// Gets the dc.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// Selects the object.
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <param name="hObj">The h object.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObj);

        /// <summary>
        /// Releases the dc.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="hDC">The h dc.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// Deletes the dc.
        /// </summary>
        /// <param name="hDC">The h dc.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteDC(IntPtr hDC);

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="hObj">The h object.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteObject(IntPtr hObj);

        /// <summary>
        /// Updates the layered window.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="hdcDst">The HDC DST.</param>
        /// <param name="pptDst">The PPT DST.</param>
        /// <param name="psize">The psize.</param>
        /// <param name="hdcSrc">The HDC source.</param>
        /// <param name="pptSrc">The PPT source.</param>
        /// <param name="crKey">The cr key.</param>
        /// <param name="pblend">The pblend.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        /// <summary>
        /// Exts the create region.
        /// </summary>
        /// <param name="lpXform">The lp xform.</param>
        /// <param name="nCount">The n count.</param>
        /// <param name="rgnData">The RGN data.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr ExtCreateRegion(IntPtr lpXform, uint nCount, IntPtr rgnData);
    }
}
