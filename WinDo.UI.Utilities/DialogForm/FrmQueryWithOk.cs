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
    public partial class FrmQueryWithOk : FrmBase
    {
        public FrmQueryWithOk()
        {
            InitializeComponent();
            base.ShowInTaskbar = false;
            base.ProcessDoEnter = false;
            InitFormMove(this.lblTitle);
            SizeChanged += new EventHandler(FrmQueryWithOk_SizeChanged);
            IsShowShadowForm = true;

            btnClose.Click += new EventHandler(btnClose_Click);
            ControlHelper.SetCloseBackColor(btnClose);
        }

        public void SetTitle(string title)
        {
            lblTitle.Text = title;
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }



        void btnOk_BtnClick(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        void FrmQueryWithOk_SizeChanged(object sender, EventArgs e)
        {
            btnOk.Left = (this.Width - btnOk.Width) / 2;
        }
    }   
}
