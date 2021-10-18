using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Forms;
using WinDo.Utilities.PublicResource;
using WinDoControls;

namespace WinDo.UI.Utilities.DialogForm
{
    public partial class FrmWith2WordsOk : FrmBase
    {
        public FrmWith2WordsOk()
        {
            InitializeComponent();
            base.ShowInTaskbar = false;
            InitFormMove(this.lblTitle);
            SizeChanged += new EventHandler(FrmQueryWithOk_SizeChanged);
            WindowState = FormWindowState.Normal;
            IsShowShadowForm = true;
            btnClose.Click += new EventHandler(btnClose_Click);
            BorderStyleColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            ControlHelper.SetCloseBackColor(btnClose);
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
                  

        void FrmQueryWithOk_SizeChanged(object sender, EventArgs e)
        {
            btnOk.Left = (this.Width - btnOk.Width) / 2;
        }
    }
}
