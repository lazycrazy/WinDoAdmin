using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{
    public class CycleLabel : Label
    {
        public CycleLabel()
        {
            base.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            base.Text = "描述";
            base.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            base.Margin = new Padding(0);
            base.Padding = new Padding(0);
            base.Font = WDFonts.TextFont;
            SetCycleImg(WDColors.geekblue6);
        }

        private void SetCycleImg(Color color)
        {
            base.Image = WDImages.GetBtnIconImage("A_fa_circle", 14, color);
        }

        private Color _cycleColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;

        [Description("圆形颜色"), Category("自定义")]
        public Color CycleColor
        {
            get { return _cycleColor; }
            set
            {
                _cycleColor = value;
                SetCycleImg(_cycleColor);
            }
        }

        protected override void InitLayout()   //有用
        {
            base.InitLayout();
            AutoSize = false;
        }

        

    }
}
