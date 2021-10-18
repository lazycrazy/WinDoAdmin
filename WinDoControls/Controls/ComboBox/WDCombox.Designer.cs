














using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{





    partial class WDCombox
    {



        private System.ComponentModel.IContainer components = null;





        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码





        private void InitializeComponent()
        {
            this.txtInput = new WinDoControls.Controls.TextBoxEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.BackColor = System.Drawing.Color.White;
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInput.DecLength = 2;
            this.txtInput.Font = WDFonts.TextFont;
            this.txtInput.ForeColor = System.Drawing.Color.Black;
            this.txtInput.InputType = WinDoControls.TextInputType.NotControl;
            this.txtInput.Location = new System.Drawing.Point(5, 7);
            this.txtInput.Margin = new System.Windows.Forms.Padding(0);
            this.txtInput.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtInput.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.txtInput.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.txtInput.Name = "txtInput";
            this.txtInput.OldText = null;
            this.txtInput.PromptColor = System.Drawing.Color.Silver;
            this.txtInput.PromptFont = WDFonts.TextFont; // new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtInput.PromptText = "";
            this.txtInput.RegexPattern = "";
            this.txtInput.Size = new System.Drawing.Size(345, 19);
            this.txtInput.TabIndex = 1;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = global::WinDoControls.Properties.Resources.ComboBox;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Location = new System.Drawing.Point(352, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(30, 26);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.click_MouseDown);
            // 
            // UCCombox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ConerRadius = 5;
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.panel1);
            this.IsRadius = true;
            this.IsShowRect = true;
            this.Name = "UCCombox";
            this.Font = WDFonts.TextFont15;
            this.Size = new System.Drawing.Size(383, 32);
            this.Load += new System.EventHandler(this.UCComboBox_Load);
            this.SizeChanged += new System.EventHandler(this.UCComboBox_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.click_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion




        protected System.Windows.Forms.Panel panel1;



        public TextBoxEx txtInput;
    }
}
