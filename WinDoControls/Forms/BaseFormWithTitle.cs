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

namespace WinDoControls.Forms
{
    public partial class BaseFormWithTitle : FrmBase
    {
        public BaseFormWithTitle()
        {
            InitializeComponent();
            IsShowShadowForm = true;
            InitFormMove(this.panelTitle);
            base.ShowInTaskbar = false;
            WindowState = FormWindowState.Normal;           
            BorderStyleColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            lblClose.Click += new EventHandler(lblClose_Click);
            ControlHelper.SetCloseBackColor(lblClose);
        }

        void lblClose_Click(object sender, EventArgs e)
        {
            CloseWindow();
        }
        public virtual void CloseWindow()
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
