using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{
    [DefaultEvent("BtnClick")]
    [Description("左右都可以显示图标的按钮")]
    public partial class WDBtnExt2Img : WDCtrlBase
    {

        //右边图标  I_success_fill   I_exclamation_fill
        //左边图标  I_register I_registed
        #region 字段属性
        [Description("是否显示右边图标"), Category("自定义"), DefaultValue(true)]
        public bool IsShowR
        {
            get
            {
                return this.lblRight.Visible;
            }
            set
            {
                this.lblRight.Visible = value;
                if (this.lblRight.Visible)
                    lblRight.BringToFront();
                lblText.BringToFront();

            }
        }

        [Description("是否显示左边图标"), Category("自定义"), DefaultValue(true)]
        public bool IsShowL
        {
            get
            {
                return this.lblLeft.Visible;
            }
            set
            {
                this.lblLeft.Visible = value;
                if (lblLeft.Visible)
                    lblLeft.BringToFront();

                lblText.BringToFront();
            }

        }


        private Color _btnBackColor = Color.White;

        [Description("按钮背景色"), Category("自定义")]
        public Color BtnBackColor
        {
            get { return _btnBackColor; }
            set
            {
                _btnBackColor = value;
                this.BackColor = value;
            }
        }

        private bool useSelectState = true;
        [Description("使用选中状态"), Category("自定义")]
        public bool UseSelectState
        {
            get
            {
                return useSelectState;
            }
            set { useSelectState = value; }
        }

        private bool _IsSelected = false;
        [Description("使选中状态"), Category("自定义")]
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                if (UseSelectState)
                    SetSelected();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (UseSelectState && IsSelected)
            {
                e.Graphics.DrawLine(new Pen(WDColors.BtnSelectedLine, 4f), 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Top);
            }
        }




        private Color _btnForeColor = WDColors.BlackColor;




        [Description("按钮字体颜色"), Category("自定义")]
        public virtual Color BtnForeColor
        {
            get { return _btnForeColor; }
            set
            {
                _btnForeColor = value;
                _defaultBtnForeColor = value;
                this.lblText.ForeColor = value;
            }
        }

        private Font _btnFont = WDFonts.TextFont;

        [Description("按钮字体"), Category("自定义")]
        public Font BtnFont
        {
            get { return _btnFont; }
            set
            {
                _btnFont = value;
                _defaultBtnFont = value;
                this.lblText.Font = value;
            }
        }


        [Description("按钮点击事件"), Category("自定义")]
        public event EventHandler BtnClick;



        private Font _defaultBtnFont;
        private string _btnText;




        [Description("按钮文字"), Category("自定义")]
        public virtual string BtnText
        {
            get { return _btnText; }
            set
            {
                _btnText = value;
                lblText.Text = value;
            }
        }

        [Description("文字对齐"), Category("自定义")]
        public virtual ContentAlignment BtnTextAlign
        {
            get { return lblText.TextAlign; }
            set
            {
                lblText.TextAlign = value;
            }
        }

        private Color m_rColor = WDColors.StatusBlue;


        [Description("右标颜色"), Category("自定义")]
        public Color RColor
        {
            get { return m_rColor; }
            set
            {
                m_rColor = value;
                SetSelected();
            }
        }


        private string _RIconName = "I_success_fill";
        private string _LIconName = "I_register";
        [Description("右标名"), Category("自定义")]
        public string RIconName
        {
            get { return _RIconName; }
            set
            {
                _RIconName = value;
                lblRight.Image = WDImages.GetBtnIconImage(RIconName, color: RColor);
            }
        }
        [Description("左标名"), Category("自定义")]
        public string LIconName
        {
            get { return _LIconName; }
            set
            {
                _LIconName = value;
                SetSelected();
            }
        }
        #endregion



        public WDBtnExt2Img()
        {
            InitializeComponent();
            this.TabStop = false;
            lblRight.MouseClick += new MouseEventHandler(lbl_MouseDown);
            lblLeft.MouseClick += new MouseEventHandler(lbl_MouseDown);
            BtnClick += new EventHandler(UCBtnExt_Click);
            BtnForeColor = WDColors.ForeColor;
            lblRight.Image = WDImages.GetBtnIconImage(RIconName, color: RColor);
            lblLeft.ImageAlign = ContentAlignment.MiddleCenter;
            lblRight.ImageAlign = ContentAlignment.MiddleCenter;
            UseSelectState = true;
            BtnText = "左右";
            IsShowL = true;
            IsShowR = true;
            Padding = new Padding(0);
            Margin = new Padding(0);
            BtnTextAlign = ContentAlignment.MiddleCenter;


            SetSelected();
            //lblRight.BringToFront();
            //lblLeft.BringToFront();
            //lblText.SendToBack();
        }

        private Font _selectedFont = WDFonts.TextFontBold;

        private Color _defaultFillColor = Color.Transparent;
        private Color _defaultBtnForeColor = SystemColors.ControlText;

        void UCBtnExt_Click(object sender, EventArgs e)
        {
            this.IsSelected = true;
        }

        protected void SetBtnForeColor(Color color)
        {
            _btnForeColor = color;
            this.lblText.ForeColor = color;
        }

        protected void SetBtnFont(Font font)
        {
            _btnFont = font;
            this.lblText.Font = font;
        }


        public void SetSelected()
        {
            //if (IsSelected == selected) return;

            if (_IsSelected)
            {
                SetBtnFont(_selectedFont);
                SetBtnForeColor(SelectedBtnForeColor);
                SetFillColor(SelectedFillColor);
                lblLeft.Image = WDImages.GetBtnIconImage(LIconName, color: BtnForeColor);
            }
            else
            {
                SetBtnFont(_defaultBtnFont);
                SetBtnForeColor(_defaultBtnForeColor);
                SetFillColor(_defaultFillColor);
                lblLeft.Image = WDImages.GetBtnIconImage(LIconName, color: BtnForeColor);
            }

        }




        [Description("选中填充色"), Category("自定义")]
        public Color SelectedFillColor
        {
            get { return _SelectedFillColor; }
            set { _SelectedFillColor = value; }
        }
        private Color _SelectedFillColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
        [Description("选中前景色"), Category("自定义")]
        public Color SelectedBtnForeColor
        {
            get { return _SelectedBtnForeColor; }
            set { _SelectedBtnForeColor = value; }
        }
        private Color _SelectedBtnForeColor = WDColors.WhiteColor;


        private bool _SelectedUseRibbon = true;
        [Description("选中状态显示带状条"), Category("自定义"), DefaultValue(true)]
        public bool SelectedUseRibbon
        {
            get
            {
                return _SelectedUseRibbon;
            }
            set
            {
                _SelectedUseRibbon = value;
            }
        }


        /// <summary>
        /// 设置右标
        /// </summary>
        /// <param name="success"></param>
        public void SetSuccess(bool success)
        {
            RIconName = success ? "I_success_fill" : "I_exclamation_fill";
        }


        private void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.BtnClick != null)
                BtnClick(this, e);
        }


    }
}
