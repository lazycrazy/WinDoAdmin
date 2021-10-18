using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Collections;
using System.Drawing.Text;
using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{
    /// <summary>
    /// 日期美化控件
    /// </summary>
    [ToolboxItem(true)]
    [Description("日期美化控件")]
    public partial class DateExt : Control
    {
        #region

        private bool backBorderShow = true;
        /// <summary>
        /// 是否显示边框
        /// </summary>
        [DefaultValue(true)]
        [Description("是否显示边框")]
        public bool BackBorderShow
        {
            get { return this.backBorderShow; }
            set
            {
                if (this.backBorderShow == value)
                    return;
                this.backBorderShow = value;
                this.Invalidate();
            }
        }

        private ImageAnchors imageAnchor = ImageAnchors.Right;
        /// <summary>
        /// 图片位置
        /// </summary>
        [DefaultValue(ImageAnchors.Right)]
        [Description("图片位置")]
        public ImageAnchors ImageAnchor
        {
            get { return this.imageAnchor; }
            set
            {
                if (this.imageAnchor == value)
                    return;
                this.imageAnchor = value;
                this.Invalidate();
            }
        }

        private Color backBorderColor = Color.FromArgb(192, 192, 192);
        /// <summary>
        /// 边框颜色
        /// </summary>
        [DefaultValue(typeof(Color), "192, 192, 192")]
        [Description("边框颜色")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color BackBorderColor
        {
            get { return this.backBorderColor; }
            set
            {
                if (this.backBorderColor == value)
                    return;
                this.backBorderColor = value;
                this.Invalidate();
            }
        }

        private DatePickerExt datePicker = null;
        /// <summary>
        /// 日期选择面板
        /// </summary>
        [Browsable(true)]
        [Description("日期选择面板")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DatePickerExt DatePicker
        {
            get { return this.datePicker; }
            set { this.datePicker = value; }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(130, 23);
            }
        }

        protected override Cursor DefaultCursor
        {
            get
            {
                return Cursors.Hand;
            }
        }

        /// <summary>
        /// 日期面板显示状态
        /// </summary>
        private bool DisplayStatus = false;

        private ToolStripDropDown tsdd = null;

        private ToolStripControlHost tsch = null;

        private readonly StringFormat date_sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.None, FormatFlags = StringFormatFlags.NoWrap };

        #endregion

        public DateExt()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();

            this.BackColor = Color.FromArgb(255, 255, 255);
            this.ForeColor = Color.FromArgb(105, 105, 105);
            this.Font = new Font("微软雅黑", 9);

            this.DatePicker = new DatePickerExt();
            this.tsdd = new ToolStripDropDown() { Padding = Padding.Empty };
            this.tsch = new ToolStripControlHost(this.DatePicker) { Margin = Padding.Empty, Padding = Padding.Empty };
            tsdd.Items.Add(this.tsch);


            this.tsdd.Closed += new ToolStripDropDownClosedEventHandler(this.tsdd_Closed);
            this.DatePicker.BottomBarConfirmClick += new DatePickerExt.BottomBarIiemClickEvent(this.DatePicker_BottomBarConfirmClick);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            if (this.BackBorderShow)
            {
                Pen backborder_pen = new Pen(this.BackBorderColor, 1);
                g.DrawRectangle(backborder_pen, new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1));
                backborder_pen.Dispose();
            }

            string date_format = this.DatePicker.DateDisplayType == DatePickerExt.DateDisplayTypes.Year ? "yyyy" : (this.DatePicker.DateDisplayType == DatePickerExt.DateDisplayTypes.YearMonth ? "yyyy-MM" : "yyyy-MM-dd");
            int image_width = 40;// global::AnimationLibrary.Properties.Resources.date.Width;
            int image_height = 32;// global::AnimationLibrary.Properties.Resources.date.Height;
            Rectangle image_rect = new Rectangle(e.ClipRectangle.Right - image_width - 2, (e.ClipRectangle.Y + e.ClipRectangle.Height - image_height) / 2, 16, 16);
            Rectangle date_rect = new Rectangle(e.ClipRectangle.X + 5, e.ClipRectangle.Y, e.ClipRectangle.Width - image_width - 12, e.ClipRectangle.Height);
            if (this.ImageAnchor == ImageAnchors.Left)
            {
                image_rect = new Rectangle(e.ClipRectangle.X + 2, (e.ClipRectangle.Y + e.ClipRectangle.Height - image_height) / 2, 16, 16);
                date_rect = new Rectangle(e.ClipRectangle.X + 2 + image_width + 5, e.ClipRectangle.Y, e.ClipRectangle.Width - image_width - 12, e.ClipRectangle.Height);
            }
            SolidBrush date_sb = new SolidBrush(this.ForeColor);
            //g.DrawImage(global::AnimationLibrary.Properties.Resources.date, image_rect); 日历按钮图片
            g.DrawString(this.DatePicker.Value.ToString(date_format), this.Font, date_sb, date_rect, date_sf);
            date_sb.Dispose();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!this.DisplayStatus)
            {
                tsdd.Show(this.PointToScreen(new Point(0, this.Height + 2)));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                if (this.date_sf != null)
                    this.date_sf.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DatePicker_BottomBarConfirmClick(object sender, DatePickerExt.BottomBarIiemEventArgs e)
        {
            this.tsdd.Close();
            this.DisplayStatus = false;
            this.Invalidate();
        }

        private void tsdd_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.Invalidate();
            this.DisplayStatus = false;
        }

        /// <summary>
        /// 图片位置
        /// </summary>
        [Description("图片位置")]
        public enum ImageAnchors
        {
            /// <summary>
            /// 左边
            /// </summary>
            Left,
            /// <summary>
            /// 右边
            /// </summary>
            Right
        }

    }

    /// <summary>
    /// 日期面板美化控件
    /// </summary>
    [ToolboxItem(true)]
    [DefaultProperty("Value")]
    [DefaultEvent("BottomBarConfirmClick")]
    [Description("日期面板美化控件")]
    [TypeConverter(typeof(DatePickerExpandableObjectConverter))]
    public partial class DatePickerExt : System.Windows.Forms.Control
    {
        public delegate void TopBarIiemClickEvent(object sender, TopBarIiemEventArgs e);
        public delegate void DateMainItemClickEvent(object sender, DateMainItemEventArgs e);
        public delegate void BottomBarIiemClickEvent(object sender, BottomBarIiemEventArgs e);

        #region 事件
        #region 顶部工具栏

        private event TopBarIiemClickEvent topBarPrevYearClick;
        /// <summary>
        /// 顶部工具栏上一年单击事件
        /// </summary>
        [Description("顶部工具栏上一年单击事件")]
        public event TopBarIiemClickEvent TopBarPrevYearClick
        {
            add { this.topBarPrevYearClick += value; }
            remove { this.topBarPrevYearClick -= value; }
        }

        private event TopBarIiemClickEvent topBarPrevMonthClick;
        /// <summary>
        /// 顶部工具栏上一月单击事件
        /// </summary>
        [Description("顶部工具栏上一月单击事件")]
        public event TopBarIiemClickEvent TopBarPrevMonthClick
        {
            add { this.topBarPrevMonthClick += value; }
            remove { this.topBarPrevMonthClick -= value; }
        }

        private event TopBarIiemClickEvent topBarMonthClick;
        /// <summary>
        /// 顶部工具栏月单击事件
        /// </summary>
        [Description("顶部工具栏月单击事件")]
        public event TopBarIiemClickEvent TopBarMonthClick
        {
            add { this.topBarMonthClick += value; }
            remove { this.topBarMonthClick -= value; }
        }

        private event TopBarIiemClickEvent topBarYearClick;
        /// <summary>
        /// 顶部工具栏年单击事件
        /// </summary>
        [Description("顶部工具栏年单击事件")]
        public event TopBarIiemClickEvent TopBarYearClick
        {
            add { this.topBarYearClick += value; }
            remove { this.topBarYearClick -= value; }
        }

        private event TopBarIiemClickEvent topBarNextMonthClick;
        /// <summary>
        /// 顶部工具栏下一月单击事件
        /// </summary>
        [Description("顶部工具栏下一月单击事件")]
        public event TopBarIiemClickEvent TopBarNextMonthClick
        {
            add { this.topBarNextMonthClick += value; }
            remove { this.topBarNextMonthClick -= value; }
        }

        private event TopBarIiemClickEvent topBarNextYearClick;
        /// <summary>
        /// 顶部工具栏下一年单击事件
        /// </summary>
        [Description("顶部工具栏下一年单击事件")]
        public event TopBarIiemClickEvent TopBarNextYearClick
        {
            add { this.topBarNextYearClick += value; }
            remove { this.topBarNextYearClick -= value; }
        }

        #endregion
        #region 日期面板

        private event DateMainItemClickEvent dateMainYearClick;
        /// <summary>
        /// 日期面板年选项单击事件
        /// </summary>
        [Description("日期面板年选项单击事件")]
        public event DateMainItemClickEvent DateMainYearClick
        {
            add { this.dateMainYearClick += value; }
            remove { this.dateMainYearClick -= value; }
        }

        private event DateMainItemClickEvent dateMainMonthClick;
        /// <summary>
        /// 日期面板月选项单击事件
        /// </summary>
        [Description("日期面板月选项单击事件")]
        public event DateMainItemClickEvent DateMainMonthClick
        {
            add { this.dateMainMonthClick += value; }
            remove { this.dateMainMonthClick -= value; }
        }

        private event DateMainItemClickEvent dateMainDayClick;
        /// <summary>
        /// 日期面板日选项单击事件
        /// </summary>
        [Description("日期面板日选项单击事件")]
        public event DateMainItemClickEvent DateMainDayClick
        {
            add { this.dateMainDayClick += value; }
            remove { this.dateMainDayClick -= value; }
        }

        #endregion
        #region 底部工具栏

        private event BottomBarIiemClickEvent bottomBarClearClick;
        /// <summary>
        /// 底部工具栏清除单击事件
        /// </summary>
        [Description("底部工具栏清除单击事件")]
        public event BottomBarIiemClickEvent BottomBarClearClick
        {
            add { this.bottomBarClearClick += value; }
            remove { this.bottomBarClearClick -= value; }
        }

        private event BottomBarIiemClickEvent bottomBarNowClick;
        /// <summary>
        /// 底部工具栏现在单击事件
        /// </summary>
        [Description("底部工具栏现在单击事件")]
        public event BottomBarIiemClickEvent BottomBarNowClick
        {
            add { this.bottomBarNowClick += value; }
            remove { this.bottomBarNowClick -= value; }
        }


        private event BottomBarIiemClickEvent bottomBarConfirmClick;
        /// <summary>
        /// 底部工具栏确认单击事件
        /// </summary>
        [Description("底部工具栏确认单击事件")]
        public event BottomBarIiemClickEvent BottomBarConfirmClick
        {
            add { this.bottomBarConfirmClick += value; }
            remove { this.bottomBarConfirmClick -= value; }
        }

        #endregion
        #endregion

        #region 属性

        private DateDisplayTypes dateDisplayType = DateDisplayTypes.YearMonthDay;
        /// <summary>
        /// 显示功能类型
        /// </summary>
        [DefaultValue(DateDisplayTypes.YearMonthDay)]
        [Description("显示功能类型")]
        public DateDisplayTypes DateDisplayType
        {
            get { return this.dateDisplayType; }
            set
            {
                if (this.dateDisplayType == value)
                    return;
                this.dateDisplayType = value;
                this.DateDisplayStatus = DateDisplayStatuss.Default;

                this.DateObject.year = this.Value.Year;
                this.DateObject.display_year = this.Value.Year;
                this.DateObject.month = this.Value.Month;
                this.DateObject.display_month = this.Value.Month;
                this.DateObject.day = this.Value.Day;
                this.DateObject.display_day = this.Value.Day;

                this.UpdateControlText();
                this.Invalidate();
            }
        }

        private DateDisplayStatuss dateDisplayStatus = DateDisplayStatuss.Default;
        /// <summary>
        /// 在指定显示功能类型下面板显示状态
        /// </summary>
        [Browsable(false)]
        [DefaultValue(DateDisplayStatuss.Default)]
        [Description("在指定显示功能类型下面板显示状态")]
        public DateDisplayStatuss DateDisplayStatus
        {
            get { return this.dateDisplayStatus; }
            set
            {
                if (this.dateDisplayStatus == value)
                    return;
                this.dateDisplayStatus = value;
            }
        }

        private bool dateReadOnly = false;
        /// <summary>
        /// 日期面板是否只读
        /// </summary>
        [DefaultValue(false)]
        [Description("日期面板是否只读")]
        public bool DateReadOnly
        {
            get { return this.dateReadOnly; }
            set
            {
                if (this.dateReadOnly == value)
                    return;
                this.dateReadOnly = value;
                this.UpdateControlText();
                this.Invalidate();
            }
        }

        private bool minMaxInput = false;
        /// <summary>
        /// 最小值最大值是否限制输入值(否则只限制选择面板)
        /// </summary>
        [DefaultValue(false)]
        [Description("最小值最大值是否限制输入值(否则只限制选择面板)")]
        public bool MinMaxInput
        {
            get { return this.minMaxInput; }
            set
            {
                if (this.minMaxInput == value)
                    return;
                this.minMaxInput = value;
                this.UpdateControlText();
                this.Invalidate();
            }
        }

        private bool minMaxTip = true;
        /// <summary>
        /// 是否显示最小值最大值限制提示信息
        /// </summary>
        [DefaultValue(true)]
        [Description("是否显示最小值最大值限制提示信息")]
        public bool MinMaxTip
        {
            get { return this.minMaxTip; }
            set
            {
                if (this.minMaxTip == value)
                    return;
                this.minMaxTip = value;
                this.UpdateControlText();
                this.Invalidate();
            }
        }

        private DateTime minValue = minDate;
        /// <summary>
        /// 最小日期
        /// </summary>
        [DefaultValue(typeof(DateTime), "1753,1,1")]
        [Description("最小日期")]
        public DateTime MinValue
        {
            get { return this.minValue; }
            set
            {
                if (this.minValue.Date == value.Date)
                    return;
                if (value.Date < minDate)
                    value = minDate;
                if (value.Date > this.MaxValue)
                    value = this.MaxValue;
                this.minValue = value.Date;
                this.UpdateControlText();
                this.Invalidate();
            }
        }

        private DateTime maxValue = maxDate;
        /// <summary>
        /// 最大日期
        /// </summary>
        [DefaultValue(typeof(DateTime), "9998,12,31")]
        [Description("最大日期")]
        public DateTime MaxValue
        {
            get { return this.maxValue; }
            set
            {
                if (this.maxValue.Date == value.Date)
                    return;
                if (value.Date > maxDate)
                    value = maxDate;
                if (value.Date < this.MinValue)
                    value = this.MinValue;
                this.maxValue = value.Date;
                this.UpdateControlText();
                this.Invalidate();
            }
        }

        private DateTime value = minDate;
        /// <summary>
        /// 日期
        /// </summary>
        [Description("日期")]
        public DateTime Value
        {
            get { return this.value.Date; }
            set
            {
                if (this.value.Date == value.Date)
                    return;
                if (this.MinMaxInput)
                {
                    if (value.Date < this.MinValue)
                        value = this.MinValue;
                    if (value.Date > this.MaxValue)
                        value = this.MaxValue;
                }
                this.value = value.Date;
                this.DateObject.year = value.Year;
                this.DateObject.display_year = value.Year;
                this.DateObject.month = value.Month;
                this.DateObject.display_month = value.Month;
                this.DateObject.day = value.Day;
                this.DateObject.display_day = value.Day;

                this.UpdateControlText();
                this.Invalidate();
            }
        }


        #region 顶部工具栏

        private Color topBarBackColor = Color.FromArgb(153, 204, 153);
        /// <summary>
        /// 顶部工具栏背景颜色
        /// </summary>
        [DefaultValue(typeof(Color), "153, 204, 153")]
        [Description("顶部工具栏背景颜色")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color TopBarBackColor
        {
            get { return this.topBarBackColor; }
            set
            {
                if (this.topBarBackColor == value)
                    return;
                this.topBarBackColor = value;
            }
        }

        private Color topBarBtnForeColor = Color.FromArgb(255, 255, 255);
        /// <summary>
        /// 顶部工具栏按钮字体颜色
        /// </summary>
        [DefaultValue(typeof(Color), "255, 255, 255")]
        [Description("顶部工具栏按钮字体颜色")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color TopBarBtnForeColor
        {
            get { return this.topBarBtnForeColor; }
            set
            {
                if (this.topBarBtnForeColor == value)
                    return;
                this.topBarBtnForeColor = value;
            }
        }

        private Color topBarBtnForeEnterColor = Color.FromArgb(200, 255, 255, 255);
        /// <summary>
        /// 顶部工具栏按钮字体颜色(鼠标进入)
        /// </summary>
        [DefaultValue(typeof(Color), "200, 255, 255, 255")]
        [Description("顶部工具栏按钮字体颜色(鼠标进入)")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color TopBarBtnForeEnterColor
        {
            get { return this.topBarBtnForeEnterColor; }
            set
            {
                if (this.topBarBtnForeEnterColor == value)
                    return;
                this.topBarBtnForeEnterColor = value;
            }
        }

        #endregion

        #region 日期面板

        private Color dateBackColor = Color.FromArgb(255, 255, 255);
        /// <summary>
        /// 日期面板背景颜色
        /// </summary>
        [DefaultValue(typeof(Color), "255, 255, 255")]
        [Description("日期面板背景颜色")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color DateBackColor
        {
            get { return this.dateBackColor; }
            set
            {
                if (this.dateBackColor == value)
                    return;
                this.dateBackColor = value;
            }
        }

        private Color dateTitleForeColor = Color.FromArgb(105, 105, 105);
        /// <summary>
        /// 日期面板标题字体颜色
        /// </summary>
        [DefaultValue(typeof(Color), "105, 105, 105")]
        [Description("日期面板标题字体颜色")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color DateTitleForeColor
        {
            get { return this.dateTitleForeColor; }
            set
            {
                if (this.dateTitleForeColor == value)
                    return;
                this.dateTitleForeColor = value;
            }
        }

        private Color datePastForeColor = Color.FromArgb(135, 206, 235);
        /// <summary>
        /// 日期面板过期日期字体颜色
        /// </summary>
        [DefaultValue(typeof(Color), "135, 206, 235")]
        [Description("日期面板过期日期字体颜色")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color DatePastForeColor
        {
            get { return this.datePastForeColor; }
            set
            {
                if (this.datePastForeColor == value)
                    return;
                this.datePastForeColor = value;
            }
        }

        private Color dateNormalForeColor = Color.FromArgb(153, 204, 153);
        /// <summary>
        /// 日期面板正常日期字体颜色
        /// </summary>
        [DefaultValue(typeof(Color), "153, 204, 153")]
        [Description("日期面板正常日期字体颜色")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color DateNormalForeColor
        {
            get { return this.dateNormalForeColor; }
            set
            {
                if (this.dateNormalForeColor == value)
                    return;
                this.dateNormalForeColor = value;
            }
        }

        private Color dateFutureForeColor = Color.FromArgb(135, 206, 235);
        /// <summary>
        /// 日期面板未来日期字体颜色
        /// </summary>
        [DefaultValue(typeof(Color), "135, 206, 235")]
        [Description("日期面板未来日期字体颜色")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color DateFutureForeColor
        {
            get { return this.dateFutureForeColor; }
            set
            {
                if (this.dateFutureForeColor == value)
                    return;
                this.dateFutureForeColor = value;
            }
        }

        private Color dateBackEnterColor = Color.FromArgb(189, 220, 220, 220);
        /// <summary>
        /// 日期面板日期背景颜色(鼠标进入)
        /// </summary>
        [DefaultValue(typeof(Color), "189, 220, 220, 220")]
        [Description("日期面板日期背景颜色(鼠标进入)")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color DateBackEnterColor
        {
            get { return this.dateBackEnterColor; }
            set
            {
                if (this.dateBackEnterColor == value)
                    return;
                this.dateBackEnterColor = value;
            }
        }

        private Color dateBackSelectedColor = Color.FromArgb(153, 204, 153);
        /// <summary>
        /// 日期面板日期背景颜色(选中)
        /// </summary>
        [DefaultValue(typeof(Color), "153, 204, 153")]
        [Description("日期面板日期背景颜色(选中)")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color DateBackSelectedColor
        {
            get { return this.dateBackSelectedColor; }
            set
            {
                if (this.dateBackSelectedColor == value)
                    return;
                this.dateBackSelectedColor = value;
            }
        }

        private Color dateForeSelectedColor = Color.FromArgb(255, 255, 255);
        /// <summary>
        /// 日期面板日期字体颜色(选中)
        /// </summary>
        [DefaultValue(typeof(Color), "255, 255, 255")]
        [Description("日期面板日期字体颜色(选中)")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color DateForeSelectedColor
        {
            get { return this.dateForeSelectedColor; }
            set
            {
                if (this.dateForeSelectedColor == value)
                    return;
                this.dateForeSelectedColor = value;
            }
        }

        private Color dateBackDisabledColor = Color.FromArgb(220, 220, 220);
        /// <summary>
        /// 日期面板日期背景颜色(禁用)
        /// </summary>
        [DefaultValue(typeof(Color), "220, 220, 220")]
        [Description("日期面板日期背景颜色(禁用)")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color DateBackDisabledColor
        {
            get { return this.dateBackDisabledColor; }
            set
            {
                if (this.dateBackDisabledColor == value)
                    return;
                this.dateBackDisabledColor = value;
            }
        }

        private Color dateForeDisabledColor = Color.FromArgb(192, 192, 192);
        /// <summary>
        /// 日期面板日期字体颜色(禁用)
        /// </summary>
        [DefaultValue(typeof(Color), "192, 192, 192")]
        [Description("日期面板日期字体颜色(禁用)")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color DateForeDisabledColor
        {
            get { return this.dateForeDisabledColor; }
            set
            {
                if (this.dateForeDisabledColor == value)
                    return;
                this.dateForeDisabledColor = value;
            }
        }

        #endregion

        #region 底部工具栏

        private Color bottomBarBackBorderColor = Color.FromArgb(233, 233, 233);
        /// <summary>
        /// 底部工具栏边框颜色
        /// </summary>
        [DefaultValue(typeof(Color), "233, 233, 233")]
        [Description("底部工具栏边框颜色")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color BottomBarBackBorderColor
        {
            get { return this.bottomBarBackBorderColor; }
            set
            {
                if (this.bottomBarBackBorderColor == value)
                    return;
                this.bottomBarBackBorderColor = value;
            }
        }

        private Color bottomBarBackColor = Color.FromArgb(255, 255, 255);
        /// <summary>
        /// 底部工具栏背景颜色
        /// </summary>
        [DefaultValue(typeof(Color), "255, 255, 255")]
        [Description("底部工具栏背景颜色")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color BottomBarBackColor
        {
            get { return this.bottomBarBackColor; }
            set
            {
                if (this.bottomBarBackColor == value)
                    return;
                this.bottomBarBackColor = value;
            }
        }

        private Color bottomBarBtnBackColor = Color.FromArgb(153, 204, 153);
        /// <summary>
        /// 底部工具栏按钮背景颜色(正常)
        /// </summary>
        [DefaultValue(typeof(Color), "153, 204, 153")]
        [Description("底部工具栏按钮背景颜色(正常)")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color BottomBarBtnBackColor
        {
            get { return this.bottomBarBtnBackColor; }
            set
            {
                if (this.bottomBarBtnBackColor == value)
                    return;
                this.bottomBarBtnBackColor = value;
            }
        }

        private Color bottomBarBtnForeColor = Color.FromArgb(255, 255, 255);
        /// <summary>
        /// 底部工具栏按钮字体颜色(正常)
        /// </summary>
        [DefaultValue(typeof(Color), "255,255,255")]
        [Description("底部工具栏按钮字体颜色(正常)")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color BottomBarBtnForeColor
        {
            get { return this.bottomBarBtnForeColor; }
            set
            {
                if (this.bottomBarBtnForeColor == value)
                    return;
                this.bottomBarBtnForeColor = value;
            }
        }

        private Color bottomBarBtnBackDisabledColor = Color.FromArgb(170, 153, 204, 153);
        /// <summary>
        /// 底部工具栏按钮背景颜色(禁用)
        /// </summary>
        [DefaultValue(typeof(Color), "170, 153, 204, 153")]
        [Description("底部工具栏按钮背景颜色(禁用)")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color BottomBarBtnBackDisabledColor
        {
            get { return this.bottomBarBtnBackDisabledColor; }
            set
            {
                if (this.bottomBarBtnBackDisabledColor == value)
                    return;
                this.bottomBarBtnBackDisabledColor = value;
            }
        }

        private Color bottomBarBtnForeDisabledColor = Color.FromArgb(170, 255, 255, 255);
        /// <summary>
        /// 底部工具栏按钮字体颜色(禁用)
        /// </summary>
        [DefaultValue(typeof(Color), "170, 255, 255, 255")]
        [Description("底部工具栏按钮字体颜色(禁用)")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color BottomBarBtnForeDisabledColor
        {
            get { return this.bottomBarBtnForeDisabledColor; }
            set
            {
                if (this.bottomBarBtnForeDisabledColor == value)
                    return;
                this.bottomBarBtnForeDisabledColor = value;
            }
        }

        private Color bottomBarBtnBackEnterColor = Color.FromArgb(200, 153, 204, 153);
        /// <summary>
        /// 底部工具栏按钮背景颜色(鼠标进入)
        /// </summary>
        [DefaultValue(typeof(Color), "200, 153, 204, 153")]
        [Description("底部工具栏按钮背景颜色(鼠标进入)")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color BottomBarBtnBackEnterColor
        {
            get { return this.bottomBarBtnBackEnterColor; }
            set
            {
                if (this.bottomBarBtnBackEnterColor == value)
                    return;
                this.bottomBarBtnBackEnterColor = value;
            }
        }

        private Color bottomBarBtnForeEnterColor = Color.FromArgb(200, 255, 255, 255);
        /// <summary>
        /// 底部工具栏按钮字体颜色(鼠标进入)
        /// </summary>
        [DefaultValue(typeof(Color), "200,255,255,255")]
        [Description("底部工具栏按钮字体颜色(鼠标进入)")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color BottomBarBtnForeEnterColor
        {
            get { return this.bottomBarBtnForeEnterColor; }
            set
            {
                if (this.bottomBarBtnForeEnterColor == value)
                    return;
                this.bottomBarBtnForeEnterColor = value;
            }
        }

        private Color bottomBarTipForeColor = Color.FromArgb(255, 204, 153);
        /// <summary>
        /// 底部工具栏最小最大限制提示字体颜色
        /// </summary>
        [DefaultValue(typeof(Color), "255, 204, 153")]
        [Description("底部工具栏最小最大限制提示字体颜色")]
        [Editor(typeof(ColorEditorExt), typeof(System.Drawing.Design.UITypeEditor))]
        public Color BottomBarTipForeColor
        {
            get { return this.bottomBarTipForeColor; }
            set
            {
                if (this.bottomBarTipForeColor == value)
                    return;
                this.bottomBarTipForeColor = value;
            }
        }

        #endregion

        /// <summary>
        /// 控件默认大小
        /// </summary>
        [DefaultValue(typeof(Size), "226, 298")]
        [Description("控件默认大小")]
        protected override Size DefaultSize
        {
            get
            {
                return new Size(226, 268); ;
            }
        }

        /// <summary>
        /// 最小日期
        /// </summary>
        private static readonly DateTime minDate = new DateTime(1753, 1, 1).Date;
        /// <summary>
        /// 最大日期
        /// </summary>
        private static readonly DateTime maxDate = new DateTime(9998, 12, 31).Date;

        /// <summary>
        /// 顶部工具栏高度
        /// </summary>
        private readonly int topbar_rect_height = 36;
        /// <summary>
        /// 顶部工具栏字体
        /// </summary>
        private readonly Font topbar_rect_font = new Font("微软雅黑", 10);

        /// <summary>
        /// 日期面板宽度
        /// </summary>
        private readonly int date_rect_width = 226;
        /// <summary>
        /// 日期面板高度
        /// </summary>
        private readonly int date_rect_height = 226;
        /// <summary>
        /// 日期面板字体
        /// </summary>
        private readonly Font date_rect_font = new Font("微软雅黑", 10);
        /// <summary>
        /// 日期标题
        /// </summary>
        private readonly string[] days_titles = new string[] { "日", "一", "二", "三", "四", "五", "六" };
        /// <summary>
        /// 年面板年选项宽度
        /// </summary>
        private readonly int year_rectf_item_width = 66;
        /// <summary>
        /// 年面板年选项高度
        /// </summary>
        private readonly int year_rectf_item_height = 36;
        /// <summary>
        /// 月面板月选项宽度
        /// </summary>
        private readonly int month_rectf_item_width = 66;
        /// <summary>
        /// 月面板月选项高度
        /// </summary>
        private readonly int month_rectf_item_height = 36;
        /// <summary>
        /// 日面板月选项宽度
        /// </summary>
        private readonly int day_rectf_item_width = 30;
        /// <summary>
        /// 日面板月选项高度
        /// </summary>
        private readonly int day_rectf_item_height = 30;

        /// <summary>
        /// 底部工具栏高度
        /// </summary>
        private readonly int bottombar_rect_height = 36;
        /// <summary>
        /// 底部工具栏字体
        /// </summary>
        private readonly Font bottom_rect_font = new Font("微软雅黑", 10);
        /// <summary>
        /// 底部工具栏提示信息字体
        /// </summary>
        private readonly Font bottom_rect_tip_font = new Font("微软雅黑", 9f);

        /// <summary>
        /// 开始日期对象
        /// </summary>
        private DateClass DateObject;

        /// <summary>
        /// 画笔管理
        /// </summary>
        private SolidBrushManage SolidBrushManageObject;

        /// <summary>
        /// 顶部工具栏字体格式
        /// </summary>
        private readonly StringFormat top_sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        /// <summary>
        /// 日期面板字体格式
        /// </summary>
        private readonly StringFormat date_sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        /// <summary>
        /// 底部工具栏字体格式
        /// </summary>
        private readonly StringFormat bottom_sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        #endregion

        /// <summary>
        /// 创建默认的日历控件
        /// </summary>
        /// <returns></returns>
        public static DatePickerExt CreateDefaultDatePickExt()
        {
            var datePicker = new WinDoControls.Controls.DatePickerExt();
            datePicker.BottomBarBtnBackColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            datePicker.BottomBarBtnBackDisabledColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            datePicker.BottomBarBtnBackEnterColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            datePicker.DateBackSelectedColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            datePicker.DateNormalForeColor = ColorTranslator.FromHtml("#000000");
            datePicker.DatePastForeColor = WDColors.LinkColor;
            datePicker.TopBarBackColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            datePicker.MinMaxTip = false;
            return datePicker;
        }

        public DatePickerExt()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();

            this.BackColor = Color.FromArgb(255, 255, 255);

            this.SolidBrushManageObject = new SolidBrushManage(this);
            this.DateObject = new DateClass(this);
            this.InitializeControlRectangle();
            this.UpdateControlText();
        }

        #region 重写

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            RectangleF rectf = new RectangleF(g.ClipBounds.X, g.ClipBounds.Y, g.ClipBounds.Width, g.ClipBounds.Height);

            #region 背景

            SolidBrush back_sb = new SolidBrush(this.BackColor);
            g.FillRectangle(back_sb, rectf);
            back_sb.Dispose();

            #endregion
            #region 顶部工具栏
            this.DrawTopBar(g);
            #endregion
            #region 面板

            if (this.DateDisplayType == DateDisplayTypes.Year || (this.DateDisplayType == DateDisplayTypes.YearMonth && this.DateDisplayStatus == DateDisplayStatuss.YearMonth_Year) || (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Year))
            {
                this.DrawYear(g);
            }
            else if (this.DateDisplayType == DateDisplayTypes.YearMonth || (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Month))
            {
                this.DrawYearMonth(g);
            }
            else if (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.Default)
            {
                this.DrawYearMonthDay(g);
            }

            #endregion
            #region 底部工具栏
            //this.DrawBottomBar(g);
            #endregion
            using (var pen = new Pen(WDColors.geekblue6))
            {
                var brect = this.ClientRectangle;
                brect.Height = 267;
                brect.Width -= 1;
                e.Graphics.DrawRectangle(pen, brect);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.DateReadOnly)
                return;

            #region 顶部工具栏
            if (this.DateObject.DateTopBar.Rect.Contains(e.Location))
            {
                bool isenter = false;
                if (this.DateObject.DateTopBar.prev_year_btn.Rect.Contains(e.Location))
                {
                    this.DateObject.DateTopBar.prev_year_btn.MoveStatus = DateItemMoveStatuss.Enter;
                    isenter = true;
                }
                else
                {
                    this.DateObject.DateTopBar.prev_year_btn.MoveStatus = DateItemMoveStatuss.Normal;
                }
                if (this.DateObject.DateTopBar.prev_month_btn.Rect.Contains(e.Location) && this.DateDisplayType == DateDisplayTypes.YearMonthDay)
                {
                    this.DateObject.DateTopBar.prev_month_btn.MoveStatus = DateItemMoveStatuss.Enter;
                    isenter = true;
                }
                else
                {
                    this.DateObject.DateTopBar.prev_month_btn.MoveStatus = DateItemMoveStatuss.Normal;
                }
                if (this.DateObject.DateTopBar.month_btn.Rect.Contains(e.Location) && this.DateDisplayType == DateDisplayTypes.YearMonthDay)
                {
                    this.DateObject.DateTopBar.month_btn.MoveStatus = DateItemMoveStatuss.Enter;
                    isenter = true;
                }
                else
                {
                    this.DateObject.DateTopBar.month_btn.MoveStatus = DateItemMoveStatuss.Normal;
                }
                if (this.DateObject.DateTopBar.year_btn.Rect.Contains(e.Location) && (this.DateDisplayType == DateDisplayTypes.YearMonth || this.DateDisplayType == DateDisplayTypes.YearMonthDay))
                {
                    this.DateObject.DateTopBar.year_btn.MoveStatus = DateItemMoveStatuss.Enter;
                    isenter = true;
                }
                else
                {
                    this.DateObject.DateTopBar.year_btn.MoveStatus = DateItemMoveStatuss.Normal;
                }
                if (this.DateObject.DateTopBar.next_month_btn.Rect.Contains(e.Location) && this.DateDisplayType == DateDisplayTypes.YearMonthDay)
                {
                    this.DateObject.DateTopBar.next_month_btn.MoveStatus = DateItemMoveStatuss.Enter;
                    isenter = true;
                }
                else
                {
                    this.DateObject.DateTopBar.next_month_btn.MoveStatus = DateItemMoveStatuss.Normal;
                }
                if (this.DateObject.DateTopBar.next_year_btn.Rect.Contains(e.Location))
                {
                    this.DateObject.DateTopBar.next_year_btn.MoveStatus = DateItemMoveStatuss.Enter;
                    isenter = true;
                }
                else
                {
                    this.DateObject.DateTopBar.next_year_btn.MoveStatus = DateItemMoveStatuss.Normal;
                }
                this.Cursor = isenter ? Cursors.Hand : Cursors.Default;
            }
            else
            {
                this.DateObject.DateTopBar.prev_year_btn.MoveStatus = DateItemMoveStatuss.Normal;
                this.DateObject.DateTopBar.prev_month_btn.MoveStatus = DateItemMoveStatuss.Normal;
                this.DateObject.DateTopBar.month_btn.MoveStatus = DateItemMoveStatuss.Normal;
                this.DateObject.DateTopBar.year_btn.MoveStatus = DateItemMoveStatuss.Normal;
                this.DateObject.DateTopBar.next_month_btn.MoveStatus = DateItemMoveStatuss.Normal;
                this.DateObject.DateTopBar.next_year_btn.MoveStatus = DateItemMoveStatuss.Normal;
            }
            #endregion
            #region 日期面板
            if (this.DateObject.DateMain.Rect.Contains(e.Location))
            {
                bool isenter = false;
                if (this.DateDisplayType == DateDisplayTypes.Year || (this.DateDisplayType == DateDisplayTypes.YearMonth && this.DateDisplayStatus == DateDisplayStatuss.YearMonth_Year) || (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Year))
                {
                    for (int i = 0; i < this.DateObject.DateMain.yearItem.Length; i++)
                    {
                        if (this.DateObject.DateMain.yearItem[i].Rect.Contains(e.Location) && this.DateObject.DateMain.yearItem[i].ItemType == DateItemTypes.Normal)
                        {
                            this.DateObject.DateMain.yearItem[i].MoveStatus = DateItemMoveStatuss.Enter;
                            isenter = true;
                        }
                        else
                        {
                            this.DateObject.DateMain.yearItem[i].MoveStatus = DateItemMoveStatuss.Normal;
                        }
                    }
                }
                else if (this.DateDisplayType == DateDisplayTypes.YearMonth || (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Month))
                {
                    for (int i = 0; i < this.DateObject.DateMain.monthItem.Length; i++)
                    {
                        if (this.DateObject.DateMain.monthItem[i].Rect.Contains(e.Location) && this.DateObject.DateMain.monthItem[i].ItemType == DateItemTypes.Normal)
                        {
                            this.DateObject.DateMain.monthItem[i].MoveStatus = DateItemMoveStatuss.Enter;
                            isenter = true;
                        }
                        else
                        {
                            this.DateObject.DateMain.monthItem[i].MoveStatus = DateItemMoveStatuss.Normal;
                        }
                    }
                }
                else if (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.Default)
                {
                    for (int i = 0; i < this.DateObject.DateMain.dayItem.Length; i++)
                    {
                        if (this.DateObject.DateMain.dayItem[i].Rect.Contains(e.Location) && (this.DateObject.DateMain.dayItem[i].ItemType == DateItemTypes.Past || this.DateObject.DateMain.dayItem[i].ItemType == DateItemTypes.Normal || this.DateObject.DateMain.dayItem[i].ItemType == DateItemTypes.Future))
                        {
                            this.DateObject.DateMain.dayItem[i].MoveStatus = DateItemMoveStatuss.Enter;
                            isenter = true;
                        }
                        else
                        {
                            this.DateObject.DateMain.dayItem[i].MoveStatus = DateItemMoveStatuss.Normal;
                        }
                    }
                }
                this.Cursor = isenter ? Cursors.Hand : Cursors.Default;
            }
            else
            {
                if (this.DateDisplayType == DateDisplayTypes.Year || (this.DateDisplayType == DateDisplayTypes.YearMonth && this.DateDisplayStatus == DateDisplayStatuss.YearMonth_Year) || (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Year))
                    for (int i = 0; i < this.DateObject.DateMain.yearItem.Length; i++)
                    {
                        this.DateObject.DateMain.yearItem[i].MoveStatus = DateItemMoveStatuss.Normal;
                    }
                else if (this.DateDisplayType == DateDisplayTypes.YearMonth || (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Month))
                    for (int i = 0; i < this.DateObject.DateMain.monthItem.Length; i++)
                    {
                        this.DateObject.DateMain.monthItem[i].MoveStatus = DateItemMoveStatuss.Normal;
                    }
                else if (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.Default)
                    for (int i = 0; i < this.DateObject.DateMain.dayItem.Length; i++)
                    {
                        this.DateObject.DateMain.dayItem[i].MoveStatus = DateItemMoveStatuss.Normal;
                    }
            }
            #endregion
            #region  底部工具栏
            //if (this.DateObject.DateBottomBar.Rect.Contains(e.Location))
            //{
            //    bool isenter = false;
            //    //if (this.DateObject.DateBottomBar.bottombar_clear_btn.Rect.Contains(e.Location))
            //    //{
            //    //    this.DateObject.DateBottomBar.bottombar_clear_btn.MoveStatus = DateItemMoveStatuss.Enter;
            //    //    isenter = true;
            //    //}
            //    //else
            //    //{
            //    //    this.DateObject.DateBottomBar.bottombar_clear_btn.MoveStatus = DateItemMoveStatuss.Normal;
            //    //}
            //    if (this.DateObject.DateBottomBar.bottombar_now_btn.Rect.Contains(e.Location) && this.DateObject.DateBottomBar.bottombar_now_btn.ItemType == DateItemTypes.Normal)
            //    {
            //        this.DateObject.DateBottomBar.bottombar_now_btn.MoveStatus = DateItemMoveStatuss.Enter;
            //        isenter = true;
            //    }
            //    else
            //    {
            //        this.DateObject.DateBottomBar.bottombar_now_btn.MoveStatus = DateItemMoveStatuss.Normal;
            //    }
            //    //if (this.DateObject.DateBottomBar.bottombar_confirm_btn.Rect.Contains(e.Location))
            //    //{
            //    //    this.DateObject.DateBottomBar.bottombar_confirm_btn.MoveStatus = DateItemMoveStatuss.Enter;
            //    //    isenter = true;
            //    //}
            //    //else
            //    //{
            //    //    this.DateObject.DateBottomBar.bottombar_confirm_btn.MoveStatus = DateItemMoveStatuss.Normal;
            //    //}
            //    this.Cursor = isenter ? Cursors.Hand : Cursors.Default;
            //}
            //else
            //{
            //    this.DateObject.DateBottomBar.bottombar_clear_btn.MoveStatus = DateItemMoveStatuss.Normal;
            //    this.DateObject.DateBottomBar.bottombar_now_btn.MoveStatus = DateItemMoveStatuss.Normal;
            //    this.DateObject.DateBottomBar.bottombar_confirm_btn.MoveStatus = DateItemMoveStatuss.Normal;
            //}
            #endregion
        }

        //protected override void OnDoubleClick(EventArgs e)
        //{
        //    if (this.DateReadOnly)
        //        return;
        //    Point point = this.PointToClient(Control.MousePosition);
        //    if (this.DateObject.DateMain.Rect.Contains(point))
        //    {
        //        if ((this.DateDisplayType == DateDisplayTypes.Year && this.DateObject.year != -1) || (this.DateDisplayType == DateDisplayTypes.YearMonth && this.DateObject.year != -1 && this.DateObject.month != -1) || (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateObject.year != -1 && this.DateObject.month != -1 && this.DateObject.day != -1))
        //        {
        //            this.OnBottomBarConfirmClick(this, new BottomBarIiemEventArgs() { Item = this.DateObject.DateBottomBar.bottombar_confirm_btn });
        //            if (this.bottomBarConfirmClick != null)
        //                this.bottomBarConfirmClick(this, new BottomBarIiemEventArgs() { Item = this.DateObject.DateBottomBar.bottombar_confirm_btn });
        //        }
        //    }
        //    base.OnDoubleClick(e);
        //}
        protected override void OnClick(EventArgs e)
        {
            if (this.DateReadOnly)
                return;

            Point point = this.PointToClient(Control.MousePosition);
            #region 顶部工具栏
            if (this.DateObject.DateTopBar.Rect.Contains(point))
            {
                if (this.DateObject.DateTopBar.prev_year_btn.Rect.Contains(point))
                {
                    this.OnTopBarPrevYearClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.prev_year_btn });
                    if (this.topBarPrevYearClick != null)
                        this.topBarPrevYearClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.prev_year_btn });
                    this.UpdateControlText();
                    this.Invalidate();
                }
                else if (this.DateObject.DateTopBar.prev_month_btn.Rect.Contains(point))
                {
                    if (this.DateDisplayType == DateDisplayTypes.YearMonthDay)
                    {
                        this.OnTopBarPrevMonthClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.prev_month_btn });
                        if (this.topBarPrevMonthClick != null)
                            this.topBarPrevMonthClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.prev_month_btn });
                        this.UpdateControlText();
                        this.Invalidate();
                    }
                }
                else if (this.DateObject.DateTopBar.month_btn.Rect.Contains(point) && this.DateDisplayType == DateDisplayTypes.YearMonthDay)
                {
                    if (this.DateDisplayType == DateDisplayTypes.YearMonthDay)
                    {
                        this.OnTopBarMonthClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.month_btn });
                        if (this.topBarMonthClick != null)
                            this.topBarMonthClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.month_btn });
                        this.UpdateControlText();
                        this.Invalidate();
                    }
                }
                else if (this.DateObject.DateTopBar.year_btn.Rect.Contains(point) && (this.DateDisplayType == DateDisplayTypes.YearMonth || this.DateDisplayType == DateDisplayTypes.YearMonthDay))
                {
                    if (this.DateDisplayType == DateDisplayTypes.YearMonth || this.DateDisplayType == DateDisplayTypes.YearMonthDay)
                    {
                        this.OnTopBarYearClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.year_btn });
                        if (this.topBarYearClick != null)
                            this.topBarYearClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.year_btn });
                        this.UpdateControlText();
                        this.Invalidate();
                    }
                }
                else if (this.DateObject.DateTopBar.next_month_btn.Rect.Contains(point))
                {
                    if (this.DateDisplayType == DateDisplayTypes.YearMonthDay)
                    {
                        this.OnTopBarNextMonthClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.next_month_btn });
                        if (this.topBarNextMonthClick != null)
                            this.topBarNextMonthClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.next_month_btn });
                        this.UpdateControlText();
                        this.Invalidate();
                    }
                }
                else if (this.DateObject.DateTopBar.next_year_btn.Rect.Contains(point))
                {
                    this.OnTopBarNextYearClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.next_year_btn });
                    if (this.topBarNextYearClick != null)
                        this.topBarNextYearClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.next_year_btn });
                    this.UpdateControlText();
                    this.Invalidate();
                }
            }
            #endregion
            #region 日期面板
            else if (this.DateObject.DateMain.Rect.Contains(point))
            {
                if (this.DateDisplayType == DateDisplayTypes.Year || (this.DateDisplayType == DateDisplayTypes.YearMonth && this.DateDisplayStatus == DateDisplayStatuss.YearMonth_Year) || (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Year))
                {
                    for (int i = 0; i < this.DateObject.DateMain.yearItem.Length; i++)
                    {
                        if (this.DateObject.DateMain.yearItem[i].Rect.Contains(point) && this.DateObject.DateMain.yearItem[i].ItemType == DateItemTypes.Normal)
                        {
                            this.OnDateMainYearClick(this, new DateMainItemEventArgs() { Item = this.DateObject.DateMain.yearItem[i] });
                            if (this.dateMainYearClick != null)
                                this.dateMainYearClick(this, new DateMainItemEventArgs() { Item = this.DateObject.DateMain.yearItem[i] });
                            this.UpdateControlText();
                            this.Invalidate();
                        }
                    }
                }
                else if (this.DateDisplayType == DateDisplayTypes.YearMonth || (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Month))
                {
                    for (int i = 0; i < this.DateObject.DateMain.monthItem.Length; i++)
                    {
                        if (this.DateObject.DateMain.monthItem[i].Rect.Contains(point) && this.DateObject.DateMain.monthItem[i].ItemType == DateItemTypes.Normal)
                        {
                            this.OnDateMainMonthClick(this, new DateMainItemEventArgs() { Item = this.DateObject.DateMain.monthItem[i] });
                            if (this.dateMainMonthClick != null)
                                this.dateMainMonthClick(this, new DateMainItemEventArgs() { Item = this.DateObject.DateMain.monthItem[i] });
                            this.UpdateControlText();
                            this.Invalidate();
                        }
                    }
                }
                else if (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.Default)
                {
                    for (int i = 0; i < this.DateObject.DateMain.dayItem.Length; i++)
                    {
                        if (this.DateObject.DateMain.dayItem[i].Rect.Contains(point) && (this.DateObject.DateMain.dayItem[i].ItemType == DateItemTypes.Past || this.DateObject.DateMain.dayItem[i].ItemType == DateItemTypes.Normal || this.DateObject.DateMain.dayItem[i].ItemType == DateItemTypes.Future))
                        {
                            this.OnDateMainDayClick(this, new DateMainItemEventArgs() { Item = this.DateObject.DateMain.dayItem[i] });
                            if (this.dateMainDayClick != null)
                                this.dateMainDayClick(this, new DateMainItemEventArgs() { Item = this.DateObject.DateMain.dayItem[i] });

                            if (this.DateObject.DateMain.dayItem[i].ItemType == DateItemTypes.Past)
                            {
                                this.OnTopBarPrevMonthClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.prev_month_btn });
                                if (this.topBarPrevMonthClick != null)
                                    this.topBarPrevMonthClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.prev_month_btn });
                            }
                            else if (this.DateObject.DateMain.dayItem[i].ItemType == DateItemTypes.Future)
                            {
                                this.OnTopBarNextMonthClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.next_month_btn });
                                if (this.topBarNextMonthClick != null)
                                    this.topBarNextMonthClick(this, new TopBarIiemEventArgs() { Item = this.DateObject.DateTopBar.next_month_btn });
                            }
                            this.UpdateControlText();
                            this.Invalidate();
                            if ((this.DateDisplayType == DateDisplayTypes.Year && this.DateObject.year != -1) || (this.DateDisplayType == DateDisplayTypes.YearMonth && this.DateObject.year != -1 && this.DateObject.month != -1) || (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateObject.year != -1 && this.DateObject.month != -1 && this.DateObject.day != -1))
                            {
                                this.OnBottomBarConfirmClick(this, new BottomBarIiemEventArgs() { Item = this.DateObject.DateBottomBar.bottombar_confirm_btn });
                                if (this.bottomBarConfirmClick != null)
                                    this.bottomBarConfirmClick(this, new BottomBarIiemEventArgs() { Item = this.DateObject.DateBottomBar.bottombar_confirm_btn });
                            }
                        }
                    }
                }
            }
            #endregion
            #region  底部工具栏
            //else if (this.DateObject.DateBottomBar.Rect.Contains(point))
            //{
            //    //if (this.DateObject.DateBottomBar.bottombar_clear_btn.Rect.Contains(point))
            //    //{
            //    //    this.OnBottomBarClearClick(this, new BottomBarIiemEventArgs() { Item = this.DateObject.DateBottomBar.bottombar_clear_btn });
            //    //    if (this.bottomBarClearClick != null)
            //    //        this.bottomBarClearClick(this, new BottomBarIiemEventArgs() { Item = this.DateObject.DateBottomBar.bottombar_clear_btn });
            //    //}
            //    //else 
            //    if (this.DateObject.DateBottomBar.bottombar_now_btn.Rect.Contains(point) && this.DateObject.DateBottomBar.bottombar_now_btn.ItemType == DateItemTypes.Normal)
            //    {
            //        this.OnBottomBarNowClick(this, new BottomBarIiemEventArgs() { Item = this.DateObject.DateBottomBar.bottombar_now_btn });
            //        if (this.bottomBarNowClick != null)
            //            this.bottomBarNowClick(this, new BottomBarIiemEventArgs() { Item = this.DateObject.DateBottomBar.bottombar_now_btn });
            //    }
            //    else if (this.DateObject.DateBottomBar.bottombar_confirm_btn.Rect.Contains(point))
            //    {
            //        //if ((this.DateDisplayType == DateDisplayTypes.Year && this.DateObject.year != -1) || (this.DateDisplayType == DateDisplayTypes.YearMonth && this.DateObject.year != -1 && this.DateObject.month != -1) || (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateObject.year != -1 && this.DateObject.month != -1 && this.DateObject.day != -1))
            //        //{
            //        //    this.OnBottomBarConfirmClick(this, new BottomBarIiemEventArgs() { Item = this.DateObject.DateBottomBar.bottombar_confirm_btn });
            //        //    if (this.bottomBarConfirmClick != null)
            //        //        this.bottomBarConfirmClick(this, new BottomBarIiemEventArgs() { Item = this.DateObject.DateBottomBar.bottombar_confirm_btn });
            //        //}
            //    }
            //}
            #endregion

            base.OnClick(e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            width = this.date_rect_width;
            height = this.topbar_rect_height + this.date_rect_height + this.bottombar_rect_height;
            base.SetBoundsCore(x, y, width, height, specified);
            this.InitializeControlRectangle();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                if (this.SolidBrushManageObject != null)
                    this.SolidBrushManageObject.ReleaseSolidBrushs();
                if (this.topbar_rect_font != null)
                    this.topbar_rect_font.Dispose();
                if (this.date_rect_font != null)
                    this.date_rect_font.Dispose();
                if (this.bottom_rect_font != null)
                    this.bottom_rect_font.Dispose();
                if (this.bottom_rect_tip_font != null)
                    this.bottom_rect_tip_font.Dispose();
                if (this.top_sf != null)
                    this.top_sf.Dispose();
                if (this.date_sf != null)
                    this.date_sf.Dispose();
                if (this.bottom_sf != null)
                    this.bottom_sf.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region 虚方法
        #region 顶部工具栏
        /// <summary>
        /// 上一年
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnTopBarPrevYearClick(object sender, TopBarIiemEventArgs e)
        {
            if (this.DateDisplayType == DateDisplayTypes.Year)
            {
                e.Item.ower.display_year -= 12;
            }
            else if (this.DateDisplayType == DateDisplayTypes.YearMonth)
            {
                e.Item.ower.display_year -= 1;
            }
            else if (this.DateDisplayType == DateDisplayTypes.YearMonthDay)
            {
                e.Item.ower.display_year -= this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Year ? 12 : 1;
            }
        }
        /// <summary>
        /// 上一月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnTopBarPrevMonthClick(object sender, TopBarIiemEventArgs e)
        {
            if (this.DateDisplayType == DateDisplayTypes.YearMonthDay)
            {
                DateTime now = new DateTime(this.DateObject.display_year, this.DateObject.display_month, 1).AddMonths(-1);
                e.Item.ower.display_year = now.Year;
                e.Item.ower.display_month = now.Month;
            }
        }
        /// <summary>
        /// 显示月面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnTopBarMonthClick(object sender, TopBarIiemEventArgs e)
        {
            if (this.DateDisplayType == DateDisplayTypes.YearMonthDay)
            {
                if (this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Month)
                {
                    this.DateDisplayStatus = DateDisplayStatuss.Default;
                }
                else
                {
                    this.DateDisplayStatus = DateDisplayStatuss.YearMonthDay_Month;
                }
            }
        }
        /// <summary>
        /// 显示年面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnTopBarYearClick(object sender, TopBarIiemEventArgs e)
        {
            if (this.DateDisplayType == DateDisplayTypes.YearMonth)
            {
                if (this.DateDisplayStatus == DateDisplayStatuss.YearMonth_Year)
                {
                    this.DateDisplayStatus = DateDisplayStatuss.Default;
                }
                else
                {
                    this.DateDisplayStatus = DateDisplayStatuss.YearMonth_Year;
                }
            }
            else if (this.DateDisplayType == DateDisplayTypes.YearMonthDay)
            {
                if (this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Year)
                {
                    this.DateDisplayStatus = DateDisplayStatuss.Default;
                }
                else
                {
                    this.DateDisplayStatus = DateDisplayStatuss.YearMonthDay_Year;
                }
            }
        }
        /// <summary>
        /// 下一月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnTopBarNextMonthClick(object sender, TopBarIiemEventArgs e)
        {
            if (this.DateDisplayType == DateDisplayTypes.YearMonthDay)
            {
                DateTime now = new DateTime(this.DateObject.display_year, this.DateObject.display_month, 1).AddMonths(1);
                e.Item.ower.display_year = now.Year;
                e.Item.ower.display_month = now.Month;
            }
        }
        /// <summary>
        /// 下一年
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnTopBarNextYearClick(object sender, TopBarIiemEventArgs e)
        {
            if (this.DateDisplayType == DateDisplayTypes.Year)
            {
                e.Item.ower.display_year += 12;
            }
            else if (this.DateDisplayType == DateDisplayTypes.YearMonth)
            {
                e.Item.ower.display_year += 1;
            }
            else if (this.DateDisplayType == DateDisplayTypes.YearMonthDay)
            {
                e.Item.ower.display_year += this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Year ? 12 : 1;
            }
        }

        #endregion
        #region 日期面板
        /// <summary>
        /// 年选项单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnDateMainYearClick(object sender, DateMainItemEventArgs e)
        {
            if (this.DateDisplayType == DateDisplayTypes.Year)
            {
                this.DateObject.year = e.Item.Value.Year;
            }
            else if (this.DateDisplayType == DateDisplayTypes.YearMonth && this.DateDisplayStatus == DateDisplayStatuss.YearMonth_Year)
            {
                this.DateObject.display_year = e.Item.Value.Year;
                this.DateDisplayStatus = DateDisplayStatuss.Default;
            }
            else if (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Year)
            {
                this.DateObject.display_year = e.Item.Value.Year;
                this.DateDisplayStatus = DateDisplayStatuss.Default;
            }
        }
        /// <summary>
        /// 月选项单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnDateMainMonthClick(object sender, DateMainItemEventArgs e)
        {
            if (this.DateDisplayType == DateDisplayTypes.YearMonth)
            {
                this.DateObject.year = e.Item.Value.Year;
                this.DateObject.month = e.Item.Value.Month;
            }
            else if (this.DateDisplayType == DateDisplayTypes.YearMonthDay && this.DateDisplayStatus == DateDisplayStatuss.YearMonthDay_Month)
            {
                this.DateObject.display_year = e.Item.Value.Year;
                this.DateObject.display_month = e.Item.Value.Month;
                this.DateDisplayStatus = DateDisplayStatuss.Default;
            }
        }
        /// <summary>
        /// 日选项单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnDateMainDayClick(object sender, DateMainItemEventArgs e)
        {
            this.DateObject.year = e.Item.Value.Year;
            this.DateObject.month = e.Item.Value.Month;
            this.DateObject.day = e.Item.Value.Day;
        }
        #endregion
        #region 底部工具栏
        ///// <summary>
        ///// 清除
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void OnBottomBarClearClick(object sender, BottomBarIiemEventArgs e)
        //{
        //    this.DateObject.year = -1;
        //    this.DateObject.month = -1;
        //    this.DateObject.day = -1;
        //    this.Invalidate();
        //}
        ///// <summary>
        ///// 现在
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void OnBottomBarNowClick(object sender, BottomBarIiemEventArgs e)
        //{
        //    DateTime now = DateTime.Now;
        //    if (this.DateDisplayType == DateDisplayTypes.Year)
        //    {
        //        this.DateObject.year = now.Year;
        //        this.DateObject.display_year = now.Year;
        //        this.value = new DateTime(now.Year, 1, 1).Date;
        //    }
        //    if (this.DateDisplayType == DateDisplayTypes.YearMonth)
        //    {
        //        this.DateObject.year = now.Year;
        //        this.DateObject.display_year = now.Year;
        //        this.DateObject.month = now.Month;
        //        this.DateObject.display_month = now.Month;
        //        this.value = new DateTime(now.Year, now.Month, 1).Date;
        //    }
        //    if (this.DateDisplayType == DateDisplayTypes.YearMonthDay)
        //    {
        //        this.DateObject.year = now.Year;
        //        this.DateObject.display_year = now.Year;
        //        this.DateObject.month = now.Month;
        //        this.DateObject.display_month = now.Month;
        //        this.DateObject.day = now.Day;
        //        this.DateObject.display_day = now.Day;
        //        this.value = new DateTime(now.Year, now.Month, now.Day).Date;
        //    }
        //    this.UpdateControlText();
        //    this.Invalidate();
        //}
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnBottomBarConfirmClick(object sender, BottomBarIiemEventArgs e)
        {
            if (this.DateDisplayType == DateDisplayTypes.Year)
            {
                this.value = new DateTime(this.DateObject.year, 1, 1).Date;
            }
            if (this.DateDisplayType == DateDisplayTypes.YearMonth)
            {
                this.value = new DateTime(this.DateObject.year, this.DateObject.month, 1).Date;
            }
            if (this.DateDisplayType == DateDisplayTypes.YearMonthDay)
            {
                this.value = new DateTime(this.DateObject.year, this.DateObject.month, this.DateObject.day).Date;
            }
        }

        #endregion
        #endregion

        #region 绘制

        /// <summary>
        /// 绘制顶部工具栏
        /// </summary>
        /// <param name="g"></param>
        private void DrawTopBar(Graphics g)
        {
            //背景
            g.FillRectangle(this.SolidBrushManageObject.topbarback_sb, this.DateObject.DateTopBar.Rect);

            SmoothingMode sm = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            #region 上一年
            if (this.DateObject.DateTopBar.prev_year_btn.MoveStatus == DateItemMoveStatuss.Enter)
            {
                g.DrawLines(this.SolidBrushManageObject.topbarbtnfore_enter_pen, this.DateObject.DateTopBar.prev_year_btn.LineLeftPoint);
                g.DrawLines(this.SolidBrushManageObject.topbarbtnfore_enter_pen, this.DateObject.DateTopBar.prev_year_btn.LineRightPoint);
            }
            else
            {
                g.DrawLines(this.SolidBrushManageObject.topbarbtnfore_pen, this.DateObject.DateTopBar.prev_year_btn.LineLeftPoint);
                g.DrawLines(this.SolidBrushManageObject.topbarbtnfore_pen, this.DateObject.DateTopBar.prev_year_btn.LineRightPoint);
            }
            #endregion
            #region 上一月

            if (DateDisplayType == DateDisplayTypes.YearMonthDay)
            {
                if (this.DateObject.DateTopBar.prev_month_btn.MoveStatus == DateItemMoveStatuss.Enter)
                {
                    g.DrawLines(this.SolidBrushManageObject.topbarbtnfore_enter_pen, this.DateObject.DateTopBar.prev_month_btn.LineLeftPoint);
                }
                else
                {
                    g.DrawLines(this.SolidBrushManageObject.topbarbtnfore_pen, this.DateObject.DateTopBar.prev_month_btn.LineLeftPoint);
                }
            }
            #endregion
            #region 年面板
            if (this.DateDisplayType == DateDisplayTypes.Year)
            {
                g.DrawString(this.DateObject.DateTopBar.yearscope_btn.Text, this.topbar_rect_font, this.SolidBrushManageObject.topbarbtnfore_sb, this.DateObject.DateTopBar.yearscope_btn.Rect, this.top_sf);
            }
            #endregion
            #region 月面板
            else if (this.DateDisplayType == DateDisplayTypes.YearMonth)
            {
                g.DrawString(this.DateObject.DateTopBar.monthyear_btn.Text, this.topbar_rect_font, this.SolidBrushManageObject.topbarbtnfore_sb, this.DateObject.DateTopBar.monthyear_btn.Rect, this.top_sf);
            }
            #endregion
            #region 日面板
            else if (this.DateDisplayType == DateDisplayTypes.YearMonthDay)
            {
                g.DrawString(this.DateObject.DateTopBar.month_btn.Text, this.topbar_rect_font, this.SolidBrushManageObject.topbarbtnfore_sb, this.DateObject.DateTopBar.month_btn.Rect, this.top_sf);
                g.DrawString(this.DateObject.DateTopBar.year_btn.Text, this.topbar_rect_font, this.SolidBrushManageObject.topbarbtnfore_sb, this.DateObject.DateTopBar.year_btn.Rect, this.top_sf);
            }
            #endregion
            #region 下一月
            if (DateDisplayType == DateDisplayTypes.YearMonthDay)
            {
                if (this.DateObject.DateTopBar.next_month_btn.MoveStatus == DateItemMoveStatuss.Enter)
                {
                    g.DrawLines(this.SolidBrushManageObject.topbarbtnfore_enter_pen, this.DateObject.DateTopBar.next_month_btn.LineRightPoint);
                }
                else
                {
                    g.DrawLines(this.SolidBrushManageObject.topbarbtnfore_pen, this.DateObject.DateTopBar.next_month_btn.LineRightPoint);
                }
            }
            #endregion
            #region 下一年
            if (this.DateObject.DateTopBar.next_year_btn.MoveStatus == DateItemMoveStatuss.Enter)
            {
                g.DrawLines(this.SolidBrushManageObject.topbarbtnfore_enter_pen, this.DateObject.DateTopBar.next_year_btn.LineLeftPoint);
                g.DrawLines(this.SolidBrushManageObject.topbarbtnfore_enter_pen, this.DateObject.DateTopBar.next_year_btn.LineRightPoint);
            }
            else
            {
                g.DrawLines(this.SolidBrushManageObject.topbarbtnfore_pen, this.DateObject.DateTopBar.next_year_btn.LineLeftPoint);
                g.DrawLines(this.SolidBrushManageObject.topbarbtnfore_pen, this.DateObject.DateTopBar.next_year_btn.LineRightPoint);
            }
            #endregion

            g.SmoothingMode = sm;
        }

        /// <summary>
        /// 绘制年面板
        /// </summary>
        /// <param name="g"></param>
        private void DrawYear(Graphics g)
        {
            //背景
            g.FillRectangle(this.SolidBrushManageObject.dateback_sb, this.DateObject.DateMain.Rect);

            for (int i = 0; i < this.DateObject.DateMain.yearItem.Length; i++)
            {
                if (this.DateObject.year != -1)
                {
                    if (this.DateObject.DateMain.yearItem[i].Value.Year < this.MinValue.Year || this.DateObject.DateMain.yearItem[i].Value.Year > this.MaxValue.Year)
                    {
                        if (this.DateObject.DateMain.yearItem[i].Value.Year == this.DateObject.year)
                        {
                            g.FillRectangle(this.SolidBrushManageObject.backdisabled_sb, this.DateObject.DateMain.yearItem[i].Rect);
                        }
                        g.DrawString(this.DateObject.DateMain.yearItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.foredisabled_sb, this.DateObject.DateMain.yearItem[i].Rect, this.date_sf);
                    }
                    else if (this.DateObject.DateMain.yearItem[i].Value.Year == this.DateObject.year)
                    {
                        g.FillRectangle(this.SolidBrushManageObject.backselected_sb, this.DateObject.DateMain.yearItem[i].Rect);
                        g.DrawString(this.DateObject.DateMain.yearItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.foreselected_sb, this.DateObject.DateMain.yearItem[i].Rect, this.date_sf);
                    }
                    else
                    {
                        if (this.DateObject.DateMain.yearItem[i].MoveStatus == DateItemMoveStatuss.Enter)
                        {
                            g.FillRectangle(this.SolidBrushManageObject.backenter_sb, this.DateObject.DateMain.yearItem[i].Rect);
                        }
                        g.DrawString(this.DateObject.DateMain.yearItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.normalfore_sb, this.DateObject.DateMain.yearItem[i].Rect, this.date_sf);
                    }
                }
                else
                {
                    if (this.DateObject.DateMain.yearItem[i].MoveStatus == DateItemMoveStatuss.Enter)
                    {
                        g.FillRectangle(this.SolidBrushManageObject.backenter_sb, this.DateObject.DateMain.yearItem[i].Rect);
                    }
                    g.DrawString(this.DateObject.DateMain.yearItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.normalfore_sb, this.DateObject.DateMain.yearItem[i].Rect, this.date_sf);
                }
            }

        }

        /// <summary>
        /// 绘制月面板
        /// </summary>
        /// <param name="g"></param>
        private void DrawYearMonth(Graphics g)
        {
            //背景
            g.FillRectangle(this.SolidBrushManageObject.dateback_sb, this.DateObject.DateMain.Rect);

            DateTime min = new DateTime(this.MinValue.Year, this.MinValue.Month, 1);
            DateTime max = new DateTime(this.MaxValue.Year, this.MaxValue.Month, 1);
            for (int i = 0; i < this.DateObject.DateMain.monthItem.Length; i++)
            {
                if (this.DateObject.year != -1 && this.DateObject.month != -1)
                {
                    DateTime now = new DateTime(this.DateObject.year, this.DateObject.month, 1).Date;
                    if (this.DateObject.DateMain.monthItem[i].Value < min || this.DateObject.DateMain.monthItem[i].Value > max)
                    {
                        if (this.DateObject.DateMain.monthItem[i].Value == now)
                        {
                            g.FillRectangle(this.SolidBrushManageObject.backdisabled_sb, this.DateObject.DateMain.monthItem[i].Rect);
                        }
                        g.DrawString(this.DateObject.DateMain.monthItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.foredisabled_sb, this.DateObject.DateMain.monthItem[i].Rect, this.date_sf);
                    }
                    else if (this.DateObject.DateMain.monthItem[i].Value == now)
                    {
                        g.FillRectangle(this.SolidBrushManageObject.backselected_sb, this.DateObject.DateMain.monthItem[i].Rect);
                        g.DrawString(this.DateObject.DateMain.monthItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.foreselected_sb, this.DateObject.DateMain.monthItem[i].Rect, this.date_sf);
                    }
                    else
                    {
                        if (this.DateObject.DateMain.monthItem[i].MoveStatus == DateItemMoveStatuss.Enter)
                        {
                            g.FillRectangle(this.SolidBrushManageObject.backenter_sb, this.DateObject.DateMain.monthItem[i].Rect);
                        }
                        g.DrawString(this.DateObject.DateMain.monthItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.normalfore_sb, this.DateObject.DateMain.monthItem[i].Rect, this.date_sf);
                    }
                }
                else
                {
                    if (this.DateObject.DateMain.monthItem[i].MoveStatus == DateItemMoveStatuss.Enter)
                    {
                        g.FillRectangle(this.SolidBrushManageObject.backenter_sb, this.DateObject.DateMain.monthItem[i].Rect);
                    }
                    g.DrawString(this.DateObject.DateMain.monthItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.normalfore_sb, this.DateObject.DateMain.monthItem[i].Rect, this.date_sf);
                }
            }

        }

        /// <summary>
        /// 绘制日面板
        /// </summary>
        /// <param name="g"></param>
        private void DrawYearMonthDay(Graphics g)
        {
            //背景
            g.FillRectangle(this.SolidBrushManageObject.dateback_sb, this.DateObject.DateMain.Rect);

            for (int i = 0; i < 7; i++)
            {
                g.DrawString(this.DateObject.DateMain.dayItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.titlefore_sb, this.DateObject.DateMain.dayItem[i].Rect, this.date_sf);
            }

            bool isselect = (this.DateObject.year != -1 && this.DateObject.month != -1 && this.DateObject.day != -1);//是否有选中日期
            DateTime select_date = isselect ? new DateTime(this.DateObject.year, this.DateObject.month, this.DateObject.day).Date : new DateTime(0001, 1, 1).Date;

            for (int i = 7; i < this.DateObject.DateMain.dayItem.Length; i++)
            {
                if (this.DateObject.DateMain.dayItem[i].ItemType == DateItemTypes.Disabled)
                {
                    if (isselect && this.DateObject.DateMain.dayItem[i].Value == select_date)
                    {
                        g.FillRectangle(this.SolidBrushManageObject.backdisabled_sb, this.DateObject.DateMain.dayItem[i].Rect);
                    }
                    g.DrawString(this.DateObject.DateMain.dayItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.foredisabled_sb, this.DateObject.DateMain.dayItem[i].Rect, this.date_sf);
                }
                else if (isselect && this.DateObject.DateMain.dayItem[i].Value == select_date)
                {
                    g.FillRectangle(this.SolidBrushManageObject.backselected_sb, this.DateObject.DateMain.dayItem[i].Rect);
                    g.DrawString(this.DateObject.DateMain.dayItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.foreselected_sb, this.DateObject.DateMain.dayItem[i].Rect, this.date_sf);
                }
                else if (this.DateObject.DateMain.dayItem[i].ItemType == DateItemTypes.Past)
                {
                    if (this.DateObject.DateMain.dayItem[i].MoveStatus == DateItemMoveStatuss.Enter)
                    {
                        g.FillRectangle(this.SolidBrushManageObject.backenter_sb, this.DateObject.DateMain.dayItem[i].Rect);
                    }
                    g.DrawString(this.DateObject.DateMain.dayItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.pastfore_sb, this.DateObject.DateMain.dayItem[i].Rect, this.date_sf);
                }
                else if (this.DateObject.DateMain.dayItem[i].ItemType == DateItemTypes.Normal)
                {
                    if (this.DateObject.DateMain.dayItem[i].MoveStatus == DateItemMoveStatuss.Enter)
                    {
                        g.FillRectangle(this.SolidBrushManageObject.backenter_sb, this.DateObject.DateMain.dayItem[i].Rect);
                    }
                    g.DrawString(this.DateObject.DateMain.dayItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.normalfore_sb, this.DateObject.DateMain.dayItem[i].Rect, this.date_sf);
                }
                else if (this.DateObject.DateMain.dayItem[i].ItemType == DateItemTypes.Future)
                {
                    if (this.DateObject.DateMain.dayItem[i].MoveStatus == DateItemMoveStatuss.Enter)
                    {
                        g.FillRectangle(this.SolidBrushManageObject.backenter_sb, this.DateObject.DateMain.dayItem[i].Rect);
                    }
                    g.DrawString(this.DateObject.DateMain.dayItem[i].Text, this.date_rect_font, this.SolidBrushManageObject.futurefore_sb, this.DateObject.DateMain.dayItem[i].Rect, this.date_sf);
                }
            }

        }

        ///// <summary>
        ///// 绘制底部工具栏
        ///// </summary>
        ///// <param name="g"></param>
        //private void DrawBottomBar(Graphics g)
        //{
        //    //背景
        //    g.FillRectangle(this.SolidBrushManageObject.bottombarback_sb, this.DateObject.DateBottomBar.Rect);

        //    //边框
        //    g.DrawLine(this.SolidBrushManageObject.bottombarborder_pen, this.DateObject.DateBottomBar.Rect.X, this.DateObject.DateBottomBar.Rect.Y, this.DateObject.DateBottomBar.Rect.Right, this.DateObject.DateBottomBar.Rect.Y);
        //    if (this.MinMaxTip)
        //    {
        //        g.DrawLines(this.SolidBrushManageObject.bottombarborder_pen, this.DateObject.DateBottomBar.bottombar_minmaxborder_btn.LinePoint);
        //        g.DrawString(this.DateObject.DateBottomBar.bottombar_mindate_btn.Text, this.bottom_rect_tip_font, this.SolidBrushManageObject.bottombar_tip_sb, this.DateObject.DateBottomBar.bottombar_mindate_btn.Rect.Location);
        //        g.DrawString(this.DateObject.DateBottomBar.bottombar_maxdate_btn.Text, this.bottom_rect_tip_font, this.SolidBrushManageObject.bottombar_tip_sb, this.DateObject.DateBottomBar.bottombar_maxdate_btn.Rect.Location);

        //    }

        //    SmoothingMode sm = g.SmoothingMode;
        //    g.SmoothingMode = SmoothingMode.AntiAlias;

        //    SolidBrush confirm_btn_back_sb = this.DateObject.DateBottomBar.bottombar_confirm_btn.ItemType == DateItemTypes.Disabled ? this.SolidBrushManageObject.bottombarbtnbackdisabled_sb : (this.DateObject.DateBottomBar.bottombar_confirm_btn.MoveStatus == DateItemMoveStatuss.Enter ? this.SolidBrushManageObject.bottombarbtnbackenter_sb : this.SolidBrushManageObject.bottombarbtnback_sb);
        //    SolidBrush confirm_btn_fore_sb = this.DateObject.DateBottomBar.bottombar_confirm_btn.ItemType == DateItemTypes.Disabled ? this.SolidBrushManageObject.bottombarbtnforedisabled_sb : (this.DateObject.DateBottomBar.bottombar_confirm_btn.MoveStatus == DateItemMoveStatuss.Enter ? this.SolidBrushManageObject.bottombarbtnforeenter_sb : this.SolidBrushManageObject.bottombarbtnfore_sb);

        //    SolidBrush now_btn_back_sb = this.DateObject.DateBottomBar.bottombar_now_btn.ItemType == DateItemTypes.Disabled ? this.SolidBrushManageObject.bottombarbtnbackdisabled_sb : (this.DateObject.DateBottomBar.bottombar_now_btn.MoveStatus == DateItemMoveStatuss.Enter ? this.SolidBrushManageObject.bottombarbtnbackenter_sb : this.SolidBrushManageObject.bottombarbtnback_sb);
        //    SolidBrush now_btn_fore_sb = this.DateObject.DateBottomBar.bottombar_now_btn.ItemType == DateItemTypes.Disabled ? this.SolidBrushManageObject.bottombarbtnforedisabled_sb : (this.DateObject.DateBottomBar.bottombar_now_btn.MoveStatus == DateItemMoveStatuss.Enter ? this.SolidBrushManageObject.bottombarbtnforeenter_sb : this.SolidBrushManageObject.bottombarbtnfore_sb);

        //    //SolidBrush clear_btn_back_sb = this.DateObject.DateBottomBar.bottombar_clear_btn.ItemType == DateItemTypes.Disabled ? this.SolidBrushManageObject.bottombarbtnbackdisabled_sb : (this.DateObject.DateBottomBar.bottombar_clear_btn.MoveStatus == DateItemMoveStatuss.Enter ? this.SolidBrushManageObject.bottombarbtnbackenter_sb : this.SolidBrushManageObject.bottombarbtnback_sb);
        //    //SolidBrush clear_btn_fore_sb = this.DateObject.DateBottomBar.bottombar_clear_btn.ItemType == DateItemTypes.Disabled ? this.SolidBrushManageObject.bottombarbtnforedisabled_sb : (this.DateObject.DateBottomBar.bottombar_clear_btn.MoveStatus == DateItemMoveStatuss.Enter ? this.SolidBrushManageObject.bottombarbtnforeenter_sb : this.SolidBrushManageObject.bottombarbtnfore_sb);

        //    //g.FillPath(confirm_btn_back_sb, TransformCircular(this.DateObject.DateBottomBar.bottombar_confirm_btn.Rect, 0, 4, 4, 0));
        //    //g.DrawString(this.DateObject.DateBottomBar.bottombar_confirm_btn.Text, this.bottom_rect_font, confirm_btn_fore_sb, this.DateObject.DateBottomBar.bottombar_confirm_btn.Rect, this.bottom_sf);

        //    g.FillRectangle(now_btn_back_sb, this.DateObject.DateBottomBar.bottombar_now_btn.Rect);
        //    g.DrawString(this.DateObject.DateBottomBar.bottombar_now_btn.Text, this.bottom_rect_font, now_btn_fore_sb, this.DateObject.DateBottomBar.bottombar_now_btn.Rect, this.bottom_sf);

        //    //g.FillPath(clear_btn_back_sb, TransformCircular(this.DateObject.DateBottomBar.bottombar_clear_btn.Rect, 4, 0, 0, 4));
        //    //g.DrawString(this.DateObject.DateBottomBar.bottombar_clear_btn.Text, this.bottom_rect_font, clear_btn_fore_sb, this.DateObject.DateBottomBar.bottombar_clear_btn.Rect, this.bottom_sf);

        //    g.SmoothingMode = sm;
        //}

        #endregion

        #region 私有

        /// <summary>
        /// 初始化控件布局
        /// </summary>
        private void InitializeControlRectangle()
        {
            this.DateObject.DateTopBar.Rect = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.date_rect_width, this.topbar_rect_height);
            this.DateObject.DateMain.Rect = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y + this.topbar_rect_height, this.date_rect_width, this.date_rect_height);
            this.DateObject.DateBottomBar.Rect = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Bottom - this.bottombar_rect_height, this.ClientRectangle.Width, this.bottombar_rect_height);

            #region 顶部工具栏
            int topbar_btn_rectf_width = 24;
            float topbar_avg_w = topbar_btn_rectf_width / 3f;
            float topbar_avg_h = this.DateObject.DateTopBar.Rect.Height / 6f;

            #region 上一年
            this.DateObject.DateTopBar.prev_year_btn.Rect = new Rectangle(this.DateObject.DateTopBar.Rect.X, this.DateObject.DateTopBar.Rect.Y, topbar_btn_rectf_width, this.DateObject.DateTopBar.Rect.Height);
            this.DateObject.DateTopBar.prev_year_btn.LineLeftPoint = new Point[3];
            this.DateObject.DateTopBar.prev_year_btn.LineLeftPoint[0] = new Point((int)(this.DateObject.DateTopBar.prev_year_btn.Rect.X + topbar_avg_w * 2 - 3), (int)(this.DateObject.DateTopBar.prev_year_btn.Rect.Y + topbar_avg_h * 2));
            this.DateObject.DateTopBar.prev_year_btn.LineLeftPoint[1] = new Point((int)(this.DateObject.DateTopBar.prev_year_btn.Rect.X + topbar_avg_w), (int)(this.DateObject.DateTopBar.prev_year_btn.Rect.Y + topbar_avg_h * 3));
            this.DateObject.DateTopBar.prev_year_btn.LineLeftPoint[2] = new Point((int)(this.DateObject.DateTopBar.prev_year_btn.Rect.X + topbar_avg_w * 2 - 3), (int)(this.DateObject.DateTopBar.prev_year_btn.Rect.Y + topbar_avg_h * 4));

            this.DateObject.DateTopBar.prev_year_btn.LineRightPoint = new Point[3];
            this.DateObject.DateTopBar.prev_year_btn.LineRightPoint[0] = new Point((int)(this.DateObject.DateTopBar.prev_year_btn.Rect.X + topbar_avg_w * 2), (int)(this.DateObject.DateTopBar.prev_year_btn.Rect.Y + topbar_avg_h * 2));
            this.DateObject.DateTopBar.prev_year_btn.LineRightPoint[1] = new Point((int)(this.DateObject.DateTopBar.prev_year_btn.Rect.X + topbar_avg_w + 3), (int)(this.DateObject.DateTopBar.prev_year_btn.Rect.Y + topbar_avg_h * 3));
            this.DateObject.DateTopBar.prev_year_btn.LineRightPoint[2] = new Point((int)(this.DateObject.DateTopBar.prev_year_btn.Rect.X + topbar_avg_w * 2), (int)(this.DateObject.DateTopBar.prev_year_btn.Rect.Y + topbar_avg_h * 4));
            #endregion
            #region 上一月
            this.DateObject.DateTopBar.prev_month_btn.Rect = new Rectangle(this.DateObject.DateTopBar.Rect.X + topbar_btn_rectf_width, this.DateObject.DateTopBar.Rect.Y, topbar_btn_rectf_width, this.DateObject.DateTopBar.Rect.Height);

            this.DateObject.DateTopBar.prev_month_btn.LineLeftPoint = new Point[3];
            this.DateObject.DateTopBar.prev_month_btn.LineLeftPoint[0] = new Point((int)(this.DateObject.DateTopBar.prev_month_btn.Rect.X + topbar_avg_w * 2 - 3), (int)(this.DateObject.DateTopBar.prev_month_btn.Rect.Y + topbar_avg_h * 2));
            this.DateObject.DateTopBar.prev_month_btn.LineLeftPoint[1] = new Point((int)(this.DateObject.DateTopBar.prev_month_btn.Rect.X + topbar_avg_w), (int)(this.DateObject.DateTopBar.prev_month_btn.Rect.Y + topbar_avg_h * 3));
            this.DateObject.DateTopBar.prev_month_btn.LineLeftPoint[2] = new Point((int)(this.DateObject.DateTopBar.prev_month_btn.Rect.X + topbar_avg_w * 2 - 3), (int)(this.DateObject.DateTopBar.prev_month_btn.Rect.Y + topbar_avg_h * 4));
            #endregion
            this.DateObject.DateTopBar.yearscope_btn.Rect = new Rectangle(this.DateObject.DateTopBar.Rect.X + (this.DateObject.DateTopBar.Rect.Width - 120) / 2, this.DateObject.DateTopBar.Rect.Y, 120, this.DateObject.DateTopBar.Rect.Height);
            this.DateObject.DateTopBar.monthyear_btn.Rect = new Rectangle(this.DateObject.DateTopBar.Rect.X + (this.DateObject.DateTopBar.Rect.Width - 60) / 2, this.DateObject.DateTopBar.Rect.Y, 60, this.DateObject.DateTopBar.Rect.Height);
            this.DateObject.DateTopBar.month_btn.Rect = new Rectangle(this.DateObject.DateTopBar.Rect.X + (this.DateObject.DateTopBar.Rect.Width - (37 + 60)) / 2, this.DateObject.DateTopBar.Rect.Y, 37, this.DateObject.DateTopBar.Rect.Height);
            this.DateObject.DateTopBar.year_btn.Rect = new Rectangle(this.DateObject.DateTopBar.Rect.X + (this.DateObject.DateTopBar.Rect.Width - (37 + 60)) / 2 + 37, this.DateObject.DateTopBar.Rect.Y, 60, this.DateObject.DateTopBar.Rect.Height);
            #region 下一月
            this.DateObject.DateTopBar.next_month_btn.Rect = new Rectangle(this.DateObject.DateTopBar.Rect.Right - topbar_btn_rectf_width - topbar_btn_rectf_width, this.DateObject.DateTopBar.Rect.Y, topbar_btn_rectf_width, this.DateObject.DateTopBar.Rect.Height);

            this.DateObject.DateTopBar.next_month_btn.LineRightPoint = new Point[3];
            this.DateObject.DateTopBar.next_month_btn.LineRightPoint[0] = new Point((int)(this.DateObject.DateTopBar.next_month_btn.Rect.Right - topbar_avg_w * 2 + 3), (int)(this.DateObject.DateTopBar.next_month_btn.Rect.Y + topbar_avg_h * 2));
            this.DateObject.DateTopBar.next_month_btn.LineRightPoint[1] = new Point((int)(this.DateObject.DateTopBar.next_month_btn.Rect.Right - topbar_avg_w), (int)(this.DateObject.DateTopBar.next_month_btn.Rect.Y + topbar_avg_h * 3));
            this.DateObject.DateTopBar.next_month_btn.LineRightPoint[2] = new Point((int)(this.DateObject.DateTopBar.next_month_btn.Rect.Right - topbar_avg_w * 2 + 3), (int)(this.DateObject.DateTopBar.next_month_btn.Rect.Y + topbar_avg_h * 4));
            #endregion
            #region 下一年
            this.DateObject.DateTopBar.next_year_btn.Rect = new Rectangle(this.DateObject.DateTopBar.Rect.Right - topbar_btn_rectf_width, this.DateObject.DateTopBar.Rect.Y, topbar_btn_rectf_width, this.DateObject.DateTopBar.Rect.Height);

            this.DateObject.DateTopBar.next_year_btn.LineLeftPoint = new Point[3];
            this.DateObject.DateTopBar.next_year_btn.LineLeftPoint[0] = new Point((int)(this.DateObject.DateTopBar.next_year_btn.Rect.Right - topbar_avg_w * 2 + 3), (int)(this.DateObject.DateTopBar.next_year_btn.Rect.Y + topbar_avg_h * 2));
            this.DateObject.DateTopBar.next_year_btn.LineLeftPoint[1] = new Point((int)(this.DateObject.DateTopBar.next_year_btn.Rect.Right - topbar_avg_w), (int)(this.DateObject.DateTopBar.next_year_btn.Rect.Y + topbar_avg_h * 3));
            this.DateObject.DateTopBar.next_year_btn.LineLeftPoint[2] = new Point((int)(this.DateObject.DateTopBar.next_year_btn.Rect.Right - topbar_avg_w * 2 + 3), (int)(this.DateObject.DateTopBar.next_year_btn.Rect.Y + topbar_avg_h * 4));

            this.DateObject.DateTopBar.next_year_btn.LineRightPoint = new Point[3];
            this.DateObject.DateTopBar.next_year_btn.LineRightPoint[0] = new Point((int)(this.DateObject.DateTopBar.next_year_btn.Rect.Right - topbar_avg_w * 2), (int)(this.DateObject.DateTopBar.next_year_btn.Rect.Y + topbar_avg_h * 2));
            this.DateObject.DateTopBar.next_year_btn.LineRightPoint[1] = new Point((int)(this.DateObject.DateTopBar.next_year_btn.Rect.Right - topbar_avg_w - 3), (int)(this.DateObject.DateTopBar.next_year_btn.Rect.Y + topbar_avg_h * 3));
            this.DateObject.DateTopBar.next_year_btn.LineRightPoint[2] = new Point((int)(this.DateObject.DateTopBar.next_year_btn.Rect.Right - topbar_avg_w * 2), (int)(this.DateObject.DateTopBar.next_year_btn.Rect.Y + topbar_avg_h * 4));
            #endregion
            #endregion
            #region 年面板
            int year_col = 3;
            int year_row = 4;
            float year_space_width = (this.date_rect_width - this.year_rectf_item_width * year_col) / (float)(year_col + 1);
            float year_space_height = (this.date_rect_height - this.year_rectf_item_height * year_row) / (float)(year_row + 1);
            for (int i = 0; i < this.DateObject.DateMain.yearItem.Length; i++)
            {
                float x = this.DateObject.DateMain.Rect.X + year_space_width + (i % year_col) * (this.year_rectf_item_width + year_space_width);
                float y = this.DateObject.DateMain.Rect.Y + year_space_height + (i / year_col) * (this.year_rectf_item_height + year_space_height);
                this.DateObject.DateMain.yearItem[i].Rect = new Rectangle((int)x, (int)y, this.year_rectf_item_width, this.year_rectf_item_height);
            }
            #endregion
            #region 月面板
            int month_col = 3;
            int month_row = 4;
            float month_space_width = (this.date_rect_width - this.month_rectf_item_width * month_col) / (float)(month_col + 1);
            float month_space_height = (this.date_rect_height - this.month_rectf_item_height * month_row) / (float)(month_row + 1);
            for (int i = 0; i < this.DateObject.DateMain.monthItem.Length; i++)
            {
                float x = this.DateObject.DateMain.Rect.X + month_space_width + (i % month_col) * (this.month_rectf_item_width + month_space_width);
                float y = this.DateObject.DateMain.Rect.Y + month_space_height + (i / month_col) * (this.month_rectf_item_height + month_space_height);
                this.DateObject.DateMain.monthItem[i].Rect = new Rectangle((int)x, (int)y, this.month_rectf_item_width, this.month_rectf_item_height);
            }
            #endregion
            #region 日面板
            int days = DateTime.DaysInMonth(this.DateObject.year, this.DateObject.month);//指定月份的总天数
            DateTime first_day = new DateTime(this.DateObject.year, this.DateObject.month, 1);//指定年月的第一天
            int week_day = (int)(first_day.DayOfWeek);//指定日期为星期几
            if (week_day == 0)
                week_day = 7;

            int day_col = 7;
            int day_row = 7;

            float day_space_width = 1;
            float day_space_height = 1;
            float day_space_left = (this.date_rect_width - this.day_rectf_item_width * day_col - day_space_width * (day_col - 1)) / 2f;
            float day_space_top = (this.date_rect_height - this.day_rectf_item_height * day_row - day_space_height * (day_row - 1)) / 2f;

            for (int i = 0; i < this.DateObject.DateMain.dayItem.Length; i++)
            {
                float x = this.DateObject.DateMain.Rect.X + day_space_left + (i % day_col) * (this.day_rectf_item_width + day_space_width);
                float y = this.DateObject.DateMain.Rect.Y + day_space_top + (i / day_col) * (this.day_rectf_item_height + day_space_height);
                this.DateObject.DateMain.dayItem[i].Rect = new Rectangle((int)x, (int)y, this.day_rectf_item_width, this.day_rectf_item_height);

                if (i < day_col)//绘制标题
                {
                    this.DateObject.DateMain.dayItem[i].ItemType = DateItemTypes.Title;
                    this.DateObject.DateMain.dayItem[i].Value = DateTime.MinValue;
                    this.DateObject.DateMain.dayItem[i].Text = this.days_titles[i];
                }
                else if (i - 7 < week_day)//绘制指定月份上一个月的日期
                {
                    int day = -(week_day - (i - 7));
                    this.DateObject.DateMain.dayItem[i].ItemType = DateItemTypes.Past;
                    this.DateObject.DateMain.dayItem[i].Value = first_day.AddDays(day);
                    this.DateObject.DateMain.dayItem[i].Text = this.DateObject.DateMain.dayItem[i].Value.Day.ToString();
                }
                else if (i - 7 - week_day < days)//绘制指定月份的日期
                {
                    int day = i - 7 - week_day;
                    this.DateObject.DateMain.dayItem[i].ItemType = DateItemTypes.Normal;
                    this.DateObject.DateMain.dayItem[i].Value = first_day.AddDays(day);
                    this.DateObject.DateMain.dayItem[i].Text = this.DateObject.DateMain.dayItem[i].Value.Day.ToString();
                }
                else//绘制指定月份下一个月的日期
                {
                    int day = i - 7 - week_day;
                    this.DateObject.DateMain.dayItem[i].ItemType = DateItemTypes.Future;
                    this.DateObject.DateMain.dayItem[i].Value = first_day.AddDays(day);
                    this.DateObject.DateMain.dayItem[i].Text = this.DateObject.DateMain.dayItem[i].Value.Day.ToString();
                }
            }

            #endregion
            #region 底部工具栏
            int bottombar_btn_rectf_width = 40;
            int bottombar_btn_rectf_height = 28;
            int bottombar_space_width = 2;
            int bottombar_space_right = 5;
            int bottombar_btn_y = this.DateObject.DateBottomBar.Rect.Y + (this.DateObject.DateBottomBar.Rect.Height - bottombar_btn_rectf_height) / 2;

            int bottombar_tip_border_width = 5;
            this.DateObject.DateBottomBar.bottombar_minmaxborder_btn.LinePoint = new Point[4];
            this.DateObject.DateBottomBar.bottombar_minmaxborder_btn.LinePoint[0] = new Point(this.DateObject.DateBottomBar.Rect.X + 5 + bottombar_tip_border_width, this.DateObject.DateBottomBar.Rect.Y + 10);
            this.DateObject.DateBottomBar.bottombar_minmaxborder_btn.LinePoint[1] = new Point(this.DateObject.DateBottomBar.Rect.X + 5, this.DateObject.DateBottomBar.Rect.Y + 10);
            this.DateObject.DateBottomBar.bottombar_minmaxborder_btn.LinePoint[2] = new Point(this.DateObject.DateBottomBar.Rect.X + 5, this.DateObject.DateBottomBar.Rect.Bottom - 10);
            this.DateObject.DateBottomBar.bottombar_minmaxborder_btn.LinePoint[3] = new Point(this.DateObject.DateBottomBar.Rect.X + 5 + bottombar_tip_border_width, this.DateObject.DateBottomBar.Rect.Bottom - 10);

            this.DateObject.DateBottomBar.bottombar_mindate_btn.Rect = new Rectangle(this.DateObject.DateBottomBar.Rect.X + 5 + bottombar_tip_border_width, this.DateObject.DateBottomBar.Rect.Y + 2, 0, 0);
            this.DateObject.DateBottomBar.bottombar_maxdate_btn.Rect = new Rectangle(this.DateObject.DateBottomBar.Rect.X + 5 + bottombar_tip_border_width, this.DateObject.DateBottomBar.Rect.Bottom - 18, 0, 0);

            this.DateObject.DateBottomBar.bottombar_confirm_btn.Rect = new Rectangle(this.DateObject.DateBottomBar.Rect.Right - bottombar_space_right - bottombar_btn_rectf_width, bottombar_btn_y, bottombar_btn_rectf_width, bottombar_btn_rectf_height);
            this.DateObject.DateBottomBar.bottombar_now_btn.Rect = new Rectangle(this.DateObject.DateBottomBar.bottombar_confirm_btn.Rect.X - bottombar_space_width - bottombar_btn_rectf_width, bottombar_btn_y, bottombar_btn_rectf_width, bottombar_btn_rectf_height);
            this.DateObject.DateBottomBar.bottombar_clear_btn.Rect = new Rectangle(this.DateObject.DateBottomBar.bottombar_now_btn.Rect.X - bottombar_space_width - bottombar_btn_rectf_width, bottombar_btn_y, bottombar_btn_rectf_width, bottombar_btn_rectf_height);

            #endregion
        }

        /// <summary>
        /// 更新画面信息
        /// </summary>
        private void UpdateControlText()
        {
            int start_year = getStartYaer(this.DateObject.display_year);
            #region 顶部工具栏
            this.DateObject.DateTopBar.yearscope_btn.Text = String.Format("{0}年~{1}年", start_year.ToString().PadLeft(4, '0'), (start_year + 11).ToString().PadLeft(4, '0'));
            this.DateObject.DateTopBar.monthyear_btn.Text = this.DateObject.display_year + "年";
            this.DateObject.DateTopBar.month_btn.Text = String.Format("{0}月", this.DateObject.display_month.ToString().PadLeft(2, '0'));
            this.DateObject.DateTopBar.year_btn.Text = String.Format("{0}年", this.DateObject.display_year.ToString().PadLeft(4, '0'));
            #endregion
            #region 年面板
            for (int i = 0; i < this.DateObject.DateMain.yearItem.Length; i++)
            {
                DateTime now = new DateTime(start_year + i, 1, 1).Date;
                this.DateObject.DateMain.yearItem[i].Value = now;
                this.DateObject.DateMain.yearItem[i].Text = String.Format("{0}年", now.ToString("yyyy"));
                this.DateObject.DateMain.yearItem[i].MoveStatus = DateItemMoveStatuss.Normal;
                this.DateObject.DateMain.yearItem[i].ItemType = (now.Year < this.MinValue.Year || now.Year > this.MaxValue.Year) ? DateItemTypes.Disabled : DateItemTypes.Normal;
            }
            #endregion
            #region 月面板
            DateTime month_min = new DateTime(this.MinValue.Year, MinValue.Month, 1).Date;
            DateTime month_max = new DateTime(this.MaxValue.Year, MaxValue.Month, 1).Date;
            for (int i = 0; i < this.DateObject.DateMain.monthItem.Length; i++)
            {
                DateTime now = new DateTime(this.DateObject.display_year, i + 1, 1).Date;
                this.DateObject.DateMain.monthItem[i].Value = now;
                this.DateObject.DateMain.monthItem[i].Text = String.Format("{0}月", now.ToString("MM"));
                this.DateObject.DateMain.monthItem[i].MoveStatus = DateItemMoveStatuss.Normal;
                this.DateObject.DateMain.monthItem[i].ItemType = (now < month_min || now > month_max) ? DateItemTypes.Disabled : DateItemTypes.Normal;
            }
            #endregion
            #region 日面板
            int days = DateTime.DaysInMonth(this.DateObject.display_year, this.DateObject.display_month);//指定月份的总天数
            DateTime first_day = new DateTime(this.DateObject.display_year, this.DateObject.display_month, 1).Date;//指定年月的第一天
            int week_day = (int)(first_day.DayOfWeek);//指定日期为星期几
            if (week_day == 0)
                week_day = 7;

            DateTime year_min = this.MinValue.Date;
            DateTime year_max = this.MaxValue.Date;
            for (int i = 0; i < this.DateObject.DateMain.dayItem.Length; i++)
            {
                if (i < 7)//绘制标题
                {
                    this.DateObject.DateMain.dayItem[i].Value = DateTime.MinValue.Date;
                    this.DateObject.DateMain.dayItem[i].Text = this.days_titles[i];
                    this.DateObject.DateMain.dayItem[i].MoveStatus = DateItemMoveStatuss.Normal;
                    this.DateObject.DateMain.dayItem[i].ItemType = DateItemTypes.Title;
                }
                else if (i - 7 < week_day)//绘制指定月份上一个月的日期
                {
                    int day = -(week_day - (i - 7));
                    DateTime now = first_day.AddDays(day);
                    this.DateObject.DateMain.dayItem[i].Value = now;
                    this.DateObject.DateMain.dayItem[i].Text = now.Day.ToString();
                    this.DateObject.DateMain.dayItem[i].MoveStatus = DateItemMoveStatuss.Normal;
                    this.DateObject.DateMain.dayItem[i].ItemType = (now < year_min || now > year_max) ? DateItemTypes.Disabled : DateItemTypes.Past;
                }
                else if (i - 7 - week_day < days)//绘制指定月份的日期
                {
                    int day = i - 7 - week_day;
                    DateTime now = first_day.AddDays(day).Date;
                    this.DateObject.DateMain.dayItem[i].Value = now;
                    this.DateObject.DateMain.dayItem[i].Text = now.Day.ToString();
                    this.DateObject.DateMain.dayItem[i].MoveStatus = DateItemMoveStatuss.Normal;
                    this.DateObject.DateMain.dayItem[i].ItemType = (now < year_min || now > year_max) ? DateItemTypes.Disabled : DateItemTypes.Normal;
                }
                else//绘制指定月份下一个月的日期
                {
                    int day = i - 7 - week_day;
                    DateTime now = first_day.AddDays(day).Date;
                    this.DateObject.DateMain.dayItem[i].Value = now;
                    this.DateObject.DateMain.dayItem[i].Text = now.Day.ToString();
                    this.DateObject.DateMain.dayItem[i].MoveStatus = DateItemMoveStatuss.Normal;
                    this.DateObject.DateMain.dayItem[i].ItemType = (now < year_min || now > year_max) ? DateItemTypes.Disabled : DateItemTypes.Future;
                }
            }
            #endregion
            #region 底部工具栏
            string strformat = this.DateDisplayType == DateDisplayTypes.Year ? "yyyy" : (this.DateDisplayType == DateDisplayTypes.YearMonth ? "yyyy-MM" : "yyyy-MM-dd");
            this.DateObject.DateBottomBar.bottombar_mindate_btn.Text = this.MinValue.ToString(strformat);
            this.DateObject.DateBottomBar.bottombar_maxdate_btn.Text = this.MaxValue.ToString(strformat);

            this.DateObject.DateBottomBar.bottombar_confirm_btn.MoveStatus = DateItemMoveStatuss.Normal;
            this.DateObject.DateBottomBar.bottombar_confirm_btn.ItemType = this.dateReadOnly ? DateItemTypes.Disabled : DateItemTypes.Normal;

            this.DateObject.DateBottomBar.bottombar_now_btn.MoveStatus = DateItemMoveStatuss.Normal;
            this.DateObject.DateBottomBar.bottombar_now_btn.ItemType = (this.dateReadOnly || (DateTime.Now.Date < this.MinValue || DateTime.Now.Date > this.MaxValue)) ? DateItemTypes.Disabled : DateItemTypes.Normal;

            this.DateObject.DateBottomBar.bottombar_clear_btn.MoveStatus = DateItemMoveStatuss.Normal;
            this.DateObject.DateBottomBar.bottombar_clear_btn.ItemType = this.dateReadOnly ? DateItemTypes.Disabled : DateItemTypes.Normal;
            #endregion
        }

        /// <summary>
        /// 获取指定年份的年面板第一个开始的年份(以2010年为第一格)
        /// </summary>
        /// <param name="year">指定年份</param>
        /// <returns></returns>
        private int getStartYaer(int year)
        {
            if (year > 2010)
            {
                return 2010 + ((year - 2010) / 12) * 12;
            }
            if (year < 2010)
            {
                int remainder = (2010 - year) % 12 == 0 ? 0 : 1;
                return 2010 - ((2010 - year) / 12 + remainder) * 12;
            }
            return year;
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
        private static GraphicsPath TransformCircular(RectangleF rectf, int leftTopRadius = 0, int rightTopRadius = 0, int rightBottomRadius = 0, int leftBottomRadius = 0)
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

        #endregion

        #region 实体
        /// <summary>
        /// 日期面板
        /// </summary>
        [Description("日期面板")]
        public class DateClass
        {
            private DatePickerExt parent;

            public DateClass(DatePickerExt parent)
            {
                this.parent = parent;

                this.DateTopBar = new DateTopBarClass(parent, this);
                this.DateMain = new DateMainClass(parent, this);
                this.DateBottomBar = new BottomBarClass(parent, this);
            }

            /// <summary>
            /// 日期面板顶部工具栏
            /// </summary>
            public DateTopBarClass DateTopBar;
            /// <summary>
            /// 日期面板
            /// </summary>
            public DateMainClass DateMain;
            /// <summary>
            /// 底部工具栏
            /// </summary>
            public BottomBarClass DateBottomBar;

            /// <summary>
            /// 年份选择(已选择值)
            /// </summary>
            public int year = 1005;
            /// <summary>
            /// 月份选择(已选择值)
            /// </summary>
            public int month = 1;
            /// <summary>
            /// 日选择(已选择值)
            /// </summary>
            public int day = 1;

            /// <summary>
            /// 年份选择(用于显示)
            /// </summary>
            public int display_year = 1005;
            /// <summary>
            /// 月份选择(用于显示)
            /// </summary>
            public int display_month = 1;
            /// <summary>
            /// 日选择(用于显示)
            /// </summary>
            public int display_day = 1;

        }

        /// <summary>
        /// 日期面板顶部工具栏
        /// </summary>
        [Description("日期面板顶部工具栏")]
        public class DateTopBarClass
        {
            public DatePickerExt parent;
            public DateClass ower;

            public DateTopBarClass(DatePickerExt parent, DateClass ower)
            {
                this.parent = parent;
                this.ower = ower;

                this.prev_year_btn = new DateTopBarItemClass(parent, ower);
                this.prev_month_btn = new DateTopBarItemClass(parent, ower);
                this.yearscope_btn = new DateTopBarItemClass(parent, ower);
                this.monthyear_btn = new DateTopBarItemClass(parent, ower);
                this.month_btn = new DateTopBarItemClass(parent, ower);
                this.year_btn = new DateTopBarItemClass(parent, ower);
                this.next_month_btn = new DateTopBarItemClass(parent, ower);
                this.next_year_btn = new DateTopBarItemClass(parent, ower);
            }

            /// <summary>
            /// 日期面板顶部工具栏rect信息
            /// </summary>
            public Rectangle Rect;

            /// <summary>
            /// 上一年按钮
            /// </summary>
            public DateTopBarItemClass prev_year_btn;
            /// <summary>
            /// 上一月按钮
            /// </summary>
            public DateTopBarItemClass prev_month_btn;
            /// <summary>
            /// 年范围描述
            /// </summary>
            public DateTopBarItemClass yearscope_btn;
            /// <summary>
            /// 月面板年按钮
            /// </summary>
            public DateTopBarItemClass monthyear_btn;
            /// <summary>
            /// 月按钮
            /// </summary>
            public DateTopBarItemClass month_btn;
            /// <summary>
            /// 年按钮
            /// </summary>
            public DateTopBarItemClass year_btn;
            /// <summary>
            /// 下一月按钮
            /// </summary>
            public DateTopBarItemClass next_month_btn;
            /// <summary>
            /// 下一年按钮
            /// </summary>
            public DateTopBarItemClass next_year_btn;
        }
        /// <summary>
        /// 日期面板顶部工具栏选项
        /// </summary>
        [Description("日期面板顶部工具栏选项")]
        public class DateTopBarItemClass
        {
            public DatePickerExt parent;
            public DateClass ower;

            public DateTopBarItemClass(DatePickerExt parent, DateClass ower)
            {
                this.parent = parent;
                this.ower = ower;
            }

            /// <summary>
            /// 顶部工具栏选项rect信息
            /// </summary>
            public Rectangle Rect;

            /// <summary>
            ///顶部工具栏选项文本
            /// </summary>
            public string Text;
            /// <summary>
            /// 顶部工具栏选项按钮图形路径
            /// </summary>
            public Point[] LineLeftPoint;
            /// <summary>
            /// 顶部工具栏选项按钮图形路径
            /// </summary>
            public Point[] LineRightPoint;

            private DateItemMoveStatuss moveStatus = DateItemMoveStatuss.Normal;
            /// <summary>
            /// 顶部工具栏选项鼠标状态
            /// </summary>
            [DefaultValue(DateItemMoveStatuss.Normal)]
            [Description("顶部工具栏选项鼠标状态")]
            public DateItemMoveStatuss MoveStatus
            {
                get { return this.moveStatus; }
                set
                {
                    if (this.moveStatus == value)
                        return;
                    this.moveStatus = value;
                    if (this.parent != null)
                    {
                        this.parent.Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// 日期面板
        /// </summary>
        [Description("日期面板")]
        public class DateMainClass
        {
            public DatePickerExt parent;
            public DateClass ower;

            public DateMainClass(DatePickerExt parent, DateClass ower)
            {
                this.parent = parent;
                this.ower = ower;

                this.yearItem = new DateMainItemClass[12];
                for (int i = 0; i < this.yearItem.Length; i++)
                {
                    this.yearItem[i] = new DateMainItemClass(parent, ower);
                }
                this.monthItem = new DateMainItemClass[12];
                for (int i = 0; i < this.monthItem.Length; i++)
                {
                    this.monthItem[i] = new DateMainItemClass(parent, ower);
                }
                this.dayItem = new DateMainItemClass[49];
                for (int i = 0; i < this.dayItem.Length; i++)
                {
                    this.dayItem[i] = new DateMainItemClass(parent, ower);
                }
            }

            /// <summary>
            /// 日期面板rect信息
            /// </summary>
            public Rectangle Rect;
            /// <summary>
            /// 年选项列表
            /// </summary>
            public DateMainItemClass[] yearItem;
            /// <summary>
            /// 月选项列表
            /// </summary>
            public DateMainItemClass[] monthItem;
            /// <summary>
            /// 日选项列表
            /// </summary>
            public DateMainItemClass[] dayItem;
        }
        /// <summary>
        /// 日期面板选项
        /// </summary>
        [Description("日期面板选项")]
        public class DateMainItemClass
        {
            public DatePickerExt parent;
            public DateClass ower;

            public DateMainItemClass(DatePickerExt parent, DateClass ower)
            {
                this.parent = parent;
                this.ower = ower;
            }

            /// <summary>
            /// 选项rect信息
            /// </summary>
            public Rectangle Rect;

            /// <summary>
            ///选项值
            /// </summary>
            public DateTime Value;

            /// <summary>
            ///选项文本
            /// </summary>
            public string Text;

            private DateItemMoveStatuss moveStatus = DateItemMoveStatuss.Normal;
            /// <summary>
            /// 日期面板选项选项鼠标状态
            /// </summary>
            [DefaultValue(DateItemMoveStatuss.Normal)]
            [Description("日期面板选项选项鼠标状态")]
            public DateItemMoveStatuss MoveStatus
            {
                get { return this.moveStatus; }
                set
                {
                    if (this.moveStatus == value)
                        return;
                    this.moveStatus = value;
                    if (this.parent != null)
                    {
                        this.parent.Invalidate();
                    }
                }
            }

            /// <summary>
            /// 日期面板选项类型
            /// </summary>
            public DateItemTypes ItemType;
        }

        /// <summary>
        /// 底部工具栏
        /// </summary>
        [Description("底部工具栏")]
        public class BottomBarClass
        {
            public DatePickerExt parent;
            public DateClass ower;

            public BottomBarClass(DatePickerExt parent, DateClass ower)
            {
                this.parent = parent;
                this.ower = ower;

                this.bottombar_minmaxborder_btn = new BottomBarItemClass(parent, ower) { Text = "", MoveStatus = DateItemMoveStatuss.Normal };
                this.bottombar_mindate_btn = new BottomBarItemClass(parent, ower) { Text = "", MoveStatus = DateItemMoveStatuss.Normal };
                this.bottombar_maxdate_btn = new BottomBarItemClass(parent, ower) { Text = "", MoveStatus = DateItemMoveStatuss.Normal };
                this.bottombar_clear_btn = new BottomBarItemClass(parent, ower) { Text = "清除", MoveStatus = DateItemMoveStatuss.Normal };
                this.bottombar_now_btn = new BottomBarItemClass(parent, ower) { Text = "现在", MoveStatus = DateItemMoveStatuss.Normal };
                this.bottombar_confirm_btn = new BottomBarItemClass(parent, ower) { Text = "确认", MoveStatus = DateItemMoveStatuss.Normal };
            }

            /// <summary>
            /// 底部工具栏rect信息
            /// </summary>
            public Rectangle Rect;
            /// <summary>
            /// 底部工具栏最小时间最大时间线
            /// </summary>
            public BottomBarItemClass bottombar_minmaxborder_btn;
            /// <summary>
            /// 底部工具栏最小时间
            /// </summary>
            public BottomBarItemClass bottombar_mindate_btn;
            /// <summary>
            /// 底部工具栏最大时间
            /// </summary>
            public BottomBarItemClass bottombar_maxdate_btn;
            /// <summary>
            /// 底部工具栏清除按钮
            /// </summary>
            public BottomBarItemClass bottombar_clear_btn;
            /// <summary>
            /// 底部工具栏现在按钮
            /// </summary>
            public BottomBarItemClass bottombar_now_btn;
            /// <summary>
            /// 底部工具栏确认按钮
            /// </summary>
            public BottomBarItemClass bottombar_confirm_btn;
        }
        /// <summary>
        /// 底部工具栏选项
        /// </summary>
        [Description("底部工具栏选项")]
        public class BottomBarItemClass
        {
            public DatePickerExt parent;
            public DateClass ower;

            public BottomBarItemClass(DatePickerExt parent, DateClass ower)
            {
                this.parent = parent;
                this.ower = ower;
            }

            /// <summary>
            /// 底部工具栏选项rect信息
            /// </summary>
            public Rectangle Rect;

            /// <summary>
            ///底部工具栏选项文本
            /// </summary>
            public string Text;

            /// <summary>
            /// 底部工具栏选项按钮图形路径
            /// </summary>
            public Point[] LinePoint;

            private DateItemMoveStatuss moveStatus = DateItemMoveStatuss.Normal;
            /// <summary>
            /// 底部工具栏选项鼠标状态
            /// </summary>
            [DefaultValue(DateItemMoveStatuss.Normal)]
            [Description("底部工具栏选项鼠标状态")]
            public DateItemMoveStatuss MoveStatus
            {
                get { return this.moveStatus; }
                set
                {
                    if (this.moveStatus == value)
                        return;
                    this.moveStatus = value;
                    if (this.parent != null)
                    {
                        this.parent.Invalidate();
                    }
                }
            }

            /// <summary>
            /// 日期面板选项类型
            /// </summary>
            public DateItemTypes ItemType;
        }
        #endregion

        #region 枚举
        /// <summary>
        /// 显示功能类型
        /// </summary>
        [Description("显示功能类型")]
        public enum DateDisplayTypes
        {
            /// <summary>
            /// 年
            /// </summary>
            Year,
            /// <summary>
            /// 年月
            /// </summary>
            YearMonth,
            /// <summary>
            /// 年月日
            /// </summary>
            YearMonthDay
        }
        /// <summary>
        /// 在指定显示功能类型下面板显示状态
        /// </summary>
        [Browsable(false)]
        [Description("在指定显示功能类型下面板显示状态")]
        public enum DateDisplayStatuss
        {
            /// <summary>
            /// 功能默认面板
            /// </summary>
            Default,
            /// <summary>
            /// 年月功能中(年面板)
            /// </summary>
            YearMonth_Year,
            /// <summary>
            /// 年月日功能中(月面板)
            /// </summary>
            YearMonthDay_Month,
            /// <summary>
            /// 年月日功能中(年面板)
            /// </summary>
            YearMonthDay_Year
        }
        /// <summary>
        /// 鼠标在选项上状态
        /// </summary>
        [Description("鼠标在选项上状态")]
        public enum DateItemMoveStatuss
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal,
            /// <summary>
            /// 鼠标进入
            /// </summary>
            Enter
        }
        /// <summary>
        /// 日期面板选项类型
        /// </summary>
        [Description("日期面板选项类型")]
        public enum DateItemTypes
        {
            /// <summary>
            /// 标题
            /// </summary>
            Title,
            /// <summary>
            /// 过期日期
            /// </summary>
            Past,
            /// <summary>
            /// 正常日期
            /// </summary>
            Normal,
            /// <summary>
            /// 未来日期
            /// </summary>
            Future,
            /// <summary>
            /// 禁用
            /// </summary>
            Disabled
        }
        #endregion

        #region 事件参数
        /// <summary>
        /// 顶部工具栏选项单击事件参数
        /// </summary>
        [Description("顶部工具栏选项单击事件参数")]
        public class TopBarIiemEventArgs : EventArgs
        {
            /// <summary>
            /// 顶部工具栏选项
            /// </summary>
            [Description("顶部工具栏选项")]
            public DateTopBarItemClass Item { get; set; }
        }
        /// <summary>
        /// 日期面板选项单击事件参数
        /// </summary>
        [Description("日期面板选项单击事件参数")]
        public class DateMainItemEventArgs : EventArgs
        {
            /// <summary>
            /// 日期面板选项
            /// </summary>
            [Description("日期面板选项")]
            public DateMainItemClass Item { get; set; }
        }
        /// <summary>
        /// 底部工具栏选项单击事件参数
        /// </summary>
        [Description("底部工具栏选项单击事件参数")]
        public class BottomBarIiemEventArgs : EventArgs
        {
            /// <summary>
            /// 底部工具栏选项
            /// </summary>
            [Description("底部工具栏选项")]
            public BottomBarItemClass Item { get; set; }
        }
        #endregion

        /// <summary>
        /// 画笔管理
        /// </summary>
        [Description("画笔管理")]
        public class SolidBrushManage
        {
            private DatePickerExt ower;

            public SolidBrushManage(DatePickerExt ower)
            {
                this.ower = ower;
            }

            #region 顶部工具栏

            private SolidBrush _topbarback_sb = null;
            public SolidBrush topbarback_sb
            {
                get
                {
                    if (this._topbarback_sb == null)
                        this._topbarback_sb = new SolidBrush(this.ower.TopBarBackColor);
                    return this._topbarback_sb;
                }
            }

            private SolidBrush _topbarbtnfore_sb = null;
            public SolidBrush topbarbtnfore_sb
            {
                get
                {
                    if (this._topbarbtnfore_sb == null)
                        this._topbarbtnfore_sb = new SolidBrush(this.ower.TopBarBtnForeColor);
                    return this._topbarbtnfore_sb;
                }
            }

            private Pen _topbarbtnfore_pen = null;
            public Pen topbarbtnfore_pen
            {
                get
                {
                    if (this._topbarbtnfore_pen == null)
                        this._topbarbtnfore_pen = new Pen(this.ower.TopBarBtnForeColor, 1);
                    return this._topbarbtnfore_pen;
                }
            }

            private Pen _topbarbtnfore_enter_pen = null;
            public Pen topbarbtnfore_enter_pen
            {
                get
                {
                    if (this._topbarbtnfore_enter_pen == null)
                        this._topbarbtnfore_enter_pen = new Pen(this.ower.topBarBtnForeEnterColor, 1);
                    return this._topbarbtnfore_enter_pen;
                }
            }

            #endregion

            #region 日期面板

            private SolidBrush _dateback_sb = null;
            public SolidBrush dateback_sb
            {
                get
                {
                    if (this._dateback_sb == null)
                        this._dateback_sb = new SolidBrush(this.ower.DateBackColor);
                    return this._dateback_sb;
                }
            }

            private SolidBrush _titlefore_sb = null;
            public SolidBrush titlefore_sb
            {
                get
                {
                    if (this._titlefore_sb == null)
                        this._titlefore_sb = new SolidBrush(this.ower.DateTitleForeColor);
                    return this._titlefore_sb;
                }
            }

            private SolidBrush _pastfore_sb = null;
            public SolidBrush pastfore_sb
            {
                get
                {
                    if (this._pastfore_sb == null)
                        this._pastfore_sb = new SolidBrush(this.ower.DatePastForeColor);
                    return this._pastfore_sb;
                }
            }

            private SolidBrush _normalfore_sb = null;
            public SolidBrush normalfore_sb
            {
                get
                {
                    if (this._normalfore_sb == null)
                        this._normalfore_sb = new SolidBrush(this.ower.DateNormalForeColor);
                    return this._normalfore_sb;
                }
            }

            private SolidBrush _futurefore_sb = null;
            public SolidBrush futurefore_sb
            {
                get
                {
                    if (this._futurefore_sb == null)
                        this._futurefore_sb = new SolidBrush(this.ower.DateFutureForeColor);
                    return this._futurefore_sb;
                }
            }

            private SolidBrush _backdisabled_sb = null;
            public SolidBrush backdisabled_sb
            {
                get
                {
                    if (this._backdisabled_sb == null)
                        this._backdisabled_sb = new SolidBrush(this.ower.DateBackDisabledColor);
                    return this._backdisabled_sb;
                }
            }

            private SolidBrush _foredisabled_sb = null;
            public SolidBrush foredisabled_sb
            {
                get
                {
                    if (this._foredisabled_sb == null)
                        this._foredisabled_sb = new SolidBrush(this.ower.DateForeDisabledColor);
                    return this._foredisabled_sb;
                }
            }

            private SolidBrush _backselected_sb = null;
            public SolidBrush backselected_sb
            {
                get
                {
                    if (this._backselected_sb == null)
                        this._backselected_sb = new SolidBrush(this.ower.DateBackSelectedColor);
                    return this._backselected_sb;
                }
            }

            private SolidBrush _foreselected_sb = null;
            public SolidBrush foreselected_sb
            {
                get
                {
                    if (this._foreselected_sb == null)
                        this._foreselected_sb = new SolidBrush(this.ower.DateForeSelectedColor);
                    return this._foreselected_sb;
                }
            }

            private SolidBrush _backenter_sb = null;
            public SolidBrush backenter_sb
            {
                get
                {
                    if (this._backenter_sb == null)
                        this._backenter_sb = new SolidBrush(this.ower.DateBackEnterColor);
                    return this._backenter_sb;
                }
            }

            #endregion

            #region 底部工具栏

            private SolidBrush _bottombarback_sb = null;
            public SolidBrush bottombarback_sb
            {
                get
                {
                    if (this._bottombarback_sb == null)
                        this._bottombarback_sb = new SolidBrush(this.ower.BottomBarBackColor);
                    return this._bottombarback_sb;
                }
            }

            private Pen _bottombarborder_pen = null;
            public Pen bottombarborder_pen
            {
                get
                {
                    if (this._bottombarborder_pen == null)
                        this._bottombarborder_pen = new Pen(this.ower.BottomBarBackBorderColor, 1);
                    return this._bottombarborder_pen;
                }
            }

            private SolidBrush _bottombar_tip_sb = null;
            public SolidBrush bottombar_tip_sb
            {
                get
                {
                    if (this._bottombar_tip_sb == null)
                        this._bottombar_tip_sb = new SolidBrush(this.ower.BottomBarTipForeColor);
                    return this._bottombar_tip_sb;
                }
            }

            private SolidBrush _bottombarbtnback_sb = null;
            public SolidBrush bottombarbtnback_sb
            {
                get
                {
                    if (this._bottombarbtnback_sb == null)
                        this._bottombarbtnback_sb = new SolidBrush(this.ower.BottomBarBtnBackColor);
                    return this._bottombarbtnback_sb;
                }
            }

            private SolidBrush _bottombarbtnfore_sb = null;
            public SolidBrush bottombarbtnfore_sb
            {
                get
                {
                    if (this._bottombarbtnfore_sb == null)
                        this._bottombarbtnfore_sb = new SolidBrush(this.ower.BottomBarBtnForeColor);
                    return this._bottombarbtnfore_sb;
                }
            }

            private SolidBrush _bottombarbtnbackdisabled_sb = null;
            public SolidBrush bottombarbtnbackdisabled_sb
            {
                get
                {
                    if (this._bottombarbtnbackdisabled_sb == null)
                        this._bottombarbtnbackdisabled_sb = new SolidBrush(this.ower.BottomBarBtnBackDisabledColor);
                    return this._bottombarbtnbackdisabled_sb;
                }
            }

            private SolidBrush _bottombarbtnforedisabled_sb = null;
            public SolidBrush bottombarbtnforedisabled_sb
            {
                get
                {
                    if (this._bottombarbtnforedisabled_sb == null)
                        this._bottombarbtnforedisabled_sb = new SolidBrush(this.ower.BottomBarBtnForeDisabledColor);
                    return this._bottombarbtnforedisabled_sb;
                }
            }

            private SolidBrush _bottombarbtnbackenter_sb = null;
            public SolidBrush bottombarbtnbackenter_sb
            {
                get
                {
                    if (this._bottombarbtnbackenter_sb == null)
                        this._bottombarbtnbackenter_sb = new SolidBrush(this.ower.BottomBarBtnBackEnterColor);
                    return this._bottombarbtnbackenter_sb;
                }
            }

            private SolidBrush _bottombarbtnforeenter_sb = null;
            public SolidBrush bottombarbtnforeenter_sb
            {
                get
                {
                    if (this._bottombarbtnforeenter_sb == null)
                        this._bottombarbtnforeenter_sb = new SolidBrush(this.ower.BottomBarBtnForeEnterColor);
                    return this._bottombarbtnforeenter_sb;
                }
            }

            #endregion

            /// <summary>
            /// 释放所有画笔
            /// </summary>
            public void ReleaseSolidBrushs()
            {
                #region
                if (this._topbarback_sb != null)
                    this._topbarback_sb.Dispose();
                if (this._topbarbtnfore_sb != null)
                    this._topbarbtnfore_sb.Dispose();
                if (this._topbarbtnfore_pen != null)
                    this._topbarbtnfore_pen.Dispose();
                if (this._topbarbtnfore_enter_pen != null)
                    this._topbarbtnfore_enter_pen.Dispose();
                #endregion

                #region
                if (this._dateback_sb != null)
                    this._dateback_sb.Dispose();
                if (this._titlefore_sb != null)
                    this._titlefore_sb.Dispose();
                if (this._pastfore_sb != null)
                    this._pastfore_sb.Dispose();
                if (this._normalfore_sb != null)
                    this._normalfore_sb.Dispose();
                if (this._futurefore_sb != null)
                    this._futurefore_sb.Dispose();
                if (this._backdisabled_sb != null)
                    this._backdisabled_sb.Dispose();
                if (this._foredisabled_sb != null)
                    this._foredisabled_sb.Dispose();
                if (this._backselected_sb != null)
                    this._backselected_sb.Dispose();
                if (this._foreselected_sb != null)
                    this._foreselected_sb.Dispose();
                if (this._backenter_sb != null)
                    this._backenter_sb.Dispose();
                #endregion

                #region
                if (this._bottombarback_sb != null)
                    this._bottombarback_sb.Dispose();
                if (this._bottombarborder_pen != null)
                    this._bottombarborder_pen.Dispose();
                if (this._bottombar_tip_sb != null)
                    this._bottombar_tip_sb.Dispose();
                if (this._bottombarbtnback_sb != null)
                    this._bottombarbtnback_sb.Dispose();
                if (this._bottombarbtnfore_sb != null)
                    this._bottombarbtnfore_sb.Dispose();
                if (this._bottombarbtnbackdisabled_sb != null)
                    this._bottombarbtnbackdisabled_sb.Dispose();
                if (this._bottombarbtnforedisabled_sb != null)
                    this._bottombarbtnforedisabled_sb.Dispose();
                if (this._bottombarbtnbackenter_sb != null)
                    this._bottombarbtnbackenter_sb.Dispose();
                if (this._bottombarbtnforeenter_sb != null)
                    this._bottombarbtnforeenter_sb.Dispose();
                #endregion
            }
        }
    }

    /// <summary>
    /// 展开属性选型去除描述
    /// </summary>
    [Description("展开属性选型去除描述")]
    public class DatePickerExpandableObjectConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// 当该属性为展开属性选型时，属性编辑器删除该属性的描述
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == typeof(DatePickerExt))
                return (object)"";
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
