














namespace WinDoControls.Controls
{





    public partial class WDBtnExt2Img
    {
        
        
        
        private System.ComponentModel.IContainer components = null;

        
        
        
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        
        
        
        
        private void InitializeComponent()
        {
            this.lblText = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.BackColor = System.Drawing.Color.Transparent;
            this.lblText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblText.ForeColor = System.Drawing.Color.White;
            this.lblText.Location = new System.Drawing.Point(16, 0);
            this.lblText.Margin = new System.Windows.Forms.Padding(0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(99, 42);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "自定义按钮";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseDown);
            // 
            // lblRight
            // 
            this.lblRight.BackColor = System.Drawing.Color.Transparent;
            this.lblRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRight.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblRight.ForeColor = System.Drawing.Color.Transparent;
            this.lblRight.ImageIndex = 0;
            this.lblRight.Location = new System.Drawing.Point(115, 0);
            this.lblRight.Margin = new System.Windows.Forms.Padding(0);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(16, 42);
            this.lblRight.TabIndex = 1;
            this.lblRight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRight.Visible = false;
            // 
            // lblLeft
            // 
            this.lblLeft.BackColor = System.Drawing.Color.Transparent;
            this.lblLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLeft.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblLeft.ForeColor = System.Drawing.Color.White;
            this.lblLeft.ImageIndex = 0;
            this.lblLeft.Location = new System.Drawing.Point(0, 0);
            this.lblLeft.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(16, 42);
            this.lblLeft.TabIndex = 2;
            this.lblLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLeft.Visible = false;
            // 
            // UCBtnExt2Img
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.ConerRadius = 1;
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lblRight);
            this.Controls.Add(this.lblLeft);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FillColor = System.Drawing.Color.Empty;
            this.IsRadius = true;
            this.IsShowRect = true;
            this.Name = "UCBtnExt2Img";
            this.RectColor = System.Drawing.Color.Empty;
            this.Size = new System.Drawing.Size(131, 42);
            this.ResumeLayout(false);

        }

        #endregion

        
        
        
        public System.Windows.Forms.Label lblText;
        
        
        
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label lblLeft;


    }
}
