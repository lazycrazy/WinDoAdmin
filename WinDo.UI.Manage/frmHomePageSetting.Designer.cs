namespace WinDo.UI.Manage
{
    partial class frmHomePageSetting
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
            this.ucControlBase1 = new WinDoControls.Controls.WDCtrlBase();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelTitle.SuspendLayout();
            this.ucControlBase1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(912, 40);
            // 
            // panelTitle
            // 
            this.panelTitle.Size = new System.Drawing.Size(958, 40);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(483, 16);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(403, 16);
            // 
            // ucControlBase1
            // 
            this.ucControlBase1.BackColor = System.Drawing.Color.Transparent;
            this.ucControlBase1.ConerRadius = 1;
            this.ucControlBase1.Controls.Add(this.dataGridView1);
            this.ucControlBase1.FillColor = System.Drawing.Color.White;
            this.ucControlBase1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase1.IsRadius = true;
            this.ucControlBase1.IsShowRect = true;
            this.ucControlBase1.IsShowShadow = false;
            this.ucControlBase1.Location = new System.Drawing.Point(18, 58);
            this.ucControlBase1.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase1.Name = "ucControlBase1";
            this.ucControlBase1.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase1.RectWidth = 1;
            this.ucControlBase1.Size = new System.Drawing.Size(925, 526);
            this.ucControlBase1.TabIndex = 8;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(1, 1);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(923, 524);
            this.dataGridView1.TabIndex = 0;
            // 
            // frmHomePageSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 650);
            this.Controls.Add(this.ucControlBase1);
            this.Name = "frmHomePageSetting";
            this.Text = "frmHomePageSetting";
            this.Controls.SetChildIndex(this.panelTitle, 0);
            this.Controls.SetChildIndex(this.ucControlBase1, 0);
            this.panelTitle.ResumeLayout(false);
            this.ucControlBase1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WinDoControls.Controls.WDCtrlBase ucControlBase1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}