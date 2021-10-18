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
    public partial class UCHighPanelQuote : UserControl
    {
        public UCHighPanelQuote()
        {
            InitializeComponent();
            ucPanelQuote1.LeftColor = WDColors.StatusRed;
            SplitLineColor = WDColors.StatusGray;
            LeftPadding = 3;
            lblt1.Font = WDFonts.TextFontBold;
        }




        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        [Description("边框颜色"), Category("自定义")]
        public Color BorderColor
        {
            get { return ucPanelQuote1.BorderColor; }
            set
            {
                ucPanelQuote1.BorderColor = value;                
                this.Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the color of the left.
        /// </summary>
        /// <value>The color of the left.</value>
        [Description("左侧颜色"), Category("自定义")]
        public Color LeftColor
        {
            get { return ucPanelQuote1.LeftColor; }
            set
            {
                ucPanelQuote1.LeftColor = value;
                this.Invalidate();
            }
        }

        [Description("水平分割线颜色"), Category("自定义")]
        public Color SplitLineColor
        {
            get { return ucSplitLine_H1.BackColor; }
            set
            {
                ucSplitLine_H1.BackColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the left.
        /// </summary>
        /// <value>The color of the left.</value>
        [Description("左侧宽度"), Category("自定义")]
        public int LeftPadding
        {
            get { return ucPanelQuote1.Padding.Left; }
            set
            {
                ucPanelQuote1.Padding = new Padding(value, ucPanelQuote1.Padding.Top, ucPanelQuote1.Padding.Right, ucPanelQuote1.Padding.Bottom);
                this.Invalidate();
            }
        }

        [Description("间隔宽度"), Category("自定义")]
        public int SpaceWidth
        {
            get { return this.ucSplitLine_V1.Width; }
            set
            {
                this.ucSplitLine_V1.Width = value;
                this.Invalidate();
            }
        }

        [Description("标题1字体"), Category("自定义")]
        public Font T1Font
        {
            get
            {
                return lblt1.Font;
            }
            set
            {
                lblt1.Font = value;
            }
        }
        [Description("标题2字体"), Category("自定义")]
        public Font T2Font
        {
            get
            {
                return lblt2.Font;
            }
            set
            {
                lblt2.Font = value;
            }
        }
       

        [Description("标题1"), Category("自定义")]
        public string Title1
        {
            get { return this.lblt1.Text; }
            set { this.lblt1.Text = value; }
        }

        [Description("标题2"), Category("自定义")]
        public string Title2
        {
            get { return this.lblt2.Text; }
            set { this.lblt2.Text = value; }
        } 
    }
}
