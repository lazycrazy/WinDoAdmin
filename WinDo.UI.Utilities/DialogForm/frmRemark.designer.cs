using WinDoControls.Controls;

namespace WinDo.Control.DialogForm
{
    partial class frmRemark
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRemark));
            this.txtRemark = new WDTextBoxClear();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panelBottom.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(457, 40);
            this.lblTitle.Text = "忽略预警";
            // 
            // panelBottom
            // 
            this.panelBottom.Location = new System.Drawing.Point(1, 191);
            this.panelBottom.Size = new System.Drawing.Size(457, 64);
            // 
            // panelTitle
            // 
            this.panelTitle.Location = new System.Drawing.Point(1, 2);
            this.panelTitle.Size = new System.Drawing.Size(457, 40);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(232, 16);
            this.btnCancel.BtnClick += new System.EventHandler(this.btnCancel_BtnClick);
            // 
            // btnOK
            // 
            this.btnOK.BtnText = "确定";
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(152, 16);
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.Transparent;
            this.txtRemark.ConerRadius = 1;
            this.txtRemark.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRemark.DecLength = 2;
            this.txtRemark.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtRemark.FocusBorderColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.txtRemark.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRemark.InputText = "";
            this.txtRemark.InputType = WinDoControls.TextInputType.NotControl;
            this.txtRemark.IsErrorColor = false;
            this.txtRemark.IsFocusColor = false;
            this.txtRemark.IsRadius = true;
            this.txtRemark.IsShowClearBtn = false;
            this.txtRemark.IsShowKeyboard = false;
            this.txtRemark.IsShowRect = true;
            this.txtRemark.IsShowSearchBtn = false;
            this.txtRemark.IsShowShadow = false;
            this.txtRemark.Location = new System.Drawing.Point(144, 97);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(0);
            this.txtRemark.MaxLength = 256;
            this.txtRemark.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtRemark.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.txtRemark.MultiLine = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.PromptColor = System.Drawing.Color.Gray;
            this.txtRemark.PromptFont = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRemark.PromptText = "";
            this.txtRemark.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.txtRemark.RectWidth = 1;
            this.txtRemark.RegexPattern = "";
            this.txtRemark.Size = new System.Drawing.Size(297, 87);
            this.txtRemark.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label1.Location = new System.Drawing.Point(97, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "备注：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(87, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 21);
            this.label2.TabIndex = 11;
            this.label2.Text = "*";
            this.label2.Visible = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblInfo.Location = new System.Drawing.Point(190, 60);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(79, 19);
            this.lblInfo.TabIndex = 12;
            this.lblInfo.Text = "确认忽略？";
            // 
            // frmRemark
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(459, 257);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRemark";
            this.Text = "frmChangePasswrod";
            this.Controls.SetChildIndex(this.panelBottom, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.panelTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblInfo, 0);
            this.Controls.SetChildIndex(this.txtRemark, 0);
            this.panelBottom.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public WDTextBoxClear txtRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblInfo;
    }
}