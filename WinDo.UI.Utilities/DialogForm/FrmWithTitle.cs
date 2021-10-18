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
    public partial class FrmWithTitle : FrmBase
    {
        public FrmWithTitle()
        {
            InitializeComponent();
            base.ShowInTaskbar = false;
            InitFormMove(this.panel2);
            WindowState = FormWindowState.Normal;
            IsShowShadowForm = true;
            btnClose.Click += new EventHandler(btnClose_Click);
            panel2.Paint += PanelTitle_Paint;
            ControlHelper.SetCloseBackColor(btnClose);
        }
        private void PanelTitle_Paint(object sender, PaintEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_LabelTitle)) return;
            var color = Color.White;
            var rect = panel2.ClientRectangle;
            rect.Offset(10, 0);
            using (var brush = new SolidBrush(color))
                e.Graphics.DrawString(_LabelTitle, WDFonts.TextFont, brush, rect, new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });

        }
        private string _LabelTitle = "提示";
        public FrmWithTitle(string title) : this()
        {
            _LabelTitle = title;
        }

        public string Title
        {
            get { return _LabelTitle; }
            set { _LabelTitle = value; }
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
