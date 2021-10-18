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
    public partial class UCLowPanelQuote : UserControl
    {
        public UCLowPanelQuote()
        {
            InitializeComponent();
            label1.Font = WDFonts.TextFontBold;
            ucPanelQuote1.LeftColor = WDColors.StatusBlue;
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
        public new Color LeftColor
        {
            get { return ucPanelQuote1.LeftColor; }
            set
            {
                ucPanelQuote1.LeftColor = value;
                //label1.ForeColor = value;
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

        [Description("标题字体"), Category("自定义")]
        public override Font Font
        {
            get
            {
                return label1.Font;
            }
            set
            {
                label1.Font = value;
            }
        }



        [Description("标题"), Category("自定义")]
        public string Title
        {
            get { return this.label1.Text; }
            set { this.label1.Text = value; }
        }


        [Description("标题颜色"), Category("自定义")]
        public Color TitleForeColor
        {
            get { return this.label1.ForeColor; }
            set { this.label1.ForeColor = value; }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
    }
}
