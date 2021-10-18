














using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{
    
    
    
    
    
    partial class WDTextBoxEx
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
            this.btnSearch = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInput.DecLength = 2;
            this.txtInput.Font = WDFonts.TextFont;// new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtInput.ForeColor = System.Drawing.Color.Black;
            this.txtInput.InputType = WinDoControls.TextInputType.NotControl;
            this.txtInput.Location = new System.Drawing.Point(5, 10);
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
            this.txtInput.PromptColor = System.Drawing.Color.Gray;
            this.txtInput.PromptFont = WDFonts.TextFont;// new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtInput.PromptText = "";
            this.txtInput.RegexPattern = "";
            this.txtInput.Size = new System.Drawing.Size(254, 19);
            this.txtInput.TabIndex = 0;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::WinDoControls.Properties.Resources.ic_search_black_24dp;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.Location = new System.Drawing.Point(246, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(17, 38);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Visible = false;
            this.btnSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSearch_MouseDown);
            // 
            // UCTextBoxEx
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.ConerRadius = 5;
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtInput);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Font = WDFonts.TextFont15;// new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.IsRadius = true;
            this.IsShowRect = true;
            this.Name = "UCTextBoxEx";
            this.Size = new System.Drawing.Size(263, 38);
            this.Load += new System.EventHandler(this.UCTextBoxEx_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UCTextBoxEx_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        
        
        
        
        public TextBoxEx txtInput;
        
        
        
        
        
        private System.Windows.Forms.Panel btnSearch;


    }
}
