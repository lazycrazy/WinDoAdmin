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
    public class WDTextBoxClear : WDTextBoxEx
    {
        public WDTextBoxClear()
        {
            base.ConerRadius = 1;
            base.BackColor = Color.Transparent;
            base.FillColor = WDColors.WhiteColor;
            //base.FocusBorderColor = Color.Gainsboro;
            base.PromptColor = Color.Gray;
            base.PromptFont = WDFonts.TextFont;
            base.PromptText = "";
            base.RectColor = WDColors.GrayRectColor;
            this.Size = new Size(206, 32);
            base.Margin = new System.Windows.Forms.Padding(0);
            base.Padding = new System.Windows.Forms.Padding(0); //new System.Windows.Forms.Padding(8, 5, 5, 5);
            base.IsShowSearchBtn = false;
            base.IsShowShadow = false;
            base.IsShowClearBtn = false;
            base.IsShowKeyboard = false;
            //txtInput.Font = WinDoFonts.TextFont;           
            base.txtInput.WordWrap = true;
            //base.IsFocusColor = false;
            base.IsShowRect = true;
            //base.txtInput.Multiline = true;
        }


        [Description("多行文本"), Category("自定义"), DefaultValue(false)]
        public bool MultiLine
        {
            get { return base.txtInput.Multiline; }
            set
            {
                base.txtInput.Multiline = value;

            }
        }


        public int MaxLength
        {
            get { return base.txtInput.MaxLength; }
            set { base.txtInput.MaxLength = value; }
        }

    }


    public class UCTextBoxRaw : WDCtrlBase
    {
        public UCTextBoxRaw()
        {
            base.IsShowRect = true;
            base.IsRadius = true;
            base.ConerRadius = 1;
            txtInput = new System.Windows.Forms.TextBox();
            txtInput.Width = this.Width-2;
            txtInput.Top = (this.Height - txtInput.Height) / 2;
            txtInput.Left = 1;
            txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtInput.Font = WDFonts.TextFont;
            txtInput.BackColor = Color.White;
            txtInput.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.Controls.Add(txtInput);
            this.SizeChanged += UCTextBoxRaw_SizeChanged;
        }
        private bool isErrorColor = false;

        [Description("是否为错误状态"), Category("自定义")]
        public bool IsErrorColor
        {
            get { return isErrorColor; }
            set
            {
                isErrorColor = value;
                this.RectColor = value ? WDColors.ErrorRedColor : defaultRectColor;
            }
        }
        public string InputText
        {
            get
            {
                return txtInput.Text;
            }
            set
            {
                txtInput.Text = value;
            }
        }
        Color defaultRectColor = WDColors.GrayRectColor;
        private void UCTextBoxRaw_SizeChanged(object sender, EventArgs e)
        {
            txtInput.Top = (this.Height - txtInput.Height) / 2;
        }

        public System.Windows.Forms.TextBox txtInput;

    }
}
