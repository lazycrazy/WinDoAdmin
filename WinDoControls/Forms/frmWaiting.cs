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
using System.Threading.Tasks;

namespace WinDoControls.Forms
{
    public partial class frmWaiting : Form
    {
        public frmWaiting()
        {
            InitializeComponent();
            base.ShowInTaskbar = false;
            Text = "请稍候";
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;
            this.Opacity = 1;

            Load += new EventHandler(frmWaiting_Load);
        }

        void frmWaiting_Load(object sender, EventArgs e)
        {
            if (_action == null)
                return;
            Task.Factory.StartNew(() =>
            {
                _action?.Invoke();
            }).ContinueWith(a =>
                {
                    if (a.IsFaulted)
                    {
                        WinDo.Utilities.LogHelper.WriteException(a.Exception);
                        this.SafeBeginInvoke(() =>
                        {
                            FrmShadowDialog.ShowErrDialog(this, "执行任务失败，" + a.Exception.InnerException.Message, blnShowCancel: false);
                        });
                    }
                    System.Threading.Thread.Sleep(10);
                    this.SafeBeginInvoke(Close);
                });
        }



        public string TextInfo
        {
            get { return this.ucLoadProgressExt1.Text; }
            set { this.ucLoadProgressExt1.Text = value; }
        }
        private Action _action;
        public frmWaiting(Action action)
            : this()
        {
            _action = action;
        }
    }
}
