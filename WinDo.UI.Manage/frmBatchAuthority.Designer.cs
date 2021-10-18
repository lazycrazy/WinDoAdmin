
using WinDoControls.Controls;

namespace WinDo.UI.Manage
{
    partial class frmBatchAuthority
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatchAuthority));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucControlBase1 = new WinDoControls.Controls.WDCtrlBase();
            this.dgv2 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnQuery = new WDBtnImg2WordsYS();
            this.combRole = new WDLabelComboBox();
            this.combTitle = new WDLabelComboBox();
            this.txtUserName = new WDLabelTextBox();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ucControlBase1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(793, 40);
            // 
            // btnCancel
            // 
            this.btnCancel.BtnText = "返回";
            this.btnCancel.IconName = "I_leftarrow_clear";
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(304, 16);
            // 
            // btnOK
            // 
            this.btnOK.BtnText = "批量授权";
            this.btnOK.IconName = "I_copy";
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(191, 16);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(839, 600);
            this.panel3.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.ucControlBase1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(839, 600);
            this.panel1.TabIndex = 4;
            // 
            // ucControlBase1
            // 
            this.ucControlBase1.ConerRadius = 1;
            this.ucControlBase1.Controls.Add(this.dgv2);
            this.ucControlBase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucControlBase1.FillColor = System.Drawing.Color.Transparent;
            this.ucControlBase1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase1.IsRadius = true;
            this.ucControlBase1.IsShowRect = true;
            this.ucControlBase1.IsShowShadow = false;
            this.ucControlBase1.Location = new System.Drawing.Point(10, 60);
            this.ucControlBase1.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase1.Name = "ucControlBase1";
            this.ucControlBase1.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase1.RectWidth = 1;
            this.ucControlBase1.Size = new System.Drawing.Size(819, 530);
            this.ucControlBase1.TabIndex = 3;
            // 
            // dgv2
            // 
            this.dgv2.BackgroundColor = System.Drawing.Color.White;
            this.dgv2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv2.Location = new System.Drawing.Point(1, 1);
            this.dgv2.Name = "dgv2";
            this.dgv2.RowTemplate.Height = 23;
            this.dgv2.Size = new System.Drawing.Size(817, 528);
            this.dgv2.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnQuery);
            this.panel2.Controls.Add(this.combRole);
            this.panel2.Controls.Add(this.combTitle);
            this.panel2.Controls.Add(this.txtUserName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(819, 50);
            this.panel2.TabIndex = 0;
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
            this.btnQuery.FillColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
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
            this.btnQuery.Location = new System.Drawing.Point(708, 9);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(0);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Padding = new System.Windows.Forms.Padding(7, 0, 8, 0);
            this.btnQuery.RectColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.btnQuery.RectWidth = 1;
            this.btnQuery.Size = new System.Drawing.Size(72, 32);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.TabStop = false;
            this.btnQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuery.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnQuery.TipsText = "";
            this.btnQuery.UseHoverColor = false;
            this.btnQuery.BtnClick += new System.EventHandler(this.btnQuery_BtnClick);
            // 
            // combRole
            // 
            this.combRole.CtrlsSpace = 10;
            this.combRole.FirstCtrlWidth = 40;
            this.combRole.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.combRole.IsRequired = false;
            this.combRole.LabelCtrlsSpace = 10;
            this.combRole.LabelText = "角色:";
            this.combRole.LabelWidth = 40;
            this.combRole.Location = new System.Drawing.Point(480, 9);
            this.combRole.Name = "combRole";
            this.combRole.Padding = new System.Windows.Forms.Padding(1);
            this.combRole.ReadOnly = false;
            this.combRole.Size = new System.Drawing.Size(211, 32);
            this.combRole.TabIndex = 2;
            this.combRole.TabStop = true;
            // 
            // combTitle
            // 
            this.combTitle.CtrlsSpace = 10;
            this.combTitle.FirstCtrlWidth = 40;
            this.combTitle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.combTitle.IsRequired = false;
            this.combTitle.LabelCtrlsSpace = 10;
            this.combTitle.LabelText = "职务:";
            this.combTitle.LabelWidth = 40;
            this.combTitle.Location = new System.Drawing.Point(263, 9);
            this.combTitle.Name = "combTitle";
            this.combTitle.Padding = new System.Windows.Forms.Padding(1);
            this.combTitle.ReadOnly = false;
            this.combTitle.Size = new System.Drawing.Size(211, 32);
            this.combTitle.TabIndex = 1;
            this.combTitle.TabStop = true;
            // 
            // txtUserName
            // 
            this.txtUserName.CtrlsSpace = 10;
            this.txtUserName.FirstCtrlWidth = 100;
            this.txtUserName.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtUserName.IsRequired = false;
            this.txtUserName.LabelCtrlsSpace = 10;
            this.txtUserName.LabelText = "用户名/姓名:";
            this.txtUserName.Location = new System.Drawing.Point(0, 9);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Padding = new System.Windows.Forms.Padding(1);
            this.txtUserName.ReadOnly = false;
            this.txtUserName.Size = new System.Drawing.Size(245, 32);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.TabStop = true;
            // 
            // frmBatchAuthority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 704);
            this.Controls.Add(this.panel3);
            this.Name = "frmBatchAuthority";
            this.Controls.SetChildIndex(this.panel3, 0);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ucControlBase1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

 
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private WinDoControls.Controls.WDCtrlBase ucControlBase1;
        private WDBtnImg2WordsYS btnQuery;
        private WDLabelComboBox combRole;
        private WDLabelComboBox combTitle;
        private WDLabelTextBox txtUserName;
        private System.Windows.Forms.DataGridView dgv2;
    }
}