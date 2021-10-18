using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{
    /// <summary>
    /// 自动宽度按钮
    /// </summary>
    public partial class WDBtnImageAutoWidth : WinDoControls.Controls.WDBtnImg
    {
        public override string BtnText
        {
            get => base.BtnText;
            set
            {
                base.BtnText = value;
                this.Width = (base.Image != null ? base.Image.Width : 10)
                    + Math.Max(10, TextRenderer.MeasureText(base.BtnText, base.BtnFont, new Size(0, 0), TextFormatFlags.NoPadding).Width);
            }
        }
    }
    public partial class WDBtnImg2Words : WinDoControls.Controls.WDBtnImg
    {
        public int ImageSize = 14;
        private string iconName = "I_info";
        [Description("字体图标名"), Category("自定义")]
        public string IconName
        {
            get { return iconName; }
            set
            {

                iconName = value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    base.Image = null;
                }
                else
                {
                    base.Image = WDImages.GetBtnIconImage(value, ImageSize, color: base.BtnForeColor);
                }
            }
        }

        protected override void RefreshIcon()
        {
            IconName = IconName;
        }
        public WDBtnImg2Words()
        {
            InitializeComponent();
            base.Padding = new System.Windows.Forms.Padding(6, 0, 8, 0);//new System.Windows.Forms.Padding(7, 0, 8, 0);
            base.BtnText = "按钮";
            base.BtnFont = WDFonts.TextFont;
            base.BtnTextAlign = ContentAlignment.MiddleRight;
            base.ImageAlign = ContentAlignment.MiddleLeft;
            base.IsShowShadow = true;
            this.BackColor = Color.Transparent;
            base.BtnBackColor = Color.Transparent;
            base.ConerRadius = 1;
            SetColor();

            //this.MouseEnter += new EventHandler(lbl_MouseEnter);
            //this.MouseLeave += new EventHandler(lbl_MouseLeave);
            base.lbl.MouseEnter += new EventHandler(lbl_MouseEnter);
            base.lbl.MouseLeave += new EventHandler(lbl_MouseLeave);
            //base.lblTips.MouseEnter += new EventHandler(lbl_MouseEnter);
            //base.lblTips.MouseLeave += new EventHandler(lbl_MouseLeave);



        }







        private bool isLink = false;
        [Description("超链接"), Category("自定义")]
        public bool IsLink
        {
            get { return isLink; }
            set { isLink = value; }
        }

        protected bool isHoverClick = false;

        void lbl_MouseLeave(object sender, EventArgs e)
        {
            if (!BtnEnabled) return;
            if (isLink)
            {
                base.lbl.Font = _fontBak;
            }
            else
            {
                if (isHoverClick) { isHoverClick = false; return; }
                if (_fillColor == Color.Transparent || _fillColor == Color.Black) return;
                SetFillColor(_fillColorBak);
            }
            this.Invalidate();
        }

        protected Color _fillColorBak;
        private Font _fontBak;
        void lbl_MouseEnter(object sender, EventArgs e)
        {
            if (!BtnEnabled) return;
            isHoverClick = false;
            if (isLink)
            {
                _fontBak = base.lbl.Font;
                base.lbl.Font = new Font(base.lbl.Font.Name, base.lbl.Font.Size, base.lbl.Font.Style | FontStyle.Underline, base.lbl.Font.Unit);
            }
            else
            {
                if (_fillColor == Color.Transparent || _fillColor == Color.Black) return;
                _fillColorBak = base._fillColor;
                SetFillColor(Color.FromArgb(200, base._fillColor));
            }
            this.Invalidate();
        }



        protected virtual void SetColor()
        {
            base.BtnForeColor = WDColors.BtnForeColor;
            base.FillColor = WDColors.WhiteColor;
            base.RectColor = WDColors.GrayRectColor;
            this.IconName = "I_info";
        }

        protected void SetColorAndIcon(Color color, string iconName)
        {
            base.BtnForeColor = WDColors.WhiteColor;
            base.FillColor = color;
            base.RectColor = color;
            this.IconName = iconName;
        }

        protected void SetMainColor()
        {
            SetColorAndIcon(WDColors.geekblue6, "I_info");
        }

        protected void SetRedColor()
        {
            SetColorAndIcon(WDColors.red5, "I_trash");
        }

        protected void SetOrangeColor()
        {
            SetColorAndIcon(WDColors.OrangeColor, "I_trash");

        }
    }

    public class WDBtnImg2WordsGrid : WDBtnImg2Words
    {
        public WDBtnImg2WordsGrid()
            : base()
        {
            base.BtnText = "调整";
            base.Size = new Size(74, 28);
            base.BtnForeColor = WDColors.BtnForeColor;
            base.FillColor = WDColors.WhiteColor;
            base.RectColor = WDColors.GrayRectColor;
            base.IsShowShadow = true;
            this.IconName = "I_edit";
        }
    }

    public class WDBtnImg2WordsGridRed : WDBtnImg2Words
    {
        public WDBtnImg2WordsGridRed()
            : base()
        {
            base.BtnText = "删除";
            base.Size = new Size(74, 28);
            base.BtnForeColor = WDColors.red5;
            base.FillColor = Color.White;
            base.RectColor = WDColors.red5;
            base.IsShowShadow = true;
            this.IconName = "I_trash";
        }
    }

    public class WDBtnImg2WordsYS : WDBtnImg2Words
    {
        protected override void SetColor()
        {
            base.SetMainColor();
        }
    }
    public class WDBtnImg2WordsYSRed : WDBtnImg2Words
    {
        protected override void SetColor()
        {
            base.SetRedColor();
        }
    }

    public class WDBtnImg2WordsYSOrange : WDBtnImg2Words
    {
        protected override void SetColor()
        {
            base.SetOrangeColor();
        }
    }

    public class WDBtnImg3Words : WDBtnImg2Words
    {
        public WDBtnImg3Words()
            : base()
        {
            base.BtnText = "三个字";
            base.Size = new Size(82, 32);
            //base.Padding = new System.Windows.Forms.Padding(7, 0, 5, 0);
        }
    }

    public class WDBtnImg3WordsYS : WDBtnImg3Words
    {
        protected override void SetColor()
        {
            base.SetMainColor();
        }
    }
    public class WDBtnImg3WordsYSRed : WDBtnImg3Words
    {
        protected override void SetColor()
        {
            base.SetRedColor();
        }
    }

    public class WDBtnImg4Words : WDBtnImg2Words
    {
        public WDBtnImg4Words()
            : base()
        {
            base.BtnText = "四字四字";
            this.Size = new Size(97, 32);
            //base.Padding = new System.Windows.Forms.Padding(7, 0, 5, 0);
        }
    }

    public class WDBtnImg4WordsYS : WDBtnImg4Words
    {
        protected override void SetColor()
        {
            base.SetMainColor();
        }
    }
    public class WDBtnImg4WordsYSRed : WDBtnImg4Words
    {
        protected override void SetColor()
        {
            base.SetRedColor();
        }
    }

    public class WDBtnImg0Words : WDBtnImg2Words
    {
        public WDBtnImg0Words()
            : base()
        {
            base.ImageAlign = ContentAlignment.MiddleCenter;
            base.BtnText = "";
            this.Size = new Size(40, 32);
            base.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            base.ImageSize = 25;
        }



    }

    public class WDBtnImg0WordsYS : WDBtnImg0Words
    {
        protected override void SetColor()
        {
            base.SetMainColor();
            this.IconName = "I_search";
        }
    }

    public class WDBtnImg0WordsYSRed : WDBtnImg0Words
    {
        protected override void SetColor()
        {
            base.SetRedColor();
        }
    }


    public class WDBtnImg5Words : WDBtnImg2Words
    {
        public WDBtnImg5Words()
            : base()
        {
            base.BtnText = "五个五个字";
            this.Size = new Size(110, 32);
            //base.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
        }
    }

    public class WDBtnImg5WordsYS : WDBtnImg5Words
    {
        protected override void SetColor()
        {
            base.SetMainColor();
        }
    }
    public class WDBtnImg5WordsYSRed : WDBtnImg5Words
    {
        protected override void SetColor()
        {
            base.SetRedColor();
        }
    }

    [Description("小加号按钮"), Category("按钮组")]
    public partial class WDBtnImgMiniPlus : WinDoControls.Controls.WDBtnImg
    {

        private string iconName = "A_fa_plus"; //"I_plus";
        [Description("字体图标名"), Category("自定义")]
        public string IconName
        {
            get { return iconName; }
            set
            {

                iconName = value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    base.Image = null;
                }
                else
                {
                    base.Image = WDImages.GetBtnIconImage(value, 14, base.BtnForeColor);
                }
            }
        }

        public WDBtnImgMiniPlus()
        {
            this.Size = new System.Drawing.Size(50, 20);
            base.Padding = new System.Windows.Forms.Padding(0);
            base.BtnText = "加加";
            base.ConerRadius = 1;
            base.BtnFont = WDFonts.TextFont12;
            base.BtnTextAlign = ContentAlignment.MiddleRight;
            base.ImageAlign = ContentAlignment.MiddleLeft;
            base.IsShowShadow = false;
            this.BackColor = Color.Transparent;
            base.BtnBackColor = Color.Transparent;
            SetColor();
        }

        protected virtual void SetColor()
        {
            base.BtnForeColor = WDColors.WhiteColor;
            base.FillColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            base.RectColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.IconName = IconName;
        }
    }



    public class WDStateBtnImg0Words : WDBtnImg0Words
    {
        public WDStateBtnImg0Words()
        {
            _btnForeColorBak = BtnForeColor;
            BtnClick += new EventHandler(UCStateBtnImg0Words_BtnClick);
        }

        void UCStateBtnImg0Words_BtnClick(object sender, EventArgs e)
        {
            IsSelected = !IsSelected;
        }


        public bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                if (UseSelectStated)
                    SetSelect();
            }
        }

        private void SetSelect()
        {
            isHoverClick = true;
            if (isSelected)
            {
                SetFillColor(selectedFillColor);
                SetBtnForeColor(selectedForeColor);
            }
            else
            {
                SetFillColor(_defaultFillColor);
                SetBtnForeColor(_defaultForeColor);
            }
            RefreshIcon();
        }

        public bool useSelectStated = true;
        public bool UseSelectStated
        {
            get { return useSelectStated; }
            set { useSelectStated = value; }
        }

        private Color _btnForeColorBak;



        private Color selectedFillColor;
        public Color SelectedFillColor
        {
            get { return selectedFillColor; }
            set
            {
                selectedFillColor = value;
            }
        }

        private Color selectedForeColor;
        public Color SelectedForeColor
        {
            get { return selectedForeColor; }
            set
            {
                selectedForeColor = value;
            }
        }

    }

}
