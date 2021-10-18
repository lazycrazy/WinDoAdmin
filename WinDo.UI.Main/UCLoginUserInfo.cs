using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDo.Utilities;
using WinDo.Utilities.PublicResource;
using WinDoControls.Controls;

namespace WinDo.UI.Main
{
    public partial class UCLoginUserInfo : WDDropDownBtn
    {
        public UCLoginUserInfo()
        {
            BackColor = Color.Transparent;
            UseHoverColor = true;
            Paint += new PaintEventHandler(ucDropDownBtn1_Paint);
            DropPanelWidth = 100;
            _isRightExpand = true;
        }


        void ucDropDownBtn1_Paint(object sender, PaintEventArgs e)
        {
            if (leftImage == null) return;
            var textWidth = TextRenderer.MeasureText(this.BtnText, WinDo.Utilities.PublicResource.WDFonts.TextFont).Width;
            e.Graphics.DrawImage(leftImage, ((this.Width - textWidth) / 2) - leftImage.Width - 2, (this.Height - leftImage.Height) / 2, leftImage.Width, leftImage.Height);
        }

        private Image leftImage;
        /// <summary>
        /// 左边图片
        /// </summary>
        /// <value>The image.</value>
        [Description("左边图片"), Category("自定义")]
        public Image LeftImage
        {
            get
            {
                return leftImage;
            }
            set
            {
                leftImage = value;
            }
        }

        /// <summary>
        /// 图片
        /// </summary>
        /// <value>The image.</value>
        [Description("按钮文字"), Category("自定义")]
        public override string BtnText
        {
            get
            {
                return base.BtnText;
            }
            set
            {
                base.BtnText = value + " ";
                var minWidth = TextRenderer.MeasureText("客户端配置", WDFonts.TextFont).Width + 20;
                var txtWidth = TextRenderer.MeasureText(PublicRes.CurUser.RealName, WDFonts.TextFont).Width;
                this.Width = Math.Max(minWidth, txtWidth + 60);
                this.Update();
            }
        }
    }
}
