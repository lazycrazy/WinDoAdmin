
using WinDoControls.Controls;

namespace WinDo.UI.Manage
{
    partial class frmSystemDicEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSystemDicEdit));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucControlBase1 = new WinDoControls.Controls.WDCtrlBase();
            this.ucControlBase2 = new WinDoControls.Controls.WDCtrlBase();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SysDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SysVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SysVal2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SysVal3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucTextBoxExRemark = new WinDoControls.Controls.WDTextBoxClear();
            this.ucControlBase3 = new WinDoControls.Controls.WDCtrlBase();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucLowPanelQuote1 = new WinDoControls.Controls.UCLowPanelQuote();
            this.ucSplitLine_V1 = new WinDoControls.Controls.WDSplitLine_V();
            this.ucBtnImg0Words2 = new WinDoControls.Controls.WDBtnImg0Words();
            this.ucBtnImg0Words3 = new WinDoControls.Controls.WDBtnImg0Words();
            this.ucBtnImg0Words4 = new WinDoControls.Controls.WDBtnImg0Words();
            this.ucBtnImg0Words1 = new WinDoControls.Controls.WDBtnImg0Words();
            this.panel3.SuspendLayout();
            this.ucControlBase1.SuspendLayout();
            this.ucControlBase2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.ucControlBase3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(839, 40);
            this.lblTitle.Text = "编辑字典";
            // 
            // btnCancel
            // 
            this.btnCancel.BtnText = "返回";
            this.btnCancel.IconName = "I_leftarrow_clear";
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(424, 12);
            // 
            // btnOK
            // 
            this.btnOK.BtnText = "确定";
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(342, 12);
            this.btnOK.Size = new System.Drawing.Size(72, 32);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.ucControlBase1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(839, 600);
            this.panel3.TabIndex = 13;
            // 
            // ucControlBase1
            // 
            this.ucControlBase1.BackColor = System.Drawing.Color.Transparent;
            this.ucControlBase1.ConerRadius = 1;
            this.ucControlBase1.Controls.Add(this.ucControlBase2);
            this.ucControlBase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucControlBase1.FillColor = System.Drawing.Color.Transparent;
            this.ucControlBase1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase1.IsRadius = false;
            this.ucControlBase1.IsShowRect = false;
            this.ucControlBase1.IsShowShadow = false;
            this.ucControlBase1.Location = new System.Drawing.Point(0, 0);
            this.ucControlBase1.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase1.Name = "ucControlBase1";
            this.ucControlBase1.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase1.RectWidth = 1;
            this.ucControlBase1.Size = new System.Drawing.Size(839, 600);
            this.ucControlBase1.TabIndex = 3;
            // 
            // ucControlBase2
            // 
            this.ucControlBase2.BackColor = System.Drawing.Color.White;
            this.ucControlBase2.ConerRadius = 24;
            this.ucControlBase2.Controls.Add(this.dataGridView1);
            this.ucControlBase2.Controls.Add(this.panel2);
            this.ucControlBase2.Controls.Add(this.panel1);
            this.ucControlBase2.Controls.Add(this.ucBtnImg0Words2);
            this.ucControlBase2.Controls.Add(this.ucBtnImg0Words3);
            this.ucControlBase2.Controls.Add(this.ucBtnImg0Words4);
            this.ucControlBase2.Controls.Add(this.ucBtnImg0Words1);
            this.ucControlBase2.FillColor = System.Drawing.Color.Transparent;
            this.ucControlBase2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase2.IsRadius = false;
            this.ucControlBase2.IsShowRect = false;
            this.ucControlBase2.IsShowShadow = false;
            this.ucControlBase2.Location = new System.Drawing.Point(18, 16);
            this.ucControlBase2.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase2.Name = "ucControlBase2";
            this.ucControlBase2.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase2.RectWidth = 0;
            this.ucControlBase2.Size = new System.Drawing.Size(802, 580);
            this.ucControlBase2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SysDes,
            this.SysVal,
            this.SysVal2,
            this.SysVal3});
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(151, 89);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(624, 242);
            this.dataGridView1.TabIndex = 29;
            this.dataGridView1.AllowUserToAddRowsChanged += new System.EventHandler(this.ucBtnImg0Words4_BtnClick);
            // 
            // SysDes
            // 
            this.SysDes.DataPropertyName = "SysDes";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SysDes.DefaultCellStyle = dataGridViewCellStyle1;
            this.SysDes.HeaderText = "描述";
            this.SysDes.Name = "SysDes";
            this.SysDes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SysVal
            // 
            this.SysVal.DataPropertyName = "SysVal";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SysVal.DefaultCellStyle = dataGridViewCellStyle2;
            this.SysVal.HeaderText = "值1";
            this.SysVal.Name = "SysVal";
            this.SysVal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SysVal2
            // 
            this.SysVal2.DataPropertyName = "SysVal2";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SysVal2.DefaultCellStyle = dataGridViewCellStyle3;
            this.SysVal2.HeaderText = "值2";
            this.SysVal2.Name = "SysVal2";
            this.SysVal2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SysVal3
            // 
            this.SysVal3.DataPropertyName = "SysVal3";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SysVal3.DefaultCellStyle = dataGridViewCellStyle4;
            this.SysVal3.HeaderText = "值3";
            this.SysVal3.Name = "SysVal3";
            this.SysVal3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucTextBoxExRemark);
            this.panel2.Controls.Add(this.ucControlBase3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(1, 336);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 243);
            this.panel2.TabIndex = 28;
            // 
            // ucTextBoxExRemark
            // 
            this.ucTextBoxExRemark.AutoScroll = true;
            this.ucTextBoxExRemark.BackColor = System.Drawing.Color.Transparent;
            this.ucTextBoxExRemark.ConerRadius = 1;
            this.ucTextBoxExRemark.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ucTextBoxExRemark.DecLength = 2;
            this.ucTextBoxExRemark.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ucTextBoxExRemark.FocusBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.ucTextBoxExRemark.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextBoxExRemark.InputText = "";
            this.ucTextBoxExRemark.InputType = WinDoControls.TextInputType.NotControl;
            this.ucTextBoxExRemark.IsErrorColor = false;
            this.ucTextBoxExRemark.IsFocusColor = false;
            this.ucTextBoxExRemark.IsRadius = true;
            this.ucTextBoxExRemark.IsShowClearBtn = false;
            this.ucTextBoxExRemark.IsShowKeyboard = false;
            this.ucTextBoxExRemark.IsShowRect = true;
            this.ucTextBoxExRemark.IsShowSearchBtn = false;
            this.ucTextBoxExRemark.IsShowShadow = false;
            this.ucTextBoxExRemark.Location = new System.Drawing.Point(151, 159);
            this.ucTextBoxExRemark.Margin = new System.Windows.Forms.Padding(0);
            this.ucTextBoxExRemark.MaxLength = 32767;
            this.ucTextBoxExRemark.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.ucTextBoxExRemark.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.ucTextBoxExRemark.MultiLine = true;
            this.ucTextBoxExRemark.Name = "ucTextBoxExRemark";
            this.ucTextBoxExRemark.PromptColor = System.Drawing.Color.Gray;
            this.ucTextBoxExRemark.PromptFont = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextBoxExRemark.PromptText = "";
            this.ucTextBoxExRemark.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ucTextBoxExRemark.RectWidth = 1;
            this.ucTextBoxExRemark.RegexPattern = "";
            this.ucTextBoxExRemark.Size = new System.Drawing.Size(627, 74);
            this.ucTextBoxExRemark.TabIndex = 31;
            // 
            // ucControlBase3
            // 
            this.ucControlBase3.BackColor = System.Drawing.Color.Transparent;
            this.ucControlBase3.ConerRadius = 1;
            this.ucControlBase3.Controls.Add(this.textBox1);
            this.ucControlBase3.FillColor = System.Drawing.Color.White;
            this.ucControlBase3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase3.IsRadius = true;
            this.ucControlBase3.IsShowRect = true;
            this.ucControlBase3.IsShowShadow = false;
            this.ucControlBase3.Location = new System.Drawing.Point(151, 3);
            this.ucControlBase3.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase3.Name = "ucControlBase3";
            this.ucControlBase3.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase3.RectWidth = 1;
            this.ucControlBase3.Size = new System.Drawing.Size(627, 153);
            this.ucControlBase3.TabIndex = 33;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(1, 1);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(625, 151);
            this.textBox1.TabIndex = 32;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucLowPanelQuote1);
            this.panel1.Controls.Add(this.ucSplitLine_V1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 30);
            this.panel1.TabIndex = 26;
            // 
            // ucLowPanelQuote1
            // 
            this.ucLowPanelQuote1.BackColor = System.Drawing.Color.Transparent;
            this.ucLowPanelQuote1.BorderColor = System.Drawing.Color.Transparent;
            this.ucLowPanelQuote1.LeftColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(212)))));
            this.ucLowPanelQuote1.LeftPadding = 5;
            this.ucLowPanelQuote1.Location = new System.Drawing.Point(9, 7);
            this.ucLowPanelQuote1.Margin = new System.Windows.Forms.Padding(0);
            this.ucLowPanelQuote1.Name = "ucLowPanelQuote1";
            this.ucLowPanelQuote1.Size = new System.Drawing.Size(387, 14);
            this.ucLowPanelQuote1.TabIndex = 3;
            this.ucLowPanelQuote1.Title = "标题";
            this.ucLowPanelQuote1.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(191)))), ((int)(((byte)(213)))));
            // 
            // ucSplitLine_V1
            // 
            this.ucSplitLine_V1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ucSplitLine_V1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucSplitLine_V1.Location = new System.Drawing.Point(0, 27);
            this.ucSplitLine_V1.Name = "ucSplitLine_V1";
            this.ucSplitLine_V1.Size = new System.Drawing.Size(800, 3);
            this.ucSplitLine_V1.TabIndex = 1;
            this.ucSplitLine_V1.TabStop = false;
            // 
            // ucBtnImg0Words2
            // 
            this.ucBtnImg0Words2.BackColor = System.Drawing.Color.Transparent;
            this.ucBtnImg0Words2.BtnBackColor = System.Drawing.Color.Transparent;
            this.ucBtnImg0Words2.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg0Words2.BtnForeColor = System.Drawing.Color.Black;
            this.ucBtnImg0Words2.BtnText = "";
            this.ucBtnImg0Words2.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnImg0Words2.ConerRadius = 1;
            this.ucBtnImg0Words2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImg0Words2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ucBtnImg0Words2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg0Words2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg0Words2.IconName = "I_info";
            this.ucBtnImg0Words2.Image = ((System.Drawing.Image)(resources.GetObject("ucBtnImg0Words2.Image")));
            this.ucBtnImg0Words2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg0Words2.ImageFontIcons = null;
            this.ucBtnImg0Words2.IsLink = false;
            this.ucBtnImg0Words2.IsRadius = true;
            this.ucBtnImg0Words2.IsShowRect = true;
            this.ucBtnImg0Words2.IsShowShadow = true;
            this.ucBtnImg0Words2.IsShowTips = false;
            this.ucBtnImg0Words2.Location = new System.Drawing.Point(737, 38);
            this.ucBtnImg0Words2.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnImg0Words2.Name = "ucBtnImg0Words2";
            this.ucBtnImg0Words2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ucBtnImg0Words2.RectWidth = 1;
            this.ucBtnImg0Words2.Size = new System.Drawing.Size(36, 31);
            this.ucBtnImg0Words2.TabIndex = 23;
            this.ucBtnImg0Words2.TabStop = false;
            this.ucBtnImg0Words2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnImg0Words2.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnImg0Words2.TipsText = "";
            this.ucBtnImg0Words2.UseHoverColor = false;
            this.ucBtnImg0Words2.BtnClick += new System.EventHandler(this.ucBtnImg0Words2_BtnClick);
            // 
            // ucBtnImg0Words3
            // 
            this.ucBtnImg0Words3.BackColor = System.Drawing.Color.Transparent;
            this.ucBtnImg0Words3.BtnBackColor = System.Drawing.Color.Transparent;
            this.ucBtnImg0Words3.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg0Words3.BtnForeColor = System.Drawing.Color.Black;
            this.ucBtnImg0Words3.BtnText = "";
            this.ucBtnImg0Words3.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnImg0Words3.ConerRadius = 1;
            this.ucBtnImg0Words3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImg0Words3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ucBtnImg0Words3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg0Words3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg0Words3.IconName = "I_info";
            this.ucBtnImg0Words3.Image = ((System.Drawing.Image)(resources.GetObject("ucBtnImg0Words3.Image")));
            this.ucBtnImg0Words3.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg0Words3.ImageFontIcons = null;
            this.ucBtnImg0Words3.IsLink = false;
            this.ucBtnImg0Words3.IsRadius = true;
            this.ucBtnImg0Words3.IsShowRect = true;
            this.ucBtnImg0Words3.IsShowShadow = true;
            this.ucBtnImg0Words3.IsShowTips = false;
            this.ucBtnImg0Words3.Location = new System.Drawing.Point(698, 38);
            this.ucBtnImg0Words3.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnImg0Words3.Name = "ucBtnImg0Words3";
            this.ucBtnImg0Words3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ucBtnImg0Words3.RectWidth = 1;
            this.ucBtnImg0Words3.Size = new System.Drawing.Size(36, 31);
            this.ucBtnImg0Words3.TabIndex = 24;
            this.ucBtnImg0Words3.TabStop = false;
            this.ucBtnImg0Words3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnImg0Words3.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnImg0Words3.TipsText = "";
            this.ucBtnImg0Words3.UseHoverColor = false;
            this.ucBtnImg0Words3.BtnClick += new System.EventHandler(this.ucBtnImg0Words3_BtnClick);
            // 
            // ucBtnImg0Words4
            // 
            this.ucBtnImg0Words4.BackColor = System.Drawing.Color.Transparent;
            this.ucBtnImg0Words4.BtnBackColor = System.Drawing.Color.Transparent;
            this.ucBtnImg0Words4.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg0Words4.BtnForeColor = System.Drawing.Color.Black;
            this.ucBtnImg0Words4.BtnText = "";
            this.ucBtnImg0Words4.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnImg0Words4.ConerRadius = 1;
            this.ucBtnImg0Words4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImg0Words4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ucBtnImg0Words4.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg0Words4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg0Words4.IconName = "I_plus";
            this.ucBtnImg0Words4.Image = ((System.Drawing.Image)(resources.GetObject("ucBtnImg0Words4.Image")));
            this.ucBtnImg0Words4.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg0Words4.ImageFontIcons = null;
            this.ucBtnImg0Words4.IsLink = false;
            this.ucBtnImg0Words4.IsRadius = true;
            this.ucBtnImg0Words4.IsShowRect = true;
            this.ucBtnImg0Words4.IsShowShadow = true;
            this.ucBtnImg0Words4.IsShowTips = false;
            this.ucBtnImg0Words4.Location = new System.Drawing.Point(620, 38);
            this.ucBtnImg0Words4.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnImg0Words4.Name = "ucBtnImg0Words4";
            this.ucBtnImg0Words4.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ucBtnImg0Words4.RectWidth = 1;
            this.ucBtnImg0Words4.Size = new System.Drawing.Size(36, 31);
            this.ucBtnImg0Words4.TabIndex = 25;
            this.ucBtnImg0Words4.TabStop = false;
            this.ucBtnImg0Words4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnImg0Words4.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnImg0Words4.TipsText = "";
            this.ucBtnImg0Words4.UseHoverColor = false;
            this.ucBtnImg0Words4.BtnClick += new System.EventHandler(this.ucBtnImg0Words4_BtnClick);
            // 
            // ucBtnImg0Words1
            // 
            this.ucBtnImg0Words1.BackColor = System.Drawing.Color.Transparent;
            this.ucBtnImg0Words1.BtnBackColor = System.Drawing.Color.Transparent;
            this.ucBtnImg0Words1.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg0Words1.BtnForeColor = System.Drawing.Color.Black;
            this.ucBtnImg0Words1.BtnText = "";
            this.ucBtnImg0Words1.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnImg0Words1.ConerRadius = 1;
            this.ucBtnImg0Words1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImg0Words1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ucBtnImg0Words1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg0Words1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg0Words1.IconName = "I_trash";
            this.ucBtnImg0Words1.Image = ((System.Drawing.Image)(resources.GetObject("ucBtnImg0Words1.Image")));
            this.ucBtnImg0Words1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg0Words1.ImageFontIcons = null;
            this.ucBtnImg0Words1.IsLink = false;
            this.ucBtnImg0Words1.IsRadius = true;
            this.ucBtnImg0Words1.IsShowRect = true;
            this.ucBtnImg0Words1.IsShowShadow = true;
            this.ucBtnImg0Words1.IsShowTips = false;
            this.ucBtnImg0Words1.Location = new System.Drawing.Point(659, 38);
            this.ucBtnImg0Words1.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnImg0Words1.Name = "ucBtnImg0Words1";
            this.ucBtnImg0Words1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ucBtnImg0Words1.RectWidth = 1;
            this.ucBtnImg0Words1.Size = new System.Drawing.Size(36, 31);
            this.ucBtnImg0Words1.TabIndex = 22;
            this.ucBtnImg0Words1.TabStop = false;
            this.ucBtnImg0Words1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ucBtnImg0Words1.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnImg0Words1.TipsText = "";
            this.ucBtnImg0Words1.UseHoverColor = false;
            this.ucBtnImg0Words1.BtnClick += new System.EventHandler(this.ucBtnImg0Words1_BtnClick);
            // 
            // frmSystemDicEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 704);
            this.Controls.Add(this.panel3);
            this.Name = "frmSystemDicEdit";
            this.Activated += new System.EventHandler(this.frmSystemDicEdit_Activated);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.panel3.ResumeLayout(false);
            this.ucControlBase1.ResumeLayout(false);
            this.ucControlBase2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ucControlBase3.ResumeLayout(false);
            this.ucControlBase3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

 
        private System.Windows.Forms.Panel panel3;
        private WinDoControls.Controls.WDCtrlBase ucControlBase1;
        private WinDoControls.Controls.WDCtrlBase ucControlBase2;
        private System.Windows.Forms.Panel panel1;
        private WinDoControls.Controls.WDSplitLine_V ucSplitLine_V1;
        private WDBtnImg0Words ucBtnImg0Words2;
        private WDBtnImg0Words ucBtnImg0Words3;
        private WDBtnImg0Words ucBtnImg0Words4;
        private WDBtnImg0Words ucBtnImg0Words1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private WDTextBoxClear ucTextBoxExRemark;
        private UCLowPanelQuote ucLowPanelQuote1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SysDes;
        private System.Windows.Forms.DataGridViewTextBoxColumn SysVal;
        private System.Windows.Forms.DataGridViewTextBoxColumn SysVal2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SysVal3;
        private System.Windows.Forms.TextBox textBox1;
        private WinDoControls.Controls.WDCtrlBase ucControlBase3;
    }
}