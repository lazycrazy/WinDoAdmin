using WinDoControls.Controls;

namespace WinDo.UI.Utilities.DialogForm
{
    partial class FrmTextBoxWithOk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTextBoxWithOk));
            this.ucSplitLine_H2 = new WinDoControls.Controls.WDSplitLine_H();
            this.lblTitle = new System.Windows.Forms.Label();
            this.ucSplitLine_H1 = new WinDoControls.Controls.WDSplitLine_H();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtInput = new WDLabelTextBox();
            this.btnOK = new WDBtnImg2WordsYS();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucSplitLine_H2
            // 
            this.ucSplitLine_H2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ucSplitLine_H2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucSplitLine_H2.Location = new System.Drawing.Point(0, 97);
            this.ucSplitLine_H2.Name = "ucSplitLine_H2";
            this.ucSplitLine_H2.Size = new System.Drawing.Size(337, 1);
            this.ucSplitLine_H2.TabIndex = 11;
            this.ucSplitLine_H2.TabStop = false;
            this.ucSplitLine_H2.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(291, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "提示";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucSplitLine_H1
            // 
            this.ucSplitLine_H1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ucSplitLine_H1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucSplitLine_H1.Location = new System.Drawing.Point(0, 40);
            this.ucSplitLine_H1.Name = "ucSplitLine_H1";
            this.ucSplitLine_H1.Size = new System.Drawing.Size(337, 1);
            this.ucSplitLine_H1.TabIndex = 10;
            this.ucSplitLine_H1.TabStop = false;
            this.ucSplitLine_H1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 98);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(337, 64);
            this.panel1.TabIndex = 9;
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(291, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(46, 40);
            this.btnClose.TabIndex = 4;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(337, 40);
            this.panel2.TabIndex = 12;
            // 
            // txtInput
            // 
            this.txtInput.AutoSize = true;
            this.txtInput.CtrlsSpace = 10;
            this.txtInput.FirstCtrlWidth = 50;
            this.txtInput.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtInput.LabelCtrlsSpace = 10;
            this.txtInput.LabelText = "文本框";
            this.txtInput.LabelWidth = 50;
            this.txtInput.Location = new System.Drawing.Point(26, 53);
            this.txtInput.Name = "txtInput";
            this.txtInput.Padding = new System.Windows.Forms.Padding(1);
            this.txtInput.Size = new System.Drawing.Size(251, 38);
            this.txtInput.TabIndex = 13;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BtnBackColor = System.Drawing.Color.Transparent;
            this.btnOK.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnOK.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnOK.BtnText = "确定";
            this.btnOK.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.ConerRadius = 1;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.FillColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnOK.IconName = "I_info";
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.ImageFontIcons = null;
            this.btnOK.IsRadius = true;
            this.btnOK.IsShowRect = true;
            this.btnOK.IsShowShadow = true;
            this.btnOK.IsShowTips = false;
            this.btnOK.Location = new System.Drawing.Point(132, 16);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(10, 0, 6, 0);
            this.btnOK.RectColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.btnOK.RectWidth = 1;
            this.btnOK.Size = new System.Drawing.Size(72, 32);
            this.btnOK.TabIndex = 0;
            this.btnOK.TabStop = false;
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnOK.TipsText = "";
            // 
            // FrmTextBoxWithOk
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = WinDo.Utilities.PublicResource.WDColors.SelectedBackColor;
            this.BorderStyleColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.BorderStyleSize = 1;
            this.BorderStyleType = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.ClientSize = new System.Drawing.Size(337, 162);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.ucSplitLine_H2);
            this.Controls.Add(this.ucSplitLine_H1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "FrmTextBoxWithOk";
            this.Redraw = true;
            this.RegionRadius = 1;
            this.Text = "frmProblemQuery";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinDoControls.Controls.WDSplitLine_H ucSplitLine_H2;
        private System.Windows.Forms.Label lblTitle;
        private WinDoControls.Controls.WDSplitLine_H ucSplitLine_H1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel btnClose;
        private System.Windows.Forms.Panel panel2;
        private WDBtnImg2WordsYS btnOK;
        private WDLabelTextBox txtInput;
    }
}