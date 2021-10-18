using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Forms;
using System.Drawing;
using WinDo.Utilities.PublicResource;
using WinDo.Utilities;

namespace WinDoControls.Controls
{
    /// <summary>
    /// 固定长度的label
    /// </summary>
    public class LabelDot : Label
    {
        public int MaxLength = 30;
        FrmAnchorTips TipsControl = null;
        private string Tips = "";
        public new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                if (value != null && StringHelper.NumChar(value) > MaxLength)
                {
                    base.Text = StringHelper.CutStringByte(value,MaxLength); 
                    Tips = string.Join<string>("\r\n", StringHelper.Split(value, MaxLength));
                    this.MouseHover += new EventHandler(LabelDot_MouseHover);
                    this.MouseLeave += new EventHandler(LabelDot_MouseLeave);
                }
            }
        }

        void LabelDot_MouseLeave(object sender, EventArgs e)
        {
            if (TipsControl == null) return;
            TipsControl.Close();
            TipsControl = null;
        }

        void LabelDot_MouseHover(object sender, EventArgs e)
        {
            TipsControl = FrmAnchorTips.ShowTips(sender as System.Windows.Forms.Control, Tips, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, alignment: StringAlignment.Center, autoCloseTime: 6000);
        }
    }
}
