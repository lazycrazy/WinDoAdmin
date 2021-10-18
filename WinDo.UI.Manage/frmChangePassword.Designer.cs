using WinDoControls.Controls;

namespace WinDo.UI.Manage
{
    partial class frmChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePassword));
            this.txtOldPwd = new WDLabelTextBox();
            this.txtNewPwd = new WDLabelTextBox();
            this.txtConfirmPwd = new WDLabelTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelBottom.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(333, 40);
            // 
            // panelBottom
            // 
            this.panelBottom.Location = new System.Drawing.Point(1, 270);
            this.panelBottom.Size = new System.Drawing.Size(333, 64);
            // 
            // panelTitle
            // 
            this.panelTitle.Size = new System.Drawing.Size(333, 40);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(170, 16);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(90, 16);
            // 
            // txtOldPwd
            // 
            this.txtOldPwd.AutoSize = true;
            this.txtOldPwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.txtOldPwd.CtrlsSpace = 5;
            this.txtOldPwd.FirstCtrlWidth = 90;
            this.txtOldPwd.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtOldPwd.IsErrorColor = false;
            this.txtOldPwd.LabelCtrlsSpace = 5;
            this.txtOldPwd.LabelText = "旧密码：";
            this.txtOldPwd.LabelWidth = 90;
            this.txtOldPwd.Location = new System.Drawing.Point(11, 65);
            this.txtOldPwd.Name = "txtOldPwd";
            this.txtOldPwd.Padding = new System.Windows.Forms.Padding(1);
            this.txtOldPwd.ReadOnly = false;
            this.txtOldPwd.Size = new System.Drawing.Size(297, 34);
            this.txtOldPwd.TabIndex = 9;
            // 
            // txtNewPwd
            // 
            this.txtNewPwd.AutoSize = true;
            this.txtNewPwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.txtNewPwd.CtrlsSpace = 5;
            this.txtNewPwd.FirstCtrlWidth = 90;
            this.txtNewPwd.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNewPwd.IsErrorColor = false;
            this.txtNewPwd.LabelCtrlsSpace = 5;
            this.txtNewPwd.LabelText = "新密码：";
            this.txtNewPwd.LabelWidth = 90;
            this.txtNewPwd.Location = new System.Drawing.Point(11, 113);
            this.txtNewPwd.Name = "txtNewPwd";
            this.txtNewPwd.Padding = new System.Windows.Forms.Padding(1);
            this.txtNewPwd.ReadOnly = false;
            this.txtNewPwd.Size = new System.Drawing.Size(297, 34);
            this.txtNewPwd.TabIndex = 10;
            // 
            // txtConfirmPwd
            // 
            this.txtConfirmPwd.AutoSize = true;
            this.txtConfirmPwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.txtConfirmPwd.CtrlsSpace = 5;
            this.txtConfirmPwd.FirstCtrlWidth = 90;
            this.txtConfirmPwd.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtConfirmPwd.IsErrorColor = false;
            this.txtConfirmPwd.LabelCtrlsSpace = 5;
            this.txtConfirmPwd.LabelText = "确定密码：";
            this.txtConfirmPwd.LabelWidth = 90;
            this.txtConfirmPwd.Location = new System.Drawing.Point(11, 205);
            this.txtConfirmPwd.Name = "txtConfirmPwd";
            this.txtConfirmPwd.Padding = new System.Windows.Forms.Padding(1);
            this.txtConfirmPwd.ReadOnly = false;
            this.txtConfirmPwd.Size = new System.Drawing.Size(297, 34);
            this.txtConfirmPwd.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label1.Location = new System.Drawing.Point(110, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 41);
            this.label1.TabIndex = 12;
            this.label1.Text = "新密码只允许输入数字、字母和符号，区分大小写";
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 335);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOldPwd);
            this.Controls.Add(this.txtConfirmPwd);
            this.Controls.Add(this.txtNewPwd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChangePassword";
            this.Text = "frmChangePasswrod";
            this.Controls.SetChildIndex(this.panelTitle, 0);
            this.Controls.SetChildIndex(this.panelBottom, 0);
            this.Controls.SetChildIndex(this.txtNewPwd, 0);
            this.Controls.SetChildIndex(this.txtConfirmPwd, 0);
            this.Controls.SetChildIndex(this.txtOldPwd, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.panelBottom.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WDLabelTextBox txtOldPwd;
        private System.Windows.Forms.Label label1;
        private WDLabelTextBox txtConfirmPwd;
        private WDLabelTextBox txtNewPwd;
    }
}