namespace WinDoControls.Forms
{
    partial class frmWaiting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWaiting));
            this.ucLoadProgressExt1 = new WinDoControls.Controls.UCLoadProgressExt();
            this.SuspendLayout();
            // 
            // ucLoadProgressExt1
            // 
            this.ucLoadProgressExt1.Active = true;
            this.ucLoadProgressExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLoadProgressExt1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucLoadProgressExt1.ForeColor = System.Drawing.Color.Black;
            this.ucLoadProgressExt1.Location = new System.Drawing.Point(0, 0);
            this.ucLoadProgressExt1.Name = "ucLoadProgressExt1";
            this.ucLoadProgressExt1.Size = new System.Drawing.Size(40, 40);
            this.ucLoadProgressExt1.TabIndex = 1;
            this.ucLoadProgressExt1.Text = "ucLoadProgressExt2";
            this.ucLoadProgressExt1.ThicknessColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(43)))), ((int)(((byte)(78)))));
            // 
            // frmWaiting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(40, 40);
            this.ControlBox = false;
            this.Controls.Add(this.ucLoadProgressExt1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1, 1);
            this.Name = "frmWaiting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmWait";
            this.ResumeLayout(false);

        }

        #endregion

        public WinDoControls.Controls.UCLoadProgressExt ucLoadProgressExt1;
    }
}