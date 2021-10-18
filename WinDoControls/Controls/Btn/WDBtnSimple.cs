using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WinDoControls.Controls
{
    /// <summary>
    /// 简单按钮，圆角，阴影，文字居中，文字颜色
    /// </summary>
    public class WDBtnSimple : WinDoControls.Controls.WDCtrlBase
    {
        private Color _LabelTextColor = Color.White;
        public Color LabelTextColor
        {
            get { return _LabelTextColor; }
            set
            {
                _LabelTextColor = value;
                this.Invalidate();
            }
        }

        private string _text = "";
        public string LabelText
        {
            get { return _text; }
            set
            {
                _text = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_text.Length > 0)
                e.Graphics.DrawString(_text, WinDo.Utilities.PublicResource.WDFonts.TextFont,
                    new System.Drawing.SolidBrush(_LabelTextColor)
          , e.ClipRectangle, new System.Drawing.StringFormat() { Alignment = System.Drawing.StringAlignment.Center, LineAlignment = System.Drawing.StringAlignment.Center });
        }
    }
}
