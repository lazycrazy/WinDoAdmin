using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinDoControls.Controls;
using System.Drawing;
using System.ComponentModel;
using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{
    [DisplayName("搜索文本框")]
    [Description("搜索文本框...")]
    public partial class WDTextBoxSearch : WDTextBoxEx
    {
        public WDTextBoxSearch()
        {
            base.Font = WDFonts.TextFont;
            base.ConerRadius = 20;
            base.BackColor = Color.Transparent;
            base.FillColor = WDColors.WhiteColor;
            //base.FocusBorderColor = Color.Gainsboro;
            base.PromptColor = Color.Gray;
            base.PromptFont = WDFonts.TextFont;
            base.PromptText = "请输入ID或姓名";
            base.RectColor = WDColors.GrayRectColor;
            this.Size = new Size(206, 32);
            base.Margin = new System.Windows.Forms.Padding(0);
            base.Padding = new System.Windows.Forms.Padding(8, 5, 5, 5);
            base.IsShowSearchBtn = true;
            base.IsShowShadow = true;
            base.IsShowClearBtn = true;
            base.IsShowKeyboard = false;
            base.IsFocusColor = false;
        }
    }
}
