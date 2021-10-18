using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinDoControls.Controls
{
    [DisplayName("必须输入Label")]
    [Description("前面带红色星号Label")]
    [ToolboxItem(true)]
    public partial class WDRedALabel : Label
    {
        public WDRedALabel() : base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            TextAlign = ContentAlignment.MiddleRight;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var sf = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
            if (TextAlign == ContentAlignment.TopLeft || TextAlign == ContentAlignment.TopCenter || TextAlign == ContentAlignment.TopRight)
                sf.LineAlignment = StringAlignment.Near;
            var width = TextRenderer.MeasureText(this.Text, this.Font).Width;
            var rect = this.ClientRectangle; //e.Graphics.ClipBounds;
            rect.Width -= width;
            rect.Width += 3;
            if (!this.ShowRedAsterisk) return;
            e.Graphics.DrawString("*", this.Font, Brushes.Red, rect, sf);
        }

        void lblText_TextChanged(object sender, EventArgs e)
        {
            //自动设置宽度
            //lblText.Width = TextRenderer.MeasureText(lblText.Text, lblText.Font).Width;

        }

        void UCRedALabel_Resize(object sender, EventArgs e)
        {
            //lblText.Left = (this.ClientSize.Width - lblText.Width) / 2;
            //lblText.Top = (this.ClientSize.Height - lblText.Height) / 2;
        }

        public ContentAlignment LabelTextAlignment
        {
            get { return this.TextAlign; }
            set
            {
                this.TextAlign = value;
            }
        }

        public Font LabelTextFont
        {
            get { return this.Font; }
            set
            {
                this.Font = value;
            }
        }

        [Description("文本"), Category("自定义")]
        public string TextValue
        {
            get { return this.Text; }
            set
            {
                this.Text = value;
            }
        }
        private bool _ShowRedAsterisk = false;

        [Description("是否显示红色星号"), Category("自定义")]
        public bool ShowRedAsterisk
        {
            get { return _ShowRedAsterisk; }
            set
            {
                _ShowRedAsterisk = value;
                this.Invalidate();
            }
        }
    }      
}
