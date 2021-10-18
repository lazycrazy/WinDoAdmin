using WinDoControls.Controls;

namespace WinDo.UI.Utilities.DialogForm
{
    partial class UCTextBoxAndBtn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTextBoxAndBtn));
            this.btnOK = new WDBtnImg2WordsYS();
            this.txtInput = new WDTextBoxClear();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BtnBackColor = System.Drawing.Color.Transparent;
            this.btnOK.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnOK.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnOK.BtnText = "查询";
            this.btnOK.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.ConerRadius = 1;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.FillColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnOK.IconName = "I_info";
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.ImageFontIcons = null;
            this.btnOK.IsRadius = true;
            this.btnOK.IsShowRect = true;
            this.btnOK.IsShowShadow = true;
            this.btnOK.IsShowTips = false;
            this.btnOK.Location = new System.Drawing.Point(297, 16);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(10, 0, 6, 0);
            this.btnOK.RectColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.btnOK.RectWidth = 1;
            this.btnOK.Size = new System.Drawing.Size(72, 32);
            this.btnOK.TabIndex = 0;
            this.btnOK.TabStop = false;
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnOK.TipsText = "";
            this.btnOK.UseHoverColor = false;
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.BackColor = System.Drawing.Color.Transparent;
            this.txtInput.ConerRadius = 1;
            this.txtInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtInput.DecLength = 2;
            this.txtInput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtInput.FocusBorderColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.txtInput.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtInput.InputText = "";
            this.txtInput.InputType = WinDoControls.TextInputType.NotControl;
            this.txtInput.IsErrorColor = false;
            this.txtInput.IsFocusColor = false;
            this.txtInput.IsRadius = true;
            this.txtInput.IsShowClearBtn = false;
            this.txtInput.IsShowKeyboard = false;
            this.txtInput.IsShowRect = true;
            this.txtInput.IsShowSearchBtn = false;
            this.txtInput.IsShowShadow = false;
            this.txtInput.Location = new System.Drawing.Point(19, 16);
            this.txtInput.Margin = new System.Windows.Forms.Padding(0);
            this.txtInput.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtInput.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.txtInput.Name = "txtInput";
            this.txtInput.PromptColor = System.Drawing.Color.Gray;
            this.txtInput.PromptFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtInput.PromptText = "";
            this.txtInput.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.txtInput.RectWidth = 1;
            this.txtInput.RegexPattern = "";
            this.txtInput.Size = new System.Drawing.Size(270, 32);
            this.txtInput.TabIndex = 1;
            // 
            // UCTextBoxAndBtn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = WinDo.Utilities.PublicResource.WDColors.SelectedBackColor;
            this.ConerRadius = 1;
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnOK);
            this.IsRadius = true;
            this.IsShowRect = true;
            this.Name = "UCTextBoxAndBtn";
            this.Size = new System.Drawing.Size(388, 64);
            this.ResumeLayout(false);

        }

        #endregion

        private WDBtnImg2WordsYS btnOK;
        private WDTextBoxClear txtInput;
    }
}