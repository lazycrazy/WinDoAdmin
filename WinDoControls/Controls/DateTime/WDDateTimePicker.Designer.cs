namespace WinDoControls.Controls
{
    partial class WDDateTimePicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WDDateTimePicker));
            this.ucBtnImg0Words1 = new WDBtnImg0Words();
            this.SuspendLayout();
            // 
            // ucBtnImg0Words1
            // 
            this.ucBtnImg0Words1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucBtnImg0Words1.BackColor = System.Drawing.Color.Transparent;
            this.ucBtnImg0Words1.BtnBackColor = System.Drawing.Color.Transparent;
            this.ucBtnImg0Words1.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg0Words1.BtnForeColor = System.Drawing.Color.Black;
            this.ucBtnImg0Words1.BtnText = "";
            this.ucBtnImg0Words1.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnImg0Words1.ConerRadius = 1;
            this.ucBtnImg0Words1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImg0Words1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ucBtnImg0Words1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg0Words1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg0Words1.IconName = "I_info";
            this.ucBtnImg0Words1.Image = ((System.Drawing.Image)(resources.GetObject("ucBtnImg0Words1.Image")));
            this.ucBtnImg0Words1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg0Words1.ImageFontIcons = null;
            this.ucBtnImg0Words1.IsLink = false;
            this.ucBtnImg0Words1.IsRadius = true;
            this.ucBtnImg0Words1.IsShowRect = true;
            this.ucBtnImg0Words1.IsShowShadow = false;
            this.ucBtnImg0Words1.IsShowTips = false;
            this.ucBtnImg0Words1.Location = new System.Drawing.Point(235, 0);
            this.ucBtnImg0Words1.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnImg0Words1.Name = "ucBtnImg0Words1";
            this.ucBtnImg0Words1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ucBtnImg0Words1.RectWidth = 1;
            this.ucBtnImg0Words1.Size = new System.Drawing.Size(40, 32);
            this.ucBtnImg0Words1.TabIndex = 1;
            this.ucBtnImg0Words1.TabStop = false;
            this.ucBtnImg0Words1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnImg0Words1.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnImg0Words1.TipsText = "";
            this.ucBtnImg0Words1.UseHoverColor = false;
            // 
            // UCDateTimePicker
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.ConerRadius = 1;
            this.Controls.Add(this.ucBtnImg0Words1);
            this.IsRadius = true;
            this.IsShowRect = true;
            this.Name = "UCDateTimePicker";
            this.Size = new System.Drawing.Size(275, 32);
            this.ResumeLayout(false);

        }

        #endregion

        protected WDBtnImg0Words ucBtnImg0Words1;
    }
}
