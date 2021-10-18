namespace WinDoControls.Controls
{
    partial class frmSelectTime
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
            this.listHour = new System.Windows.Forms.ListBox();
            this.ListMinute = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucControlBase1 = new WinDoControls.Controls.WDCtrlBase();
            this.ucControlBase3 = new WinDoControls.Controls.WDCtrlBase();
            this.ucSplitLine_V2 = new WinDoControls.Controls.WDSplitLine_V();
            this.btnNow = new System.Windows.Forms.Label();
            this.ucSplitLine_V1 = new WinDoControls.Controls.WDSplitLine_V();
            this.btnClear = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Label();
            this.ucControlBase2 = new WinDoControls.Controls.WDCtrlBase();
            this.ucSplitLine_H1 = new WinDoControls.Controls.WDSplitLine_H();
            this.ucControlBase1.SuspendLayout();
            this.ucControlBase3.SuspendLayout();
            this.ucControlBase2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listHour
            // 
            this.listHour.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listHour.Dock = System.Windows.Forms.DockStyle.Left;
            this.listHour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listHour.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.listHour.ItemHeight = 24;
            this.listHour.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.listHour.Location = new System.Drawing.Point(1, 1);
            this.listHour.Name = "listHour";
            this.listHour.Size = new System.Drawing.Size(74, 178);
            this.listHour.TabIndex = 5;
            this.listHour.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listHour_MouseClick);
            this.listHour.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listHour_DrawItem);
            // 
            // ListMinute
            // 
            this.ListMinute.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListMinute.Dock = System.Windows.Forms.DockStyle.Right;
            this.ListMinute.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ListMinute.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ListMinute.ItemHeight = 24;
            this.ListMinute.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.ListMinute.Location = new System.Drawing.Point(81, 1);
            this.ListMinute.Name = "ListMinute";
            this.ListMinute.Size = new System.Drawing.Size(68, 178);
            this.ListMinute.TabIndex = 5;
            this.ListMinute.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listHour_MouseClick);
            this.ListMinute.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listHour_DrawItem);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(106, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "分";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(33, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "时";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择时间";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucControlBase1
            // 
            this.ucControlBase1.ConerRadius = 5;
            this.ucControlBase1.Controls.Add(this.ucControlBase3);
            this.ucControlBase1.Controls.Add(this.ucControlBase2);
            this.ucControlBase1.Controls.Add(this.label3);
            this.ucControlBase1.Controls.Add(this.label2);
            this.ucControlBase1.Controls.Add(this.ucSplitLine_H1);
            this.ucControlBase1.Controls.Add(this.label1);
            this.ucControlBase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucControlBase1.FillColor = System.Drawing.Color.White;
            this.ucControlBase1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase1.IsRadius = true;
            this.ucControlBase1.IsShowRect = true;
            this.ucControlBase1.IsShowShadow = false;
            this.ucControlBase1.Location = new System.Drawing.Point(0, 0);
            this.ucControlBase1.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase1.Name = "ucControlBase1";
            this.ucControlBase1.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase1.RectWidth = 1;
            this.ucControlBase1.Size = new System.Drawing.Size(170, 310);
            this.ucControlBase1.TabIndex = 1;
            // 
            // ucControlBase3
            // 
            this.ucControlBase3.ConerRadius = 1;
            this.ucControlBase3.Controls.Add(this.ucSplitLine_V2);
            this.ucControlBase3.Controls.Add(this.btnNow);
            this.ucControlBase3.Controls.Add(this.ucSplitLine_V1);
            this.ucControlBase3.Controls.Add(this.btnClear);
            this.ucControlBase3.Controls.Add(this.btnOK);
            this.ucControlBase3.FillColor = System.Drawing.Color.White;
            this.ucControlBase3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase3.IsRadius = true;
            this.ucControlBase3.IsShowRect = true;
            this.ucControlBase3.IsShowShadow = false;
            this.ucControlBase3.Location = new System.Drawing.Point(25, 264);
            this.ucControlBase3.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase3.Name = "ucControlBase3";
            this.ucControlBase3.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase3.RectWidth = 1;
            this.ucControlBase3.Size = new System.Drawing.Size(135, 30);
            this.ucControlBase3.TabIndex = 8;
            // 
            // ucSplitLine_V2
            // 
            this.ucSplitLine_V2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ucSplitLine_V2.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucSplitLine_V2.Location = new System.Drawing.Point(90, 1);
            this.ucSplitLine_V2.Name = "ucSplitLine_V2";
            this.ucSplitLine_V2.Size = new System.Drawing.Size(1, 28);
            this.ucSplitLine_V2.TabIndex = 4;
            this.ucSplitLine_V2.TabStop = false;
            // 
            // btnNow
            // 
            this.btnNow.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnNow.Location = new System.Drawing.Point(46, 1);
            this.btnNow.Name = "btnNow";
            this.btnNow.Size = new System.Drawing.Size(44, 28);
            this.btnNow.TabIndex = 1;
            this.btnNow.Text = "现在";
            this.btnNow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
            // 
            // ucSplitLine_V1
            // 
            this.ucSplitLine_V1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ucSplitLine_V1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucSplitLine_V1.Location = new System.Drawing.Point(45, 1);
            this.ucSplitLine_V1.Name = "ucSplitLine_V1";
            this.ucSplitLine_V1.Size = new System.Drawing.Size(1, 28);
            this.ucSplitLine_V1.TabIndex = 3;
            this.ucSplitLine_V1.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnClear.Location = new System.Drawing.Point(1, 1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(44, 28);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "清空";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(90, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(44, 28);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ucControlBase2
            // 
            this.ucControlBase2.ConerRadius = 1;
            this.ucControlBase2.Controls.Add(this.ListMinute);
            this.ucControlBase2.Controls.Add(this.listHour);
            this.ucControlBase2.FillColor = System.Drawing.Color.White;
            this.ucControlBase2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucControlBase2.IsRadius = true;
            this.ucControlBase2.IsShowRect = true;
            this.ucControlBase2.IsShowShadow = false;
            this.ucControlBase2.Location = new System.Drawing.Point(10, 71);
            this.ucControlBase2.Margin = new System.Windows.Forms.Padding(0);
            this.ucControlBase2.Name = "ucControlBase2";
            this.ucControlBase2.Padding = new System.Windows.Forms.Padding(1);
            this.ucControlBase2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucControlBase2.RectWidth = 1;
            this.ucControlBase2.Size = new System.Drawing.Size(150, 180);
            this.ucControlBase2.TabIndex = 7;
            // 
            // ucSplitLine_H1
            // 
            this.ucSplitLine_H1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ucSplitLine_H1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucSplitLine_H1.Location = new System.Drawing.Point(1, 45);
            this.ucSplitLine_H1.Name = "ucSplitLine_H1";
            this.ucSplitLine_H1.Size = new System.Drawing.Size(168, 1);
            this.ucSplitLine_H1.TabIndex = 1;
            this.ucSplitLine_H1.TabStop = false;
            // 
            // frmSelectTime1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(170, 310);
            this.ControlBox = false;
            this.Controls.Add(this.ucControlBase1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSelectTime1";
            this.Text = "frmSelectTime";
            this.Load += new System.EventHandler(this.frmSelectTime_Load);
            this.ucControlBase1.ResumeLayout(false);
            this.ucControlBase1.PerformLayout();
            this.ucControlBase3.ResumeLayout(false);
            this.ucControlBase2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox ListMinute;
        private System.Windows.Forms.ListBox listHour;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private WinDoControls.Controls.WDCtrlBase ucControlBase1;
        private WinDoControls.Controls.WDCtrlBase ucControlBase2;
        private WinDoControls.Controls.WDSplitLine_H ucSplitLine_H1;
        private WinDoControls.Controls.WDCtrlBase ucControlBase3;
        private WinDoControls.Controls.WDSplitLine_V ucSplitLine_V2;
        private System.Windows.Forms.Label btnNow;
        private WinDoControls.Controls.WDSplitLine_V ucSplitLine_V1;
        private System.Windows.Forms.Label btnClear;
        private System.Windows.Forms.Label btnOK;
    }
}