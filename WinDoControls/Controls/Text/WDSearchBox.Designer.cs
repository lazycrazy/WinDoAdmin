using WinDoControls.Controls;

namespace WinDoControls.Controls
{
    partial class WDSearchBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WDSearchBox));
            this.ucTextBoxClear1 = new WDTextBoxClear();
            this.ucBtnImg0WordsYS1 = new WDBtnImg0WordsYS();
            this.ucTextBoxClear1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucTextBoxClear1
            // 
            this.ucTextBoxClear1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucTextBoxClear1.BackColor = System.Drawing.Color.Transparent;
            this.ucTextBoxClear1.ConerRadius = 1;
            this.ucTextBoxClear1.Controls.Add(this.ucBtnImg0WordsYS1);
            this.ucTextBoxClear1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ucTextBoxClear1.DecLength = 2;
            this.ucTextBoxClear1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ucTextBoxClear1.FocusBorderColor = System.Drawing.Color.Gainsboro;
            this.ucTextBoxClear1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextBoxClear1.InputText = "";
            this.ucTextBoxClear1.InputType = WinDoControls.TextInputType.NotControl;
            this.ucTextBoxClear1.IsErrorColor = false;
            this.ucTextBoxClear1.IsFocusColor = true;
            this.ucTextBoxClear1.IsRadius = true;
            this.ucTextBoxClear1.IsShowClearBtn = false;
            this.ucTextBoxClear1.IsShowKeyboard = false;
            this.ucTextBoxClear1.IsShowRect = false;
            this.ucTextBoxClear1.IsShowSearchBtn = false;
            this.ucTextBoxClear1.IsShowShadow = false;
            this.ucTextBoxClear1.Location = new System.Drawing.Point(0, 1);
            this.ucTextBoxClear1.Margin = new System.Windows.Forms.Padding(0);
            this.ucTextBoxClear1.MaxLength = 32767;
            this.ucTextBoxClear1.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.ucTextBoxClear1.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.ucTextBoxClear1.Name = "ucTextBoxClear1";
            this.ucTextBoxClear1.Padding = new System.Windows.Forms.Padding(1);
            this.ucTextBoxClear1.PromptColor = System.Drawing.Color.Gray;
            this.ucTextBoxClear1.PromptFont = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextBoxClear1.PromptText = "";
            this.ucTextBoxClear1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ucTextBoxClear1.RectWidth = 1;
            this.ucTextBoxClear1.RegexPattern = "";
            this.ucTextBoxClear1.Size = new System.Drawing.Size(250, 30);
            this.ucTextBoxClear1.TabIndex = 0;
            // 
            // ucBtnImg0WordsYS1
            // 
            this.ucBtnImg0WordsYS1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucBtnImg0WordsYS1.BackColor = System.Drawing.Color.Transparent;
            this.ucBtnImg0WordsYS1.BtnBackColor = System.Drawing.Color.Transparent;
            this.ucBtnImg0WordsYS1.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg0WordsYS1.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ucBtnImg0WordsYS1.BtnText = "";
            this.ucBtnImg0WordsYS1.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnImg0WordsYS1.ConerRadius = 1;
            this.ucBtnImg0WordsYS1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImg0WordsYS1.FillColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.ucBtnImg0WordsYS1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg0WordsYS1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg0WordsYS1.IconName = "I_search";
            this.ucBtnImg0WordsYS1.Image = ((System.Drawing.Image)(resources.GetObject("ucBtnImg0WordsYS1.Image")));
            this.ucBtnImg0WordsYS1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg0WordsYS1.ImageFontIcons = null;
            this.ucBtnImg0WordsYS1.IsLink = false;
            this.ucBtnImg0WordsYS1.IsRadius = false;
            this.ucBtnImg0WordsYS1.IsShowRect = false;
            this.ucBtnImg0WordsYS1.IsShowShadow = false;
            this.ucBtnImg0WordsYS1.IsShowTips = false;
            this.ucBtnImg0WordsYS1.Location = new System.Drawing.Point(209, 0);
            this.ucBtnImg0WordsYS1.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnImg0WordsYS1.Name = "ucBtnImg0WordsYS1";
            this.ucBtnImg0WordsYS1.RectColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.ucBtnImg0WordsYS1.RectWidth = 1;
            this.ucBtnImg0WordsYS1.Size = new System.Drawing.Size(40, 30);
            this.ucBtnImg0WordsYS1.TabIndex = 1;
            this.ucBtnImg0WordsYS1.TabStop = false;
            this.ucBtnImg0WordsYS1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnImg0WordsYS1.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnImg0WordsYS1.TipsText = "";
            this.ucBtnImg0WordsYS1.UseHoverColor = false;
            // 
            // UCSearchBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ConerRadius = 1;
            this.Controls.Add(this.ucTextBoxClear1);
            this.IsRadius = true;
            this.IsShowRect = true;
            this.Name = "UCSearchBox";
            this.Size = new System.Drawing.Size(250, 32);
            this.ucTextBoxClear1.ResumeLayout(false);
            this.ucTextBoxClear1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private WDTextBoxClear ucTextBoxClear1;
        private WDBtnImg0WordsYS ucBtnImg0WordsYS1;
    }
}
