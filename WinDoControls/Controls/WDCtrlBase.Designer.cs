














using System.Drawing;
using System.Windows.Forms;
namespace WinDoControls.Controls
{





    partial class WDCtrlBase
    {



        private System.ComponentModel.IContainer components = null;





        protected override void Dispose(bool disposing)
        {
            if (disposing && Parent != null)
            {
                Parent.Paint -= new PaintEventHandler(Parent_Paint);
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码





        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UCControlBase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCControlBase";
            this.Size = new System.Drawing.Size(237, 154);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
