using WinDoControls.Controls;

namespace WinDo.UI.Manage
{
    partial class frmUserManage
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
            WinDoControls.Controls.WinDoListItem winDoListItem3 = new WinDoControls.Controls.WinDoListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserManage));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucListV_UserManage1 = new WinDoControls.Controls.ActivityTabs();
            this.panelUserList = new System.Windows.Forms.Panel();
            this.ucControlBase1 = new WinDoControls.Controls.WDCtrlBase();
            this.dgvUserList1 = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel2 = new System.Windows.Forms.Panel();
            this.txtLoginName = new WinDoControls.Controls.WDLabelTextBox();
            this.txtRealName = new WinDoControls.Controls.WDLabelTextBox();
            this.combTitle = new WinDoControls.Controls.WDLabelComboBox();
            this.combRole = new WinDoControls.Controls.WDLabelComboBox();
            this.btnQuery = new WinDoControls.Controls.WDBtnImg2WordsYS();
            this.panelMatrix = new System.Windows.Forms.Panel();
            this.ucControlBase2 = new WinDoControls.Controls.WDCtrlBase();
            this.dgvMatrix1 = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.Panel();
            this.txtLoginName1 = new WinDoControls.Controls.WDLabelTextBox();
            this.txtRealName1 = new WinDoControls.Controls.WDLabelTextBox();
            this.combTitle1 = new WinDoControls.Controls.WDLabelComboBox();
            this.combRole1 = new WinDoControls.Controls.WDLabelComboBox();
            this.btnQuery1 = new WinDoControls.Controls.WDBtnImg2WordsYS();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSave = new WinDoControls.Controls.WDBtnImg2WordsYS();
            this.tablessControl1 = new WinDoControls.Controls.WDTablessControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucMyGroupTab1 = new WinDoControls.Controls.WinDoList_4();
            this.panel1.SuspendLayout();
            this.panelUserList.SuspendLayout();
            this.ucControlBase1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList1)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.panelMatrix.SuspendLayout();
            this.ucControlBase2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatrix1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tablessControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.ucListV_UserManage1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panel1.Size = new System.Drawing.Size(1386, 50);
            this.panel1.TabIndex = 0;
            // 
            // ucListV_UserManage1
            // 
            this.ucListV_UserManage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            winDoListItem3.BannerColor = null;
            winDoListItem3.ClientRectangle = new System.Drawing.Rectangle(0, 0, 93, 40);
            winDoListItem3.Code = null;
            winDoListItem3.CurLeftIcon = null;
            winDoListItem3.CurRightIcon = null;
            winDoListItem3.Data = null;
            winDoListItem3.Items = null;
            winDoListItem3.LeftIcon = null;
            winDoListItem3.OverLengthTips = null;
            winDoListItem3.RelationControl = null;
            winDoListItem3.RightIcon = null;
            winDoListItem3.Text = "用户列表";
            winDoListItem3.TipColor = System.Drawing.Color.Empty;
            winDoListItem3.TipText = null;
            this.ucListV_UserManage1.CurrentItem = winDoListItem3;
            this.ucListV_UserManage1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucListV_UserManage1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucListV_UserManage1.HighLightCurrentItem = false;
            this.ucListV_UserManage1.ItemHeight = 40;
            this.ucListV_UserManage1.Location = new System.Drawing.Point(0, 10);
            this.ucListV_UserManage1.Name = "ucListV_UserManage1";
            this.ucListV_UserManage1.Size = new System.Drawing.Size(336, 40);
            this.ucListV_UserManage1.TabIndex = 0;
            this.ucListV_UserManage1.Text = "ucListV_UserManage1";
            // 
            // panelUserList
            // 
            this.panelUserList.Controls.Add(this.ucControlBase1);
            this.panelUserList.Controls.Add(this.flowLayoutPanel2);
            this.panelUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUserList.Location = new System.Drawing.Point(3, 3);
            this.panelUserList.Name = "panelUserList";
            this.panelUserList.Size = new System.Drawing.Size(1372, 706);
            this.panelUserList.TabIndex = 1;
            // 
            // ucControlBase1
            // 
            this.ucControlBase1.BackColor = System.Drawing.Color.Transparent;
            this.ucControlBase1.ConerRadius = 1;
            this.ucControlBase1.Controls.Add(this.dgvUserList1);
            this.ucControlBase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucControlBase1.FillColor = System.Drawing.Color.Transparent;
            this.ucControlBase1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase1.IsRadius = true;
            this.ucControlBase1.IsShowRect = true;
            this.ucControlBase1.IsShowShadow = false;
            this.ucControlBase1.Location = new System.Drawing.Point(0, 47);
            this.ucControlBase1.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase1.Name = "ucControlBase1";
            this.ucControlBase1.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase1.RectWidth = 1;
            this.ucControlBase1.Size = new System.Drawing.Size(1372, 659);
            this.ucControlBase1.TabIndex = 8;
            // 
            // dgvUserList1
            // 
            this.dgvUserList1.BackgroundColor = System.Drawing.Color.White;
            this.dgvUserList1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUserList1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUserList1.Location = new System.Drawing.Point(1, 1);
            this.dgvUserList1.Name = "dgvUserList1";
            this.dgvUserList1.RowTemplate.Height = 23;
            this.dgvUserList1.Size = new System.Drawing.Size(1370, 657);
            this.dgvUserList1.TabIndex = 9;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel2.Controls.Add(this.txtLoginName);
            this.flowLayoutPanel2.Controls.Add(this.txtRealName);
            this.flowLayoutPanel2.Controls.Add(this.combTitle);
            this.flowLayoutPanel2.Controls.Add(this.combRole);
            this.flowLayoutPanel2.Controls.Add(this.btnQuery);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1372, 47);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // txtLoginName
            // 
            this.txtLoginName.BackColor = System.Drawing.Color.Transparent;
            this.txtLoginName.CtrlsSpace = 10;
            this.txtLoginName.FirstCtrlWidth = 65;
            this.txtLoginName.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtLoginName.IsErrorColor = false;
            this.txtLoginName.IsRequired = false;
            this.txtLoginName.LabelCtrlsSpace = 10;
            this.txtLoginName.LabelText = "用户名：";
            this.txtLoginName.LabelWidth = 65;
            this.txtLoginName.Location = new System.Drawing.Point(3, 7);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Padding = new System.Windows.Forms.Padding(1);
            this.txtLoginName.ReadOnly = false;
            this.txtLoginName.Size = new System.Drawing.Size(188, 32);
            this.txtLoginName.TabIndex = 1;
            this.txtLoginName.TabStop = true;
            // 
            // txtRealName
            // 
            this.txtRealName.BackColor = System.Drawing.Color.Transparent;
            this.txtRealName.CtrlsSpace = 10;
            this.txtRealName.FirstCtrlWidth = 55;
            this.txtRealName.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRealName.IsErrorColor = false;
            this.txtRealName.IsRequired = false;
            this.txtRealName.LabelCtrlsSpace = 10;
            this.txtRealName.LabelText = "姓名：";
            this.txtRealName.LabelWidth = 55;
            this.txtRealName.Location = new System.Drawing.Point(197, 7);
            this.txtRealName.Name = "txtRealName";
            this.txtRealName.Padding = new System.Windows.Forms.Padding(1);
            this.txtRealName.ReadOnly = false;
            this.txtRealName.Size = new System.Drawing.Size(200, 32);
            this.txtRealName.TabIndex = 2;
            this.txtRealName.TabStop = true;
            // 
            // combTitle
            // 
            this.combTitle.BackColor = System.Drawing.Color.Transparent;
            this.combTitle.CtrlsSpace = 10;
            this.combTitle.FirstCtrlWidth = 55;
            this.combTitle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.combTitle.IsRequired = false;
            this.combTitle.LabelCtrlsSpace = 10;
            this.combTitle.LabelText = "职务：";
            this.combTitle.LabelWidth = 55;
            this.combTitle.Location = new System.Drawing.Point(403, 7);
            this.combTitle.Name = "combTitle";
            this.combTitle.Padding = new System.Windows.Forms.Padding(1);
            this.combTitle.ReadOnly = false;
            this.combTitle.Size = new System.Drawing.Size(260, 32);
            this.combTitle.TabIndex = 3;
            this.combTitle.TabStop = true;
            // 
            // combRole
            // 
            this.combRole.BackColor = System.Drawing.Color.Transparent;
            this.combRole.CtrlsSpace = 10;
            this.combRole.FirstCtrlWidth = 55;
            this.combRole.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.combRole.IsRequired = false;
            this.combRole.LabelCtrlsSpace = 10;
            this.combRole.LabelText = "角色：";
            this.combRole.LabelWidth = 55;
            this.combRole.Location = new System.Drawing.Point(669, 7);
            this.combRole.Name = "combRole";
            this.combRole.Padding = new System.Windows.Forms.Padding(1);
            this.combRole.ReadOnly = false;
            this.combRole.Size = new System.Drawing.Size(268, 32);
            this.combRole.TabIndex = 4;
            this.combRole.TabStop = true;
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.Transparent;
            this.btnQuery.BtnBackColor = System.Drawing.Color.Transparent;
            this.btnQuery.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnQuery.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnQuery.BtnText = "查询";
            this.btnQuery.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuery.ConerRadius = 1;
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.btnQuery.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnQuery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnQuery.IconName = "I_search";
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuery.ImageFontIcons = null;
            this.btnQuery.IsLink = false;
            this.btnQuery.IsRadius = true;
            this.btnQuery.IsShowRect = true;
            this.btnQuery.IsShowShadow = true;
            this.btnQuery.IsShowTips = false;
            this.btnQuery.Location = new System.Drawing.Point(943, 7);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(0);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Padding = new System.Windows.Forms.Padding(7, 0, 8, 0);
            this.btnQuery.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.btnQuery.RectWidth = 1;
            this.btnQuery.Size = new System.Drawing.Size(72, 32);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.TabStop = false;
            this.btnQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuery.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnQuery.TipsText = "";
            this.btnQuery.UseHoverColor = false;
            this.btnQuery.BtnClick += new System.EventHandler(this.btnQuery_BtnClick);
            // 
            // panelMatrix
            // 
            this.panelMatrix.Controls.Add(this.ucControlBase2);
            this.panelMatrix.Controls.Add(this.flowLayoutPanel1);
            this.panelMatrix.Controls.Add(this.panel3);
            this.panelMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMatrix.Location = new System.Drawing.Point(3, 3);
            this.panelMatrix.Name = "panelMatrix";
            this.panelMatrix.Size = new System.Drawing.Size(1372, 706);
            this.panelMatrix.TabIndex = 2;
            // 
            // ucControlBase2
            // 
            this.ucControlBase2.AutoScroll = true;
            this.ucControlBase2.BackColor = System.Drawing.Color.Transparent;
            this.ucControlBase2.ConerRadius = 1;
            this.ucControlBase2.Controls.Add(this.dgvMatrix1);
            this.ucControlBase2.Controls.Add(this.ucMyGroupTab1);
            this.ucControlBase2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucControlBase2.FillColor = System.Drawing.Color.Transparent;
            this.ucControlBase2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase2.IsRadius = true;
            this.ucControlBase2.IsShowRect = true;
            this.ucControlBase2.IsShowShadow = false;
            this.ucControlBase2.Location = new System.Drawing.Point(0, 44);
            this.ucControlBase2.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase2.Name = "ucControlBase2";
            this.ucControlBase2.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase2.RectWidth = 1;
            this.ucControlBase2.Size = new System.Drawing.Size(1372, 612);
            this.ucControlBase2.TabIndex = 7;
            // 
            // dgvMatrix1
            // 
            this.dgvMatrix1.BackgroundColor = System.Drawing.Color.White;
            this.dgvMatrix1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMatrix1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatrix1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatrix1.Location = new System.Drawing.Point(1, 35);
            this.dgvMatrix1.Name = "dgvMatrix1";
            this.dgvMatrix1.RowTemplate.Height = 23;
            this.dgvMatrix1.Size = new System.Drawing.Size(1370, 576);
            this.dgvMatrix1.TabIndex = 11;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Controls.Add(this.txtLoginName1);
            this.flowLayoutPanel1.Controls.Add(this.txtRealName1);
            this.flowLayoutPanel1.Controls.Add(this.combTitle1);
            this.flowLayoutPanel1.Controls.Add(this.combRole1);
            this.flowLayoutPanel1.Controls.Add(this.btnQuery1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1372, 44);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // txtLoginName1
            // 
            this.txtLoginName1.BackColor = System.Drawing.Color.Transparent;
            this.txtLoginName1.CtrlsSpace = 10;
            this.txtLoginName1.FirstCtrlWidth = 65;
            this.txtLoginName1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtLoginName1.IsErrorColor = false;
            this.txtLoginName1.IsRequired = false;
            this.txtLoginName1.LabelCtrlsSpace = 10;
            this.txtLoginName1.LabelText = "用户名：";
            this.txtLoginName1.LabelWidth = 65;
            this.txtLoginName1.Location = new System.Drawing.Point(3, 6);
            this.txtLoginName1.Name = "txtLoginName1";
            this.txtLoginName1.Padding = new System.Windows.Forms.Padding(1);
            this.txtLoginName1.ReadOnly = false;
            this.txtLoginName1.Size = new System.Drawing.Size(188, 32);
            this.txtLoginName1.TabIndex = 1;
            this.txtLoginName1.TabStop = true;
            // 
            // txtRealName1
            // 
            this.txtRealName1.BackColor = System.Drawing.Color.Transparent;
            this.txtRealName1.CtrlsSpace = 10;
            this.txtRealName1.FirstCtrlWidth = 55;
            this.txtRealName1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRealName1.IsErrorColor = false;
            this.txtRealName1.IsRequired = false;
            this.txtRealName1.LabelCtrlsSpace = 10;
            this.txtRealName1.LabelText = "姓名：";
            this.txtRealName1.LabelWidth = 55;
            this.txtRealName1.Location = new System.Drawing.Point(197, 6);
            this.txtRealName1.Name = "txtRealName1";
            this.txtRealName1.Padding = new System.Windows.Forms.Padding(1);
            this.txtRealName1.ReadOnly = false;
            this.txtRealName1.Size = new System.Drawing.Size(200, 32);
            this.txtRealName1.TabIndex = 2;
            this.txtRealName1.TabStop = true;
            // 
            // combTitle1
            // 
            this.combTitle1.BackColor = System.Drawing.Color.Transparent;
            this.combTitle1.CtrlsSpace = 10;
            this.combTitle1.FirstCtrlWidth = 55;
            this.combTitle1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.combTitle1.IsRequired = false;
            this.combTitle1.LabelCtrlsSpace = 10;
            this.combTitle1.LabelText = "职务：";
            this.combTitle1.LabelWidth = 55;
            this.combTitle1.Location = new System.Drawing.Point(403, 6);
            this.combTitle1.Name = "combTitle1";
            this.combTitle1.Padding = new System.Windows.Forms.Padding(1);
            this.combTitle1.ReadOnly = false;
            this.combTitle1.Size = new System.Drawing.Size(260, 32);
            this.combTitle1.TabIndex = 3;
            this.combTitle1.TabStop = true;
            // 
            // combRole1
            // 
            this.combRole1.BackColor = System.Drawing.Color.Transparent;
            this.combRole1.CtrlsSpace = 10;
            this.combRole1.FirstCtrlWidth = 55;
            this.combRole1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.combRole1.IsRequired = false;
            this.combRole1.LabelCtrlsSpace = 10;
            this.combRole1.LabelText = "角色：";
            this.combRole1.LabelWidth = 55;
            this.combRole1.Location = new System.Drawing.Point(669, 6);
            this.combRole1.Name = "combRole1";
            this.combRole1.Padding = new System.Windows.Forms.Padding(1);
            this.combRole1.ReadOnly = false;
            this.combRole1.Size = new System.Drawing.Size(266, 32);
            this.combRole1.TabIndex = 4;
            this.combRole1.TabStop = true;
            // 
            // btnQuery1
            // 
            this.btnQuery1.BackColor = System.Drawing.Color.Transparent;
            this.btnQuery1.BtnBackColor = System.Drawing.Color.Transparent;
            this.btnQuery1.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnQuery1.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnQuery1.BtnText = "查询";
            this.btnQuery1.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuery1.ConerRadius = 1;
            this.btnQuery1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.btnQuery1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnQuery1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnQuery1.IconName = "I_search";
            this.btnQuery1.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery1.Image")));
            this.btnQuery1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuery1.ImageFontIcons = null;
            this.btnQuery1.IsLink = false;
            this.btnQuery1.IsRadius = true;
            this.btnQuery1.IsShowRect = true;
            this.btnQuery1.IsShowShadow = true;
            this.btnQuery1.IsShowTips = false;
            this.btnQuery1.Location = new System.Drawing.Point(943, 6);
            this.btnQuery1.Margin = new System.Windows.Forms.Padding(0);
            this.btnQuery1.Name = "btnQuery1";
            this.btnQuery1.Padding = new System.Windows.Forms.Padding(7, 0, 8, 0);
            this.btnQuery1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.btnQuery1.RectWidth = 1;
            this.btnQuery1.Size = new System.Drawing.Size(72, 32);
            this.btnQuery1.TabIndex = 5;
            this.btnQuery1.TabStop = false;
            this.btnQuery1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuery1.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnQuery1.TipsText = "";
            this.btnQuery1.UseHoverColor = false;
            this.btnQuery1.BtnClick += new System.EventHandler(this.btnQuery1_BtnClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 656);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1372, 50);
            this.panel3.TabIndex = 9;
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
            this.btnSave.Location = new System.Drawing.Point(650, 9);
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
            // tablessControl1
            // 
            this.tablessControl1.Controls.Add(this.tabPage1);
            this.tablessControl1.Controls.Add(this.tabPage2);
            this.tablessControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablessControl1.Location = new System.Drawing.Point(0, 50);
            this.tablessControl1.Name = "tablessControl1";
            this.tablessControl1.SelectedIndex = 0;
            this.tablessControl1.Size = new System.Drawing.Size(1386, 738);
            this.tablessControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelUserList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1378, 712);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelMatrix);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1378, 712);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucMyGroupTab1
            // 
            this.ucMyGroupTab1.BackColor = System.Drawing.Color.White;
            this.ucMyGroupTab1.CurrentItem = null;
            this.ucMyGroupTab1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucMyGroupTab1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.ucMyGroupTab1.ForeColor = System.Drawing.Color.Black;
            this.ucMyGroupTab1.HighLightCurrentItem = false;
            this.ucMyGroupTab1.ItemHeight = 34;
            this.ucMyGroupTab1.Location = new System.Drawing.Point(1, 1);
            this.ucMyGroupTab1.Name = "ucMyGroupTab1";
            this.ucMyGroupTab1.Size = new System.Drawing.Size(1370, 34);
            this.ucMyGroupTab1.TabIndex = 12;
            this.ucMyGroupTab1.Text = "ucMyGroupTab1";
            // 
            // frmUserManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1386, 788);
            this.Controls.Add(this.tablessControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserManage";
            this.Text = "frmUserManage";
            this.panel1.ResumeLayout(false);
            this.panelUserList.ResumeLayout(false);
            this.ucControlBase1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList1)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.panelMatrix.ResumeLayout(false);
            this.ucControlBase2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatrix1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tablessControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelUserList;
        private System.Windows.Forms.Panel panelMatrix;
        private WDLabelComboBox combTitle1;
        private WDLabelComboBox combRole1;
        private WDLabelTextBox txtRealName1;
        private WDLabelTextBox txtLoginName1;
        private System.Windows.Forms.Panel flowLayoutPanel2;
        private WDLabelTextBox txtLoginName;
        private WDLabelTextBox txtRealName;
        private WDLabelComboBox combTitle;
        private WDLabelComboBox combRole;
        private WDBtnImg2WordsYS btnQuery;
        private System.Windows.Forms.Panel flowLayoutPanel1;
        private WDBtnImg2WordsYS btnQuery1;
        private WinDoControls.Controls.WDCtrlBase ucControlBase1;
        private WinDoControls.Controls.WDCtrlBase ucControlBase2;
        private System.Windows.Forms.Panel panel3;
        private WDBtnImg2WordsYS btnSave;
        private System.Windows.Forms.DataGridView dgvMatrix1;
        private System.Windows.Forms.DataGridView dgvUserList1;
        private WinDoControls.Controls.WDTablessControl tablessControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ActivityTabs ucListV_UserManage1;
        private WinDoList_4 ucMyGroupTab1;
    }
}