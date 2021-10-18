using WinDoControls.Controls;

namespace WinDo.UI.Manage
{
    partial class frmUserDetailInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserDetailInfo));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucControlBase1 = new WinDoControls.Controls.WDCtrlBase();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtLoingName = new WinDoControls.Controls.WDLabelLabel();
            this.txtRealName = new WinDoControls.Controls.WDLabelLabel();
            this.txtSex = new WinDoControls.Controls.WDLabelLabel();
            this.txtStatus = new WinDoControls.Controls.WDLabelLabel();
            this.txtTitle = new WinDoControls.Controls.WDLabelLabel();
            this.combRole = new WinDoControls.Controls.WDLabelComboBox();
            this.combSuperiorDoctor = new WinDoControls.Controls.WDLabelComboxGrid();
            this.combDoctorGroup = new WinDoControls.Controls.WDLabelComboxGrid();
            this.combSuperiorPhysicist = new WinDoControls.Controls.WDLabelCheckComboxGrid();
            this.combPhysicistGroup = new WinDoControls.Controls.WDLabelComboxGrid();
            this.txtPhone = new WinDoControls.Controls.WDLabelTextBox();
            this.txtHomePhone = new WinDoControls.Controls.WDLabelTextBox();
            this.txtRemark = new WinDoControls.Controls.WDLabelMultiLineTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucSplitLine_H1 = new WinDoControls.Controls.WDSplitLine_H();
            this.ucLowPanelQuote1 = new WinDoControls.Controls.UCLowPanelQuote();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReturn = new WinDoControls.Controls.WDBtnImg2Words();
            this.btnSave = new WinDoControls.Controls.WDBtnImg2WordsYS();
            this.panel1.SuspendLayout();
            this.ucControlBase1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucControlBase1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(1173, 583);
            this.panel1.TabIndex = 0;
            // 
            // ucControlBase1
            // 
            this.ucControlBase1.BackColor = System.Drawing.Color.White;
            this.ucControlBase1.ConerRadius = 1;
            this.ucControlBase1.Controls.Add(this.flowLayoutPanel1);
            this.ucControlBase1.Controls.Add(this.panel3);
            this.ucControlBase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucControlBase1.FillColor = System.Drawing.Color.Transparent;
            this.ucControlBase1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase1.IsRadius = true;
            this.ucControlBase1.IsShowRect = true;
            this.ucControlBase1.IsShowShadow = false;
            this.ucControlBase1.Location = new System.Drawing.Point(10, 10);
            this.ucControlBase1.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase1.Name = "ucControlBase1";
            this.ucControlBase1.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase1.RectWidth = 1;
            this.ucControlBase1.Size = new System.Drawing.Size(1153, 563);
            this.ucControlBase1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.txtLoingName);
            this.flowLayoutPanel1.Controls.Add(this.txtRealName);
            this.flowLayoutPanel1.Controls.Add(this.txtStatus);
            this.flowLayoutPanel1.Controls.Add(this.txtSex);
            this.flowLayoutPanel1.Controls.Add(this.txtTitle);
            this.flowLayoutPanel1.Controls.Add(this.combRole);
            this.flowLayoutPanel1.Controls.Add(this.combSuperiorDoctor);
            this.flowLayoutPanel1.Controls.Add(this.combDoctorGroup);
            this.flowLayoutPanel1.Controls.Add(this.combSuperiorPhysicist);
            this.flowLayoutPanel1.Controls.Add(this.combPhysicistGroup);
            this.flowLayoutPanel1.Controls.Add(this.txtPhone);
            this.flowLayoutPanel1.Controls.Add(this.txtHomePhone);
            this.flowLayoutPanel1.Controls.Add(this.txtRemark);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(17, 47);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(691, 624);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // txtLoingName
            // 
            this.txtLoingName.BackColor = System.Drawing.Color.Transparent;
            this.txtLoingName.CtrlsSpace = 10;
            this.txtLoingName.FirstCtrlWidth = 140;
            this.txtLoingName.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtLoingName.LabelCtrlsSpace = 10;
            this.txtLoingName.LabelText = "用户名：";
            this.txtLoingName.LabelWidth = 140;
            this.txtLoingName.Location = new System.Drawing.Point(3, 3);
            this.txtLoingName.Name = "txtLoingName";
            this.txtLoingName.Padding = new System.Windows.Forms.Padding(1);
            this.txtLoingName.ReadOnly = false;
            this.txtLoingName.Size = new System.Drawing.Size(367, 32);
            this.txtLoingName.TabIndex = 0;
            this.txtLoingName.TabStop = true;
            // 
            // txtRealName
            // 
            this.txtRealName.BackColor = System.Drawing.Color.Transparent;
            this.txtRealName.CtrlsSpace = 10;
            this.txtRealName.FirstCtrlWidth = 100;
            this.txtRealName.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRealName.LabelCtrlsSpace = 10;
            this.txtRealName.LabelText = "姓名：";
            this.txtRealName.Location = new System.Drawing.Point(376, 3);
            this.txtRealName.Name = "txtRealName";
            this.txtRealName.Padding = new System.Windows.Forms.Padding(1);
            this.txtRealName.ReadOnly = false;
            this.txtRealName.Size = new System.Drawing.Size(312, 32);
            this.txtRealName.TabIndex = 1;
            this.txtRealName.TabStop = true;
            // 
            // txtSex
            // 
            this.txtSex.BackColor = System.Drawing.Color.Transparent;
            this.txtSex.CtrlsSpace = 10;
            this.txtSex.FirstCtrlWidth = 100;
            this.txtSex.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtSex.IsRequired = false;
            this.txtSex.LabelCtrlsSpace = 10;
            this.txtSex.LabelText = "性别：";
            this.txtSex.Location = new System.Drawing.Point(376, 41);
            this.txtSex.Name = "txtSex";
            this.txtSex.Padding = new System.Windows.Forms.Padding(1);
            this.txtSex.ReadOnly = false;
            this.txtSex.Size = new System.Drawing.Size(312, 32);
            this.txtSex.TabIndex = 3;
            this.txtSex.TabStop = true;
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.Transparent;
            this.txtStatus.CtrlsSpace = 10;
            this.txtStatus.FirstCtrlWidth = 140;
            this.txtStatus.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtStatus.IsRequired = false;
            this.txtStatus.LabelCtrlsSpace = 10;
            this.txtStatus.LabelText = "账户状态：";
            this.txtStatus.LabelWidth = 140;
            this.txtStatus.Location = new System.Drawing.Point(3, 41);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Padding = new System.Windows.Forms.Padding(1);
            this.txtStatus.ReadOnly = false;
            this.txtStatus.Size = new System.Drawing.Size(367, 32);
            this.txtStatus.TabIndex = 4;
            this.txtStatus.TabStop = true;
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.Transparent;
            this.txtTitle.CtrlsSpace = 10;
            this.txtTitle.FirstCtrlWidth = 140;
            this.txtTitle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTitle.LabelCtrlsSpace = 10;
            this.txtTitle.LabelText = "职务：";
            this.txtTitle.LabelWidth = 140;
            this.txtTitle.Location = new System.Drawing.Point(3, 79);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Padding = new System.Windows.Forms.Padding(1);
            this.txtTitle.ReadOnly = false;
            this.txtTitle.Size = new System.Drawing.Size(367, 32);
            this.txtTitle.TabIndex = 5;
            this.txtTitle.TabStop = true;
            // 
            // combRole
            // 
            this.combRole.BackColor = System.Drawing.Color.Transparent;
            this.combRole.CtrlsSpace = 10;
            this.combRole.FirstCtrlWidth = 100;
            this.combRole.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.combRole.LabelCtrlsSpace = 10;
            this.combRole.LabelText = "角色：";
            this.combRole.Location = new System.Drawing.Point(376, 79);
            this.combRole.Name = "combRole";
            this.combRole.Padding = new System.Windows.Forms.Padding(1);
            this.combRole.ReadOnly = false;
            this.combRole.Size = new System.Drawing.Size(312, 32);
            this.combRole.TabIndex = 0;
            this.combRole.TabStop = true;
            // 
            // combSuperiorDoctor
            // 
            this.combSuperiorDoctor.BackColor = System.Drawing.Color.Transparent;
            this.combSuperiorDoctor.CtrlsSpace = 10;
            this.combSuperiorDoctor.FirstCtrlWidth = 140;
            this.combSuperiorDoctor.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.combSuperiorDoctor.IsRequired = false;
            this.combSuperiorDoctor.LabelCtrlsSpace = 10;
            this.combSuperiorDoctor.LabelText = "上级开发：";
            this.combSuperiorDoctor.LabelWidth = 140;
            this.combSuperiorDoctor.Location = new System.Drawing.Point(3, 117);
            this.combSuperiorDoctor.Name = "combSuperiorDoctor";
            this.combSuperiorDoctor.Padding = new System.Windows.Forms.Padding(1);
            this.combSuperiorDoctor.ReadOnly = false;
            this.combSuperiorDoctor.Size = new System.Drawing.Size(367, 32);
            this.combSuperiorDoctor.TabIndex = 1;
            this.combSuperiorDoctor.TabStop = true;
            // 
            // combDoctorGroup
            // 
            this.combDoctorGroup.BackColor = System.Drawing.Color.Transparent;
            this.combDoctorGroup.CtrlsSpace = 10;
            this.combDoctorGroup.FirstCtrlWidth = 100;
            this.combDoctorGroup.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.combDoctorGroup.IsRequired = false;
            this.combDoctorGroup.LabelCtrlsSpace = 10;
            this.combDoctorGroup.LabelText = "开发组：";
            this.combDoctorGroup.Location = new System.Drawing.Point(376, 117);
            this.combDoctorGroup.Name = "combDoctorGroup";
            this.combDoctorGroup.Padding = new System.Windows.Forms.Padding(1);
            this.combDoctorGroup.ReadOnly = false;
            this.combDoctorGroup.Size = new System.Drawing.Size(312, 32);
            this.combDoctorGroup.TabIndex = 2;
            this.combDoctorGroup.TabStop = true;
            // 
            // combSuperiorPhysicist
            // 
            this.combSuperiorPhysicist.BackColor = System.Drawing.Color.Transparent;
            this.combSuperiorPhysicist.CtrlsSpace = 10;
            this.combSuperiorPhysicist.FirstCtrlWidth = 140;
            this.combSuperiorPhysicist.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.combSuperiorPhysicist.IsRequired = false;
            this.combSuperiorPhysicist.LabelCtrlsSpace = 10;
            this.combSuperiorPhysicist.LabelText = "上级产品：";
            this.combSuperiorPhysicist.LabelWidth = 140;
            this.combSuperiorPhysicist.Location = new System.Drawing.Point(3, 155);
            this.combSuperiorPhysicist.Name = "combSuperiorPhysicist";
            this.combSuperiorPhysicist.Padding = new System.Windows.Forms.Padding(1);
            this.combSuperiorPhysicist.ReadOnly = false;
            this.combSuperiorPhysicist.Size = new System.Drawing.Size(367, 32);
            this.combSuperiorPhysicist.TabIndex = 3;
            this.combSuperiorPhysicist.TabStop = true;
            // 
            // combPhysicistGroup
            // 
            this.combPhysicistGroup.BackColor = System.Drawing.Color.Transparent;
            this.combPhysicistGroup.CtrlsSpace = 10;
            this.combPhysicistGroup.FirstCtrlWidth = 100;
            this.combPhysicistGroup.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.combPhysicistGroup.IsRequired = false;
            this.combPhysicistGroup.LabelCtrlsSpace = 10;
            this.combPhysicistGroup.LabelText = "产品组：";
            this.combPhysicistGroup.Location = new System.Drawing.Point(376, 155);
            this.combPhysicistGroup.Name = "combPhysicistGroup";
            this.combPhysicistGroup.Padding = new System.Windows.Forms.Padding(1);
            this.combPhysicistGroup.ReadOnly = false;
            this.combPhysicistGroup.Size = new System.Drawing.Size(312, 32);
            this.combPhysicistGroup.TabIndex = 4;
            this.combPhysicistGroup.TabStop = true;
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.Transparent;
            this.txtPhone.CtrlsSpace = 10;
            this.txtPhone.FirstCtrlWidth = 140;
            this.txtPhone.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPhone.IsErrorColor = false;
            this.txtPhone.LabelCtrlsSpace = 10;
            this.txtPhone.LabelText = "手机：";
            this.txtPhone.LabelWidth = 140;
            this.txtPhone.Location = new System.Drawing.Point(3, 193);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Padding = new System.Windows.Forms.Padding(1);
            this.txtPhone.ReadOnly = false;
            this.txtPhone.Size = new System.Drawing.Size(312, 32);
            this.txtPhone.TabIndex = 10;
            this.txtPhone.TabStop = true;
            // 
            // txtHomePhone
            // 
            this.txtHomePhone.BackColor = System.Drawing.Color.Transparent;
            this.txtHomePhone.CtrlsSpace = 10;
            this.txtHomePhone.FirstCtrlWidth = 140;
            this.txtHomePhone.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtHomePhone.IsErrorColor = false;
            this.txtHomePhone.IsRequired = false;
            this.txtHomePhone.LabelCtrlsSpace = 10;
            this.txtHomePhone.LabelText = "座机：";
            this.txtHomePhone.LabelWidth = 140;
            this.txtHomePhone.Location = new System.Drawing.Point(3, 231);
            this.txtHomePhone.Name = "txtHomePhone";
            this.txtHomePhone.Padding = new System.Windows.Forms.Padding(1);
            this.txtHomePhone.ReadOnly = false;
            this.txtHomePhone.Size = new System.Drawing.Size(685, 32);
            this.txtHomePhone.TabIndex = 11;
            this.txtHomePhone.TabStop = true;
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.Transparent;
            this.txtRemark.CtrlsSpace = 10;
            this.txtRemark.FirstCtrlWidth = 140;
            this.txtRemark.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRemark.IsRequired = false;
            this.txtRemark.LabelCtrlsSpace = 10;
            this.txtRemark.LabelText = "备注：";
            this.txtRemark.LabelWidth = 140;
            this.txtRemark.Location = new System.Drawing.Point(3, 269);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Padding = new System.Windows.Forms.Padding(1);
            this.txtRemark.ReadOnly = false;
            this.txtRemark.Size = new System.Drawing.Size(685, 59);
            this.txtRemark.TabIndex = 12;
            this.txtRemark.TabStop = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ucSplitLine_H1);
            this.panel3.Controls.Add(this.ucLowPanelQuote1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(1, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1151, 41);
            this.panel3.TabIndex = 0;
            // 
            // ucSplitLine_H1
            // 
            this.ucSplitLine_H1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ucSplitLine_H1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucSplitLine_H1.Location = new System.Drawing.Point(0, 40);
            this.ucSplitLine_H1.Name = "ucSplitLine_H1";
            this.ucSplitLine_H1.Size = new System.Drawing.Size(1151, 1);
            this.ucSplitLine_H1.TabIndex = 0;
            this.ucSplitLine_H1.TabStop = false;
            // 
            // ucLowPanelQuote1
            // 
            this.ucLowPanelQuote1.BackColor = System.Drawing.Color.Transparent;
            this.ucLowPanelQuote1.BorderColor = System.Drawing.Color.Transparent;
            this.ucLowPanelQuote1.LeftColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(212)))));
            this.ucLowPanelQuote1.LeftPadding = 5;
            this.ucLowPanelQuote1.Location = new System.Drawing.Point(26, 13);
            this.ucLowPanelQuote1.Margin = new System.Windows.Forms.Padding(0);
            this.ucLowPanelQuote1.Name = "ucLowPanelQuote1";
            this.ucLowPanelQuote1.Size = new System.Drawing.Size(387, 14);
            this.ucLowPanelQuote1.TabIndex = 0;
            this.ucLowPanelQuote1.Title = "用户基本信息";
            this.ucLowPanelQuote1.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(191)))), ((int)(((byte)(213)))));
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnReturn);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 583);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1173, 53);
            this.panel2.TabIndex = 1;
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.BtnBackColor = System.Drawing.Color.Transparent;
            this.btnReturn.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnReturn.BtnForeColor = System.Drawing.Color.Black;
            this.btnReturn.BtnText = "返回";
            this.btnReturn.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReturn.ConerRadius = 1;
            this.btnReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnReturn.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnReturn.IconName = "I_leftarrow_clear";
            this.btnReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.Image")));
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.ImageFontIcons = null;
            this.btnReturn.IsLink = false;
            this.btnReturn.IsRadius = true;
            this.btnReturn.IsShowRect = true;
            this.btnReturn.IsShowShadow = true;
            this.btnReturn.IsShowTips = false;
            this.btnReturn.Location = new System.Drawing.Point(550, 10);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(0);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Padding = new System.Windows.Forms.Padding(7, 0, 8, 0);
            this.btnReturn.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.btnReturn.RectWidth = 1;
            this.btnReturn.Size = new System.Drawing.Size(72, 32);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.TabStop = false;
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReturn.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnReturn.TipsText = "";
            this.btnReturn.UseHoverColor = false;
            this.btnReturn.BtnClick += new System.EventHandler(this.btnReturn_BtnClick);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BtnBackColor = System.Drawing.Color.Transparent;
            this.btnSave.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnSave.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSave.BtnText = "保存";
            this.btnSave.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.ConerRadius = 1;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
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
            this.btnSave.Location = new System.Drawing.Point(470, 10);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 8, 0);
            this.btnSave.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.btnSave.RectWidth = 1;
            this.btnSave.Size = new System.Drawing.Size(72, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.TabStop = false;
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnSave.TipsText = "";
            this.btnSave.UseHoverColor = false;
            this.btnSave.BtnClick += new System.EventHandler(this.btnSave_BtnClick);
            // 
            // frmUserDetailInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1173, 636);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserDetailInfo";
            this.Text = "frmUserManage";
            this.panel1.ResumeLayout(false);
            this.ucControlBase1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private WDCtrlBase ucControlBase1;
        private System.Windows.Forms.Panel panel3;
        private WDSplitLine_H ucSplitLine_H1;
        private UCLowPanelQuote ucLowPanelQuote1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private WDBtnImg2Words btnReturn;
        private WDBtnImg2WordsYS btnSave;
        private WDLabelLabel txtLoingName;
        private WDLabelLabel txtRealName;
        private WDLabelLabel txtSex;
        private WDLabelLabel txtStatus;
        private WDLabelLabel txtTitle;
        private WDLabelComboBox combRole;
        private WDLabelTextBox txtPhone;
        private WDLabelTextBox txtHomePhone;
        private WDLabelMultiLineTextBox txtRemark;
        private WDLabelComboxGrid combSuperiorDoctor;
        private WDLabelComboxGrid combDoctorGroup;
        private WDLabelComboxGrid combPhysicistGroup;
        private WDLabelCheckComboxGrid combSuperiorPhysicist;
    }
}