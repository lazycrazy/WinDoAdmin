using WinDoControls.Controls;

namespace WinDo.UI.Manage
{
    partial class frmSystemDicManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSystemDicManage));
            this.ucdbPagerControl1 = new WinDoControls.Controls.WDPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelSystemDicList = new System.Windows.Forms.Panel();
            this.ucControlBase1 = new WinDoControls.Controls.WDCtrlBase();
            this.flowLayoutPanel2 = new System.Windows.Forms.Panel();
            this.ucTextBoxClearKeyWord = new WDTextBoxClear();
            this.comSystemDic = new WDLabelComboBox();
            this.btnQuery = new WDBtnImg2WordsYS();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panelSystemDicList.SuspendLayout();
            this.ucControlBase1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ucdbPagerControl1
            // 
            this.ucdbPagerControl1.BackColor = System.Drawing.Color.White;
            this.ucdbPagerControl1.DataSource = ((System.Collections.Generic.List<object>)(resources.GetObject("ucdbPagerControl1.DataSource")));
            this.ucdbPagerControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucdbPagerControl1.Location = new System.Drawing.Point(0, 700);
            this.ucdbPagerControl1.Name = "ucdbPagerControl1";
            this.ucdbPagerControl1.PageCount = 0;
            this.ucdbPagerControl1.PageIndex = 1;
            this.ucdbPagerControl1.PageSize = 20;
            this.ucdbPagerControl1.Size = new System.Drawing.Size(1370, 49);
            this.ucdbPagerControl1.StartIndex = 0;
            this.ucdbPagerControl1.TabIndex = 4;
            this.ucdbPagerControl1.TotalCount = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelSystemDicList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 749);
            this.panel1.TabIndex = 5;
            // 
            // panelSystemDicList
            // 
            this.panelSystemDicList.Controls.Add(this.ucControlBase1);
            this.panelSystemDicList.Controls.Add(this.ucdbPagerControl1);
            this.panelSystemDicList.Controls.Add(this.flowLayoutPanel2);
            this.panelSystemDicList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSystemDicList.Location = new System.Drawing.Point(0, 0);
            this.panelSystemDicList.Name = "panelSystemDicList";
            this.panelSystemDicList.Size = new System.Drawing.Size(1370, 749);
            this.panelSystemDicList.TabIndex = 5;
            // 
            // ucControlBase1
            // 
            this.ucControlBase1.BackColor = System.Drawing.Color.Transparent;
            this.ucControlBase1.ConerRadius = 1;
            this.ucControlBase1.Controls.Add(this.dataGridView1);
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
            this.ucControlBase1.Size = new System.Drawing.Size(1370, 653);
            this.ucControlBase1.TabIndex = 8;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel2.Controls.Add(this.ucTextBoxClearKeyWord);
            this.flowLayoutPanel2.Controls.Add(this.comSystemDic);
            this.flowLayoutPanel2.Controls.Add(this.btnQuery);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1370, 47);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // ucTextBoxClearKeyWord
            // 
            this.ucTextBoxClearKeyWord.BackColor = System.Drawing.Color.Transparent;
            this.ucTextBoxClearKeyWord.ConerRadius = 1;
            this.ucTextBoxClearKeyWord.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ucTextBoxClearKeyWord.DecLength = 2;
            this.ucTextBoxClearKeyWord.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ucTextBoxClearKeyWord.FocusBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.ucTextBoxClearKeyWord.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextBoxClearKeyWord.InputText = "";
            this.ucTextBoxClearKeyWord.InputType = WinDoControls.TextInputType.NotControl;
            this.ucTextBoxClearKeyWord.IsErrorColor = false;
            this.ucTextBoxClearKeyWord.IsFocusColor = false;
            this.ucTextBoxClearKeyWord.IsRadius = true;
            this.ucTextBoxClearKeyWord.IsShowClearBtn = false;
            this.ucTextBoxClearKeyWord.IsShowKeyboard = false;
            this.ucTextBoxClearKeyWord.IsShowRect = true;
            this.ucTextBoxClearKeyWord.IsShowSearchBtn = false;
            this.ucTextBoxClearKeyWord.IsShowShadow = false;
            this.ucTextBoxClearKeyWord.Location = new System.Drawing.Point(281, 7);
            this.ucTextBoxClearKeyWord.Margin = new System.Windows.Forms.Padding(0);
            this.ucTextBoxClearKeyWord.MaxLength = 32767;
            this.ucTextBoxClearKeyWord.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.ucTextBoxClearKeyWord.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.ucTextBoxClearKeyWord.Name = "ucTextBoxClearKeyWord";
            this.ucTextBoxClearKeyWord.PromptColor = System.Drawing.Color.Gray;
            this.ucTextBoxClearKeyWord.PromptFont = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextBoxClearKeyWord.PromptText = "请输入关键词进行查询";
            this.ucTextBoxClearKeyWord.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ucTextBoxClearKeyWord.RectWidth = 1;
            this.ucTextBoxClearKeyWord.RegexPattern = "";
            this.ucTextBoxClearKeyWord.Size = new System.Drawing.Size(206, 32);
            this.ucTextBoxClearKeyWord.TabIndex = 7;
            // 
            // comSystemDic
            // 
            this.comSystemDic.BackColor = System.Drawing.Color.Transparent;
            this.comSystemDic.CtrlsSpace = 10;
            this.comSystemDic.FirstCtrlWidth = 50;
            this.comSystemDic.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.comSystemDic.IsRequired = false;
            this.comSystemDic.LabelCtrlsSpace = 10;
            this.comSystemDic.LabelText = "模块:";
            this.comSystemDic.LabelWidth = 50;
            this.comSystemDic.Location = new System.Drawing.Point(3, 7);
            this.comSystemDic.Name = "comSystemDic";
            this.comSystemDic.Padding = new System.Windows.Forms.Padding(1);
            this.comSystemDic.ReadOnly = false;
            this.comSystemDic.Size = new System.Drawing.Size(268, 32);
            this.comSystemDic.TabIndex = 6;
            this.comSystemDic.TabStop = true;
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
            this.btnQuery.Location = new System.Drawing.Point(499, 7);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(0);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Padding = new System.Windows.Forms.Padding(8, 0, 7, 0);
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
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(1, 1);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1368, 651);
            this.dataGridView1.TabIndex = 9;
            // 
            // frmSystemDicManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSystemDicManage";
            this.Text = "frmSystemDicManage";
            this.Load += new System.EventHandler(this.frmSystemDicManage_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSystemDicManage_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panelSystemDicList.ResumeLayout(false);
            this.ucControlBase1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private WinDoControls.Controls.WDPage ucdbPagerControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelSystemDicList;
        private System.Windows.Forms.Panel flowLayoutPanel2;
        private WDTextBoxClear ucTextBoxClearKeyWord;
        private WDLabelComboBox comSystemDic;
        private WDBtnImg2WordsYS btnQuery;
        private WinDoControls.Controls.WDCtrlBase ucControlBase1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}