using WinDo.Utilities.PublicResource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Forms;

namespace WinDoControls.Controls
{
    /// <summary>
    /// 百分比饼图
    /// </summary>
    public class CircularChart : System.Windows.Forms.Control
    {

        public CircularChart()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.BackColor = Color.White;
            this.Size = new Size(140, 140);
            this.ParentChanged += CircularChart_ParentChanged;
            this.VisibleChanged += CircularChart_VisibleChanged;
        }

        private void CircularChart_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                CloseTips();
            }
        }

        private void CircularChart_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent == null)
                return;
            this.Parent.MouseMove -= Parent_MouseMove1;
            this.Parent.MouseMove += Parent_MouseMove1;
        }

        private void Parent_MouseMove1(object sender, MouseEventArgs e)
        {
            CloseTips();
        }

        private int arcRadius = 70;//140/2
        /// <summary>
        /// 圆形半径
        /// </summary>
        [DefaultValue(50)]
        [Description("圆形半径")]
        public int ArcRadius
        {
            get { return this.arcRadius; }
            set
            {
                if (this.arcRadius == value || value < 0)
                    return;
                this.arcRadius = value;
                this.Invalidate();
            }
        }

        private int arcThickness = 12;
        /// <summary>
        /// 弧线大小
        /// </summary>
        [DefaultValue(12)]
        [Description("弧线大小")]
        public int ArcThickness
        {
            get { return this.arcThickness; }
            set
            {
                if (this.arcThickness == value || value < 0 || value > this.ArcRadius)
                    return;
                this.arcThickness = value;
                this.Invalidate();
            }
        }

        private Color arcAnnulusBackColor = Color.Empty;
        /// <summary>
        /// 环形背景颜色
        /// </summary>
        [DefaultValue(typeof(Color), "White")]
        [Description("环形背景颜色")]
        public Color ArcAnnulusBackColor
        {
            get { return this.arcAnnulusBackColor; }
            set
            {
                if (this.arcAnnulusBackColor == value)
                    return;
                this.arcAnnulusBackColor = value;
                this.Invalidate();
            }
        }

        private Color arcBackColor = Color.White;
        /// <summary>
        /// 弧线背景颜色
        /// </summary>
        [DefaultValue(typeof(Color), "White")]
        [Description("弧线背景颜色")]
        public Color ArcBackColor
        {
            get { return this.arcBackColor; }
            set
            {
                if (this.arcBackColor == value)
                    return;
                this.arcBackColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 技术信息集合
        /// </summary>
        public List<ArcInfo> ArcInfos = new List<ArcInfo>();

        protected override void OnParentChanged(EventArgs e)
        {
            if (this.Parent != null)
            {
                Parent.MouseMove -= Parent_MouseMove;
                Parent.MouseMove += Parent_MouseMove;
            }
            base.OnParentChanged(e);
        }

        private void Parent_MouseMove(object sender, MouseEventArgs e)
        {
            CloseTips();
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var arc in ArcInfos)
            {
                if (arc.ArcRegion != null)
                {
                    arc.ArcRegion.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        FrmAnchorTips _frmAnchorTips = null;

        public void CloseTips()
        {
            if (_frmAnchorTips != null)
            {
                _frmAnchorTips.Close();
                _frmAnchorTips = null;
            }
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (ArcInfos.Count == 0)
            {
                CloseTips();
            }
            foreach (var arc in ArcInfos)
            {
                if (arc.ArcRegion != null && arc.ArcRegion.IsVisible(e.Location))
                {
                    var rect = arc.ArcRectangle;
                    var p = this.Parent.PointToScreen(this.Location);
                    rect.Offset(p);
                    if ((_frmAnchorTips != null && _frmAnchorTips.RectControl != rect) || (_frmAnchorTips != null && !_frmAnchorTips.Visible))
                    {
                        _frmAnchorTips.Close();
                        _frmAnchorTips = null;
                    }
                    var tips = arc.Function + "：人次 " + arc.Num + "  野数 " + arc.FieldNum;
                    if (_frmAnchorTips == null)
                    {
                        _frmAnchorTips = FrmAnchorTips.ShowTips(rect, tips, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 2000);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            Rectangle rect = new Rectangle((int)this.ClientRectangle.X + ((int)this.ClientRectangle.Width / 2 - this.ArcRadius) + 1, (int)this.ClientRectangle.Y + ((int)this.ClientRectangle.Height / 2 - this.ArcRadius) + 1, this.ArcRadius * 2 - 2, this.ArcRadius * 2 - 2);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            #region 背景
            using (Pen arcback_pen = new Pen(this.ArcBackColor, this.ArcThickness))
                g.DrawArc(arcback_pen, ControlCommom.TransformRectangleByPen(rect, this.ArcThickness), 270, 360);
            #endregion
            if (this.ArcAnnulusBackColor != Color.Empty)
            {
                using (SolidBrush arcannulusback_sb = new SolidBrush(this.ArcAnnulusBackColor))
                    g.FillPie(arcannulusback_sb, rect.X + this.ArcThickness, rect.Y + this.ArcThickness, rect.Width - this.ArcThickness * 2, rect.Height - this.ArcThickness * 2, 270, 360);
            }

            #region 百分比值

            var startAngle = 270f;
            if (ArcInfos == null || ArcInfos.Count == 0)
            {
                using (var arc_pen = new Pen(Color.LightGray, this.ArcThickness))
                {
                    //g.DrawRectangle(Pens.Black, rect);
                    var rectP = ControlCommom.TransformRectangleByPen(rect, this.ArcThickness);
                    g.DrawArc(arc_pen, rectP, startAngle, 360);
                }
            }
            else
            {

                //g.DrawRectangle(Pens.Black, rect);
                foreach (var arc in ArcInfos)
                {
                    var percentValue = arc.Num / (float)ArcInfos.Sum(v => v.Num);
                    using (var arc_pen = new Pen(arc.FunctionColor, this.ArcThickness))
                    {
                        var subPercentValue = percentValue;
                        //if (ArcInfos.Count > 1)
                        //    subPercentValue = (percentValue - 0.005f);
                        var rectP = ControlCommom.TransformRectangleByPen(rect, this.ArcThickness);
                        g.DrawArc(arc_pen, rectP, startAngle, 360 * subPercentValue);

                        if (arc.ArcRegion != null)
                        {
                            arc.ArcRegion.Dispose();
                        }
                        //外圆
                        //Create a graphics path
                        using (GraphicsPath pathW = new GraphicsPath())
                        {
                            pathW.AddPie(rect, startAngle, 360 * subPercentValue);
                            //g.DrawPath(Pens.Red, pathW);
                            //Create a Region object from the path and set it as the form's region
                            Region rgnW = new Region(pathW);
                            {
                                //内圆
                                var rectN = rect;
                                rectN.Inflate(-this.arcThickness, -this.arcThickness);
                                //g.DrawRectangle(Pens.Blue, rectN);
                                using (GraphicsPath pathN = new GraphicsPath())
                                {
                                    pathN.AddPie(rectN, startAngle, 360);
                                    //g.DrawPath(Pens.Red, pathN);
                                    //Create a Region object from the path and set it as the form's region
                                    using (Region rgnN = new Region(pathN))
                                    {
                                        rgnW.Exclude(rgnN);
                                        arc.ArcRegion = rgnW;
                                        var cp = Point.Ceiling(ControlCommom.RegionCentroid(arc.ArcRegion, g.Transform));
                                        cp.Offset(-1, -1);
                                        arc.ArcRectangle = new Rectangle(cp, new Size(1, 1));

                                        //g.DrawRectangle(Pens.Black, arc.ArcRectangle);

                                        //using (var brush = new SolidBrush(item.Color))
                                        //    g.FillRegion(brush, rgnW);
                                    }
                                }
                            }
                        }
                    }
                    startAngle += 360 * (percentValue);
                }

                var rrr = new Rectangle(56, 0, this.ArcThickness + 2, 2);

                if (ArcInfos.Count > 1)
                    foreach (var arc in ArcInfos)
                    {
                        var percentValue = arc.Num / (float)ArcInfos.Sum(v => v.Num);
                        {
                            e.Graphics.ResetTransform();
                            g.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
                            e.Graphics.RotateTransform(startAngle + (percentValue - 0.005f) * 360);
                            e.Graphics.FillRectangle(Brushes.White, rrr);
                            e.Graphics.ResetTransform();
                        }
                        startAngle += 360 * (percentValue);
                    }
            }

            #endregion

            #region 文本
            this.DrawValueText(g, rect);
            #endregion

        }
        public void RotateRectangle(Graphics g, Rectangle r, float angle)
        {
            using (Matrix m = new Matrix())
            {
                m.RotateAt(angle, new PointF(r.Left + (r.Width / 2),
                                          r.Top + (r.Height / 2)));
                g.Transform = m;
                g.DrawRectangle(Pens.Black, r);
                g.ResetTransform();
            }
        }

        private Color valueColor = ColorTranslator.FromHtml("#7e7e7e");
        /// <summary>
        /// 百分比值颜色
        /// </summary>
        [DefaultValue(typeof(Color), "#7e7e7e")]
        [Description("百分比值颜色")]
        //[Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color ValueColor
        {
            get { return this.valueColor; }
            set
            {
                if (this.valueColor == value)
                    return;
                this.valueColor = value;
                this.Invalidate();
            }
        }

        private Font valueFont = new Font("微软雅黑", 14, FontStyle.Bold, GraphicsUnit.Pixel);
        /// <summary>
        /// 百分比值字体
        /// </summary>
        [DefaultValue(typeof(Font), "微软雅黑, 14px")]
        [Description("百分比值字体")]
        public Font ValueFont
        {
            get { return this.valueFont; }
            set
            {
                if (this.valueFont == value)
                    return;
                this.valueFont = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 绘制百分值文本
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        private void DrawValueText(Graphics g, RectangleF rectf)
        {
            using (StringFormat value_sf = new StringFormat())
            {
                value_sf.FormatFlags = StringFormatFlags.NoWrap;
                value_sf.Alignment = StringAlignment.Center;
                value_sf.Trimming = StringTrimming.None;
                value_sf.LineAlignment = StringAlignment.Far;
                //Size value_size = g.MeasureString(value_str, this.ValueFont, new Size(), value_sf).ToSize();

                using (SolidBrush value_sb = new SolidBrush(this.ValueColor))
                {
                    var rect1 = this.ClientRectangle;
                    rect1.Height = rect1.Height / 2;
                    g.DrawString("Info:", this.ValueFont, value_sb, rect1, value_sf);
                    value_sf.LineAlignment = StringAlignment.Near;
                    rect1.Offset(0, rect1.Height);
                    g.DrawString(ArcInfos.Sum(a => a.Num).ToString(), this.ValueFont, value_sb, rect1, value_sf);
                }

            }
        }
    }


    public class ControlCommom
    {
        public static PointF RegionCentroid(Region region, Matrix transform)
        {
            float mx = 0;
            float my = 0;
            float total_weight = 0;
            foreach (RectangleF rect in region.GetRegionScans(transform))
            {
                float rect_weight = rect.Width * rect.Height;
                mx += rect_weight * (rect.Left + rect.Width / 2f);
                my += rect_weight * (rect.Top + rect.Height / 2f);
                total_weight += rect_weight;
            }

            return new PointF(mx / total_weight, my / total_weight);
        }

        /// <summary>
        /// 转换成圆角
        /// </summary>
        /// <param name="rectf">要转换的rectf</param>
        /// <param name="radius">圆角半径的大小</param>
        /// <returns></returns>
        public static GraphicsPath TransformCircular(RectangleF rectf, float radius = 0)
        {
            return TransformCircular(rectf, radius, radius, radius, radius);
        }

        /// <summary>
        /// 转换成圆角
        /// </summary>
        /// <param name="rectf">要转换的rectf</param>
        /// <param name="leftTopRadius">左上角</param>
        /// <param name="rightTopRadius">右上角</param>
        /// <param name="rightBottomRadius">右下角</param>
        /// <param name="leftBottomRadius">左下角</param>
        /// <returns></returns>
        public static GraphicsPath TransformCircular(RectangleF rectf, float leftTopRadius = 0f, float rightTopRadius = 0f, float rightBottomRadius = 0f, float leftBottomRadius = 0f)
        {
            GraphicsPath gp = new GraphicsPath();
            if (leftTopRadius > 0)
            {
                RectangleF lefttop_rect = new RectangleF(rectf.X, rectf.Y, leftTopRadius * 2, leftTopRadius * 2);
                gp.AddArc(lefttop_rect, 180, 90);
            }
            else
            {
                gp.AddLine(new PointF(rectf.X, rectf.Y), new PointF(rightTopRadius > 0 ? rectf.Right - rightTopRadius * 2 : rectf.Right, rectf.Y));
            }
            if (rightTopRadius > 0)
            {
                RectangleF righttop_rect = new RectangleF(rectf.Right - rightTopRadius * 2, rectf.Y, rightTopRadius * 2, rightTopRadius * 2);
                gp.AddArc(righttop_rect, 270, 90);
            }
            else
            {
                gp.AddLine(new PointF(rectf.Right, rectf.Y), new PointF(rectf.Right, rightBottomRadius > 0 ? rectf.Bottom - rightTopRadius * 2 : rectf.Bottom));
            }
            if (rightBottomRadius > 0)
            {
                RectangleF rightbottom_rect = new RectangleF(rectf.Right - rightTopRadius * 2, rectf.Bottom - rightTopRadius * 2, rightBottomRadius * 2, rightBottomRadius * 2);
                gp.AddArc(rightbottom_rect, 0, 90);
            }
            else
            {
                gp.AddLine(new PointF(rectf.Right, rectf.Bottom), new PointF(leftBottomRadius > 0 ? leftBottomRadius * 2 : rectf.X, rectf.Bottom));
            }
            if (leftBottomRadius > 0)
            {
                RectangleF rightbottom_rect = new RectangleF(rectf.X, rectf.Bottom - leftBottomRadius * 2, leftBottomRadius * 2, leftBottomRadius * 2);
                gp.AddArc(rightbottom_rect, 90, 90);
            }
            else
            {
                gp.AddLine(new PointF(rectf.X, rectf.Bottom), new PointF(rectf.X, leftTopRadius > 0 ? rectf.X + leftTopRadius * 2 : rectf.X));
            }
            gp.CloseAllFigures();
            return gp;
        }

        /// <summary>
        /// 根据画笔大小计算出真是rectf
        /// </summary>
        /// <param name="rectf">要转换的rectf</param>
        /// <param name="pen">画笔大小大小</param>
        /// <returns></returns>
        public static RectangleF TransformRectangleF(RectangleF rectf, float pen)
        {
            RectangleF result = new RectangleF();
            result.Width = rectf.Width - (pen < 1 ? 0 : pen);
            result.Height = rectf.Height - (pen < 1 ? 0 : pen);
            result.X = rectf.X + (pen / 2f);
            result.Y = rectf.Y + (pen / 2f);
            return result;
        }

        /// <summary>
        /// 倒影变换
        /// </summary>
        /// <param name="bmp">原图片</param>
        /// <param name="reflectionTop">倒影边距</param>
        /// <param name="reflectionBrightness">明亮度</param>
        /// <param name="reflectionTransparentStart">倒影开始透明度</param>
        /// <param name="reflectionTransparentEnd">倒影结束透明度</param>
        /// <param name="reflectionHeight">倒影高度</param>
        /// <returns></returns>
        public static Bitmap TransformReflection(Bitmap bmp, int reflectionTop = 10, int reflectionBrightness = -50, int reflectionTransparentStart = 200, int reflectionTransparentEnd = -0, int reflectionHeight = 50)
        {
            /// <summary>
            /// 图片最终高度
            /// </summary>
            int finallyHeight = bmp.Height + reflectionTop + reflectionHeight;

            Color pixel;
            int transparentGradient = 0;//透明梯度
            transparentGradient = (reflectionTransparentEnd - reflectionTransparentStart) / reflectionHeight;
            if (transparentGradient == 0)
                transparentGradient = 1;

            Bitmap result = new Bitmap(bmp.Width, finallyHeight);
            Graphics graphic = Graphics.FromImage(result);
            graphic.DrawImage(bmp, new RectangleF(0, 0, bmp.Width, bmp.Height));
            graphic.Dispose();

            for (int y = 0; y < reflectionHeight; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    pixel = bmp.GetPixel(x, bmp.Height - 1 - y);
                    int a = VerifyRGB(reflectionTransparentStart + y * transparentGradient);
                    if (pixel.A == 0 || pixel.A < a)
                    {
                        result.SetPixel(x, bmp.Height - 1 + reflectionTop + y, pixel);
                    }
                    else
                    {
                        int r = VerifyRGB(pixel.R + reflectionBrightness);
                        int g = VerifyRGB(pixel.G + reflectionBrightness);
                        int b = VerifyRGB(pixel.B + reflectionBrightness);
                        result.SetPixel(x, bmp.Height - 1 + reflectionTop + y, Color.FromArgb(a, r, g, b));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 检查RGB值ed有效范围
        /// </summary>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static int VerifyRGB(int rgb)
        {
            if (rgb < 0)
                return 0;
            if (rgb > 255)
                return 255;
            return rgb;
        }

        /// <summary>
        /// 计算指定角度的坐标
        /// </summary>
        /// <param name="center">圆心坐标</param>
        /// <param name="radius">圆半径</param>
        /// <param name="angle">角度</param>
        /// <returns></returns>
        public static PointF CalculatePointForAngle(PointF center, float radius, float angle)
        {
            if (radius == 0)
                return center;

            float w = 0;
            float h = 0;
            if (angle <= 90)
            {
                w = radius * (float)Math.Cos(Math.PI / 180 * angle);
                h = radius * (float)Math.Sin(Math.PI / 180 * angle);
            }
            else if (angle <= 180)
            {
                w = -radius * (float)Math.Sin(Math.PI / 180 * (angle - 90));
                h = radius * (float)Math.Cos(Math.PI / 180 * (angle - 90));

            }
            else if (angle <= 270)
            {
                w = -radius * (float)Math.Cos(Math.PI / 180 * (angle - 180));
                h = -radius * (float)Math.Sin(Math.PI / 180 * (angle - 180));
            }
            else
            {
                w = radius * (float)Math.Sin(Math.PI / 180 * (angle - 270));
                h = -radius * (float)Math.Cos(Math.PI / 180 * (angle - 270));

            }
            return new PointF(center.X + w, center.Y + h);
        }

        /// <summary>
        /// 根据画笔大小转换rectf
        /// </summary>
        /// <param name="rectf">要转换的rectf</param>
        /// <param name="pen">画笔大小大小</param>
        /// <returns></returns>
        public static RectangleF TransformRectangleByPen(RectangleF rectf, float pen)
        {
            RectangleF result = new RectangleF();
            result.Width = rectf.Width - (pen < 1 ? 0 : pen);
            result.Height = rectf.Height - (pen < 1 ? 0 : pen);
            result.X = rectf.X + (float)(pen / 2);
            result.Y = rectf.Y + (float)(pen / 2);
            return result;
        }

        /// <summary>
        /// 结构转指针
        /// </summary>
        /// <typeparam name="T">结构类型</typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        public static IntPtr StructToIntPtr<T>(T info)
        {
            int size = Marshal.SizeOf(info);
            IntPtr intPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(info, intPtr, true);
            return intPtr;
        }

        /// <summary>
        /// 指针转结构
        /// </summary>
        /// <typeparam name="T">结构类型</typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        public static T IntPtrToStruct<T>(IntPtr info)
        {
            return (T)Marshal.PtrToStructure(info, typeof(T));
        }
    }

    /// <summary>
    /// 技术信息
    /// </summary>
    public class ArcInfo
    {
        /// <summary>
        /// 技术Code
        /// </summary>
        public string Function_Code { get; set; }
        /// <summary>
        /// 技术
        /// </summary>
        public string Function { get; set; }
        /// <summary>
        /// 技术颜色
        /// </summary>
        public Color FunctionColor { get; set; }
        /// <summary>
        /// 人次
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 野数
        /// </summary>
        public int FieldNum { get; set; }
        /// <summary>
        /// 弧线区域
        /// </summary>
        public Region ArcRegion { get; set; }

        /// <summary>
        /// 弧线矩形
        /// </summary>
        public Rectangle ArcRectangle { get; set; }

    }
}
