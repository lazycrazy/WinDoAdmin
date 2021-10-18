














namespace WinDoControls.Controls
{
    
    
    
    
    
    partial class UCItemPanel
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
            this.ucControlBase1 = new WinDoControls.Controls.WDCtrlBase();
            this.panMain = new System.Windows.Forms.TableLayoutPanel();
            this.ucControlBase1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucControlBase1
            // 
            this.ucControlBase1.AutoScroll = true;
            this.ucControlBase1.BackColor = System.Drawing.Color.Transparent;
            this.ucControlBase1.ConerRadius = 24;
            this.ucControlBase1.Controls.Add(this.panMain);
            this.ucControlBase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucControlBase1.FillColor = System.Drawing.Color.White;
            this.ucControlBase1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase1.IsRadius = false;
            this.ucControlBase1.IsShowRect = false;
            this.ucControlBase1.IsShowShadow = false;
            this.ucControlBase1.Location = new System.Drawing.Point(1, 1);
            this.ucControlBase1.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase1.Name = "ucControlBase1";
            this.ucControlBase1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase1.RectWidth = 1;
            this.ucControlBase1.Size = new System.Drawing.Size(99, 228);
            this.ucControlBase1.TabIndex = 1;
            // 
            // panMain
            // 
            this.panMain.AutoSize = true;
            this.panMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panMain.ColumnCount = 1;
            this.panMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Name = "panMain";
            this.panMain.RowCount = 1;
            this.panMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panMain.Size = new System.Drawing.Size(99, 0);
            this.panMain.TabIndex = 1;
            // 
            // UCTimePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ConerRadius = 1;
            this.Controls.Add(this.ucControlBase1);
            this.IsRadius = true;
            this.IsShowRect = true;
            this.Name = "UCTimePanel";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(101, 230);
            this.Load += new System.EventHandler(this.UCTimePanel_Load);
            this.ucControlBase1.ResumeLayout(false);
            this.ucControlBase1.PerformLayout();
            this.ResumeLayout(false);

        }











        #endregion

        private WDCtrlBase ucControlBase1;
        private System.Windows.Forms.TableLayoutPanel panMain;
    }
}
