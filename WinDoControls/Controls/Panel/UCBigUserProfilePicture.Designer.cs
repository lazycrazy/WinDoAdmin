namespace WinDoControls.Controls
{
    partial class UCBigUserProfilePicture
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
            this.panelCamera = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelCamera
            // 
            this.panelCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCamera.Location = new System.Drawing.Point(225, 225);
            this.panelCamera.Name = "panelCamera";
            this.panelCamera.Size = new System.Drawing.Size(32, 32);
            this.panelCamera.TabIndex = 3;
            // 
            // UCBigUserProfilePicture
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelCamera);
            this.Name = "UCBigUserProfilePicture";
            this.Size = new System.Drawing.Size(260, 260);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label panelCamera;
    }
}
