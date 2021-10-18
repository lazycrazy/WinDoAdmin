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
    public partial class WDBtnExt : WDCtrlBase
    {
        #region 字段属性


        private bool isShowTips = false;

        [Description("是否显示角标"), Category("自定义")]
        public bool IsShowTips
        {
            get
            {
                return this.isShowTips;
            }
            set
            {
                this.isShowTips = value;
                this.Invalidate();
            }
        }

        public int AddTipCount(int count = -1)
        {
            var num = int.Parse(TipsText);
            var snum = (Math.Max(num + count, 0));
            TipsText = snum.ToString();
            return snum;
        }



        private string tipsText = "";

        [Description("角标文字"), Category("自定义")]
        public string TipsText
        {
            get
            {
                return this.tipsText;
            }
            set
            {
                this.tipsText = value;
                this.Invalidate();
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


        public new bool BtnEnabled
        {
            get { return _btnEnabled; }
            set
            {
                _btnEnabled = value;
                if (_btnEnabled)
                {
                    SetBtnForeColor(_defaultForeColor);
                    RefreshIcon();
                    if (this.IsShowShadow && this.Parent != null)
                        this.Parent.Invalidate();
                }
                else
                {
                    if (this._fillColor == WinDo.Utilities.PublicResource.WDColors.WhiteColor
                        || this._fillColor == Color.White
                        )
                    {
                        SetBtnForeColor(ColorTranslator.FromHtml("#b6b6b6"));
                        RefreshIcon();
                        if (this.IsShowShadow && this.Parent != null)
                            this.Parent.Invalidate();
                    }
                }
                this.Cursor = _btnEnabled ? Cursors.Hand : Cursors.Default;
                this.Invalidate();
            }
        }

        protected virtual void RefreshIcon()
        {
        }

        protected Color _defaultForeColor;


        protected Color _btnForeColor = Color.White;
        [Description("按钮字体颜色"), Category("自定义")]
        public virtual Color BtnForeColor
        {
            get { return _btnForeColor; }
            set
            {
                _btnForeColor = value;
                _defaultForeColor = _btnForeColor;
                this.lbl.ForeColor = value;
            }
        }

        protected void SetBtnForeColor(Color color)
        {
            _btnForeColor = color;
            this.lbl.ForeColor = color;
        }


        private Font _btnFont = WDFonts.TextFont; //new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));




        [Description("按钮字体"), Category("自定义")]
        public Font BtnFont
        {
            get { return _btnFont; }
            set
            {
                _btnFont = value;
                _defaultBtnFont = value;
                this.lbl.Font = value;
            }
        }

        protected void SetBtnFont(Font value)
        {
            _btnFont = value;
            this.lbl.Font = value;
        }
        protected Font _defaultBtnFont;

        [Description("按钮点击事件"), Category("自定义")]
        public event EventHandler BtnClick;

        private bool _enabled = true;

        public void OnBtnClick(object sender, EventArgs e)
        {
            if (!_btnEnabled) return;
            try
            {
                if (_enabled)
                {
                    _enabled = false;
                    if (BtnClick != null)
                        BtnClick(sender, e);
                }
            }
            finally
            {
                _enabled = true;
            }
        }


        private string _btnText;




        [Description("按钮文字"), Category("自定义")]
        public virtual string BtnText
        {
            get { return _btnText; }
            set
            {
                _btnText = value;
                lbl.Text = value;
            }
        }






        [Description("文字对齐"), Category("自定义")]
        public virtual ContentAlignment BtnTextAlign
        {
            get { return lbl.TextAlign; }
            set
            {
                lbl.TextAlign = value;
            }
        }




        private Color m_tipsColor = WDColors.LinkColor;




        [Description("角标颜色"), Category("自定义")]
        public Color TipsColor
        {
            get { return m_tipsColor; }
            set { m_tipsColor = value; }
        }
        #endregion

        public void RemoveTip()
        {
            //this.lblTips.Dispose();
            //this.lblTips = null;
        }

        public WDBtnExt()
        {
            InitializeComponent();
            this.TabStop = false;
            //lblTips.Paint += lblTips_Paint;
            //lblTips.MouseClick += new MouseEventHandler(lblTips_MouseClick);
            lbl.MouseHover += new EventHandler(OnLblMouseHover);
            lbl.MouseEnter += new EventHandler(lbl_MouseEnter);
            lbl.MouseLeave += new EventHandler(lbl_MouseLeave);
        }

        public event EventHandler LblMouseHover;
        void OnLblMouseHover(object sender, EventArgs e)
        {
            if (LblMouseHover != null)
                LblMouseHover(this, e);
        }
        public event EventHandler LblMouseLevel;
        void OnLblMouseLevel(object sender, EventArgs e)
        {
            if (LblMouseLevel != null)
                LblMouseLevel(this, e);
        }

        void lbl_MouseLeave(object sender, EventArgs e)
        {
            this.OnLblMouseLevel(sender, e);
            if (!UseHoverColor) return;
            FillColor = _fillColorBak;
            BtnForeColor = _foreColorBak;
        }


        private Color _fillColorBak;
        private Color _foreColorBak;
        void lbl_MouseEnter(object sender, EventArgs e)
        {

            if (!UseHoverColor) return;
            _fillColorBak = _fillColor;
            _foreColorBak = _btnForeColor;
            FillColor = HoverColor;
            BtnForeColor = HoverForeColor;

        }

        public Color HoverColor = WDColors.geekblue6;
        public Color HoverForeColor = WDColors.WhiteColor;


        public bool _UseHoverColor = false;
        public bool UseHoverColor
        {
            get
            {
                if (!_btnEnabled)
                    return false;
                return _UseHoverColor;
            }
            set { _UseHoverColor = value; }
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None
                && (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter))
            {
                OnBtnClick(this, e);
            }
            base.OnKeyDown(e);
        }





        void lblTips_MouseClick(object sender, MouseEventArgs e)
        {
            OnBtnClick(this, e);
        }




        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.isShowTips && !string.IsNullOrWhiteSpace(this.tipsText))
            {
                var btsize = TextRenderer.MeasureText(this.BtnText, BtnFont);
                var tsize = TextRenderer.MeasureText(this.tipsText, BtnFont);
                var allwidth = btsize.Width + tsize.Width + 10;
                this.Width = allwidth;
                var sf = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
                var rect = e.ClipRectangle;
                rect.Width -= 10;
                rect.Height += 2;
                using (var brush = new SolidBrush(this.m_tipsColor))
                    e.Graphics.DrawString(this.tipsText, BtnFont, brush, rect, sf);
            }
        }







        void lblTips_Paint(object sender, PaintEventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(TipsText)) return;
            //e.Graphics.SetGDIHigh();
            ////e.Graphics.FillEllipse(new SolidBrush(m_tipsColor), new Rectangle(0, lblTips.Height / 2 - 10, lblTips.Width - 1, lblTips.Width - 1));
            //System.Drawing.SizeF sizeEnd = e.Graphics.MeasureString(TipsText, lblTips.Font);

            //e.Graphics.DrawString(TipsText, lblTips.Font, new SolidBrush(lblTips.ForeColor), new PointF((lblTips.Width - sizeEnd.Width) / 2, (lblTips.Height - sizeEnd.Height) / 2));
        }






        private void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            OnBtnClick(this, e);
        }


    }
}
