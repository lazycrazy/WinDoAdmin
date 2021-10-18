using WinDoControls.Controls;

namespace WinDo.UI.Manage
{
    partial class frmUserInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserInfo));
            this.lblName = new WDLabelLabel();
            this.lblUserName = new WDLabelLabel();
            this.lblUserTitle = new WDLabelLabel();
            this.txtPhone = new WDLabelTextBox();
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
            this.panelTitle.Location = new System.Drawing.Point(1, 2);
            this.panelTitle.Size = new System.Drawing.Size(333, 40);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.btnCancel.Location = new System.Drawing.Point(170, 16);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(90, 16);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblName.CtrlsSpace = 5;
            this.lblName.FirstCtrlWidth = 65;
            this.lblName.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblName.IsRequired = false;
            this.lblName.LabelCtrlsSpace = 5;
            this.lblName.LabelText = "姓名：";
            this.lblName.LabelWidth = 65;
            this.lblName.Location = new System.Drawing.Point(39, 51);
            this.lblName.Name = "lblName";
            this.lblName.Padding = new System.Windows.Forms.Padding(1);
            this.lblName.ReadOnly = false;
            this.lblName.Size = new System.Drawing.Size(261, 42);
            this.lblName.TabIndex = 6;
            this.lblName.TabStop = true;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblUserName.CtrlsSpace = 5;
            this.lblUserName.FirstCtrlWidth = 65;
            this.lblUserName.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblUserName.IsRequired = false;
            this.lblUserName.LabelCtrlsSpace = 5;
            this.lblUserName.LabelText = "用户名：";
            this.lblUserName.LabelWidth = 65;
            this.lblUserName.Location = new System.Drawing.Point(39, 101);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Padding = new System.Windows.Forms.Padding(1);
            this.lblUserName.ReadOnly = false;
            this.lblUserName.Size = new System.Drawing.Size(261, 42);
            this.lblUserName.TabIndex = 7;
            this.lblUserName.TabStop = true;
            // 
            // lblUserTitle
            // 
            this.lblUserTitle.AutoSize = true;
            this.lblUserTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblUserTitle.CtrlsSpace = 5;
            this.lblUserTitle.FirstCtrlWidth = 65;
            this.lblUserTitle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblUserTitle.IsRequired = false;
            this.lblUserTitle.LabelCtrlsSpace = 5;
            this.lblUserTitle.LabelText = "职务：";
            this.lblUserTitle.LabelWidth = 65;
            this.lblUserTitle.Location = new System.Drawing.Point(39, 151);
            this.lblUserTitle.Name = "lblUserTitle";
            this.lblUserTitle.Padding = new System.Windows.Forms.Padding(1);
            this.lblUserTitle.ReadOnly = false;
            this.lblUserTitle.Size = new System.Drawing.Size(261, 42);
            this.lblUserTitle.TabIndex = 8;
            this.lblUserTitle.TabStop = true;
            // 
            // txtPhone
            // 
            this.txtPhone.AutoSize = true;
            this.txtPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.txtPhone.CtrlsSpace = 5;
            this.txtPhone.FirstCtrlWidth = 65;
            this.txtPhone.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPhone.IsErrorColor = false;
            this.txtPhone.LabelCtrlsSpace = 5;
            this.txtPhone.LabelText = "手机：";
            this.txtPhone.LabelWidth = 65;
            this.txtPhone.Location = new System.Drawing.Point(39, 201);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Padding = new System.Windows.Forms.Padding(1);
            this.txtPhone.ReadOnly = false;
            this.txtPhone.Size = new System.Drawing.Size(262, 42);
            this.txtPhone.TabIndex = 0;
            this.txtPhone.TabStop = true;
            // 
            // frmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 335);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblUserTitle);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUserInfo";
            this.Text = "frmChangePasswrod";
            this.Controls.SetChildIndex(this.panelBottom, 0);
            this.Controls.SetChildIndex(this.panelTitle, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.lblUserName, 0);
            this.Controls.SetChildIndex(this.lblUserTitle, 0);
            this.Controls.SetChildIndex(this.txtPhone, 0);
            this.panelBottom.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WDLabelLabel lblName;
        private WDLabelLabel lblUserName;
        private WDLabelLabel lblUserTitle;
        private WDLabelTextBox txtPhone;
    }
}