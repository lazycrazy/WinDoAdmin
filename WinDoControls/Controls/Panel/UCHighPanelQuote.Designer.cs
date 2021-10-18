namespace WinDoControls.Controls
{
    partial class UCHighPanelQuote
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
            this.ucPanelQuote1 = new WinDoControls.Controls.UCPanelQuote();
            this.lblt2 = new System.Windows.Forms.Label();
            this.ucSplitLine_H1 = new WinDoControls.Controls.WDSplitLine_H();
            this.lblt1 = new System.Windows.Forms.Label();
            this.ucSplitLine_V1 = new WinDoControls.Controls.WDSplitLine_V();
            this.ucPanelQuote1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucPanelQuote1
            // 
            this.ucPanelQuote1.BorderColor = System.Drawing.Color.Transparent;
            this.ucPanelQuote1.Controls.Add(this.lblt2);
            this.ucPanelQuote1.Controls.Add(this.ucSplitLine_H1);
            this.ucPanelQuote1.Controls.Add(this.lblt1);
            this.ucPanelQuote1.Controls.Add(this.ucSplitLine_V1);
            this.ucPanelQuote1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPanelQuote1.LeftColor = System.Drawing.Color.Red;
            this.ucPanelQuote1.Location = new System.Drawing.Point(0, 0);
            this.ucPanelQuote1.Name = "ucPanelQuote1";
            this.ucPanelQuote1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.ucPanelQuote1.Size = new System.Drawing.Size(373, 78);
            this.ucPanelQuote1.TabIndex = 16;
            // 
            // lblt2
            // 
            this.lblt2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblt2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblt2.Location = new System.Drawing.Point(15, 27);
            this.lblt2.Margin = new System.Windows.Forms.Padding(0);
            this.lblt2.Name = "lblt2";
            this.lblt2.Size = new System.Drawing.Size(358, 26);
            this.lblt2.TabIndex = 0;
            this.lblt2.Text = "标题2";
            this.lblt2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // ucSplitLine_H1
            // 
            this.ucSplitLine_H1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ucSplitLine_H1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucSplitLine_H1.Location = new System.Drawing.Point(15, 26);
            this.ucSplitLine_H1.Name = "ucSplitLine_H1";
            this.ucSplitLine_H1.Size = new System.Drawing.Size(358, 1);
            this.ucSplitLine_H1.TabIndex = 6;
            this.ucSplitLine_H1.TabStop = false;
            // 
            // lblt1
            // 
            this.lblt1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblt1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblt1.Location = new System.Drawing.Point(15, 0);
            this.lblt1.Margin = new System.Windows.Forms.Padding(0);
            this.lblt1.Name = "lblt1";
            this.lblt1.Size = new System.Drawing.Size(358, 26);
            this.lblt1.TabIndex = 3;
            this.lblt1.Text = "标题1";
            // 
            // ucSplitLine_V1
            // 
            this.ucSplitLine_V1.BackColor = System.Drawing.Color.Transparent;
            this.ucSplitLine_V1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucSplitLine_V1.Location = new System.Drawing.Point(5, 0);
            this.ucSplitLine_V1.Name = "ucSplitLine_V1";
            this.ucSplitLine_V1.Size = new System.Drawing.Size(10, 78);
            this.ucSplitLine_V1.TabIndex = 4;
            this.ucSplitLine_V1.TabStop = false;
            // 
            // UCHighPanelQuote
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ucPanelQuote1);
            this.Name = "UCHighPanelQuote";
            this.Size = new System.Drawing.Size(373, 78);
            this.ucPanelQuote1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WinDoControls.Controls.UCPanelQuote ucPanelQuote1;
        private System.Windows.Forms.Label lblt1;
        private System.Windows.Forms.Label lblt2;
        private WinDoControls.Controls.WDSplitLine_H ucSplitLine_H1;
        private WinDoControls.Controls.WDSplitLine_V ucSplitLine_V1;
    }
}
