using WinDoControls.Controls;

namespace WinDo.UI.Main
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.panelClose = new System.Windows.Forms.Panel();
            this.txtUserName = new WinDoControls.Controls.WDTextBoxTransparent();
            this.txtPassword = new WinDoControls.Controls.WDTextBoxTransparent();
            this.btnLogin = new WinDoControls.Controls.WDBtnSimple();
            this.SuspendLayout();
            // 
            // panelClose
            // 
            this.panelClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelClose.BackColor = System.Drawing.Color.Transparent;
            this.panelClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelClose.Location = new System.Drawing.Point(205, 23);
            this.panelClose.Name = "panelClose";
            this.panelClose.Size = new System.Drawing.Size(26, 26);
            this.panelClose.TabIndex = 0;
            this.panelClose.Text = "    ";
            // 
            // txtUserName
            // 
            this.txtUserName.BackAlpha = 0;
            this.txtUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.DecLength = 2;
            this.txtUserName.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtUserName.ForeColor = System.Drawing.Color.White;
            this.txtUserName.InputType = WinDoControls.TextInputType.NotControl;
            this.txtUserName.Location = new System.Drawing.Point(76, 80);
            this.txtUserName.MaxLength = 10;
            this.txtUserName.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtUserName.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.txtUserName.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.OldText = null;
            this.txtUserName.PromptColor = System.Drawing.Color.Gray;
            this.txtUserName.PromptFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtUserName.PromptText = "";
            this.txtUserName.RegexPattern = "";
            this.txtUserName.Size = new System.Drawing.Size(130, 19);
            this.txtUserName.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.BackAlpha = 0;
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.DecLength = 2;
            this.txtPassword.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.InputType = WinDoControls.TextInputType.NotControl;
            this.txtPassword.Location = new System.Drawing.Point(76, 133);
            this.txtPassword.MaxLength = 10;
            this.txtPassword.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtPassword.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.txtPassword.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.OldText = null;
            this.txtPassword.PromptColor = System.Drawing.Color.Gray;
            this.txtPassword.PromptFont = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPassword.PromptText = "";
            this.txtPassword.RegexPattern = "";
            this.txtPassword.Size = new System.Drawing.Size(130, 19);
            this.txtPassword.TabIndex = 0;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.ConerRadius = 10;
            this.btnLogin.FillColor = System.Drawing.Color.Transparent;
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnLogin.IsRadius = false;
            this.btnLogin.IsShowRect = false;
            this.btnLogin.IsShowShadow = false;
            this.btnLogin.LabelText = "";
            this.btnLogin.LabelTextColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(46, 217);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Padding = new System.Windows.Forms.Padding(1);
            this.btnLogin.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnLogin.RectWidth = 1;
            this.btnLogin.Size = new System.Drawing.Size(164, 28);
            this.btnLogin.TabIndex = 14;
            // 
            // frmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::WinDo.UI.Main.Properties.Resources.login_shadow;
            this.ClientSize = new System.Drawing.Size(256, 296);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.panelClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogin";
            this.Text = "DoAdmin - 登录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelClose;
        private WinDoControls.Controls.WDTextBoxTransparent txtUserName;
        private WinDoControls.Controls.WDTextBoxTransparent txtPassword;
        private WDBtnSimple btnLogin;
    }
}

