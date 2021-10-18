
using WinDoControls.Controls;

namespace WinDo.UI.Main
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            WinDoControls.Controls.WDMenuItemList wdMenuItemList1 = new WinDoControls.Controls.WDMenuItemList();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblMini = new System.Windows.Forms.Label();
            this.panelClose = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.mainMenuBar = new WinDoControls.Controls.WDMenuBar();
            this.panelMenuRight = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLogoff = new WinDoControls.Controls.WDBtnImg2Words();
            this.btnHandRegister = new WinDoControls.Controls.WDBtnImg4WordsYS();
            this.btnRegister = new WinDoControls.Controls.WDBtnImg2WordsYS();
            this.panelTabs = new System.Windows.Forms.FlowLayoutPanel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tablessControl1 = new WinDoControls.Controls.WDTablessControl();
            this.ucSplitLine_H1 = new WinDoControls.Controls.WDSplitLine_H();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelTabMenu = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panelMenuRight.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(1, 1);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panelTitle.Size = new System.Drawing.Size(1384, 24);
            this.panelTitle.TabIndex = 1;
            // 
            // lblMini
            // 
            this.lblMini.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMini.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.lblMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMini.Location = new System.Drawing.Point(1313, 1);
            this.lblMini.Name = "lblMini";
            this.lblMini.Size = new System.Drawing.Size(36, 30);
            this.lblMini.TabIndex = 41;
            this.lblMini.Text = "  ";
            this.lblMini.Click += new System.EventHandler(this.lblMini_Click);
            // 
            // panelClose
            // 
            this.panelClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.panelClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelClose.Location = new System.Drawing.Point(1350, 1);
            this.panelClose.Name = "panelClose";
            this.panelClose.Size = new System.Drawing.Size(36, 30);
            this.panelClose.TabIndex = 42;
            this.panelClose.Text = "  ";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(1, 763);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelBottom.Size = new System.Drawing.Size(1384, 24);
            this.panelBottom.TabIndex = 36;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.panelMenu.Controls.Add(this.mainMenuBar);
            this.panelMenu.Controls.Add(this.panelMenuRight);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenu.Location = new System.Drawing.Point(1, 25);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(1384, 48);
            this.panelMenu.TabIndex = 37;
            // 
            // mainMenuBar
            // 
            this.mainMenuBar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.mainMenuBar.ClientSize = new System.Drawing.Size(891, 48);
            this.mainMenuBar.DisabledTextColor = System.Drawing.Color.Red;
            this.mainMenuBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMenuBar.EnableBorderDrawing = false;
            this.mainMenuBar.EnableHoverBorderDrawing = false;
            this.mainMenuBar.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mainMenuBar.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.mainMenuBar.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mainMenuBar.HoverFont = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mainMenuBar.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mainMenuBar.ItemPaddingH = 28;
            this.mainMenuBar.LeftMargin = 0;
            this.mainMenuBar.MenuItems = wdMenuItemList1;
            this.mainMenuBar.Name = "mainMenuBar";
            this.mainMenuBar.ParentMenu = null;
            this.mainMenuBar.ParentMenuItem = null;
            this.mainMenuBar.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mainMenuBar.ShowInTaskbar = false;
            this.mainMenuBar.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.mainMenuBar.TabStop = false;
            this.mainMenuBar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mainMenuBar.TopMargin = 0;
            // 
            // panelMenuRight
            // 
            this.panelMenuRight.Controls.Add(this.flowLayoutPanel1);
            this.panelMenuRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelMenuRight.Location = new System.Drawing.Point(891, 0);
            this.panelMenuRight.Name = "panelMenuRight";
            this.panelMenuRight.Size = new System.Drawing.Size(493, 48);
            this.panelMenuRight.TabIndex = 39;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.flowLayoutPanel1.Controls.Add(this.btnLogoff);
            this.flowLayoutPanel1.Controls.Add(this.btnHandRegister);
            this.flowLayoutPanel1.Controls.Add(this.btnRegister);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(493, 48);
            this.flowLayoutPanel1.TabIndex = 43;
            // 
            // btnLogoff
            // 
            this.btnLogoff.BackColor = System.Drawing.Color.Transparent;
            this.btnLogoff.BtnBackColor = System.Drawing.Color.Transparent;
            this.btnLogoff.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnLogoff.BtnForeColor = System.Drawing.Color.White;
            this.btnLogoff.BtnText = "注销";
            this.btnLogoff.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLogoff.ConerRadius = 5;
            this.btnLogoff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogoff.FillColor = System.Drawing.Color.Transparent;
            this.btnLogoff.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnLogoff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnLogoff.IconName = "I_logoff";
            this.btnLogoff.Image = ((System.Drawing.Image)(resources.GetObject("btnLogoff.Image")));
            this.btnLogoff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogoff.ImageFontIcons = null;
            this.btnLogoff.IsLink = false;
            this.btnLogoff.IsRadius = true;
            this.btnLogoff.IsShowRect = false;
            this.btnLogoff.IsShowShadow = false;
            this.btnLogoff.IsShowTips = false;
            this.btnLogoff.Location = new System.Drawing.Point(417, 9);
            this.btnLogoff.Margin = new System.Windows.Forms.Padding(0, 9, 4, 0);
            this.btnLogoff.Name = "btnLogoff";
            this.btnLogoff.Padding = new System.Windows.Forms.Padding(4, 0, 9, 0);
            this.btnLogoff.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.btnLogoff.RectWidth = 1;
            this.btnLogoff.Size = new System.Drawing.Size(72, 32);
            this.btnLogoff.TabIndex = 44;
            this.btnLogoff.TabStop = false;
            this.btnLogoff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLogoff.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnLogoff.TipsText = "";
            this.btnLogoff.UseHoverColor = false;
            // 
            // btnHandRegister
            // 
            this.btnHandRegister.BackColor = System.Drawing.Color.Transparent;
            this.btnHandRegister.BtnBackColor = System.Drawing.Color.Transparent;
            this.btnHandRegister.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnHandRegister.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnHandRegister.BtnText = "手工登记";
            this.btnHandRegister.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHandRegister.ConerRadius = 5;
            this.btnHandRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHandRegister.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(196)))), ((int)(((byte)(26)))));
            this.btnHandRegister.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnHandRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnHandRegister.IconName = "I_info";
            this.btnHandRegister.Image = ((System.Drawing.Image)(resources.GetObject("btnHandRegister.Image")));
            this.btnHandRegister.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHandRegister.ImageFontIcons = null;
            this.btnHandRegister.IsLink = false;
            this.btnHandRegister.IsRadius = true;
            this.btnHandRegister.IsShowRect = true;
            this.btnHandRegister.IsShowShadow = false;
            this.btnHandRegister.IsShowTips = false;
            this.btnHandRegister.Location = new System.Drawing.Point(312, 9);
            this.btnHandRegister.Margin = new System.Windows.Forms.Padding(0, 9, 8, 0);
            this.btnHandRegister.Name = "btnHandRegister";
            this.btnHandRegister.Padding = new System.Windows.Forms.Padding(7, 0, 5, 0);
            this.btnHandRegister.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.btnHandRegister.RectWidth = 1;
            this.btnHandRegister.Size = new System.Drawing.Size(97, 32);
            this.btnHandRegister.TabIndex = 43;
            this.btnHandRegister.TabStop = false;
            this.btnHandRegister.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHandRegister.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnHandRegister.TipsText = "";
            this.btnHandRegister.UseHoverColor = false;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.Transparent;
            this.btnRegister.BtnBackColor = System.Drawing.Color.Transparent;
            this.btnRegister.BtnFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnRegister.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRegister.BtnText = "登记";
            this.btnRegister.BtnTextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegister.ConerRadius = 5;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(196)))), ((int)(((byte)(26)))));
            this.btnRegister.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnRegister.IconName = "I_info";
            this.btnRegister.Image = ((System.Drawing.Image)(resources.GetObject("btnRegister.Image")));
            this.btnRegister.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegister.ImageFontIcons = null;
            this.btnRegister.IsLink = false;
            this.btnRegister.IsRadius = true;
            this.btnRegister.IsShowRect = true;
            this.btnRegister.IsShowShadow = false;
            this.btnRegister.IsShowTips = false;
            this.btnRegister.Location = new System.Drawing.Point(228, 9);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(0, 9, 8, 0);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Padding = new System.Windows.Forms.Padding(10, 0, 6, 0);
            this.btnRegister.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(84)))), ((int)(((byte)(235)))));
            this.btnRegister.RectWidth = 1;
            this.btnRegister.Size = new System.Drawing.Size(76, 32);
            this.btnRegister.TabIndex = 42;
            this.btnRegister.TabStop = false;
            this.btnRegister.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegister.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btnRegister.TipsText = "";
            this.btnRegister.UseHoverColor = false;
            // 
            // panelTabs
            // 
            this.panelTabs.BackColor = System.Drawing.Color.White;
            this.panelTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTabs.Location = new System.Drawing.Point(0, 0);
            this.panelTabs.Margin = new System.Windows.Forms.Padding(0);
            this.panelTabs.Name = "panelTabs";
            this.panelTabs.Size = new System.Drawing.Size(1322, 28);
            this.panelTabs.TabIndex = 38;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tablessControl1);
            this.panelMain.Controls.Add(this.ucSplitLine_H1);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(1, 73);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(1);
            this.panelMain.Size = new System.Drawing.Size(1384, 690);
            this.panelMain.TabIndex = 39;
            // 
            // tablessControl1
            // 
            this.tablessControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablessControl1.Location = new System.Drawing.Point(1, 30);
            this.tablessControl1.Name = "tablessControl1";
            this.tablessControl1.SelectedIndex = 0;
            this.tablessControl1.Size = new System.Drawing.Size(1382, 659);
            this.tablessControl1.TabIndex = 41;
            // 
            // ucSplitLine_H1
            // 
            this.ucSplitLine_H1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ucSplitLine_H1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucSplitLine_H1.Location = new System.Drawing.Point(1, 29);
            this.ucSplitLine_H1.Name = "ucSplitLine_H1";
            this.ucSplitLine_H1.Size = new System.Drawing.Size(1382, 1);
            this.ucSplitLine_H1.TabIndex = 39;
            this.ucSplitLine_H1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelTabs);
            this.panel1.Controls.Add(this.panelTabMenu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1382, 28);
            this.panel1.TabIndex = 40;
            // 
            // panelTabMenu
            // 
            this.panelTabMenu.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTabMenu.Location = new System.Drawing.Point(1322, 0);
            this.panelTabMenu.Name = "panelTabMenu";
            this.panelTabMenu.Size = new System.Drawing.Size(60, 28);
            this.panelTabMenu.TabIndex = 39;
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1386, 788);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelClose);
            this.Controls.Add(this.lblMini);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panelMenu.ResumeLayout(false);
            this.panelMenuRight.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Panel panelClose;
        private System.Windows.Forms.Label lblMini;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelMenuRight;
        private WDBtnImg4WordsYS btnHandRegister;
        private WDBtnImg2WordsYS btnRegister;
        private WDBtnImg2Words btnLogoff;
        private System.Windows.Forms.FlowLayoutPanel panelTabs;
        private System.Windows.Forms.Panel panelMain;
        private WinDoControls.Controls.WDSplitLine_H ucSplitLine_H1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelTabMenu;
        private WinDoControls.Controls.WDMenuBar mainMenuBar;
        private WDTablessControl tablessControl1;
    }
}

