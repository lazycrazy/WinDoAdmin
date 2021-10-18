using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text; 
using WinDoControls.Forms;
namespace WinDoControls.Controls
{
    //private FrmAnchor _frmAnchor;
    //private void button3_Click(object sender, EventArgs e)
    //{
    //    if (_frmAnchor == null || _frmAnchor.IsDisposed || _frmAnchor.Visible == false)
    //    {
    //        if (_frmAnchor != null && !_frmAnchor.IsDisposed)
    //        {
    //            _frmAnchor.Close();
    //            _frmAnchor = null;
    //        }
    //        var frm = new frmSelectTime1(0, 0);
    //        frm.TopLevel = false;
    //        _frmAnchor = new FrmAnchor(button3, frm);
    //        frm.FormClosed += (ss, ee) =>
    //        {
    //            //设置时间
    //            if (frm.Time.Length > 0)
    //                MessageBox.Show(frm.Time);
    //            if (_frmAnchor == null || _frmAnchor.IsDisposed) return;
    //            _frmAnchor.Close(); _frmAnchor = null;
    //        };
    //        //frmAnchor.VisibleChanged += FrmAnchor_VisibleChanged;
    //        frm.Show();
    //        _frmAnchor.Show();
    //    }
    //    else
    //    {
    //        _frmAnchor.Close();
    //        _frmAnchor = null;
    //    }
    //}
    public partial class frmSelectTime : System.Windows.Forms.Form
    {

        public string MaxTime;// 最大时间10:12
        public string MinTime;// 最小时间11:01
        public frmSelectTime(int x, int y)
        {
            InitializeComponent();
            base.ShowInTaskbar = false;
            listHour.Font = WinDo.Utilities.PublicResource.WDFonts.TextFont;
            ListMinute.Font = WinDo.Utilities.PublicResource.WDFonts.TextFont;
            listHour.SelectionMode = System.Windows.Forms.SelectionMode.One;
            ListMinute.SelectionMode = System.Windows.Forms.SelectionMode.One;
            ListMinute.SelectedIndexChanged += ListMinute_SelectedIndexChanged;
            listHour.SelectedIndexChanged += ListMinute_SelectedIndexChanged;
        }

        private void ListMinute_SelectedIndexChanged(object sender, EventArgs e)
        {
            var o = sender as System.Windows.Forms.ListBox;
            o.Invalidate();
        }

        private string mTime = "";

        /// <summary>
        /// 返回预约房间
        /// </summary>
        public string Time
        {
            get
            {
                return mTime;
            }
            set
            {
                mTime = value;
            }
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            //listHour.SelectedIndex = -1;
            //ListMinute.SelectedIndex = -1;

            mTime = "";
            this.Close();
            return;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listHour.SelectedIndex == -1 || ListMinute.SelectedIndex == -1)
            {
                //FrmShadowDialog.ShowErrDialog(this, "请选择时间", "提示", false, false, false, true, new Size(-111, -3), IsLogin: true);
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                mTime = "";
                this.Close();
                return;
            }

            mTime = listHour.Text.ToString() + ":" + ListMinute.Text.ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnNow_Click(object sender, EventArgs e)
        {
            mTime = DateTime.Now.ToString("HH:mm");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void frmSelectTime_Load(object sender, EventArgs e)
        {
        }

        private void listHour_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            var ls = sender as System.Windows.Forms.ListBox;
            using (var brush = new SolidBrush((ls.SelectedIndex == e.Index) ? (WinDo.Utilities.PublicResource.WDColors.geekblue6) : Color.White))
                e.Graphics.FillRectangle(brush, e.Bounds);
            StringFormat strFmt = new System.Drawing.StringFormat();
            strFmt.Alignment = StringAlignment.Center; //文本垂直居中
            strFmt.LineAlignment = StringAlignment.Center; //文本水平居中
            using (var brush = new SolidBrush(ls.SelectedIndex == e.Index ? Color.White : Color.Black))
                e.Graphics.DrawString(ls.Items[e.Index].ToString(), e.Font, brush, e.Bounds, strFmt);
        }

        private void listHour_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var ls = sender as System.Windows.Forms.ListBox;
            ls.SelectedIndex = -1;
            for (int i = 0; i < ls.Items.Count; i++)
            {
                var item = ls.GetItemRectangle(i);
                if (item.Contains(e.Location))
                {
                    ls.SelectedIndex = i;
                }
            }
            if (sender == this.listHour && this.ListMinute.SelectedIndex < 0)
            {
                this.ListMinute.SelectedIndex = 0;
            }
        }
    }


}
