using WinDoControls.Controls;

namespace WinDo.UI.Manage
{
    partial class frmUserPrivileges
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserPrivileges));
            this.lblUser = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucControlBase1 = new WinDoControls.Controls.WDCtrlBase();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSave = new WDBtnImg2WordsYS();
            this.btnReturn = new WDBtnImg2Words();
            this.ucSplitLine_H1 = new WinDoControls.Controls.WDSplitLine_H();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ucControlBase1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUser.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblUser.Location = new System.Drawing.Point(0, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(1025, 35);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "用户：xxxx";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel1.Size = new System.Drawing.Size(1025, 45);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucControlBase1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(1045, 610);
            this.panel2.TabIndex = 3;
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
            this.ucControlBase1.Location = new System.Drawing.Point(10, 55);
            this.ucControlBase1.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase1.Name = "ucControlBase1";
            this.ucControlBase1.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase1.RectWidth = 1;
            this.ucControlBase1.Size = new System.Drawing.Size(1025, 545);
            this.ucControlBase1.TabIndex = 2;
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
            this.dataGridView1.Size = new System.Drawing.Size(1023, 543);
            this.dataGridView1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnReturn);
            this.panel3.Controls.Add(this.ucSplitLine_H1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 610);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1045, 74);
            this.panel3.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BtnBackColor = System.Drawing.Color.Transparent;
            this.btnSave.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnSave.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSave.BtnText = "保存";
            this.btnSave.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.ConerRadius = 1;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FillColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
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
            this.btnSave.Location = new System.Drawing.Point(433, 25);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 8, 0);
            this.btnSave.RectColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.btnSave.RectWidth = 1;
            this.btnSave.Size = new System.Drawing.Size(72, 32);
            this.btnSave.TabIndex = 3;
            this.btnSave.TabStop = false;
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnSave.TipsText = "";
            this.btnSave.UseHoverColor = false;
            this.btnSave.BtnClick += new System.EventHandler(this.btnSave_BtnClick);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
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
            this.btnReturn.Location = new System.Drawing.Point(540, 25);
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
            // ucSplitLine_H1
            // 
            this.ucSplitLine_H1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ucSplitLine_H1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucSplitLine_H1.Location = new System.Drawing.Point(0, 0);
            this.ucSplitLine_H1.Name = "ucSplitLine_H1";
            this.ucSplitLine_H1.Size = new System.Drawing.Size(1045, 1);
            this.ucSplitLine_H1.TabIndex = 0;
            this.ucSplitLine_H1.TabStop = false;
            // 
            // frmUserPrivileges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 684);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmUserPrivileges";
            this.Text = "frmUserPrivileges";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ucControlBase1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private WDBtnImg2WordsYS btnSave;
        private WDBtnImg2Words btnReturn;
        private WinDoControls.Controls.WDSplitLine_H ucSplitLine_H1;
        private WinDoControls.Controls.WDCtrlBase ucControlBase1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}