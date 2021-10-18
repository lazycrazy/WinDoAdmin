using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinDoControls.Controls;
using System.Windows.Forms;
using System.Drawing;
using WinDo.Utilities.PublicResource;
using System.ComponentModel;

namespace WinDoControls.Controls
{
    [ToolboxItem(false)]
    public class WDBtnExtTips : WDBtnExt
    {
        public WDBtnExtTips()
        {
            base.IsShowTips = true;
            base.TipsText = "99";
            base.TipsColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            base.BtnTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            base.BackColor = Color.Transparent;
            base.Font = WDFonts.TextFont;
            base.BtnBackColor = Color.Transparent;
            base.BtnFont = WDFonts.TextFont;
            base.BtnForeColor = WDColors.BtnForeColor;
            this.Cursor = Cursors.Default;
            this.BtnText = "状态按钮";
            this.Size = new Size(73, 42);
        }
    }
}
