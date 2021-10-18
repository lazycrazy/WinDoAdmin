
using WinDoControls.Controls;

namespace WinDo.UI.Manage
{
    partial class frmConfigEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigEdit));
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucControlBase1 = new WinDoControls.Controls.WDCtrlBase();
            this.ucControlBase2 = new WinDoControls.Controls.WDCtrlBase();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ucTextBoxExValue = new WDTextBoxClear();
            this.ucComboxValues = new WinDoControls.Controls.WDCombox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucLabelSysNoteValue = new System.Windows.Forms.TextBox();
            this.ucTextBoxExRemark = new WDTextBoxClear();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucLowPanelQuote1 = new UCLowPanelQuote();
            this.ucSplitLine_V1 = new WinDoControls.Controls.WDSplitLine_V();
            this.ucControlBase3 = new WinDoControls.Controls.WDCtrlBase();
            this.panel3.SuspendLayout();
            this.ucControlBase1.SuspendLayout();
            this.ucControlBase2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ucControlBase3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(837, 40);
            this.lblTitle.Text = "编辑配置";
            // 
            // btnCancel
            // 
            this.btnCancel.BtnText = "返回";
            this.btnCancel.IconName = "I_leftarrow_clear";
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(423, 12);
            // 
            // btnOK
            // 
            this.btnOK.BtnText = "确定";
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(341, 12);
            this.btnOK.Size = new System.Drawing.Size(72, 32);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.ucControlBase1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(837, 477);
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
            this.ucControlBase1.Size = new System.Drawing.Size(837, 477);
            this.ucControlBase1.TabIndex = 3;
            // 
            // ucControlBase2
            // 
            this.ucControlBase2.BackColor = System.Drawing.Color.White;
            this.ucControlBase2.ConerRadius = 24;
            this.ucControlBase2.Controls.Add(this.panel4);
            this.ucControlBase2.Controls.Add(this.panel2);
            this.ucControlBase2.Controls.Add(this.panel1);
            this.ucControlBase2.FillColor = System.Drawing.Color.Transparent;
            this.ucControlBase2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase2.IsRadius = false;
            this.ucControlBase2.IsShowRect = false;
            this.ucControlBase2.IsShowShadow = false;
            this.ucControlBase2.Location = new System.Drawing.Point(18, 21);
            this.ucControlBase2.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase2.Name = "ucControlBase2";
            this.ucControlBase2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase2.RectWidth = 1;
            this.ucControlBase2.Size = new System.Drawing.Size(802, 451);
            this.ucControlBase2.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ucTextBoxExValue);
            this.panel4.Controls.Add(this.ucComboxValues);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 44);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(802, 163);
            this.panel4.TabIndex = 33;
            // 
            // ucTextBoxExValue
            // 
            this.ucTextBoxExValue.BackColor = System.Drawing.Color.Transparent;
            this.ucTextBoxExValue.ConerRadius = 1;
            this.ucTextBoxExValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ucTextBoxExValue.DecLength = 2;
            this.ucTextBoxExValue.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ucTextBoxExValue.FocusBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.ucTextBoxExValue.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextBoxExValue.InputText = "";
            this.ucTextBoxExValue.InputType = WinDoControls.TextInputType.NotControl;
            this.ucTextBoxExValue.IsErrorColor = false;
            this.ucTextBoxExValue.IsFocusColor = false;
            this.ucTextBoxExValue.IsRadius = true;
            this.ucTextBoxExValue.IsShowClearBtn = false;
            this.ucTextBoxExValue.IsShowKeyboard = false;
            this.ucTextBoxExValue.IsShowRect = true;
            this.ucTextBoxExValue.IsShowSearchBtn = false;
            this.ucTextBoxExValue.IsShowShadow = false;
            this.ucTextBoxExValue.Location = new System.Drawing.Point(150, 22);
            this.ucTextBoxExValue.Margin = new System.Windows.Forms.Padding(0);
            this.ucTextBoxExValue.MaxLength = 32767;
            this.ucTextBoxExValue.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.ucTextBoxExValue.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.ucTextBoxExValue.MultiLine = true;
            this.ucTextBoxExValue.Name = "ucTextBoxExValue";
            this.ucTextBoxExValue.PromptColor = System.Drawing.Color.Gray;
            this.ucTextBoxExValue.PromptFont = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextBoxExValue.PromptText = "";
            this.ucTextBoxExValue.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ucTextBoxExValue.RectWidth = 1;
            this.ucTextBoxExValue.RegexPattern = "";
            this.ucTextBoxExValue.Size = new System.Drawing.Size(620, 135);
            this.ucTextBoxExValue.TabIndex = 35;
            this.ucTextBoxExValue.Visible = false;
            // 
            // ucComboxValues
            // 
            this.ucComboxValues.BackColor = System.Drawing.Color.Transparent;
            this.ucComboxValues.BackColorExt = System.Drawing.Color.White;
            this.ucComboxValues.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.ucComboxValues.ConerRadius = 5;
            this.ucComboxValues.DropPanelHeight = -1;
            this.ucComboxValues.DropPanelWidth = -1;
            this.ucComboxValues.FillColor = System.Drawing.Color.White;
            this.ucComboxValues.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucComboxValues.IsErrorColor = false;
            this.ucComboxValues.IsRadius = true;
            this.ucComboxValues.IsShowRect = true;
            this.ucComboxValues.IsShowShadow = false;
            this.ucComboxValues.ItemWidth = 70;
            this.ucComboxValues.Location = new System.Drawing.Point(148, 18);
            this.ucComboxValues.Margin = new System.Windows.Forms.Padding(0);
            this.ucComboxValues.MaxLenth = 32767;
            this.ucComboxValues.Name = "ucComboxValues";
            this.ucComboxValues.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucComboxValues.RectWidth = 1;
            this.ucComboxValues.SelectedIndex = -1;
            this.ucComboxValues.SelectedValue = "";
            this.ucComboxValues.Size = new System.Drawing.Size(383, 32);
            this.ucComboxValues.Source = null;
            this.ucComboxValues.TabIndex = 33;
            this.ucComboxValues.TextValue = "";
            this.ucComboxValues.TriangleColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucComboxValues.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucControlBase3);
            this.panel2.Controls.Add(this.ucTextBoxExRemark);
            this.panel2.Location = new System.Drawing.Point(0, 213);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(802, 231);
            this.panel2.TabIndex = 28;
            // 
            // ucLabelSysNoteValue
            // 
            this.ucLabelSysNoteValue.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucLabelSysNoteValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ucLabelSysNoteValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLabelSysNoteValue.Location = new System.Drawing.Point(1, 1);
            this.ucLabelSysNoteValue.Multiline = true;
            this.ucLabelSysNoteValue.Name = "ucLabelSysNoteValue";
            this.ucLabelSysNoteValue.ReadOnly = true;
            this.ucLabelSysNoteValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ucLabelSysNoteValue.Size = new System.Drawing.Size(618, 130);
            this.ucLabelSysNoteValue.TabIndex = 34;
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
            this.ucTextBoxExRemark.Location = new System.Drawing.Point(150, 144);
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
            this.ucTextBoxExRemark.Size = new System.Drawing.Size(620, 77);
            this.ucTextBoxExRemark.TabIndex = 33;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucLowPanelQuote1);
            this.panel1.Controls.Add(this.ucSplitLine_V1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(802, 44);
            this.panel1.TabIndex = 26;
            // 
            // ucLowPanelQuote1
            // 
            this.ucLowPanelQuote1.BackColor = System.Drawing.Color.Transparent;
            this.ucLowPanelQuote1.BorderColor = System.Drawing.Color.Transparent;
            this.ucLowPanelQuote1.LeftColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(212)))));
            this.ucLowPanelQuote1.LeftPadding = 5;
            this.ucLowPanelQuote1.Location = new System.Drawing.Point(14, 15);
            this.ucLowPanelQuote1.Margin = new System.Windows.Forms.Padding(0);
            this.ucLowPanelQuote1.Name = "ucLowPanelQuote1";
            this.ucLowPanelQuote1.Size = new System.Drawing.Size(387, 14);
            this.ucLowPanelQuote1.TabIndex = 2;
            this.ucLowPanelQuote1.Title = "标题";
            this.ucLowPanelQuote1.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(191)))), ((int)(((byte)(213)))));
            // 
            // ucSplitLine_V1
            // 
            this.ucSplitLine_V1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ucSplitLine_V1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucSplitLine_V1.Location = new System.Drawing.Point(0, 41);
            this.ucSplitLine_V1.Name = "ucSplitLine_V1";
            this.ucSplitLine_V1.Size = new System.Drawing.Size(802, 3);
            this.ucSplitLine_V1.TabIndex = 1;
            this.ucSplitLine_V1.TabStop = false;
            // 
            // ucControlBase3
            // 
            this.ucControlBase3.BackColor = System.Drawing.Color.White;
            this.ucControlBase3.ConerRadius = 1;
            this.ucControlBase3.Controls.Add(this.ucLabelSysNoteValue);
            this.ucControlBase3.FillColor = System.Drawing.Color.White;
            this.ucControlBase3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase3.IsRadius = true;
            this.ucControlBase3.IsShowRect = true;
            this.ucControlBase3.IsShowShadow = false;
            this.ucControlBase3.Location = new System.Drawing.Point(150, 4);
            this.ucControlBase3.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase3.Name = "ucControlBase3";
            this.ucControlBase3.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase3.RectWidth = 1;
            this.ucControlBase3.Size = new System.Drawing.Size(620, 132);
            this.ucControlBase3.TabIndex = 35;
            // 
            // frmConfigEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(839, 583);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConfigEdit";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.panel3.ResumeLayout(false);
            this.ucControlBase1.ResumeLayout(false);
            this.ucControlBase2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ucControlBase3.ResumeLayout(false);
            this.ucControlBase3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

 
        private System.Windows.Forms.Panel panel3;
        private WinDoControls.Controls.WDCtrlBase ucControlBase1;
        private WinDoControls.Controls.WDCtrlBase ucControlBase2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private UCLowPanelQuote ucLowPanelQuote1;
        private WinDoControls.Controls.WDSplitLine_V ucSplitLine_V1;
        private WDTextBoxClear ucTextBoxExRemark;
        private System.Windows.Forms.Panel panel4;
        private WDTextBoxClear ucTextBoxExValue;
        private WinDoControls.Controls.WDCombox ucComboxValues;
        private System.Windows.Forms.TextBox ucLabelSysNoteValue;
        private WinDoControls.Controls.WDCtrlBase ucControlBase3;
    }
}