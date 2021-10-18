namespace MIP.UI.Main
{
    partial class frmClientLoginManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientLoginManage));
            this.ucBtnBack = new MIP.UI.Utilities.Controls.UCBtnImg2Words();
            this.btnSave = new MIP.UI.Utilities.Controls.UCBtnImg2WordsYS();
            this.ucTxtDep = new MIP.UI.Utilities.Controls.UCLabelTextBox();
            this.ucTxtWorkStationName = new MIP.UI.Utilities.Controls.UCLabelTextBox();
            this.ucTxtWorkStationAddress = new MIP.UI.Utilities.Controls.UCLabelTextBox();
            this.ucTxtTel = new MIP.UI.Utilities.Controls.UCLabelTextBox();
            this.ucTxtUser = new MIP.UI.Utilities.Controls.UCLabelTextBox();
            this.ucLabelIP = new MIP.UI.Utilities.Controls.UCLabelLabel();
            this.ucLabelMAC = new MIP.UI.Utilities.Controls.UCLabelLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSysName
            // 
            this.lblSysName.Size = new System.Drawing.Size(65, 20);
            this.lblSysName.Text = "注册信息";
            // 
            // ucBtnBack
            // 
            this.ucBtnBack.BackColor = System.Drawing.Color.Transparent;
            this.ucBtnBack.BtnBackColor = System.Drawing.Color.Transparent;
            this.ucBtnBack.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnBack.BtnForeColor = System.Drawing.Color.Black;
            this.ucBtnBack.BtnText = "取消";
            this.ucBtnBack.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnBack.ConerRadius = 5;
            this.ucBtnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnBack.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ucBtnBack.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnBack.IconName = "I_close";
            this.ucBtnBack.Image = ((System.Drawing.Image)(resources.GetObject("ucBtnBack.Image")));
            this.ucBtnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ucBtnBack.ImageFontIcons = null;
            this.ucBtnBack.IsLink = false;
            this.ucBtnBack.IsRadius = true;
            this.ucBtnBack.IsShowRect = true;
            this.ucBtnBack.IsShowShadow = true;
            this.ucBtnBack.IsShowTips = false;
            this.ucBtnBack.Location = new System.Drawing.Point(194, 358);
            this.ucBtnBack.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnBack.Name = "ucBtnBack";
            this.ucBtnBack.Padding = new System.Windows.Forms.Padding(8, 0, 7, 0);
            this.ucBtnBack.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ucBtnBack.RectWidth = 1;
            this.ucBtnBack.Size = new System.Drawing.Size(72, 32);
            this.ucBtnBack.TabIndex = 10;
            this.ucBtnBack.TabStop = false;
            this.ucBtnBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnBack.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnBack.TipsText = "";
            this.ucBtnBack.UseHoverColor = false;
            this.ucBtnBack.BtnClick += new System.EventHandler(this.ucBtnBack_BtnClick);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BtnBackColor = System.Drawing.Color.Transparent;
            this.btnSave.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnSave.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSave.BtnText = "保存";
            this.btnSave.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.ConerRadius = 5;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(101)))), ((int)(((byte)(129)))));
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnSave.IconName = "I_save";
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.ImageFontIcons = null;
            this.btnSave.IsLink = false;
            this.btnSave.IsRadius = true;
            this.btnSave.IsShowRect = true;
            this.btnSave.IsShowShadow = true;
            this.btnSave.IsShowTips = false;
            this.btnSave.Location = new System.Drawing.Point(112, 358);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(8, 0, 7, 0);
            this.btnSave.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(101)))), ((int)(((byte)(129)))));
            this.btnSave.RectWidth = 1;
            this.btnSave.Size = new System.Drawing.Size(72, 32);
            this.btnSave.TabIndex = 11;
            this.btnSave.TabStop = false;
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnSave.TipsText = "";
            this.btnSave.UseHoverColor = false;
            this.btnSave.BtnClick += new System.EventHandler(this.btnSave_BtnClick);
            // 
            // ucTxtDep
            // 
            this.ucTxtDep.CtrlsSpace = 10;
            this.ucTxtDep.FirstCtrlWidth = 105;
            this.ucTxtDep.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucTxtDep.LabelCtrlsSpace = 10;
            this.ucTxtDep.LabelText = "科室：";
            this.ucTxtDep.LabelWidth = 105;
            this.ucTxtDep.Location = new System.Drawing.Point(24, 137);
            this.ucTxtDep.Name = "ucTxtDep";
            this.ucTxtDep.Padding = new System.Windows.Forms.Padding(1);
            this.ucTxtDep.ReadOnly = false;
            this.ucTxtDep.Size = new System.Drawing.Size(301, 32);
            this.ucTxtDep.TabIndex = 13;
            this.ucTxtDep.TabStop = true;
            // 
            // ucTxtWorkStationName
            // 
            this.ucTxtWorkStationName.CtrlsSpace = 10;
            this.ucTxtWorkStationName.FirstCtrlWidth = 105;
            this.ucTxtWorkStationName.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTxtWorkStationName.LabelCtrlsSpace = 10;
            this.ucTxtWorkStationName.LabelText = "工作站名称：";
            this.ucTxtWorkStationName.LabelWidth = 105;
            this.ucTxtWorkStationName.Location = new System.Drawing.Point(24, 175);
            this.ucTxtWorkStationName.Name = "ucTxtWorkStationName";
            this.ucTxtWorkStationName.Padding = new System.Windows.Forms.Padding(1);
            this.ucTxtWorkStationName.ReadOnly = false;
            this.ucTxtWorkStationName.Size = new System.Drawing.Size(301, 32);
            this.ucTxtWorkStationName.TabIndex = 15;
            this.ucTxtWorkStationName.TabStop = true;
            // 
            // ucTxtWorkStationAddress
            // 
            this.ucTxtWorkStationAddress.CtrlsSpace = 10;
            this.ucTxtWorkStationAddress.FirstCtrlWidth = 100;
            this.ucTxtWorkStationAddress.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTxtWorkStationAddress.IsRequired = false;
            this.ucTxtWorkStationAddress.LabelCtrlsSpace = 10;
            this.ucTxtWorkStationAddress.LabelText = "工作站地址：";
            this.ucTxtWorkStationAddress.Location = new System.Drawing.Point(29, 213);
            this.ucTxtWorkStationAddress.Name = "ucTxtWorkStationAddress";
            this.ucTxtWorkStationAddress.Padding = new System.Windows.Forms.Padding(1);
            this.ucTxtWorkStationAddress.ReadOnly = false;
            this.ucTxtWorkStationAddress.Size = new System.Drawing.Size(296, 32);
            this.ucTxtWorkStationAddress.TabIndex = 16;
            this.ucTxtWorkStationAddress.TabStop = true;
            // 
            // ucTxtTel
            // 
            this.ucTxtTel.CtrlsSpace = 10;
            this.ucTxtTel.FirstCtrlWidth = 100;
            this.ucTxtTel.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTxtTel.IsRequired = false;
            this.ucTxtTel.LabelCtrlsSpace = 10;
            this.ucTxtTel.LabelText = "电话：";
            this.ucTxtTel.Location = new System.Drawing.Point(29, 251);
            this.ucTxtTel.Name = "ucTxtTel";
            this.ucTxtTel.Padding = new System.Windows.Forms.Padding(1);
            this.ucTxtTel.ReadOnly = false;
            this.ucTxtTel.Size = new System.Drawing.Size(296, 32);
            this.ucTxtTel.TabIndex = 16;
            this.ucTxtTel.TabStop = true;
            // 
            // ucTxtUser
            // 
            this.ucTxtUser.CtrlsSpace = 10;
            this.ucTxtUser.FirstCtrlWidth = 100;
            this.ucTxtUser.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucTxtUser.IsRequired = false;
            this.ucTxtUser.LabelCtrlsSpace = 10;
            this.ucTxtUser.LabelText = "使用人：";
            this.ucTxtUser.Location = new System.Drawing.Point(29, 289);
            this.ucTxtUser.Name = "ucTxtUser";
            this.ucTxtUser.Padding = new System.Windows.Forms.Padding(1);
            this.ucTxtUser.ReadOnly = false;
            this.ucTxtUser.Size = new System.Drawing.Size(296, 32);
            this.ucTxtUser.TabIndex = 17;
            this.ucTxtUser.TabStop = true;
            // 
            // ucLabelIP
            // 
            this.ucLabelIP.CtrlsSpace = 0;
            this.ucLabelIP.FirstCtrlWidth = 0;
            this.ucLabelIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucLabelIP.IsRequired = false;
            this.ucLabelIP.LabelCtrlsSpace = 0;
            this.ucLabelIP.LabelText = "";
            this.ucLabelIP.LabelWidth = 0;
            this.ucLabelIP.Location = new System.Drawing.Point(128, 66);
            this.ucLabelIP.Name = "ucLabelIP";
            this.ucLabelIP.Padding = new System.Windows.Forms.Padding(1);
            this.ucLabelIP.ReadOnly = false;
            this.ucLabelIP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ucLabelIP.Size = new System.Drawing.Size(197, 32);
            this.ucLabelIP.TabIndex = 18;
            this.ucLabelIP.TabStop = true;
            // 
            // ucLabelMAC
            // 
            this.ucLabelMAC.CtrlsSpace = 0;
            this.ucLabelMAC.FirstCtrlWidth = 0;
            this.ucLabelMAC.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucLabelMAC.IsRequired = false;
            this.ucLabelMAC.LabelCtrlsSpace = 0;
            this.ucLabelMAC.LabelText = "MAC地址：";
            this.ucLabelMAC.LabelWidth = 0;
            this.ucLabelMAC.Location = new System.Drawing.Point(128, 99);
            this.ucLabelMAC.Name = "ucLabelMAC";
            this.ucLabelMAC.Padding = new System.Windows.Forms.Padding(1);
            this.ucLabelMAC.ReadOnly = false;
            this.ucLabelMAC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ucLabelMAC.Size = new System.Drawing.Size(197, 32);
            this.ucLabelMAC.TabIndex = 19;
            this.ucLabelMAC.TabStop = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(95, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 19);
            this.label1.TabIndex = 20;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(47, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 19);
            this.label2.TabIndex = 21;
            this.label2.Text = "MAC地址:";
            // 
            // frmClientLoginManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.BorderStyleSize = 1;
            this.ClientSize = new System.Drawing.Size(364, 423);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucLabelMAC);
            this.Controls.Add(this.ucLabelIP);
            this.Controls.Add(this.ucTxtUser);
            this.Controls.Add(this.ucTxtTel);
            this.Controls.Add(this.ucTxtWorkStationAddress);
            this.Controls.Add(this.ucTxtWorkStationName);
            this.Controls.Add(this.ucTxtDep);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ucBtnBack);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "frmClientLoginManage";
            this.Text = "";
            this.Load += new System.EventHandler(this.frmClientLoginManage_Load);
            this.Controls.SetChildIndex(this.ucBtnBack, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.ucTxtDep, 0);
            this.Controls.SetChildIndex(this.ucTxtWorkStationName, 0);
            this.Controls.SetChildIndex(this.ucTxtWorkStationAddress, 0);
            this.Controls.SetChildIndex(this.ucTxtTel, 0);
            this.Controls.SetChildIndex(this.ucTxtUser, 0);
            this.Controls.SetChildIndex(this.ucLabelIP, 0);
            this.Controls.SetChildIndex(this.ucLabelMAC, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);

        }

        #endregion
        private Utilities.Controls.UCBtnImg2Words ucBtnBack;
        private Utilities.Controls.UCBtnImg2WordsYS btnSave;
        private Utilities.Controls.UCLabelTextBox ucTxtDep;
        private Utilities.Controls.UCLabelTextBox ucTxtWorkStationName;
        private Utilities.Controls.UCLabelTextBox ucTxtWorkStationAddress;
        private Utilities.Controls.UCLabelTextBox ucTxtTel;
        private Utilities.Controls.UCLabelTextBox ucTxtUser;
        private Utilities.Controls.UCLabelLabel ucLabelIP;
        private Utilities.Controls.UCLabelLabel ucLabelMAC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}