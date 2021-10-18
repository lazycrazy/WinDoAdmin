using WinDoControls.Controls;

namespace WinDo.UI.Manage
{
    partial class frmClientConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientConfig));
            this.lblIP = new WDLabelLabel();
            this.lblMAC = new WDLabelLabel();
            this.txtName = new WDLabelTextBox();
            this.txtUserName = new WDLabelTextBox();
            this.txtPhone = new WDLabelTextBox();
            this.txtExitTime = new WDLabelTextBox();
            this.ucLabelLabel1 = new WDLabelLabel();
            this.lblversion = new WDLabelLabel();
            this.lbltime = new WDLabelLabel();
            this.rdoModel = new WDLabelSwitch();
            this.lblHomePage = new WDLabelLabel();
            this.panelBottom.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(428, 40);
            // 
            // panelBottom
            // 
            this.panelBottom.Location = new System.Drawing.Point(1, 462);
            this.panelBottom.Size = new System.Drawing.Size(428, 64);
            // 
            // panelTitle
            // 
            this.panelTitle.Location = new System.Drawing.Point(1, 2);
            this.panelTitle.Size = new System.Drawing.Size(428, 40);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.btnCancel.BtnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.btnCancel.Location = new System.Drawing.Point(218, 16);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.btnOK.BtnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblIP.CtrlsSpace = 5;
            this.lblIP.FirstCtrlWidth = 165;
            this.lblIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIP.IsRequired = false;
            this.lblIP.LabelCtrlsSpace = 5;
            this.lblIP.LabelText = "IP：";
            this.lblIP.LabelWidth = 165;
            this.lblIP.Location = new System.Drawing.Point(40, 50);
            this.lblIP.Name = "lblIP";
            this.lblIP.Padding = new System.Windows.Forms.Padding(1);
            this.lblIP.ReadOnly = false;
            this.lblIP.Size = new System.Drawing.Size(350, 36);
            this.lblIP.TabIndex = 6;
            this.lblIP.TabStop = true;
            // 
            // lblMAC
            // 
            this.lblMAC.AutoSize = true;
            this.lblMAC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblMAC.CtrlsSpace = 5;
            this.lblMAC.FirstCtrlWidth = 165;
            this.lblMAC.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMAC.IsRequired = false;
            this.lblMAC.LabelCtrlsSpace = 5;
            this.lblMAC.LabelText = "MAC地址：";
            this.lblMAC.LabelWidth = 165;
            this.lblMAC.Location = new System.Drawing.Point(40, 90);
            this.lblMAC.Name = "lblMAC";
            this.lblMAC.Padding = new System.Windows.Forms.Padding(1);
            this.lblMAC.ReadOnly = false;
            this.lblMAC.Size = new System.Drawing.Size(350, 36);
            this.lblMAC.TabIndex = 7;
            this.lblMAC.TabStop = true;
            // 
            // txtName
            // 
            this.txtName.AutoSize = true;
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.txtName.CtrlsSpace = 5;
            this.txtName.FirstCtrlWidth = 165;
            this.txtName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.IsErrorColor = false;
            this.txtName.LabelCtrlsSpace = 5;
            this.txtName.LabelText = "客户端名称：";
            this.txtName.LabelWidth = 165;
            this.txtName.Location = new System.Drawing.Point(40, 130);
            this.txtName.Name = "txtName";
            this.txtName.Padding = new System.Windows.Forms.Padding(1);
            this.txtName.ReadOnly = false;
            this.txtName.Size = new System.Drawing.Size(350, 36);
            this.txtName.TabIndex = 0;
            this.txtName.TabStop = true;
            // 
            // txtUserName
            // 
            this.txtUserName.AutoSize = true;
            this.txtUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.txtUserName.CtrlsSpace = 5;
            this.txtUserName.FirstCtrlWidth = 165;
            this.txtUserName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserName.IsErrorColor = false;
            this.txtUserName.IsRequired = false;
            this.txtUserName.LabelCtrlsSpace = 5;
            this.txtUserName.LabelText = "使用人：";
            this.txtUserName.LabelWidth = 165;
            this.txtUserName.Location = new System.Drawing.Point(40, 170);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Padding = new System.Windows.Forms.Padding(1);
            this.txtUserName.ReadOnly = false;
            this.txtUserName.Size = new System.Drawing.Size(350, 36);
            this.txtUserName.TabIndex = 9;
            this.txtUserName.TabStop = true;
            // 
            // txtPhone
            // 
            this.txtPhone.AutoSize = true;
            this.txtPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.txtPhone.CtrlsSpace = 5;
            this.txtPhone.FirstCtrlWidth = 165;
            this.txtPhone.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPhone.IsErrorColor = false;
            this.txtPhone.IsRequired = false;
            this.txtPhone.LabelCtrlsSpace = 5;
            this.txtPhone.LabelText = "联系电话：";
            this.txtPhone.LabelWidth = 165;
            this.txtPhone.Location = new System.Drawing.Point(40, 210);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Padding = new System.Windows.Forms.Padding(1);
            this.txtPhone.ReadOnly = false;
            this.txtPhone.Size = new System.Drawing.Size(350, 36);
            this.txtPhone.TabIndex = 10;
            this.txtPhone.TabStop = true;
            // 
            // txtExitTime
            // 
            this.txtExitTime.AutoSize = true;
            this.txtExitTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.txtExitTime.CtrlsSpace = 5;
            this.txtExitTime.FirstCtrlWidth = 165;
            this.txtExitTime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtExitTime.IsErrorColor = false;
            this.txtExitTime.LabelCtrlsSpace = 5;
            this.txtExitTime.LabelText = "客户端自动退出时间：";
            this.txtExitTime.LabelWidth = 165;
            this.txtExitTime.Location = new System.Drawing.Point(40, 250);
            this.txtExitTime.Name = "txtExitTime";
            this.txtExitTime.Padding = new System.Windows.Forms.Padding(1);
            this.txtExitTime.ReadOnly = false;
            this.txtExitTime.Size = new System.Drawing.Size(250, 36);
            this.txtExitTime.TabIndex = 11;
            this.txtExitTime.TabStop = true;
            // 
            // ucLabelLabel1
            // 
            this.ucLabelLabel1.AutoSize = true;
            this.ucLabelLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.ucLabelLabel1.CtrlsSpace = 5;
            this.ucLabelLabel1.FirstCtrlWidth = 40;
            this.ucLabelLabel1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucLabelLabel1.IsRequired = false;
            this.ucLabelLabel1.LabelCtrlsSpace = 5;
            this.ucLabelLabel1.LabelText = "分钟";
            this.ucLabelLabel1.LabelWidth = 40;
            this.ucLabelLabel1.Location = new System.Drawing.Point(297, 250);
            this.ucLabelLabel1.Name = "ucLabelLabel1";
            this.ucLabelLabel1.Padding = new System.Windows.Forms.Padding(1);
            this.ucLabelLabel1.ReadOnly = false;
            this.ucLabelLabel1.Size = new System.Drawing.Size(45, 36);
            this.ucLabelLabel1.TabIndex = 14;
            this.ucLabelLabel1.TabStop = true;
            // 
            // lblversion
            // 
            this.lblversion.AutoSize = true;
            this.lblversion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblversion.CtrlsSpace = 5;
            this.lblversion.FirstCtrlWidth = 165;
            this.lblversion.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblversion.IsRequired = false;
            this.lblversion.LabelCtrlsSpace = 5;
            this.lblversion.LabelText = "当前版本：";
            this.lblversion.LabelWidth = 165;
            this.lblversion.Location = new System.Drawing.Point(40, 370);
            this.lblversion.Name = "lblversion";
            this.lblversion.Padding = new System.Windows.Forms.Padding(1);
            this.lblversion.ReadOnly = false;
            this.lblversion.Size = new System.Drawing.Size(350, 36);
            this.lblversion.TabIndex = 16;
            this.lblversion.TabStop = true;
            // 
            // lbltime
            // 
            this.lbltime.AutoSize = true;
            this.lbltime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lbltime.CtrlsSpace = 5;
            this.lbltime.FirstCtrlWidth = 165;
            this.lbltime.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltime.IsRequired = false;
            this.lbltime.LabelCtrlsSpace = 5;
            this.lbltime.LabelText = "升级时间：";
            this.lbltime.LabelWidth = 165;
            this.lbltime.Location = new System.Drawing.Point(40, 410);
            this.lbltime.Name = "lbltime";
            this.lbltime.Padding = new System.Windows.Forms.Padding(1);
            this.lbltime.ReadOnly = false;
            this.lbltime.Size = new System.Drawing.Size(350, 36);
            this.lbltime.TabIndex = 17;
            this.lbltime.TabStop = true;
            // 
            // rdoModel
            // 
            this.rdoModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.rdoModel.CtrlsSpace = 10;
            this.rdoModel.FirstCtrlWidth = 165;
            this.rdoModel.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.rdoModel.LabelCtrlsSpace = 10;
            this.rdoModel.LabelText = "扫描枪模式：";
            this.rdoModel.LabelWidth = 165;
            this.rdoModel.Location = new System.Drawing.Point(40, 296);
            this.rdoModel.Name = "rdoModel";
            this.rdoModel.Padding = new System.Windows.Forms.Padding(1);
            this.rdoModel.ReadOnly = false;
            this.rdoModel.Size = new System.Drawing.Size(216, 22);
            this.rdoModel.TabIndex = 18;
            this.rdoModel.TabStop = true;
            // 
            // lblHomePage
            // 
            this.lblHomePage.AutoSize = true;
            this.lblHomePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblHomePage.CtrlsSpace = 5;
            this.lblHomePage.FirstCtrlWidth = 165;
            this.lblHomePage.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHomePage.IsRequired = false;
            this.lblHomePage.LabelCtrlsSpace = 5;
            this.lblHomePage.LabelText = "默认主页：";
            this.lblHomePage.LabelWidth = 165;
            this.lblHomePage.Location = new System.Drawing.Point(40, 330);
            this.lblHomePage.Name = "lblHomePage";
            this.lblHomePage.Padding = new System.Windows.Forms.Padding(1);
            this.lblHomePage.ReadOnly = false;
            this.lblHomePage.Size = new System.Drawing.Size(350, 36);
            this.lblHomePage.TabIndex = 19;
            this.lblHomePage.TabStop = true;
            // 
            // frmClientConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 528);
            this.Controls.Add(this.lblHomePage);
            this.Controls.Add(this.rdoModel);
            this.Controls.Add(this.lbltime);
            this.Controls.Add(this.lblversion);
            this.Controls.Add(this.ucLabelLabel1);
            this.Controls.Add(this.txtExitTime);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblMAC);
            this.Controls.Add(this.lblIP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsShowRegion = true;
            this.Name = "frmClientConfig";
            this.Text = "frmChangePasswrod";
            this.Controls.SetChildIndex(this.panelBottom, 0);
            this.Controls.SetChildIndex(this.lblIP, 0);
            this.Controls.SetChildIndex(this.lblMAC, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtPhone, 0);
            this.Controls.SetChildIndex(this.txtExitTime, 0);
            this.Controls.SetChildIndex(this.ucLabelLabel1, 0);
            this.Controls.SetChildIndex(this.panelTitle, 0);
            this.Controls.SetChildIndex(this.lblversion, 0);
            this.Controls.SetChildIndex(this.lbltime, 0);
            this.Controls.SetChildIndex(this.rdoModel, 0);
            this.Controls.SetChildIndex(this.lblHomePage, 0);
            this.panelBottom.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WDLabelLabel lblIP;
        private WDLabelLabel lblMAC;
        private WDLabelTextBox txtName;
        private WDLabelTextBox txtUserName;
        private WDLabelTextBox txtPhone;
        private WDLabelTextBox txtExitTime;
        private WDLabelLabel ucLabelLabel1;
        private WDLabelLabel lblversion;
        private WDLabelLabel lbltime;
        private WDLabelSwitch rdoModel;
        private WDLabelLabel lblHomePage;
    }
}