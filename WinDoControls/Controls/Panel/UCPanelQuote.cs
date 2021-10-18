using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinDoControls.Controls
{
    /// <summary>
    /// Class UCPanelQuote.
    /// Implements the <see cref="System.Windows.Forms.Panel" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Panel" />
    public class UCPanelQuote : Panel
    {
        /// <summary>
        /// The border color
        /// </summary>
        private Color borderColor = Color.Transparent;

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        [Description("边框颜色"), Category("自定义")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// The left color
        /// </summary>
        private Color leftColor = Color.Red;

        /// <summary>
        /// Gets or sets the color of the left.
        /// </summary>
        /// <value>The color of the left.</value>
        [Description("左侧颜色"), Category("自定义")]
        public Color LeftColor
        {
            get { return leftColor; }
            set
            {
                leftColor = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UCPanelQuote"/> class.
        /// </summary>
        public UCPanelQuote()
            : base()
        {
            Padding = new Padding(5, 0, 0, 0);
        }

        /// <summary>
        /// 引发 <see cref="E:System.Windows.Forms.Control.Paint" /> 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.PaintEventArgs" />。</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SetGDIHigh();
            if (borderColor != Color.Transparent)
                e.Graphics.DrawLines(new Pen(borderColor), new Point[] 
            { 
                new Point(e.ClipRectangle.Left,e.ClipRectangle.Top),
                new Point(e.ClipRectangle.Right-1,e.ClipRectangle.Top),
                new Point(e.ClipRectangle.Right-1,e.ClipRectangle.Bottom-1),
                new Point(e.ClipRectangle.Left,e.ClipRectangle.Bottom-1),
                new Point(e.ClipRectangle.Left,e.ClipRectangle.Top)
            });

            e.Graphics.FillRectangle(new SolidBrush(leftColor), new Rectangle(0, 0, Padding.Left, this.Height));
        }
    }
}
