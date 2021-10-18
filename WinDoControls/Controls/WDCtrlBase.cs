using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{

    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class WDCtrlBase : UserControl, IContainerControl
    {



        protected bool _isRadius = true;




        protected int _cornerRadius = 1;





        protected bool _isShowRect = true;
        protected bool _isShowShadow = false;







        protected Color _rectColor = Color.FromArgb(220, 220, 220);




        protected int _rectWidth = 1;




        protected Color _fillColor = Color.White;




        [Description("是否圆角"), Category("自定义")]
        public virtual bool IsRadius
        {
            get
            {
                return this._isRadius;
            }
            set
            {
                this._isRadius = value;
                Invalidate();
            }
        }




        [Description("圆角角度"), Category("自定义")]
        public virtual int ConerRadius
        {
            get
            {
                return this._cornerRadius;
            }
            set
            {
                this._cornerRadius = Math.Max(value, 1);
                Invalidate();
            }
        }





        [Description("是否显示边框"), Category("自定义")]
        public virtual bool IsShowRect
        {
            get
            {
                return this._isShowRect;
            }
            set
            {
                this._isShowRect = value;
                Invalidate();
            }
        }





        [Description("是否显示阴影"), Category("自定义")]
        public virtual bool IsShowShadow
        {
            get
            {
                return this._isShowShadow;
            }
            set
            {
                this._isShowShadow = value;
                Invalidate();
            }
        }




        [Description("边框颜色"), Category("自定义")]
        public virtual Color RectColor
        {
            get
            {
                return this._rectColor;
            }
            set
            {
                this._rectColor = value;
                Invalidate();
            }
        }




        [Description("边框宽度"), Category("自定义")]
        public virtual int RectWidth
        {
            get
            {
                return this._rectWidth;
            }
            set
            {
                this._rectWidth = value;
                Invalidate();
            }
        }

        protected void SetFillColor(Color color)
        {
            this._fillColor = color;
            Invalidate();
        }

        protected Color _defaultFillColor;


        [Description("当使用边框时填充颜色，当值为背景色或透明色或空值则不填充"), Category("自定义")]
        public virtual Color FillColor
        {
            get
            {
                return this._fillColor;
            }
            set
            {
                this._fillColor = value;
                _defaultFillColor = _fillColor;
                Invalidate();
            }
        }

        #region 窗体边框阴影效果变量申明

        const int CS_DropSHADOW = 0x20000;
        const int GCL_STYLE = (-26);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        #endregion




        public WDCtrlBase()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.InitializeComponent();
            this.ParentChanged += new EventHandler(UCControlBase_ParentChanged);
            this.LocationChanged += UCControlBase_LocationChanged;
            base.Padding = new Padding(1);
        }

        private void UCControlBase_LocationChanged(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Invalidate();
            }
        }

        private Control preParent = null;

        void UCControlBase_ParentChanged(object sender, EventArgs e)
        {
            if (preParent != null)
                preParent.Paint -= new PaintEventHandler(Parent_Paint);
            //var ctrl = sender as Control;
            //if (ctrl == null) return;
            if (this.Parent != null)
            {
                preParent = this.Parent;
                this.Parent.Paint += new PaintEventHandler(Parent_Paint);
            }

        }



        void Parent_Paint(object sender, PaintEventArgs e)
        {
            Control control = sender as Control;
            if (control == null) return;
            if (this.IsDisposed || this.Disposing) return;
            //屏幕中位置
            if (!this.Visible) return;
            if (!this._btnEnabled)
            {
                return;
            };
            if (!this._isShowShadow) return;
            var dropShadowStruct = GetDropShadowStruct("DropShadow:0,2,20,11,#0000FF,noinset");
            if (dropShadowStruct == null || dropShadowStruct.Inset) return;

            DrawOutsetShadow(e.Graphics, dropShadowStruct, this);
        }

        private void DrawOutsetShadow(Graphics g, dynamic dropShadowStruct, Control control)
        {
            var rOuter = control.Bounds;
            var rInner = control.Bounds;
            rInner.Offset(dropShadowStruct.HShadow, dropShadowStruct.VShadow);
            rInner.Inflate(-dropShadowStruct.Blur, -dropShadowStruct.Blur);
            rOuter.Inflate(dropShadowStruct.Spread, dropShadowStruct.Spread);
            rOuter.Offset(dropShadowStruct.HShadow, dropShadowStruct.VShadow);
            var originalOuter = rOuter;

            using (var img = new Bitmap(originalOuter.Width, originalOuter.Height, g))
            {
                using (var g2 = Graphics.FromImage(img))
                {
                    var currentBlur = 0;
                    do
                    {
                        var transparency = (rOuter.Height - rInner.Height) / (double)(dropShadowStruct.Blur * 2 + dropShadowStruct.Spread * 2);
                        var color = Color.FromArgb(((int)(255 * (transparency * transparency))), this._rectColor);//dropShadowStruct.Color);
                        var rOutput = rInner;
                        rOutput.Offset(-originalOuter.Left, -originalOuter.Top);
                        DrawRoundedRectangle(g2, rOutput, currentBlur, Pens.Transparent, color);
                        rInner.Inflate(1, 1);
                        currentBlur = (int)((double)dropShadowStruct.Blur * (1 - (transparency * transparency)));
                    } while (rOuter.Contains(rInner));
                    g2.Flush();
                }
                g.DrawImage(img, originalOuter);
            }
        }

        private static dynamic GetDropShadowStruct(string desc)
        {
            if (string.IsNullOrWhiteSpace(desc)) return null;

            string[] dropShadowParams = desc.Split(':')[1].Split(',');
            var dropShadowStruct = new
            {
                HShadow = Convert.ToInt32(dropShadowParams[0]),
                VShadow = Convert.ToInt32(dropShadowParams[1]),
                Blur = Convert.ToInt32(dropShadowParams[2]),
                Spread = Convert.ToInt32(dropShadowParams[3]),
                Color = ColorTranslator.FromHtml(dropShadowParams[4]),
                Inset = dropShadowParams[5].ToLowerInvariant() == "inset"
            };
            return dropShadowStruct;
        }

        private void DrawRoundedRectangle(Graphics gfx, Rectangle bounds, int cornerRadius, Pen drawPen, Color fillColor)
        {
            int strokeOffset = Convert.ToInt32(Math.Ceiling(drawPen.Width));
            bounds = Rectangle.Inflate(bounds, -strokeOffset, -strokeOffset);
            using (var gfxPath = new GraphicsPath())
            {
                if (cornerRadius > 0)
                {
                    gfxPath.AddArc(bounds.X, bounds.Y, cornerRadius, cornerRadius, 180, 90);
                    gfxPath.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y, cornerRadius, cornerRadius, 270, 90);
                    gfxPath.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y + bounds.Height - cornerRadius, cornerRadius,
                                   cornerRadius, 0, 90);
                    gfxPath.AddArc(bounds.X, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                }
                else
                {
                    gfxPath.AddRectangle(bounds);
                }
                gfxPath.CloseAllFigures();
                gfx.FillPath(new SolidBrush(fillColor), gfxPath);
                if (drawPen != Pens.Transparent)
                {
                    using (var pen = new Pen(drawPen.Color))
                    {
                        pen.EndCap = pen.StartCap = LineCap.Round;
                        gfx.DrawPath(pen, gfxPath);
                    }
                }
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Visible)
            {
                if (this._isRadius)
                {
                    this.SetWindowRegion();
                }

                GraphicsPath graphicsPath = new GraphicsPath();
                if (this._isShowRect || (_fillColor != Color.Empty && _fillColor != Color.Transparent && _fillColor != this.BackColor))
                {
                    Rectangle clientRectangle = base.ClientRectangle;
                    if (_isRadius)
                    {
                        graphicsPath.AddArc(0, 0, _cornerRadius, _cornerRadius, 180f, 90f);
                        graphicsPath.AddArc(clientRectangle.Width - _cornerRadius - 1, 0, _cornerRadius, _cornerRadius, 270f, 90f);
                        graphicsPath.AddArc(clientRectangle.Width - _cornerRadius - 1, clientRectangle.Height - _cornerRadius - 1, _cornerRadius, _cornerRadius, 0f, 90f);
                        graphicsPath.AddArc(0, clientRectangle.Height - _cornerRadius - 1, _cornerRadius, _cornerRadius, 90f, 90f);
                        graphicsPath.CloseFigure();
                    }
                    else
                    {
                        var rect = new Rectangle(clientRectangle.X - 1, clientRectangle.Y - 1, clientRectangle.Width + 1, clientRectangle.Height + 1);
                        graphicsPath.AddRectangle(rect);
                    }
                }
                e.Graphics.SetGDIHigh();
                if (_fillColor != Color.Empty && _fillColor != Color.Transparent && _fillColor != this.BackColor)
                {
                    var fc = this._fillColor;
                    if (!_btnEnabled)
                    {
                        if (_fillColor == WDColors.geekblue6)
                            fc = WDColors.MainDisableColor;
                        else if (_fillColor == WDColors.red5)
                            fc = WDColors.RedDisableColor;
                        else if (_fillColor == WDColors.OrangeColor)
                            fc = WDColors.OrangeDisableColor;
                        else if (_fillColor == WDColors.WhiteColor || _fillColor == Color.White)
                            fc = WDColors.BtnDisableGrayBack;
                    }
                    using (var brush = new SolidBrush(fc))
                    {
                        e.Graphics.FillPath(brush, graphicsPath);
                    }
                }


                if (this._isShowRect)
                {
                    var rectColor = this._rectColor;
                    if (!_btnEnabled)
                    {
                        if (_fillColor == WDColors.geekblue6)
                            rectColor = WDColors.MainDisableColor;
                        else if (_fillColor == WDColors.red5)
                            rectColor = WDColors.RedDisableColor;
                        else if (_fillColor == WDColors.OrangeColor)
                            rectColor = WDColors.OrangeDisableColor;
                        else if (_fillColor == WDColors.WhiteColor || _fillColor == Color.White)
                            rectColor = WDColors.BtnDisableGrayBack;
                    }
                    Pen pen = new Pen(rectColor, (float)this._rectWidth);
                    e.Graphics.DrawPath(pen, graphicsPath);
                }
            }
            base.OnPaint(e);
        }


        protected bool _btnEnabled = true;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool BtnEnabled
        {
            get { return _btnEnabled; }
            set
            {
                _btnEnabled = value;
                this.Invalidate();
            }
        }


        protected void SetWindowRegion()
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(-1, -1, base.Width + 1, base.Height);
            path = this.GetRoundedRectPath(rect, this._cornerRadius);
            base.Region = new Region(path);
        }



        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            Rectangle rect2 = new Rectangle(rect.Location, new Size(radius, radius));
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect2, 180f, 90f);//左上角
            rect2.X = rect.Right - radius;
            graphicsPath.AddArc(rect2, 270f, 90f);//右上角
            rect2.Y = rect.Bottom - radius;
            rect2.Width += 1;
            rect2.Height += 1;
            graphicsPath.AddArc(rect2, 360f, 90f);//右下角           
            rect2.X = rect.Left;
            graphicsPath.AddArc(rect2, 90f, 90f);//左下角
            graphicsPath.CloseFigure();
            return graphicsPath;
        }






        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 20)
            {
                base.WndProc(ref m);
            }
        }
    }





    public class UCControlBaseShadow : WDCtrlBase
    {
        public UCControlBaseShadow()
        {
            base.IsShowShadow = true;

        }
        public string _shadowParms = "DropShadow:0,2,20,11,#0000FF,noinset";
        public string ShadowParms
        {
            get { return _shadowParms; }
            set { _shadowParms = value; }
        }
    }

    public class UCControlBaseWithError : WDCtrlBase
    {
        private bool isErrorColor = false;


        [Description("是否为错误状态"), Category("自定义")]
        public bool IsErrorColor
        {
            get { return isErrorColor; }
            set
            {
                isErrorColor = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Visible && IsErrorColor)
            {
                if (this._isRadius)
                {
                    this.SetWindowRegion();
                }

                GraphicsPath graphicsPath = new GraphicsPath();
                if (this._isShowRect || (_fillColor != Color.Empty && _fillColor != Color.Transparent && _fillColor != this.BackColor))
                {
                    Rectangle clientRectangle = base.ClientRectangle;
                    if (_isRadius)
                    {
                        graphicsPath.AddArc(0, 0, _cornerRadius, _cornerRadius, 180f, 90f);
                        graphicsPath.AddArc(clientRectangle.Width - _cornerRadius - 1, 0, _cornerRadius, _cornerRadius, 270f, 90f);
                        graphicsPath.AddArc(clientRectangle.Width - _cornerRadius - 1, clientRectangle.Height - _cornerRadius - 1, _cornerRadius, _cornerRadius, 0f, 90f);
                        graphicsPath.AddArc(0, clientRectangle.Height - _cornerRadius - 1, _cornerRadius, _cornerRadius, 90f, 90f);
                        graphicsPath.CloseFigure();
                    }
                    else
                    {
                        var rect = new Rectangle(clientRectangle.X - 1, clientRectangle.Y - 1, clientRectangle.Width + 1, clientRectangle.Height + 1);
                        graphicsPath.AddRectangle(rect);
                    }
                }
                e.Graphics.SetGDIHigh();
                if (_fillColor != Color.Empty && _fillColor != Color.Transparent && _fillColor != this.BackColor)
                    e.Graphics.FillPath(new SolidBrush(this._fillColor), graphicsPath);

                if (this._isShowRect)
                {
                    Color rectColor = IsErrorColor ? WDColors.ErrorRedColor : this._rectColor;
                    Pen pen = new Pen(rectColor, (float)this._rectWidth);
                    e.Graphics.DrawPath(pen, graphicsPath);
                }
            }
        }
    }

}
